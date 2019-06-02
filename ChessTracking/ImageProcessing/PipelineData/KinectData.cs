using Microsoft.Kinect;

namespace ChessTracking.ImageProcessing.PipelineData
{
    /// <summary>
    /// Contains all important data from sensor
    /// </summary>
    class KinectData
    {
        public byte[] ColorFrameData { get; set; }
        public ushort[] DepthData { get; set; }
        public ushort[] InfraredData { get; set; }
        public CameraSpacePoint[] CameraSpacePointsFromDepthData { get; set; }
        public DepthSpacePoint[] PointsFromColorToDepth { get; set; }
        public ColorSpacePoint[] PointsFromDepthToColor { get; set; }

        public KinectData(
            byte[] colorFrameData,
            ushort[] depthData,
            ushort[] infraredData,
            CameraSpacePoint[] cameraSpacePointsFromDepthData,
            DepthSpacePoint[] pointsFromColorToDepth,
            ColorSpacePoint[] pointsFromDepthToColor
        )
        {
            ColorFrameData = colorFrameData;
            DepthData = depthData;
            InfraredData = infraredData;
            CameraSpacePointsFromDepthData = cameraSpacePointsFromDepthData;
            PointsFromColorToDepth = pointsFromColorToDepth;
            PointsFromDepthToColor = pointsFromDepthToColor;
        }
    }
}
