using ChessTracking.MultithreadingMessages;

namespace ChessTracking.ImageProcessing.PipelineData
{
    /// <summary>
    /// Data arriving into pipeline
    /// </summary>
    class InputData
    {
        public KinectData KinectData { get; set; }
        public TrackingResultData ResultData { get; set; }
        public UserDefinedParameters UserParameters { get; set; }
        public TrackingState TrackingStateOfGame { get; set; }

        public InputData(KinectData kinectData, UserDefinedParameters userParameters, TrackingState trackingStateOfGame = null)
        {
            KinectData = kinectData;
            UserParameters = userParameters;
            ResultData = new TrackingResultData();
            TrackingStateOfGame = trackingStateOfGame;
        }
    }
}
