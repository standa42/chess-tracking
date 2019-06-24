using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;
using ChessTracking.ImageProcessing.PipelineData;
using ChessTracking.ImageProcessing.PlaneAlgorithms;
using Emgu.CV;
using Emgu.CV.CvEnum;
using Emgu.CV.Structure;
using Microsoft.Kinect;
using Point2D = MathNet.Spatial.Euclidean.Point2D;

namespace ChessTracking.ImageProcessing.ChessboardAlgorithms
{
    class ChessboardLocalizationAlgorithm : IChessboardLocalizationAlgorithm
    {
        public (Chessboard3DReprezentation boardReprezentation, SceneCalibrationSnapshot snapshot) LocateChessboard(ChessboardTrackingCompleteData chessboardData)
        {
            var grayImage = GetGrayImage(chessboardData.PlaneData.MaskedColorImageOfTable);
            var binarizedImage = GetBinarizedImage(grayImage, chessboardData.UserParameters.OtzuActiveInBinarization, chessboardData.UserParameters.BinarizationThreshold);
            var cannyDetectorImage = ApplyCannyDetector(binarizedImage);

            var linesTuple = GetFilteredHoughLines(cannyDetectorImage);

            var snapshot = new SceneCalibrationSnapshot()
            {
                BinarizationImage = (Bitmap)binarizedImage.Convert<Rgb, byte>().Bitmap.Clone(),
                CannyImage = (Bitmap)cannyDetectorImage.Convert<Rgb, byte>().Bitmap.Clone(),
                GrayImage = (Bitmap)grayImage.Convert<Rgb, byte>().Bitmap.Clone(),
                MaskedColorImage = (Bitmap)chessboardData.PlaneData.MaskedColorImageOfTable.Convert<Rgb, byte>().Bitmap.Clone()
            };

            var contractedPoints = LinesIntersections.GetIntersectionPointsOfTwoGroupsOfLines(linesTuple);

            var boardRepresentation = ChessboardFittingAlgorithm(contractedPoints, chessboardData);
            
            return (boardRepresentation, snapshot);
        }

        private Image<Gray, byte> GetGrayImage(Image<Rgb, byte> colorImage)
        {
            return colorImage.Convert<Gray, Byte>();
        }

        private Image<Gray, byte> GetBinarizedImage(Image<Gray, byte> grayImage, bool otzuActive, int binaryThreshold)
        {
            var binarizedImg = new Image<Gray, byte>(grayImage.Width, grayImage.Height);

            var threshType = otzuActive ? ThresholdType.Binary | ThresholdType.Otsu : ThresholdType.Binary;
            CvInvoke.Threshold(grayImage, binarizedImg, binaryThreshold, 255, threshType);

            return binarizedImg;
        }

        private Image<Gray, Byte> ApplyCannyDetector(Image<Gray, byte> image)
        {
            return image.Canny(700, 1400, 5, true).SmoothGaussian(3).ThresholdBinary(new Gray(50), new Gray(255));
        }

        /// <summary>
        /// Extracts 2 groups of lines from given image
        /// -> 2 stages of hough transform to get better result lines
        /// -> lines are filtered to two larges groups based on angle in image
        /// </summary>
        /// <param name="image"></param>
        /// <returns></returns>
        private Tuple<LineSegment2D[], LineSegment2D[]> GetFilteredHoughLines(Image<Gray, byte> image)
        {
            // first stage hough transform
            var lines = image.HoughLinesBinary(
                0.8f,  //Distance resolution in pixel-related units
                Math.PI / 1500, //Angle resolution measured in radians.
                220, //threshold
                100, //min Line width (90)
                35 //gap between lines
            )[0];

            // render image with lines from first stage
            Image<Bgr, Byte> drawnEdges = new Image<Bgr, Byte>(new Size(image.Width, image.Height));

            foreach (LineSegment2D line in lines)
                CvInvoke.Line(drawnEdges, line.P1, line.P2, new Bgr(Color.White).MCvScalar, 1);

            // apply second hough transform
            var lines2 = drawnEdges.Convert<Gray, byte>().HoughLinesBinary(
                0.8f,
                Math.PI / 1500,
                50,
                100,
                10
            )[0];

            return LinesIntoGroups.FilterLinesBasedOnAngle(lines2, 25);
        }
        

