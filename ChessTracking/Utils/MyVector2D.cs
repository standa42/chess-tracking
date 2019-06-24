using System;

namespace ChessTracking.Utils
{
    public class MyVector2D
    {
        public double X;
        public double Y;

        public MyVector2D(double x, double y)
        {
            this.X = x;
            this.Y = y;
        }

        public static double GetAngleBetweenTwoVectors(MyVector2D v1, MyVector2D v2)
        {
            return Math.Acos(MyVector2D.ScalarProduct(v1, v2) / (v1.Magnitude() * v2.Magnitude()));
        }

        public static double ScalarProduct(MyVector2D v1, MyVector2D v2)
        {
            return (v1.X * v2.X) + (v1.Y * v2.Y);
        }

        public double Magnitude()
        {
            return Math.Sqrt(X * X + Y * Y);
        }

        public static MyVector2D Difference(MyVector2D v1, MyVector2D v2)
        {
            return new MyVector2D(v2.X - v1.X, v2.Y - v1.Y);
        }

        public static MyVector2D Normalize(MyVector2D v)
        {
            double length = v.Magnitude();

            return new MyVector2D(v.X / length, v.Y / length);
        }

        public static MyVector2D MultiplyByNumber(MyVector2D v, double number)
        {
            return new MyVector2D(v.X * number, v.Y * number);
        }

        public static double Distance(MyVector2D v1, MyVector2D v2)
        {
            double diffX = v1.X - v2.X;
            double diffY = v1.Y - v2.Y;

            return Math.Sqrt(diffX * diffX + diffY * diffY);
        }

        public static double DistancePointLine(MyVector2D linePoint1, MyVector2D linePoint2, double x, double y)
        {
            double first = (linePoint2.Y - linePoint1.Y) * x;
            double second = (linePoint2.X - linePoint1.X) * y;
            double third = linePoint2.X * linePoint1.Y;
            double fourth = linePoint1.X * linePoint2.Y;

            double citatel = Math.Abs(first - second + third - fourth);

            double fifth = linePoint2.Y - linePoint1.Y;
            double sixth = linePoint2.X - linePoint1.X;

            double jmenovatel = Math.Sqrt(fifth * fifth + sixth * sixth);

            return citatel / jmenovatel;
        }
    }
}