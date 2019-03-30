using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using ChessboardTrackingOctoberEdition.Kinect;
using ChessboardTrackingOctoberEdition.Shared;
using ChessboardTrackingOctoberEdition.TrackingPart;
using Emgu.CV;
using Emgu.CV.CvEnum;
using Emgu.CV.Structure;
using Emgu.CV.Util;
using Kinect_v0._1;
using MathNet.Numerics.Distributions;
using Microsoft.Kinect;
using Sandbox.Forms.HarrisCornerDetection;
using Random = System.Random;

namespace ChessboardTrackingOctoberEdition.Visualisers
{
    class ChessboardLocalizationSecond : IVisualiser
    {
        private DisplayForm displayForm;
        private IKinect kinect;
        private StateAutomataWrapper stateAutomata;

        private Image<Rgb, byte> colorImg;
        public int whateverPositive { get; set; } = 7;
        public int stEven { get; set; } = 7;
        public float stSmall { get; set; } = 0.01f;

        public ChessboardLocalizationSecond(Form form, IKinect kinect, StateAutomataWrapper stateAutomata)
        {
            this.kinect = kinect;
            this.stateAutomata = stateAutomata;
            form.Invoke(new MethodInvoker(() =>
            {
                DisplayForm newForm = new DisplayForm();
                displayForm = newForm;
                newForm.SetDimensions(kinect.ColorFrameDescription.Width / 2, kinect.ColorFrameDescription.Height / 2);
                newForm.Show();
            }));

            colorImg = new Image<Rgb, byte>(kinect.ColorFrameDescription.Width, kinect.ColorFrameDescription.Height);
        }

        private int frameCounter;

