using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChessTracking.MultithreadingMessages
{
    class VisualisationChangeMessage : Message
    {
        public VisualisationType VisualisationType { get; set; }

        public VisualisationChangeMessage(VisualisationType visualisationType)
        {
            VisualisationType = visualisationType;
        }
    }
}
