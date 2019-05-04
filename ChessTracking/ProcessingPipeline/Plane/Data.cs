using System;
using System.Collections.Generic;
//using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

using Microsoft.Kinect;

using System.Windows.Forms;

using System.Drawing;
using System.Drawing.Imaging;
using System.Drawing.Drawing2D;

using System.Diagnostics;
using System.Globalization;
using System.Runtime.CompilerServices;

using MathNet.Numerics;
using MathNet.Numerics.LinearRegression;
using MathNet.Numerics.LinearAlgebra;
using MathNet.Numerics.LinearAlgebra.Double;

namespace ChessTracking.ProcessingPipeline.Plane
{

    public class Data
    {
        /// <summary>
        /// Reprezents raw points that can be further processed
        /// </summary>
        public MyCameraSpacePoint[] RawData;

        public Data(KinectSensor kinectSensor, KinectBuffer depthBuffer, FrameDescription depthFrameDescription)
        {
            var TempData = new CameraSpacePoint[(depthBuffer.Size / depthFrameDescription.BytesPerPixel)];
            kinectSensor.CoordinateMapper.MapDepthFrameToCameraSpaceUsingIntPtr(depthBuffer.UnderlyingBuffer, depthBuffer.Size, TempData);

            // Convert to my reprezentation
            RawData = new MyCameraSpacePoint[TempData.Length];
            for (int i = 0; i < TempData.Length; i++)
            {
                RawData[i] = new MyCameraSpacePoint(ref TempData[i]);
            }
        }

        public Data(CameraSpacePoint[] preparedCameraSpacePoints)
        {
            RawData = new MyCameraSpacePoint[preparedCameraSpacePoints.Length];
            for (int i = 0; i < preparedCameraSpacePoints.Length; i++)
            {
                RawData[i] = new MyCameraSpacePoint(ref preparedCameraSpacePoints[i]);
            }
        }

        /// <summary>
        /// Renders data to bitmap a draws them to the form graphics
        /// </summary>
        /// <param name="bm">bitmap</param>
        /// <param name="g">graphics</param>
        /// <param name="UpperLeftCorner">left-upper coordinates to draw bitmap</param>
        public unsafe void Render(Bitmap bm, Graphics g, Position2D UpperLeftCorner)
        {
            BitmapData bmpData = bm.LockBits(new Rectangle(0, 0, bm.Width, bm.Height), ImageLockMode.ReadWrite, PixelFormat.Format24bppRgb);

            unsafe
            {
                byte* ptr = (byte*)bmpData.Scan0;

                int bitmapSize = Config.DepthImageHeight * Config.DepthImageWidth;
                int depthColorChange = Config.DepthColorChange;

                // draw pixel according to its type
                for (int y = 0; y < Config.DepthImageHeight; y++)
                {
                    // compensates sensors natural flip of image
                    for (int x = Config.DepthImageWidth - 1; x >= 0; x--)
                    {
                        int position = y * Config.DepthImageWidth + x;

                        if (RawData[position].type == PixelType.Invalid)
                        {
                            *ptr++ = 255;
                            *ptr++ = 0;
                            *ptr++ = 255;
                        }
                        else if (RawData[position].type == PixelType.Table)
                        {
                            byte value = (byte)(RawData[position].Z * depthColorChange);

                            *ptr++ = 0;
                            *ptr++ = 0;
                            *ptr++ = value;
                        }
                        else if (RawData[position].type == PixelType.Object)
                        {
                            byte value = (byte)(RawData[position].Z * depthColorChange);

                            *ptr++ = 0;
                            *ptr++ = 255;
                            *ptr++ = 0;
                        }
                        else
                        {
                            byte value = (byte)(RawData[position].Z * depthColorChange);

                            *ptr++ = value;
                            *ptr++ = value;
                            *ptr++ = value;
                        }
                    }
                }
            }
            //bm.Save(@"D:\Desktop\SmallONe");
            bm.UnlockBits(bmpData);
            g.DrawImage(bm, UpperLeftCorner.X, UpperLeftCorner.Y);
        }

        public void SerializeToFile(string location)
        {
            if (Config.SerializeThisFrameToFile)
            {
                Config.SerializeThisFrameToFile = false;
            }
            else
            {
                return;
            }

            Random rnd = new Random();
            StreamWriter sw = new StreamWriter(location + DateTime.Now.ToString("X_dd-MM-yyyy_HH-MM-ss-fff", CultureInfo.InvariantCulture) + ".xyz");
            int depthColorChange = Config.DepthColorChange;
            float scaleDownConstant = 0.05f;

            for (int i = 0; i < RawData.Length; i++)
            {
                if (RawData[i].type != PixelType.Invalid)
                {
                    int r = 0;
                    int g = 0;
                    int b = 0;

                    if (RawData[i].type == PixelType.Table)
                    {
                        byte value = (byte)(RawData[i].Z * depthColorChange);
                        b = 255;
                        g = 0;
                        r = 255;
                    }
                    else if (RawData[i].type == PixelType.Object)
                    {
                        byte value = (byte)(RawData[i].Z * depthColorChange);

                        b = 0;
                        g = 255;
                        r = 0;
                    }
                    else
                    {
                        byte value = (byte)(RawData[i].Z * depthColorChange);

                        b = 255;
                        g = 255;
                        r = 255;
                    }


                    sw.WriteLine(
                   string.Format(
                       "{0} {1} {2} {3} {4} {5}",
                       (RawData[i].X * scaleDownConstant).ToString("0.00000", System.Globalization.CultureInfo.InvariantCulture),
                       (RawData[i].Y * scaleDownConstant).ToString("0.00000", System.Globalization.CultureInfo.InvariantCulture),
                       (RawData[i].Z * scaleDownConstant).ToString("0.00000", System.Globalization.CultureInfo.InvariantCulture),
                       r,
                       g,
                       b
                       )
                   );
                }
            }

            sw.Close();
        }

