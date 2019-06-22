namespace ChessTracking.ImageProcessing.PipelineData
{
    /// <summary>
    /// Output information of chessboard localization procedure
    /// </summary>
    class ChessboardTrackingCompleteData
    {
        public KinectData KinectData { get; set; }
        public TrackingResultData ResultData { get; set; }
        public PlaneTrackingData PlaneData { get; set; }
        public ChessboardTrackingData ChessboardData { get; set; }
        public UserDefinedParameters UserParameters { get; set; }
        
        public ChessboardTrackingCompleteData(PlaneTrackingCompleteData planeData)
        {
            KinectData = planeData.KinectData;
            ResultData = planeData.ResultData;
            PlaneData = planeData.PlaneData;
            UserParameters = planeData.UserParameters;
            ChessboardData = new ChessboardTrackingData();
        }
    }
}