        private Chessboard3DReprezentation ChessboardFittingAlgorithm(List<Point2D> contractedPoints, ChessboardTrackingCompleteData chessboardData)
        {

            List<CameraSpacePoint> contractedPointsCsp = new List<CameraSpacePoint>();

            foreach (var contractedPoint in contractedPoints)
            {
                var depthReference =
                    chessboardData.KinectData.PointsFromColorToDepth[(int)contractedPoint.X + (int)contractedPoint.Y * 1920];
                if (!float.IsInfinity(depthReference.X))
                {
                    var csp = chessboardData.KinectData.CameraSpacePointsFromDepthData[
                        (int)depthReference.X + (int)depthReference.Y * 512];
                    contractedPointsCsp.Add(csp);
                }
            }



            var contractedPointsCspStruct =
                contractedPointsCsp.Select(x => new MyVector3DStruct(x.X, x.Y, x.Z)).ToArray();

            double lowestError = double.MaxValue;
            Chessboard3DReprezentation BoardRepresentation = null;
            //int eliminator = 0;
            Parallel.ForEach( contractedPointsCspStruct/*.Where(x => (eliminator++) % 2 == 0)*/,csp =>
            {
                // take _ nearest neighbors
                var neighbors = contractedPointsCspStruct.OrderBy(
                    (MyVector3DStruct x) =>
                    {
                        return
                            Math.Sqrt(
                                (x.x - csp.x) * (x.x - csp.x) + (x.y - csp.y) * (x.y - csp.y) +
                                (x.z - csp.z) * (x.z - csp.z)
                            );
                    }).Take(7).ToArray();

                // take all pairs
                for (int i = 0; i < neighbors.Length; i++)
                {
                    for (int j = i + 1; j < neighbors.Length; j++)
                    {
                        var first = neighbors[i];
                        var second = neighbors[j];

                        var firstPoint = new MyVector3DStruct(first.x, first.y, first.z);
                        var secondPoint = new MyVector3DStruct(second.x, second.y, second.z);

                        // st. point + 2 vectors
                        var cspPoint = new MyVector3DStruct(csp.x, csp.y, csp.z);
                        var firstVector = MyVector3DStruct.Difference(ref firstPoint, ref cspPoint);
                        var secondVector = MyVector3DStruct.Difference(ref secondPoint, ref cspPoint);

                        // perpendicularity check
                        double angleBetweenVectors =
                            MyVector3DStruct.AngleInDeg(ref firstVector, ref secondVector);
                        if (!(angleBetweenVectors < 91.4 && angleBetweenVectors > 88.6))
                        {
                            break;
                        }

                        // length check
                        double ratio = firstVector.Magnitude() / secondVector.Magnitude();
                        if (!(ratio > 0.85f && ratio < 1.15f))
                        {
                            break;
                        }

                        // ensure right orientation
                        if (MyVector3DStruct.CrossProduct(ref firstVector, ref secondVector).z < 0)
                        {
                            var temp = firstVector;
                            firstVector = secondVector;
                            secondVector = temp;
                        }

                        // length normalization
                        double averageLength = (firstVector.Magnitude() + secondVector.Magnitude()) / 2;

                        firstVector =
                            MyVector3DStruct.MultiplyByNumber(MyVector3DStruct.Normalize(ref firstVector),
                                averageLength);
                        secondVector =
                            MyVector3DStruct.MultiplyByNumber(MyVector3DStruct.Normalize(ref secondVector),
                                averageLength);

                        var negatedFirstVector = MyVector3DStruct.Negate(ref firstVector);
                        var negatedSecondVector = MyVector3DStruct.Negate(ref secondVector);

                        // locate all possible starting points 
                        for (int f = 0; f < 9; f++)
                        {
                            for (int s = 0; s < 9; s++)
                            {
                                var startingPoint =
                                    MyVector3DStruct.Addition(
                                        MyVector3DStruct.Addition(
                                            MyVector3DStruct.MultiplyByNumber(negatedFirstVector, f),
                                            MyVector3DStruct.MultiplyByNumber(negatedSecondVector, s)
                                        )
                                        ,
                                        cspPoint
                                    );

                                double currentError = 0;

                                // generate all possible chessboards for given starting point
                                for (int ff = 0; ff < 9; ff++)
                                {
                                    for (int ss = 0; ss < 9; ss++)
                                    {
                                        var currentPoint = MyVector3DStruct.Addition(
                                            startingPoint,
                                            MyVector3DStruct.Addition(
                                                MyVector3DStruct.MultiplyByNumber(firstVector, ff),
                                                MyVector3DStruct.MultiplyByNumber(secondVector, ss)
                                            )
                                        );

                                        var closestPointDistance = contractedPointsCspStruct.Min(x =>
                                            MyVector3DStruct.Distance(ref currentPoint,
                                                new MyVector3DStruct(x.x, x.y, x.z)));

                                        closestPointDistance =
                                            closestPointDistance > 0.01 ? 0.01 : closestPointDistance;

                                        currentError += closestPointDistance;
                                    }
                                }

                                if (currentError < lowestError)
                                {
                                    lowestError = currentError;
                                    BoardRepresentation = new Chessboard3DReprezentation(startingPoint, firstVector, secondVector);
                                }
                            }
                        }
                    }
                }
            });

            return BoardRepresentation;
        }
        
        

        
    }
}
