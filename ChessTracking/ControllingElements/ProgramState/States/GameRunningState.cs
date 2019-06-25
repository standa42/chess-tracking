using System;
using System.Collections.Generic;
using ChessTracking.Localization;

namespace ChessTracking.ControllingElements.ProgramState.States
{
    /// <summary>
    /// State of program, where game is loaded and not finished, but there ain't no tracking
    /// </summary>
    class GameRunningState : ProgramState
    {
        public GameRunningState(StateContext stateContext) : base(stateContext)
        {

        }

        public override void GameEnded()
        {
            StateContext.OutputFacade.Clear();
            StateContext.OutputFacade.InitialUiLockState();
            StateContext.InternalState = new NoGameRunningState(StateContext);
        }

        public override void StartedTracking()
        {
            StateContext.OutputFacade.StartedTrackingLockState();
            StateContext.OutputFacade.AddToTrackingLog(ProgramLocalization.DashedLine);
            StateContext.OutputFacade.AddToTrackingLog(ProgramLocalization.TrackingCalibrationStarted);
            StateContext.InternalState = new TrackingStartedState(StateContext);
        }

    }
}
