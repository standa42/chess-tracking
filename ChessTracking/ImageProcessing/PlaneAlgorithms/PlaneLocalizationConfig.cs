using System;

namespace ChessTracking.ImageProcessing.PlaneAlgorithms
{
    /// <summary>
    /// Class containing configuration for plane localization objects and algorithms
    /// </summary>
    public static class PlaneLocalizationConfig
    {
        // Depth image dimensions
        public static readonly int DepthImageWidth = 512;
        public static readonly int DepthImageHeight = 424;

        // Number of paralel parts (multithreading in algorithms)
        public static int ParalelTasksCount = Environment.ProcessorCount * 2;
        
        // Optimalizations
        public static int RegressionIsEvaluatedOnEveryNthTablePixel = 1;
        public static int GridSizeForTestingPlaneFit = 10; // 10
        
        // Interval from camera that is evaluated
        public static float MinDepth = 0.5f;
        public static float MaxDepth = 2.2f;

        public static float RegreseTloustka = 0.007f;

        // RANSAC constants
        
        public static float RansacThickness = 0.008f;
        public static double FinalD = 0;
        public static MyVector3D FinalNormal = new MyVector3D(0, 0, 0);
        public static int MaxObservedPlaneSize = 0;
        public static object LockingObj = new object();
        
    }
}
