using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChessTracking.Game
{
    /// <summary>
    /// Object describing result of chess validation procedure
    /// </summary>
    public class ValidationResult
    {
        public bool IsValid { get; set; }
        public GameData NewGameState { get; set; }
        
        public ValidationResult(bool isValid, GameData newGameState)
        {
            IsValid = isValid;
            NewGameState = newGameState;
        }
    }
}
