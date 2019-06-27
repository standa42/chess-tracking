using System.Drawing;
using ChessTracking.ImageProcessing.PipelineData;
using ChessTracking.MultithreadingMessages;

namespace ChessTracking.ImageProcessing.FiguresAlgorithms
{
    /// <summary>
    /// Algorithm providing detection of presence and color of figures on chessboard from incoming data
    /// </summary>
    interface IFiguresLocalizationAlgorithm
    {
        (TrackingState, int[,]) LocateFigures(KinectData kinectData, double fieldSize, byte[] canniedBytes,  UserDefinedParameters userParameters, TrackingResultData resultData, Bitmap ColorBitmap, TrackingState gameTrackingState);
    }
}
