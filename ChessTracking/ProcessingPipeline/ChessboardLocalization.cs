using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Accord.Math;
using Accord.Math.Geometry;
using ChessTracking.MultithreadingMessages;
using ChessTracking.ProcessingPipeline.Plane;
using Emgu.CV;
using Emgu.CV.CvEnum;
using Emgu.CV.Structure;
using MathNet.Spatial.Euclidean;
using Microsoft.Kinect;
using Point = Accord.Point;
using Point2D = MathNet.Spatial.Euclidean.Point2D;

namespace ChessTracking.ProcessingPipeline
{
    class ChessboardLocalization
    {
        public Pipeline Pipeline { get; }

        MyVector3DStruct startingPointFinal = new MyVector3DStruct();
        MyVector3DStruct firstVectorFinal = new MyVector3DStruct();
        MyVector3DStruct secondVectorFinal = new MyVector3DStruct();


        public ChessboardLocalization(Pipeline pipeline)
        {
            this.Pipeline = pipeline;
        }

        public ChessboardDoneData Recalibrate(PlaneDoneData planeData)
        {
            var chessboardData = new ChessboardDoneData(planeData);

            {
                Image<Bgr, Byte> drawnEdges2 = null;

                Image<Gray, Byte> grayImage = planeData.MaskedColorImageOfTable.Convert<Gray, Byte>();
                //grayImage.Save(@"D:\Desktop\LocGray.jpeg");
                var binarizedImg = new Image<Gray, byte>(grayImage.Width, grayImage.Height);
                CvInvoke.Threshold(grayImage, binarizedImg, 200, 255, ThresholdType.Otsu);
                //binarizedImg.Save(@"D:\Desktop\LocOtzu.jpeg");
                Image<Gray, Byte> cannyEdges = binarizedImg.Canny(700, 1400, 5, true).SmoothGaussian(3)
                    .ThresholdBinary(new Gray(50), new Gray(255));
                //cannyEdges.Save(@"D:\Desktop\LocCanny.jpeg");
                var lines = cannyEdges.HoughLinesBinary(
                    0.8f, //Distance resolution in pixel-related units
                    Math.PI / 1500, //Angle resolution measured in radians.
                    220, //threshold
                    100, //min Line width (90)
                    35 //gap between lines
                )[0];
                Image<Bgr, Byte> drawnEdges =
                    new Image<Bgr, Byte>(new Size(cannyEdges.Width, cannyEdges.Height) /*cannyEdges.ToBitmap()*/);
                foreach (LineSegment2D line in lines)
                    CvInvoke.Line(drawnEdges, line.P1, line.P2,
                        new Bgr( /*Color.Red*/ /*RandomColor()*/ Color.White).MCvScalar, 1);

                var lines2 = drawnEdges.Convert<Gray, byte>().HoughLinesBinary(
                    0.8f, //Distance resolution in pixel-related units
                    Math.PI / 1500, //Angle resolution measured in radians.
                    50, //threshold
                    100, //90                //min Line width
                    10 //gap between lines
                )[0];

                Pen redPen = new Pen(Color.Red, 3);

                var colorNonFiltered = (Image)planeData.MaskedColorImageOfTable.Bitmap.Clone();
                using (var graphics = Graphics.FromImage(colorNonFiltered))
                {
                    foreach (var line in lines2)
                    {
                        graphics.DrawLine(redPen, line.P1.X, line.P1.Y, line.P2.X, line.P2.Y);
                    }
                }
                //colorNonFiltered.Save(@"D:\Desktop\LocHoughNonfiltered.jpeg");
                

                var linesTuple = FilterLinesBasedOnAngle(lines2, 25); //TODO 25


                var colorFiltered = (Image)planeData.MaskedColorImageOfTable.Bitmap.Clone();
                using (var graphics = Graphics.FromImage(colorFiltered))
                {
                    foreach (var line in linesTuple.Item1.Concat(linesTuple.Item2))
                    {
                        graphics.DrawLine(redPen, line.P1.X, line.P1.Y, line.P2.X, line.P2.Y);
                    }
                }
                colorFiltered.Save(@"D:\Desktop\LocHoughfiltered.jpeg");

                drawnEdges2 = new Image<Bgr, Byte>( /*colorImg.Bitmap*/
                    new Size(cannyEdges.Width, cannyEdges.Height));

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

                var colorD = (Bitmap)planeData.MaskedColorImageOfTable.Bitmap.Clone();
                using (var graphics = Graphics.FromImage(colorD))
                {
                    foreach (var line in linesTuple.Item1.Concat(linesTuple.Item2))
                    {
                        graphics.DrawLine(redPen, line.P1.X, line.P1.Y, line.P2.X, line.P2.Y);
                    }
                }


                Pen yellowPen = new Pen(Color.Yellow, 3);
                foreach (var contractedPoint in contractedPoints)
                {
                    try
                    {
                        using (var graphics = Graphics.FromImage(colorD))
                        {
                             graphics.DrawRectangle(yellowPen, new Rectangle((int)contractedPoint.X-3, (int)contractedPoint.Y-3, 7 , 7));
                        }
                    }
                    catch (Exception)
                    {

                    }
                }
                //colorD.Save(@"D:\Desktop\LocIntersections.jpeg");
                /////////////////////////////////////////////////////////////////////////////////////////////////////

                List<CameraSpacePoint> contractedPointsCSP = new List<CameraSpacePoint>();

                foreach (var contractedPoint in contractedPoints)
                {
                    var depthReference =
                        chessboardData.PointsFromColorToDepth[(int)contractedPoint.X + (int)contractedPoint.Y * 1920];
                    if (!float.IsInfinity(depthReference.X))
                    {
                        var csp = chessboardData.CameraSpacePointsFromDepthData[
                            (int)depthReference.X + (int)depthReference.Y * 512];
                        contractedPointsCSP.Add(csp);
                    }
                }



                var contractedPointsCSPStruct =
                    contractedPointsCSP.Select(x => new MyVector3DStruct(x.X, x.Y, x.Z)).ToArray();

                double lowestError = double.MaxValue;
                //int eliminator = 0;
                foreach (var csp in contractedPointsCSPStruct/*.Where(x => (eliminator++) % 2 == 0)*/)
                {
                    // take 6 nearest neighbors
                    var neighbors = contractedPointsCSPStruct.OrderBy(
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

                                            var closestPointDistance = contractedPointsCSPStruct.Min(x =>
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
            RotateSpaceToChessboard(startingPointFinal, firstVectorFinal, secondVectorFinal, chessboardData.CameraSpacePointsFromDepthData);

            return chessboardData;
        }

        public ChessboardDoneData Track(PlaneDoneData planeData)
        {
            var chessboardData = new ChessboardDoneData(planeData);

            {
                RotateSpaceToChessboard(startingPointFinal, firstVectorFinal, secondVectorFinal, chessboardData.CameraSpacePointsFromDepthData);
                chessboardData.FirstVectorFinal = firstVectorFinal;
            }

            if (chessboardData.VisualisationType == VisualisationType.HighlightedChessboard)
                chessboardData.Bitmap = 
                    ReturnLocalizedChessboardWithTable(
                        chessboardData.ColorBitmap,
                        chessboardData.MaskOfTable,
                        chessboardData.PointsFromColorToDepth,
                        chessboardData.CameraSpacePointsFromDepthData, 
                        firstVectorFinal);

            return chessboardData;
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

        int Mod(int x, int m)
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
                
                /*
                {xVec.x, xVec.y, xVec.z},
                {yVec.x, yVec.y, yVec.z},
                {zVec.x, zVec.y, zVec.z}
                */
                
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

                /*
                cspFromdd[i].X = (float)(inverseMatrix[0, 0] * cspFromdd[i].X + inverseMatrix[1, 0] * cspFromdd[i].Y + inverseMatrix[2, 0] * cspFromdd[i].Z);
                cspFromdd[i].Y = (float)(inverseMatrix[0, 1] * cspFromdd[i].X + inverseMatrix[1, 1] * cspFromdd[i].Y + inverseMatrix[2, 1] * cspFromdd[i].Z);
                cspFromdd[i].Z = (float)(inverseMatrix[0, 2] * cspFromdd[i].X + inverseMatrix[1, 2] * cspFromdd[i].Y + inverseMatrix[2, 2] * cspFromdd[i].Z);
                */
            }

        }
        
        
    }
}
