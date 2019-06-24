using System;
using System.Collections.Concurrent;
using System.Drawing;
using System.Threading;
using System.Threading.Tasks;
using ChessTracking.ImageProcessing.PipelineData;
using ChessTracking.ImageProcessing.PipelineParts.Stages;
using ChessTracking.ImageProcessing.PipelineParts.StagesInterfaces;
using ChessTracking.MultithreadingMessages;
using ChessTracking.MultithreadingMessages.FromProcessing;
using ChessTracking.MultithreadingMessages.ToProcessing;
using ChessTracking.Utils;

namespace ChessTracking.ImageProcessing.PipelineParts.General
{
    /// <summary>
    /// Maintains all processing of chessboard tracking
    /// </summary>
    class Pipeline
    {
        public BlockingCollection<Message> ProcessingOutputQueue { get; }
        private UserDefinedParametersPrototypeFactory UserParametersFactory { get; }
        private KinectDataBuffer Buffer { get; set; }

        private IPlaneLocalization PlaneLocalization { get; }
        private IChessboardLocalization ChessboardLocalization { get; }
        private IFiguresLocalization FiguresLocalization { get; }

        private UserDefinedParameters UserParameters { get; set; }
        private DateTime LastReleasedTrackingTask { get; set; } = DateTime.MinValue;

        /// <summary>
        /// Indicates calibration/tracking state
        /// </summary>
        private bool IsTracking { get; set; }
        /// <summary>
        /// Indicated, whether there was exception in calibration causing end of tracking
        /// </summary>
        private bool TrackingCanceled { get; set; }
        private SemaphoreSlim Semaphore { get; } = new SemaphoreSlim(1);
        
        public Pipeline(BlockingCollection<Message> processingOutputQueue, UserDefinedParametersPrototypeFactory userParametersFactory)
        {
            ProcessingOutputQueue = processingOutputQueue;
            UserParametersFactory = userParametersFactory;
            UserParameters = userParametersFactory.GetShallowCopy();
            IsTracking = false;
            PlaneLocalization = new PlaneLocalization(this);
            ChessboardLocalization = new ChessboardLocalization();
            FiguresLocalization = new FiguresLocalization();
        }

        public void SetBuffer(KinectDataBuffer buffer)
        {
            Buffer = buffer;
        }

        public void ResetCalibration()
        {
            IsTracking = false;
            TrackingCanceled = false;
        }

        public void MoveChessboard(ChessboardMovement movement)
        {
            ChessboardLocalization.MoveChessboard(movement);
        }

        /// <summary>
        /// Entry point of pipeline processing, decides whether calibrate or track
        /// </summary>
        public void Update()
        {
            if (TrackingCanceled)
                return;

            if (!IsTracking)
            {
                try
                {
                    Calibration();
                    IsTracking = true;
                }
                catch (Exception e)
                {
                    TrackingCanceled = true;
                    if (e is TimeoutException)
                        SendResultMessageToUserThread(new TrackingError("Calibration - no kinect data - check connection of sensor"));
                    else
                    {
                        SendResultMessageToUserThread(new TrackingError("Calibration threw an exception"));
                    }

                }
            }
            else
            {
                Tracking();
            }
        }

        private void Calibration()
        {
            // if data arrive in 10 seconds, there is probably something wrong
            var data = Buffer.TryTake(10000);

            if(data == null)
                throw new TimeoutException();
            
            UserParameters = UserParametersFactory.GetShallowCopy();
            var inputData = new InputData(data, UserParameters);

            var planeData = PlaneLocalization.Calibrate(inputData);
            var chessboardData = ChessboardLocalization.Calibrate(planeData, ProcessingOutputQueue);
            
            var figuresData = FiguresLocalization.Calibrate(chessboardData);

            SendResultMessageToUserThread(new TrackingStartSuccessfulMessage());
        }

        private void Tracking()
        {
            PipelineSlowdown();

            Semaphore.Wait();

            LastReleasedTrackingTask = DateTime.Now;

            Task.Run(() =>
            {
                try
                {
                    TrackingImplementation();
                }
                catch (Exception)
                {
                    // ignored
                }
                finally
                {
                    Semaphore.Release();
                }
            });
        }

        private void TrackingImplementation()
        {
            var data = Buffer.TryTake();

            if (data == null)
                return;

            UserParameters = UserParametersFactory.GetShallowCopy();
            var inputData = new InputData(data, UserParameters);

            var planeData = PlaneLocalization.Track(inputData);
            var chessboardData = ChessboardLocalization.Track(planeData);
            var figuresData = FiguresLocalization.Track(chessboardData);

            SendResultMessageToUserThread(
                new ResultMessage(figuresData.ResultData.VisualisationBitmap.HorizontalFlip(), figuresData.ResultData.TrackingState, figuresData.ResultData.SceneDisrupted)
            );
        }

        /// <summary>
        /// If next release of task is too soon, sleep for reasonable time
        /// </summary>
        private void PipelineSlowdown()
        {
            var milisecondsSinceLastTaskRelease = (DateTime.Now - LastReleasedTrackingTask).Milliseconds;
            if (milisecondsSinceLastTaskRelease < UserParameters.MinimalTimeBetweenTrackingTasksInMiliseconds)
                Thread.Sleep(UserParameters.MinimalTimeBetweenTrackingTasksInMiliseconds - milisecondsSinceLastTaskRelease);
        }

        private void SendResultMessageToUserThread(Message msg)
        {
            ProcessingOutputQueue.Add(msg);
        }
    }
}
