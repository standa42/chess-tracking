using System;
using Accord.Math;
using ChessTracking.ImageProcessing.PlaneAlgorithms;
using ChessTracking.Utils;
using Microsoft.Kinect;

namespace ChessTracking.ImageProcessing.ChessboardAlgorithms
{
    class RotateSpaceToChessboard : IRotateSpaceToChessboard
    {
        public void Rotate(Chessboard3DReprezentation boardRepresentation, CameraSpacePoint[] csp)
        {
            var firstVector = boardRepresentation.FieldVector1;
            var secondVector = boardRepresentation.FieldVector2;
            var cornerPoint = boardRepresentation.CornerOfChessboard;

            firstVector = MyVector3DStruct.Normalize(firstVector);
            secondVector = MyVector3DStruct.Normalize(secondVector);

            var a = MyVector3DStruct.CrossProduct(ref firstVector, ref secondVector);
            var b = MyVector3DStruct.CrossProduct(ref secondVector, ref firstVector);
            var xVec = new MyVector3DStruct();
            var yVec = new MyVector3DStruct();
            var zVec = new MyVector3DStruct();
            
            // get new base based on two known vectors and their cross product
            if ((a.z > 0 && b.z < 0)) // originally (a.z > 0 && b.z < 0)
            {
                zVec = a; // a
                yVec = MyVector3DStruct.Normalize(MyVector3DStruct.CrossProduct(ref xVec, ref zVec));

            }
            else if ((a.z < 0 && b.z > 0)) // originally (a.z < 0 && b.z > 0)
            {
                zVec = b; // b
                yVec = MyVector3DStruct.Normalize(MyVector3DStruct.CrossProduct(ref xVec, ref zVec));
            }
            else
            {
                throw new InvalidOperationException();
            }

            // normalize base
            xVec = MyVector3DStruct.Normalize(firstVector);
            yVec = MyVector3DStruct.Normalize(secondVector);
            zVec = MyVector3DStruct.Normalize(zVec);

            // get inverse mapping to base
            double[,] matrix =
            {

                {xVec.x, yVec.x, zVec.x},
                {xVec.y, yVec.y, zVec.y},
                {xVec.z, yVec.z, zVec.z}
            };
            var inverseMatrix = matrix.Inverse();

            // move all points
            for (int i = 0; i < csp.Length; i++)
            {
                // translation to corner point
                var nx = (float)(csp[i].X - cornerPoint.x);
                var ny = (float)(csp[i].Y - cornerPoint.y);
                var nz = (float)(csp[i].Z - cornerPoint.z);

                // rotation around it to given base
                csp[i].X = (float)(inverseMatrix[0, 0] * nx + inverseMatrix[0, 1] * ny + inverseMatrix[0, 2] * nz);
                csp[i].Y = (float)(inverseMatrix[1, 0] * nx + inverseMatrix[1, 1] * ny + inverseMatrix[1, 2] * nz);
                csp[i].Z = (float)(inverseMatrix[2, 0] * nx + inverseMatrix[2, 1] * ny + inverseMatrix[2, 2] * nz);
            }
        }
    }
}
