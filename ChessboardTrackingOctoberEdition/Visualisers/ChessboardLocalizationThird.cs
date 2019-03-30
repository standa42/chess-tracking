using System;
using System.Collections.Generic;
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
    class ChessboardLocalizationThird : IVisualiser
    {
        private DisplayForm displayForm;
        private IKinect kinect;
        private StateAutomataWrapper stateAutomata;

        private Image<Rgb, byte> colorImg;
        public int whateverPositive { get; set; } = 7;
        public int stEven { get; set; } = 7;
        public float stSmall { get; set; } = 0.01f;

        public ChessboardLocalizationThird(Form form, IKinect kinect, StateAutomataWrapper stateAutomata)
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

                Image<Bgr, Byte> drawnEdges = new Image<Bgr, Byte>(cannyEdges.ToBitmap());
                foreach (LineSegment2D line in lines)
                    CvInvoke.Line(drawnEdges, line.P1, line.P2, new Bgr(Color.Red).MCvScalar, 3);



                if (displayForm.IsDisposed || displayForm.Disposing)
                {
                    stateAutomata.RemoveVisualiser(this);
                }

                if (!(displayForm.IsDisposed || displayForm.Disposing))
                {
                    displayForm?.BeginInvoke(new MethodInvoker(
                        () => { displayForm?.DisplayBitmap(drawnEdges.Bitmap); }
                    ));
                }

                /*
                if (!(displayForm.IsDisposed || displayForm.Disposing))
                {
                    displayForm?.BeginInvoke(new MethodInvoker(
                        () => { displayForm?.DisplayBitmap(cannyEdges.ThresholdBinary(new Gray(5), new Gray(255)).ToBitmap()); }
                    ));
                }*/
            });


        }

        /*
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
        */
    }
}

