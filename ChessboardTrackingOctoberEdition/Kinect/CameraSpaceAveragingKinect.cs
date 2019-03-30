using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Kinect;

namespace ChessboardTrackingOctoberEdition.Kinect
{
    class CameraSpaceAveragingKinect : IKinect
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

        /// <summary>
        /// Number of frames to the past, that will be averaged over
        /// </summary>
        private int numberOfFramesToAverage;

        /// <summary>
        /// Array for storing data for #numberOfFramesToAverage frames
        /// </summary>
        private CameraSpacePoint[][] frames;

        private int frameCounter;

        public CameraSpaceAveragingKinect(IKinect adaptedKinect, int numberOfFramesToAverage)
        {
            this.adaptedKinect = adaptedKinect;
            adaptedKinect.KinectMultiframeArrived += MultiframeArrived;

            ColorFrameDescription = adaptedKinect.ColorFrameDescription;
            DepthFrameDescription = adaptedKinect.DepthFrameDescription;
            InfraredFrameDescription = adaptedKinect.InfraredFrameDescription;

            this.numberOfFramesToAverage = numberOfFramesToAverage;

            InitFrames();

            frameCounter = 0;
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
            LoopIncrementFrameCounter();

            if (numberOfFramesToAverage > 1)
            {
                cameraSpacePointsFromDepthData =
                    GetAveragedCameraSpacePointsOverSeveralFrames(cameraSpacePointsFromDepthData);
            }

            KinectMultiframeArrived?.Invoke(
                colorFrameData,
                depthData,
                infraredData,
                cameraSpacePointsFromDepthData,
                pointsFromColorToDepth,
                pointsFromDepthToColor
            );
        }

        private CameraSpacePoint[] GetAveragedCameraSpacePointsOverSeveralFrames(CameraSpacePoint[] cameraSpacePoints)
        {
            ReplaceOldestFrameWithNewOne(cameraSpacePoints);
            return GetFinalAverage(cameraSpacePoints);
        }

        private void ReplaceOldestFrameWithNewOne(CameraSpacePoint[] cameraSpacePoints)
        {
            for (int i = 0; i < DepthFrameDescription.LengthInPixels; i++)
            {
                frames[i][frameCounter] = cameraSpacePoints[i];
            }
        }

        private CameraSpacePoint[] GetFinalAverage(CameraSpacePoint[] cameraSpacePoints)
        {
            for (int pixelIndex = 0; pixelIndex < frames.Length; pixelIndex++)
            {
                int validValuesCounter = 0;
                float sumX = 0;
                float sumY = 0;
                float sumZ = 0;

                for (int frameIndex = 0; frameIndex < frames[pixelIndex].Length; frameIndex++)
                {
                    if (!float.IsInfinity(frames[pixelIndex][frameIndex].X))
                    {
                        validValuesCounter++;
                        sumX += frames[pixelIndex][frameIndex].X;
                        sumY += frames[pixelIndex][frameIndex].Y;
                        sumZ += frames[pixelIndex][frameIndex].Z;
                    }
                }

                if (validValuesCounter == 0)
                {
                    cameraSpacePoints[pixelIndex].X = float.NegativeInfinity;
                    cameraSpacePoints[pixelIndex].Y = float.NegativeInfinity;
                    cameraSpacePoints[pixelIndex].Z = float.NegativeInfinity;
                }
                else
                {
                    cameraSpacePoints[pixelIndex].X = sumX / validValuesCounter;
                    cameraSpacePoints[pixelIndex].Y = sumY / validValuesCounter;
                    cameraSpacePoints[pixelIndex].Z = sumZ / validValuesCounter;
                }
            }

            return cameraSpacePoints;
        }

        private void LoopIncrementFrameCounter()
        {
            frameCounter++;
            if (frameCounter >= numberOfFramesToAverage)
            {
                frameCounter = 0;
            }
        }

        private void InitFrames()
        {
            // init frame array
            frames = new CameraSpacePoint[DepthFrameDescription.LengthInPixels][];

            // init concrete frame one by one
            for (int i = 0; i < frames.Length; i++)
            {
                frames[i] = new CameraSpacePoint[numberOfFramesToAverage];
            }

            // fill them with invalid "-inf" values
            for (int i = 0; i < frames.Length; i++)
            {
                for (int j = 0; j < frames[i].Length; j++)
                {
                    frames[i][j] = new CameraSpacePoint()
                    {
                        X = float.NegativeInfinity,
                        Y = float.NegativeInfinity,
                        Z = float.NegativeInfinity
                    };
                }
            }
        }

        public void Dispose()
        {
            adaptedKinect?.Dispose();
        }
    }
}
