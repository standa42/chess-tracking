using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChessTracking.ImageProcessing.PipelineData
{
    struct Point2DWithColor
    {
        public Color Color { get; }
        public int X { get; }
        public int Y { get; }

        public Point2DWithColor(Color color, int x, int y)
        {
            Color = color;
            X = x;
            Y = y;
        }
    }
}
