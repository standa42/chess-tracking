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

                int newColumn, newRow = 0;
                for (int oldColumn = finalMatrix.GetLength(1) - 1; oldColumn >= 0; oldColumn--)
                {
                    newColumn = 0;
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

        public void RotateCounterClockWise(int rightAngleRotations)
        {
            throw new NotImplementedException();
        }


        public static bool operator ==(TrackingState lhs, TrackingState rhs)
        {
            return Equals(lhs, rhs);
        }

        public static bool operator !=(TrackingState lhs, TrackingState rhs)
        {
            return !Equals(lhs, rhs);
        }

        public bool Equals(TrackingState other)
        {
            if (ReferenceEquals(null, other))
            {
                return false;
            }
            if (ReferenceEquals(this, other))
            {
                return true;
            }

            bool equal = true;

            for (int x = 0; x < 8; x++)
            {
                for (int y = 0; y < 8; y++)
                {
                    if (Figures[x,y] != other.Figures[x,y])
                    {
                        equal = false;
                    }
                }
            }

            return equal;
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj))
            {
                return false;
            }
            if (ReferenceEquals(this, obj))
            {
                return true;
            }

            return obj.GetType() == GetType() && Equals((TrackingState)obj);
        }
    }
}
