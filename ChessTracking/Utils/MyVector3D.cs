using System;

namespace ChessTracking.Utils
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
}
