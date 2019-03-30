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
using Emgu.CV.CvEnum;
using Emgu.CV.Structure;
using Microsoft.Kinect;

namespace ChessboardTrackingOctoberEdition.Visualisers
{
    class FirstCornerDetectionVisualiser : IVisualiser
    {
        private DisplayForm displayForm;
        private IKinect kinect;
        private StateAutomataWrapper stateAutomata;

        private Image<Bgra, byte> colorImg;
        public int whateverPositive { get; set; } = 7;
        public int stEven { get; set; } = 7;
        public float stSmall { get; set; } = 0.01f;

        public FirstCornerDetectionVisualiser(Form form, IKinect kinect, StateAutomataWrapper stateAutomata)
        {
            this.kinect = kinect;
            this.stateAutomata = stateAutomata;
            form.Invoke(new MethodInvoker(() =>
            {
                DisplayForm newForm = new DisplayForm();
                displayForm = newForm;
                newForm.SetDimensions(kinect.ColorFrameDescription.Width / 2, kinect.ColorFrameDescription.Height / 2);
                newForm.Show();
                displayForm.BeginInvoke(new MethodInvoker(() =>
                    displayForm.Text = $"{whateverPositive} {stEven} {stSmall}"));
            }));

            colorImg = new Image<Bgra, byte>(kinect.ColorFrameDescription.Width, kinect.ColorFrameDescription.Height);
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
                    
                    colorImg.Bytes = colorFrameData;

                    Image<Gray, Byte> grayImage = colorImg/*.Resize(1024, 576, Inter.Linear)*/.Convert<Gray, Byte>(); // 1024,728


                    Image<Gray, float> m_CornerImage = null;

                    // create corner strength image and do Harris
                    m_CornerImage = new Image<Gray, float>(grayImage.Size).ThresholdBinary(new Gray(220), new Gray(255));
                    CvInvoke.CornerHarris(grayImage, m_CornerImage, whateverPositive, stEven, stSmall); //3, 3, 0.01)

                    if (displayForm.IsDisposed || displayForm.Disposing)
                    {
                        stateAutomata.RemoveVisualiser(this);
                    }

                    if (!(displayForm.IsDisposed || displayForm.Disposing))
                    {
                        displayForm?.BeginInvoke(new MethodInvoker(
                            () => { displayForm?.DisplayBitmap(m_CornerImage.ToBitmap()); }
                        ));
                    }
                }
            );
        }
    }
}
