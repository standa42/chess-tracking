using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChessTracking.Game
{
    public class GameMoveVector
    {
        public int X { get; set; }
        public int Y { get; set; }

        public GameMoveVector(int x, int y)
        {
            this.X = x;
            this.Y = y;
        }

    }
}
