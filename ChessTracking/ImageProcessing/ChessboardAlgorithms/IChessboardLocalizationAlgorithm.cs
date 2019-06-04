using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ChessTracking.ImageProcessing.PipelineData;
using Point2D = MathNet.Spatial.Euclidean.Point2D;

namespace ChessTracking.ImageProcessing.ChessboardAlgorithms
{
    interface IChessboardLocalizationAlgorithm
    {
        Chessboard3DReprezentation LocateChessboard(ChessboardTrackingCompleteData chessboardData);
    }
}
