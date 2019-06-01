using System;

namespace ChessTracking.ControllingElements.ProgramState.States
{
    class GameRunningState : IProgramState
    {
        private StateContext StateContext { get; }

        public GameRunningState(StateContext stateContext)
        {
            StateContext = stateContext;
        }

        public void GameLoaded()
        {
            throw new NotImplementedException();
        }

        public void GameEnded()
        {
            throw new NotImplementedException();
        }

        public void StartedTracking()
        {
            throw new NotImplementedException();
        }

        public void StoppedTracking()
        {
            throw new NotImplementedException();
        }

        public void Recalibrating()
        {
            throw new NotImplementedException();
        }

        public void GameFinished()
        {
            throw new NotImplementedException();
        }

        public void ErrorInTracking()
        {
            throw new NotImplementedException();
        }

        public void TrackingStartSuccessful()
        {
            throw new NotImplementedException();
        }
    }
}
