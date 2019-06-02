namespace ChessTracking.ImageProcessing.PipelineData
{
    /// <summary>
    /// Output information of figures localization procedure
    /// </summary>
    class FiguresTrackingCompleteData
    {
        public KinectData KinectData { get; set; }
        public TrackingResultData ResultData { get; set; }
        public PlaneTrackingData PlaneData { get; set; }
        public ChessboardTrackingData ChessboardData { get; set; }
        public UserDefinedParameters UserParameters { get; set; }
        
        public FiguresTrackingCompleteData(ChessboardTrackingCompleteData chessboardData)
        {
            KinectData = chessboardData.KinectData;
            ResultData = chessboardData.ResultData;
            PlaneData = chessboardData.PlaneData;
            ChessboardData = chessboardData.ChessboardData;
            UserParameters = chessboardData.UserParameters;
        }
    }
}
