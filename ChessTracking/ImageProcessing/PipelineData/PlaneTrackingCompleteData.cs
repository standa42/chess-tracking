namespace ChessTracking.ImageProcessing.PipelineData
{
    /// <summary>
    /// Output information of plane localization procedure
    /// </summary>
    class PlaneTrackingCompleteData
    {
        public KinectData KinectData { get; set; }
        public TrackingResultData ResultData { get; set; }
        public PlaneTrackingData PlaneData { get; set; }
        public UserDefinedParameters UserParameters { get; set; }
        
        public PlaneTrackingCompleteData(InputData inputData)
        {
            KinectData = inputData.KinectData;
            UserParameters = inputData.UserParameters;
            ResultData = new TrackingResultData();
            PlaneData = new PlaneTrackingData();
        }
    }
}
