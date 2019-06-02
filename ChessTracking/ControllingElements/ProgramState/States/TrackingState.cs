using System;

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
            StateContext.OutputFacade.AddToTrackingLog("Game Recognized");
            StateContext.InternalState = new TrackingGameState(StateContext);
        }

        public override void Recalibrating()
        {
            StateContext.OutputFacade.StartedTrackingLockState();
            StateContext.OutputFacade.AddToTrackingLog("Recalibration started, please wait");
            StateContext.InternalState = new TrackingStartedState(StateContext);
        }

        public override void StoppedTracking()
        {
            StateContext.OutputFacade.GameRunningLockState();
            StateContext.OutputFacade.AddToTrackingLog("Tracking stoped");
            StateContext.InternalState = new GameRunningState(StateContext);
        }
    }
}
