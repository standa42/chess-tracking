using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ChessTracking.ControllingElements;
using ChessTracking.Game;
using ChessTracking.MultithreadingMessages;

namespace ChessTracking.UserInterface
{
    class UserInterfaceInputFacade
    {
        private UserInterfaceOutputFacade OutputFacade { get; }
        private TrackingManager TrackingManager { get; }
        private TrackingResultReceiver TrackingResultReceiver { get; }
        private TrackingResultProcessing TrackingResultProcessing { get; }
        private GameController GameController { get; }

        public UserInterfaceInputFacade(UserInterfaceOutputFacade outputFacade)
        {
            OutputFacade = outputFacade;
            GameController = new GameController(outputFacade);
            TrackingManager = new TrackingManager(OutputFacade);
            TrackingResultProcessing = new TrackingResultProcessing(outputFacade, GameController);
            TrackingResultReceiver = new TrackingResultReceiver(outputFacade, TrackingManager.ProcessingOutputQueue, TrackingResultProcessing);
        }

        public void NewGame()
        {
            GameController.NewGame();
        }

        public void SaveGame()
        {
            GameController.SaveGame();
        }

        public void LoadGame()
        {
            GameController.LoadGame();
        }

        public void ChangeVisualisation(VisualisationType type)
        {
            TrackingManager.ChangeVisualisation(type);
        }

        public void StartTracking()
        {
            TrackingManager.StartTracking();
        }

        public void StopTracking()
        {
            TrackingManager.StopTracking();
        }

        public void Recalibrate()
        {
            TrackingManager.Recalibrate();
        }

        public void ProcessQueueTick()
        {
            TrackingResultReceiver.ProcessQueue();
        }
    }
}
