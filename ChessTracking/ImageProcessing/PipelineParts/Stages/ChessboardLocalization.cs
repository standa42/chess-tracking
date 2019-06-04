using ChessTracking.ImageProcessing.ChessboardAlgorithms;
using ChessTracking.ImageProcessing.PipelineData;
using ChessTracking.ImageProcessing.PipelineParts.StagesInterfaces;
using ChessTracking.MultithreadingMessages;

namespace ChessTracking.ImageProcessing.PipelineParts.Stages
{
    class ChessboardLocalization : IChessboardLocalization
    {
        private Chessboard3DReprezentation BoardReprezentation { get; set; }
        private IRotateSpaceToChessboard RotationAlgorithm { get; }
        private IChessboardLocalizationAlgorithm ChessboardAlgorithm { get; }
        private RendererOfSceneWithChessboard Renderer { get; }

        public ChessboardLocalization()
        {
            RotationAlgorithm = new RotateSpaceToChessboard();
            ChessboardAlgorithm = new ChessboardLocalizationAlgorithm();
            Renderer = new RendererOfSceneWithChessboard();
        }

        public ChessboardTrackingCompleteData Calibrate(PlaneTrackingCompleteData planeData)
        {
            var chessboardData = new ChessboardTrackingCompleteData(planeData);

            BoardReprezentation = ChessboardAlgorithm.LocateChessboard(chessboardData);

            RotationAlgorithm.Rotate(BoardReprezentation, chessboardData.KinectData.CameraSpacePointsFromDepthData);

            return chessboardData;
        }

        public ChessboardTrackingCompleteData Track(PlaneTrackingCompleteData planeData)
        {
            var chessboardData = new ChessboardTrackingCompleteData(planeData);

            RotationAlgorithm.Rotate(BoardReprezentation, chessboardData.KinectData.CameraSpacePointsFromDepthData);

            chessboardData.ChessboardData.FieldSize = BoardReprezentation.FieldVector1.Magnitude();

            if (chessboardData.UserParameters.VisualisationType == VisualisationType.HighlightedChessboard)
                chessboardData.ResultData.VisualisationBitmap =
                    Renderer.ReturnLocalizedChessboardWithTable(
                        chessboardData.PlaneData.ColorBitmap,
                        chessboardData.PlaneData.MaskOfTable,
                        chessboardData.KinectData.PointsFromColorToDepth,
                        chessboardData.KinectData.CameraSpacePointsFromDepthData,
                        BoardReprezentation.FieldVector1.Magnitude());

            return chessboardData;
        }

        

    }
}
