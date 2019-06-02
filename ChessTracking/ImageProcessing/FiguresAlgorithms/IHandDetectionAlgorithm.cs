using Microsoft.Kinect;

namespace ChessTracking.ImageProcessing.FiguresAlgorithms
{
    /// <summary>
    /// Algorithm providing detection of objects/points in space over chessboard
    /// </summary>
    interface IHandDetectionAlgorithm
    {
        bool HandDetected(CameraSpacePoint[] cameraSpacePointsFromDepthData, double fieldSize);
    }
}
