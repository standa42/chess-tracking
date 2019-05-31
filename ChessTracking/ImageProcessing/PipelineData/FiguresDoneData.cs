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
        public RawData RawData { get; set; }
        public TrackingResultData ResultData { get; set; }

        public VisualisationType VisualisationType { get; set; }

        public Emgu.CV.Image<Rgb, byte> MaskedColorImageOfTable { get; set; }
        public byte[] CannyDepthData { get; set; }
        public MyVector3DStruct FirstVectorFinal;
        public Bitmap ColorBitmap { get; set; }
        public bool[] MaskOfTable { get; set; }

        public FiguresDoneData(ChessboardDoneData chessboardData)
        {
            this.RawData = chessboardData.RawData;
            this.ResultData = chessboardData.ResultData;

            this.VisualisationType = chessboardData.VisualisationType;
            this.MaskedColorImageOfTable = chessboardData.MaskedColorImageOfTable;
            this.CannyDepthData = chessboardData.CannyDepthData;
            this.FirstVectorFinal = chessboardData.FirstVectorFinal;
            this.MaskOfTable = chessboardData.MaskOfTable;
            this.ColorBitmap = chessboardData.ColorBitmap;
        }
    }
}
