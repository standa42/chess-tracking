using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ChessboardTrackingOctoberEdition.TrackingPart;

namespace ChessboardTrackingOctoberEdition.UserPart
{
    class Game
    {
        private KinectSwitch kinectSwitch;

        public Game()
        {
            kinectSwitch = new KinectSwitch();
        }

        public void StartTracking(UserPartForm userForm, TrackingPartForm trackingForm)
        {
            kinectSwitch.StartTracking(userForm, trackingForm);
        }

        public void StopTracking()
        {
            kinectSwitch.StopTracking();
        }
    }
}
