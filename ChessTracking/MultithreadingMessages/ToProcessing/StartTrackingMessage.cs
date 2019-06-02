using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ChessTracking.ImageProcessing.PipelineData;

namespace ChessTracking.MultithreadingMessages
{
    /// <summary>
    /// Command for pipeline to start tracking
    /// </summary>
    class StartTrackingMessage : Message
    {
        public KinectDataBuffer Buffer { get; set; }

        public StartTrackingMessage(KinectDataBuffer buffer)
        {
            Buffer = buffer;
        }
    }
}

