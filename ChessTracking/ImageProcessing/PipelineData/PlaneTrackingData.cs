using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Emgu.CV.Structure;

namespace ChessTracking.ImageProcessing.PipelineData
{
    class PlaneTrackingData
    {
        public Emgu.CV.Image<Rgb, byte> MaskedColorImageOfTable { get; set; }
        public byte[] CannyDepthData { get; set; }
        public Bitmap ColorBitmap { get; set; }
        public bool[] MaskOfTable { get; set; }
    }
}
