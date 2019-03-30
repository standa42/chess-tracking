using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
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

        public Pipeline(BlockingCollection<Message> processingOutputQueue)
        {
            ProcessingOutputQueue = processingOutputQueue;
            CongestionControl = new PipelineCongestionControl();
        }

        public void ChangeVisualisationState(VisualisationType visualisationType)
        {
            VisualisationType = visualisationType;
        }

        public void ProcessIncomingKinectData(KinectResourcesMessage resources)
        {
            Bitmap bmp = new Bitmap(1920, 1080, PixelFormat.Format32bppArgb);
            BitmapData bmpData = bmp.LockBits(new Rectangle(0, 0,
                    bmp.Width,
                    bmp.Height),
                ImageLockMode.WriteOnly,
                bmp.PixelFormat);

            IntPtr pNative = bmpData.Scan0;
            Marshal.Copy(resources.colorFrameData, 0, pNative, resources.colorFrameData.Length);

            bmp.UnlockBits(bmpData);

            SendResultMessage(new ResultMessage(bmp));
        }

        public void Recalibrate()
        {

        }

        private void SendResultMessage(Message msg)
        {
            ProcessingOutputQueue.Add(msg);
        }
    }
}
