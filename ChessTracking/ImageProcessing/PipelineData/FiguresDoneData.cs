using System.Drawing;
using ChessTracking.ImageProcessing.PlaneAlgorithms;
using ChessTracking.MultithreadingMessages;
using Emgu.CV.Structure;
using Microsoft.Kinect;
using TrackingState = ChessTracking.MultithreadingMessages.TrackingState;

namespace ChessTracking.ImageProcessing.PipelineData
{
    /// <summary>
    /// Output information of figures localization procedure
    /// </summary>
    class FiguresDoneData
    {
        public KinectData KinectData { get; set; }
        public TrackingResultData ResultData { get; set; }
        public PlaneTrackingData PlaneData { get; set; }
        public ChessboardTrackingData ChessboardData { get; set; }

        public VisualisationType VisualisationType { get; set; }
        
        public FiguresDoneData(ChessboardDoneData chessboardData)
        {
            this.KinectData = chessboardData.KinectData;
            this.ResultData = chessboardData.ResultData;
            this.PlaneData = chessboardData.PlaneData;
            this.ChessboardData = chessboardData.ChessboardData;

            this.VisualisationType = chessboardData.VisualisationType;
        }
    }
}
