using System;

namespace ChessTracking.ControllingElements.ProgramState.States
{
    class GameFinishedState : ProgramState
    {
        public GameFinishedState(StateContext stateContext) : base(stateContext)
        {

        }

        public override void GameEnded()
        {
            StateContext.OutputFacade.Clear();
            StateContext.OutputFacade.InitialUiLockState();
            StateContext.InternalState = new NoGameRunningState(StateContext);
        }
    }
}
