using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.Concurrent;
using ChessTracking.Forms;
using ChessTracking.MultithreadingMessages;
using ChessTracking.ProcessingElements;

namespace ChessTracking.ControllingElements
{
    class TrackingManager
    {
        public MainGameForm GameForm { get; }
        public BlockingCollection<Message> ProcessingCommandsQueue { get; set; }
        public BlockingCollection<Message> ProcessingOutputQueue { get; set; }
        public FPSCounter FpsCounter;

        public TrackingManager(MainGameForm mainGameForm)
        {
            this.GameForm = mainGameForm;
            ProcessingCommandsQueue = new BlockingCollection<Message>();
            ProcessingOutputQueue = new BlockingCollection<Message>();
            FpsCounter = new FPSCounter();;

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

        public void ProcessQueue()
        {
            bool messageProcessed = false;

            do
            {
                messageProcessed = ProcessingOutputQueue.TryTake(out var message);

                if (message is ResultMessage resultMessage)
                {
                    UpdateFps();
                    GameForm.DisplayVizuaization(resultMessage.BitmapToDisplay);
                    GameForm.UpdateImmediateBoard(resultMessage.FiguresBitmap);
                }
            } while (messageProcessed);
        }

        private void UpdateFps()
        {
            int? fps = FpsCounter.Update();
            if (fps != null)
            {
                GameForm.UpdateFPS(fps.Value);
            }
        }
    }
}
