using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using ChessTracking.ImageProcessing.PipelineData;
using ChessTracking.MultithreadingMessages;
using ChessTracking.Utils;
using Point = System.Drawing.Point;

namespace ChessTracking.ImageProcessing.FiguresAlgorithms
{
    class FiguresLocalizationAlgorithm : IFiguresLocalizationAlgorithm
    {
        public (TrackingState, int[,]) LocateFigures(KinectData kinectData, double fieldSize, byte[] canniedBytes, UserDefinedParameters userParameters, TrackingResultData resultData, Bitmap colorBitmap, TrackingState gameTrackingState)
        {
            // Collection of pixel colors for each field on chessboard
            var colorsOfPointsOverIndividualFields = InitializeColorCollection();

            colorsOfPointsOverIndividualFields = FillColorsOverFiledsArrayWithData(colorsOfPointsOverIndividualFields, kinectData, fieldSize, canniedBytes, userParameters);

            var trackingState = DetectPresenceAndColorOfFiguresOnFields(colorsOfPointsOverIndividualFields, gameTrackingState, userParameters);

            resultData.VisualisationBitmap = RenderLabelsToFigures(colorsOfPointsOverIndividualFields, trackingState, resultData.VisualisationBitmap);

            var pointCounts = GetPointsCountsOverIndividualFields(colorsOfPointsOverIndividualFields);

            return (trackingState, pointCounts);
        }

        private int[,] GetPointsCountsOverIndividualFields(List<Point2DWithColor>[,] colorArray)
        {
            var counts = new int[8, 8];

            for (int x = 0; x < 8; x++)
                for (int y = 0; y < 8; y++)
                    counts[x, y] = colorArray[x, y].Count;

            return counts;
        }

        /// <summary>
        /// Decides which fields contain figure and decides its color
        /// -> if there are more points over field than threshold, the figure is considered as present
        /// -> color is decided by thresholding average fitness of points over field
        /// </summary>
        /// <param name="inputColorsData">Colors of points over individual fields</param>
        /// <param name="userParameters">User defined parameters</param>
        /// <returns>Tracking state of chessboard</returns>
        private TrackingState DetectPresenceAndColorOfFiguresOnFields(List<Point2DWithColor>[,] inputColorsData, TrackingState gameTrackingState, UserDefinedParameters userParameters)
        {
            var figures = new TrackingFieldState[8, 8];

            for (int x = 0; x < 8; x++)
                for (int y = 0; y < 8; y++)
                {
                    int presenceInfluence = 0;
                    float colorInfluence = 0;

                    if (gameTrackingState != null)
                    {
                        presenceInfluence =
                            gameTrackingState.Figures[x,y] != TrackingFieldState.None 
                                ? userParameters.GameStateInfluenceOnPresence
                                : 0;

                        colorInfluence =
                            (userParameters.GameStateInfluenceOnColor / 100f) *
                            (gameTrackingState.Figures[x, y] == TrackingFieldState.None
                                ? 0
                                : gameTrackingState.Figures[x, y] == TrackingFieldState.White
                                    ? 1
                                    : -1
                            );
                    }


                    if ((inputColorsData[x, y].Count + presenceInfluence) < userParameters.NumberOfPointsIndicatingFigure)
                        figures[x, y] = TrackingFieldState.None;
                    else
                    {
                        if (gameTrackingState != null && inputColorsData[x, y].Count == 0)
                        {
                            figures[x, y] = gameTrackingState.Figures[x, y];
                            continue;;
                        }

                        double averageBrightnessInField;

                        if (userParameters.IsFiguresColorMetricExperimental)
                        {
                            averageBrightnessInField =
                                inputColorsData[x, y].Sum(f => 1 - Math.Pow(-2.5f * (Color.FromArgb(f.Color.R, f.Color.G, f.Color.B).CustomBrightness() - 0.5f), 2) + colorInfluence)
                                / inputColorsData[x, y].Count;
                        }
                        else
                        {
                            averageBrightnessInField =
                                inputColorsData[x, y].Sum(f => Color.FromArgb(f.Color.R, f.Color.G, f.Color.B).GetBrightness() + colorInfluence)
                                / inputColorsData[x, y].Count;
                        }


                        figures[x, y] =
                            averageBrightnessInField > 0.5 + userParameters.ColorCalibrationAdditiveConstant
                            ? TrackingFieldState.White
                            : TrackingFieldState.Black;
                    }
                }

            return new TrackingState(figures);
        }

        /// <summary>
        /// Detects points over individual fields of chessboard satisfying required conditions
        /// </summary>
        private List<Point2DWithColor>[,] FillColorsOverFiledsArrayWithData(List<Point2DWithColor>[,] array, KinectData kinectData, double fieldSize, byte[] canniedBytes, UserDefinedParameters userParameters)
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
                            array[x, y].Add(new Point2DWithColor(Color.FromArgb(r, g, b), (int)reference.X, (int)reference.Y));
                        }
                    }
                }
            }

            return array;
        }

        private List<Point2DWithColor>[,] InitializeColorCollection()
        {
            var collection = new List<Point2DWithColor>[8, 8];

            // Populate array with lists
            for (int x = 0; x < 8; x++)
                for (int y = 0; y < 8; y++)
                    collection[x, y] = new List<Point2DWithColor>();

            return collection;
        }

        /// <summary>
        /// Generates bitmap with figures labeled by recognized color
        /// </summary>
        private Bitmap RenderLabelsToFigures(List<Point2DWithColor>[,] points, TrackingState trackingState, Bitmap colorBitmap)
        {
            var bm = (Bitmap)colorBitmap.Clone();

            Brush whiteBrush = new SolidBrush(Color.White);
            Brush blackBrush = new SolidBrush(Color.Black);
            Pen whitePen = new Pen(Color.White, 2);
            Pen blackPen = new Pen(Color.Black, 2);

            using (Graphics gr = Graphics.FromImage(bm))
            {
                for (int x = 0; x < 8; x++)
                    for (int y = 0; y < 8; y++)
                    {
                        if (trackingState.Figures[x, y] != TrackingFieldState.None)
                        {
                            // locate point to draw (mean of coordinates of points of figure)
                            var xCoordinatesSum = points[x, y].Sum(p => p.X);
                            var yCoordinatesSum = points[x, y].Sum(p => p.Y);
                            var xMean = xCoordinatesSum / points[x, y].Count;
                            var yMean = yCoordinatesSum / points[x, y].Count;

                            // draw
                            var isWhite = trackingState.Figures[x, y] == TrackingFieldState.White;

                            gr.FillEllipse(isWhite ? whiteBrush : blackBrush, xMean - 7, yMean - 7, 14, 14);
                            gr.DrawEllipse(isWhite ? blackPen : whitePen, xMean - 7, yMean - 7, 14, 14);
                        }
                    }
            }

            return bm;
        }
    }
}
