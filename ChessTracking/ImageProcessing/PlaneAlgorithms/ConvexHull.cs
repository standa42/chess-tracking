/* 
 * Convex hull algorithm - Library (C#)
 * 
 * Copyright (c) 2017 Project Nayuki
 * https://www.nayuki.io/page/convex-hull-algorithm
 * 
 * This program is free software: you can redistribute it and/or modify
 * it under the terms of the GNU Lesser General Public License as published by
 * the Free Software Foundation, either version 3 of the License, or
 * (at your option) any later version.
 * 
 * This program is distributed in the hope that it will be useful,
 * but WITHOUT ANY WARRANTY; without even the implied warranty of
 * MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
 * GNU Lesser General Public License for more details.
 * 
 * You should have received a copy of the GNU Lesser General Public License
 * along with this program (see COPYING.txt and COPYING.LESSER.txt).
 * If not, see <http://www.gnu.org/licenses/>.
 */


using System.Collections.Generic;
using System.Linq;

namespace ChessTracking.ImageProcessing.PlaneAlgorithms
{

    public class ConvexHull
    {

        // Returns a new list of points representing the convex hull of
        // the given set of points. The convex hull excludes collinear points.
        // This algorithm runs in O(n log n) time.
        public static IList<ConvexHullPoints> MakeHull(IList<ConvexHullPoints> points)
        {
            List<ConvexHullPoints> newPoints = new List<ConvexHullPoints>(points);
            newPoints.Sort();
            return MakeHullPresorted(newPoints);
        }


        // Returns the convex hull, assuming that each points[i] <= points[i + 1]. Runs in O(n) time.
        public static IList<ConvexHullPoints> MakeHullPresorted(IList<ConvexHullPoints> points)
        {
            if (points.Count <= 1)
                return new List<ConvexHullPoints>(points);

            // Andrew's monotone chain algorithm. Positive y coordinates correspond to "up"
            // as per the mathematical convention, instead of "down" as per the computer
            // graphics convention. This doesn't affect the correctness of the result.

            List<ConvexHullPoints> upperHull = new List<ConvexHullPoints>();
            foreach (ConvexHullPoints p in points)
            {
                while (upperHull.Count >= 2)
                {
                    ConvexHullPoints q = upperHull[upperHull.Count - 1];
                    ConvexHullPoints r = upperHull[upperHull.Count - 2];
                    if ((q.X - r.X) * (p.Y - r.Y) >= (q.Y - r.Y) * (p.X - r.X))
                        upperHull.RemoveAt(upperHull.Count - 1);
                    else
                        break;
                }
                upperHull.Add(p);
            }
            upperHull.RemoveAt(upperHull.Count - 1);

            IList<ConvexHullPoints> lowerHull = new List<ConvexHullPoints>();
            for (int i = points.Count - 1; i >= 0; i--)
            {
                ConvexHullPoints p = points[i];
                while (lowerHull.Count >= 2)
                {
                    ConvexHullPoints q = lowerHull[lowerHull.Count - 1];
                    ConvexHullPoints r = lowerHull[lowerHull.Count - 2];
                    if ((q.X - r.X) * (p.Y - r.Y) >= (q.Y - r.Y) * (p.X - r.X))
                        lowerHull.RemoveAt(lowerHull.Count - 1);
                    else
                        break;
                }
                lowerHull.Add(p);
            }
            lowerHull.RemoveAt(lowerHull.Count - 1);

            if (!(upperHull.Count == 1 && Enumerable.SequenceEqual(upperHull, lowerHull)))
                upperHull.AddRange(lowerHull);
            return upperHull;
        }

    }

}