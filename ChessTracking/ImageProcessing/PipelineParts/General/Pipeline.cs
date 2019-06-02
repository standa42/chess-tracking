using System;
using System.Collections.Concurrent;
using System.Threading;
using System.Threading.Tasks;
using ChessTracking.ImageProcessing.PipelineData;
using ChessTracking.ImageProcessing.PipelineParts.Stages;
using ChessTracking.ImageProcessing.PipelineParts.StagesInterfaces;
using ChessTracking.MultithreadingMessages;
using ChessTracking.MultithreadingMessages.FromProcessing;

namespace ChessTracking.ImageProcessing.PipelineParts.General
{
    /// <summary>
    /// Maintains all processing of chessboard tracking
    /// </summary>
    class Pipeline
    {
        public BlockingCollection<Message> ProcessingOutputQueue { get; }
        private UserDefinedParametersPrototypeFactory UserParameters { get; }
        private KinectDataBuffer Buffer { get; set; }

        private IPlaneLocalization PlaneLocalization { get; }
        private IChessboardLocalization ChessboardLocalization { get; }
        private IFiguresLocalization FiguresLocalization { get; }

        /// <summary>
        /// Indicates calibration/tracking state
        /// </summary>
        private bool IsTracking { get; set; }
        /// <summary>
        /// Indicated, whether there was exception in calibration causing end of tracking
        /// </summary>
        private bool TrackingCanceled { get; set; }
        private SemaphoreSlim Semaphore { get; } = new SemaphoreSlim(1);
        
        public Pipeline(BlockingCollection<Message> processingOutputQueue, UserDefinedParametersPrototypeFactory userParameters)
        {
            ProcessingOutputQueue = processingOutputQueue;
            UserParameters = userParameters;
            IsTracking = false;
            PlaneLocalization = new PlaneLocalization(this);
            ChessboardLocalization = new ChessboardLocalization(this);
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
                    if(e is TimeoutException)
                        SendResultMessageToUserThread(new TrackingError("Calibration - no kinect data - check connection of sensor"));
                    else
                        SendResultMessageToUserThread(new TrackingError("Calibration threw an exception"));
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

            var inputData = new InputData(data, UserParameters.GetShallowCopy());

            var planeData = PlaneLocalization.Calibrate(inputData);
            var chessboardData = ChessboardLocalization.Calibrate(planeData);
            var figuresData = FiguresLocalization.Calibrate(chessboardData);

            SendResultMessageToUserThread(new TrackingStartSuccessfulMessage());
        }

        private void Tracking()
        {
            Semaphore.Wait();

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

            var inputData = new InputData(data, UserParameters.GetShallowCopy());

            var planeData = PlaneLocalization.Track(inputData);
            var chessboardData = ChessboardLocalization.Track(planeData);
            var figuresData = FiguresLocalization.Track(chessboardData);

            SendResultMessageToUserThread(
                new ResultMessage(figuresData.ResultData.VisualisationBitmap, figuresData.ResultData.TrackingState, figuresData.ResultData.SceneDisrupted)
            );
        }

        private void SendResultMessageToUserThread(Message msg)
        {
            ProcessingOutputQueue.Add(msg);
        }
    }
}
