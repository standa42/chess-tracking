using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ChessTracking.MultithreadingMessages;
using Microsoft.Kinect;

namespace ChessTracking.ProcessingElements
{
    class Kinect
    {
        public FrameDescription ColorFrameDescription { get; }
        public FrameDescription DepthFrameDescription { get; }
        public FrameDescription InfraredFrameDescription { get; }

        private KinectSensor KinectSensor { get; set; }
        private MultiSourceFrameReader Reader { get; set; }
        private CoordinateMapper CoordinateMapper { get; }

        public BlockingCollection<Message> OutputQueue { get; }

        public Kinect(BlockingCollection<Message> processingCommandsQueue)
        {
            OutputQueue = processingCommandsQueue;

            KinectSensor = KinectSensor.GetDefault();

            Reader = KinectSensor.OpenMultiSourceFrameReader(FrameSourceTypes.Color | FrameSourceTypes.Depth | FrameSourceTypes.Infrared);
            Reader.MultiSourceFrameArrived += MultisourceFrameArrived;

            CoordinateMapper = KinectSensor.CoordinateMapper;

            ColorFrameDescription = KinectSensor.ColorFrameSource.FrameDescription;
            DepthFrameDescription = KinectSensor.DepthFrameSource.FrameDescription;
            InfraredFrameDescription = KinectSensor.InfraredFrameSource.FrameDescription;

            KinectSensor.Open();
        }

        private void MultisourceFrameArrived(object sender, MultiSourceFrameArrivedEventArgs e)
        {
            if (KinectSensor == null || Reader == null)
            {
                return;
            }

            // acquire frame data
            MultiSourceFrame multiSourceFrame = e.FrameReference.AcquireFrame();

            // If the Frame has expired by the time we process this event, return.
            if (multiSourceFrame == null)
            {
                return;
            }

            if (OutputQueue.Count >= 3)
            {
                return;
            }

            ColorFrame colorFrame = null;
            DepthFrame depthFrame = null;
            InfraredFrame infraredFrame = null;

            byte[] colorFrameData = null;
            ushort[] depthData = null;
            ushort[] infraredData = null;
            DepthSpacePoint[] pointsFromColorToDepth = null;
            ColorSpacePoint[] pointsFromDepthToColor = null;
            CameraSpacePoint[] cameraSpacePointsFromDepthData = null;

            try
            {
                colorFrame = multiSourceFrame.ColorFrameReference.AcquireFrame();
                depthFrame = multiSourceFrame.DepthFrameReference.AcquireFrame();
                infraredFrame = multiSourceFrame.InfraredFrameReference.AcquireFrame();

                // If any frame has expired by the time we process this event, return.
                if (colorFrame == null || depthFrame == null || infraredFrame == null)
                {
                    return;
                }

                // use frame data to fill arrays
                colorFrameData = new byte[ColorFrameDescription.LengthInPixels * 4];
                depthData = new ushort[DepthFrameDescription.LengthInPixels];
                infraredData = new ushort[InfraredFrameDescription.LengthInPixels];

                colorFrame.CopyConvertedFrameDataToArray(colorFrameData, ColorImageFormat.Bgra);
                depthFrame.CopyFrameDataToArray(depthData);
                infraredFrame.CopyFrameDataToArray(infraredData);

                pointsFromColorToDepth = new DepthSpacePoint[ColorFrameDescription.LengthInPixels];
                pointsFromDepthToColor = new ColorSpacePoint[DepthFrameDescription.LengthInPixels];
                cameraSpacePointsFromDepthData = new CameraSpacePoint[DepthFrameDescription.LengthInPixels];

                using (KinectBuffer depthFrameData = depthFrame.LockImageBuffer())
                {
                    CoordinateMapper.MapColorFrameToDepthSpaceUsingIntPtr(
                        depthFrameData.UnderlyingBuffer,
                        depthFrameData.Size,
                        pointsFromColorToDepth);

                    CoordinateMapper.MapDepthFrameToColorSpaceUsingIntPtr(
                        depthFrameData.UnderlyingBuffer,
                        depthFrameData.Size,
                        pointsFromDepthToColor);

                    CoordinateMapper.MapDepthFrameToCameraSpaceUsingIntPtr(
                        depthFrameData.UnderlyingBuffer,
                        depthFrameData.Size,
                        cameraSpacePointsFromDepthData);
                }

                // dispose frames so that Kinect can continue processing
                colorFrame.Dispose();
                depthFrame.Dispose();
                infraredFrame.Dispose();
            }
            finally
            {
                colorFrame?.Dispose();
                depthFrame?.Dispose();
                infraredFrame?.Dispose();

                // invoke observable event
                if (
                    colorFrameData != null &&
                    depthData != null &&
                    infraredData != null &&
                    cameraSpacePointsFromDepthData != null &&
                    pointsFromColorToDepth != null &&
                    pointsFromDepthToColor != null
                    )
                {
                    OutputQueue.Add(
                        new KinectResourcesMessage(
                            colorFrameData,
                            depthData,
                            infraredData,
                            cameraSpacePointsFromDepthData,
                            pointsFromColorToDepth,
                            pointsFromDepthToColor
                            )
                        );
                }

            }
        }

        public void Dispose()
        {
            if (KinectSensor != null)
            {
                KinectSensor.Close();
                KinectSensor = null;
            }

            if (Reader != null)
            {
                Reader.Dispose();
                Reader = null;
            }
        }
    }
}
