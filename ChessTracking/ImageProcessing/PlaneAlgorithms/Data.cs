using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using ChessTracking.Utils;
using MathNet.Numerics.LinearAlgebra.Double;
using MathNet.Numerics.LinearRegression;
using Microsoft.Kinect;

namespace ChessTracking.ImageProcessing.PlaneAlgorithms
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
                TriangleSeedsForRansac = TrianglesGenerator.GenerateTriangleSeedsInImage();

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

                        float distancePointPlane =
                            (float)(
                                    Math.Abs(normal.x * pointX + normal.y * pointY + normal.z * pointZ + d) /
                                    Math.Sqrt(normal.x * normal.x + normal.y * normal.y + normal.z * normal.z)
                                   );

                        if (distancePointPlane < threshold)
                        {
                            numberOfTablePoints++;
                        }
                    }
                }

                // discrimination of planes with wierd angles (like kinect is upside down)
                if ((normal.y > 0 && normal.z > 0) || (normal.y < 0 && normal.z < 0))
                {
                    numberOfTablePoints /= 3;
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
        /// Selects largest continuous area marked as table
        /// Permorms it throught BFS from pseudorandom points
        /// </summary>
        public void LargestTableArea()
        {
            List<Root> roots = new List<Root> {new Root(0, 0, 0)}; // empty root as padding for forbidden value 0
            // array recording which pixel belongs to which root
            int[] rootOwnershipMarkersOnIndividualPixels = new int[DepthData.Length];

            // roots are seeded on every x-th pixel to speedup computation
            int rootSeedingSkipValue = 20;

            // for each possible root position (forming a grid)
            for (int y = 1; y < PlaneLocalizationConfig.DepthImageHeight; y += rootSeedingSkipValue)
            {
                for (int x = 1; x < PlaneLocalizationConfig.DepthImageWidth; x += rootSeedingSkipValue)
                {
                    // is table pixel and doesnt belong to any area yet? -> perform bfs from this point
                    if ((DepthData[PosFromCoor(x, y)].Type == PixelType.Table) && (rootOwnershipMarkersOnIndividualPixels[PosFromCoor(x, y)] == 0))
                    {
                        int position = PosFromCoor(x, y);
                        int areaNumber = roots.Count;

                        // add new root
                        roots.Add(new Root(areaNumber, x, y));

                        Queue<Point> points = new Queue<Point>();

                        // mark point
                        points.Enqueue(new Point(x, y));
                        Root root = roots[areaNumber];
                        root.count++;

                        // perform bfs spreading
                        while (points.Count != 0)
                        {
                            Point point = points.Dequeue();

                            if ((DepthData[point.position].Type == PixelType.Table) && (rootOwnershipMarkersOnIndividualPixels[point.position] == 0))
                            {
                                root.count++;
                                rootOwnershipMarkersOnIndividualPixels[point.position] = areaNumber;
                                
                                if (point.y > 0)
                                    points.Enqueue(new Point(point.x, point.y - 1));

                                if (point.x > 0)
                                    points.Enqueue(new Point(point.x - 1, point.y));

                                if (point.y < PlaneLocalizationConfig.DepthImageHeight - 1)
                                    points.Enqueue(new Point(point.x, point.y + 1));

                                if (point.x < PlaneLocalizationConfig.DepthImageWidth - 1)
                                    points.Enqueue(new Point(point.x + 1, point.y));
                            }
                        }

                    }
                }
            }

            // Finds largest area
            int max = 0;
            int maxPosition = 0;
            for (int i = 0; i < roots.Count; i++)
                if (roots[i].count > max)
                {
                    max = roots[i].count;
                    maxPosition = i;
                }

            // Invalidate all table pixels that doesnt belong to the largest area
            for (int i = 0; i < DepthData.Length; i++)
                if ((rootOwnershipMarkersOnIndividualPixels[i] != maxPosition) && DepthData[i].Type == PixelType.Table)
                    DepthData[i].Type = PixelType.NotMarked;
        }
        
        /// <summary>
        /// Performs linear regression that is evaluated on every x-th pixel
        /// </summary>
        public void LinearRegression()
        {
            int step = PlaneLocalizationConfig.RegressionIsEvaluatedOnEveryNthTablePixel;

            // count how many evaluated pixels are valid
            int numberOfEvaluatedTablePixels = 0;
            for (int i = 0; i < DepthData.Length; i += step)
                if (DepthData[i].Type == PixelType.Table)
                    numberOfEvaluatedTablePixels++;

            if (numberOfEvaluatedTablePixels == 0) return;

            // allocate arrays for values for regression
            double[] lAx = new double[numberOfEvaluatedTablePixels];
            double[] lAy = new double[numberOfEvaluatedTablePixels];
            double[] lAvalue = new double[numberOfEvaluatedTablePixels];

            double[] ones = new double[numberOfEvaluatedTablePixels];

            // temporary - hold position in upper arrays
            int counter = 0;

            // fill data to the arrays
            for (int i = 0; i < numberOfEvaluatedTablePixels; i++)
            {
                ones[i] = 1;
            }
            for (int i = 0; i < DepthData.Length; i += step)
            {
                if (DepthData[i].Type == PixelType.Table)
                {
                    lAx[counter] = DepthData[i].X;
                    lAy[counter] = DepthData[i].Y;
                    lAvalue[counter] = DepthData[i].Z;

                    counter++;
                }
            }

            // library regression solver
            var X = DenseMatrix.OfColumnArrays(ones, lAx, lAy);
            var YY = new DenseVector(lAvalue);
            
            double[][] abc = new double[3][];
            abc[0] = ones;
            abc[1] = lAx;
            abc[2] = lAy;

            var p = MultipleRegression.QR((MathNet.Numerics.LinearAlgebra.Matrix<double>)X, YY);

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
                if (DepthData[i].Type == PixelType.Table)
                    DepthData[i].Type = PixelType.NotMarked;

            // Mark pixels as table, based on distance from computed plane 
            for (int y = 0; y < PlaneLocalizationConfig.DepthImageHeight; y++)
            {
                for (int x = 0; x < PlaneLocalizationConfig.DepthImageWidth; x++)
                {
                    if (DepthData[x + y * PlaneLocalizationConfig.DepthImageWidth].Type == PixelType.Invalid)
                        continue;

                    float pointX = DepthData[x + y * PlaneLocalizationConfig.DepthImageWidth].X;
                    float pointY = DepthData[x + y * PlaneLocalizationConfig.DepthImageWidth].Y;
                    float pointZ = DepthData[x + y * PlaneLocalizationConfig.DepthImageWidth].Z;

                    float distance = (float)(Math.Abs(normal.x * pointX + normal.y * pointY + normal.z * pointZ + d) / 
                                             Math.Sqrt(normal.x * normal.x + normal.y * normal.y + normal.z * normal.z));

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
        
        /// <summary>
        /// Computes table as minimal bounding rectangle over table pixels and makes a mask of table and table items
        /// </summary>
        public bool[] ConvexHullAlgorithm()
        {
            // get candidates to convex hull -> those marked as table points that neighbors with different type of point
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
                                left = true;
                        }
                        else
                            left = true;

                        if (x < PlaneLocalizationConfig.DepthImageWidth - 1)
                        {
                            if (DepthData[PosFromCoor(x + 1, y)].Type == PixelType.Table)
                                right = true;
                        }
                        else
                            right = true;

                        if (y > 0)
                        {
                            if (DepthData[PosFromCoor(x, y - 1)].Type == PixelType.Table)
                                up = true;
                        }
                        else
                            up = true;

                        if (y < PlaneLocalizationConfig.DepthImageHeight - 1)
                        {
                            if (DepthData[PosFromCoor(x, y + 1)].Type == PixelType.Table)
                                down = true;
                        }
                        else
                            down = true;

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

            // make Minimal Bounding Rectangle using one edge of hull at a time
            double minArea = double.PositiveInfinity;
            double minAreaAngle = double.PositiveInfinity;
            MyVector2D maxValues = new MyVector2D(0, 0);
            MyVector2D minValues = new MyVector2D(0, 0);

            for (int i = 0; i < hullPts.Count; i++)
            {
                // from which points will be the edge
                int index1 = i;
                int index2 = i + 1;

                // special case of overflowing index
                if (index1 == (hullPts.Count - 1))
                    index2 = 0;

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
                        maxX = x;
                    if (y > maxY)
                        maxY = y;
                    if (x < minX)
                        minX = x;
                    if (y < minY)
                        minY = y;
                }

                // compute area of rectangle
                double area = Math.Abs(maxX - minX) * Math.Abs(maxY - minY);
                if (area < minArea)
                {
                    minArea = area;
                    minAreaAngle = angle;
                    minValues = new MyVector2D(minX, minY);
                    maxValues = new MyVector2D(maxX, maxY);
                }
            }

            // clipping of edges - for small corrections of table edge error - not necessary
            minValues.X += 0.01f;
            minValues.Y += 0.01f;
            maxValues.X += -0.01f;
            maxValues.Y += -0.01f;

            // compute mask for given minimal bounding rectangle and some height limits
            bool[] resultingMaskOfTable = new bool[512*424];

            for (int i = 0; i < DepthData.Length; i++)
            {
                double x = DepthData[i].X * Math.Cos(minAreaAngle) - DepthData[i].Y * Math.Sin(minAreaAngle);
                double y = DepthData[i].X * Math.Sin(minAreaAngle) + DepthData[i].Y * Math.Cos(minAreaAngle);

                if (
                    (x > minValues.X) && (x < maxValues.X) &&
                    (y > minValues.Y) && (y < maxValues.Y) &&
                    DepthData[i].Z < 0.2f && DepthData[i].Z > -0.3f
                    )
                    resultingMaskOfTable[i] = true;
                else
                    resultingMaskOfTable[i] = false;
            }


            return resultingMaskOfTable;
        }

        /// <summary>
        /// Get linear position in picture from 2D coordinates
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static int PosFromCoor(int x, int y)
        {
            return y * PlaneLocalizationConfig.DepthImageWidth + x;
        }
    }
}
