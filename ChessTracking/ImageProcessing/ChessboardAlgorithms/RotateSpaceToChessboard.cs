using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Accord.Math;
using ChessTracking.ImageProcessing.PlaneAlgorithms;
using Microsoft.Kinect;

namespace ChessTracking.ImageProcessing.ChessboardAlgorithms
{
    class RotateSpaceToChessboard : IRotateSpaceToChessboard
    {
        public void Rotate(Chessboard3DReprezentation boardRepprezentation, CameraSpacePoint[] cspFromdd)
        {
            var firstVector = boardRepprezentation.FieldVector1;
            var secondVector = boardRepprezentation.FieldVector2;
            var cornerPoint = boardRepprezentation.CornerOfChessboard;

            firstVector = MyVector3DStruct.Normalize(firstVector);
            secondVector = MyVector3DStruct.Normalize(secondVector);

            var a = MyVector3DStruct.CrossProduct(ref firstVector, ref secondVector);
            var b = MyVector3DStruct.CrossProduct(ref secondVector, ref firstVector);
            var xVec = new MyVector3DStruct();
            var yVec = new MyVector3DStruct();
            var zVec = new MyVector3DStruct();
            
            // get new base based on cross product direction
            if ((a.z > 0 && b.z < 0)) // puvodne (a.z > 0 && b.z < 0)
            {
                zVec = a; // a
                yVec = MyVector3DStruct.Normalize(MyVector3DStruct.CrossProduct(ref xVec, ref zVec));

            }
            else if ((a.z < 0 && b.z > 0)) // puvodne (a.z < 0 && b.z > 0)
            {
                zVec = b; // b
                yVec = MyVector3DStruct.Normalize(MyVector3DStruct.CrossProduct(ref xVec, ref zVec));
            }
            else
            {
                throw new OutOfMemoryException();
            }

            xVec = MyVector3DStruct.Normalize(firstVector);
            yVec = MyVector3DStruct.Normalize(secondVector);
            zVec = MyVector3DStruct.Normalize(zVec);

            // spočítat inverzní matici

            double[,] matrix =
            {

                {xVec.x, yVec.x, zVec.x},
                {xVec.y, yVec.y, zVec.y},
                {xVec.z, yVec.z, zVec.z}
            };
            var inverseMatrix = matrix.Inverse();

            for (int i = 0; i < cspFromdd.Length; i++)
            {
                var nx = (float)(cspFromdd[i].X - cornerPoint.x);
                var ny = (float)(cspFromdd[i].Y - cornerPoint.y);
                var nz = (float)(cspFromdd[i].Z - cornerPoint.z);

                cspFromdd[i].X = (float)(inverseMatrix[0, 0] * nx + inverseMatrix[0, 1] * ny + inverseMatrix[0, 2] * nz);
                cspFromdd[i].Y = (float)(inverseMatrix[1, 0] * nx + inverseMatrix[1, 1] * ny + inverseMatrix[1, 2] * nz);
                cspFromdd[i].Z = (float)(inverseMatrix[2, 0] * nx + inverseMatrix[2, 1] * ny + inverseMatrix[2, 2] * nz);
            }
        }
    }
}
