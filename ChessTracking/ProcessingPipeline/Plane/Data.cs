using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Kinect;
using System.Drawing;
using System.Runtime.CompilerServices;
using MathNet.Numerics.LinearRegression;
using MathNet.Numerics.LinearAlgebra.Double;

namespace ChessTracking.ProcessingPipeline.Plane
{

    public class Data
    {
        /// <summary>
        /// Custom structure holding depth data from senzor, that are beeing processed
        /// </summary>
        public MyCameraSpacePoint[] DepthData;

        /// <summary>
        /// Triangles generated uniformly over image. Serve as seeds for RANSAC algorithm
        /// </summary>
        public List<RansacSeedTriangle> TriangleSeedsForRansac;

        /// <summary>
        /// Converts depth data from sensor to custom structure
        /// </summary>
        /// <param name="cameraSpacePoints">Camera space points from sensor</param>
        public Data(CameraSpacePoint[] cameraSpacePoints)
        {
            DepthData = new MyCameraSpacePoint[cameraSpacePoints.Length];

            for (int i = 0; i < cameraSpacePoints.Length; i++)
            {
                DepthData[i] = new MyCameraSpacePoint(ref cameraSpacePoints[i]);
            }
        }

        /// <summary>
        /// Marks (Cuts off) depths under minDepth and over maxDepth as invalid.
        /// Used for distances, where sensor cannot guarantee reliability of measurement, of
        /// for distance requirements of user
        /// </summary>
        /// <param name="minDepth">Minimal depth that is recognized as valid</param>
        /// <param name="maxDepth">Maximal depth that is recognized as valid</param>
        public void CutOffMinMaxDepth(float minDepth, float maxDepth)
        {
            // Uniform distribution of work into tasks
            int partsCount = PlaneLocalizationConfig.ParalelTasksCount;
            var tasks = new List<Task>();

            int dataLength = DepthData.Length;
            int step = dataLength / partsCount;
            int start = 0;
            int end = step;

            for (int i = 0; i < partsCount; i++)
            {
                var startClosure = start;
                var endClosure = end;

                var t = Task.Run(() => CutOffMinMaxDepthImplementation(minDepth, maxDepth, startClosure, endClosure));
                tasks.Add(t);

                start += step;
                end += step;
            }

            Task.WaitAll(tasks.ToArray());
        }

        /// <summary>
        /// Marks (Cuts off) depths under minDepth and over maxDepth in given range as invalid.
        /// Used for distances, where sensor cannot guarantee reliability of measurement, of
        /// for distance requirements of user
        /// </summary>
        /// <param name="minDepth">Minimal depth that is recognized as valid</param>
        /// <param name="maxDepth">Maximal depth that is recognized as valid</param>
        /// <param name="startIndex">Start index of array for processing</param>
        /// <param name="endIndex">Last index of array for processing</param>
        private void CutOffMinMaxDepthImplementation(float minDepth, float maxDepth, int startIndex, int endIndex)
        {
            for (int i = startIndex; i < endIndex; i++)
            {
                if ((DepthData[i].Z > maxDepth) || (DepthData[i].Z < minDepth))
                {
                    DepthData[i].Z = 0f;
                    DepthData[i].Type = PixelType.Invalid;
                }
            }
        }
        
        /// <summary>
        /// Performs RANSAC algorithm
        /// </summary>
        public void Ransac()
        {
            if (TriangleSeedsForRansac == null)
                GenerateTriangleSeedsInImage();

            // Uniform distribution of work into tasks
            int partsCount = PlaneLocalizationConfig.ParalelTasksCount;
            var tasks = new List<Task>();

            int trianglesCount = TriangleSeedsForRansac.Count;
            int step = trianglesCount / partsCount;
            int start = 0;
            int end = step;

            for (int i = 0; i < partsCount; i++)
            {
                int startClosure = start;
                int endClosure = end;

                var t = Task.Run(() => RansacImplementation(startClosure, endClosure));
                tasks.Add(t);

                start += step;
                end += step;
            }

            Task.WaitAll(tasks.ToArray());

            MarkTablePixels();
        }

