using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ChessTracking.MultithreadingMessages;
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

        public int? InitiateWithTracingInput(TrackingState trackingState)
        {
            TrackingFieldState[,] figures = new TrackingFieldState[8, 8];
            figures[0, 0] = TrackingFieldState.White;
            figures[1, 0] = TrackingFieldState.White;
            figures[2, 0] = TrackingFieldState.White;
            figures[3, 0] = TrackingFieldState.White;
            figures[4, 0] = TrackingFieldState.White;
            figures[5, 0] = TrackingFieldState.White;
            figures[6, 0] = TrackingFieldState.White;
            figures[7, 0] = TrackingFieldState.White;

            figures[0, 1] = TrackingFieldState.White;
            figures[1, 1] = TrackingFieldState.White;
            figures[2, 1] = TrackingFieldState.White;
            figures[3, 1] = TrackingFieldState.White;
            figures[4, 1] = TrackingFieldState.White;
            figures[5, 1] = TrackingFieldState.White;
            figures[6, 1] = TrackingFieldState.White;
            figures[7, 1] = TrackingFieldState.White;

            figures[0, 7] = TrackingFieldState.Black;
            figures[1, 7] = TrackingFieldState.Black;
            figures[2, 7] = TrackingFieldState.Black;
            figures[3, 7] = TrackingFieldState.Black;
            figures[4, 7] = TrackingFieldState.Black;
            figures[5, 7] = TrackingFieldState.Black;
            figures[6, 7] = TrackingFieldState.Black;
            figures[7, 7] = TrackingFieldState.Black;

            figures[0, 6] = TrackingFieldState.Black;
            figures[1, 6] = TrackingFieldState.Black;
            figures[2, 6] = TrackingFieldState.Black;
            figures[3, 6] = TrackingFieldState.Black;
            figures[4, 6] = TrackingFieldState.Black;
            figures[5, 6] = TrackingFieldState.Black;
            figures[6, 6] = TrackingFieldState.Black;
            figures[7, 6] = TrackingFieldState.Black;

            TrackingState chessboardState = new TrackingState(figures);

            for (int i = 0; i < 4; i++)
            {
                if (chessboardState == trackingState)
                {
                    return i;
                }
                trackingState.RotateClockWise(1);
            }

            return null;
        }

        public void TryChangeChessboardState(TrackingState trackingState)
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
