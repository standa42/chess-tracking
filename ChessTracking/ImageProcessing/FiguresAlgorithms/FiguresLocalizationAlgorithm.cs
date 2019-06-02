using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using ChessTracking.ImageProcessing.PipelineData;
using ChessTracking.MultithreadingMessages;

namespace ChessTracking.ImageProcessing.FiguresAlgorithms
{
    class FiguresLocalizationAlgorithm : IFiguresLocalizationAlgorithm
    {
        public TrackingState LocateFigures(KinectData kinectData, double fieldSize, byte[] canniedBytes, UserDefinedParameters userParameters)
        {
            // Collection of pixel colors for each field on chessboard
            var colorsOfPointsOverIndividualFields = InitializeColorCollection();

            colorsOfPointsOverIndividualFields = FillArrayWithData(colorsOfPointsOverIndividualFields, kinectData, fieldSize, canniedBytes, userParameters);

            var trackingState = InferPresenceAndColorOfFiguresOnFields(colorsOfPointsOverIndividualFields, userParameters);
            
            return trackingState;
        }

        private TrackingState InferPresenceAndColorOfFiguresOnFields(List<Color>[,] inputColorsData, UserDefinedParameters userParameters)
        {
            var figures = new TrackingFieldState[8, 8];

            for (int x = 0; x < 8; x++)
                for (int y = 0; y < 8; y++)
                {
                    if (inputColorsData[x, y].Count < userParameters.NumberOfPointsIndicatingFigure)
                        figures[x, y] = TrackingFieldState.None;
                    else
                    {
                        var averageBrightnessInField = 
                            inputColorsData[x, y].Sum(f => Color.FromArgb(f.R, f.G, f.B).GetBrightness()) 
                            / inputColorsData[x, y].Count;

                        figures[x, y] = 
                            averageBrightnessInField > 0.5 + userParameters.ColorCalibrationAdditiveConstant
                            ? TrackingFieldState.White
                            : TrackingFieldState.Black;
                    }
                }

            return new TrackingState(figures);
        }

        private List<Color>[,] FillArrayWithData(List<Color>[,] array, KinectData kinectData, double fieldSize, byte[] canniedBytes, UserDefinedParameters userParameters)
        {
            var csp = kinectData.CameraSpacePointsFromDepthData;
            var infraredData = kinectData.InfraredData;
            var colorFrameData = kinectData.ColorFrameData;
            var pointsFromDepthToColor = kinectData.PointsFromDepthToColor;

            var chessboardSize = fieldSize * 8;

            for (int i = 0; i < csp.Length; i++)
            {
                var isValid = !(float.IsInfinity(csp[i].Z) || float.IsNaN(csp[i].Z));
                var lowerBoundOfXIsOk = csp[i].X > 0;
                var lowerBoundOfYIsOk = csp[i].Y > 0;
                var upperBoundOfXIsOk = csp[i].X < chessboardSize;
                var upperBoundOfYIsOk = csp[i].Y < chessboardSize;
                var infraredThresholdIsOk = infraredData[i] > 1500;
                var aintFlyingPixelAccordingToCannyDetectorOnDepth = canniedBytes[i] != 255;
                var isHighEnought = csp[i].Z < -(userParameters.MilimetersClippedFromFigure / 1000d);
                var isBelowThreshold = csp[i].Z > -0.5f;

                if (isValid
                    && lowerBoundOfXIsOk
                    && lowerBoundOfYIsOk
                    && upperBoundOfXIsOk
                    && upperBoundOfYIsOk
                    && infraredThresholdIsOk
                    && aintFlyingPixelAccordingToCannyDetectorOnDepth
                    && isHighEnought
                    && isBelowThreshold)
                {
                    var reference = pointsFromDepthToColor[i];

                    if (reference.X > 0 && reference.X < 1920 && reference.Y > 0 && reference.Y < 1080)
                    {
                        var r = colorFrameData[((int)reference.X + (int)reference.Y * 1920) * 4 + 0];
                        var g = colorFrameData[((int)reference.X + (int)reference.Y * 1920) * 4 + 1];
                        var b = colorFrameData[((int)reference.X + (int)reference.Y * 1920) * 4 + 2];

                        int x = (int)Math.Floor(csp[i].X / fieldSize);
                        int y = (int)Math.Floor(csp[i].Y / fieldSize);

                        if (x >= 0 && y >= 0 && x < 8 && y < 8)
                        {
                            array[x, y].Add(Color.FromArgb(r, g, b));
                        }
                    }
                }
            }

            return array;
        }

        private List<Color>[,] InitializeColorCollection()
        {
            var collection = new List<Color>[8, 8];

            // Populate array with lists
            for (int x = 0; x < 8; x++)
                for (int y = 0; y < 8; y++)
                    collection[x, y] = new List<Color>();

            return collection;
        }
    }
}
