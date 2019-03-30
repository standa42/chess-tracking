using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.Kinect;
using Emgu.CV;
using Emgu.CV.CvEnum;
using Emgu.CV.Structure;

namespace Sandbox.Forms.HarrisCornerDetection
{
    public class LineExtractorFromCornerImage
    {
        public LineExtractorFromCornerImage()
        {
            
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        byte[] pixels = null;
        Image<Bgra, byte> colorImg;
        private Bitmap colorBitmap;
        
        
        public static Bitmap FinalLine(Bitmap bm, Point2D[] points, Bitmap pictureBitmap)
        {
            int pairCount = ((points.Length * (points.Length - 1)) / 2);
            float distanceThreshold = 14f;
            Line[] lines = new Line[pairCount];

            int counter = 0;

            for (int i = 0; i < points.Length; i++)
            {
                for (int j = 0; j < points.Length; j++)
                {
                    if (i < j)
                    {
                        float a = (points[i].Y - points[j].Y) / ((float)(points[i].X - points[j].X));
                        float b = points[i].Y - a * points[i].X;

                        lines[counter] = new Line(a, b);

                        var closePoints = new List<Point2D>();

                        for (int k = 0; k < points.Length; k++)
                        {
                            var distance = ((Math.Abs(a * points[k].X + b - points[k].Y)) / (Math.Sqrt(a * a + 1)));

                            if (distance < distanceThreshold)
                            {
                                closePoints.Add(points[k]);

                                lines[counter].pointCount++;
                                if (lines[counter].minX > points[k].X)
                                {
                                    lines[counter].minX = points[k].X;
                                }
                                if (lines[counter].maxX < points[k].X)
                                {
                                    lines[counter].maxX = points[k].X;
                                }
                                if (lines[counter].maxY < points[k].Y)
                                {
                                    lines[counter].maxY = points[k].Y;
                                }

                                lines[counter].Ysum += points[k].Y;
                                lines[counter].error += (float)(distance);
                            }
                        }

                        if (closePoints.Count > 8)
                        {
                            float distanceDifferenceThreshold = 14f;

                            var sorted = closePoints.OrderBy(x => x.X);

                            float distSum = 0;

                            for (int l = 0; l < closePoints.Count - 1; l++)
                            {
                                distSum += (float)Math.Sqrt(((closePoints[l].X - closePoints[l + 1].X) * (closePoints[l].X - closePoints[l + 1].X)) + ((closePoints[l].Y - closePoints[l + 1].Y) * (closePoints[l].Y - closePoints[l + 1].Y)));
                            }

                            lines[counter].DistanceMean = distSum / (closePoints.Count - 1);

                            bool distanceContraintIsOk = true;

                            for (int l = 0; l < closePoints.Count - 1; l++)
                            {
                                var distance = (float)Math.Sqrt(((closePoints[l].X - closePoints[l + 1].X) * (closePoints[l].X - closePoints[l + 1].X)) + ((closePoints[l].Y - closePoints[l + 1].Y) * (closePoints[l].Y - closePoints[l + 1].Y)));
                                if (!(((distance + distanceDifferenceThreshold) > lines[counter].DistanceMean) && ((distance - distanceDifferenceThreshold) < lines[counter].DistanceMean)))
                                {
                                    distanceContraintIsOk = false;
                                }
                            }

                            if (!distanceContraintIsOk)
                            {
                                lines[counter].pointCount = 1;
                                lines[counter].b = -200000;
                            }

                        }

                        lines[counter].Ymean = lines[counter].Ysum / (float)lines[counter].pointCount;
                        lines[counter].error = lines[counter].error / lines[counter].pointCount;

                        counter++;
                    }
                }
            }

            List<Line> result = lines
                .ToList()
                .Where(x => x.pointCount > 8)
                .OrderBy(x => x.maxY)
                .ThenBy(x => x.Ymean)
                .ThenByDescending(x => x.error)
                //.OrderByDescending(x => x.error)
                //.ThenBy(x => x.Ymean)
                //.ThenBy(x => x.maxY)
                .ToList();

            if (result.Count == 0)
            {
                return bm;
            }

            var item = result[result.Count - 1];

            BitmapData bitmapData = pictureBitmap.LockBits(new Rectangle(0, 0, pictureBitmap.Width, pictureBitmap.Height), ImageLockMode.ReadWrite, PixelFormat.Format24bppRgb);
            int bitmapSize = pictureBitmap.Height * pictureBitmap.Width;
            int width = pictureBitmap.Width;
            int height = pictureBitmap.Height;
            unsafe
            {
                byte* ptr = (byte*)bitmapData.Scan0;

                for (int y = 0; y < height; y++)
                {
                    for (int x = 0; x < width; x++)
                    {
                        if (
                            (((Math.Abs(item.a * x + item.b - y)) / (Math.Sqrt(item.a * item.a + 1))) < 1.5f) &&
                            x < item.maxX &&
                            x > item.minX
                            )
                        {
                            *(ptr) = 0;
                            *(ptr + 1) = 255;
                            *(ptr + 2) = 0;
                        }

                        ptr++;
                        ptr++;
                        ptr++;
                    }
                }
            }
            pictureBitmap.UnlockBits(bitmapData);


            return pictureBitmap;
        }

        private struct Line
        {
            public float a;
            public float b;
            public int pointCount;
            public int minX;
            public int maxX;
            public int maxY;
            public float Ymean;
            public int Ysum;
            public float error;
            public float DistanceMean;

            public Line(float a, float b)
            {
                this.a = a;
                this.b = b;
                pointCount = 0;
                minX = int.MaxValue;
                maxX = int.MinValue;
                maxY = int.MinValue;
                Ymean = 0;
                Ysum = 0;
                error = 0;
                DistanceMean = 0;
            }
        }

        public static Point2D[] GimmeRepresentants(Bitmap bm)
        {
            // -1 empty, 0 corner, 1+ areas 
            int[] map = new int[bm.Width * bm.Height];
            for (int i = 0; i < map.Length; i++)
            {
                map[i] = -1;
            }

            BitmapData bitmapData = bm.LockBits(new Rectangle(0, 0, bm.Width, bm.Height), ImageLockMode.ReadWrite, PixelFormat.Format24bppRgb);
            int bitmapSize = bm.Height * bm.Width;
            int width = bm.Width;
            int height = bm.Height;
            unsafe
            {
                byte* ptr = (byte*)bitmapData.Scan0;

                for (int y = 0; y < height; y++)
                {
                    for (int x = 0; x < width; x++)
                    {
                        if (*ptr == 255)
                        {
                            int position = y * width + x;
                            map[position] = 0;
                        }

                        ptr++;
                        ptr++;
                        ptr++;
                    }
                }
            }
            bm.UnlockBits(bitmapData);

            int representantCount = 0;

            for (int y = 0; y < height; y++)
            {
                for (int x = 0; x < width; x++)
                {
                    var position = y * width + x;

                    if (map[position] == 0)
                    {
                        representantCount++;

                        Queue<Point2D> q = new Queue<Point2D>();

                        map[position] = representantCount;
                        q.Enqueue(new Point2D(x, y));

                        while (q.Count != 0)
                        {
                            var item = q.Dequeue();

                            int pos;

                            pos = PosFromCoor(item.X + 1, item.Y, width);
                            if (ValidCoordiante(0, bitmapSize, pos) && map[pos] == 0)
                            {
                                map[pos] = representantCount;
                                q.Enqueue(new Point2D(item.X + 1, item.Y));
                            }

                            pos = PosFromCoor(item.X - 1, item.Y, width);
                            if (ValidCoordiante(0, bitmapSize, pos) && map[pos] == 0)
                            {
                                map[pos] = representantCount;
                                q.Enqueue(new Point2D(item.X - 1, item.Y));
                            }

                            pos = PosFromCoor(item.X, item.Y + 1, width);
                            if (ValidCoordiante(0, bitmapSize, pos) && map[pos] == 0)
                            {
                                map[pos] = representantCount;
                                q.Enqueue(new Point2D(item.X, item.Y + 1));
                            }

                            pos = PosFromCoor(item.X, item.Y - 1, width);
                            if (ValidCoordiante(0, bitmapSize, pos) && map[pos] == 0)
                            {
                                map[pos] = representantCount;
                                q.Enqueue(new Point2D(item.X, item.Y - 1));
                            }
                        }
                    }
                }
            }

            Point2D[] points = new Point2D[representantCount + 1];
            int[] theirCount = new int[representantCount + 1];

            for (int y = 0; y < height; y++)
            {
                for (int x = 0; x < width; x++)
                {
                    int position = PosFromCoor(x, y, width);

                    if (map[position] > 0)
                    {
                        theirCount[map[position]]++;
                        points[map[position]].X += x;
                        points[map[position]].Y += y;
                    }
                }
            }

            List<Point2D> pointsList = new List<Point2D>();

            for (int i = 1; i < representantCount + 1; i++)
            {
                pointsList.Add(new Point2D(points[i].X / theirCount[i], points[i].Y / theirCount[i]));
            }

            foreach (var point2D in pointsList)
            {
                bm.SetPixel(point2D.X, point2D.Y, Color.Brown);
            }

            return pointsList.ToArray();
        }

        private static int PosFromCoor(int x, int y, int width)
        {
            return y * width + x;
        }

        public static unsafe Bitmap RefineBitmap(Bitmap oldBitmap/*, Graphics g*/)
        {
            var newBitmap = new Bitmap(oldBitmap.Width, oldBitmap.Height, PixelFormat.Format24bppRgb);

            BitmapData newBitmapData = newBitmap.LockBits(new Rectangle(0, 0, newBitmap.Width, newBitmap.Height), ImageLockMode.ReadWrite, PixelFormat.Format24bppRgb); // PixelFormat.Format24bppRgb
            BitmapData oldBitmapData = oldBitmap.LockBits(new Rectangle(0, 0, oldBitmap.Width, oldBitmap.Height), ImageLockMode.ReadWrite, PixelFormat.Format24bppRgb); // PixelFormat.Format24bppRgb

            unsafe
            {
                byte* oldPtr = (byte*)oldBitmapData.Scan0;
                byte* newPtr = (byte*)newBitmapData.Scan0;

                int bitmapSize = oldBitmap.Height * oldBitmap.Width;
                int width = oldBitmap.Width;
                int height = oldBitmap.Height;

                // draw pixel according to its type
                for (int y = 0; y < height; y++)
                {
                    // compensates sensors natural flip of image
                    //for (int x = bm.Width - 1; x >= 0; x--)
                    for (int x = 0; x < width; x++)
                    {
                        int position = y * width + x;
                        byte value = 255;

                        if (*oldPtr == value)
                        {
                            if (ValidCoordiante(0, bitmapSize, position - oldBitmap.Width))
                            {
                                *(newPtr - (width * 3) + 0) = value;
                                *(newPtr - (width * 3) + 1) = value;
                                *(newPtr - (width * 3) + 2) = value;
                            }

                            if (ValidCoordiante(0, bitmapSize, position + oldBitmap.Width))
                            {
                                *(newPtr + (width * 3) + 0) = value;
                                *(newPtr + (width * 3) + 1) = value;
                                *(newPtr + (width * 3) + 2) = value;
                            }

                            if (ValidCoordiante(0, bitmapSize, position - 1))
                            {
                                *(newPtr - (1 * 3) + 0) = value;
                                *(newPtr - (1 * 3) + 1) = value;
                                *(newPtr - (1 * 3) + 2) = value;
                            }

                            if (ValidCoordiante(0, bitmapSize, position + 1))
                            {
                                *(newPtr + (1 * 3) + 0) = value;
                                *(newPtr + (1 * 3) + 1) = value;
                                *(newPtr + (1 * 3) + 2) = value;
                            }
                        }

                        oldPtr++;
                        oldPtr++;
                        oldPtr++;


                        newPtr++;
                        newPtr++;
                        newPtr++;



                        /*
                        *ptr++ = 255;
                        *ptr++ = 0;
                        *ptr++ = 255;
                        */
                    }
                }
            }

            oldBitmap.UnlockBits(oldBitmapData);
            newBitmap.UnlockBits(newBitmapData);

            return newBitmap;
            //g.DrawImage(bm, UpperLeftCorner.X, UpperLeftCorner.Y);
        }

        public static bool ValidCoordiante(int min, int max, int value)
        {
            if (value < max && value >= min)
            {
                return true;
            }
            return false;
        }

        private int whatever = 7;
        private int stEven = 7;
        private int stSmallValue = 1;
        private int stSmallExponent = -2;
        private float stSmall;
        private int grayThreshold = 2;
        
    }

    public struct Point2D
    {
        public int X;
        public int Y;

        public Point2D(int x, int y)
        {
            this.X = x;
            this.Y = y;
        }
    }
}
