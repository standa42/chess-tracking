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
    class ChessboardDoneData
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
        public bool[] MaskOfTable { get; set; }
        public Bitmap ColorBitmap { get; set; }
        public MyVector3DStruct FirstVectorFinal { get; set; }

        public ChessboardDoneData(PlaneDoneData planeData)
        {
            this.ColorFrameData = planeData.ColorFrameData;
            this.DepthData = planeData.DepthData;
            this.InfraredData = planeData.InfraredData;
            this.CameraSpacePointsFromDepthData = planeData.CameraSpacePointsFromDepthData;
            this.PointsFromColorToDepth = planeData.PointsFromColorToDepth;
            this.PointsFromDepthToColor = planeData.PointsFromDepthToColor;

            this.VisualisationType = planeData.VisualisationType;
            this.Bitmap = planeData.Bitmap;
            this.MaskedColorImageOfTable = planeData.MaskedColorImageOfTable;
            this.CannyDepthData = planeData.CannyDepthData;
            this.MaskOfTable = planeData.MaskOfTable;
            this.ColorBitmap = planeData.ColorBitmap;
        }
    }
}
