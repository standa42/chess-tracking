using ChessTracking.ImageProcessing.PipelineData;

namespace ChessTracking.MultithreadingMessages
{
    /// <summary>
    /// Message with data resources from sensor
    /// </summary>
    class KinectResourcesMessage : Message
    {
        public RawData Data;

        public KinectResourcesMessage(RawData rawData)
        {
            Data = rawData;
        }
    }
}
