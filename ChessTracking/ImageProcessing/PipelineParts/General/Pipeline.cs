using System;
using System.Collections.Concurrent;
using System.Threading;
using System.Threading.Tasks;
using ChessTracking.ImageProcessing.PipelineData;
using ChessTracking.ImageProcessing.PipelineParts.Stages;
using ChessTracking.MultithreadingMessages;

namespace ChessTracking.ImageProcessing.PipelineParts.General
{
    /// <summary>
    /// Maintains all processing of chessboard tracking
    /// </summary>
    class Pipeline
    {
        public BlockingCollection<Message> ProcessingOutputQueue { get; }
        
        private bool IsTracking { get; set; }
        private PlaneLocalization PlaneLocalization { get; set; }
        private ChessboardLocalization ChessboardLocalization { get; set; }
        private FiguresLocalization FiguresLocalization { get; set; }
        private SemaphoreSlim Semaphore { get; } = new SemaphoreSlim(1);
        private UserDefinedParametersPrototypeFactory UserParameters { get; set; }
        private KinectDataBuffer Buffer { get; set; }

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

        public void Recalibrate()
        {
            IsTracking = false;
        }

        public void Update()
        {
            if (!IsTracking)
            {
                Calibration();
                IsTracking = true;
            }
            else
            {
                Tracking();
            }
        }

        private void Calibration()
        {
            var data = Buffer.Take();

            var inputData = new InputData(data, UserParameters.GetShallowCopy());

            var planeData = PlaneLocalization.Recalibrate(inputData);
            var chessboardData = ChessboardLocalization.Recalibrate(planeData);
            var figuresData = FiguresLocalization.Recalibrate(chessboardData);
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
            SendResultMessage(
                new ResultMessage(figuresData.ResultData.VisualisationBitmap, figuresData.ResultData.TrackingState, figuresData.ResultData.HandDetected)
            );
        }

        private void SendResultMessage(Message msg)
        {
            ProcessingOutputQueue.Add(msg);
        }
    }
}
