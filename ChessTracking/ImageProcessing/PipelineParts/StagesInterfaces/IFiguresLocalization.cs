using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ChessTracking.ImageProcessing.PipelineData;

namespace ChessTracking.ImageProcessing.PipelineParts.StagesInterfaces
{
    interface IFiguresLocalization
    {
        FiguresDoneData Calibrate(ChessboardDoneData chessboardData);

        FiguresDoneData Track(ChessboardDoneData chessboardData);
    }
}
