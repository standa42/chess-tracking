using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Kinect;

namespace ChessTracking.MultithreadingMessages
{
    /// <summary>
    /// Message with data resources from sensor
    /// </summary>
    class KinectResourcesMessage : Message
    {
        public byte[] ColorFrameData { get; set; }
        public ushort[] DepthData { get; set; }
        public ushort[] InfraredData { get; set; }
        public CameraSpacePoint[] CameraSpacePointsFromDepthData { get; set; }
        public DepthSpacePoint[] PointsFromColorToDepth { get; set; }
        public ColorSpacePoint[] PointsFromDepthToColor { get; set; }

        public KinectResourcesMessage(
            byte[] colorFrameData,
            ushort[] depthData,
            ushort[] infraredData,
            CameraSpacePoint[] cameraSpacePointsFromDepthData,
            DepthSpacePoint[] pointsFromColorToDepth,
            ColorSpacePoint[] pointsFromDepthToColor
        )
        {
            this.ColorFrameData = colorFrameData;
            this.DepthData = depthData;
            this.InfraredData = infraredData;
            this.CameraSpacePointsFromDepthData = cameraSpacePointsFromDepthData;
            this.PointsFromColorToDepth = pointsFromColorToDepth;
            this.PointsFromDepthToColor = pointsFromDepthToColor;
        }
    }
}
