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
        public Bitmap ColorBitmap { get; set; }
        public bool[] MaskOfTable { get; set; }

        public PlaneDoneData(RawData rawData)
        {
            this.ColorFrameData = rawData.ColorFrameData;
            this.DepthData = rawData.DepthData;
            this.InfraredData = rawData.InfraredData;
            this.CameraSpacePointsFromDepthData = rawData.CameraSpacePointsFromDepthData;
            this.PointsFromColorToDepth = rawData.PointsFromColorToDepth;
            this.PointsFromDepthToColor = rawData.PointsFromDepthToColor;
        }

    }
}
