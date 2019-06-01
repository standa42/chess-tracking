using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ChessTracking.ControllingElements.ProgramState.States;
using ChessTracking.Game;
using ChessTracking.UserInterface;

namespace ChessTracking.ControllingElements.ProgramState
{
    class ProgramStateController : IProgramState
    {
        private StateContext StateContext { get; }

        public ProgramStateController(UserInterfaceOutputFacade outputFacade, GameController gameController)
        {
            StateContext = new StateContext(null, outputFacade, gameController);
            var initialState = new NoGameRunningState(StateContext);
            StateContext.InternalState = initialState;
        }

        public void GameLoaded()
        {
            StateContext.InternalState.GameLoaded();
        }

        public void GameEnded()
        {
            StateContext.InternalState.GameEnded();
        }

        public void StartedTracking()
        {
            StateContext.InternalState.StartedTracking();
        }

        public void StoppedTracking()
        {
            StateContext.InternalState.StoppedTracking();
        }

        public void Recalibrating()
        {
            StateContext.InternalState.Recalibrating();
        }

        public void GameFinished()
        {
            StateContext.InternalState.GameFinished();
        }

        public void ErrorInTracking()
        {
            StateContext.InternalState.ErrorInTracking();
        }

        public void TrackingStartSuccessful()
        {
            StateContext.InternalState.TrackingStartSuccessful();
        }
    }
}
