using System;
using System.Collections.Generic;
using System.Linq;
using Accord;
using Accord.Math.Geometry;
using Emgu.CV.Structure;
using MathNet.Spatial.Euclidean;
using Point2D = MathNet.Spatial.Euclidean.Point2D;

namespace ChessTracking.ImageProcessing.ChessboardAlgorithms
{
    /// <summary>
    /// Contains algorithm for intersection of two groups of hough lines
    /// </summary>
    static class LinesIntersections
    {
        /// <summary>
        /// Performs algorithm for intersection of two groups of hough lines
        /// </summary>
        /// <param name="linesTuple">Two groups of lines</param>
        /// <returns>Contracted intersections</returns>
        public static List<Point2D> GetIntersectionPointsOfTwoGroupsOfLines(Tuple<LineSegment2D[], LineSegment2D[]> linesTuple)
        {
            var firstGroup = linesTuple.Item1;
            var secondGroup = linesTuple.Item2;
            
            var points = GetIntersections(firstGroup, secondGroup);

            // add ending points of lines (they can be involved into edges of chessboard)
            foreach (var line in firstGroup.Concat(secondGroup))
            {
                points.Add(new Point2D(line.P1.X, line.P1.Y));
                points.Add(new Point2D(line.P2.X, line.P2.Y));
            }
            
            return GetContractedPoints(points);
        }

        /// <summary>
        /// Computes mutual intersections of two groups of lines using library functions
        /// </summary>
        private static List<Point2D> GetIntersections(LineSegment2D[] firstGroup, LineSegment2D[] secondGroup)
        {
            var points = new List<Point2D>();

            // convert groups to library representation of lines, so we can use built-in intersection algorithm
            var firstGroupLibrary =
                firstGroup.Select(line =>
                    new Line2D(
                        new Point2D(line.P1.X, line.P1.Y),
                        new Point2D(line.P2.X, line.P2.Y)
                    )
                ).ToList();

            var secondGroupLibrary =
                secondGroup.Select(line =>
                    new Line2D(
                        new Point2D(line.P1.X, line.P1.Y),
                        new Point2D(line.P2.X, line.P2.Y)
                    )
                ).ToList();
            
            // compute intersections
            foreach (var line1 in firstGroupLibrary)
            {
                foreach (var line2 in secondGroupLibrary)
                {
                    var accordLine1 =
                        new LineSegment(
                            new Point((float)line1.StartPoint.X, (float)line1.StartPoint.Y),
                            new Point((float)line1.EndPoint.X, (float)line1.EndPoint.Y)
                        );
                    var accordLine2 =
                        new LineSegment(
                            new Point((float)line2.StartPoint.X, (float)line2.StartPoint.Y),
                            new Point((float)line2.EndPoint.X, (float)line2.EndPoint.Y)
                        );

                    var accordNullableIntersectionPoint = accordLine1.GetIntersectionWith(accordLine2);
                    if (accordNullableIntersectionPoint != null)
                        points.Add(new Point2D(accordNullableIntersectionPoint.Value.X, accordNullableIntersectionPoint.Value.Y));
                }
            }

            return points;
        }

        /// <summary>
        /// Points that are too close to each other are merged into a single point
        /// </summary>
        private static List<Point2D> GetContractedPoints(List<Point2D> points)
        {
            double maximalContractionDistance = 12; //15

            var contractedPoints = new List<Point2D>();

            while (true)
            {
                // if remaining points list is empty -> break
                if (points.Count == 0)
                    break;

                // new list for points to average and add first element from remaining list
                var pointsToContract = new List<Point2D>();
                var processedPoint = points.First();
                points.RemoveAt(0);

                pointsToContract.Add(processedPoint);

                // loop throught remaining list and find close neighbors
                foreach (var point in points)
                {
                    double diffX = (processedPoint.X - point.X);
                    double diffY = (processedPoint.Y - point.Y);

                    if (Math.Sqrt(diffX * diffX + diffY * diffY) < maximalContractionDistance)
                    {
                        pointsToContract.Add(point);
                    }
                }

                // remove them all from remaining list
                points.RemoveAll(p => pointsToContract.Contains(p));

                // compute average and add it to list
                var x = pointsToContract.Sum(p => p.X);
                var y = pointsToContract.Sum(p => p.Y);
                var count = pointsToContract.Count;

                contractedPoints.Add(new Point2D((int)x / count, (int)y / count));
            }

            return contractedPoints;
        }
    }
}