        public unsafe void CutOffMinMaxDepth(float minDepth, float maxDepth)
        {
            int pocetCasti = Config.paralelTasksCount;
            List<Task> tasky = new List<Task>();

            int delka = RawData.Length;
            int zacatek = 0;
            int konec = delka / pocetCasti;
            int krok = delka / pocetCasti;

            for (int i = 0; i < pocetCasti; i++)
            {
                int zac = zacatek;
                int kon = konec;

                var t = Task.Run(() => CutOffMinMaxDepthImplementation(minDepth, maxDepth, zac, kon));
                tasky.Add(t);

                zacatek += krok;
                konec += krok;
            }

            Task.WaitAll(tasky.ToArray());
        }

        private unsafe void CutOffMinMaxDepthImplementation(float minDepth, float maxDepth, int startIndex, int endIndex)
        {
            for (int i = startIndex; i < endIndex; i++)
            {
                if ((RawData[i].Z > maxDepth) || (RawData[i].Z < minDepth))
                {
                    RawData[i].Z = 0f;
                }
            }

            ValidatePointsImplementation(startIndex, endIndex);
        }

        private void ValidatePointsImplementation(int startIndex, int endIndex)
        {
            for (int i = startIndex; i < endIndex; i++)
            {
                if (RawData[i].Z == 0f)
                {
                    RawData[i].type = PixelType.Invalid;
                }
            }
        }

        public unsafe void MakeAverageOverSeveralFrames(int frameCount)
        {
            if (Config.NumberOfFramesToAverage == 1) return;

            // index of old frame the should be overriden by the new one
            Config.frameNr++;
            if (Config.frameNr == Config.NumberOfFramesToAverage)
            {
                Config.frameNr = 0;
            }
            //

            int pocetCasti = Config.paralelTasksCount;
            List<Task> tasky = new List<Task>();

            int delka = RawData.Length;
            int zacatek = 0;
            int konec = delka / pocetCasti;
            int krok = delka / pocetCasti;

            for (int i = 0; i < pocetCasti; i++)
            {
                int zac = zacatek;
                int kon = konec;

                var t = Task.Run(() => MakeAverageOverSeveralFramesImplementation(zac, kon));
                tasky.Add(t);

                zacatek += krok;
                konec += krok;
            }

            Task.WaitAll(tasky.ToArray());
        }

        private void MakeAverageOverSeveralFramesImplementation(int zacatek, int konec)
        {
            // overriding the oldest frame with the new one
            for (int i = zacatek; i < konec; i++)
            {
                Config.DataToMean[i][Config.frameNr] = RawData[i];
            }

            // make an average of several non-zero frames and assign it back to RawData
            for (int i = zacatek; i < konec; i++)
            {
                // get values from frames
                int zeroesValuesCounter = 0;
                float sumX = 0;
                float sumY = 0;
                float sumZ = 0;
                int nonZeroValuesCounter = 0;

                for (int j = 0; j < Config.NumberOfFramesToAverage; j++)
                {
                    if (Config.DataToMean[i][j].type == PixelType.Invalid)
                    {
                        zeroesValuesCounter++;
                    }
                    else
                    {
                        sumX += Config.DataToMean[i][j].X;
                        sumY += Config.DataToMean[i][j].Y;
                        sumZ += Config.DataToMean[i][j].Z;
                        nonZeroValuesCounter++;
                    }
                }

                // make an average
                if (nonZeroValuesCounter == 0)
                {
                    RawData[i].type = PixelType.Invalid;
                }
                else
                {
                    RawData[i].type = PixelType.NotMarked;
                    RawData[i].X = sumX / nonZeroValuesCounter;
                    RawData[i].Y = sumY / nonZeroValuesCounter;
                    RawData[i].Z = sumZ / nonZeroValuesCounter;
                }

            }

        }

        public void MedianTimeFilter()
        {
            int pocetCasti = 8;
            List<Task> tasky = new List<Task>();

            int delka = RawData.Length;
            int zacatek = 0;
            int konec = delka / pocetCasti;
            int krok = delka / pocetCasti;

            for (int i = 0; i < pocetCasti; i++)
            {
                int zac = zacatek;
                int kon = konec;

                var t = Task.Run(() => MedianTimeFilterImplementation(zac, kon));
                tasky.Add(t);

                zacatek += krok;
                konec += krok;
            }

            Task.WaitAll(tasky.ToArray());
        }

        public void MedianTimeFilterImplementation(int zacatek, int konec)
        {
            int modPosition = Config.FrameCount % Config.NumberOfFramesForMedian;

            for (int i = zacatek; i < konec; i++)
            {
                // delete too old frame value
                float valueToDelete = Config.queues[i][modPosition];
                bool deleted = false;
                for (int j = 0; j < Config.NumberOfFramesForMedian; j++)
                {
                    if (deleted)
                    {
                        Config.medians[i][j - 1] = Config.medians[i][j];
                    }

                    if (valueToDelete == Config.medians[i][j] && !deleted)
                    {
                        deleted = true;
                    }
                }

                // replace it with new one
                float valueToReplace = RawData[i].Z;
                Config.queues[i][modPosition] = valueToReplace;
                bool inserted = false;
                for (int j = 0; j < Config.NumberOfFramesForMedian; j++)
                {
                    if (valueToDelete > Config.medians[i][j] && !inserted)
                    {
                        inserted = true;
                    }

                    if (inserted)
                    {
                        float temp = Config.medians[i][j];
                        Config.medians[i][j] = valueToReplace;
                        valueToReplace = temp;
                    }
                }

                RawData[i].Z = Config.medians[i][Config.MedianPosition];
            }
        }

