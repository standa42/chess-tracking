using System;
using System.Collections.Concurrent;
using ChessTracking.ImageProcessing.PipelineData;
using ChessTracking.MultithreadingMessages;
using ChessTracking.MultithreadingMessages.ToProcessing;

namespace ChessTracking.ImageProcessing.PipelineParts.General
{
    class PipelineController
    {
        public BlockingCollection<Message> ProcessingCommandsQueue { get; }
        public BlockingCollection<Message> ProcessingOutputQueue { get; }
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

        private void Process(KinectUpdateMessage msg)
        {
            Pipeline.Update();
        }

        private void Process(StartTrackingMessage msg)
        {
            Pipeline.SetBuffer(msg.Buffer);
            Pipeline.ResetCalibration();
        }

        private void Process(RecalibrateMessage msg)
        {
            Pipeline.ResetCalibration();
        }

        private void Process(ChessboardMovementMessage msg)
        {
            Pipeline.MoveChessboard(msg.Movement);
        }

    }
}
