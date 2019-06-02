using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChessTracking.MultithreadingMessages.FromProcessing
{
    /// <summary>
    /// Message notifying about error in tracking
    /// </summary>
    class TrackingError : Message
    {
        public string Message { get; }

        public TrackingError(string message)
        {
            Message = message;
        }
    }
}