        public void RANSAC()
        {
            if (Config.tringlePointsForRANSAC == null) GeneratePseudorandomTriangles();
            //if (Config.NumberOfFramesToAverage == 1) return;

            int pocetCasti = Config.paralelTasksCount;
            List<Task> tasky = new List<Task>();

            int delka = Config.triangleCount;
            int zacatek = 0;
            int konec = delka / pocetCasti;
            int krok = delka / pocetCasti;

            for (int i = 0; i < pocetCasti; i++)
            {
                int zac = zacatek;
                int kon = konec;

                var t = Task.Run(() => RANSACimplementace(zac, kon));
                tasky.Add(t);

                zacatek += krok;
                konec += krok;
            }

            Task.WaitAll(tasky.ToArray());

            for (int i = 0; i < RawData.Length; i++)
            {
                if (RawData[i].type == PixelType.Invalid) continue;

                float pointX = RawData[i].X;
                float pointY = RawData[i].Y;
                float pointZ = RawData[i].Z;

                float distance = (float)(Math.Abs(Config.finalNormal.x * pointX + Config.finalNormal.y * pointY + Config.finalNormal.z * pointZ + Config.finalD) / Math.Sqrt(Config.finalNormal.x * Config.finalNormal.x + Config.finalNormal.y * Config.finalNormal.y + Config.finalNormal.z * Config.finalNormal.z));

                if (distance < Config.RansacThickness)
                {
                    RawData[i].type = PixelType.Table;
                }
            }
        }

        public void RANSACimplementace(int zacatek, int konec)
        {
            int GridSizeForTestingPlaneFit = Config.GridSizeForTestingPlaneFitInRANSAC;
            float threshold = Config.RansacThickness;

            double finalD = 0;
            MyVector3D finalNormal = new MyVector3D(0, 0, 0);
            int max = 0;

            for (int i = zacatek; i < konec; i++)
            {
                // get pseudorandom predefinied triangle points
                int a = Config.tringlePointsForRANSAC[3 * i];
                int b = Config.tringlePointsForRANSAC[3 * i + 1];
                int c = Config.tringlePointsForRANSAC[3 * i + 2];

                // are those points even valid?
                if (
                    RawData[a].type == PixelType.Invalid ||
                    RawData[b].type == PixelType.Invalid ||
                    RawData[c].type == PixelType.Invalid
                    ) continue;

                // get plane analytical equation (ax + by + cz + c)
                var normal = MyVector3D.CrossProduct(
                    new MyVector3D(RawData[a].X - RawData[b].X, RawData[a].Y - RawData[b].Y, RawData[a].Z - RawData[b].Z),
                    new MyVector3D(RawData[a].X - RawData[c].X, RawData[a].Y - RawData[c].Y, RawData[a].Z - RawData[c].Z)
                    );
                double d = -(normal.x * RawData[a].X + normal.y * RawData[a].Y + normal.z * RawData[a].Z);

                // Get number of points that belong to this plane
                int numberOfTablePoints = 0;
                for (int y = 0; y < Config.DepthImageHeight; y += GridSizeForTestingPlaneFit)
                {
                    for (int x = 0; x < Config.DepthImageHeight; x += GridSizeForTestingPlaneFit)
                    {
                        if (RawData[x + y * Config.DepthImageWidth].type == PixelType.Invalid) continue;

                        float pointX = RawData[x + y * Config.DepthImageWidth].X;
                        float pointY = RawData[x + y * Config.DepthImageWidth].Y;
                        float pointZ = RawData[x + y * Config.DepthImageWidth].Z;

                        float distance = (float)(Math.Abs(normal.x * pointX + normal.y * pointY + normal.z * pointZ + d) / Math.Sqrt(normal.x * normal.x + normal.y * normal.y + normal.z * normal.z));

                        if (distance < threshold)
                        {
                            numberOfTablePoints++;
                        }
                    }
                }

                // check it its optimal estimation so far
                if (numberOfTablePoints > max)
                {
                    // locally
                    max = numberOfTablePoints;
                    finalD = d;
                    finalNormal = normal;

                    // globally (over many frames)
                    if (max > Config.maxObservedPlaneSize)
                    {
                        lock (Config.lockingObj)
                        {
                            Config.maxObservedPlaneSize = max;
                            Config.finalD = finalD;
                            Config.finalNormal = finalNormal;
                        }
                    }
                }

            }




        }

        /// <summary>
        /// Selects largest continuous area marked as table
        /// Permorms it throught BFS from pseudorandom points
        /// </summary>
        public void LargestTableArea()
        {
            List<Root> roots = new List<Root>();
            roots.Add(new Root(0, 0, 0)); // empty root for filling the list
            int[] rootOvnershipMarkers = new int[RawData.Length];

            for (int y = 1; y < Config.DepthImageHeight; y += 20)
            {
                for (int x = 1; x < Config.DepthImageWidth; x += 20)
                {
                    // is table pixel and doesnt belong to any area yet? -> perform bfs from this point
                    if ((RawData[PosFromCoor(x, y)].type == PixelType.Table) && (rootOvnershipMarkers[PosFromCoor(x, y)] == 0))
                    {
                        int position = PosFromCoor(x, y);
                        int areaNumber = roots.Count;

                        roots.Add(new Root(areaNumber, x, y));

                        Queue<Point> points = new Queue<Point>();

                        points.Enqueue(new Point(x, y));
                        Root root = roots[areaNumber];
                        root.count++;

                        while (points.Count != 0)
                        {
                            Point point = points.Dequeue();

                            if ((RawData[point.position].type == PixelType.Table) && (rootOvnershipMarkers[point.position] == 0))
                            {
                                root.count++;
                                rootOvnershipMarkers[point.position] = areaNumber;

                                //
                                if (point.y > 0)
                                {
                                    points.Enqueue(new Point(point.x, point.y - 1));
                                }

                                if (point.x > 0)
                                {
                                    points.Enqueue(new Point(point.x - 1, point.y));
                                }

                                if (point.y < Config.DepthImageHeight - 1)
                                {
                                    points.Enqueue(new Point(point.x, point.y + 1));
                                }

                                if (point.x < Config.DepthImageWidth - 1)
                                {
                                    points.Enqueue(new Point(point.x + 1, point.y));
                                }

                                //

                            }
                        }

                    }
                }
            }

            // Finds largest area
            int max = 0;
            int maxPosition = 0;
            for (int i = 0; i < roots.Count; i++)
            {
                if (roots[i].count > max)
                {
                    max = roots[i].count;
                    maxPosition = i;
                }
            }

            // Invalidate all table pixels that doesnt belong to the largest area
            for (int i = 0; i < RawData.Length; i++)
            {
                if ((rootOvnershipMarkers[i] != maxPosition) && RawData[i].type == PixelType.Table)
                {
                    RawData[i].type = PixelType.NotMarked;
                }

            }

        }

