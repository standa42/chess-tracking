using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using ChessTracking.MultithreadingMessages;

namespace ChessTracking.ProcessingElements
{
    class ProcessingController
    {
        public BlockingCollection<Message> ProcessingCommandsQueue { get; }
        public BlockingCollection<Message> ProcessingOutputQueue { get; }
        public Kinect Kinect;

        public ProcessingController(BlockingCollection<Message> processingCommandsQueue, BlockingCollection<Message> processingOutputQueue)
        {
            this.ProcessingCommandsQueue = processingCommandsQueue;
            this.ProcessingOutputQueue = processingOutputQueue;
            Kinect = new Kinect(processingCommandsQueue);
        }

        public void Start()
        {
            while (!ProcessingCommandsQueue.IsCompleted)
            {
                var message = ProcessingCommandsQueue.Take();

                if (message is KinectResourcesMessage resourcesMessage)
                {
                    Bitmap bmp = new Bitmap(1920, 1080, PixelFormat.Format32bppArgb);
                    BitmapData bmpData = bmp.LockBits(new Rectangle(0, 0,
                            bmp.Width,
                            bmp.Height),
                        ImageLockMode.WriteOnly,
                        bmp.PixelFormat);

                    IntPtr pNative = bmpData.Scan0;
                    Marshal.Copy(resourcesMessage.colorFrameData, 0, pNative, resourcesMessage.colorFrameData.Length);

                    bmp.UnlockBits(bmpData);

                    ProcessingOutputQueue.Add(
                            new ResultMessage(
                                    bmp
                                )
                        );
                }

                if (message is CommandMessage commandMessage)
                {
                    Kinect.Dispose();
                    return;
                }
            }
        }
    }
}
