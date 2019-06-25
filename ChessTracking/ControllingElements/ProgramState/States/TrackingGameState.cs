using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ChessTracking.Localization;

namespace ChessTracking.ControllingElements.ProgramState.States
{
    /// <summary>
    /// State of tracking both chessboard and on going game
    /// </summary>
    class TrackingGameState : ProgramState
    {
        public TrackingGameState(StateContext stateContext) : base(stateContext)
        {

        }

        public override void Recalibrating()
        {
            StateContext.OutputFacade.StartedTrackingLockState();
            StateContext.OutputFacade.AddToTrackingLog(ProgramLocalization.RecalibrationStarted);
            StateContext.InternalState = new TrackingStartedState(StateContext);
        }

        public override void StoppedTracking()
        {
            StateContext.OutputFacade.GameRunningLockState();
            StateContext.OutputFacade.AddToTrackingLog(ProgramLocalization.TrackingStopped);
            StateContext.InternalState = new GameRunningState(StateContext);
        }

        public override void GameFinished()
        {
            StateContext.OutputFacade.GameFinishedLockState();
            StateContext.OutputFacade.AddToTrackingLog(ProgramLocalization.TrackingStopped);
            StateContext.TrackingManager.StopTracking(gameFinished: true);
            StateContext.InternalState = new GameFinishedState(StateContext);
        }
    }
}
