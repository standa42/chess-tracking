using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ChessTracking.MultithreadingMessages;
using ChessTracking.ProcessingPipeline.Plane;
using Emgu.CV.Structure;
using Microsoft.Kinect;
using TrackingState = ChessTracking.MultithreadingMessages.TrackingState;

namespace ChessTracking.ProcessingPipeline
{
    /// <summary>
    /// Output information of figures localization procedure
    /// </summary>
    class FiguresDoneData
    {
        public byte[] ColorFrameData { get; set; }
        public ushort[] DepthData { get; set; }
        public ushort[] InfraredData { get; set; }
        public CameraSpacePoint[] CameraSpacePointsFromDepthData { get; set; }
        public DepthSpacePoint[] PointsFromColorToDepth { get; set; }
        public ColorSpacePoint[] PointsFromDepthToColor { get; set; }

        public VisualisationType VisualisationType { get; set; }
        public Bitmap Bitmap { get; set; }

        public Emgu.CV.Image<Rgb, byte> MaskedColorImageOfTable { get; set; }
        public byte[] CannyDepthData { get; set; }
        public MyVector3DStruct FirstVectorFinal;
        public Bitmap ColorBitmap { get; set; }
        public bool[] MaskOfTable { get; set; }
        public string HandDetected { get; set; }

        public TrackingState TrackingState { get; set; }

        public FiguresDoneData(ChessboardDoneData chessboardData)
        {
            this.ColorFrameData = chessboardData.ColorFrameData;
            this.DepthData = chessboardData.DepthData;
            this.InfraredData = chessboardData.InfraredData;
            this.CameraSpacePointsFromDepthData = chessboardData.CameraSpacePointsFromDepthData;
            this.PointsFromColorToDepth = chessboardData.PointsFromColorToDepth;
            this.PointsFromDepthToColor = chessboardData.PointsFromDepthToColor;

            this.VisualisationType = chessboardData.VisualisationType;
            this.Bitmap = chessboardData.Bitmap;
            this.MaskedColorImageOfTable = chessboardData.MaskedColorImageOfTable;
            this.CannyDepthData = chessboardData.CannyDepthData;
            this.FirstVectorFinal = chessboardData.FirstVectorFinal;
            this.MaskOfTable = chessboardData.MaskOfTable;
            this.ColorBitmap = chessboardData.ColorBitmap;
        }
    }
}
