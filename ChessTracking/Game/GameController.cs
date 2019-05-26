using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Accord.IO;
using ChessTracking.MultithreadingMessages;
using ChessTracking.UserInterface;

namespace ChessTracking.Game
{
    class GameController
    {
        private UserInterfaceOutputFacade OutputFacade { get; }
        private GameData Game { get; set; }

        public GameController(UserInterfaceOutputFacade outputFacade)
        {
            OutputFacade = outputFacade;
        }

        public void NewGame()
        {
            Game = GameFactory.NewGame();

            OutputFacade.UpdateBoardState(RenderGameState());
        }

        public void SaveGame(StreamWriter stream)
        {
            stream.Write(Game.ExportGameToAlgebraicNotation());
        }

        public void LoadGame(StreamReader stream)
        {
            var loadingResult = GameFactory.LoadGame(stream);

            if (loadingResult.LoadingSuccesfull)
            {
                Game = loadingResult.Game;
                OutputFacade.UpdateRecordState(Game.RecordOfGame);
                OutputFacade.UpdateBoardState(RenderGameState());
            }
            else
            {
                OutputFacade.UpdateRecordState(new List<string>(){"Game loading failed"});
            }
        }

        public Bitmap RenderGameState()
        {
            return GameRenderer.RenderGameState(Game);
        }

        public int? InitiateWithTracingInput(TrackingState trackingState)
        {
            var figures = Game.Chessboard.GetTrackingStates().Figures;

            var chessboardState = new TrackingState(figures);

            for (int i = 0; i < 4; i++)
            {
                if (TrackingState.IsEquivalent(chessboardState, trackingState))
                {
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
                else
                    OutputFacade.AddToUserLog("ValidationError");

                OutputFacade.UpdateRecordState(Game.RecordOfGame);
                OutputFacade.UpdateBoardState(RenderGameState());

                if (Game.EndState != GameState.StillPlaying)
                {
                    // do some stopping of everything}
                    OutputFacade.AddToUserLog("Game ended");
                    if (Game.EndState == GameState.BlackWin)
                    {
                        OutputFacade.AddToUserLog("Black won");
                        Game.RecordOfGame.Add("0-1");
                    }
                    if (Game.EndState == GameState.WhiteWin)
                    {
                        OutputFacade.AddToUserLog("White won");
                        Game.RecordOfGame.Add("1-0");
                    }
                    if (Game.EndState == GameState.Draw)
                    {
                        OutputFacade.AddToUserLog("Its a draw");
                        Game.RecordOfGame.Add("1/2-1/2");
                    }
                }

                
            }
        }
        
    }
}
