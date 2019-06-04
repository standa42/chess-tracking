using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Threading.Tasks;
using Accord.Math;
using Accord.Math.Geometry;
using ChessTracking.ImageProcessing.PipelineData;
using ChessTracking.ImageProcessing.PipelineParts.General;
using ChessTracking.ImageProcessing.PipelineParts.StagesInterfaces;
using ChessTracking.ImageProcessing.PlaneAlgorithms;
using ChessTracking.MultithreadingMessages;
using Emgu.CV;
using Emgu.CV.CvEnum;
using Emgu.CV.Structure;
using MathNet.Spatial.Euclidean;
using Microsoft.Kinect;
using Point = Accord.Point;
using Point2D = MathNet.Spatial.Euclidean.Point2D;

namespace ChessTracking.ImageProcessing.ChessboardAlgorithms
{
    class ChessboardLocalizationAlgorithm : IChessboardLocalizationAlgorithm
    {
        public Chessboard3DReprezentation LocateChessboard(ChessboardTrackingCompleteData chessboardData)
        {
            var grayImage = GetGrayImage(chessboardData.PlaneData.MaskedColorImageOfTable);
            var binarizedImage = GetBinarizedImage(grayImage);
            var cannyDetectorImage = ApplyCannyDetector(binarizedImage);

            var linesTuple = GetFilteredHoughLines(cannyDetectorImage);

            var contractedPoints = GetConcractedPoints(linesTuple);

            var boardRepresentation = ChessboardFittingAlgorithm(contractedPoints, chessboardData);

            return boardRepresentation;
        }

        private Image<Gray, byte> GetGrayImage(Image<Rgb, byte> colorImage)
        {
            return colorImage.Convert<Gray, Byte>();
        }

        private Image<Gray, byte> GetBinarizedImage(Image<Gray, byte> grayImage)
        {
            var binarizedImg = new Image<Gray, byte>(grayImage.Width, grayImage.Height);
            CvInvoke.Threshold(grayImage, binarizedImg, 200, 255, ThresholdType.Otsu);
            return binarizedImg;
        }

        private Image<Gray, Byte> ApplyCannyDetector(Image<Gray, byte> image)
        {
            return image.Canny(700, 1400, 5, true).SmoothGaussian(3).ThresholdBinary(new Gray(50), new Gray(255));
        }

        /// <summary>
        /// Extracts 2 groups of lines from given image
        /// -> 2 stages of hough transform to get better result lines
        /// -> lines are filtered to two larges groups based on angle in image
        /// </summary>
        /// <param name="image"></param>
        /// <returns></returns>
        private Tuple<LineSegment2D[], LineSegment2D[]> GetFilteredHoughLines(Image<Gray, byte> image)
        {
            // first stage hough transform
            var lines = image.HoughLinesBinary(
                0.8f,  //Distance resolution in pixel-related units
                Math.PI / 1500, //Angle resolution measured in radians.
                220, //threshold
                100, //min Line width (90)
                35 //gap between lines
            )[0];

            // render image with lines from first stage
            Image<Bgr, Byte> drawnEdges =
                new Image<Bgr, Byte>(new Size(image.Width, image.Height));

            foreach (LineSegment2D line in lines)
                CvInvoke.Line(drawnEdges, line.P1, line.P2,
                    new Bgr(Color.White).MCvScalar, 1);

            // apply second hough transform
            var lines2 = drawnEdges.Convert<Gray, byte>().HoughLinesBinary(
                0.8f,
                Math.PI / 1500,
                50,
                100,
                10
            )[0];

            return FilterLinesBasedOnAngle(lines2, 25);
        }

