using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Diagnostics;
using System.Runtime.CompilerServices;

namespace ChessTracking.ProcessingPipeline.Plane
{
    public static class Config
    {
        // Depth image dimensions
        public static readonly int DepthImageWidth = 512;
        public static readonly int DepthImageHeight = 424;

        // Number of paralel parts (multithreading in algorithms)
        public static int paralelTasksCount = Environment.ProcessorCount * 2;
        
        // Optimalizations
        public static int RegressionIsEvaluatedOnEveryXthTablePixel = 1;
        public static int GridSizeForTestingPlaneFitInRANSAC = 10; // 10
        
        // Interval from camera that is evaluated
        public static float minDepth = 0.5f;
        public static float maxDepth = 2.2f;

        public static float RegreseTloustka = 0.007f;

        // RANSAC constants
        public static int[] tringlePointsForRANSAC;
        public static int triangleCount = 0;
        public static float RansacThickness = 0.008f;
        public static double finalD = 0;
        public static MyVector3D finalNormal = new MyVector3D(0, 0, 0);
        public static int maxObservedPlaneSize = 0;
        public static object lockingObj = new object();
        
        
    }
}