        public void Update(byte[] colorFrameData, ushort[] depthData, ushort[] infraredData, CameraSpacePoint[] cameraSpacePointsFromDepthData,
            DepthSpacePoint[] pointsFromColorToDepth, ColorSpacePoint[] pointsFromDepthToColor)
        {
            if (frameCounter++ % 3 != 0)
            {
                return;
            }

            bool[] resultBools = new bool[512 * 424];
            Data data = new Data(cameraSpacePointsFromDepthData);

            data.CutOffMinMaxDepth(Config.minDepth, Config.maxDepth);


            try
            {
                //if (Config.Gaussian) data.GaussianFilterOnZBasedOnBitmapCoordinates();
                data.RANSAC();
                data.LargestTableArea();
                data.LinearRegression();
                data.LargestTableArea();
                data.LinearRegression();
                data.LargestTableArea();

                data.RotationTo2DModified();

                if (Config.Gaussian) data.GaussianFilterOnZBasedOnBitmapCoordinates();

                resultBools = data.ConvexHullAlgorithmModified();
            }
            catch (Exception ex)
            {
            }



            /////////////////////////////////////////////////////////

            Bitmap bm = new Bitmap(1920, 1080, PixelFormat.Format24bppRgb);

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

                        if (float.IsInfinity(point.X) || point.X < 0 || point.Y < 0)
                        {
                            *(ptr + rgbPositon + 0) = 255;
                            *(ptr + rgbPositon + 1) = 255;
                            *(ptr + rgbPositon + 2) = 255;
                        }
                        else
                        {
                            int colorX = (int)/*.Floor(*/point.X/* + 0.5)*/;
                            int colorY = (int)/*Math.Floor(*/point.Y/* + 0.5)*/;

                            //int colorX = (int)(point.X);
                            //int colorY = (int)(point.Y);

                            if (colorY < 424 && colorX < 512)
                            {
                                int colorImageIndex = ((512 * colorY) + colorX);

                                if (resultBools[colorImageIndex])
                                {
                                    *(ptr + rgbPositon + 0) = (byte)((colorFrameData[pixelPostion * 4]));
                                    *(ptr + rgbPositon + 1) = (byte)((colorFrameData[pixelPostion * 4 + 1]));
                                    *(ptr + rgbPositon + 2) = (byte)((colorFrameData[pixelPostion * 4 + 2]));
                                }
                                else
                                {
                                    *(ptr + rgbPositon + 0) = 255;
                                    *(ptr + rgbPositon + 1) = 255;
                                    *(ptr + rgbPositon + 2) = 255;
                                }
                            }

                        }
                    }
                }
            }
            bm.UnlockBits(bitmapData);

            /////////////////////////////////////////////////////////

            BitmapData bmpdata = null;

            try
            {
                bmpdata = bm.LockBits(new Rectangle(0, 0, bm.Width, bm.Height), ImageLockMode.ReadOnly, bm.PixelFormat);
                int numbytes = bmpdata.Stride * bm.Height;
                byte[] bytedata = new byte[numbytes];
                IntPtr ptr = bmpdata.Scan0;

                Marshal.Copy(ptr, bytedata, 0, numbytes);

                colorImg.Bytes = bytedata;
            }
            finally
            {
                if (bmpdata != null)
                    bm.UnlockBits(bmpdata);
            }

            //Task.Run(() =>
            //{
                Image<Gray, Byte> grayImage = colorImg/*.Resize(1024, 576, Inter.Linear)*/.Convert<Gray, Byte>(); // 1024,728
                                                                                                                  //Image<Gray, Byte> cannyEdges = grayImage.Canny(45, 160, 3, true);
                Image<Gray, Byte> cannyEdges = grayImage.Canny(700, 1400, 5, true).SmoothGaussian(3).ThresholdBinary(new Gray(50), new Gray(255));

                // create corner strength image and do Harris
                /*
                var cornerImage = new Image<Gray, float>(cannyEdges.Size);
                CvInvoke.CornerHarris(cannyEdges, cornerImage, 7, 7, 0.03); //3, 3, 0.01)

                var rawBitmapFromCornerDetection =
                    cornerImage
                        .ThresholdBinary(new Gray(10), new Gray(255))
                        .ToBitmap();

                var cannyBitmap = cannyEdges.ToBitmap();

                var modifiedBitmap = LineExtractorFromCornerImage.RefineBitmap(rawBitmapFromCornerDetection);
                var representants = LineExtractorFromCornerImage.GimmeRepresentants(modifiedBitmap);
                var bbBitmap = LineExtractorFromCornerImage.FinalLine(modifiedBitmap, representants, bm);
                */
                /*
                LineSegment2D[][] lines = cannyEdges.HoughLinesBinary(0.1, Math.PI / 180.0, 1, 20, 1.0);

                foreach (var line in lines)
                {
                    CvInvoke.Line(cannyEdges, lines[1].);
                }
                */
                LineSegment2D[] lines;
                //lines = CvInvoke.HoughLinesP(cannyEdges, 0.3f, Math.PI / 720, 30, 100, 10); //gap between lines
                //lines = CvInvoke.HoughLinesP(cannyEdges, 0.2f, Math.PI / 2500, 20, 90, 10); //gap between lines

                //Distance resolution in pixel-related units

                //CvInvoke.HoughLines(cannyEdges,lines,2, Math.PI / 180, 40 );


                lines = cannyEdges.HoughLinesBinary(
                    0.8f,                  //Distance resolution in pixel-related units
                    Math.PI / 1500,     //Angle resolution measured in radians.
                    100,                 //threshold
                    150,//90                //min Line width
                    10                  //gap between lines
                )[0];


                /*
                using (var vector = new VectorOfPointF())
                {
                    CvInvoke.HoughLines(cannyEdges, vector,
                        1,
                        Math.PI / 1440,
                        180);

                    var linesList = new List<LineSegment2D>();
                    for (var i = 0; i < vector.Size; i++)
                    {
                        var rho = vector[i].X;
                        var theta = vector[i].Y;
                        var pt1 = new Point();
                        var pt2 = new Point();
                        var a = Math.Cos(theta);
                        var b = Math.Sin(theta);
                        var x0 = a * rho;
                        var y0 = b * rho;
                        pt1.X = (int)Math.Round(x0 + 2000 * (-b));
                        pt1.Y = (int)Math.Round(y0 + 2000 * (a));
                        pt2.X = (int)Math.Round(x0 - 2000 * (-b));
                        pt2.Y = (int)Math.Round(y0 - 2000 * (a));
                        linesList.Add(new LineSegment2D(pt1, pt2));
                    }

                    lines = linesList.ToArray();
                }

                */

                Image<Gray, Byte> drawnEdges = new Image<Gray, Byte>(cannyEdges.Size);
                foreach (LineSegment2D line in lines)
                    CvInvoke.Line(drawnEdges, line.P1, line.P2, new Bgr(Color.White).MCvScalar, 5);


                LineSegment2D[] lines2 = drawnEdges.HoughLinesBinary(
                    0.8f,                  //Distance resolution in pixel-related units
                    Math.PI / 1500,     //Angle resolution measured in radians.
                    100,                 //threshold
                    150,//90                //min Line width
                    17                  //gap between lines
                )[0];

                /*
                Image<Bgr, Byte> drawnEdges2 = new Image<Bgr, Byte>(cannyEdges.Size);
                foreach (LineSegment2D line in lines2)
                    CvInvoke.Line(drawnEdges2, line.P1, line.P2, new Bgr(RandomColor()).MCvScalar, 3);
                */

                List<LineSpecial> specialLines = new List<LineSpecial>();
                foreach (var line in lines2)
                {
                    try
                    {
                        if (line.P1.X < line.P2.X)
                        {
                            var low =
                                data.RawData[(int)Math.Floor(
                                    pointsFromColorToDepth[line.P1.Y * 1920 + line.P1.X].Y * 512 +
                                    pointsFromColorToDepth[line.P1.Y * 1920 + line.P1.X].X)];
                            var high =
                                data.RawData[(int)Math.Floor(
                                    pointsFromColorToDepth[line.P2.Y * 1920 + line.P2.X].Y * 512 +
                                    pointsFromColorToDepth[line.P2.Y * 1920 + line.P2.X].X)];

                            var lowcsp = new CameraSpacePoint() { X = low.X, Y = low.Y, Z = low.Z };
                            var highcsp = new CameraSpacePoint() { X = high.X, Y = high.Y, Z = high.Z };

                            specialLines.Add(new LineSpecial(line.P1, line.P2, lowcsp, highcsp, colorFrameData));
                        }
                        else
                        {
                            var low =
                                data.RawData[(int)Math.Floor(
                                    pointsFromColorToDepth[line.P2.Y * 1920 + line.P2.X].Y * 512 +
                                    pointsFromColorToDepth[line.P2.Y * 1920 + line.P2.X].X)];
                            var high =
                                data.RawData[(int)Math.Floor(
                                    pointsFromColorToDepth[line.P1.Y * 1920 + line.P1.X].Y * 512 +
                                    pointsFromColorToDepth[line.P1.Y * 1920 + line.P1.X].X)];

                            var lowcsp = new CameraSpacePoint() { X = low.X, Y = low.Y, Z = low.Z };
                            var highcsp = new CameraSpacePoint() { X = high.X, Y = high.Y, Z = high.Z };

                            specialLines.Add(new LineSpecial(line.P2, line.P1, lowcsp, highcsp, colorFrameData));
                        }
                    }
                    catch (Exception e)
                    {
                    }
                }

                ChooseGoodLines(specialLines);


                Image<Bgr, Byte> drawnEdges3 = new Image<Bgr, Byte>(cannyEdges.Size);


                foreach (var line in
                    //new List<LineSpecial>() { 
                    specialLines
                    .Where(x => x.otherLinesTouchCounter >= 3)
                    //.Where(x => x.length3D > 28f && x.length3D < 36f)
                    //.OrderByDescending(x => Math.Min(x.HighXPoint.Y, x.LowXPoint.Y))
                    //.OrderByDescending(x => x.length3D)
                    .OrderByDescending(x => x.length2D)
                    //.First()
                    .Take(1)
                    //}
                    )
                    CvInvoke.Line(colorImg, line.LowXPoint, line.HighXPoint, new Bgr(RandomColor()).MCvScalar, 3);

                var finalEdge = specialLines
                    .Where(x => x.otherLinesTouchCounter >= 3)
                    //.Where(x => x.length3D > 28f && x.length3D < 36f)
                    //.OrderByDescending(x => Math.Min(x.HighXPoint.Y, x.LowXPoint.Y))
                    //.OrderByDescending(x => x.length3D)
                    .OrderByDescending(x => x.length2D)
                    //.First()
                    .First();

                ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
                // restrict to the square made by one chessboard edge

                var first = new MyVector2D(finalEdge.HighCsp.X, finalEdge.HighCsp.Y);
                var second = new MyVector2D(finalEdge.LowCsp.X, finalEdge.LowCsp.Y);

                var firstSecondVector = MyVector2D.Difference(first, second);
                var normal1 = new MyVector2D(firstSecondVector.Y, -firstSecondVector.X);
                var normal2 = new MyVector2D(-normal1.X, -normal1.Y);

                var scaledNormal1 = MyVector2D.MultiplyByNumber(MyVector2D.Normalize(normal1), firstSecondVector.Magnitude());
                var scaledNormal2 = MyVector2D.MultiplyByNumber(MyVector2D.Normalize(normal2), firstSecondVector.Magnitude());

                var point1 = new MyVector2D(first.X + scaledNormal1.X, first.Y + scaledNormal1.Y);
                var point2 = new MyVector2D(first.X + scaledNormal2.X, first.Y + scaledNormal2.Y);

                int counterFor1 = 0;
                foreach (var point in finalEdge.pointsAtTheEndOfNormal)
                {
                    var point2D = new MyVector2D(point.X, point.Y);
                    if (MyVector2D.Distance(point2D, point1) < MyVector2D.Distance(point2D, point2))
                    {
                        counterFor1++;
                    }
                }

                MyVector2D finalNormal = counterFor1 > finalEdge.pointsAtTheEndOfNormal.Count / 2 ? scaledNormal1 : scaledNormal2;

                var third = new MyVector2D(first.X + finalNormal.X, first.Y + finalNormal.Y);
                var fourth = new MyVector2D(second.X + finalNormal.X, second.Y + finalNormal.Y);

                /////////*/**/*/*/*/*/*/*/*/*/*

                Bitmap squaredBm = new Bitmap(1920, 1080, PixelFormat.Format24bppRgb);
                Bitmap colorBitmap = colorImg.Bitmap;

                BitmapData squaredBmbitmapData = squaredBm.LockBits(new Rectangle(0, 0, squaredBm.Width, squaredBm.Height), ImageLockMode.ReadWrite, PixelFormat.Format24bppRgb);
                bitmapData = colorBitmap.LockBits(new Rectangle(0, 0, colorBitmap.Width, colorBitmap.Height), ImageLockMode.ReadWrite, PixelFormat.Format24bppRgb);
                unsafe
                {
                    byte* ptr = (byte*)squaredBmbitmapData.Scan0;
                    byte* ptrColor = (byte*)bitmapData.Scan0;

                    Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
                    StreamWriter sw = new StreamWriter($@"D:\Desktop\Clouds\neco{finalNormal.GetHashCode()}.txt");

                    //StreamWriter sw = new StreamWriter($@"D:\Desktop\neco{finalNormal.GetHashCode()}.txt");
                    //sw.WriteLine($"{first.X} {first.Y} {0} {255} {0} {0}");
                    //sw.WriteLine($"{second.X} {second.Y} {0} {255} {0} {0}");
                    //sw.WriteLine($"{third.X} {third.Y} {0} {255} {0} {0}");
                    //sw.WriteLine($"{fourth.X} {fourth.Y} {0} {255} {0} {0}");

                    Color[][] colors = new Color[9][];
                    for (int i = 0; i < 9; i++)
                    {
                        colors[i] = new Color[9];
                        for (int j = 0; j < 9; j++)
                        {
                            colors[i][j] = RandomColor();
                        }
                    }

                    for (int y = 0; y < height; y++)
                    {
                        for (int x = 0; x < width; x++)
                        {
                            int pixelPostion = (y * 1920 + x);
                            int rgbPositon = pixelPostion * 3;

                            double lengthToSubtract = firstSecondVector.Magnitude();

                            var depthReference = pointsFromColorToDepth[pixelPostion];
                            if (!double.IsInfinity(depthReference.Y) && !double.IsNaN(depthReference.Y))
                            {
                                var csp = data.RawData[(int)depthReference.X + (int)depthReference.Y * 512];

                                var dis1a = MyVector2D.DistancePointLine(first, second, csp.X, csp.Y) * 1000;
                                var dis1b = MyVector2D.DistancePointLine(third, fourth, csp.X, csp.Y) * 1000;

                                bool dis1 = dis1a +
                                            dis1b -
                                            lengthToSubtract * 1000 < 0.0001f;

                                var dis2a = MyVector2D.DistancePointLine(first, third, csp.X, csp.Y) * 1000;
                                var dis2b = MyVector2D.DistancePointLine(second, fourth, csp.X, csp.Y) * 1000;

                                bool dis2 = dis2a +
                                            dis2b -
                                            lengthToSubtract * 1000 < 0.0001f;




                                if ((dis1 && dis2))
                                {

                                    if (csp.Z < -0.012f) //(csp.Z > -0.007f)
                                {
                                        *(ptr + rgbPositon + 0) = *(ptrColor + rgbPositon + 0);
                                        *(ptr + rgbPositon + 1) = *(ptrColor + rgbPositon + 1);
                                        *(ptr + rgbPositon + 2) = *(ptrColor + rgbPositon + 2);

                                        sw.WriteLine($"{csp.X} {csp.Y} {csp.Z} {*(ptr + rgbPositon + 0)} {*(ptr + rgbPositon + 1)} {*(ptr + rgbPositon + 2)}");
                                    }
                                    else
                                    {
                                        int firstIndex = (int)Math.Floor(dis1a / (lengthToSubtract*1000 / 8));
                                        int secondIndex = (int)Math.Floor(dis2a / (lengthToSubtract*1000 / 8));

                                        *(ptr + rgbPositon + 0) = colors[firstIndex][secondIndex].R;
                                        *(ptr + rgbPositon + 1) = colors[firstIndex][secondIndex].G;
                                        *(ptr + rgbPositon + 2) = colors[firstIndex][secondIndex].B;

                                        sw.WriteLine($"{csp.X} {csp.Y} {csp.Z} {*(ptr + rgbPositon + 0)} {*(ptr + rgbPositon + 1)} {*(ptr + rgbPositon + 2)}");
                                    }
                                    

                                    /*
                                    *(ptr + rgbPositon + 0) = *(ptrColor + rgbPositon + 0);
                                    *(ptr + rgbPositon + 1) = *(ptrColor + rgbPositon + 1);
                                    *(ptr + rgbPositon + 2) = *(ptrColor + rgbPositon + 2);
                                     */
                                }
                                else
                                {
                                    *(ptr + rgbPositon + 0) = 255;
                                    *(ptr + rgbPositon + 1) = 255;
                                    *(ptr + rgbPositon + 2) = 255;
                                }
                                /*
                                if (!double.IsInfinity(csp.Y) && resultBools[(int)depthReference.X + (int)depthReference.Y * 512])
                                {
                                    sw.WriteLine($"{csp.X} {csp.Y} {csp.Z} {*(ptr + rgbPositon + 0)} {*(ptr + rgbPositon + 1)} {*(ptr + rgbPositon + 2)}");
                                }
                                */
                            }

                        }
                    }
                    sw.Close();
                }
                colorBitmap.UnlockBits(bitmapData);
                squaredBm.UnlockBits(squaredBmbitmapData);



                ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

                if (displayForm.IsDisposed || displayForm.Disposing)
                {
                    stateAutomata.RemoveVisualiser(this);
                }

                if (!(displayForm.IsDisposed || displayForm.Disposing))
                {
                    displayForm?.BeginInvoke(new MethodInvoker(
                        //() => { displayForm?.DisplayBitmap(drawnEdges.Bitmap); }
                        //() => { displayForm?.DisplayBitmap(drawnEdges3.Bitmap); }
                        () => { displayForm?.DisplayBitmap(squaredBm); }
                    ));
                }

                /*
                if (!(displayForm.IsDisposed || displayForm.Disposing))
                {
                    displayForm?.BeginInvoke(new MethodInvoker(
                        () => { displayForm?.DisplayBitmap(cannyEdges.ThresholdBinary(new Gray(5), new Gray(255)).ToBitmap()); }
                    ));
                }*/
            //}); // task end


        }

        public void ChooseGoodLines(List<LineSpecial> lines)
        {
            for (var i = 0; i < lines.Count; i++)
            {
                for (var j = 0; j < lines.Count; j++)
                {
                    if (lines[i] != lines[j])
                    {
                        bool lowIsNear = false;
                        bool highIsNear = false;

                        lowIsNear = IsPointNearLine3D(lines[i], lines[j], lines[j].LowCsp, false);
                        highIsNear = IsPointNearLine3D(lines[i], lines[j], lines[j].HighCsp, true);

                        if ((lowIsNear && !highIsNear) || (highIsNear && !lowIsNear))
                        {
                            if (lowIsNear)
                            {
                                lines[i].pointsAtTheEndOfNormal.Add(lines[j].HighCsp);
                            }
                            else
                            {
                                lines[i].pointsAtTheEndOfNormal.Add(lines[j].LowCsp);
                            }

                            lines[i].otherLinesTouchCounter++;
                        }
                    }
                }
            }
        }

        public bool IsPointNearLine3D(LineSpecial line1, LineSpecial line2, CameraSpacePoint csp, bool high)
        {
            if (high)
            {
                if (line1.HighXPoint.X < line2.HighXPoint.X || line1.LowXPoint.X > line2.LowXPoint.X)
                {
                    return false;
                }
            }
            else
            {
                if (line1.HighXPoint.X < line2.LowXPoint.X || line1.LowXPoint.X > line2.LowXPoint.X)
                {
                    return false;
                }
            }

            MyVector3D lowX = new MyVector3D(line1.LowCsp.X, line1.LowCsp.Y, line1.LowCsp.Z);
            MyVector3D highX = new MyVector3D(line1.HighCsp.X, line1.HighCsp.Y, line1.HighCsp.Z);
            MyVector3D point = new MyVector3D(csp.X, csp.Y, csp.Z);

            double citatel = (MyVector3D.CrossProduct(MyVector3D.Difference(point, lowX), MyVector3D.Difference(point, highX))).Magnitude();
            double jmenovatel = (MyVector3D.Difference(highX, lowX)).Magnitude();

            double vzdalenostKPrimce = (citatel / jmenovatel) * 1000;

            MyVector3D line = MyVector3D.Difference(highX, lowX);
            MyVector3D normalLine = MyVector3D.Difference(new MyVector3D(line2.HighCsp.X, line2.HighCsp.Y, line2.HighCsp.Z), new MyVector3D(line2.LowCsp.X, line2.LowCsp.Y, line2.LowCsp.Z));


            return (vzdalenostKPrimce < 4f &&
                    MyVector3D.Distance(point, lowX) * 1000 > 6 &&
                    MyVector3D.Distance(point, highX) * 1000 > 6 &&
                    MyVector3D.AngleInDeg(line, normalLine) > 70
                    ) ? true : false;
        }



        public class LineSpecial
        {
            public Point LowXPoint;
            public Point HighXPoint;
            public CameraSpacePoint LowCsp;
            public CameraSpacePoint HighCsp;
            public float avgY;
            public float length3D;
            public float length2D;
            public int otherLinesTouchCounter;
            public List<CameraSpacePoint> pointsAtTheEndOfNormal = new List<CameraSpacePoint>();

            public LineSpecial(Point lowXPoint, Point highXPoint, CameraSpacePoint lowCsp, CameraSpacePoint highCsp, byte[] colorFrameData)
            {
                LowXPoint = lowXPoint;
                HighXPoint = highXPoint;
                LowCsp = lowCsp;
                HighCsp = highCsp;

                float xdiff = lowXPoint.X - highXPoint.X;
                float ydiff = lowXPoint.Y - highXPoint.Y;
                length2D = (float)Math.Sqrt(xdiff * xdiff + ydiff * ydiff);

                length3D = (float)Math.Sqrt((lowCsp.X - highCsp.X) * (lowCsp.X - highCsp.X) + (lowCsp.Y - highCsp.Y) * (lowCsp.Y - highCsp.Y) + (lowCsp.Z - highCsp.Z) * (lowCsp.Z - highCsp.Z));
                length3D *= 100;
                avgY = (highXPoint.Y + lowXPoint.Y) / 2f;

                otherLinesTouchCounter = 0;
            }
            public static bool operator ==(LineSpecial c1, LineSpecial c2)
            {
                return c1.Equals(c2);
            }

            public static bool operator !=(LineSpecial c1, LineSpecial c2)
            {
                return !c1.Equals(c2);
            }
        }



        static Random rnd = new Random();

        public Color RandomColor()
        {
            switch (rnd.Next(10))
            {
                case 0:
                    return Color.Red;
                case 1:
                    return Color.Blue;
                case 2:
                    return Color.Aqua;
                case 3:
                    return Color.Chocolate;
                case 4:
                    return Color.CornflowerBlue;
                case 5:
                    return Color.DarkSeaGreen;
                case 6:
                    return Color.DeepPink;
                case 7:
                    return Color.Yellow;
                case 8:
                    return Color.BlueViolet;
                case 9:
                    return Color.SpringGreen;
                case 10:
                    return Color.Pink;
                default:
                    return Color.Cyan;
            }
        }

    }
}

