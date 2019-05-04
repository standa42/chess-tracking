using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ChessTracking.Game.Figures;
using ChessTracking.MultithreadingMessages;

namespace ChessTracking.Game
{
    public class GameData
    {
        public ChessboardModel Chessboard;
        public bool IsWhitePlaying { get; set; }

        public GameData(ChessboardModel chessboard, bool isWhitePlaying = true)
        {
            this.Chessboard = chessboard;
            this.IsWhitePlaying = isWhitePlaying;
        }

    }
}
