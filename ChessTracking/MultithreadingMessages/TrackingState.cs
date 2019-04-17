using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChessTracking.MultithreadingMessages
{
    class TrackingState
    {
        public TrackingFieldState[,] Figures { get; }

        public TrackingState(TrackingFieldState[,] figures)
        {
            this.Figures = figures;
        }

        public void RotateClockWise(int rightAngleRotations)
        {

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
