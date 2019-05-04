using System;
using System.Collections.Generic;
using System.Linq;

namespace ChessTracking.ProcessingPipeline.Plane
{

    public class ConvexHull
    {

        // Returns a new list of points representing the convex hull of
        // the given set of points. The convex hull excludes collinear points.
        // This algorithm runs in O(n log n) time.
        public static IList<ConvexAlgorithmPoints> MakeHull(IList<ConvexAlgorithmPoints> points)
        {
            List<ConvexAlgorithmPoints> newPoints = new List<ConvexAlgorithmPoints>(points);
            newPoints.Sort();
            return MakeHullPresorted(newPoints);
        }


        // Returns the convex hull, assuming that each points[i] <= points[i + 1]. Runs in O(n) time.
        public static IList<ConvexAlgorithmPoints> MakeHullPresorted(IList<ConvexAlgorithmPoints> points)
        {
            if (points.Count <= 1)
                return new List<ConvexAlgorithmPoints>(points);

            // Andrew's monotone chain algorithm. Positive y coordinates correspond to "up"
            // as per the mathematical convention, instead of "down" as per the computer
            // graphics convention. This doesn't affect the correctness of the result.

            List<ConvexAlgorithmPoints> upperHull = new List<ConvexAlgorithmPoints>();
            foreach (ConvexAlgorithmPoints p in points)
            {
                while (upperHull.Count >= 2)
                {
                    ConvexAlgorithmPoints q = upperHull[upperHull.Count - 1];
                    ConvexAlgorithmPoints r = upperHull[upperHull.Count - 2];
                    if ((q.X - r.X) * (p.Y - r.Y) >= (q.Y - r.Y) * (p.X - r.X))
                        upperHull.RemoveAt(upperHull.Count - 1);
                    else
                        break;
                }
                upperHull.Add(p);
            }
            upperHull.RemoveAt(upperHull.Count - 1);

            IList<ConvexAlgorithmPoints> lowerHull = new List<ConvexAlgorithmPoints>();
            for (int i = points.Count - 1; i >= 0; i--)
            {
                ConvexAlgorithmPoints p = points[i];
                while (lowerHull.Count >= 2)
                {
                    ConvexAlgorithmPoints q = lowerHull[lowerHull.Count - 1];
                    ConvexAlgorithmPoints r = lowerHull[lowerHull.Count - 2];
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