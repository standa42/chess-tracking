using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChessTracking.MultithreadingMessages
{
    /// <summary>
    /// Description of presence of figures on chessboard
    /// </summary>
    public class TrackingState
    {
        /// <summary>
        /// Indication, whether field contains white/black/none figure
        /// </summary>
        public TrackingFieldState[,] Figures { get; set; }

        public TrackingState(TrackingFieldState[,] figures)
        {
            this.Figures = figures;
        }

        public void RotateClockWise(int times)
        {
            var finalMatrix = Figures;

            for (int i = 0; i < times; i++)
            {
                var newMatrix = new TrackingFieldState[finalMatrix.GetLength(1), finalMatrix.GetLength(0)];

                int newRow = 0;
                for (int oldColumn = finalMatrix.GetLength(1) - 1; oldColumn >= 0; oldColumn--)
                {
                    var newColumn = 0;
                    for (int oldRow = 0; oldRow < finalMatrix.GetLength(0); oldRow++)
                    {
                        newMatrix[newRow, newColumn] = finalMatrix[oldRow, oldColumn];
                        newColumn++;
                    }
                    newRow++;
                }

                finalMatrix = newMatrix;
            }

            Figures = finalMatrix;
        }

        /// <summary>
        /// Checks if position on chessboard are the same
        /// </summary>
        public bool IsEquivalentTo(TrackingState other)
        {
            return TrackingState.IsEquivalent(this, other);
        }

        /// <summary>
        /// Checks if position on chessboard are the same
        /// </summary>
        public static bool IsEquivalent(TrackingState lhs, TrackingState rhs)
        {
            var figures = lhs.Figures;
            var otherFigures = rhs.Figures;

            bool equivalent = true;

            for (int x = 0; x < 8; x++)
            {
                for (int y = 0; y < 8; y++)
                {
                    if (figures[x, y] != otherFigures[x, y])
                    {
                        equivalent = false;
                    }
                }
            }

            return equivalent;
        }

        public void VerticalFlip()
        {
            var newMatrix = new TrackingFieldState[Figures.GetLength(1), Figures.GetLength(0)];

            for (int column = 0; column < 0; column++)
            {
                for (int row = 0; row < Figures.GetLength(0); row++)
                {
                    newMatrix[row, column] = Figures[row, (Figures.GetLength(0) - 1) - column];
                }
            }

            Figures = newMatrix;
        }

        public void HorizontalFlip()
        {
            var newMatrix = new TrackingFieldState[Figures.GetLength(1), Figures.GetLength(0)];

            for (int column = 0; column < Figures.GetLength(0); column++)
            {
                for (int row = 0; row < Figures.GetLength(1); row++)
                {
                    newMatrix[row, column] = Figures[(Figures.GetLength(1) - 1) - row, column];
                }
            }

            Figures = newMatrix;
        }

    }
}
