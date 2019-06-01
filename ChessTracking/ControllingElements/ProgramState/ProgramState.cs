using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace ChessTracking.ControllingElements.ProgramState
{
    abstract class ProgramState : IProgramState
    {
        protected StateContext StateContext { get; }

        protected ProgramState(StateContext stateContext)
        {
            StateContext = stateContext;
        }

        public virtual void GameLoaded()
        {
            throw new NotImplementedException();
        }

        public virtual void GameEnded()
        {
            throw new NotImplementedException();
        }

        public virtual void StartedTracking()
        {
            throw new NotImplementedException();
        }

        public virtual void StoppedTracking()
        {
            throw new NotImplementedException();
        }

        public virtual void Recalibrating()
        {
            throw new NotImplementedException();
        }

        public virtual void GameFinished()
        {
            throw new NotImplementedException();
        }

        public virtual void ErrorInTracking()
        {
            throw new NotImplementedException();
        }

        public virtual void TrackingStartSuccessful()
        {
            throw new NotImplementedException();
        }

        public virtual void GameRecognized()
        {
            throw new NotImplementedException();
        }
    }
}
