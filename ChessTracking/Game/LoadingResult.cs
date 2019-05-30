using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChessTracking.Game
{
    public class LoadingResult
    {
        public GameData Game { get; set; }
        public bool LoadingSuccesfull { get; set; }

        public LoadingResult(GameData game, bool loadingSuccesfull)
        {
            Game = game;
            LoadingSuccesfull = loadingSuccesfull;
        }
    }
}
