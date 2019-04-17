using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ChessTracking.MultithreadingMessages;
using ChessTracking.UserInterface;

namespace ChessTracking.ControllingElements
{
    class TrackingResultReceiver
    {
        private UserInterfaceOutputFacade OutputFacade { get; }
        
        private BlockingCollection<Message> ProcessingOutputQueue { get; }
        private TrackingResultProcessing TrackingResultProcessing { get; }

        public TrackingResultReceiver(UserInterfaceOutputFacade outputFacade, BlockingCollection<Message> processingOutputQueue, TrackingResultProcessing trackingResultProcessing)
        {
            OutputFacade = outputFacade;
            ProcessingOutputQueue = processingOutputQueue;
            
            this.TrackingResultProcessing = trackingResultProcessing;
        }

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
                
                if(messageProcessed && message == null)
                {
                    throw new InvalidOperationException($"Unexpected message in {nameof(TrackingResultReceiver)}");
                }

            } while (messageProcessed);
        }
        
    }
}
