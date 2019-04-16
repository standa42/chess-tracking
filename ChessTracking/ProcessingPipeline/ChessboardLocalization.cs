using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChessTracking.ProcessingPipeline
{
    class ChessboardLocalization
    {
        public Pipeline Pipeline { get; }

        public ChessboardLocalization(Pipeline pipeline)
        {
            this.Pipeline = pipeline;
        }

        public ChessboardDoneData Recalibrate(PlaneDoneData planeData)
        {
            return new ChessboardDoneData(planeData);
        }

        public ChessboardDoneData Track(PlaneDoneData planeData)
        {
            return new ChessboardDoneData(planeData);
        }
    }
}
