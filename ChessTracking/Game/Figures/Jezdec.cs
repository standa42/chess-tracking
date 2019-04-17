using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChessTracking.Game.Figures
{
    class Jezdec : Figure
    {
        public Jezdec(Position position, bool isWhite) : base(position, isWhite)
        {
            ImageBitmap = isWhite ? new Bitmap("bilakun.png") : new Bitmap("cernakun.png");
        }

        public override bool IsMoveValid(Position newPosition, Figure[,] board)
        {
            throw new NotImplementedException();
        }

        public override Bitmap DrawBitmap()
        {
            throw new NotImplementedException();
        }
    }
}
