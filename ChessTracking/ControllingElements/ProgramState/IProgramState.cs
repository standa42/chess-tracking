using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChessTracking.ControllingElements.ProgramState
{
    /// <summary>
    /// Interface representing state of program (modification of state pattern) - inner state can be changed by defined actions
    /// </summary>
    interface IProgramState
    {
        /// <summary>
        /// Game was loaded
        /// </summary>
        void GameLoaded();

        /// <summary>
        /// Game was ended
        /// </summary>
        void GameEnded();

        /// <summary>
        /// Tracking has started
        /// </summary>
        void StartedTracking();

        /// <summary>
        /// Tracking has stopped
        /// </summary>
        void StoppedTracking();

        /// <summary>
        /// Recalibration command was issued
        /// </summary>
        void Recalibrating();

        /// <summary>
        /// Game has finished
        /// </summary>
        void GameFinished();

        /// <summary>
        /// Error in tracking procedure occured
        /// </summary>
        /// <param name="message"></param>
        void ErrorInTracking(string message);

        /// <summary>
        /// Calibration is succesfully done
        /// </summary>
        void TrackingStartSuccessful();

        /// <summary>
        /// Game on chessboard was recognized by program
        /// </summary>
        void GameRecognized();
    }
}
