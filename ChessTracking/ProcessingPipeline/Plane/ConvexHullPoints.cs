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


using System;

namespace ChessTracking.ProcessingPipeline.Plane
{
    public struct ConvexHullPoints : IComparable<ConvexHullPoints>
    {
        public float X;
        public float Y;
        public float Z;

        public PixelType Type;

        public int PositionInBitmap;

        public ConvexHullPoints(ref MyCameraSpacePoint point, int position)
        {
            X = point.X;
            Y = point.Y;
            Z = point.Z;

            Type = point.Type;

            PositionInBitmap = position;
        }

        public int CompareTo(ConvexHullPoints other)
        {
            if (X < other.X)
                return -1;
            if (X > other.X)
                return +1;
            if (Y < other.Y)
                return -1;
            if (Y > other.Y)
                return +1;
            return 0;
        }
    }
}
