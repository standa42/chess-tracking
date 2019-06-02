using System;

namespace ChessTracking.ControllingElements.ProgramState.States
{
    /// <summary>
    /// State representing, that tracking started - continues after calibration is done
    /// </summary>
    class TrackingStartedState : ProgramState
    {
        public TrackingStartedState(StateContext stateContext) : base(stateContext)
        {

        }

        public override void TrackingStartSuccessful()
        {
            StateContext.OutputFacade.TrackingLockState();
            StateContext.OutputFacade.AddToTrackingLog("Start of tracking was successful");
            StateContext.InternalState = new TrackingState(StateContext);
        }

        public override void StoppedTracking()
        {
            StateContext.OutputFacade.GameRunningLockState();
            StateContext.OutputFacade.AddToTrackingLog("Tracking stoped");
            StateContext.InternalState = new GameRunningState(StateContext);
        }
    }
}
