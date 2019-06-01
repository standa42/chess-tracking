using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChessTracking.ControllingElements.ProgramState
{
    interface IProgramState
    {
        void GameLoaded();

        void GameEnded();

        void StartedTracking();

        void StoppedTracking();

        void Recalibrating();

        void GameFinished();

        void ErrorInTracking(string message);

        void TrackingStartSuccessful();

        void GameRecognized();
    }
}
