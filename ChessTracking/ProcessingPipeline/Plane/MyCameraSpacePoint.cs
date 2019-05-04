using Microsoft.Kinect;

namespace ChessTracking.ProcessingPipeline.Plane
{
    public struct MyCameraSpacePoint
    {
        public float X;
        public float Y;
        public float Z;

        public PixelType Type;
        
        public MyCameraSpacePoint(ref CameraSpacePoint point)
        {
            X = point.X;
            Y = point.Y;
            Z = point.Z;

            Type = PixelType.NotMarked;
        }
    }
}