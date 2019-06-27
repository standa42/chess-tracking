using ChessTracking.ImageProcessing.FiguresAlgorithms;
using ChessTracking.ImageProcessing.PipelineData;
using ChessTracking.ImageProcessing.PipelineParts.StagesInterfaces;

namespace ChessTracking.ImageProcessing.PipelineParts.Stages
{
    class FiguresLocalization : IFiguresLocalization
    {
        private IHandDetectionAlgorithm HandDetectionAlgorithm { get; }
        private IFiguresLocalizationAlgorithm FiguresLocalizationAlgorithm { get; }

        public FiguresLocalization()
        {
            HandDetectionAlgorithm = new HandDetectionAlgorithm();
            FiguresLocalizationAlgorithm = new FiguresLocalizationAlgorithm();
        }

        public FiguresTrackingCompleteData Calibrate(ChessboardTrackingCompleteData chessboardData)
        {
            var figuresData = new FiguresTrackingCompleteData(chessboardData);

            return figuresData;
        }

        public FiguresTrackingCompleteData Track(ChessboardTrackingCompleteData chessboardData)
        {
            var figuresData = new FiguresTrackingCompleteData(chessboardData);

            (figuresData.ResultData.TrackingState, figuresData.ResultData.PointCountsOverFields) =
                FiguresLocalizationAlgorithm.LocateFigures(
                    figuresData.KinectData,
                    figuresData.ChessboardData.FieldSize,
                    figuresData.PlaneData.CannyDepthData,
                    figuresData.UserParameters,
                    figuresData.ResultData,
                    figuresData.PlaneData.ColorBitmap,
                    chessboardData.TrackingStateOfGame);

            var handDetected =
                HandDetectionAlgorithm.HandDetected(
                    figuresData.KinectData.CameraSpacePointsFromDepthData,
                    figuresData.ChessboardData.FieldSize
                );
            figuresData.ResultData.SceneDisrupted = handDetected || figuresData.ResultData.SceneDisrupted;
            
            return figuresData;
        }
    }
}
