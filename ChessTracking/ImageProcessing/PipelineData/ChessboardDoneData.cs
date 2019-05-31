using System.Drawing;
using ChessTracking.ImageProcessing.PlaneAlgorithms;
using ChessTracking.MultithreadingMessages;
using Emgu.CV.Structure;
using Microsoft.Kinect;

namespace ChessTracking.ImageProcessing.PipelineData
{
    /// <summary>
    /// Output information of chessboard localization procedure
    /// </summary>
    class ChessboardDoneData
    {
        public KinectData KinectData { get; set; }
        public TrackingResultData ResultData { get; set; }
        public PlaneTrackingData PlaneData { get; set; }
        public ChessboardTrackingData ChessboardData { get; set; }

        public VisualisationType VisualisationType { get; set; }
        
        public ChessboardDoneData(PlaneDoneData planeData)
        {
            this.KinectData = planeData.KinectData;
            this.ResultData = planeData.ResultData;
            this.PlaneData = planeData.PlaneData;
            this.ChessboardData = new ChessboardTrackingData();

            this.VisualisationType = planeData.VisualisationType;
        }
    }
}
