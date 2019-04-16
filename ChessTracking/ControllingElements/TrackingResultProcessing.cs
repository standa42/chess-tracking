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
    class TrackingResultProcessing
    {
        private UserInterfaceOutputFacade OutputFacade { get; }
        public FPSCounter FpsCounter { get; }
        private BlockingCollection<Message> ProcessingOutputQueue { get; }

        public TrackingResultProcessing(UserInterfaceOutputFacade outputFacade, BlockingCollection<Message> processingOutputQueue)
        {
            OutputFacade = outputFacade;
            ProcessingOutputQueue = processingOutputQueue;
            FpsCounter = new FPSCounter();
        }

        public void ProcessQueue()
        {
            bool messageProcessed = false;

            do
            {
                messageProcessed = ProcessingOutputQueue.TryTake(out var message);

                if (message is ResultMessage resultMessage)
                {
                    UpdateFps();
                    OutputFacade.DisplayVizuaization(resultMessage.BitmapToDisplay);
                    OutputFacade.UpdateImmediateBoard(resultMessage.FiguresBitmap);
                }
                
                if(messageProcessed && message == null)
                {
                    throw new InvalidOperationException($"Unexpected message in {nameof(TrackingResultProcessing)}");
                }

            } while (messageProcessed);
        }

        private void UpdateFps()
        {
            int? fps = FpsCounter.Update();
            if (fps != null)
            {
                OutputFacade.UpdateFps(fps.Value);
            }
        }
    }
}
