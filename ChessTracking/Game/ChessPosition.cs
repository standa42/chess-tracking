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

        public bool IsEquivalent(ChessPosition other)
        {
            return ChessPosition.IsEquivalent(this, other);
        }

        public static bool IsEquivalent(ChessPosition lhs, ChessPosition rhs)
        {
            return lhs.X == rhs.X && lhs.Y == rhs.Y;
        }

        public string ToChessString()
        {
            char letterCoordinate = (char) (X - (int)'a');
            return $"{letterCoordinate}{Y}";
        }
    }
}