        /// <summary>
        /// Mark pixels that are near plane as part of that plane
        /// </summary>
        private void MarkTablePixels()
        {
            for (int i = 0; i < DepthData.Length; i++)
            {
                if (DepthData[i].Type == PixelType.Invalid) continue;

                float pointX = DepthData[i].X;
                float pointY = DepthData[i].Y;
                float pointZ = DepthData[i].Z;
                
                float eucledidianDistancePointToPlane = 
                    (float)
                    (Math.Abs(
                         PlaneLocalizationConfig.FinalNormal.x * pointX + 
                         PlaneLocalizationConfig.FinalNormal.y * pointY + 
                         PlaneLocalizationConfig.FinalNormal.z * pointZ + 
                         PlaneLocalizationConfig.FinalD
                         ) 
                     / 
                     Math.Sqrt(
                         PlaneLocalizationConfig.FinalNormal.x * PlaneLocalizationConfig.FinalNormal.x +
                         PlaneLocalizationConfig.FinalNormal.y * PlaneLocalizationConfig.FinalNormal.y +
                         PlaneLocalizationConfig.FinalNormal.z * PlaneLocalizationConfig.FinalNormal.z
                         )
                     );

                if (eucledidianDistancePointToPlane < PlaneLocalizationConfig.RansacThickness)
                {
                    DepthData[i].Type = PixelType.Table;
                }
            }
        }

