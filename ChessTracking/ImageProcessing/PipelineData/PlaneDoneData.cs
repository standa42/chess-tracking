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
        public RawData RawData { get; set; }
        public TrackingResultData ResultData { get; set; }


        public VisualisationType VisualisationType { get; set; }
        
        public Emgu.CV.Image<Rgb, byte> MaskedColorImageOfTable { get; set; }
        public byte[] CannyDepthData { get; set; }
        public Bitmap ColorBitmap { get; set; }
        public bool[] MaskOfTable { get; set; }

        public PlaneDoneData(RawData rawData)
        {
            this.RawData = rawData;
            this.ResultData = new TrackingResultData();
        }

    }
}
