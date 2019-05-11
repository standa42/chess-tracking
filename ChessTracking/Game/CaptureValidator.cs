using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChessTracking.Game
{
    static class CaptureValidator
    {
        public static ValidationResult ValidateAndPerformMove(GameData game, FigureType? figure, ChessPosition from, ChessPosition to)
        {
            return new ValidationResult(true, game);
        }
    }
}
