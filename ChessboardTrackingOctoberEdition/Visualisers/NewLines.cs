using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Runtime.Remoting.Messaging;
using System.Text;
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
using Microsoft.Kinect;
using Sandbox.Forms.HarrisCornerDetection;
using Random = System.Random;

namespace ChessboardTrackingOctoberEdition.Visualisers
{
    class NewLines : IVisualiser
    {
        private DisplayForm displayForm;
        private IKinect kinect;
        private StateAutomataWrapper stateAutomata;

        private Image<Rgb, byte> colorImg;
        public int whateverPositive { get; set; } = 7;
        public int stEven { get; set; } = 7;
        public float stSmall { get; set; } = 0.01f;

        public NewLines(Form form, IKinect kinect, StateAutomataWrapper stateAutomata)
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

            Task.Run(() =>
            {
                Image<Gray, Byte> grayImage = colorImg /*.Resize(1024, 576, Inter.Linear)*/.Convert<Gray, Byte>()
                    //.ThresholdAdaptive(new Gray(205), AdaptiveThresholdType.GaussianC, ThresholdType.Otsu, 3, new Gray(255));
                    ;//.ThresholdBinary(new Gray(205), new Gray(255)); ; // 1024,728
                
                var binarizedImg = new Image<Gray, byte>(grayImage.Width, grayImage.Height);

                CvInvoke.Threshold(grayImage, binarizedImg, 200, 255, ThresholdType.Otsu /*| ThresholdType.Binary*/);
                
                //Image<Gray, Byte> cannyEdges = grayImage.Canny(45, 160, 3, true);
                Image<Gray, Byte> cannyEdges = grayImage.Canny(700, 1400, 5, true).SmoothGaussian(3).ThresholdBinary(new Gray(50), new Gray(255));

                //CvInvoke.cvThreshold(grayImg, binImg, 100, 255, THRESH.CV_THRESH_OTSU | THRESH.CV_THRESH_BINARY);

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
                    50,                 //threshold
                    80,//90                //min Line width
                    15                  //gap between lines
                )[0];
                
                Image<Bgr, Byte> drawnEdges = new Image<Bgr, Byte>(new Size(cannyEdges.Width,cannyEdges.Height) /*cannyEdges.ToBitmap()*/);
                foreach (LineSegment2D line in lines)
                    CvInvoke.Line(drawnEdges, line.P1, line.P2, new Bgr(/*Color.Red*/ /*RandomColor()*/ Color.White).MCvScalar, 1);

                //var lines2 = CvInvoke.HoughLinesP(cannyEdges, 0.3f, Math.PI / 720, 30, 100, 10); //gap between lines
                //var lines2 = CvInvoke.HoughLinesP(drawnEdges.Convert<Gray, byte>(), 0.4f, Math.PI / 1200, 30, 90, 15); //gap between lines

                
                var lines2 = drawnEdges.Convert<Gray, byte>().HoughLinesBinary(
                    0.8f,                  //Distance resolution in pixel-related units
                    Math.PI / 1500,     //Angle resolution measured in radians.
                    50,                 //threshold
                    90,//90                //min Line width
                    10                  //gap between lines
                )[0];
                

                //lines2 = FilterLinesBasedOnAngle(lines2, 25);



                Image<Bgr, Byte> drawnEdges2 = new Image<Bgr, Byte>( /*grayImage.Bitmap*/ /*colorImg.Bitmap*/ new Size(cannyEdges.Width, cannyEdges.Height) /*cannyEdges.ToBitmap()*/);
                foreach (LineSegment2D line in lines2)
                    CvInvoke.Line(drawnEdges2, line.P1, line.P2, new Bgr(/*Color.Red*/ RandomColor() /*Color.White*/).MCvScalar, 4);


                if (displayForm.IsDisposed || displayForm.Disposing)
                {
                    stateAutomata.RemoveVisualiser(this);
                }

                if (!(displayForm.IsDisposed || displayForm.Disposing))
                {
                    displayForm?.BeginInvoke(new MethodInvoker(
                        () =>
                        {
                            displayForm?.DisplayBitmap(drawnEdges2.Bitmap);
                        }
                    ));
                }
                
            });


        }

        private LineSegment2D[] FilterLinesBasedOnAngle(LineSegment2D[] lines, int angle)
        {
            int deg = 180;
            var resultLines = new List<LineSegment2D>();

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

                int theta = mod(((int)ConvertRadiansToDegrees( Math.Atan2(diffY, diffX))), deg) ;
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
                    number += linesByAngle[mod(j,deg)].Count;
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
                var linesOfCertainAngle = linesByAngle[mod(j, deg)];

                foreach (var lineOfCeratinAngle in linesOfCertainAngle)
                {
                    resultLines.Add(lineOfCeratinAngle);
                }

                linesByAngle[mod(j, deg)].Clear();
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
                    number += linesByAngle[mod(j, deg)].Count;
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
                var linesOfCertainAngle = linesByAngle[mod(j, deg)];

                foreach (var lineOfCeratinAngle in linesOfCertainAngle)
                {
                    resultLines.Add(lineOfCeratinAngle);
                }

                linesByAngle[mod(j, deg)].Clear();
            }


            return resultLines.ToArray();
        }

        public static double ConvertRadiansToDegrees(double radians)
        {
            double degrees = (180 / Math.PI) * radians;
            return (degrees);
        }

        int mod(int x, int m)
        {
            return (x % m + m) % m;
        }

        Random rnd = new Random();

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



