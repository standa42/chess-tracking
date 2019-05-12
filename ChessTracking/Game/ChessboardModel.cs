using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ChessTracking.MultithreadingMessages;

namespace ChessTracking.Game
{
    /// <summary>
    /// Representation of state of physical chessboard
    /// </summary>
    [Serializable]
    public class ChessboardModel
    {
        /// <summary>
        /// Contains information about presence of figure on each square of chessboard
        /// Figure if there is figure, null otherwise
        /// </summary>
        public Figure[,] Figures { get; set; }

        public ChessboardModel(Figure[,] figures)
        {
            this.Figures = figures;
        }

        /// <summary>
        /// Gets tracking states of given chessboard
        /// </summary>
        /// <returns>Tracking states of given chessboard</returns>
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

        /// <summary>
        /// Performs move of figure on chessboard
        /// </summary>
        /// <param name="from">Move origin positon</param>
        /// <param name="to">Move destination position</param>
        public void MoveTo(ChessPosition from, ChessPosition to)
        {
            Figures[to.X, to.Y] = Figures[from.X, from.Y];
            Figures[from.X, from.Y] = null;
            Figures[to.X, to.Y].Moved = true;
        }

        /// <summary>
        /// Performs deletion of figure on chessboard
        /// </summary>
        /// <param name="position">Position of figure to delete</param>
        public void Delete(ChessPosition position)
        {
            Figures[position.X, position.Y] = null;
        }

        /// <summary>
        /// Adds figure to chessboard
        /// </summary>
        /// <param name="figure">Added figure</param>
        /// <param name="position">Added figure position</param>
        public void AddFigure(Figure figure, ChessPosition position)
        {
            Figures[position.X, position.Y] = figure;
        }

        /// <summary>
        /// Returns figure on given position
        /// </summary>
        /// <param name="position">Position of demanded figure</param>
        /// <returns>Figure on position, null if there ain't no figure</returns>
        public Figure GetFigureOnPosition(ChessPosition position)
        {
            return Figures[position.X, position.Y];
        }
    }
}
