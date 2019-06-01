using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
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

namespace ChessTracking.ImageProcessing.PipelineParts.Stages
{
    class ChessboardLocalization : IChessboardLocalization
    {
        public Pipeline Pipeline { get; }

        private MyVector3DStruct startingPointFinal = new MyVector3DStruct();
        private MyVector3DStruct firstVectorFinal = new MyVector3DStruct();
        private MyVector3DStruct secondVectorFinal = new MyVector3DStruct();
        
        public ChessboardLocalization(Pipeline pipeline)
        {
            this.Pipeline = pipeline;
        }
        
        public ChessboardDoneData Calibrate(PlaneDoneData planeData)
        {
            var chessboardData = new ChessboardDoneData(planeData);

            var grayImage = GetGrayImage(planeData.PlaneData.MaskedColorImageOfTable);
            var binarizedImage = GetBinarizedImage(grayImage);
            var cannyDetectorImage = ApplyCannyDetector(binarizedImage);

            var linesTuple = GetFilteredHoughLines(cannyDetectorImage);

            var contractedPoints = GetConcractedPoints(linesTuple);

            ChessboardFittingAlgorithm(contractedPoints, chessboardData);

            RotateSpaceToChessboard(startingPointFinal, firstVectorFinal, secondVectorFinal, chessboardData.KinectData.CameraSpacePointsFromDepthData);

            return chessboardData;
        }

