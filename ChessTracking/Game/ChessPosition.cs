using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChessTracking.Game
{
    /// <summary>
    /// Describes position on chessboard
    /// </summary>
    [Serializable]
    public class ChessPosition
    {
        /// <summary>
        /// Column coordinate
        /// </summary>
        public int X { get; set; }

        /// <summary>
        /// Row coordinate
        /// </summary>
        public int Y { get; set; }

        public ChessPosition(int x, int y)
        {
            this.X = x;
            this.Y = y;
        }
        
        /// <summary>
        /// Checks is current position is valid position on chessboard
        /// </summary>
        /// <returns>Validity of position coordinates</returns>
        public bool IsValid()
        {
            return X >= 0 &&
                   Y >= 0 &&
                   X <= 7 && 
                   Y <= 7;
        }

        /// <summary>
        /// Checks is position is valid position on chessboard
        /// </summary>
        /// <returns>Validity of position coordinates</returns>
        public static bool IsValid(int x, int y)
        {
            return x >= 0 &&
                   y >= 0 &&
                   x <= 7 &&
                   y <= 7;
        }

        /// <summary>
        /// Checks, whether two positions correspond to the same square on chessboard
        /// </summary>
        /// <param name="other"></param>
        /// <returns></returns>
        public bool IsEquivalent(ChessPosition other)
        {
            return ChessPosition.IsEquivalent(this, other);
        }

        /// <summary>
        /// Checks, whether two positions correspond to the same square on chessboard
        /// </summary>
        /// <param name="other"></param>
        /// <returns></returns>
        public static bool IsEquivalent(ChessPosition lhs, ChessPosition rhs)
        {
            return lhs.X == rhs.X && lhs.Y == rhs.Y;
        }

        /// <summary>
        /// Serializes position to text representation
        /// </summary>
        /// <returns>Text reprezentation</returns>
        public string ToChessString()
        {
            char letterCoordinate = (char) (X - (int)'a');
            return $"{letterCoordinate}{Y}";
        }

        /// <summary>
        /// Get position after translating by given vector
        /// </summary>
        /// <param name="vector">Vector move translation</param>
        /// <returns>Transalted position</returns>
        public ChessPosition Add(GameMoveVector vector)
        {
            return new ChessPosition(this.X + vector.X, this.Y + vector.Y);
        }
    }
}
