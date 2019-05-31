using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using ChessTracking.ImageProcessing.PipelineData;
using ChessTracking.ImageProcessing.PlaneAlgorithms;
using ChessTracking.MultithreadingMessages;
using Microsoft.Kinect;
using TrackingState = ChessTracking.MultithreadingMessages.TrackingState;

namespace ChessTracking.ImageProcessing.PipelineParts
{
    class FiguresLocalization
    {
        public FiguresDoneData Recalibrate(ChessboardDoneData chessboardData)
        {
            var figuresData = new FiguresDoneData(chessboardData);

            return figuresData;
        }

        public FiguresDoneData Track(ChessboardDoneData chessboardData)
        {
            var figuresData = new FiguresDoneData(chessboardData);

            {
                figuresData.ResultData.TrackingState =
                    FigureLocalization(
                        figuresData.KinectData.CameraSpacePointsFromDepthData,
                        figuresData.KinectData.ColorFrameData,
                        figuresData.KinectData.PointsFromDepthToColor,
                        figuresData.KinectData.InfraredData,
                        figuresData.ChessboardData.FirstVectorFinal,
                        figuresData.PlaneData.CannyDepthData,
                        figuresData.UserParameters.ColorCalibrationAdditiveConstant);
                figuresData.ResultData.HandDetected =
                    HandDetection(
                        figuresData.KinectData.CameraSpacePointsFromDepthData,
                        figuresData.ChessboardData.FirstVectorFinal
                    );
            }

            return figuresData;
        }
        
        private bool HandDetection(CameraSpacePoint[] cameraSpacePointsFromDepthData, MyVector3DStruct magnitudeVector)
        {
            int counter = 0;

            for (int i = 0; i < cameraSpacePointsFromDepthData.Length; i++)
            {
                if (
                    !(float.IsInfinity(cameraSpacePointsFromDepthData[i].Z) || float.IsNaN(cameraSpacePointsFromDepthData[i].Z))
                    && cameraSpacePointsFromDepthData[i].X > (-magnitudeVector.Magnitude() * (7 / 10f))
                    && cameraSpacePointsFromDepthData[i].Y > (-magnitudeVector.Magnitude() * (7 / 10f))
                    && cameraSpacePointsFromDepthData[i].X < magnitudeVector.Magnitude() * 8 + (magnitudeVector.Magnitude() * (7 / 10f))
                    && cameraSpacePointsFromDepthData[i].Y < magnitudeVector.Magnitude() * 8 + (magnitudeVector.Magnitude() * (7 / 10f))
                    && cameraSpacePointsFromDepthData[i].Z < -0.095f
                    && cameraSpacePointsFromDepthData[i].Z > -0.2f
                )
                {
                    counter++;
                }
            }

            return counter > 20 ? true : false;
        }

        private TrackingState FigureLocalization(
            CameraSpacePoint[] cameraSpacePointsFromDepthData,
            byte[] colorFrameData,
            ColorSpacePoint[] pointsFromDepthToColor,
            ushort[] infraredData,
            MyVector3DStruct magnitudeVector,
            byte[] canniedBytes,
            double ColorCalibrationAdditiveConstant)
        {
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
                && cameraSpacePointsFromDepthData[i].X < magnitudeVector.Magnitude() * 8
                && cameraSpacePointsFromDepthData[i].Y < magnitudeVector.Magnitude() * 8
                && infraredData[i] > 1500
                && canniedBytes[i] != 255
                //&& cameraSpacePointsFromDepthData[i].Z < 0.025f
                && cameraSpacePointsFromDepthData[i].Z < -0.01f
                && cameraSpacePointsFromDepthData[i].Z > -0.5f
                )
                {
                    var reference = pointsFromDepthToColor[i];

                    if (reference.X > 0 && reference.X < 1920 && reference.Y > 0 && reference.Y < 1080)
                    {
                        var r = colorFrameData[((int)reference.X + (int)reference.Y * 1920) * 4 + 0];
                        var g = colorFrameData[((int)reference.X + (int)reference.Y * 1920) * 4 + 1];
                        var b = colorFrameData[((int)reference.X + (int)reference.Y * 1920) * 4 + 2];

                        int x = (int)Math.Floor(cameraSpacePointsFromDepthData[i].X / magnitudeVector.Magnitude());
                        int y = (int)Math.Floor(cameraSpacePointsFromDepthData[i].Y / magnitudeVector.Magnitude());

                        if (x >= 0 && y >= 0 && x < 8 && y < 8)
                        {
                            fields[x, y].Add(Color.FromArgb(r,g,b));
                        }

                    }

                }
            }

            TrackingFieldState[,] figures = new TrackingFieldState[8, 8];

            for (int x = 0; x < 8; x++)
            {
                for (int y = 0; y < 8; y++)
                {
                        if (fields[x, y].Count < 5)
                        {
                            figures[x, y] = TrackingFieldState.None;
                        }
                        else
                        {
                            var averageBrightness = fields[x, y].Sum(f => Color.FromArgb(f.R, f.G, f.B).GetBrightness()) / fields[x,y].Count;

                            figures[x, y] = averageBrightness > 0.5 + ColorCalibrationAdditiveConstant
                                ? TrackingFieldState.White
                                : TrackingFieldState.Black;
                        }

                    
                }
            }

            return new TrackingState(figures);
        }
        

    }
}
