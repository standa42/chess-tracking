using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ChessTracking.MultithreadingMessages;

namespace ChessTracking.Game
{
    [Serializable]
    public class GameData
    {
        public ChessboardModel Chessboard;
        public PlayerColor PlayerOnMove { get; set; }
        public GameWinState EndState { get; set; }
        public ChessPosition EnPassantPosition { get; set; }
        public List<string> RecordOfGame { get; set; }
        
        public GameData(ChessboardModel chessboard, PlayerColor playerOnMove, ChessPosition enPassantPosition, GameWinState endState)
        {
            Chessboard = chessboard;
            PlayerOnMove = playerOnMove;
            EnPassantPosition = enPassantPosition;
            EndState = endState;
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
