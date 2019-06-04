using System.Drawing;
using System.Drawing.Imaging;
using Microsoft.Kinect;

namespace ChessTracking.ImageProcessing.ChessboardAlgorithms
{
    class RendererOfSceneWithChessboard
    {
        public Bitmap ReturnLocalizedChessboardWithTable(Bitmap colorImg, bool[] tableMask,
            DepthSpacePoint[] pointsFromColorToDepth, CameraSpacePoint[] cameraSpacePointsFromDepthData, double fieldSize)
        {
            var chessboardSize = fieldSize * 8;

            Bitmap bm = colorImg;

            BitmapData bitmapData = bm.LockBits(new Rectangle(0, 0, bm.Width, bm.Height), ImageLockMode.ReadWrite, PixelFormat.Format24bppRgb);
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

                                if (tableMask[colorImageIndex])
                                {
                                    if (!(float.IsInfinity(cameraSpacePointsFromDepthData[pointPosition].Z) ||
                                          float.IsNaN(cameraSpacePointsFromDepthData[pointPosition].Z))

                                        && cameraSpacePointsFromDepthData[pointPosition].X > 0
                                        && cameraSpacePointsFromDepthData[pointPosition].Y > 0
                                        && cameraSpacePointsFromDepthData[pointPosition].X < chessboardSize
                                        && cameraSpacePointsFromDepthData[pointPosition].Y < chessboardSize
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
