using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using ChessTracking.MultithreadingMessages;
using Kinect_v0._1;
using Microsoft.Kinect;

namespace ChessTracking.ProcessingPipeline
{
    class FiguresLocalization
    {
        public Pipeline Pipeline { get; }

        public FiguresLocalization(Pipeline pipeline)
        {
            this.Pipeline = pipeline;
        }

        public FiguresDoneData Recalibrate(ChessboardDoneData chessboardData)
        {
            var figuresData = new FiguresDoneData(chessboardData);

            {

            }

            return figuresData;
        }

        public FiguresDoneData Track(ChessboardDoneData chessboardData)
        {
            var figuresData = new FiguresDoneData(chessboardData);

            {
                figuresData.FiguresBitmap =
                    FigureLocalization(
                        figuresData.CameraSpacePointsFromDepthData,
                        figuresData.ColorFrameData,
                        figuresData.PointsFromDepthToColor,
                        figuresData.InfraredData,
                        figuresData.FirstVectorFinal,
                        figuresData.CannyDepthData);
            }

            return figuresData;
        }

        private Bitmap FigureLocalization(CameraSpacePoint[] cameraSpacePointsFromDepthData, byte[] colorFrameData, ColorSpacePoint[] pointsFromDepthToColor, ushort[] infraredData,
                                           MyVector3DStruct magnitudeVector, byte[] canniedBytes)
        {
            List<RGBcolor>[,] loc = new List<RGBcolor>[8, 8];
            for (int x = 0; x < 8; x++)
            {
                for (int y = 0; y < 8; y++)
                {
                    loc[x, y] = new List<RGBcolor>();
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
                            /*
                            if (r > 80 && g > 80)
                            {
                                loc[x, y].Add(FigureColor.White);
                            }
                            else
                            {
                                loc[x, y].Add(FigureColor.Black);
                            }
                            */
                            loc[x, y].Add(new RGBcolor(r, g, b));
                        }

                    }

                }
            }

            var bm = new Bitmap(320, 320, PixelFormat.Format24bppRgb);
            SolidBrush blackBrush = new SolidBrush(Color.Black);
            SolidBrush whiteBrush = new SolidBrush(Color.White);
            SolidBrush blueBrush = new SolidBrush(Color.LightSkyBlue);

            for (int x = 0; x < 8; x++)
            {
                for (int y = 0; y < 8; y++)
                {
                    using (Graphics graphics = Graphics.FromImage(bm))
                    {
                        if (loc[x, y].Count < 5)
                        {
                            graphics.FillRectangle(blueBrush, new Rectangle(x * 40, y * 40, 40, 40));
                            //bm.SetPixel(x, y, Color.LightSkyBlue);
                        }
                        else
                        {
                            byte avgRed = (byte)(loc[x, y].Sum(f => f.g) / loc[x, y].Count);
                            byte avgGreen = (byte)(loc[x, y].Sum(f => f.g) / loc[x, y].Count);
                            byte avgBlue = (byte)(loc[x, y].Sum(f => f.b) / loc[x, y].Count);

                            var brightness = Color.FromArgb(avgRed, avgGreen, avgBlue).GetBrightness();

                            if (brightness > 0.47) // 41
                            {
                                graphics.FillRectangle(whiteBrush, new Rectangle(x * 40, y * 40, 40, 40));
                                //bm.SetPixel(x, y, Color.White);
                            }
                            else
                            {
                                graphics.FillRectangle(blackBrush, new Rectangle(x * 40, y * 40, 40, 40));
                                //bm.SetPixel(x, y, Color.Black);
                            }
                        }

                    }
                }
            }

            return bm;
            //DISPLAY: FormLocations.Image
        }

        private struct RGBcolor
        {
            public byte r;
            public byte g;
            public byte b;

            public RGBcolor(byte r, byte g, byte b)
            {
                this.r = r;
                this.g = g;
                this.b = b;
            }
        }

    }
}
