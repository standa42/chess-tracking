using ChessTracking.MultithreadingMessages;

namespace ChessTracking.ImageProcessing.PipelineData
{
    /// <summary>
    /// Parameters of algorithms changeable by user through user interface
    /// </summary>
    class UserDefinedParameters
    {
        public VisualisationType VisualisationType { get; set; } = VisualisationType.RawRGB;
        public double ColorCalibrationAdditiveConstant { get; set; } = 0;
        public int MilimetersClippedFromFigure { get; set; } = 10;
        public int NumberOfPointsIndicatingFigure { get; set; } = 5;
        public int MinimalTimeBetweenTrackingTasksInMiliseconds { get; set; } = 220;
        public bool OtzuActiveInBinarization { get; set; } = true;
        public int BinarizationThreshold { get; set; } = 120;

        public UserDefinedParameters GetShallowCopy()
        {
            return (UserDefinedParameters)MemberwiseClone();
        }
    }
}
