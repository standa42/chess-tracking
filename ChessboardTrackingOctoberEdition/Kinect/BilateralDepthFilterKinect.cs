using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Emgu.CV;
using Emgu.CV.Structure;
using Microsoft.Kinect;

namespace ChessboardTrackingOctoberEdition.Kinect
{
    class BilateralDepthFilterKinect : IKinect
    {
        public event KinectMultiframeArrived KinectMultiframeArrived;
        public CancellationToken Token { get; set; }

        public FrameDescription ColorFrameDescription { get; }
        public FrameDescription DepthFrameDescription { get; }
        public FrameDescription InfraredFrameDescription { get; }

        /// <summary>
        /// Source Kinect whose information will be averaged
        /// </summary>
        private IKinect adaptedKinect;

        public BilateralDepthFilterKinect(IKinect adaptedKinect)
        {
            this.adaptedKinect = adaptedKinect;
            adaptedKinect.KinectMultiframeArrived += MultiframeArrived;

            ColorFrameDescription = adaptedKinect.ColorFrameDescription;
            DepthFrameDescription = adaptedKinect.DepthFrameDescription;
            InfraredFrameDescription = adaptedKinect.InfraredFrameDescription;
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
            /*
            float[,,] channelsImage = new float[512, 424, 1];

            for (int y = 0; y < 424; y++)
            {
                for (int x = 0; x < 512; x++)
                {
                    if (!float.IsInfinity(cameraSpacePointsFromDepthData[y * 512 + x].Z))
                    {
                        channelsImage[x, y, 0] = cameraSpacePointsFromDepthData[y * 512 + x].Z;
                    }
                    else
                    {
                        channelsImage[x, y, 0] = -1000;
                    }
                }
            }

            Image<Gray, float> imgToFilter = new Image<Gray, float>(channelsImage);

            var filteredImage = imgToFilter;//.Canny(50, 20);//.Sobel(0.1f,0.1f,3);
            
            float[,] k = {
                {1f, 2f, 1f},
                {2f, 4f, 2f},
                {1f, 2f, 1f}};
            ConvolutionKernelF kernel = new ConvolutionKernelF(k);
            var filteredImage = imgToFilter * kernel;
            

            var fiteredData = filteredImage.Data;

            for (int y = 0; y < 424; y++)
            {
                for (int x = 0; x < 512; x++)
                {
                    if (fiteredData[x, y, 0] < 0.0001f)
                    {
                        cameraSpacePointsFromDepthData[y * 512 + x].Z = fiteredData[x, y, 0];
                    }
                    else
                    {
                        cameraSpacePointsFromDepthData[y * 512 + x].Z = float.NegativeInfinity;
                    }
                }
            }
            */


            KinectMultiframeArrived?.Invoke(
                colorFrameData,
                depthData,
                infraredData,
                cameraSpacePointsFromDepthData,
                pointsFromColorToDepth,
                pointsFromDepthToColor
            );
        }

        public void Dispose()
        {
            adaptedKinect?.Dispose();
        }
    }
}
