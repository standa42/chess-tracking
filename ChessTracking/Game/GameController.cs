using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ChessTracking.UserInterface;

namespace ChessTracking.Game
{
    class GameController
    {
        private UserInterfaceOutputFacade OutputFacade { get; }

        public GameController(UserInterfaceOutputFacade outputFacade)
        {
            OutputFacade = outputFacade;
        }
        
        public void TryChangeChessboardState()
        {

        }

        public void NewGame()
        {

        }

        public void SaveGame()
        {

        }

        public void LoadGame()
        {

        }


    }
}
