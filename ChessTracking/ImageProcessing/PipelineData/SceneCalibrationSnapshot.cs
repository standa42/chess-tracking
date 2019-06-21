using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChessTracking.ImageProcessing.PipelineData
{
    public class SceneCalibrationSnapshot
    {
        public Bitmap MaskedColorImage { get; set; }
        public Bitmap GrayImage { get; set; }
        public Bitmap BinarizationImage { get; set; }
        public Bitmap CannyImage { get; set; }
    }
}
