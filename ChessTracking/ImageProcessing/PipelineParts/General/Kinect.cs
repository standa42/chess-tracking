using System.Collections.Concurrent;
using ChessTracking.ImageProcessing.PipelineData;
using ChessTracking.MultithreadingMessages;
using Microsoft.Kinect;

namespace ChessTracking.ImageProcessing.PipelineParts.General
{
    class Kinect
    {
        public FrameDescription ColorFrameDescription { get; }
        public FrameDescription DepthFrameDescription { get; }
        public FrameDescription InfraredFrameDescription { get; }

        private KinectSensor KinectSensor { get; set; }
        private MultiSourceFrameReader Reader { get; set; }
        private CoordinateMapper CoordinateMapper { get; }

        private KinectDataBuffer Buffer { get; }
        
        public BlockingCollection<Message> OutputQueue { get; }

        public Kinect(BlockingCollection<Message> processingCommandsQueue, KinectDataBuffer buffer)
        {
            OutputQueue = processingCommandsQueue;
            Buffer = buffer;

            KinectSensor = KinectSensor.GetDefault();

            Reader = KinectSensor.OpenMultiSourceFrameReader(FrameSourceTypes.Color | FrameSourceTypes.Depth | FrameSourceTypes.Infrared);
            Reader.MultiSourceFrameArrived += MultisourceFrameArrived;

            CoordinateMapper = KinectSensor.CoordinateMapper;

            ColorFrameDescription = KinectSensor.ColorFrameSource.FrameDescription;
            DepthFrameDescription = KinectSensor.DepthFrameSource.FrameDescription;
            InfraredFrameDescription = KinectSensor.InfraredFrameSource.FrameDescription;

            KinectSensor.Open();
        }
        

        /// <summary>
        /// Procedure invoked by Kinect when new data are available
        /// </summary>
        private void MultisourceFrameArrived(object sender, MultiSourceFrameArrivedEventArgs e)
        {
            if (KinectSensor == null || Reader == null)
            {
                return;
            }

            // acquire frame data
            MultiSourceFrame multiSourceFrame = e.FrameReference.AcquireFrame();

            // if the Frame has expired by the time we process this event, return.
            if (multiSourceFrame == null)
            {
                return;
            }

            // Continue only if buffer is empty
            if (!Buffer.IsEmpty())
            {
                return;
            }

            // declare variables for data from sensor
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
                // get frames from sensor
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

                // send data futher
                if (
                    colorFrameData != null &&
                    depthData != null &&
                    infraredData != null &&
                    cameraSpacePointsFromDepthData != null
                    )
                {
                    // store data to buffer and notify processing thread
                    Buffer.Store(
                        new KinectData(
                            colorFrameData,
                            depthData,
                            infraredData,
                            cameraSpacePointsFromDepthData,
                            pointsFromColorToDepth,
                            pointsFromDepthToColor
                        )
                    );

                    OutputQueue.Add(new KinectUpdateMessage());
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
