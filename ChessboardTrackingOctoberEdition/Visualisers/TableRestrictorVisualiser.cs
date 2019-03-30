using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ChessboardTrackingOctoberEdition.Kinect;
using ChessboardTrackingOctoberEdition.Shared;
using ChessboardTrackingOctoberEdition.TrackingPart;
using Emgu.CV;
using Emgu.CV.CvEnum;
using Emgu.CV.Structure;
using Kinect_v0._1;
using Microsoft.Kinect;

namespace ChessboardTrackingOctoberEdition.Visualisers
{
    class TableRestrictorVisualiser : IVisualiser
    {
        private DisplayForm displayForm;
        private IKinect kinect;
        private StateAutomataWrapper stateAutomata;

        private Image<Rgb, byte> colorImg;
        public int whateverPositive { get; set; } = 7;
        public int stEven { get; set; } = 7;
        public float stSmall { get; set; } = 0.01f;

        public TableRestrictorVisualiser(Form form, IKinect kinect, StateAutomataWrapper stateAutomata)
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

        public void Update(byte[] colorFrameData, ushort[] depthData, ushort[] infraredData, CameraSpacePoint[] cameraSpacePointsFromDepthData,
            DepthSpacePoint[] pointsFromColorToDepth, ColorSpacePoint[] pointsFromDepthToColor)
        {
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
                            *(ptr + rgbPositon + 0) = 0;
                            *(ptr + rgbPositon + 1) = 0;
                            *(ptr + rgbPositon + 2) = 0;
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
                                    *(ptr + rgbPositon + 0) = (byte)((colorFrameData[pixelPostion*4  ]));
                                    *(ptr + rgbPositon + 1) = (byte)((colorFrameData[pixelPostion*4 +1 ]));
                                    *(ptr + rgbPositon + 2) = (byte)((colorFrameData[pixelPostion*4 +2]));
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



            Image<Gray, Byte> grayImage = colorImg/*.Resize(1024, 576, Inter.Linear)*/.Convert<Gray, Byte>(); // 1024,728
            
            if (displayForm.IsDisposed || displayForm.Disposing)
            {
                stateAutomata.RemoveVisualiser(this);
            }

            if (!(displayForm.IsDisposed || displayForm.Disposing))
            {
                displayForm?.BeginInvoke(new MethodInvoker(
                    () => { displayForm?.DisplayBitmap(grayImage.ToBitmap()); }
                ));
            }

        }
    }
}
