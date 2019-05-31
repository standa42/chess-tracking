using Microsoft.Kinect;

namespace ChessTracking.ImageProcessing.PlaneAlgorithms
{
    /// <summary>
    /// Custom camera space point structure with additional flag
    /// </summary>
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