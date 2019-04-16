using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.Concurrent;
using ChessTracking.Forms;
using ChessTracking.MultithreadingMessages;
using ChessTracking.ProcessingElements;
using ChessTracking.ProcessingPipeline;
using ChessTracking.UserInterface;

namespace ChessTracking.ControllingElements
{
    class TrackingManager
    {
        public UserInterfaceOutputFacade OutputFacade { get; }
        public BlockingCollection<Message> ProcessingOutputQueue { get; }
        private BlockingCollection<Message> ProcessingCommandsQueue { get; }
        
        
        public TrackingManager(UserInterfaceOutputFacade outputFacade)
        {
            this.OutputFacade = outputFacade;
            ProcessingCommandsQueue = new BlockingCollection<Message>();
            ProcessingOutputQueue = new BlockingCollection<Message>();

            InitPipelineThread();
        }

        private void InitPipelineThread()
        {
            Task.Run(() =>
            {
                var processingController = new PipelineController(ProcessingCommandsQueue, ProcessingOutputQueue);
                processingController.Start();
            });
        }

        public void StartTracking()
        {
            ProcessingCommandsQueue.Add(new CommandMessage(CommandMessageType.StartTracking));
        }

        public void StopTracking()
        {
            ProcessingCommandsQueue.Add(new CommandMessage(CommandMessageType.StopTracking));
        }

        public void Recalibrate()
        {
            ProcessingCommandsQueue.Add(new CommandMessage(CommandMessageType.Recalibrate));
        }

        public void ChangeVisualisation(VisualisationType newVisualisationType)
        {
            ProcessingCommandsQueue.Add(new VisualisationChangeMessage(newVisualisationType));
        }
    }
}
