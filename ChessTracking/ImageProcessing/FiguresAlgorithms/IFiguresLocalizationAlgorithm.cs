using ChessTracking.ImageProcessing.PipelineData;
using ChessTracking.MultithreadingMessages;

namespace ChessTracking.ImageProcessing.FiguresAlgorithms
{
    interface IFiguresLocalizationAlgorithm
    {
        TrackingState LocateFigures(KinectData kinectData, double fieldSize, byte[] canniedBytes,  UserDefinedParameters userParameters);
    }
}
