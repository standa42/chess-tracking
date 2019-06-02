using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Accord.IO;
using ChessTracking.ControllingElements.ProgramState;
using ChessTracking.MultithreadingMessages;
using ChessTracking.UserInterface;

namespace ChessTracking.Game
{
    class GameController
    {
        private UserInterfaceOutputFacade OutputFacade { get; }
        private GameData Game { get; set; }
        private IProgramState ProgramState { get; }

        public GameController(UserInterfaceOutputFacade outputFacade, IProgramState programState)
        {
            OutputFacade = outputFacade;
            ProgramState = programState;
        }

        public void NewGame()
        {
            Game = GameFactory.NewGame();

            ProgramState.GameLoaded();
            OutputFacade.UpdateBoardState(RenderGameState());
        }

        public void SaveGame(StreamWriter stream)
        {
            stream.Write(Game.ExportGameToAlgebraicNotation());
            OutputFacade.AddToTrackingLog("Game saved");
        }

        public void LoadGame(StreamReader stream)
        {
            var loadingResult = GameFactory.LoadGame(stream);

            if (loadingResult.LoadingSuccesfull)
            {
                Game = loadingResult.Game;

                if(Game.EndState == GameState.StillPlaying)
                    ProgramState.GameLoaded();
                else
                    ProgramState.GameFinished();

                OutputFacade.UpdateRecordState(Game.RecordOfGame);
                OutputFacade.UpdateBoardState(RenderGameState());
            }
            else
            {
                OutputFacade.AddToTrackingLog("Game loading failed");
            }
        }

        public void EndGame()
        {
            Game = null;
            ProgramState.GameEnded();
        }

        public Bitmap RenderGameState()
        {
            return GameRenderer.RenderGameState(Game);
        }

        public TrackingState GetTrackingState()
        {
            return Game?.Chessboard?.GetTrackingStates();
        }

        public int? InitiateWithTracingInput(TrackingState trackingState)
        {
            var figures = Game.Chessboard.GetTrackingStates().Figures;

            var chessboardState = new TrackingState(figures);

            for (int i = 0; i < 4; i++)
            {
                if (TrackingState.IsEquivalent(chessboardState, trackingState))
                {
                    OutputFacade.UpdateValidationState(true);
                    return i;
                }
                trackingState.RotateClockWise(1);
            }

            return null;
        }

        public void TryChangeChessboardState(TrackingState trackingState)
        {
            if (Game.EndState == GameState.StillPlaying)
            {
                var validationResult = GameValidator.ValidateAndPerform(Game.DeepClone(), trackingState); // get from validator

                if (validationResult.IsValid)
                    Game = validationResult.NewGameState;

                if(trackingState.IsEquivalentTo(Game.Chessboard.GetTrackingStates()))
                    OutputFacade.UpdateValidationState(true);
                else
                    OutputFacade.UpdateValidationState(validationResult.IsValid);

                if (Game.EndState != GameState.StillPlaying)
                {
                    ProgramState.GameFinished();
                    // do some stopping of everything}
                    OutputFacade.AddToTrackingLog("Game ended");
                    if (Game.EndState == GameState.BlackWin)
                    {
                        OutputFacade.AddToTrackingLog("Black won");
                        Game.RecordOfGame.Add("0-1");
                    }
                    if (Game.EndState == GameState.WhiteWin)
                    {
                        OutputFacade.AddToTrackingLog("White won");
                        Game.RecordOfGame.Add("1-0");
                    }
                    if (Game.EndState == GameState.Draw)
                    {
                        OutputFacade.AddToTrackingLog("Its a draw");
                        Game.RecordOfGame.Add("1/2-1/2");
                    }
                }

                OutputFacade.UpdateRecordState(Game.RecordOfGame);
                OutputFacade.UpdateBoardState(RenderGameState());


            }
        }
        
    }
}
