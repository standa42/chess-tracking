using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using Accord;
using ChessboardTrackingOctoberEdition.Visualisers;
using Microsoft.Kinect;

namespace ChessboardTrackingOctoberEdition.Kinect
{
    class RawKinect : IKinect
    {
        public event KinectMultiframeArrived KinectMultiframeArrived;
        public CancellationToken Token { get; set; }

        private KinectSensor kinectSensor;
        private MultiSourceFrameReader reader;
        private CoordinateMapper coordinateMapper;
        public FrameDescription ColorFrameDescription { get; }
        public FrameDescription DepthFrameDescription { get; }
        public FrameDescription InfraredFrameDescription { get; }

        public RawKinect()
        {
            kinectSensor = KinectSensor.GetDefault();

            reader = kinectSensor.OpenMultiSourceFrameReader(FrameSourceTypes.Color | FrameSourceTypes.Depth | FrameSourceTypes.Infrared);
            reader.MultiSourceFrameArrived += MultisourceFrameArrived;

            coordinateMapper = kinectSensor.CoordinateMapper;

            ColorFrameDescription = kinectSensor.ColorFrameSource.FrameDescription;
            DepthFrameDescription = kinectSensor.DepthFrameSource.FrameDescription;
            InfraredFrameDescription = kinectSensor.InfraredFrameSource.FrameDescription;

            kinectSensor.Open();
        }

        private void MultisourceFrameArrived(object sender, MultiSourceFrameArrivedEventArgs e)
        {
            if (Token != null && Token.IsCancellationRequested)
            {
                Dispose();
                Token.ThrowIfCancellationRequested();
            }

            // acquire frame data
            MultiSourceFrame multiSourceFrame = e.FrameReference.AcquireFrame();

            // If the Frame has expired by the time we process this event, return.
            if (multiSourceFrame == null)
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
                    coordinateMapper.MapColorFrameToDepthSpaceUsingIntPtr(
                        depthFrameData.UnderlyingBuffer,
                        depthFrameData.Size,
                        pointsFromColorToDepth);

                    coordinateMapper.MapDepthFrameToColorSpaceUsingIntPtr(
                        depthFrameData.UnderlyingBuffer,
                        depthFrameData.Size,
                        pointsFromDepthToColor);

                    coordinateMapper.MapDepthFrameToCameraSpaceUsingIntPtr(
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
                    KinectMultiframeArrived?.Invoke(
                        colorFrameData,
                        depthData,
                        infraredData,
                        cameraSpacePointsFromDepthData,
                        pointsFromColorToDepth,
                        pointsFromDepthToColor
                    );
                }
                
            }
        }

        public void Dispose()
        {
            Task.Run( () =>
            {
                if (kinectSensor != null)
                {
                    kinectSensor.Close();
                    kinectSensor = null;
                }

                if (reader != null)
                {
                    reader.Dispose();
                    reader = null;
                }
                
            }
            );
        }

    }
}
