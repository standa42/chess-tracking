using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChessTracking.Game.Figures
{
    class Dama : Figure
    {
        public Dama(Position position, bool isWhite) : base(position, isWhite)
        {
            ImageBitmap = isWhite ? new Bitmap("biladama.png") : new Bitmap("cernadama.png");
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
