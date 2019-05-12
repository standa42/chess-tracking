using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.Concurrent;
using ChessTracking.MultithreadingMessages;
using ChessTracking.ProcessingElements;
using ChessTracking.ProcessingPipeline;
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

        public TrackingManager(UserInterfaceOutputFacade outputFacade, TrackingResultProcessing trackingResultProcessing)
        {
            this.OutputFacade = outputFacade;
            TrackingResultProcessing = trackingResultProcessing;

            ProcessingCommandsQueue = new BlockingCollection<Message>();
            ProcessingOutputQueue = new BlockingCollection<Message>();

            InitPipelineThread();
        }

        /// <summary>
        /// Initialization of tracking thread
        /// </summary>
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

        public void CalibrateColor(double additiveConstant)
        {
            ProcessingCommandsQueue.Add(new ColorCalibrationMessage(additiveConstant));
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
