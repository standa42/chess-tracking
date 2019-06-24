using System;

namespace ChessTracking.Utils
{
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