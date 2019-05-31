using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ChessTracking.ControllingElements;
using ChessTracking.Game;
using ChessTracking.ImageProcessing.PipelineData;
using ChessTracking.MultithreadingMessages;

namespace ChessTracking.UserInterface
{
    class UserInterfaceInputFacade
    {
        private UserInterfaceOutputFacade OutputFacade { get; }
        private TrackingManager TrackingManager { get; }
        private TrackingResultProcessing TrackingResultProcessing { get; }
        private GameController GameController { get; }

        public UserInterfaceInputFacade(UserInterfaceOutputFacade outputFacade, UserDefinedParametersFactory userParameters)
        {
            OutputFacade = outputFacade;
            GameController = new GameController(outputFacade);
            TrackingResultProcessing = new TrackingResultProcessing(outputFacade, GameController);
            TrackingManager = new TrackingManager(OutputFacade, TrackingResultProcessing, userParameters);
        }

        public void NewGame()
        {
            GameController.NewGame();
        }

        public void SaveGame(StreamWriter stream)
        {
            GameController.SaveGame(stream);
        }

        public void LoadGame(StreamReader stream)
        {
            GameController.LoadGame(stream);
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
            TrackingResultProcessing.Reset();
        }

        /// <summary>
        /// Tick to process queue from tracking thread
        /// </summary>
        public void ProcessQueueTick()
        {
            TrackingManager.ProcessQueue();
        }
    }
}
