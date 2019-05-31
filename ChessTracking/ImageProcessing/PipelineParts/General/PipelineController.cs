using System;
using System.Collections.Concurrent;
using ChessTracking.ImageProcessing.PipelineData;
using ChessTracking.MultithreadingMessages;

namespace ChessTracking.ImageProcessing.PipelineParts.General
{
    class PipelineController
    {
        public BlockingCollection<Message> ProcessingCommandsQueue { get; }
        public BlockingCollection<Message> ProcessingOutputQueue { get; }
        public Kinect Kinect;
        public Pipeline Pipeline;
        

        public PipelineController(BlockingCollection<Message> processingCommandsQueue, BlockingCollection<Message> processingOutputQueue, UserDefinedParametersPrototypeFactory userParameters)
        {
            ProcessingCommandsQueue = processingCommandsQueue;
            ProcessingOutputQueue = processingOutputQueue;
            Pipeline = new Pipeline(ProcessingOutputQueue, userParameters);
        }

        /// <summary>
        /// Get and dispatch messages from user and kinect threads
        /// </summary>
        public void Start()
        {
            while (!ProcessingCommandsQueue.IsCompleted)
            {
                dynamic message = ProcessingCommandsQueue.Take();
                Process(message);
            }
        }

        private void Process(KinectResourcesMessage msg)
        {
            Pipeline.ProcessIncomingKinectData(msg);
        }

        /// <summary>
        /// Process command messages
        /// </summary>
        /// <param name="msg"></param>
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
