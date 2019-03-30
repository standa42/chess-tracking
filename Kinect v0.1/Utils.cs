using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kinect_v0._1
{
    public class MyVector3D
    {
        public double x, y, z;

        public MyVector3D()
        {
            this.x = 0;
            this.y = 0;
            this.z = 0;
        }

        public MyVector3D(double x, double y, double z)
        {
            this.x = x;
            this.y = y;
            this.z = z;
        }

        public static MyVector3D Normalize(MyVector3D v)
        {
            float magnitude = (float)Math.Sqrt((v.x * v.x) + (v.y * v.y) + (v.z * v.z));
            return new MyVector3D(v.x / magnitude, v.y / magnitude, v.z / magnitude);
        }

        public static MyVector3D Difference(MyVector3D a, MyVector3D b)
        {
            return new MyVector3D(a.x - b.x, a.y - b.y, a.z - b.z);
        }

        public static MyVector3D Addition(MyVector3D a, MyVector3D b)
        {
            return new MyVector3D(a.x + b.x, a.y + b.y, a.z + b.z);
        }

        public double GetMagnitude()
        {
            return Math.Sqrt(x * x + y * y + z * z);
        }

        public static double Distance(MyVector3D a, MyVector3D b)
        {
            return Math.Sqrt(Math.Pow(b.x - a.x, 2) + Math.Pow(b.y - a.y, 2) + Math.Pow(b.z - a.z, 2));
        }

        public static double DotProduct(MyVector3D a, MyVector3D b)
        {
            return (a.x * b.x + a.y * b.y + a.z * b.z);
        }

        public static MyVector3D CrossProduct(MyVector3D a, MyVector3D b)
        {
            return new MyVector3D((a.y * b.z) - (a.z * b.y), (a.z * b.x) - (a.x * b.z), (a.x * b.y) - (a.y * b.x));
        }

        public static double Multiplication(MyVector3D a, MyVector3D b)
        {
            return (a.x * b.x) + (a.y * b.y) + (a.z * b.z);
        }

        public double Magnitude()
        {
            return Math.Sqrt(x * x + y * y + z * z);
        }

        public static double AngleInDeg(MyVector3D a, MyVector3D b)
        {
            return Math.Acos(DotProduct(a, b) / (a.GetMagnitude() * b.GetMagnitude())) * (180 / Math.PI);
        }

        public static MyVector3D MultiplyByNumber(MyVector3D v, double number)
        {
            return new MyVector3D(v.x * number, v.y * number, v.z * number);
        }

        public static MyVector3D Negate(MyVector3D v)
        {
            return new MyVector3D(-v.x, -v.y, -v.z);
        }
}

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





    public class MyVector3DStruct
    {
        public double x, y, z;

        public MyVector3DStruct()
        {
            this.x = 0;
            this.y = 0;
            this.z = 0;
        }

        public MyVector3DStruct(double x, double y, double z)
        {
            this.x = x;
            this.y = y;
            this.z = z;
        }

        public static MyVector3DStruct Normalize(ref MyVector3DStruct v)
        {
            float magnitude = (float)Math.Sqrt((v.x * v.x) + (v.y * v.y) + (v.z * v.z));
            return new MyVector3DStruct(v.x / magnitude, v.y / magnitude, v.z / magnitude);
        }

        public static MyVector3DStruct Normalize(MyVector3DStruct v)
        {
            float magnitude = (float)Math.Sqrt((v.x * v.x) + (v.y * v.y) + (v.z * v.z));
            return new MyVector3DStruct(v.x / magnitude, v.y / magnitude, v.z / magnitude);
        }

        public static MyVector3DStruct Difference(ref MyVector3DStruct a, ref MyVector3DStruct b)
        {
            return new MyVector3DStruct(a.x - b.x, a.y - b.y, a.z - b.z);
        }

        public static MyVector3DStruct Addition(ref MyVector3DStruct a, ref MyVector3DStruct b)
        {
            return new MyVector3DStruct(a.x + b.x, a.y + b.y, a.z + b.z);
        }

        public static MyVector3DStruct Addition(MyVector3DStruct a, MyVector3DStruct b)
        {
            return new MyVector3DStruct(a.x + b.x, a.y + b.y, a.z + b.z);
        }

        public double GetMagnitude()
        {
            return Math.Sqrt(x * x + y * y + z * z);
        }

        public static double Distance(ref MyVector3DStruct a, ref MyVector3DStruct b)
        {
            return Math.Sqrt(Math.Pow(b.x - a.x, 2) + Math.Pow(b.y - a.y, 2) + Math.Pow(b.z - a.z, 2));
        }

        public static double Distance(ref MyVector3DStruct a, MyVector3DStruct b)
        {
            return Math.Sqrt(Math.Pow(b.x - a.x, 2) + Math.Pow(b.y - a.y, 2) + Math.Pow(b.z - a.z, 2));
        }

        public static double DotProduct(ref MyVector3DStruct a, ref MyVector3DStruct b)
        {
            return (a.x * b.x + a.y * b.y + a.z * b.z);
        }

        public static MyVector3DStruct CrossProduct(ref MyVector3DStruct a, ref MyVector3DStruct b)
        {
            return new MyVector3DStruct((a.y * b.z) - (a.z * b.y), (a.z * b.x) - (a.x * b.z), (a.x * b.y) - (a.y * b.x));
        }

        public static double Multiplication(ref MyVector3DStruct a, ref MyVector3DStruct b)
        {
            return (a.x * b.x) + (a.y * b.y) + (a.z * b.z);
        }

        public double Magnitude()
        {
            return Math.Sqrt(x * x + y * y + z * z);
        }

        public static double AngleInDeg(ref MyVector3DStruct a, ref MyVector3DStruct b)
        {
            return Math.Acos(DotProduct(ref a, ref b) / (a.GetMagnitude() * b.GetMagnitude())) * (180 / Math.PI);
        }

        public static MyVector3DStruct MultiplyByNumber(ref MyVector3DStruct v, double number)
        {
            return new MyVector3DStruct(v.x * number, v.y * number, v.z * number);
        }

        public static MyVector3DStruct MultiplyByNumber(MyVector3DStruct v, double number)
        {
            return new MyVector3DStruct(v.x * number, v.y * number, v.z * number);
        }

        public static MyVector3DStruct Negate(ref MyVector3DStruct v)
        {
            return new MyVector3DStruct(-v.x, -v.y, -v.z);
        }
    }





}
