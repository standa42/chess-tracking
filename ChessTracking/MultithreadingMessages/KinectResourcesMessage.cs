using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Kinect;

namespace ChessTracking.MultithreadingMessages
{
    class KinectResourcesMessage : Message
    {
        public byte[] colorFrameData;
        public ushort[] depthData;
        public ushort[] infraredData;
        public CameraSpacePoint[] cameraSpacePointsFromDepthData;
        public DepthSpacePoint[] pointsFromColorToDepth;
        public ColorSpacePoint[] pointsFromDepthToColor;

        public KinectResourcesMessage(
            byte[] colorFrameData,
            ushort[] depthData,
            ushort[] infraredData,
            CameraSpacePoint[] cameraSpacePointsFromDepthData,
            DepthSpacePoint[] pointsFromColorToDepth,
            ColorSpacePoint[] pointsFromDepthToColor
        )
        {
            this.colorFrameData = colorFrameData;
            this.depthData = depthData;
            this.infraredData = infraredData;
            this.cameraSpacePointsFromDepthData = cameraSpacePointsFromDepthData;
            this.pointsFromColorToDepth = pointsFromColorToDepth;
            this.pointsFromDepthToColor = pointsFromDepthToColor;
        }
    }
}
