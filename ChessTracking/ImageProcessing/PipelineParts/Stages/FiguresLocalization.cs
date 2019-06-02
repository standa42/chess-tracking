using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using ChessTracking.ImageProcessing.FiguresAlgorithms;
using ChessTracking.ImageProcessing.PipelineData;
using ChessTracking.ImageProcessing.PlaneAlgorithms;
using ChessTracking.MultithreadingMessages;
using MathNet.Numerics;
using Microsoft.Kinect;
using TrackingState = ChessTracking.MultithreadingMessages.TrackingState;

namespace ChessTracking.ImageProcessing.PipelineParts.Stages
{
    class FiguresLocalization
    {
        private IHandDetectionAlgorithm HandDetectionAlgorithm { get; }

        public FiguresLocalization()
        {
            HandDetectionAlgorithm = new HandDetectionAlgorithm();
        }

        public FiguresDoneData Calibrate(ChessboardDoneData chessboardData)
        {
            var figuresData = new FiguresDoneData(chessboardData);

            return figuresData;
        }

        public FiguresDoneData Track(ChessboardDoneData chessboardData)
        {
            var figuresData = new FiguresDoneData(chessboardData);

            figuresData.ResultData.TrackingState =
                FigureLocalization(
                    figuresData.KinectData,
                    figuresData.ChessboardData.FieldSize,
                    figuresData.PlaneData.CannyDepthData,
                    figuresData.UserParameters);

            var handDetected =
                HandDetectionAlgorithm.HandDetected(
                    figuresData.KinectData.CameraSpacePointsFromDepthData,
                    figuresData.ChessboardData.FieldSize
                );
            figuresData.ResultData.HandDetected = handDetected || figuresData.ResultData.HandDetected;


            return figuresData;
        }

        private TrackingState FigureLocalization(
            KinectData kinectData,
            double fieldSize,
            byte[] canniedBytes,
            UserDefinedParameters userParameters)
        {
            var cameraSpacePointsFromDepthData = kinectData.CameraSpacePointsFromDepthData;
            var infraredData = kinectData.InfraredData;
            var colorFrameData = kinectData.ColorFrameData;
            var pointsFromDepthToColor = kinectData.PointsFromDepthToColor;

            // Collection of pixel colors for each field on chessboard
            var fields = new List<Color>[8, 8];

            // Populate it

            for (int x = 0; x < 8; x++)
            {
                for (int y = 0; y < 8; y++)
                {
                    fields[x, y] = new List<Color>();
                }
            }

            for (int i = 0; i < cameraSpacePointsFromDepthData.Length; i++)
            {
                if (!(float.IsInfinity(cameraSpacePointsFromDepthData[i].Z) || float.IsNaN(cameraSpacePointsFromDepthData[i].Z))
                && cameraSpacePointsFromDepthData[i].X > 0
                && cameraSpacePointsFromDepthData[i].Y > 0
                && cameraSpacePointsFromDepthData[i].X < fieldSize * 8
                && cameraSpacePointsFromDepthData[i].Y < fieldSize * 8
                && infraredData[i] > 1500
                && canniedBytes[i] != 255
                //&& cameraSpacePointsFromDepthData[i].Z < 0.025f
                && cameraSpacePointsFromDepthData[i].Z < -(userParameters.MilimetersClippedFromFigure / 1000d)
                && cameraSpacePointsFromDepthData[i].Z > -0.5f
                )
                {
                    var reference = pointsFromDepthToColor[i];

                    if (reference.X > 0 && reference.X < 1920 && reference.Y > 0 && reference.Y < 1080)
                    {
                        var r = colorFrameData[((int)reference.X + (int)reference.Y * 1920) * 4 + 0];
                        var g = colorFrameData[((int)reference.X + (int)reference.Y * 1920) * 4 + 1];
                        var b = colorFrameData[((int)reference.X + (int)reference.Y * 1920) * 4 + 2];

                        int x = (int)Math.Floor(cameraSpacePointsFromDepthData[i].X / fieldSize);
                        int y = (int)Math.Floor(cameraSpacePointsFromDepthData[i].Y / fieldSize);

                        if (x >= 0 && y >= 0 && x < 8 && y < 8)
                        {
                            fields[x, y].Add(Color.FromArgb(r, g, b));
                        }

                    }

                }
            }

            TrackingFieldState[,] figures = new TrackingFieldState[8, 8];

            for (int x = 0; x < 8; x++)
            {
                for (int y = 0; y < 8; y++)
                {
                    if (fields[x, y].Count < userParameters.NumberOfPointsIndicatingFigure)
                    {
                        figures[x, y] = TrackingFieldState.None;
                    }
                    else
                    {
                        var averageBrightnessInField = fields[x, y].Sum(f => Color.FromArgb(f.R, f.G, f.B).GetBrightness()) / fields[x, y].Count;

                        figures[x, y] = averageBrightnessInField > 0.5 + userParameters.ColorCalibrationAdditiveConstant
                            ? TrackingFieldState.White
                            : TrackingFieldState.Black;
                    }


                }
            }

            return new TrackingState(figures);
        }


    }
}
