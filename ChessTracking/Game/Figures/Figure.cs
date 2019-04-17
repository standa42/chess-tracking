using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChessTracking.Game.Figures
{
    abstract class Figure
    {
        public Position Position { get; set; }
        public bool IsWhite { get; set; }
        public Bitmap ImageBitmap { get; set; }

        public Figure(Position position, bool isWhite)
        {
            this.Position = position;
            this.IsWhite = isWhite;
        }

        public abstract bool IsMoveValid(Position newPosition, Figure[,] board);
        public abstract Bitmap DrawBitmap();
    }
}
