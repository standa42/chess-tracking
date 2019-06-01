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

        public StateContext(IProgramState internalState, UserInterfaceOutputFacade outputFacade, GameController gameController)
        {
            InternalState = internalState;
            OutputFacade = outputFacade;
            GameController = gameController;
        }
    }
}
