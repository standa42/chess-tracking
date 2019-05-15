using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChessTracking.Game
{
    [Serializable]
    public class Figure
    {
        public FigureType Type { get; set; }
        public PlayerColor Color { get; set; }
        public bool Moved;

        public Figure(FigureType type, PlayerColor color, bool moved = false)
        {
            Type = type;
            Color = color;
            Moved = moved;
        }

        #region Static images of figures

        private static Bitmap WhiteQueenBitmap { get; }
        private static Bitmap WhiteKingBitmap { get; }
        private static Bitmap WhiteBishopBitmap { get; }
        private static Bitmap WhiteKnightBitmap { get; }
        private static Bitmap WhiteRookBitmap { get; }
        private static Bitmap WhitePawnBitmap { get; }

        private static Bitmap BlackQueenBitmap { get; }
        private static Bitmap BlackKingBitmap { get; }
        private static Bitmap BlackBishopBitmap { get; }
        private static Bitmap BlackKnightBitmap { get; }
        private static Bitmap BlackRookBitmap { get; }
        private static Bitmap BlackPawnBitmap { get; }

        #endregion

        static Figure()
        {
            WhiteQueenBitmap = Properties.Resources.WhiteQueen; 
            WhiteKingBitmap = Properties.Resources.WhiteKing;
            WhiteBishopBitmap = Properties.Resources.WhiteBishop;
            WhiteKnightBitmap = Properties.Resources.WhiteKnight;
            WhiteRookBitmap = Properties.Resources.WhiteRook;
            WhitePawnBitmap = Properties.Resources.WhitePawn;

            BlackQueenBitmap = Properties.Resources.BlackQueen;
            BlackKingBitmap = Properties.Resources.BlackKing;
            BlackBishopBitmap = Properties.Resources.BlackBishop;
            BlackKnightBitmap = Properties.Resources.BlackKnight;
            BlackRookBitmap = Properties.Resources.BlackRook;
            BlackPawnBitmap = Properties.Resources.BlackPawn;
        }

        public static Bitmap GetBitmapRepresentation(FigureType type, PlayerColor color)
        {
            switch (type)
            {
                case FigureType.Queen:
                    return color == PlayerColor.White ? WhiteQueenBitmap : BlackQueenBitmap;
                case FigureType.King:
                    return color == PlayerColor.White ? WhiteKingBitmap : BlackKingBitmap;
                case FigureType.Rook:
                    return color == PlayerColor.White ? WhiteRookBitmap : BlackRookBitmap;
                case FigureType.Knight:
                    return color == PlayerColor.White ? WhiteKnightBitmap : BlackKnightBitmap;
                case FigureType.Bishop:
                    return color == PlayerColor.White ? WhiteBishopBitmap : BlackBishopBitmap;
                case FigureType.Pawn:
                    return color == PlayerColor.White ? WhitePawnBitmap : BlackPawnBitmap;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        public bool Fullfils(PlayerColor color)
        {
            return Color == color;
        }

        public bool Fullfils(FigureType type)
        {
            return Type == type;
        }

        public bool Fullfils(PlayerColor color, FigureType type)
        {
            return Fullfils(color) && Fullfils(type);
        }
    }
}
