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
        public RawData RawData { get; set; }
        public TrackingResultData ResultData { get; set; }

        public VisualisationType VisualisationType { get; set; }

        public Emgu.CV.Image<Rgb, byte> MaskedColorImageOfTable { get; set; }
        public byte[] CannyDepthData { get; set; }
        public bool[] MaskOfTable { get; set; }
        public Bitmap ColorBitmap { get; set; }
        public MyVector3DStruct FirstVectorFinal { get; set; }

        public ChessboardDoneData(PlaneDoneData planeData)
        {
            this.RawData = planeData.RawData;
            this.ResultData = planeData.ResultData;

            this.VisualisationType = planeData.VisualisationType;
            this.MaskedColorImageOfTable = planeData.MaskedColorImageOfTable;
            this.CannyDepthData = planeData.CannyDepthData;
            this.MaskOfTable = planeData.MaskOfTable;
            this.ColorBitmap = planeData.ColorBitmap;
        }
    }
}
