using System;
using System.Collections.Generic;

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
            StateContext.OutputFacade.AddToTrackingLog("–––––––––––––––––––––––––");
            StateContext.OutputFacade.AddToTrackingLog("Tracking calibration started, please wait");
            StateContext.InternalState = new TrackingStartedState(StateContext);
        }

    }
}
