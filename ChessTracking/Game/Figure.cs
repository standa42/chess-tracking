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
            WhiteQueenBitmap = new Bitmap($@"img\WhiteQueen.png");
            WhiteKingBitmap = new Bitmap($@"img\WhiteKing.png");
            WhiteBishopBitmap = new Bitmap($@"img\WhiteBishop.png");
            WhiteKnightBitmap = new Bitmap($@"img\WhiteKnight.png");
            WhiteRookBitmap = new Bitmap($@"img\WhiteRook.png");
            WhitePawnBitmap = new Bitmap($@"img\WhitePawn.png");

            BlackQueenBitmap = new Bitmap($@"img\BlackQueen.png");
            BlackKingBitmap = new Bitmap($@"img\BlackKing.png");
            BlackBishopBitmap = new Bitmap($@"img\BlackBishop.png");
            BlackKnightBitmap = new Bitmap($@"img\BlackKnight.png");
            BlackRookBitmap = new Bitmap($@"img\BlackRook.png");
            BlackPawnBitmap = new Bitmap($@"img\BlackPawn.png");
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
