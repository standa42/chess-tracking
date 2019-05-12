using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChessTracking.MultithreadingMessages
{
    /// <summary>
    /// Message transfering demand to change visualisation state
    /// </summary>
    class VisualisationChangeMessage : Message
    {
        public VisualisationType VisualisationType { get; set; }

        public VisualisationChangeMessage(VisualisationType visualisationType)
        {
            VisualisationType = visualisationType;
        }
    }
}
