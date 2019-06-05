using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChessTracking.MultithreadingMessages.ToProcessing
{
    class ChessboardMovementMessage : Message
    {
        public ChessboardMovement Movement { get; }

        public ChessboardMovementMessage(ChessboardMovement movement)
        {
            Movement = movement;
        }
    }
}
