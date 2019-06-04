using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using Accord.Math;
using Accord.Math.Geometry;
using ChessTracking.ImageProcessing.ChessboardAlgorithms;
using ChessTracking.ImageProcessing.PipelineData;
using ChessTracking.ImageProcessing.PipelineParts.General;
using ChessTracking.ImageProcessing.PipelineParts.StagesInterfaces;
using ChessTracking.ImageProcessing.PlaneAlgorithms;
using ChessTracking.MultithreadingMessages;
using Emgu.CV;
using Emgu.CV.CvEnum;
using Emgu.CV.Structure;
using MathNet.Spatial.Euclidean;
using Microsoft.Kinect;
using Point = Accord.Point;
using Point2D = MathNet.Spatial.Euclidean.Point2D;

namespace ChessTracking.ImageProcessing.PipelineParts.Stages
{
    class ChessboardLocalization : IChessboardLocalization
    {
        private Chessboard3DReprezentation BoardReprezentation { get; set; }
        private IRotateSpaceToChessboard RotationAlgorithm { get; }
        private IChessboardLocalizationAlgorithm ChessboardAlgorithm { get; }

        public ChessboardLocalization()
        {
            RotationAlgorithm = new RotateSpaceToChessboard();
            ChessboardAlgorithm = new ChessboardLocalizationAlgorithm();
        }

        public ChessboardTrackingCompleteData Calibrate(PlaneTrackingCompleteData planeData)
        {
            var chessboardData = new ChessboardTrackingCompleteData(planeData);

            BoardReprezentation = ChessboardAlgorithm.LocateChessboard(chessboardData);

            RotationAlgorithm.Rotate(BoardReprezentation, chessboardData.KinectData.CameraSpacePointsFromDepthData);

            return chessboardData;
        }

        public ChessboardTrackingCompleteData Track(PlaneTrackingCompleteData planeData)
        {
            var chessboardData = new ChessboardTrackingCompleteData(planeData);

            RotationAlgorithm.Rotate(BoardReprezentation, chessboardData.KinectData.CameraSpacePointsFromDepthData);

            chessboardData.ChessboardData.FieldSize = BoardReprezentation.FieldVector1.Magnitude();

            if (chessboardData.UserParameters.VisualisationType == VisualisationType.HighlightedChessboard)
                chessboardData.ResultData.VisualisationBitmap =
                    ReturnLocalizedChessboardWithTable(
                        chessboardData.PlaneData.ColorBitmap,
                        chessboardData.PlaneData.MaskOfTable,
                        chessboardData.KinectData.PointsFromColorToDepth,
                        chessboardData.KinectData.CameraSpacePointsFromDepthData,
                        BoardReprezentation.FieldVector1);

            return chessboardData;
        }

        private Bitmap ReturnLocalizedChessboardWithTable(Bitmap colorImg, bool[] resultBools,
            DepthSpacePoint[] pointsFromColorToDepth, CameraSpacePoint[] cameraSpacePointsFromDepthData, MyVector3DStruct magnitudeVector)
        {
            Bitmap bm = colorImg;

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
                        int pixelPostion = (y * 1920 + x);
                        int rgbPositon = pixelPostion * 3;

                        DepthSpacePoint point = pointsFromColorToDepth[pixelPostion];
                        int pointPosition = (int)point.X + (int)point.Y * 512;

                        if (float.IsInfinity(point.X) || point.X < 0 || point.Y < 0)
                        {
                            *(ptr + rgbPositon + 2) = 255;
                            *(ptr + rgbPositon + 1) = 255;
                            *(ptr + rgbPositon + 0) = 255;
                        }
                        else
                        {
                            int colorX = (int)point.X;
                            int colorY = (int)point.Y;

                            if (colorY < 424 && colorX < 512)
                            {
                                int colorImageIndex = ((512 * colorY) + colorX);

                                if (resultBools[colorImageIndex])
                                {
                                    if (!(float.IsInfinity(cameraSpacePointsFromDepthData[pointPosition].Z) ||
                                          float.IsNaN(cameraSpacePointsFromDepthData[pointPosition].Z))

                                        && cameraSpacePointsFromDepthData[pointPosition].X > 0
                                        && cameraSpacePointsFromDepthData[pointPosition].Y > 0
                                        && cameraSpacePointsFromDepthData[pointPosition].X < magnitudeVector.Magnitude() * 8
                                        && cameraSpacePointsFromDepthData[pointPosition].Y < magnitudeVector.Magnitude() * 8
                                    )
                                    {
                                    }
                                    else
                                    {
                                        *(ptr + rgbPositon + 2) = (byte)(*(ptr + rgbPositon + 2) * 0.8f);
                                        *(ptr + rgbPositon + 1) = (byte)(*(ptr + rgbPositon + 1) * 0.8f);


                                        var value = *(ptr + rgbPositon + 0);
                                        value += (byte)((255 - value) * 0.95f);
                                        *(ptr + rgbPositon + 0) = value; // R
                                    }
                                }
                                else
                                {
                                    *(ptr + rgbPositon + 2) = 255;
                                    *(ptr + rgbPositon + 1) = 255;
                                    *(ptr + rgbPositon + 0) = 255;
                                }
                            }

                        }
                    }
                }
            }
            bm.UnlockBits(bitmapData);


            return bm;
        }

    }
}
