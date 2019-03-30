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
using Microsoft.Kinect;

namespace ChessboardTrackingOctoberEdition.Visualisers
{
    class ColorDataVisualizer : IVisualiser
    {
        private DisplayForm displayForm;
        private IKinect kinect;
        private StateAutomataWrapper stateAutomata;

        public ColorDataVisualizer(Form form, IKinect kinect, StateAutomataWrapper stateAutomata)
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


                    if (!(displayForm.IsDisposed || displayForm.Disposing))
                    {
                        displayForm?.BeginInvoke(new MethodInvoker(
                            () => { displayForm?.DisplayBitmap(bmp); }
                        ));
                    }
                }
            );
        }
    }
}