        /// <summary>
        /// Performs RANSAC algorithm
        /// </summary>
        private void RansacImplementation(int zacatek, int konec)
        {
            int GridSizeForTestingPlaneFit = PlaneLocalizationConfig.GridSizeForTestingPlaneFit;
            float threshold = PlaneLocalizationConfig.RansacThickness;

            double finalD = 0;
            MyVector3D finalNormal = new MyVector3D(0, 0, 0);
            int max = 0;

            for (int i = zacatek; i < konec; i++)
            {
                // get pseudorandom predefinied triangle points
                int a = TriangleSeedsForRansac[i].FirstVertexIndex;
                int b = TriangleSeedsForRansac[i].SecondVertexIndex;
                int c = TriangleSeedsForRansac[i].ThirdVertexIndex;

                // are those points even valid?
                if (
                    DepthData[a].Type == PixelType.Invalid ||
                    DepthData[b].Type == PixelType.Invalid ||
                    DepthData[c].Type == PixelType.Invalid
                    ) continue;

                // get plane analytical equation (ax + by + cz + c)
                var normal = MyVector3D.CrossProduct(
                    new MyVector3D(DepthData[a].X - DepthData[b].X, DepthData[a].Y - DepthData[b].Y, DepthData[a].Z - DepthData[b].Z),
                    new MyVector3D(DepthData[a].X - DepthData[c].X, DepthData[a].Y - DepthData[c].Y, DepthData[a].Z - DepthData[c].Z)
                    );
                double d = -(normal.x * DepthData[a].X + normal.y * DepthData[a].Y + normal.z * DepthData[a].Z);

                // Get number of points that belong to this plane
                int numberOfTablePoints = 0;
                for (int y = 0; y < PlaneLocalizationConfig.DepthImageHeight; y += GridSizeForTestingPlaneFit)
                {
                    for (int x = 0; x < PlaneLocalizationConfig.DepthImageHeight; x += GridSizeForTestingPlaneFit)
                    {
                        if (DepthData[x + y * PlaneLocalizationConfig.DepthImageWidth].Type == PixelType.Invalid) continue;

                        float pointX = DepthData[x + y * PlaneLocalizationConfig.DepthImageWidth].X;
                        float pointY = DepthData[x + y * PlaneLocalizationConfig.DepthImageWidth].Y;
                        float pointZ = DepthData[x + y * PlaneLocalizationConfig.DepthImageWidth].Z;

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
                    if (max > PlaneLocalizationConfig.MaxObservedPlaneSize)
                    {
                        lock (PlaneLocalizationConfig.LockingObj)
                        {
                            PlaneLocalizationConfig.MaxObservedPlaneSize = max;
                            PlaneLocalizationConfig.FinalD = finalD;
                            PlaneLocalizationConfig.FinalNormal = finalNormal;
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
            int[] rootOvnershipMarkers = new int[DepthData.Length];

            for (int y = 1; y < PlaneLocalizationConfig.DepthImageHeight; y += 20)
            {
                for (int x = 1; x < PlaneLocalizationConfig.DepthImageWidth; x += 20)
                {
                    // is table pixel and doesnt belong to any area yet? -> perform bfs from this point
                    if ((DepthData[PosFromCoor(x, y)].Type == PixelType.Table) && (rootOvnershipMarkers[PosFromCoor(x, y)] == 0))
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

                            if ((DepthData[point.position].Type == PixelType.Table) && (rootOvnershipMarkers[point.position] == 0))
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

                                if (point.y < PlaneLocalizationConfig.DepthImageHeight - 1)
                                {
                                    points.Enqueue(new Point(point.x, point.y + 1));
                                }

                                if (point.x < PlaneLocalizationConfig.DepthImageWidth - 1)
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
            for (int i = 0; i < DepthData.Length; i++)
            {
                if ((rootOvnershipMarkers[i] != maxPosition) && DepthData[i].Type == PixelType.Table)
                {
                    DepthData[i].Type = PixelType.NotMarked;
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
                position = x + y * PlaneLocalizationConfig.DepthImageWidth;
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
            return y * PlaneLocalizationConfig.DepthImageWidth + x;
        }

        public void LinearRegression()
        {
            int step = PlaneLocalizationConfig.RegressionIsEvaluatedOnEveryNthTablePixel;

            int NumberOfEvaluatedTablePixels = 0;
            for (int i = 0; i < DepthData.Length; i += step)
            {
                if (DepthData[i].Type == PixelType.Table)
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
            for (int i = 0; i < DepthData.Length; i += step)
            {
                if (DepthData[i].Type == PixelType.Table)
                {
                    LAx[counter] = DepthData[i].X;
                    LAy[counter] = DepthData[i].Y;
                    LAvalue[counter] = DepthData[i].Z;

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
            for (int i = 0; i < DepthData.Length; i++)
            {
                if (DepthData[i].Type == PixelType.Table)
                {
                    DepthData[i].Type = PixelType.NotMarked;
                }
            }

            // Mark pixels as table, based on distance from computed plane 
            for (int y = 0; y < PlaneLocalizationConfig.DepthImageHeight; y++)
            {
                for (int x = 0; x < PlaneLocalizationConfig.DepthImageWidth; x++)
                {
                    if (DepthData[x + y * PlaneLocalizationConfig.DepthImageWidth].Type == PixelType.Invalid)
                    {
                        continue;
                    }

                    float pointX = DepthData[x + y * PlaneLocalizationConfig.DepthImageWidth].X;
                    float pointY = DepthData[x + y * PlaneLocalizationConfig.DepthImageWidth].Y;
                    float pointZ = DepthData[x + y * PlaneLocalizationConfig.DepthImageWidth].Z;

                    float distance = (float)(Math.Abs(normal.x * pointX + normal.y * pointY + normal.z * pointZ + d) / Math.Sqrt(normal.x * normal.x + normal.y * normal.y + normal.z * normal.z));

                    if (distance < PlaneLocalizationConfig.RegreseTloustka)
                    {
                        DepthData[PosFromCoor(x, y)].Type = PixelType.Table;
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
        public Tuple<double,double> RotationTo2DModified()
        {
            // get angles to vector(0,0,1)
            // angle in plane x=0
            double dotx = PlaneLocalizationConfig.FinalNormal.y * 0 + PlaneLocalizationConfig.FinalNormal.z * 1;
            double detx = PlaneLocalizationConfig.FinalNormal.y * 1 - PlaneLocalizationConfig.FinalNormal.z * 0;
            double angleX = Math.Atan2(detx, dotx);

            // angle in plane y=0
            double doty = PlaneLocalizationConfig.FinalNormal.x * 0 + PlaneLocalizationConfig.FinalNormal.z * 1;
            double dety = PlaneLocalizationConfig.FinalNormal.x * 1 - PlaneLocalizationConfig.FinalNormal.z * 0;
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
            for (int i = 0; i < DepthData.Length; i++)
            {
                if (DepthData[i].Type == PixelType.Table)
                {
                    count++;
                    sumX += DepthData[i].X;
                    sumY += DepthData[i].Y;
                    sumZ += DepthData[i].Z;
                }
            }
            MyVector3D tableCenter = new MyVector3D((sumX / count), (sumY / count), (sumZ / count));

            // Translation of origin to the center of the table 
            // and rotating them to vector(0,0,1)
            for (int i = 0; i < DepthData.Length; i++)
            {
                DepthData[i].X = (float)(DepthData[i].X - tableCenter.x);
                DepthData[i].Y = (float)(DepthData[i].Y - tableCenter.y);
                DepthData[i].Z = (float)(DepthData[i].Z - tableCenter.z);

                float newX = (float)(DepthData[i].X * m11 + DepthData[i].Y * m12 + DepthData[i].Z * m13);
                float newY = (float)(DepthData[i].X * m21 + DepthData[i].Y * m22 + DepthData[i].Z * m23);
                float newZ = (float)(DepthData[i].X * m31 + DepthData[i].Y * m32 + DepthData[i].Z * m33);


                DepthData[i].X = newX;
                DepthData[i].Y = newY;
                DepthData[i].Z = newZ;
            }
            return new Tuple<double,double>(angleX, angleY);
        }
        

        public bool[] ConvexHullAlgorithmModified()
        {
            // get candidates to convex hull
            List<ConvexHullPoints> pts = new List<ConvexHullPoints>();

            for (int y = 0; y < PlaneLocalizationConfig.DepthImageHeight; y++)
            {
                for (int x = 0; x < PlaneLocalizationConfig.DepthImageWidth; x++)
                {
                    #region longIfs - optimalization - takes only pixels of table that are surrounded with different pixel type
                    if (DepthData[PosFromCoor(x, y)].Type == PixelType.Table)
                    {

                        bool left = false;
                        bool right = false;
                        bool up = false;
                        bool down = false;

                        if (x > 0)
                        {
                            if (DepthData[PosFromCoor(x - 1, y)].Type == PixelType.Table)
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

                        if (x < PlaneLocalizationConfig.DepthImageWidth - 1)
                        {
                            if (DepthData[PosFromCoor(x + 1, y)].Type == PixelType.Table)
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
                            if (DepthData[PosFromCoor(x, y - 1)].Type == PixelType.Table)
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

                        if (y < PlaneLocalizationConfig.DepthImageHeight - 1)
                        {
                            if (DepthData[PosFromCoor(x, y + 1)].Type == PixelType.Table)
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
                            pts.Add(new ConvexHullPoints(ref DepthData[PosFromCoor(x, y)], PosFromCoor(x, y)));
                        }

                        #endregion
                    }
                }
            }

            // make convex hull with algorithm
            List<ConvexHullPoints> hullPts = (List<ConvexHullPoints>)ConvexHull.MakeHull(pts);

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

            for (int i = 0; i < DepthData.Length; i++)
            {
                double x = DepthData[i].X * Math.Cos(minAreaAngle) - DepthData[i].Y * Math.Sin(minAreaAngle);
                double y = DepthData[i].X * Math.Sin(minAreaAngle) + DepthData[i].Y * Math.Cos(minAreaAngle);

                if (
                    (x > minValues.X) && (x < maxValues.X) &&
                    (y > minValues.Y) && (y < maxValues.Y) &&
                    //DepthData[i].type == PixelType.NotMarked &&
                    //&& DepthData[i].Z > PlaneLocalizationConfig.RegreseTloustka && DepthData[i].Z < 0.2f
                    DepthData[i].Z < 0.2f && DepthData[i].Z > -0.3f
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

       /// <summary>
       /// Generates uniformly distributed right triangles into image bounds
       /// </summary>
        private void GenerateTriangleSeedsInImage()
        {
            TriangleSeedsForRansac = new List<RansacSeedTriangle>();

            int offsetFromImageBorders = 102;

            // intervals to move in
            int zacX = 0 + offsetFromImageBorders; 
            int konX = PlaneLocalizationConfig.DepthImageWidth - offsetFromImageBorders; 
            int zacY = 0 + offsetFromImageBorders; 
            int konY = PlaneLocalizationConfig.DepthImageHeight - offsetFromImageBorders;

            // generation steps
            int positionStep = 15;
            int angleStep = 43;

            int currentRotation = 0;
            
            // move in discrete steps over image
            // and generate triangles with various sizes and rotations
            for (int y = zacY; y < konY; y += positionStep)
            {
                for (int x = zacX; x < konX; x += positionStep)
                {
                    TriangleSeedsForRansac.Add(GenerateTriangle(x,y,30,currentRotation));
                    TriangleSeedsForRansac.Add(GenerateTriangle(x, y, 60, currentRotation));
                    TriangleSeedsForRansac.Add(GenerateTriangle(x, y, 98, currentRotation));
                    
                    // reset rotation if overflows
                    currentRotation += angleStep;
                    if (currentRotation >= 360) currentRotation -= 360;
                }
            }

        }

        /// <summary>
        /// Generates right triangle in x,y position with given size and rotation
        /// </summary>
        /// <returns>Generated triangle</returns>
        private RansacSeedTriangle GenerateTriangle(int x, int y, int size, int rotation)
        {
            var center = new Position2D(x, y);
            var up = new Position2D(x, y + size);
            var right = new Position2D(x + size, (int)(y));

            up.RotateAroundPoint(rotation, center);
            right.RotateAroundPoint(rotation, center);

            return new RansacSeedTriangle(
                center.X + center.Y * PlaneLocalizationConfig.DepthImageWidth,
                up.X + up.Y * PlaneLocalizationConfig.DepthImageWidth,
                right.X + right.Y * PlaneLocalizationConfig.DepthImageWidth
                );
        }


    }
}
