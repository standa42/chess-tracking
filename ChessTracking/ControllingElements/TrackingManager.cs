using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.Concurrent;
using ChessTracking.ImageProcessing.PipelineData;
using ChessTracking.ImageProcessing.PipelineParts;
using ChessTracking.MultithreadingMessages;
using ChessTracking.UserInterface;

namespace ChessTracking.ControllingElements
{
    /// <summary>
    /// Maintains communication with tracking thread
    /// </summary>
    class TrackingManager
    {
        public UserInterfaceOutputFacade OutputFacade { get; }
        private TrackingResultProcessing TrackingResultProcessing { get; }

        /// <summary>
        /// Queue of messages arriving from tracking thread
        /// </summary>
        public BlockingCollection<Message> ProcessingOutputQueue { get; }

        /// <summary>
        /// Queue pointing to tracking thread
        /// </summary>
        private BlockingCollection<Message> ProcessingCommandsQueue { get; }

        public TrackingManager(UserInterfaceOutputFacade outputFacade, TrackingResultProcessing trackingResultProcessing, UserDefinedParametersFactory userParameters)
        {
            this.OutputFacade = outputFacade;
            TrackingResultProcessing = trackingResultProcessing;

            ProcessingCommandsQueue = new BlockingCollection<Message>();
            ProcessingOutputQueue = new BlockingCollection<Message>();

            InitPipelineThread(userParameters);
        }

        /// <summary>
        /// Initialization of tracking thread
        /// </summary>
        private void InitPipelineThread(UserDefinedParametersFactory userParameters)
        {
            Task.Run(() =>
            {
                var processingController = new PipelineController(ProcessingCommandsQueue, ProcessingOutputQueue, userParameters);
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
        
        /// <summary>
        /// Processing of messages arriving from tracking thread
        /// </summary>
        public void ProcessQueue()
        {
            bool messageProcessed = false;

            do
            {
                messageProcessed = ProcessingOutputQueue.TryTake(out var message);

                if (message is ResultMessage resultMessage)
                {
                    TrackingResultProcessing.ProcessResult(resultMessage);
                }

                if (messageProcessed && message == null)
                {
                    throw new InvalidOperationException($"Unexpected incoming message in {nameof(TrackingManager)}");
                }

            } while (messageProcessed);
        }
    }
}
