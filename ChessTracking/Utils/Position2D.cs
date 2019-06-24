using System;

namespace ChessTracking.Utils
{
    public struct Position2D
    {
        public int X { get; set; }
        public int Y { get; set; }

        public Position2D(int x, int y)
        {
            X = x;
            Y = y;
        }

        public void RotateAroundPoint(int degrees, Position2D point)
        {
            int oldX = this.X - point.X;
            int oldY = this.Y - point.Y;

            int newX = (int)(oldX * Math.Cos(DegToRad(degrees)) - oldY * Math.Sin(DegToRad(degrees)));
            int newY = (int)(oldX * Math.Sin(DegToRad(degrees)) + oldY * Math.Cos(DegToRad(degrees)));

            this.X = newX + point.X;
            this.Y = newY + point.Y;
        }

        private double DegToRad(double degrees)
        {
            return (Math.PI / 180) * degrees;
        }
    }
}