using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ChessTracking.ControllingElements;
using ChessTracking.ControllingElements.ProgramState;
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
        private IProgramState ProgramState { get; }

        public UserInterfaceInputFacade(UserInterfaceOutputFacade outputFacade, UserDefinedParametersPrototypeFactory userParameters)
        {
            var programStateController = new ProgramStateController();
            ProgramState = programStateController;

            OutputFacade = outputFacade;
            GameController = new GameController(outputFacade, ProgramState);
            TrackingResultProcessing = new TrackingResultProcessing(outputFacade, GameController, ProgramState);
            TrackingManager = new TrackingManager(OutputFacade, TrackingResultProcessing, userParameters, ProgramState);

            programStateController.SetInitialContext(outputFacade, GameController, TrackingManager, TrackingResultProcessing);
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

        public void EndGame()
        {
            GameController.EndGame();
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

        /// <summary>
        /// Tick to process queue from tracking thread
        /// </summary>
        public void ProcessQueueTick()
        {
            TrackingManager.ProcessQueue();
        }
    }
}