        private List<Point2D> GetConcractedPoints(Tuple<LineSegment2D[], LineSegment2D[]> linesTuple)
        {
            var points = new List<Point2D>();
            var contractedPoints = new List<Point2D>();
            var libraryLines = new Tuple<List<Line2D>, List<Line2D>>(new List<Line2D>(), new List<Line2D>());

            foreach (var lineSegment2D in linesTuple.Item1)
            {
                libraryLines.Item1.Add(new Line2D(new Point2D(lineSegment2D.P1.X, lineSegment2D.P1.Y),
                    new Point2D(lineSegment2D.P2.X, lineSegment2D.P2.Y)));
            }

            foreach (var lineSegment2D in linesTuple.Item2)
            {
                libraryLines.Item2.Add(new Line2D(new Point2D(lineSegment2D.P1.X, lineSegment2D.P1.Y),
                    new Point2D(lineSegment2D.P2.X, lineSegment2D.P2.Y)));
            }

            foreach (var line1 in libraryLines.Item1)
            {
                foreach (var line2 in libraryLines.Item2)
                {
                    var accordLine1 =
                        new LineSegment(new Point((float)line1.StartPoint.X, (float)line1.StartPoint.Y),
                            new Point((float)line1.EndPoint.X, (float)line1.EndPoint.Y));
                    var accordLine2 =
                        new LineSegment(new Point((float)line2.StartPoint.X, (float)line2.StartPoint.Y),
                            new Point((float)line2.EndPoint.X, (float)line2.EndPoint.Y));

                    var accordNullablePoint = accordLine1.GetIntersectionWith(accordLine2);
                    if (accordNullablePoint != null)
                    {
                        points.Add(new Point2D(accordNullablePoint.Value.X, accordNullablePoint.Value.Y));
                    }
                }
            }

            foreach (var line1 in linesTuple.Item1)
            {
                points.Add(new Point2D(line1.P1.X, line1.P1.Y));
                points.Add(new Point2D(line1.P2.X, line1.P2.Y));
            }

            foreach (var line2 in linesTuple.Item2)
            {
                points.Add(new Point2D(line2.P1.X, line2.P1.Y));
                points.Add(new Point2D(line2.P2.X, line2.P2.Y));
            }


            double distance = 12; //15
            while (true)
            {
                // new list for points to average and add first element from remaining list
                var pointsToAvg = new List<Point2D>();
                var referencePoint = points.First();
                pointsToAvg.Add(referencePoint);
                points.RemoveAt(0);

                // loop throught remaining list and find close neighbors
                foreach (var point in points)
                {
                    double diffX = (referencePoint.X - point.X);
                    double diffY = (referencePoint.Y - point.Y);

                    if (Math.Sqrt(diffX * diffX + diffY * diffY) < distance)
                    {
                        pointsToAvg.Add(point);
                    }
                }

                // remove them all from remaining list
                foreach (var pointToRemove in pointsToAvg)
                {
                    points.Remove(pointToRemove);
                }

                // compute average and add it to list
                double x = 0;
                double y = 0;
                int count = 0;
                foreach (var point in pointsToAvg)
                {
                    x += point.X;
                    y += point.Y;
                    count++;
                }

                contractedPoints.Add(new Point2D((int)x / count, (int)y / count));

                // if rem. list is empty -> break
                if (points.Count == 0)
                {
                    break;
                }
            }

            return contractedPoints;
        }

