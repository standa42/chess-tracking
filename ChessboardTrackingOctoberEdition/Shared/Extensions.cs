using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Emgu.CV;

namespace ChessboardTrackingOctoberEdition.Shared
{
    public static class Extensions
    {
        public static Image<TColor, TDepth> ToOpenCVImage<TColor, TDepth>(this Bitmap bitmap)
            where TColor : struct, IColor
            where TDepth : new()
        {
            return new Image<TColor, TDepth>(bitmap);
        }
    }
}
