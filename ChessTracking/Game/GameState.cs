using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChessTracking.Game
{
    /// <summary>
    /// Representation of state of game
    /// </summary>
    public enum GameState
    {
        WhiteWin,
        BlackWin,
        Draw,
        StillPlaying
    }
}
