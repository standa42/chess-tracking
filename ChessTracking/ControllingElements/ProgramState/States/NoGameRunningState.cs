using System;

namespace ChessTracking.ControllingElements.ProgramState.States
{
    class NoGameRunningState : ProgramState
    {
        public NoGameRunningState(StateContext stateContext) : base(stateContext)
        {

        }

        public override void GameLoaded()
        {
            StateContext.OutputFacade.GameRunningLockState();
            StateContext.InternalState = new GameRunningState(StateContext);
        }
        
        public override void GameFinished()
        {
            StateContext.OutputFacade.GameFinishedLockState();
            StateContext.InternalState = new GameFinishedState(StateContext);
        }
    }
}
