using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChessTracking.Game
{
    public class ChessPosition
    {
        public int X { get; set; }
        public int Y { get; set; }

        public ChessPosition(int x, int y)
        {
            this.X = x;
            this.Y = y;
        }
        
        public bool IsValid()
        {
            return X >= 0 &&
                   Y >= 0 &&
                   X <= 7 && 
                   Y <= 7;
        }

        public static bool IsValid(int x, int y)
        {
            return x >= 0 &&
                   y >= 0 &&
                   x <= 7 &&
                   y <= 7;
        }
    }
}
