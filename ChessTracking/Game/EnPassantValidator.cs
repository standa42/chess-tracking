using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChessTracking.Game
{
    static class EnPassantValidator
    {
        public static ValidationResult ValidateAndPerformMove(GameData game, ChessPosition from, ChessPosition to, ChessPosition taking)
        {
            if (taking.IsEquivalent(game.EnPassantPosition))
            {
                if (
                    (
                    game.PlayerOnMove == PlayerColor.White &&
                    game.Chessboard.GetFigureOnPosition(from).Fullfils(PlayerColor.White, FigureType.Pawn) &&
                    game.Chessboard.GetFigureOnPosition(taking).Fullfils(PlayerColor.Black, FigureType.Pawn) &&
                    game.Chessboard.GetFigureOnPosition(to) == null &&
                    ((to.X == from.X + 1) || (to.X == from.X - 1)) &&
                    to.Y == from.Y + 1
                    )
                    ||
                    (
                    game.PlayerOnMove == PlayerColor.Black &&
                    game.Chessboard.GetFigureOnPosition(from).Fullfils(PlayerColor.Black, FigureType.Pawn) &&
                    game.Chessboard.GetFigureOnPosition(taking).Fullfils(PlayerColor.White, FigureType.Pawn) &&
                    game.Chessboard.GetFigureOnPosition(to) == null &&
                    ((to.X == from.X + 1) || (to.X == from.X - 1)) &&
                    to.Y == from.Y - 1
                    )
                   )
                {
                    game.Chessboard.Delete(taking);
                    game.Chessboard.MoveTo(from, to);
                    game.RecordOfGame.Add($"{from.ToChessString()}x{to.ToChessString()}");
                    return new ValidationResult(true, game);
                }
            }

            return new ValidationResult(false, game);
        }
    }
}
