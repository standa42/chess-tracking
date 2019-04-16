using System;
using System.Collections.Concurrent;
using ChessTracking.MultithreadingMessages;
using ChessTracking.ProcessingElements;

namespace ChessTracking.ProcessingPipeline
{
    class PipelineController
    {
        public BlockingCollection<Message> ProcessingCommandsQueue { get; }
        public BlockingCollection<Message> ProcessingOutputQueue { get; }
        public Kinect Kinect;
        public Pipeline Pipeline;

        public PipelineController(BlockingCollection<Message> processingCommandsQueue, BlockingCollection<Message> processingOutputQueue)
        {
            ProcessingCommandsQueue = processingCommandsQueue;
            ProcessingOutputQueue = processingOutputQueue;
            Pipeline = new Pipeline(ProcessingOutputQueue);
        }

        public void Start()
        {
            while (!ProcessingCommandsQueue.IsCompleted)
            {
                dynamic message = ProcessingCommandsQueue.Take();
                Process(message);
            }
        }

        private void Process(VisualisationChangeMessage msg)
        {
            Pipeline.ChangeVisualisationState(msg.VisualisationType);
        }

        private void Process(KinectResourcesMessage msg)
        {
            Pipeline.ProcessIncomingKinectData(msg);
        }

        private void Process(CommandMessage msg)
        {
            switch (msg.MessageType)
            {
                case CommandMessageType.StartTracking:
                    Kinect = new Kinect(ProcessingCommandsQueue);
                    break;
                case CommandMessageType.StopTracking:
                    Kinect.Dispose();
                    Kinect = null;
                    break;
                case CommandMessageType.Recalibrate:
                    Pipeline.Recalibrate();
                    break;
                default:
                    throw new InvalidOperationException($"Unexpected message type in {nameof(PipelineController)}");
            }
        }
        
    }
}
