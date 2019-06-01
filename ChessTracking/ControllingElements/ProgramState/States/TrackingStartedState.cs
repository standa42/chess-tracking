using System;

namespace ChessTracking.ControllingElements.ProgramState.States
{
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
    }
}
