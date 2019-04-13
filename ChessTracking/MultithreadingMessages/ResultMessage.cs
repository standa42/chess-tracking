using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChessTracking.MultithreadingMessages
{
    class ResultMessage : Message
    {
        public Bitmap BitmapToDisplay { get; }
        public Bitmap FiguresBitmap { get; }

        public ResultMessage(Bitmap bitmapToDisplay, Bitmap figuresBitmap)
        {
            this.BitmapToDisplay = bitmapToDisplay;
            FiguresBitmap = figuresBitmap;
        }
    }
}
