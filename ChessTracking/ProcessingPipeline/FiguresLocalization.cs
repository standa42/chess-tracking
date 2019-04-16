using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using ChessTracking.MultithreadingMessages;

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
            return new FiguresDoneData(chessboardData);
        }

        public FiguresDoneData Track(ChessboardDoneData chessboardData)
        {
            
            return new FiguresDoneData(chessboardData);
        }
    }
}
