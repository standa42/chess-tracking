using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.Runtime.InteropServices;
using ChessTracking.ImageProcessing.PipelineData;
using ChessTracking.ImageProcessing.PipelineParts.General;
using ChessTracking.ImageProcessing.PlaneAlgorithms;
using ChessTracking.MultithreadingMessages;
using Emgu.CV;
using Emgu.CV.Structure;
using Microsoft.Kinect;

namespace ChessTracking.ImageProcessing.PipelineParts.Stages
{
    class PlaneLocalization
    {
        public Pipeline Pipeline { get; }
        
        public bool[] LocalizedTableMask { get; private set; }

        public PlaneLocalization(Pipeline pipeline)
        {
            this.Pipeline = pipeline;
        }

        public PlaneDoneData Recalibrate(InputData inputData)
        {
            var planeData = new PlaneDoneData(inputData);

            Data data = new Data(planeData.KinectData.CameraSpacePointsFromDepthData);
            data.CutOffMinMaxDepth(PlaneLocalizationConfig.MinDepth, PlaneLocalizationConfig.MaxDepth);
            data.Ransac();
            data.LargestTableArea();
            data.LinearRegression();
            data.LargestTableArea();
            data.LinearRegression();
            data.LargestTableArea();
            data.LinearRegression();
            data.LargestTableArea();
            data.RotationTo2DModified();
            LocalizedTableMask = data.ConvexHullAlgorithmModified();

            var colorImg = ReturnColorImageOfTable(LocalizedTableMask, planeData.KinectData.ColorFrameData, planeData.KinectData.PointsFromColorToDepth);
            planeData.PlaneData.MaskedColorImageOfTable = colorImg;
            return planeData;
        }

        public PlaneDoneData Track(InputData inputData)
        {
            var planeData = new PlaneDoneData(inputData);
            
            if (planeData.UserParameters.VisualisationType == VisualisationType.RawRGB)
                planeData.ResultData.VisualisationBitmap = ReturnColorBitmap(planeData.KinectData.ColorFrameData);

            planeData.PlaneData.CannyDepthData = CannyAppliedToDepthData(planeData.KinectData.CameraSpacePointsFromDepthData);
            planeData.PlaneData.MaskOfTable = LocalizedTableMask;
            planeData.PlaneData.ColorBitmap = ReturnColorBitmap(planeData.KinectData.ColorFrameData);

            var colorImg = ReturnColorImageOfTable(LocalizedTableMask, planeData.KinectData.ColorFrameData, planeData.KinectData.PointsFromColorToDepth);
            planeData.PlaneData.MaskedColorImageOfTable = colorImg;
            //planeData.MaskedColorImageOfTable._EqualizeHist(); // TODO zjisitit jestli to pomáhá při stabilizaci osvětlení

            if (planeData.UserParameters.VisualisationType == VisualisationType.MaskedColorImageOfTable)
                planeData.ResultData.VisualisationBitmap = colorImg.Bitmap;
            
            return planeData;
        }

        private Image<Rgb, byte> ReturnColorImageOfTable(bool[] resultBools, byte[] colorFrameData, DepthSpacePoint[] pointsFromColorToDepth)
        {
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

            Bitmap bitmap = new Bitmap(512, 424, PixelFormat.Format24bppRgb);
            BitmapData bitmapData = bitmap.LockBits(new Rectangle(0, 0, bitmap.Width, bitmap.Height), ImageLockMode.ReadWrite, PixelFormat.Format24bppRgb);

            unsafe
            {
                byte* ptr = (byte*)bitmapData.Scan0;

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

            bitmap.UnlockBits(bitmapData);

            //////////////////
            Image<Gray, byte> CannyAppliedImage = new Image<Gray, byte>(bitmap);
            CannyAppliedImage = CannyAppliedImage.Canny(1000, 1200, 7, true).SmoothGaussian(3, 3, 1, 1).ThresholdBinary(new Gray(65), new Gray(255));
            var canniedBytes = CannyAppliedImage.Bytes;
            return canniedBytes;
        }
    }
}
