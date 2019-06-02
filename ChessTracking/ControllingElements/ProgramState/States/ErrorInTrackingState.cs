using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChessTracking.ControllingElements.ProgramState.States
{
    /// <summary>
    /// State representing that there is an error in tracking procedure
    /// </summary>
    class ErrorInTrackingState : ProgramState
    {
        public ErrorInTrackingState(StateContext stateContext) : base(stateContext)
        {

        }
    }
}
