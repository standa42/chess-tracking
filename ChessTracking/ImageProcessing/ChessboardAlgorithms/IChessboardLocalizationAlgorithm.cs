using ChessTracking.ImageProcessing.PipelineData;

namespace ChessTracking.ImageProcessing.ChessboardAlgorithms
{
    interface IChessboardLocalizationAlgorithm
    {
        (Chessboard3DReprezentation boardReprezentation, SceneCalibrationSnapshot snapshot) LocateChessboard(ChessboardTrackingCompleteData chessboardData);
    }
}
