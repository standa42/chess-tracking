using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ChessTracking.MultithreadingMessages;

namespace ChessTracking.Game
{
    public class GameData
    {
        public ChessboardModel Chessboard;
        public PlayerColor PlayerOnMove { get; set; }
        public PlayerColor? PlayerWon { get; set; }
        public ChessPosition EnPassantPosition { get; set; }
        public List<string> RecordOfGame { get; set; }
        
        public GameData(ChessboardModel chessboard, PlayerColor playerOnMove, ChessPosition enPassantPosition, PlayerColor? playerWon)
        {
            Chessboard = chessboard;
            PlayerOnMove = playerOnMove;
            EnPassantPosition = enPassantPosition;
            PlayerWon = playerWon;
            RecordOfGame = new List<string>();
        }

        public string ExportGameToAlgebraicNotation()
        {
            var acumulator = new StringBuilder("");

            foreach (var record in RecordOfGame)
            {
                acumulator.Append(record + Environment.NewLine);
            }

            return acumulator.ToString();
        }

    }
}
