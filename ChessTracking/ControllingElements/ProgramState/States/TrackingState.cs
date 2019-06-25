using System;
using ChessTracking.Localization;

namespace ChessTracking.ControllingElements.ProgramState.States
{
    /// <summary>
    /// State where chessboard is tracked, but game isn't recognized yet
    /// </summary>
    class TrackingState : ProgramState
    {
        public TrackingState(StateContext stateContext) : base(stateContext)
        {

        }

        public override void GameRecognized()
        {
            StateContext.OutputFacade.TrackingLockState();
            StateContext.OutputFacade.AddToTrackingLog(ProgramLocalization.GameRecognized);
            StateContext.InternalState = new TrackingGameState(StateContext);
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
    }
}
