using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChessTracking.Game
{
    public class ValidationResult
    {
        public bool IsValid { get; set; }
        public GameData NewGameState { get; set; }
        public GameData SomethingChanged { get; set; }

        public ValidationResult(bool isValid, GameData newGameState)
        {
            IsValid = isValid;
            NewGameState = newGameState;
        }
    }
}
