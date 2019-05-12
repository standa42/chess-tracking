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
            Game = GameFactory.LoadGame(stream);

            OutputFacade.UpdateBoardState(RenderGameState());
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
            if (Game.EndState == GameWinState.StillPlaying)
            {
                var validationResult = GameValidator.ValidateAndPerform(Game.DeepClone(), trackingState); // get from validator

                if (validationResult.IsValid)
                    Game = validationResult.NewGameState;
                else
                    OutputFacade.AddToUserLog("ValidationError");

                OutputFacade.UpdateRecordState(Game.RecordOfGame);
                OutputFacade.UpdateBoardState(RenderGameState());

                if (Game.EndState != GameWinState.StillPlaying)
                {
                    // do some stopping of everything}
                    OutputFacade.AddToUserLog("Game ended");
                    if (Game.EndState == GameWinState.BlackWin)
                        OutputFacade.AddToUserLog("Black won");
                    if (Game.EndState == GameWinState.WhiteWin)
                        OutputFacade.AddToUserLog("White won");
                    if (Game.EndState == GameWinState.Draw)
                        OutputFacade.AddToUserLog("Its a draw");
                }

                
            }
        }
        
    }
}
