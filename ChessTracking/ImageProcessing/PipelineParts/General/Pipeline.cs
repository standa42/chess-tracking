using System.Collections.Concurrent;
using System.Drawing;
using System.Threading;
using System.Threading.Tasks;
using ChessTracking.ImageProcessing.PipelineData;
using ChessTracking.MultithreadingMessages;

namespace ChessTracking.ImageProcessing.PipelineParts
{
    /// <summary>
    /// Maintains all processing of chessboard tracking
    /// </summary>
    class Pipeline
    {
        public BlockingCollection<Message> ProcessingOutputQueue { get; }
        public VisualisationType VisualisationType { get; set; }

        private Bitmap VisualisationBitmap { get; set; }
        private bool IsTracking { get; set; }
        private PlaneLocalization PlaneLocalization { get; set; }
        private ChessboardLocalization ChessboardLocalization { get; set; }
        private FiguresLocalization FiguresLocalization { get; set; }
        private SemaphoreSlim Semaphore { get; } = new SemaphoreSlim(2);

        public Pipeline(BlockingCollection<Message> processingOutputQueue)
        {
            ProcessingOutputQueue = processingOutputQueue;
            VisualisationType = VisualisationType.RawRGB;
            IsTracking = false;
            PlaneLocalization = new PlaneLocalization(this);
            ChessboardLocalization = new ChessboardLocalization(this);
            FiguresLocalization = new FiguresLocalization();
        }

        public void ChangeVisualisationState(VisualisationType visualisationType)
        {
            VisualisationType = visualisationType;
        }

        public void ChangeColorCalibration(double additiveConstant)
        {
            FiguresLocalization.ChangeColorCalibration(additiveConstant);
        }
        
        public void ProcessIncomingKinectData(KinectResourcesMessage resources)
        {
            var rawData = resources.Data;

            if (!IsTracking)
            {
                Calibration(rawData);
                IsTracking = true;
            }
            else
            {
                Tracking(rawData);
            }
        }

        private void Calibration(KinectData kinectData)
        {
            var planeData = PlaneLocalization.Recalibrate(kinectData);
            var chessboardData = ChessboardLocalization.Recalibrate(planeData);
            var figuresData = FiguresLocalization.Recalibrate(chessboardData);
        }

        private void Tracking(KinectData kinectData)
        {
            Semaphore.Wait();
            Task.Run(() =>
            {
                var planeData = PlaneLocalization.Track(kinectData);
                var chessboardData = ChessboardLocalization.Track(planeData);
                var figuresData = FiguresLocalization.Track(chessboardData);
                SendResultMessage(
                    new ResultMessage(figuresData.ResultData.VisualisationBitmap, figuresData.ResultData.TrackingState, figuresData.ResultData.HandDetected)
                );
                Semaphore.Release();
            });
        }

        public void Recalibrate()
        {
            IsTracking = false;
        }

        private void SendResultMessage(Message msg)
        {
            ProcessingOutputQueue.Add(msg);
        }
    }
}
