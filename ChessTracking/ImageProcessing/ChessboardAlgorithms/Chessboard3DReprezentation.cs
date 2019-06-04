using ChessTracking.ImageProcessing.PlaneAlgorithms;

namespace ChessTracking.ImageProcessing.ChessboardAlgorithms
{
    class Chessboard3DReprezentation
    {
        public MyVector3DStruct CornerOfChessboard { get; }
        public MyVector3DStruct FieldVector1 { get; }
        public MyVector3DStruct FieldVector2 { get; }

        public Chessboard3DReprezentation(MyVector3DStruct cornerOfChessboard, MyVector3DStruct fieldVector1, MyVector3DStruct fieldVector2)
        {
            CornerOfChessboard = cornerOfChessboard;
            FieldVector1 = fieldVector1;
            FieldVector2 = fieldVector2;
        }
    }
}
