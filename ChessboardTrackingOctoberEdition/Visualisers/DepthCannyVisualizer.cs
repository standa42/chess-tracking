using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ChessboardTrackingOctoberEdition.Kinect;
using ChessboardTrackingOctoberEdition.Shared;
using ChessboardTrackingOctoberEdition.TrackingPart;
using Emgu.CV;
using Emgu.CV.Structure;
using Microsoft.Kinect;

namespace ChessboardTrackingOctoberEdition.Visualisers
{
    class DepthCannyVisualizer : IVisualiser
    {
        private DisplayForm displayForm;
        private IKinect kinect;
        private StateAutomataWrapper stateAutomata;

        public DepthCannyVisualizer(Form form, IKinect kinect, StateAutomataWrapper stateAutomata)
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
        }

        public void Update(byte[] colorFrameData, ushort[] depthData, ushort[] infraredData, CameraSpacePoint[] cameraSpacePointsFromDepthData,
            DepthSpacePoint[] pointsFromColorToDepth, ColorSpacePoint[] pointsFromDepthToColor)
        {
            Task.Run(
                () =>
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

                    if (displayForm.IsDisposed || displayForm.Disposing)
                    {
                        stateAutomata.RemoveVisualiser(this);
                    }

                    /////////////////
                    int depthColorChange = 128;

                    Bitmap bm = new Bitmap(512, 424, PixelFormat.Format24bppRgb);
                    BitmapData bmData = bm.LockBits(new Rectangle(0, 0, bm.Width, bm.Height), ImageLockMode.ReadWrite, PixelFormat.Format24bppRgb);

                    unsafe
                    {
                        byte* ptr = (byte*)bmData.Scan0;

                        // draw pixel according to its type
                        for (int y = 0; y < 424; y++)
                        {
                            // compensates sensors natural flip of image
                            for (int x = 512 - 1; x >= 0; x--)
                            {
                                int position = y * 512 + x;

                                byte value = (byte)(cameraSpacePointsFromDepthData[position].Z * depthColorChange);

                                *ptr++ = value;
                                *ptr++ = value;
                                *ptr++ = value;

                            }
                        }
                    }

                    bm.UnlockBits(bmData);
             
                    //////////////////
                    Image<Gray,byte> grayImage = new Image<Gray, byte>(bm);
                    grayImage = grayImage.Canny(1000, 1200, 7, true).SmoothGaussian(3,3,1,1).ThresholdBinary(new Gray(70), new Gray(255));
                    /*
                    Image<Gray,float> floatyImage = new Image<Gray, float>(512,424);
                    float[,,] floatyData = new float[512, 424, 1];
                    floatyImage.Data = floatyData;
                    floatyImage = floatyImage.Sobel(3, 3, 3);
                    */

                    if (!(displayForm.IsDisposed || displayForm.Disposing))
                    {
                        displayForm?.BeginInvoke(new MethodInvoker(
                            () => { displayForm?.DisplayBitmap(grayImage.Bitmap); }
                        ));
                    }
                }
            );
        }
    }
}
