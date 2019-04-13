using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Net.Configuration;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using ChessTracking.MultithreadingMessages;

namespace ChessTracking.ProcessingPipeline
{
    class Pipeline
    {
        public BlockingCollection<Message> ProcessingOutputQueue { get; }
        public VisualisationType VisualisationType { get; set; }
        public PipelineCongestionControl CongestionControl { get; set; }

        public AttemptToRecreateOldVisualiser OldVisualiser { get; }

        public Pipeline(BlockingCollection<Message> processingOutputQueue)
        {
            ProcessingOutputQueue = processingOutputQueue;
            CongestionControl = new PipelineCongestionControl();

            OldVisualiser = new AttemptToRecreateOldVisualiser(processingOutputQueue);
        }

        public void ChangeVisualisationState(VisualisationType visualisationType)
        {
            VisualisationType = visualisationType;
        }

        public void ProcessIncomingKinectData(KinectResourcesMessage resources)
        {

            try
            {
                OldVisualiser.Update(
                    resources.colorFrameData,
                    resources.depthData,
                    resources.infraredData,
                    resources.cameraSpacePointsFromDepthData,
                    resources.pointsFromColorToDepth,
                    resources.pointsFromDepthToColor
                );
            }
            catch (Exception e)
            {
                var x = 110;
            }
            

            //Bitmap bmp = new Bitmap(1920, 1080, PixelFormat.Format32bppArgb);
            //BitmapData bmpData = bmp.LockBits(new Rectangle(0, 0,
            //        bmp.Width,
            //        bmp.Height),
            //    ImageLockMode.WriteOnly,
            //    bmp.PixelFormat);

            //IntPtr pNative = bmpData.Scan0;
            //Marshal.Copy(resources.colorFrameData, 0, pNative, resources.colorFrameData.Length);

            //bmp.UnlockBits(bmpData);

            //SendResultMessage(new ResultMessage(bmp, null));
        }

        public void Recalibrate()
        {
            OldVisualiser.Recalibrate();
        }

        private void SendResultMessage(Message msg)
        {
            ProcessingOutputQueue.Add(msg);
        }
    }
}
