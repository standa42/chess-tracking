using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ChessTracking.Game;
using ChessTracking.UserInterface;

namespace ChessTracking.ControllingElements.ProgramState
{
    class StateContext
    {
        public IProgramState InternalState { get; set; }
        public UserInterfaceOutputFacade OutputFacade { get; }
        public GameController GameController { get; }
        public TrackingManager TrackingManager { get; }
        public TrackingResultProcessing TrackingResultProcessing { get; }

        public StateContext(IProgramState internalState, UserInterfaceOutputFacade outputFacade, GameController gameController, TrackingManager trackingManager, TrackingResultProcessing trackingResultProcessing)
        {
            InternalState = internalState;
            OutputFacade = outputFacade;
            GameController = gameController;
            TrackingManager = trackingManager;
            TrackingResultProcessing = trackingResultProcessing;
        }
    }
}
