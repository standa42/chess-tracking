using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Net.Configuration;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using ChessTracking.MultithreadingMessages;
using ChessTracking.Utils;

namespace ChessTracking.ProcessingPipeline
{
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
            FiguresLocalization = new FiguresLocalization(this);
        }

        public void ChangeVisualisationState(VisualisationType visualisationType)
        {
            VisualisationType = visualisationType;
        }

        public void ProcessIncomingKinectData(KinectResourcesMessage resources)
        {
            var rawData = new RawData(resources, VisualisationType);
            if (!IsTracking)
            {
                var planeData = PlaneLocalization.Recalibrate(rawData);
                var chessboardData = ChessboardLocalization.Recalibrate(planeData);
                var figuresData = FiguresLocalization.Recalibrate(chessboardData);
                IsTracking = true;
            }
            else
            {
                Semaphore.Wait();
                Task.Run(() =>
                {
                    var planeData = PlaneLocalization.Track(rawData);
                    var chessboardData = ChessboardLocalization.Track(planeData);
                    var figuresData = FiguresLocalization.Track(chessboardData);
                    SendResultMessage(
                            new ResultMessage(figuresData.Bitmap, figuresData.TrackingState, figuresData.HandDetected)
                        );
                    Semaphore.Release();
                });
            }

        }
        
        public void SetVisualisationBitmap(Bitmap bm)
        {
            VisualisationBitmap = bm;
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
