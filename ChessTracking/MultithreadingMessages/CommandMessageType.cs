using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChessTracking.MultithreadingMessages
{
    /// <summary>
    /// Types of commands for tracking thread
    /// </summary>
    enum CommandMessageType
    {
        StartTracking,
        StopTracking,
        Recalibrate
    }
}
