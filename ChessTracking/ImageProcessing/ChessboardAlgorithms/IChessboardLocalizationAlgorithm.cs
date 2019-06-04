using ChessTracking.ImageProcessing.PipelineData;

namespace ChessTracking.ImageProcessing.ChessboardAlgorithms
{
    interface IChessboardLocalizationAlgorithm
    {
        Chessboard3DReprezentation LocateChessboard(ChessboardTrackingCompleteData chessboardData);
    }
}
