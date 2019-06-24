using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ChessTracking.ImageProcessing.PipelineData;
using ChessTracking.ImageProcessing.PlaneAlgorithms;
using Microsoft.Kinect;
using Point2D = MathNet.Spatial.Euclidean.Point2D;

namespace ChessTracking.ImageProcessing.ChessboardAlgorithms
{
    /// <summary>
    /// Contains algorithms for finding most suitable chessboard fitting real points
    /// </summary>
    static class ChessboardFitting
    {
        /// <summary>
        /// Finds most suitable chessboard fitting real points
        /// </summary>
        public static Chessboard3DReprezentation ChessboardFittingAlgorithm(List<Point2D> contractedPoints, ChessboardTrackingCompleteData chessboardData)
        {
            const int takeXNearestNeighbors = 7;

            var contractedPoints3D = ConvertIntersectionsTo3D(contractedPoints, chessboardData);
            
            var contractedPoints3DasStruct = contractedPoints3D.Select(x => new MyVector3DStruct(x.X, x.Y, x.Z)).ToArray();

            double lowestError = double.MaxValue;
            Chessboard3DReprezentation boardRepresentation = null;

            Parallel.ForEach(contractedPoints3DasStruct, csp =>
            {
                // take _ nearest neighbors
                var neighbors = contractedPoints3DasStruct.OrderBy((MyVector3DStruct x) => MyVector3DStruct.Distance(ref x, ref csp)).Take(takeXNearestNeighbors).ToArray();

                // take all pairs of neighbors
                for (int i = 0; i < neighbors.Length; i++)
                {
                    for (int j = i + 1; j < neighbors.Length; j++)
                    {
                        var first = neighbors[i];
                        var second = neighbors[j];

                        var firstPoint = new MyVector3DStruct(first.x, first.y, first.z);
                        var secondPoint = new MyVector3DStruct(second.x, second.y, second.z);

                        // st. point + 2 vectors
                        var initialPoint = new MyVector3DStruct(csp.x, csp.y, csp.z);
                        var firstVector = MyVector3DStruct.Difference(ref firstPoint, ref initialPoint);
                        var secondVector = MyVector3DStruct.Difference(ref secondPoint, ref initialPoint);

                        // perpendicularity check
                        double angleBetweenVectors = MyVector3DStruct.AngleInDeg(ref firstVector, ref secondVector);
                        if (!(angleBetweenVectors < 91.4 && angleBetweenVectors > 88.6))
                            break;

                        // length check
                        double ratio = firstVector.Magnitude() / secondVector.Magnitude();
                        if (!(ratio > 0.85f && ratio < 1.15f))
                            break;

                        // ensure right mutual position of first and second vector - otherwise switch them
                        if (MyVector3DStruct.CrossProduct(ref firstVector, ref secondVector).z < 0)
                        {
                            var temp = firstVector;
                            firstVector = secondVector;
                            secondVector = temp;
                        }

                        // length normalization of vectors
                        double averageLength = (firstVector.Magnitude() + secondVector.Magnitude()) / 2;

                        firstVector = MyVector3DStruct.MultiplyByNumber(MyVector3DStruct.Normalize(ref firstVector), averageLength);
                        secondVector =MyVector3DStruct.MultiplyByNumber(MyVector3DStruct.Normalize(ref secondVector), averageLength);

                        var negatedFirstVector = MyVector3DStruct.Negate(ref firstVector);
                        var negatedSecondVector = MyVector3DStruct.Negate(ref secondVector);

                        // locate all possible starting points 
                        for (int f = 0; f < 9; f++)
                        {
                            for (int s = 0; s < 9; s++)
                            {
                                var startingPoint =
                                    MyVector3DStruct.Addition(
                                        MyVector3DStruct.Addition(
                                            MyVector3DStruct.MultiplyByNumber(negatedFirstVector, f),
                                            MyVector3DStruct.MultiplyByNumber(negatedSecondVector, s)
                                        )
                                        , initialPoint
                                    );
                                // generate all possible chessboards for given starting point and compute their error
                                double currentError = 0;

                                for (int ff = 0; ff < 9; ff++)
                                {
                                    for (int ss = 0; ss < 9; ss++)
                                    {
                                        var currentPoint = MyVector3DStruct.Addition(
                                            startingPoint,
                                            MyVector3DStruct.Addition(
                                                MyVector3DStruct.MultiplyByNumber(firstVector, ff),
                                                MyVector3DStruct.MultiplyByNumber(secondVector, ss)
                                            )
                                        );

                                        var closestPointDistance = contractedPoints3DasStruct.Min(x =>
                                            MyVector3DStruct.Distance(ref currentPoint,
                                                new MyVector3DStruct(x.x, x.y, x.z)));

                                        // clipping of max error per point
                                        closestPointDistance = closestPointDistance > 0.02 ? 0.02 : closestPointDistance;

                                        currentError += closestPointDistance;
                                    }
                                }

                                if (currentError < lowestError)
                                {
                                    lowestError = currentError;
                                    boardRepresentation = new Chessboard3DReprezentation(startingPoint, firstVector, secondVector);
                                }
                            }
                        }
                    }
                }
            });

            return boardRepresentation;
        }

        /// <summary>
        /// Converts points from color bitmap to 3D coordintes using kinect defined mapping
        /// </summary>
        private static List<CameraSpacePoint> ConvertIntersectionsTo3D(List<Point2D> contractedPoints, ChessboardTrackingCompleteData chessboardData)
        {
            var contractedPoints3D = new List<CameraSpacePoint>();

            foreach (var contractedPoint in contractedPoints)
            {
                var depthReference = chessboardData.KinectData.PointsFromColorToDepth[(int)contractedPoint.X + (int)contractedPoint.Y * 1920];
                if (!float.IsInfinity(depthReference.X))
                {
                    var csp = chessboardData.KinectData.CameraSpacePointsFromDepthData[(int)depthReference.X + (int)depthReference.Y * 512];
                    contractedPoints3D.Add(csp);
                }
            }

            return contractedPoints3D;
        }
    }
}
