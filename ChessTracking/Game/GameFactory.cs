using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChessTracking.Game
{
    public static class GameFactory
    {
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

            var game = new GameData(new ChessboardModel(figures), PlayerColor.White, null, null);

            return game;
        }

        public static GameData LoadGame(StreamReader stream)
        {
            var game = NewGame();

            // TODO perform game read from stream

            return game;
        }
    }
}
