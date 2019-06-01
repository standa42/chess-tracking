using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ChessTracking.ImageProcessing.PipelineData;

namespace ChessTracking.ImageProcessing.PipelineParts.StagesInterfaces
{
    interface IPlaneLocalization
    {
        PlaneDoneData Calibrate(InputData inputData);

        PlaneDoneData Track(InputData inputData);
    }
}
