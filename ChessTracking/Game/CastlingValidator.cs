using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChessTracking.Game
{
    static class CastlingValidator
    {
        public static ValidationResult ValidateAndPerformMove(GameData game, bool kingSide)
        {
            if (game.PlayerOnMove == PlayerColor.White)
            {
                if (kingSide)
                    return ValidateAndPerformMove(
                        game,
                        new ChessPosition(4, 0),
                        new ChessPosition(5, 0),
                        new ChessPosition(6, 0),
                        new ChessPosition(7, 0)
                    );
                else
                    return ValidateAndPerformMove(
                        game,
                        new ChessPosition(0, 0),
                        new ChessPosition(2, 0),
                        new ChessPosition(3, 0),
                        new ChessPosition(4, 0)
                    );
            }
            else
            {
                if (kingSide)
                    return ValidateAndPerformMove(
                        game,
                        new ChessPosition(4, 7),
                        new ChessPosition(5, 7),
                        new ChessPosition(6, 7),
                        new ChessPosition(7, 7)
                    );
                else
                    return ValidateAndPerformMove(
                        game,
                        new ChessPosition(0, 7),
                        new ChessPosition(2, 7),
                        new ChessPosition(3, 7),
                        new ChessPosition(4, 7)
                    );
            }
        }

        public static ValidationResult ValidateAndPerformMove(GameData game, ChessPosition from, ChessPosition from2, ChessPosition to, ChessPosition to2)
        {
            var positionsOrderedByX = (new List<ChessPosition>() { from, from2, to, to2 }.OrderBy(x => x.X));

            if (game.PlayerOnMove == PlayerColor.White)
            {
                
            }
            return new ValidationResult(true, game);
        }
    }
}
