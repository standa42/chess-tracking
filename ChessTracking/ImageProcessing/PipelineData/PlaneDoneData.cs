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
        public UserDefinedParameters UserParameters { get; set; }
        
        public PlaneDoneData(InputData inputData)
        {
            this.KinectData = inputData.KinectData;
            this.UserParameters = inputData.UserParameters;
            this.ResultData = new TrackingResultData();
            this.PlaneData = new PlaneTrackingData();
        }
    }
}
