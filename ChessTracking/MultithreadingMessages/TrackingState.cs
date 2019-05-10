using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChessTracking.MultithreadingMessages
{
    public class TrackingState
    {
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

        public bool IsEquivalentTo(TrackingState other)
        {
            return TrackingState.IsEquivalent(this, other);
        }

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
        
    }
}
