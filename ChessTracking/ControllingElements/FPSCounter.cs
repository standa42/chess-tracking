using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChessTracking.ControllingElements
{
    class FPSCounter
    {
        private DateTime LastUpdate { get; set; }
        public int Counter { get; set; }

        public FPSCounter()
        {
            LastUpdate = DateTime.Now;
        }

        /// <summary>
        /// Notifies counter, that new image arrived
        /// </summary>
        /// <returns>FPS count if a second elapsed from last information, null otherwise</returns>
        public int? Update()
        {
            Counter++;

            var currentTime = DateTime.Now;
            var elapsedSeconds = (currentTime - LastUpdate).TotalSeconds;

            if (elapsedSeconds > 1)
            {
                LastUpdate = currentTime;
                int fps = Counter / (int)Math.Floor(elapsedSeconds);
                Counter = 0;
                return fps;
            }
            else
            {
                return null;
            }
        }
    }
}