        public ChessboardDoneData Track(PlaneDoneData planeData)
        {
            var chessboardData = new ChessboardDoneData(planeData);

            RotateSpaceToChessboard(startingPointFinal, firstVectorFinal, secondVectorFinal, chessboardData.KinectData.CameraSpacePointsFromDepthData);
            chessboardData.ChessboardData.FirstVectorFinal = firstVectorFinal;


            if (chessboardData.UserParameters.VisualisationType == VisualisationType.HighlightedChessboard)
                chessboardData.ResultData.VisualisationBitmap =
                    ReturnLocalizedChessboardWithTable(
                        chessboardData.PlaneData.ColorBitmap,
                        chessboardData.PlaneData.MaskOfTable,
                        chessboardData.KinectData.PointsFromColorToDepth,
                        chessboardData.KinectData.CameraSpacePointsFromDepthData,
                        firstVectorFinal);

            return chessboardData;
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

        private Tuple<LineSegment2D[], LineSegment2D[]> GetFilteredHoughLines(Image<Gray, byte> image)
        {
            var lines = image.HoughLinesBinary(
                0.8f,  //Distance resolution in pixel-related units
                Math.PI / 1500, //Angle resolution measured in radians.
                220, //threshold
                100, //min Line width (90)
                35 //gap between lines
            )[0];

            Image<Bgr, Byte> drawnEdges =
                new Image<Bgr, Byte>(new Size(image.Width, image.Height));

            foreach (LineSegment2D line in lines)
                CvInvoke.Line(drawnEdges, line.P1, line.P2,
                    new Bgr(Color.White).MCvScalar, 1);

            var lines2 = drawnEdges.Convert<Gray, byte>().HoughLinesBinary(
                0.8f, //Distance resolution in pixel-related units
                Math.PI / 1500, //Angle resolution measured in radians.
                50, //threshold
                100, //90                //min Line width
                10 //gap between lines
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

        private void ChessboardFittingAlgorithm(List<Point2D> contractedPoints, ChessboardDoneData chessboardData)
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
            //int eliminator = 0;
            foreach (var csp in contractedPointsCspStruct/*.Where(x => (eliminator++) % 2 == 0)*/)
            {
                // take 6 nearest neighbors
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
                                    startingPointFinal = startingPoint;
                                    firstVectorFinal = firstVector;
                                    secondVectorFinal = secondVector;
                                }
                            }
                        }
                    }
                }
            }
        }

        private Bitmap ReturnLocalizedChessboardWithTable(Bitmap colorImg, bool[] resultBools,
            DepthSpacePoint[] pointsFromColorToDepth, CameraSpacePoint[] cameraSpacePointsFromDepthData, MyVector3DStruct magnitudeVector)
        {
            Bitmap bm = colorImg;

            BitmapData bitmapData = bm.LockBits(new Rectangle(0, 0, bm.Width, bm.Height), ImageLockMode.ReadWrite, PixelFormat.Format24bppRgb);
            int bitmapSize = bm.Height * bm.Width;
            int width = bm.Width;
            int height = bm.Height;
            unsafe
            {
                byte* ptr = (byte*)bitmapData.Scan0;

                for (int y = 0; y < height; y++)
                {
                    for (int x = 0; x < width; x++)
                    {
                        int pixelPostion = (y * 1920 + x);
                        int rgbPositon = pixelPostion * 3;

                        DepthSpacePoint point = pointsFromColorToDepth[pixelPostion];
                        int pointPosition = (int)point.X + (int)point.Y * 512;

                        if (float.IsInfinity(point.X) || point.X < 0 || point.Y < 0)
                        {
                            *(ptr + rgbPositon + 2) = 255;
                            *(ptr + rgbPositon + 1) = 255;
                            *(ptr + rgbPositon + 0) = 255;
                        }
                        else
                        {
                            int colorX = (int)point.X;
                            int colorY = (int)point.Y;

                            if (colorY < 424 && colorX < 512)
                            {
                                int colorImageIndex = ((512 * colorY) + colorX);

                                if (resultBools[colorImageIndex])
                                {
                                    if (!(float.IsInfinity(cameraSpacePointsFromDepthData[pointPosition].Z) ||
                                          float.IsNaN(cameraSpacePointsFromDepthData[pointPosition].Z))

                                        && cameraSpacePointsFromDepthData[pointPosition].X > 0
                                        && cameraSpacePointsFromDepthData[pointPosition].Y > 0
                                        && cameraSpacePointsFromDepthData[pointPosition].X < magnitudeVector.Magnitude() * 8
                                        && cameraSpacePointsFromDepthData[pointPosition].Y < magnitudeVector.Magnitude() * 8
                                    )
                                    {
                                    }
                                    else
                                    {
                                        *(ptr + rgbPositon + 2) = (byte)(*(ptr + rgbPositon + 2) * 0.8f);
                                        *(ptr + rgbPositon + 1) = (byte)(*(ptr + rgbPositon + 1) * 0.8f);


                                        var value = *(ptr + rgbPositon + 0);
                                        value += (byte)((255 - value) * 0.95f);
                                        *(ptr + rgbPositon + 0) = value; // R
                                    }
                                }
                                else
                                {
                                    *(ptr + rgbPositon + 2) = 255;
                                    *(ptr + rgbPositon + 1) = 255;
                                    *(ptr + rgbPositon + 0) = 255;
                                }
                            }

                        }
                    }
                }
            }
            bm.UnlockBits(bitmapData);


            return bm;
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

        public static double ConvertRadiansToDegrees(double radians)
        {
            double degrees = (180 / Math.PI) * radians;
            return (degrees);
        }

        private int Mod(int x, int m)
        {
            return (x % m + m) % m;
        }

        public void RotateSpaceToChessboard(MyVector3DStruct startingPointFinal, MyVector3DStruct firstVectorFinal, MyVector3DStruct secondVectorFinal, CameraSpacePoint[] cspFromdd)
        {
            firstVectorFinal = MyVector3DStruct.Normalize(firstVectorFinal);
            secondVectorFinal = MyVector3DStruct.Normalize(secondVectorFinal);

            var a = MyVector3DStruct.CrossProduct(ref firstVectorFinal, ref secondVectorFinal);
            var b = MyVector3DStruct.CrossProduct(ref secondVectorFinal, ref firstVectorFinal);
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

            xVec = MyVector3DStruct.Normalize(firstVectorFinal);
            yVec = MyVector3DStruct.Normalize(secondVectorFinal);
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
                var nx = (float)(cspFromdd[i].X - startingPointFinal.x);
                var ny = (float)(cspFromdd[i].Y - startingPointFinal.y);
                var nz = (float)(cspFromdd[i].Z - startingPointFinal.z);

                cspFromdd[i].X = (float)(inverseMatrix[0, 0] * nx + inverseMatrix[0, 1] * ny + inverseMatrix[0, 2] * nz);
                cspFromdd[i].Y = (float)(inverseMatrix[1, 0] * nx + inverseMatrix[1, 1] * ny + inverseMatrix[1, 2] * nz);
                cspFromdd[i].Z = (float)(inverseMatrix[2, 0] * nx + inverseMatrix[2, 1] * ny + inverseMatrix[2, 2] * nz);
            }

        }
        
    }
}
