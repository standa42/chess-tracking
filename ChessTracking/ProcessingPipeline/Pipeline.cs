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
        public AttemptToRecreateOldVisualiser OldVisualiser { get; }
        private Bitmap VisualisationBitmap { get; set; }
        private bool IsTracking { get; set; }
        private PlaneLocalization PlaneLocalization { get; set; }
        private ChessboardLocalization ChessboardLocalization { get; set; }
        private FiguresLocalization FiguresLocalization { get; set; }
        private SemaphoreSlim Semaphore { get; } = new SemaphoreSlim(2);

        public Pipeline(BlockingCollection<Message> processingOutputQueue)
        {
            ProcessingOutputQueue = processingOutputQueue;
            OldVisualiser = new AttemptToRecreateOldVisualiser(processingOutputQueue);
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
                Preprocessing(rawData);
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
                    Preprocessing(rawData);
                    var planeData = PlaneLocalization.Track(rawData);
                    var chessboardData = ChessboardLocalization.Track(planeData);
                    var figuresData = FiguresLocalization.Track(chessboardData);
                    SendResultMessage(
                            new ResultMessage(figuresData.Bitmap, null)
                        );
                    Semaphore.Release();
                });
            }

            //OldVisualiser.Update(
            //    rawData.ColorFrameData,
            //    rawData.DepthData,
            //    rawData.InfraredData,
            //    rawData.CameraSpacePointsFromDepthData,
            //    rawData.PointsFromColorToDepth,
            //    rawData.PointsFromDepthToColor
            //);

            //Bitmap bmp = new Bitmap(1920, 1080, PixelFormat.Format32bppArgb);
            //BitmapData bmpData = bmp.LockBits(new Rectangle(0, 0,
            //        bmp.Width,
            //        bmp.Height),
            //    ImageLockMode.WriteOnly,
            //    bmp.PixelFormat);

            //IntPtr pNative = bmpData.Scan0;
            //Marshal.Copy(rawData.ColorFrameData, 0, pNative, rawData.ColorFrameData.Length);

            //bmp.UnlockBits(bmpData);

            //SendResultMessage(new ResultMessage(bmp, null));
        }
        private StreamWriter sw = new StreamWriter(@"D:\Desktop\log.txt");
        private void Preprocessing(RawData rawData)
        {
            // TODO: přemístit jinam
            //rawData.ColorFrameData = rawData.ColorFrameData.FlipHorizontally(1920, 1080, 4);
            //rawData.DepthData = rawData.DepthData.FlipHorizontally(512, 424);
            //rawData.InfraredData = rawData.InfraredData.FlipHorizontally(512, 424);
            //rawData.CameraSpacePointsFromDepthData = rawData.CameraSpacePointsFromDepthData.FlipHorizontally(512, 424);
            //rawData.PointsFromColorToDepth = rawData.PointsFromColorToDepth.FlipHorizontally(1920, 1080);
            //rawData.PointsFromDepthToColor = rawData.PointsFromDepthToColor.FlipHorizontally(512, 424);

            //for (int i = 0; i < rawData.CameraSpacePointsFromDepthData.Length; i++)
            //{
            //    rawData.CameraSpacePointsFromDepthData[i].X = -rawData.CameraSpacePointsFromDepthData[i].X;
            //    rawData.PointsFromDepthToColor[i].X = 1920 - rawData.PointsFromDepthToColor[i].X;
            //}

            //for (int i = 0; i < rawData.PointsFromColorToDepth.Length; i++)
            //{
            //    rawData.PointsFromColorToDepth[i].X = 512 - rawData.PointsFromColorToDepth[i].X;
            //}
        }

        public void SetVisualisationBitmap(Bitmap bm)
        {
            VisualisationBitmap = bm;
        }

        public void Recalibrate()
        {
            IsTracking = false;
            //OldVisualiser.Recalibrate();
        }

        private void SendResultMessage(Message msg)
        {
            ProcessingOutputQueue.Add(msg);
        }
    }
}
