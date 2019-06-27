using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChessTracking.MultithreadingMessages
{
    /// <summary>
    /// Message indication update in kinect data and transfering game state to processing
    /// </summary>
    class KinectUpdateMessage : Message
    {
        public TrackingState TrackingStateOfGame { get; set; }

        public KinectUpdateMessage(TrackingState trackingStateOfGame)
        {
            TrackingStateOfGame = trackingStateOfGame;
        }
    }
}
