using System.Drawing;
using ChessTracking.MultithreadingMessages;

namespace ChessTracking.ImageProcessing.PipelineData
{
    /// <summary>
    /// Results of tracking
    /// </summary>
    class TrackingResultData
    {
        public TrackingState TrackingState { get; set; }
        public bool SceneDisrupted { get; set; }
        public Bitmap VisualisationBitmap { get; set; }
    }
}