        private Chessboard3DReprezentation ChessboardFittingAlgorithm(List<Point2D> contractedPoints, ChessboardTrackingCompleteData chessboardData)
        {

            List<CameraSpacePoint> contractedPointsCsp = new List<CameraSpacePoint>();

            foreach (var contractedPoint in contractedPoints)
            {
                var depthReference =
                    chessboardData.KinectData.PointsFromColorToDepth[(int)contractedPoint.X + (int)contractedPoint.Y * 1920];
                if (!float.IsInfinity(depthReference.X))
                {
                    var csp = chessboardData.KinectData.CameraSpacePointsFromDepthData[
                        (int)depthReference.X + (int)depthReference.Y * 512];
                    contractedPointsCsp.Add(csp);
                }
            }



            var contractedPointsCspStruct =
                contractedPointsCsp.Select(x => new MyVector3DStruct(x.X, x.Y, x.Z)).ToArray();

            double lowestError = double.MaxValue;
            Chessboard3DReprezentation BoardRepresentation = null;
            //int eliminator = 0;
            Parallel.ForEach( contractedPointsCspStruct/*.Where(x => (eliminator++) % 2 == 0)*/,csp =>
            {
                // take _ nearest neighbors
                var neighbors = contractedPointsCspStruct.OrderBy(
                    (MyVector3DStruct x) =>
                    {
                        return
                            Math.Sqrt(
                                (x.x - csp.x) * (x.x - csp.x) + (x.y - csp.y) * (x.y - csp.y) +
                                (x.z - csp.z) * (x.z - csp.z)
                            );
                    }).Take(7).ToArray();

                // take all pairs
                for (int i = 0; i < neighbors.Length; i++)
                {
                    for (int j = i + 1; j < neighbors.Length; j++)
                    {
                        var first = neighbors[i];
                        var second = neighbors[j];

                        var firstPoint = new MyVector3DStruct(first.x, first.y, first.z);
                        var secondPoint = new MyVector3DStruct(second.x, second.y, second.z);

                        // st. point + 2 vectors
                        var cspPoint = new MyVector3DStruct(csp.x, csp.y, csp.z);
                        var firstVector = MyVector3DStruct.Difference(ref firstPoint, ref cspPoint);
                        var secondVector = MyVector3DStruct.Difference(ref secondPoint, ref cspPoint);

                        // perpendicularity check
                        double angleBetweenVectors =
                            MyVector3DStruct.AngleInDeg(ref firstVector, ref secondVector);
                        if (!(angleBetweenVectors < 91.4 && angleBetweenVectors > 88.6))
                        {
                            break;
                        }

                        // length check
                        double ratio = firstVector.Magnitude() / secondVector.Magnitude();
                        if (!(ratio > 0.85f && ratio < 1.15f))
                        {
                            break;
                        }

                        // ensure right orientation
                        if (MyVector3DStruct.CrossProduct(ref firstVector, ref secondVector).z < 0)
                        {
                            var temp = firstVector;
                            firstVector = secondVector;
                            secondVector = temp;
                        }

                        // length normalization
                        double averageLength = (firstVector.Magnitude() + secondVector.Magnitude()) / 2;

                        firstVector =
                            MyVector3DStruct.MultiplyByNumber(MyVector3DStruct.Normalize(ref firstVector),
                                averageLength);
                        secondVector =
                            MyVector3DStruct.MultiplyByNumber(MyVector3DStruct.Normalize(ref secondVector),
                                averageLength);

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
                                        ,
                                        cspPoint
                                    );

                                double currentError = 0;

                                // generate all possible chessboards for given starting point
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

                                        var closestPointDistance = contractedPointsCspStruct.Min(x =>
                                            MyVector3DStruct.Distance(ref currentPoint,
                                                new MyVector3DStruct(x.x, x.y, x.z)));

                                        closestPointDistance =
                                            closestPointDistance > 0.01 ? 1 : closestPointDistance;

                                        currentError += closestPointDistance;
                                    }
                                }

                                if (currentError < lowestError)
                                {
                                    lowestError = currentError;
                                    BoardRepresentation = new Chessboard3DReprezentation(startingPoint, firstVector, secondVector);
                                }
                            }
                        }
                    }
                }
            });

            return BoardRepresentation;
        }

        

        private Tuple<LineSegment2D[], LineSegment2D[]> FilterLinesBasedOnAngle(LineSegment2D[] lines, int angle)
        {
            int deg = 180;
            var resultLines1 = new List<LineSegment2D>();
            var resultLines2 = new List<LineSegment2D>();

            List<LineSegment2D>[] linesByAngle = new List<LineSegment2D>[deg];
            for (int i = 0; i < linesByAngle.Length; i++)
            {
                linesByAngle[i] = new List<LineSegment2D>();
            }

            // fill lines into their degree reprezentation
            for (int i = 0; i < lines.Length; i++)
            {
                var diffX = lines[i].P1.X - lines[i].P2.X;
                var diffY = lines[i].P1.Y - lines[i].P2.Y;

                int theta = Mod(((int)ConvertRadiansToDegrees(Math.Atan2(diffY, diffX))), deg);
                linesByAngle[theta].Add(lines[i]);
            }

            // get first max window
            int maxNumber = -1;
            int maxIndex = -1;

            for (int i = 0; i < deg; i++)
            {
                int number = 0;
                int index = i;

                for (int j = i; j < i + angle; j++)
                {
                    number += linesByAngle[Mod(j, deg)].Count;
                }

                if (number > maxNumber)
                {
                    maxNumber = number;
                    maxIndex = index;
                }
            }

            // fill and remove
            for (int j = maxIndex; j < maxIndex + angle; j++)
            {
                var linesOfCertainAngle = linesByAngle[Mod(j, deg)];

                foreach (var lineOfCeratinAngle in linesOfCertainAngle)
                {
                    resultLines1.Add(lineOfCeratinAngle);
                }

                linesByAngle[Mod(j, deg)].Clear();
            }

            // get second max window
            maxNumber = -1;
            maxIndex = -1;

            for (int i = 0; i < deg; i++)
            {
                int number = 0;
                int index = i;

                for (int j = i; j < i + angle; j++)
                {
                    number += linesByAngle[Mod(j, deg)].Count;
                }

                if (number > maxNumber)
                {
                    maxNumber = number;
                    maxIndex = index;
                }
            }

            // fill and remove
            for (int j = maxIndex; j < maxIndex + angle; j++)
            {
                var linesOfCertainAngle = linesByAngle[Mod(j, deg)];

                foreach (var lineOfCeratinAngle in linesOfCertainAngle)
                {
                    resultLines2.Add(lineOfCeratinAngle);
                }

                linesByAngle[Mod(j, deg)].Clear();
            }


            return new Tuple<LineSegment2D[], LineSegment2D[]>(resultLines1.ToArray(), resultLines2.ToArray());
        }

        /// <summary>
        /// Conversion from radians to degrees
        /// </summary>
        /// <param name="radians">value in radians</param>
        /// <returns>value in degrees</returns>
        public static double ConvertRadiansToDegrees(double radians)
        {
            double degrees = (180 / Math.PI) * radians;
            return (degrees);
        }

        /// <summary>
        /// Mathematical modulo operation - for all numbers, including negative, returns number 0..m-1
        /// </summary>
        /// <param name="x">input number</param>
        /// <param name="m">modulo coeficient</param>
        /// <returns>moduled result</returns>
        private int Mod(int x, int m)
        {
            return (x % m + m) % m;
        }

        
    }
}
