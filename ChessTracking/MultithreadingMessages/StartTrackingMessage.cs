using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ChessTracking.ImageProcessing.PipelineData;

namespace ChessTracking.MultithreadingMessages
{
    class StartTrackingMessage : Message
    {
        public KinectDataBuffer Buffer { get; set; }

        public StartTrackingMessage(KinectDataBuffer buffer)
        {
            Buffer = buffer;
        }
    }
}

