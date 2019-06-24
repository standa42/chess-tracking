using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ChessTracking.Utils;

namespace ChessTracking.ImageProcessing.PlaneAlgorithms
{
    static class TrianglesGenerator
    {
        /// <summary>
        /// Generates uniformly distributed right triangles into image bounds
        /// </summary>
        public static List<RansacSeedTriangle> GenerateTriangleSeedsInImage()
        {
            var triangleSeedsForRansac = new List<RansacSeedTriangle>();

            int offsetFromImageBorders = 102;

            // intervals to move in
            int zacX = 0 + offsetFromImageBorders;
            int konX = PlaneLocalizationConfig.DepthImageWidth - offsetFromImageBorders;
            int zacY = 0 + offsetFromImageBorders;
            int konY = PlaneLocalizationConfig.DepthImageHeight - offsetFromImageBorders;

            // generation steps
            int positionStep = 15;
            int angleStep = 43;

            int currentRotation = 0;

            // move in discrete steps over image
            // and generate triangles with various sizes and rotations
            for (int y = zacY; y < konY; y += positionStep)
            {
                for (int x = zacX; x < konX; x += positionStep)
                {
                    triangleSeedsForRansac.Add(GenerateTriangle(x, y, 30, currentRotation));
                    triangleSeedsForRansac.Add(GenerateTriangle(x, y, 60, currentRotation));
                    triangleSeedsForRansac.Add(GenerateTriangle(x, y, 98, currentRotation));

                    // reset rotation if overflows
                    currentRotation += angleStep;
                    if (currentRotation >= 360) currentRotation -= 360;
                }
            }

            return triangleSeedsForRansac;
        }

        /// <summary>
        /// Generates right triangle in x,y position with given size and rotation
        /// </summary>
        /// <returns>Generated triangle</returns>
        private static RansacSeedTriangle GenerateTriangle(int x, int y, int size, int rotation)
        {
            var center = new Position2D(x, y);
            var up = new Position2D(x, y + size);
            var right = new Position2D(x + size, (int)(y));

            up.RotateAroundPoint(rotation, center);
            right.RotateAroundPoint(rotation, center);

            return new RansacSeedTriangle(
                center.X + center.Y * PlaneLocalizationConfig.DepthImageWidth,
                up.X + up.Y * PlaneLocalizationConfig.DepthImageWidth,
                right.X + right.Y * PlaneLocalizationConfig.DepthImageWidth
                );
        }
    }
}
