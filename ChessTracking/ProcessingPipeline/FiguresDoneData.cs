using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ChessTracking.MultithreadingMessages;
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
        }
    }
}
