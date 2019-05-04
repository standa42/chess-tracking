using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Linq;

using Microsoft.Kinect;

namespace ChessTracking.ProcessingPipeline.Plane
{
    public struct MyCameraSpacePoint
    {
        public float X;
        public float Y;
        public float Z;

        public PixelType type;

        public MyCameraSpacePoint(float x, float y, float z)
        {
            X = x;
            Y = y;
            Z = z;

            type = PixelType.NotMarked;
        }

        public MyCameraSpacePoint(ref CameraSpacePoint point)
        {
            X = point.X;
            Y = point.Y;
            Z = point.Z;

            type = PixelType.NotMarked;
        }
    }

    public enum PixelType
    {
        NotMarked,
        Invalid,
        Table,
        Object,
    }

    public struct ConvexAlgorithmPoints : IComparable<ConvexAlgorithmPoints>
    {
        public float X;
        public float Y;
        public float Z;

        public PixelType type;

        public int PositionInBitmap;

        public ConvexAlgorithmPoints(ref MyCameraSpacePoint point, int position)
        {
            X = point.X;
            Y = point.Y;
            Z = point.Z;

            type = point.type;

            PositionInBitmap = position;
        }

        public int CompareTo(ConvexAlgorithmPoints other)
        {
            if (X < other.X)
                return -1;
            else if (X > other.X)
                return +1;
            else if (Y < other.Y)
                return -1;
            else if (Y > other.Y)
                return +1;
            else
                return 0;
        }
    }


    public struct ObjectLocationPoints : IComparable<ObjectLocationPoints>
    {
        public float X;
        public float Y;
        public float Z;

        public int bX;
        public int bY;

        public PixelType type;

        public ObjectLocationPoints(ref MyCameraSpacePoint point, int bitmapX, int bitmapY)
        {
            X = point.X;
            Y = point.Y;
            Z = point.Z;

            type = point.type;

            bX = bitmapX;
            bY = bitmapY;
        }

        public int CompareTo(ObjectLocationPoints other)
        {
            if (bX < other.bX)
                return -1;
            else if (bX > other.bX)
                return +1;
            else if (bY < other.bY)
                return -1;
            else if (bY > other.bY)
                return +1;
            else
                return 0;
        }
    }

}