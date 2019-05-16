using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Accord.IO;

namespace ChessTracking.Game
{
    /// <summary>
    /// Creates chess game instances
    /// </summary>
    public static class GameFactory
    {
        /// <summary>
        /// Creates new game of chess
        /// </summary>
        /// <returns>Description state of game</returns>
        public static GameData NewGame()
        {
            var figures = new Figure[8, 8];

            figures[0, 0] = new Figure(FigureType.Rook, PlayerColor.White);
            figures[1, 0] = new Figure(FigureType.Knight, PlayerColor.White);
            figures[2, 0] = new Figure(FigureType.Bishop, PlayerColor.White);
            figures[3, 0] = new Figure(FigureType.Queen, PlayerColor.White);
            figures[4, 0] = new Figure(FigureType.King, PlayerColor.White);
            figures[5, 0] = new Figure(FigureType.Bishop, PlayerColor.White);
            figures[6, 0] = new Figure(FigureType.Knight, PlayerColor.White);
            figures[7, 0] = new Figure(FigureType.Rook, PlayerColor.White);

            figures[0, 1] = new Figure(FigureType.Pawn, PlayerColor.White);
            figures[1, 1] = new Figure(FigureType.Pawn, PlayerColor.White);
            figures[2, 1] = new Figure(FigureType.Pawn, PlayerColor.White);
            figures[3, 1] = new Figure(FigureType.Pawn, PlayerColor.White);
            figures[4, 1] = new Figure(FigureType.Pawn, PlayerColor.White);
            figures[5, 1] = new Figure(FigureType.Pawn, PlayerColor.White);
            figures[6, 1] = new Figure(FigureType.Pawn, PlayerColor.White);
            figures[7, 1] = new Figure(FigureType.Pawn, PlayerColor.White);

            figures[0, 7] = new Figure(FigureType.Rook, PlayerColor.Black);
            figures[1, 7] = new Figure(FigureType.Knight, PlayerColor.Black);
            figures[2, 7] = new Figure(FigureType.Bishop, PlayerColor.Black);
            figures[3, 7] = new Figure(FigureType.Queen, PlayerColor.Black);
            figures[4, 7] = new Figure(FigureType.King, PlayerColor.Black);
            figures[5, 7] = new Figure(FigureType.Bishop, PlayerColor.Black);
            figures[6, 7] = new Figure(FigureType.Knight, PlayerColor.Black);
            figures[7, 7] = new Figure(FigureType.Rook, PlayerColor.Black);

            figures[0, 6] = new Figure(FigureType.Pawn, PlayerColor.Black);
            figures[1, 6] = new Figure(FigureType.Pawn, PlayerColor.Black);
            figures[2, 6] = new Figure(FigureType.Pawn, PlayerColor.Black);
            figures[3, 6] = new Figure(FigureType.Pawn, PlayerColor.Black);
            figures[4, 6] = new Figure(FigureType.Pawn, PlayerColor.Black);
            figures[5, 6] = new Figure(FigureType.Pawn, PlayerColor.Black);
            figures[6, 6] = new Figure(FigureType.Pawn, PlayerColor.Black);
            figures[7, 6] = new Figure(FigureType.Pawn, PlayerColor.Black);

            var game = new GameData(new ChessboardModel(figures), PlayerColor.White, GameState.StillPlaying);

            return game;
        }

        /// <summary>
        /// Loads game of chess from incoming stream
        /// </summary>
        /// <param name="stream">Record of loaded game</param>
        /// <returns>Description state of game</returns>
        public static GameData LoadGame(StreamReader stream)
        {
            var game = NewGame();

            while (!stream.EndOfStream)
            {
                if (game.EndState == GameState.StillPlaying)
                {
                    var validationResult = GameValidator.ValidateAndPerform(game.DeepClone(), stream.ReadLine()); // get from validator

                    if (validationResult.IsValid)
                        game = validationResult.NewGameState;

                    if (game.EndState != GameState.StillPlaying)
                    {
                        if (game.EndState == GameState.BlackWin)
                        {
                            game.RecordOfGame.Add("0-1");
                        }
                        if (game.EndState == GameState.WhiteWin)
                        {
                            game.RecordOfGame.Add("1-0");
                        }
                        if (game.EndState == GameState.Draw)
                        {
                            game.RecordOfGame.Add("1/2-1/2");
                        }
                    }
                }
                else
                {
                    break;
                }
            }
            
            return game; // TODO: return loadresult if it was valid, update fields, lock buttons if st game is complete, atd..
        }
    }
}
