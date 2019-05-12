using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChessTracking.MultithreadingMessages
{
    /// <summary>
    /// Message containing result of Kinect tracking
    /// </summary>
    class ResultMessage : Message
    {
        public Bitmap BitmapToDisplay { get; }
        public TrackingState TrackingState { get; }
        public string HandDetected { get; }

        public ResultMessage(Bitmap bitmapToDisplay, TrackingState trackingState, string handDetected)
        {
            this.BitmapToDisplay = bitmapToDisplay;
            TrackingState = trackingState;
            this.HandDetected = handDetected;
        }
    }
}
