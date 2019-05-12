using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChessTracking.MultithreadingMessages
{
    /// <summary>
    /// Message containing commands from tracking thread
    /// </summary>
    class CommandMessage : Message
    {
        public CommandMessageType MessageType { get; set; }

        public CommandMessage()
        {

        }

        public CommandMessage(CommandMessageType messageType)
        {
            MessageType = messageType;
        }
    }
}
