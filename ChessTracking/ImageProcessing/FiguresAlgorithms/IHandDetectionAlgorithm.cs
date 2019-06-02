using Microsoft.Kinect;

namespace ChessTracking.ImageProcessing.FiguresAlgorithms
{
    interface IHandDetectionAlgorithm
    {
        bool HandDetected(CameraSpacePoint[] cameraSpacePointsFromDepthData, double fieldSize);
    }
}
