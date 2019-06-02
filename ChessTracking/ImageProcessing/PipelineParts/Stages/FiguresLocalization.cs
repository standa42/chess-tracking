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

        public FiguresDoneData Calibrate(ChessboardDoneData chessboardData)
        {
            var figuresData = new FiguresDoneData(chessboardData);

            return figuresData;
        }

        public FiguresDoneData Track(ChessboardDoneData chessboardData)
        {
            var figuresData = new FiguresDoneData(chessboardData);

            figuresData.ResultData.TrackingState =
                FiguresLocalizationAlgorithm.LocateFigures(
                    figuresData.KinectData,
                    figuresData.ChessboardData.FieldSize,
                    figuresData.PlaneData.CannyDepthData,
                    figuresData.UserParameters);

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
