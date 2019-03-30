using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Diagnostics;
using System.Runtime.CompilerServices;

namespace Kinect_v0._1
{
    public static class Config
    {
        // Depth image dimensions
        public static readonly int DepthImageWidth = 512;
        public static readonly int DepthImageHeight = 424;

        // Number of paralel parts (multithreading in algorithms)
        public static int paralelTasksCount = Environment.ProcessorCount * 2;

        // Change of color based on distances in space
        public static int DepthColorChange = 128;

        // Frame count and fps
        public static int FrameThatStartTracking = 30;
        public static int FrameCount;
        public static long FPSLastMeasuredTime;
        public static int FPSLastMeasuredFrameCount = 0;
        public static int CurrentFPS = 0;

        // Optimalizations
        public static int TrackingIsPerformedEveryXthFrame = 5;
        public static int RegressionIsEvaluatedOnEveryXthTablePixel = 1;
        public static int GridSizeForTestingPlaneFitInRANSAC = 10; // 10

        // Constants in image filters
        public static int NumberOfFramesToAverage = 6;
        public static int NumberOfFramesForMedian = 5;
        public static int MedianPosition = (NumberOfFramesForMedian / 2) + 1;
        public static int frameNr = 0;

        // Structure for data averaging filter
        public static MyCameraSpacePoint[][] DataToMean;

        // Structures for median time algorithm
        public static float[][] queues;
        public static float[][] medians;

        // Interval from camera that is evaluated
        public static float minDepth = 0.5f;
        public static float maxDepth = 2.2f;

        // Debug visualization of RANSAC pseudorandom triangles
        public static bool vykreslovatTrojuhelniky = false;

        public static float RegreseTloustka = 0.007f;

        // RANSAC constants
        public static int[] tringlePointsForRANSAC;
        public static int triangleCount = 0;
        public static float RansacThickness = 0.008f;
        public static double finalD = 0;
        public static MyVector3D finalNormal = new MyVector3D(0, 0, 0);
        public static int maxObservedPlaneSize = 0;
        public static object lockingObj = new object();

        // Allowed distance of points of objects
        public static float distanceOfObjectPoints = 0.01f;
        public static bool LocationEnabled = false;

        // Gaussian filter on z coordinate
        public static bool Gaussian = false;

        // Serialize to xyz file
        public static bool SerializeThisFrameToFile = false;

        // Should be plane tracked?
        public static bool PlaneIsTracked = true;


        // Initializations
        static Config()
        {
            CreateArrayForFramesToBeAveraged();
        }

        public static void CreateArrayForFramesToBeAveraged()
        {
            Config.DataToMean = new MyCameraSpacePoint[512 * 424][];

            for (int i = 0; i < 512 * 424; i++)
            {
                Config.DataToMean[i] = new MyCameraSpacePoint[Config.NumberOfFramesToAverage];
            }
        }
    }
}
