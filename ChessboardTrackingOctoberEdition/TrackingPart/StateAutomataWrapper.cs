using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Runtime.InteropServices;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using ChessboardTrackingOctoberEdition.Kinect;
using ChessboardTrackingOctoberEdition.Shared;
using ChessboardTrackingOctoberEdition.Visualisers;
using Microsoft.Kinect;

namespace ChessboardTrackingOctoberEdition.TrackingPart
{
    class StateAutomataWrapper
    {
        public CancellationToken Token { get; set; }

        private IState innerState;
        private IKinect kinect;

        private UserPartForm userForm;
        private TrackingPartForm trackingForm;

        private List<IVisualiser> visualisers;



        public void StartTracking(UserPartForm userForm, TrackingPartForm trackingForm, CancellationToken token)
        {
            this.userForm = userForm;
            this.trackingForm = trackingForm;
            Token = token;

            var rawKinect = new RawKinect() { Token = token };
            //var averagingKinect = new CameraSpaceAveragingKinect(rawKinect, 7) { Token = token };
            var bilateralKinect = new BilateralDepthFilterKinect(rawKinect) {Token = token};
            kinect = bilateralKinect;

            visualisers = new List<IVisualiser>();

            //visualisers.Add(new ColorDataVisualizer(userForm, kinect, this));
            //visualisers.Add(new FirstCornerDetectionVisualiser(userForm, kinect, this) { whateverPositive = 15, stEven = 13, stSmall = 0.01f });
            //visualisers.Add(new TableRestrictorVisualiser(userForm, kinect, this));
            //visualisers.Add(new TableRestrictorCannyVisualiser(userForm, kinect, this));
            //visualisers.Add(new TableRestrictorCannyCornerVisualiser(userForm, kinect, this));
            //visualisers.Add(new ChessboardLocalizationSecond(userForm, kinect, this));
            //visualisers.Add(new NewTrial(userForm, kinect, this));
            //visualisers.Add(new NewLines(userForm, kinect, this));
            visualisers.Add(new FittingChessboard(userForm, kinect, this));
            //visualisers.Add(new DepthCannyVisualizer(userForm, kinect, this));

            kinect.KinectMultiframeArrived += MultiframeArrived;
        }

        public void RemoveVisualiser(IVisualiser visualiserToRemove)
        {
            visualisers.Remove(visualiserToRemove);
        }

        private void MultiframeArrived(
            byte[] colorFrameData,
            ushort[] depthData,
            ushort[] infraredData,
            CameraSpacePoint[] cameraSpacePointsFromDepthData,
            DepthSpacePoint[] pointsFromColorToDepth,
            ColorSpacePoint[] pointsFromDepthToColor
        )
        {
            try
            {
                trackingForm?.BeginInvoke(new MethodInvoker(
                        () =>
                        {
                            trackingForm.IncreaseFrameCount();
                        }
                    ));

                foreach (var visualiser in visualisers)
                {
                    visualiser.Update(colorFrameData, depthData, infraredData, cameraSpacePointsFromDepthData, pointsFromColorToDepth, pointsFromDepthToColor);
                }
            }
            catch (Exception e)
            when (e is ObjectDisposedException || e is InvalidOperationException)
            {

            }
        }

    }
}
