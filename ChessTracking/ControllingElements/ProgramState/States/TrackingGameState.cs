using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChessTracking.ControllingElements.ProgramState.States
{
    class TrackingGameState : ProgramState
    {
        public TrackingGameState(StateContext stateContext) : base(stateContext)
        {

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
