using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ChessTracking.MultithreadingMessages;
using Emgu.CV.Structure;
using Kinect_v0._1;
using Microsoft.Kinect;

namespace ChessTracking.ProcessingPipeline
{
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

        public Bitmap FiguresBitmap { get; set; }

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
        }
    }
}
