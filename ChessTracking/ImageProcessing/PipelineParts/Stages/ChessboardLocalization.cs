using System;
using System.Collections.Concurrent;
using ChessTracking.ImageProcessing.ChessboardAlgorithms;
using ChessTracking.ImageProcessing.PipelineData;
using ChessTracking.ImageProcessing.PipelineParts.StagesInterfaces;
using ChessTracking.ImageProcessing.PlaneAlgorithms;
using ChessTracking.MultithreadingMessages;
using ChessTracking.MultithreadingMessages.FromProcessing;
using ChessTracking.MultithreadingMessages.ToProcessing;

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

        public void MoveChessboard(ChessboardMovement direction)
        {
            var corner = BoardReprezentation.CornerOfChessboard;
            var vector1 = BoardReprezentation.FieldVector1;
            var vector2 = BoardReprezentation.FieldVector2;

            switch (direction)
            {
                case ChessboardMovement.Vector1Plus:
                    BoardReprezentation.CornerOfChessboard = MyVector3DStruct.Addition(corner, vector1);
                    break;
                case ChessboardMovement.Vector1Minus:
                    BoardReprezentation.CornerOfChessboard = MyVector3DStruct.Difference(ref corner, ref vector1);
                    break;
                case ChessboardMovement.Vector2Plus:
                    BoardReprezentation.CornerOfChessboard = MyVector3DStruct.Addition(corner, vector2);
                    break;
                case ChessboardMovement.Vector2Minus:
                    BoardReprezentation.CornerOfChessboard = MyVector3DStruct.Difference(ref corner, ref vector2);
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(direction), direction, null);
            }
        }

        public ChessboardTrackingCompleteData Calibrate(PlaneTrackingCompleteData planeData, BlockingCollection<Message> outputQueue)
        {
            var chessboardData = new ChessboardTrackingCompleteData(planeData);

            SceneCalibrationSnapshot snapshot;
            (BoardReprezentation, snapshot) = ChessboardAlgorithm.LocateChessboard(chessboardData);
            outputQueue.Add(new SceneCalibrationSnapshotMessage(snapshot));

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
