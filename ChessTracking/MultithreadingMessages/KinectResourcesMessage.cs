using ChessTracking.ImageProcessing.PipelineData;

namespace ChessTracking.MultithreadingMessages
{
    /// <summary>
    /// Message with data resources from sensor
    /// </summary>
    class KinectResourcesMessage : Message
    {
        public KinectData Data;

        public KinectResourcesMessage(KinectData kinectData)
        {
            Data = kinectData;
        }
    }
}
