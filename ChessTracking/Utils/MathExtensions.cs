using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChessTracking.Utils
{
    internal static class MathExtensions
    {
        /// <summary>
        /// Conversion from radians to degrees
        /// </summary>
        /// <param name="radians">value in radians</param>
        /// <returns>value in degrees</returns>
        public static double ConvertRadiansToDegrees(this double radians)
        {
            double degrees = (180 / Math.PI) * radians;
            return (degrees);
        }

        /// <summary>
        /// Mathematical modulo operation - for all numbers, including negative, returns number 0..m-1
        /// </summary>
        /// <param name="x">input number</param>
        /// <param name="m">modulo coeficient</param>
        /// <returns>moduled result</returns>
        public static int MathMod(this int x, int m)
        {
            return (x % m + m) % m;
        }
    }
}
