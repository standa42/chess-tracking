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
    /// <summary>
    /// Controller creating state context, transparently calls actually set state
    /// </summary>
    class ProgramStateController : IProgramState
    {
        private StateContext StateContext { get; set; }
        
        public void SetInitialContext(UserInterfaceOutputFacade outputFacade, GameController gameController, TrackingManager trackingManager, TrackingResultProcessing trackingResultProcessing)
        {
            StateContext = new StateContext(null, outputFacade, gameController, trackingManager, trackingResultProcessing);
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

        public void ErrorInTracking(string message)
        {
            StateContext.InternalState.ErrorInTracking(message);
        }

        public void TrackingStartSuccessful()
        {
            StateContext.InternalState.TrackingStartSuccessful();
        }

        public void GameRecognized()
        {
            StateContext.InternalState.GameRecognized();
        }
    }
}
