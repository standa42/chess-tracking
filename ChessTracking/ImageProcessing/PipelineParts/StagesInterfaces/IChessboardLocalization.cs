using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ChessTracking.ImageProcessing.PipelineData;

namespace ChessTracking.ImageProcessing.PipelineParts.StagesInterfaces
{
    interface IChessboardLocalization
    {
        ChessboardDoneData Calibrate(PlaneDoneData planeData);

        ChessboardDoneData Track(PlaneDoneData planeData);
    }
}
