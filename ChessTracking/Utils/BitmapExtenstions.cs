using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChessTracking.Utils
{
    static class BitmapExtenstions
    {
        /// <summary>
        /// Serializes image to byte array
        /// </summary>
        public static byte[] ImageToByteArray(this Image image)
        {
            using (var ms = new MemoryStream())
            {
                image.Save(ms, ImageFormat.Bmp);
                return ms.ToArray();
            }
        }

        /// <summary>
        /// Horizontally flips image
        /// </summary>
        /// <param name="bitmap"></param>
        /// <returns></returns>
        public static Bitmap HorizontalFlip(this Bitmap bitmap)
        {
            bitmap.RotateFlip(RotateFlipType.RotateNoneFlipX);
            return bitmap;
        }

        /// <summary>
        /// Custom computation of brightness from rgb
        /// </summary>
        public static double CustomBrightness(this Color color)
        {
            return Math.Sqrt(
                color.R * color.R * .241 +
                color.G * color.G * .691 +
                color.B * color.B * .068) / 255f;
        }
    }
}
