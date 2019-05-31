using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ChessTracking.MultithreadingMessages;

namespace ChessTracking.ImageProcessing.PipelineData
{
    class TrackingResultData
    {
        public TrackingState TrackingState { get; set; }
        public bool HandDetected { get; set; }
        public Bitmap VisualisationBitmap { get; set; }
    }
}
