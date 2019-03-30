using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChessboardTrackingOctoberEdition.UserPart
{
    static class GameFactory
    {
        public static Game NewGame()
        {
            return new Game();
        }

        public static Game LoadGame()
        {
            return new Game();
        }
    }
}
