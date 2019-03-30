using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using ChessboardTrackingOctoberEdition.TrackingPart;

namespace ChessboardTrackingOctoberEdition.UserPart
{
    class KinectSwitch
    {
        private StateAutomataWrapper stateAutomataWrapper;
        private CancellationTokenSource tokenSource;

        public void StartTracking(UserPartForm userForm, TrackingPartForm trackingForm)
        {
            if (stateAutomataWrapper != null)
            {
                StopTracking();
                Thread.Sleep(500);
            }

            tokenSource = new CancellationTokenSource();
            var token = tokenSource.Token;

            stateAutomataWrapper = new StateAutomataWrapper();
            
            Task.Run(() =>
            {
                stateAutomataWrapper.StartTracking(userForm, trackingForm, token);
            }, token);
        }

        public void StopTracking()
        {
            tokenSource?.Cancel();
        }
    }
}
