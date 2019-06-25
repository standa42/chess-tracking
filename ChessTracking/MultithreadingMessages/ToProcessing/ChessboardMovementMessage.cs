using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChessTracking.MultithreadingMessages.ToProcessing
{
    /// <summary>
    /// Carries information about which way should chessboard location move by one field
    /// </summary>
    class ChessboardMovementMessage : Message
    {
        public ChessboardMovement Movement { get; }

        public ChessboardMovementMessage(ChessboardMovement movement)
        {
            Movement = movement;
        }
    }
}
