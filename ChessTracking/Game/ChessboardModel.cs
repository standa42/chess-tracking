using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ChessTracking.MultithreadingMessages;

namespace ChessTracking.Game
{
    public class ChessboardModel
    {
        public Figure[,] Figures { get; set; }

        public ChessboardModel(Figure[,] figures)
        {
            this.Figures = figures;
        }

        public TrackingState GetTrackingStates()
        {
            var figures = new TrackingFieldState[8, 8];

            for (int x = 0; x < 8; x++)
            {
                for (int y = 0; y < 8; y++)
                {
                    if (Figures[x, y] == null)
                    {
                        figures[x, y] = TrackingFieldState.None;
                    }
                    else if (Figures[x, y].Color == PlayerColor.White)
                    {
                        figures[x, y] = TrackingFieldState.White;
                    }
                    else
                    {
                        figures[x, y] = TrackingFieldState.Black;
                    }
                }
            }

            var state = new TrackingState(figures);
            return state;
        }
    }
}