        private struct Point
        {
            public int x;
            public int y;
            public int position;

            public Point(int _x, int _y)
            {
                x = _x;
                y = _y;
                position = x + y * Config.DepthImageWidth;
            }
        }

        private class Root
        {
            public int selfReferenceNumber;

            public int x;
            public int y;

            public int count;

            public Root(int nr, int _x, int _y)
            {
                selfReferenceNumber = nr;
                x = _x;
                y = _y;
                count = 0;
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static int PosFromCoor(int x, int y)
        {
            return y * Config.DepthImageWidth + x;
        }

        public void LinearRegression()
        {
            int step = Config.RegressionIsEvaluatedOnEveryXthTablePixel;

            int NumberOfEvaluatedTablePixels = 0;
            for (int i = 0; i < RawData.Length; i += step)
            {
                if (RawData[i].type == PixelType.Table)
                {
                    NumberOfEvaluatedTablePixels++;
                }
            }

            if (NumberOfEvaluatedTablePixels == 0) return;

            // allocate arrays for values for regression
            double[] LAx = new double[NumberOfEvaluatedTablePixels];
            double[] LAy = new double[NumberOfEvaluatedTablePixels];
            double[] LAvalue = new double[NumberOfEvaluatedTablePixels];

            double[] ones = new double[NumberOfEvaluatedTablePixels];

            // temporary - hold position in upper arrays
            int counter = 0;

            // fill data to the arrays
            for (int i = 0; i < NumberOfEvaluatedTablePixels; i++)
            {
                ones[i] = 1;
            }
            for (int i = 0; i < RawData.Length; i += step)
            {
                if (RawData[i].type == PixelType.Table)
                {
                    LAx[counter] = RawData[i].X;
                    LAy[counter] = RawData[i].Y;
                    LAvalue[counter] = RawData[i].Z;

                    counter++;
                }
            }

            // library regression solver
            var X = DenseMatrix.OfColumnArrays(ones, LAx, LAy);
            var YY = new DenseVector(LAvalue);


            double[][] abc = new double[3][];
            abc[0] = ones;
            abc[1] = LAx;
            abc[2] = LAy;

            var p = MultipleRegression.QR((MathNet.Numerics.LinearAlgebra.Matrix<double>)X, YY);
            //var p = X.QR().Solve(YY);

            // regression coeficients
            double a = p[0];
            double bx = p[1];
            double by = p[2];

            // convert them to analytical plane equation (ax + by + cz + d) where (a,b,c) is normal
            // pick 3 points, count expected value, get 2 vectors from em and count normal to plane
            float x1 = 0;
            float y1 = 0;
            float z1 = (float)(a + (bx * x1) + (by * y1));

            float x2 = 1000;
            float y2 = 0;
            float z2 = (float)(a + (bx * x2) + (by * y2));

            float x3 = 0;
            float y3 = 1000;
            float z3 = (float)(a + (bx * x3) + (by * y3));

            var normal = MyVector3D.CrossProduct(
                new MyVector3D(x1 - x2, y1 - y2, z1 - z2),
                new MyVector3D(x1 - x3, y1 - y3, z1 - z3)
                );

            double d = -(normal.x * x1 + normal.y * y1 + normal.z * z1);

            // erase potentional table markers
            for (int i = 0; i < RawData.Length; i++)
            {
                if (RawData[i].type == PixelType.Table)
                {
                    RawData[i].type = PixelType.NotMarked;
                }
            }

            // Mark pixels as table, based on distance from computed plane 
            for (int y = 0; y < Config.DepthImageHeight; y++)
            {
                for (int x = 0; x < Config.DepthImageWidth; x++)
                {
                    if (RawData[x + y * Config.DepthImageWidth].type == PixelType.Invalid)
                    {
                        continue;
                    }

                    float pointX = RawData[x + y * Config.DepthImageWidth].X;
                    float pointY = RawData[x + y * Config.DepthImageWidth].Y;
                    float pointZ = RawData[x + y * Config.DepthImageWidth].Z;

                    float distance = (float)(Math.Abs(normal.x * pointX + normal.y * pointY + normal.z * pointZ + d) / Math.Sqrt(normal.x * normal.x + normal.y * normal.y + normal.z * normal.z));

                    if (distance < Config.RegreseTloustka)
                    {
                        RawData[PosFromCoor(x, y)].type = PixelType.Table;
                    }
                }
            }


        }

        /// <summary>
        /// Provides "bird view" of the table (z coordinate is height)
        /// Translates origin to the table center
        /// Takes normal vector of the table plane and rotates it to align (0,0,1)
        /// </summary>
        /// <param name="form"></param>
        public void RotationTo2D()
        {
            // get angles to vector(0,0,1)
            // angle in plane x=0
            double dotx = Config.finalNormal.y * 0 + Config.finalNormal.z * 1;
            double detx = Config.finalNormal.y * 1 - Config.finalNormal.z * 0;
            double angleX = Math.Atan2(detx, dotx);

            // angle in plane y=0
            double doty = Config.finalNormal.x * 0 + Config.finalNormal.z * 1;
            double dety = Config.finalNormal.x * 1 - Config.finalNormal.z * 0;
            double angleY = -Math.Atan2(dety, doty);

            // rotation matrix mRowColumn
            double m11 = Math.Cos(angleY);
            double m12 = 0;
            double m13 = Math.Sin(angleY);
            double m21 = Math.Sin(angleX) * Math.Sin(angleY);
            double m22 = Math.Cos(angleX);
            double m23 = -Math.Sin(angleX) * Math.Cos(angleY);
            double m31 = -Math.Cos(angleX) * Math.Sin(angleY);
            double m32 = Math.Sin(angleX);
            double m33 = Math.Cos(angleX) * Math.Cos(angleY);

            double sumX = 0;
            double sumY = 0;
            double sumZ = 0;
            int count = 0;

            // Find the center of table by averaging values of table pixels
            for (int i = 0; i < RawData.Length; i++)
            {
                if (RawData[i].type == PixelType.Table)
                {
                    count++;
                    sumX += RawData[i].X;
                    sumY += RawData[i].Y;
                    sumZ += RawData[i].Z;
                }
            }
            MyVector3D tableCenter = new MyVector3D((sumX / count), (sumY / count), (sumZ / count));

            // Translation of origin to the center of the table 
            // and rotating them to vector(0,0,1)
            for (int i = 0; i < RawData.Length; i++)
            {
                RawData[i].X = (float)(RawData[i].X - tableCenter.x);
                RawData[i].Y = (float)(RawData[i].Y - tableCenter.y);
                RawData[i].Z = (float)(RawData[i].Z - tableCenter.z);

                float newX = (float)(RawData[i].X * m11 + RawData[i].Y * m12 + RawData[i].Z * m13);
                float newY = (float)(RawData[i].X * m21 + RawData[i].Y * m22 + RawData[i].Z * m23);
                float newZ = (float)(RawData[i].X * m31 + RawData[i].Y * m32 + RawData[i].Z * m33);

                RawData[i].X = newX;
                RawData[i].Y = newY;
                RawData[i].Z = newZ;
            }

            
        }


        public void RotationTo2DModified(double angleX, double angleY)
        {
            // rotation matrix mRowColumn
            double m11 = Math.Cos(angleY);
            double m12 = 0;
            double m13 = Math.Sin(angleY);
            double m21 = Math.Sin(angleX) * Math.Sin(angleY);
            double m22 = Math.Cos(angleX);
            double m23 = -Math.Sin(angleX) * Math.Cos(angleY);
            double m31 = -Math.Cos(angleX) * Math.Sin(angleY);
            double m32 = Math.Sin(angleX);
            double m33 = Math.Cos(angleX) * Math.Cos(angleY);

            double sumX = 0;
            double sumY = 0;
            double sumZ = 0;
            int count = 0;

            // Find the center of table by averaging values of table pixels
            for (int i = 0; i < RawData.Length; i++)
            {
                if (RawData[i].type == PixelType.Table)
                {
                    count++;
                    sumX += RawData[i].X;
                    sumY += RawData[i].Y;
                    sumZ += RawData[i].Z;
                }
            }
            MyVector3D tableCenter = new MyVector3D((sumX / count), (sumY / count), (sumZ / count));

            // Translation of origin to the center of the table 
            // and rotating them to vector(0,0,1)
            for (int i = 0; i < RawData.Length; i++)
            {
                RawData[i].X = (float)(RawData[i].X - tableCenter.x);
                RawData[i].Y = (float)(RawData[i].Y - tableCenter.y);
                RawData[i].Z = (float)(RawData[i].Z - tableCenter.z);

                float newX = (float)(RawData[i].X * m11 + RawData[i].Y * m12 + RawData[i].Z * m13);
                float newY = (float)(RawData[i].X * m21 + RawData[i].Y * m22 + RawData[i].Z * m23);
                float newZ = (float)(RawData[i].X * m31 + RawData[i].Y * m32 + RawData[i].Z * m33);


                RawData[i].X = newX;
                RawData[i].Y = newY;
                RawData[i].Z = newZ;
            }

        }

        public Tuple<double,double> RotationTo2DModified()
        {
            // get angles to vector(0,0,1)
            // angle in plane x=0
            double dotx = Config.finalNormal.y * 0 + Config.finalNormal.z * 1;
            double detx = Config.finalNormal.y * 1 - Config.finalNormal.z * 0;
            double angleX = Math.Atan2(detx, dotx);

            // angle in plane y=0
            double doty = Config.finalNormal.x * 0 + Config.finalNormal.z * 1;
            double dety = Config.finalNormal.x * 1 - Config.finalNormal.z * 0;
            double angleY = -Math.Atan2(dety, doty);

            // rotation matrix mRowColumn
            double m11 = Math.Cos(angleY);
            double m12 = 0;
            double m13 = Math.Sin(angleY);
            double m21 = Math.Sin(angleX) * Math.Sin(angleY);
            double m22 = Math.Cos(angleX);
            double m23 = -Math.Sin(angleX) * Math.Cos(angleY);
            double m31 = -Math.Cos(angleX) * Math.Sin(angleY);
            double m32 = Math.Sin(angleX);
            double m33 = Math.Cos(angleX) * Math.Cos(angleY);

            double sumX = 0;
            double sumY = 0;
            double sumZ = 0;
            int count = 0;

            // Find the center of table by averaging values of table pixels
            for (int i = 0; i < RawData.Length; i++)
            {
                if (RawData[i].type == PixelType.Table)
                {
                    count++;
                    sumX += RawData[i].X;
                    sumY += RawData[i].Y;
                    sumZ += RawData[i].Z;
                }
            }
            MyVector3D tableCenter = new MyVector3D((sumX / count), (sumY / count), (sumZ / count));

            // Translation of origin to the center of the table 
            // and rotating them to vector(0,0,1)
            for (int i = 0; i < RawData.Length; i++)
            {
                RawData[i].X = (float)(RawData[i].X - tableCenter.x);
                RawData[i].Y = (float)(RawData[i].Y - tableCenter.y);
                RawData[i].Z = (float)(RawData[i].Z - tableCenter.z);

                float newX = (float)(RawData[i].X * m11 + RawData[i].Y * m12 + RawData[i].Z * m13);
                float newY = (float)(RawData[i].X * m21 + RawData[i].Y * m22 + RawData[i].Z * m23);
                float newZ = (float)(RawData[i].X * m31 + RawData[i].Y * m32 + RawData[i].Z * m33);


                RawData[i].X = newX;
                RawData[i].Y = newY;
                RawData[i].Z = newZ;
            }
            return new Tuple<double,double>(angleX, angleY);
        }

        public void GaussianFilterOnZBasedOnBitmapCoordinates()
        {
            float[] newZ = new float[RawData.Length];

            for (int y = 0; y < Config.DepthImageHeight; y++)
            {
                for (int x = 0; x < Config.DepthImageWidth; x++)
                {
                    if ((y > 2) && (x > 2) && (y < Config.DepthImageHeight - 3) && (x < Config.DepthImageWidth - 3))
                    {
                        float sum =
                            RawData[PosFromCoor(x - 2, y - 2)].Z * 1 +
                            RawData[PosFromCoor(x - 1, y - 2)].Z * 4 +
                            RawData[PosFromCoor(x, y - 2)].Z * 7 +
                            RawData[PosFromCoor(x + 1, y - 2)].Z * 4 +
                            RawData[PosFromCoor(x + 2, y - 2)].Z * 1 +
                            RawData[PosFromCoor(x - 2, y - 1)].Z * 4 +
                            RawData[PosFromCoor(x - 1, y - 1)].Z * 16 +
                            RawData[PosFromCoor(x, y - 1)].Z * 35 +
                            RawData[PosFromCoor(x + 1, y - 1)].Z * 16 +
                            RawData[PosFromCoor(x + 2, y - 1)].Z * 4 +
                            RawData[PosFromCoor(x - 2, y)].Z * 7 +
                            RawData[PosFromCoor(x - 1, y)].Z * 35 +
                            RawData[PosFromCoor(x, y)].Z * 61 +
                            RawData[PosFromCoor(x + 1, y)].Z * 35 +
                            RawData[PosFromCoor(x + 2, y)].Z * 7 +
                            RawData[PosFromCoor(x - 2, y + 1)].Z * 4 +
                            RawData[PosFromCoor(x - 1, y + 1)].Z * 16 +
                            RawData[PosFromCoor(x, y + 1)].Z * 35 +
                            RawData[PosFromCoor(x + 1, y + 1)].Z * 16 +
                            RawData[PosFromCoor(x + 2, y + 1)].Z * 4 +
                            RawData[PosFromCoor(x - 2, y + 2)].Z * 1 +
                            RawData[PosFromCoor(x - 1, y + 2)].Z * 4 +
                            RawData[PosFromCoor(x, y + 2)].Z * 7 +
                            RawData[PosFromCoor(x + 1, y + 2)].Z * 4 +
                            RawData[PosFromCoor(x + 2, y + 2)].Z * 1;
                        newZ[PosFromCoor(x, y)] = sum / 329;
                    }
                }
            }

            for (int i = 0; i < RawData.Length; i++)
            {
                RawData[i].Z = newZ[i];
            }
        }

        public void ConvexHullAlgorithm()
        {
            // get candidates to convex hull
            List<ConvexAlgorithmPoints> pts = new List<ConvexAlgorithmPoints>();

            for (int y = 0; y < Config.DepthImageHeight; y++)
            {
                for (int x = 0; x < Config.DepthImageWidth; x++)
                {
                    #region longIfs - optimalization - takes only pixels of table that are surrounded with different pixel type
                    if (RawData[PosFromCoor(x, y)].type == PixelType.Table)
                    {

                        bool left = false;
                        bool right = false;
                        bool up = false;
                        bool down = false;

                        if (x > 0)
                        {
                            if (RawData[PosFromCoor(x - 1, y)].type == PixelType.Table)
                            {
                                left = true;
                            }
                            else
                            {
                                left = false;
                            }
                        }
                        else
                        {
                            left = true;
                        }

                        if (x < Config.DepthImageWidth - 1)
                        {
                            if (RawData[PosFromCoor(x + 1, y)].type == PixelType.Table)
                            {
                                right = true;
                            }
                            else
                            {
                                right = false;
                            }
                        }
                        else
                        {
                            right = true;
                        }

                        if (y > 0)
                        {
                            if (RawData[PosFromCoor(x, y - 1)].type == PixelType.Table)
                            {
                                up = true;
                            }
                            else
                            {
                                up = false;
                            }
                        }
                        else
                        {
                            up = true;
                        }

                        if (y < Config.DepthImageHeight - 1)
                        {
                            if (RawData[PosFromCoor(x, y + 1)].type == PixelType.Table)
                            {
                                down = true;
                            }
                            else
                            {
                                down = false;
                            }
                        }
                        else
                        {
                            down = true;
                        }

                        if (!(left && right && up && down))
                        {
                            pts.Add(new ConvexAlgorithmPoints(ref RawData[PosFromCoor(x, y)], PosFromCoor(x, y)));
                        }

                        #endregion
                    }
                }
            }

            // make convex hull with algorithm
            List<ConvexAlgorithmPoints> hullPts = (List<ConvexAlgorithmPoints>)ConvexHull.MakeHull(pts);

            // make Minimal Bounding Rectangle and check, which points fall into it

            //TODO refactor
            int i1 = 0;
            int i2 = 0;
            double minArea = double.PositiveInfinity;
            double minAreaAngle = double.PositiveInfinity;
            MyVector2D maxValues = new MyVector2D(0, 0);
            MyVector2D minValues = new MyVector2D(0, 0);

            for (int i = 0; i < hullPts.Count; i++)
            {
                // z jakych bodu se bude delat vektor
                int index1 = i;
                int index2 = i + 1;

                // spojeni bodu na konci a zacatku listu
                if (index1 == (hullPts.Count - 1))
                {
                    index2 = 0;
                }

                // get vectors and angle
                MyVector2D v = new MyVector2D(hullPts[index1].X - hullPts[index2].X, hullPts[index1].Y - hullPts[index2].Y);
                MyVector2D n = new MyVector2D(1, 0);
                double angle = MyVector2D.GetAngleBetweenTwoVectors(v, n);

                // try this rotation on all points
                double minX = double.PositiveInfinity;
                double minY = double.PositiveInfinity;
                double maxX = double.NegativeInfinity;
                double maxY = double.NegativeInfinity;

                for (int j = 0; j < hullPts.Count; j++)
                {
                    double x = hullPts[j].X * Math.Cos(angle) - hullPts[j].Y * Math.Sin(angle);
                    double y = hullPts[j].X * Math.Sin(angle) + hullPts[j].Y * Math.Cos(angle);

                    if (x > maxX)
                    {
                        maxX = x;
                    }
                    if (y > maxY)
                    {
                        maxY = y;
                    }
                    if (x < minX)
                    {
                        minX = x;
                    }
                    if (y < minY)
                    {
                        minY = y;
                    }
                }

                double area = Math.Abs(maxX - minX) * Math.Abs(maxY - minY);
                if (area < minArea)
                {
                    minArea = area;
                    minAreaAngle = angle;
                    minValues = new MyVector2D(minX, minY);
                    maxValues = new MyVector2D(maxX, maxY);
                    i1 = index1;
                    i2 = index2;
                }
            }

            // male orezani okraje - neni nezbytne, jen pro debug
            minValues.X += 0.01f;
            minValues.Y += 0.01f;
            maxValues.X += -0.01f;
            maxValues.Y += -0.01f;

            for (int i = 0; i < RawData.Length; i++)
            {
                double x = RawData[i].X * Math.Cos(minAreaAngle) - RawData[i].Y * Math.Sin(minAreaAngle);
                double y = RawData[i].X * Math.Sin(minAreaAngle) + RawData[i].Y * Math.Cos(minAreaAngle);

                if (
                    (x > minValues.X) && (x < maxValues.X) &&
                    (y > minValues.Y) && (y < maxValues.Y) &&
                    RawData[i].type == PixelType.NotMarked
                    //&& RawData[i].Z > Config.RegreseTloustka && RawData[i].Z < 0.2f
                    && RawData[i].Z < 0 && RawData[i].Z > -0.2f
                    )
                {
                    RawData[i].type = PixelType.Object;
                }
            }
        }

        public bool[] ConvexHullAlgorithmModified()
        {
            // get candidates to convex hull
            List<ConvexAlgorithmPoints> pts = new List<ConvexAlgorithmPoints>();

            for (int y = 0; y < Config.DepthImageHeight; y++)
            {
                for (int x = 0; x < Config.DepthImageWidth; x++)
                {
                    #region longIfs - optimalization - takes only pixels of table that are surrounded with different pixel type
                    if (RawData[PosFromCoor(x, y)].type == PixelType.Table)
                    {

                        bool left = false;
                        bool right = false;
                        bool up = false;
                        bool down = false;

                        if (x > 0)
                        {
                            if (RawData[PosFromCoor(x - 1, y)].type == PixelType.Table)
                            {
                                left = true;
                            }
                            else
                            {
                                left = false;
                            }
                        }
                        else
                        {
                            left = true;
                        }

                        if (x < Config.DepthImageWidth - 1)
                        {
                            if (RawData[PosFromCoor(x + 1, y)].type == PixelType.Table)
                            {
                                right = true;
                            }
                            else
                            {
                                right = false;
                            }
                        }
                        else
                        {
                            right = true;
                        }

                        if (y > 0)
                        {
                            if (RawData[PosFromCoor(x, y - 1)].type == PixelType.Table)
                            {
                                up = true;
                            }
                            else
                            {
                                up = false;
                            }
                        }
                        else
                        {
                            up = true;
                        }

                        if (y < Config.DepthImageHeight - 1)
                        {
                            if (RawData[PosFromCoor(x, y + 1)].type == PixelType.Table)
                            {
                                down = true;
                            }
                            else
                            {
                                down = false;
                            }
                        }
                        else
                        {
                            down = true;
                        }

                        if (!(left && right && up && down))
                        {
                            pts.Add(new ConvexAlgorithmPoints(ref RawData[PosFromCoor(x, y)], PosFromCoor(x, y)));
                        }

                        #endregion
                    }
                }
            }

            // make convex hull with algorithm
            List<ConvexAlgorithmPoints> hullPts = (List<ConvexAlgorithmPoints>)ConvexHull.MakeHull(pts);

            // make Minimal Bounding Rectangle and check, which points fall into it

            //TODO refactor
            int i1 = 0;
            int i2 = 0;
            double minArea = double.PositiveInfinity;
            double minAreaAngle = double.PositiveInfinity;
            MyVector2D maxValues = new MyVector2D(0, 0);
            MyVector2D minValues = new MyVector2D(0, 0);

            for (int i = 0; i < hullPts.Count; i++)
            {
                // z jakych bodu se bude delat vektor
                int index1 = i;
                int index2 = i + 1;

                // spojeni bodu na konci a zacatku listu
                if (index1 == (hullPts.Count - 1))
                {
                    index2 = 0;
                }

                // get vectors and angle
                MyVector2D v = new MyVector2D(hullPts[index1].X - hullPts[index2].X, hullPts[index1].Y - hullPts[index2].Y);
                MyVector2D n = new MyVector2D(1, 0);
                double angle = MyVector2D.GetAngleBetweenTwoVectors(v, n);

                // try this rotation on all points
                double minX = double.PositiveInfinity;
                double minY = double.PositiveInfinity;
                double maxX = double.NegativeInfinity;
                double maxY = double.NegativeInfinity;

                for (int j = 0; j < hullPts.Count; j++)
                {
                    double x = hullPts[j].X * Math.Cos(angle) - hullPts[j].Y * Math.Sin(angle);
                    double y = hullPts[j].X * Math.Sin(angle) + hullPts[j].Y * Math.Cos(angle);

                    if (x > maxX)
                    {
                        maxX = x;
                    }
                    if (y > maxY)
                    {
                        maxY = y;
                    }
                    if (x < minX)
                    {
                        minX = x;
                    }
                    if (y < minY)
                    {
                        minY = y;
                    }
                }

                double area = Math.Abs(maxX - minX) * Math.Abs(maxY - minY);
                if (area < minArea)
                {
                    minArea = area;
                    minAreaAngle = angle;
                    minValues = new MyVector2D(minX, minY);
                    maxValues = new MyVector2D(maxX, maxY);
                    i1 = index1;
                    i2 = index2;
                }
            }

            // male orezani okraje - neni nezbytne, jen pro debug
            minValues.X += 0.01f;
            minValues.Y += 0.01f;
            maxValues.X += -0.01f;
            maxValues.Y += -0.01f;

            bool[] resultBools = new bool[512*424];

            for (int i = 0; i < RawData.Length; i++)
            {
                double x = RawData[i].X * Math.Cos(minAreaAngle) - RawData[i].Y * Math.Sin(minAreaAngle);
                double y = RawData[i].X * Math.Sin(minAreaAngle) + RawData[i].Y * Math.Cos(minAreaAngle);

                if (
                    (x > minValues.X) && (x < maxValues.X) &&
                    (y > minValues.Y) && (y < maxValues.Y) &&
                    //RawData[i].type == PixelType.NotMarked &&
                    //&& RawData[i].Z > Config.RegreseTloustka && RawData[i].Z < 0.2f
                    RawData[i].Z < 0.2f && RawData[i].Z > -0.3f
                    )
                {
                    resultBools[i] = true;
                }
                else
                {
                    resultBools[i] = false;
                }
            }


            return resultBools;
        }

        public void LocateObjects()
        {
            List<ObjectLocationPoints> pointList = new List<ObjectLocationPoints>();

            for (int y = 0; y < Config.DepthImageHeight; y++)
            {
                for (int x = 0; x < Config.DepthImageWidth; x++)
                {
                    if (RawData[x + y * Config.DepthImageWidth].type == PixelType.Object)
                    {
                        pointList.Add(new ObjectLocationPoints(ref RawData[x + y * Config.DepthImageWidth], x, y));
                    }
                }
            }

            var points = pointList.ToArray();
            int[] parts = new int[points.Length];
            bool[] used = new bool[points.Length];

            List<int> partsSize = new List<int>();
            partsSize.Add(0); // empty for imaginary "zero" part - it starts from part 1

            int part = 0;
            int remaining = 0;
            int sum = 0;

            Queue<int> pointsToProcessForThisPart = new Queue<int>();

            while (true)
            {
                if (sum == points.Length) break;
                if (remaining == 0)
                {
                    part++;
                    remaining++;
                    sum++;
                    partsSize.Add(1);

                    for (int j = 0; j < points.Length; j++)
                    {
                        if (!used[j])
                        {
                            parts[j] = part;
                            used[j] = true;
                            pointsToProcessForThisPart.Enqueue(j);
                            break;
                        }
                    }
                }

                var point = pointsToProcessForThisPart.Dequeue();
                remaining--;

                for (int k = 0; k < points.Length; k++)
                {
                    if (!used[k])
                    {
                        if (Config.distanceOfObjectPoints > Math.Sqrt((((double)points[point].X - points[k].X) * ((double)points[point].X - points[k].X)) + (((double)points[point].Y - points[k].Y) * ((double)points[point].Y - points[k].Y))))
                        {
                            remaining++;
                            parts[k] = part;
                            sum++;
                            used[k] = true;
                            partsSize[part]++;
                            pointsToProcessForThisPart.Enqueue(k);
                        }
                    }
                }
            }

            partsSize.RemoveAt(0); // remove additional empty zero part

            //form.UpdateObjectPositions(partsSize);

        }

        private void GeneratePseudorandomTriangles()
        {
            int zacX = 0 + 102; // 100
            int konX = 512 - 102; // 400
            int zacY = 0 + 102; // 100
            int konY = 424 - 102; // 324
            int centerStep = 15; // 3
            int angleStep = 43; // degrees 7

            int size = 60;
            int rotation = 0;
            List<int> tempBodyTrojuhelniku = new List<int>();

            for (int y = zacY; y < konY; y += centerStep)
            {
                for (int x = zacX; x < konX; x += centerStep)
                {
                    Position2D center;
                    Position2D up;
                    Position2D right;


                    size = 30;
                    center = new Position2D(x, y);
                    up = new Position2D(x, y + size);
                    right = new Position2D(x + size, (int)(y));

                    up.RotateAroundPoint(rotation, center);
                    right.RotateAroundPoint(rotation, center);

                    tempBodyTrojuhelniku.Add(center.X + center.Y * Config.DepthImageWidth);
                    tempBodyTrojuhelniku.Add(up.X + up.Y * Config.DepthImageWidth);
                    tempBodyTrojuhelniku.Add(right.X + right.Y * Config.DepthImageWidth);



                    size = 60;
                    center = new Position2D(x, y);
                    up = new Position2D(x, y + size);
                    right = new Position2D(x + size, (int)(y));

                    up.RotateAroundPoint(rotation, center);
                    right.RotateAroundPoint(rotation, center);

                    tempBodyTrojuhelniku.Add(center.X + center.Y * Config.DepthImageWidth);
                    tempBodyTrojuhelniku.Add(up.X + up.Y * Config.DepthImageWidth);
                    tempBodyTrojuhelniku.Add(right.X + right.Y * Config.DepthImageWidth);


                    size = 98;
                    center = new Position2D(x, y);
                    up = new Position2D(x, y + size);
                    right = new Position2D(x + size, (int)(y));

                    up.RotateAroundPoint(rotation, center);
                    right.RotateAroundPoint(rotation, center);

                    tempBodyTrojuhelniku.Add(center.X + center.Y * Config.DepthImageWidth);
                    tempBodyTrojuhelniku.Add(up.X + up.Y * Config.DepthImageWidth);
                    tempBodyTrojuhelniku.Add(right.X + right.Y * Config.DepthImageWidth);


                    rotation += angleStep;
                    if (rotation >= 360) rotation -= 360;
                }
            }

            Config.tringlePointsForRANSAC = tempBodyTrojuhelniku.ToArray();
            Config.triangleCount = Config.tringlePointsForRANSAC.Length / 3;

        }


    }
}
