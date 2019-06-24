using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Threading.Tasks;
using ChessTracking.ImageProcessing.PipelineData;
using ChessTracking.ImageProcessing.PlaneAlgorithms;
using ChessTracking.Utils;
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
                BinarizationImage = ((Bitmap)binarizedImage.Convert<Rgb, byte>().Bitmap.Clone()).HorizontalFlip(),
                CannyImage = ((Bitmap)cannyDetectorImage.Convert<Rgb, byte>().Bitmap.Clone()).HorizontalFlip(),
                GrayImage = ((Bitmap)grayImage.Convert<Rgb, byte>().Bitmap.Clone()).HorizontalFlip(),
                MaskedColorImage = ((Bitmap)chessboardData.PlaneData.MaskedColorImageOfTable.Convert<Rgb, byte>().Bitmap.Clone()).HorizontalFlip()
            };

            var contractedPoints = LinesIntersections.GetIntersectionPointsOfTwoGroupsOfLines(linesTuple);

            var boardRepresentation = ChessboardFitting.ChessboardFittingAlgorithm(contractedPoints, chessboardData);
            
            return (boardRepresentation, snapshot);
        }

        private Image<Gray, byte> GetGrayImage(Image<Rgb, byte> colorImage)
        {
            return colorImage.Convert<Gray, Byte>();
        }

        private Image<Gray, byte> GetBinarizedImage(Image<Gray, byte> grayImage, bool otzuActive, int binaryThreshold)
        {
            var binarizedImg = new Image<Gray, byte>(grayImage.Width, grayImage.Height);

            if (otzuActive)
            {
                var computedOtzuThreshold = CustomOtsuBinarization.GetOtsuThreshold(grayImage.Bitmap, maskedValue: 255);
                CvInvoke.Threshold(grayImage, binarizedImg, computedOtzuThreshold, 255, ThresholdType.Binary);
            }
            else
            {
                CvInvoke.Threshold(grayImage, binarizedImg, binaryThreshold, 255, ThresholdType.Binary);
            }

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
    }
}
