using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChessTracking.MultithreadingMessages.FromProcessing
{
    class TrackingError : Message
    {
        public string Message { get; }

        public TrackingError(string message)
        {
            Message = message;
        }
    }
}
