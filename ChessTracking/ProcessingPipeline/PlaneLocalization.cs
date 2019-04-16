using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using ChessTracking.MultithreadingMessages;
using Emgu.CV;
using Emgu.CV.Structure;
using Kinect_v0._1;
using Microsoft.Kinect;

namespace ChessTracking.ProcessingPipeline
{
    class PlaneLocalization
    {
        public Pipeline Pipeline { get; }

        public Tuple<double, double> AnglesOfFirstRotation { get; set; } = null;
        public bool[] LocalizedTableMask { get; private set; }

        public PlaneLocalization(Pipeline pipeline)
        {
            this.Pipeline = pipeline;
        }

        public PlaneDoneData Recalibrate(RawData rawData)
        {
            var planeData = new PlaneDoneData(rawData);

            Data data = new Data(planeData.CameraSpacePointsFromDepthData);
            data.CutOffMinMaxDepth(Config.minDepth, Config.maxDepth);
            data.RANSAC();
            data.LargestTableArea();
            data.LinearRegression();
            data.LargestTableArea();
            data.LinearRegression();
            data.LargestTableArea();
            data.LinearRegression();
            data.LargestTableArea();
            AnglesOfFirstRotation = data.RotationTo2DModified();
            LocalizedTableMask = data.ConvexHullAlgorithmModified();

            var colorImg = ReturnColorImageOfTable(LocalizedTableMask, planeData.ColorFrameData, planeData.PointsFromColorToDepth);
            planeData.MaskedColorImageOfTable = colorImg;

            return planeData;
        }

        public PlaneDoneData Track(RawData rawData)
        {
            var planeData = new PlaneDoneData(rawData);
            
            if (planeData.VisualisationType == VisualisationType.RawRGB)
                planeData.Bitmap = ReturnColorBitmap(planeData.ColorFrameData);

            planeData.CannyDepthData = CannyAppliedToDepthData(planeData.CameraSpacePointsFromDepthData);

            var colorImg = ReturnColorImageOfTable(LocalizedTableMask, planeData.ColorFrameData, planeData.PointsFromColorToDepth);
            planeData.MaskedColorImageOfTable = colorImg;

            if (planeData.VisualisationType == VisualisationType.MaskedColorImageOfTable)
                planeData.Bitmap = colorImg.Bitmap;
            
            return planeData;
        }

        private Image<Rgb, byte> ReturnColorImageOfTable(bool[] resultBools, byte[] colorFrameData, DepthSpacePoint[] pointsFromColorToDepth)
        {

            //Image<Rgb, byte> colorImg = new Image<Rgb, byte>(kinect.ColorFrameDescription.Width, kinect.ColorFrameDescription.Height);
            Image<Rgb, byte> colorImg = new Image<Rgb, byte>(1920, 1080);

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
                                    *(ptr + rgbPositon + 2) = (byte)((colorFrameData[pixelPostion * 4]));
                                    *(ptr + rgbPositon + 1) = (byte)((colorFrameData[pixelPostion * 4 + 1]));
                                    *(ptr + rgbPositon + 0) = (byte)((colorFrameData[pixelPostion * 4 + 2]));
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

            return colorImg;
        }

        private Bitmap ReturnColorBitmap(byte[] colorFrameData)
        {
            Bitmap bmp = new Bitmap(1920, 1080, PixelFormat.Format32bppArgb);
            BitmapData bmpData = bmp.LockBits(new Rectangle(0, 0,
                    bmp.Width,
                    bmp.Height),
                ImageLockMode.WriteOnly,
                bmp.PixelFormat);

            IntPtr pNative = bmpData.Scan0;
            Marshal.Copy(colorFrameData, 0, pNative, colorFrameData.Length);

            bmp.UnlockBits(bmpData);

            return bmp;
        }


        private byte[] CannyAppliedToDepthData(CameraSpacePoint[] cameraSpacePointsFromDepthData)
        {
            int depthColorChange = 128;

            Bitmap bbb = new Bitmap(512, 424, PixelFormat.Format24bppRgb);
            BitmapData bbbData = bbb.LockBits(new Rectangle(0, 0, bbb.Width, bbb.Height), ImageLockMode.ReadWrite, PixelFormat.Format24bppRgb);

            unsafe
            {
                byte* ptr = (byte*)bbbData.Scan0;

                // draw pixel according to its type
                for (int y = 0; y < 424; y++)
                {
                    // compensates sensors natural flip of image
                    for (int x = 0; x < 512; x++)
                    {
                        int position = y * 512 + x;

                        byte value = (byte)(cameraSpacePointsFromDepthData[position].Z * depthColorChange);

                        *ptr++ = value;
                        *ptr++ = value;
                        *ptr++ = value;

                    }
                }
            }

            bbb.UnlockBits(bbbData);

            //////////////////
            Image<Gray, byte> CanniedImage = new Image<Gray, byte>(bbb);
            CanniedImage = CanniedImage.Canny(1000, 1200, 7, true).SmoothGaussian(3, 3, 1, 1).ThresholdBinary(new Gray(65), new Gray(255));
            var canniedBytes = CanniedImage.Bytes;
            return canniedBytes;
        }
    }
}
