using System.Drawing;
using ChessTracking.MultithreadingMessages;
using Emgu.CV.Structure;
using Microsoft.Kinect;

namespace ChessTracking.ImageProcessing.PipelineData
{
    /// <summary>
    /// Output information of plane localization procedure
    /// </summary>
    class PlaneDoneData
    {
        public KinectData KinectData { get; set; }
        public TrackingResultData ResultData { get; set; }
        public PlaneTrackingData PlaneData { get; set; }

        public VisualisationType VisualisationType { get; set; }
        
        public PlaneDoneData(KinectData kinectData)
        {
            this.KinectData = kinectData;
            this.ResultData = new TrackingResultData();
            this.PlaneData = new PlaneTrackingData();
        }

    }
}
