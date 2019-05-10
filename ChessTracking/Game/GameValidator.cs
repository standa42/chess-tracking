using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ChessTracking.MultithreadingMessages;

namespace ChessTracking.Game
{
    public static class GameValidator
    {
        public static ValidationResult ValidateAndPerform(GameData game, TrackingState trackingState)
        {
            /*
           var tsfigures = trackingState.Figures;
           var gamefigures = Game.Chessboard.GetTrackingStates().Figures;

           ChessPosition missing = null;
           ChessPosition appearance = null;

           for (int x = 0; x < 8; x++)
           {
               for (int y = 0; y < 8; y++)
               {
                   if (gamefigures[x,y] != TrackingFieldState.None && tsfigures[x,y] == TrackingFieldState.None)
                   {
                       missing = new ChessPosition(x,y);
                   }

                   if (gamefigures[x, y] == TrackingFieldState.None && tsfigures[x, y] != TrackingFieldState.None)
                   {
                       appearance = new ChessPosition(x,y);
                   }

                   if (
                       (gamefigures[x, y] == TrackingFieldState.Black && tsfigures[x, y] == TrackingFieldState.White) ||
                       (gamefigures[x, y] == TrackingFieldState.White && tsfigures[x, y] == TrackingFieldState.Black)
                       )
                   {
                       appearance = new ChessPosition(x, y);
                   }
               }

           }

           if (missing != null && appearance != null)
           {
               Game.Chessboard.Figures[appearance.X, appearance.Y] = Game.Chessboard.Figures[missing.X, missing.Y];
               Game.Chessboard.Figures[missing.X, missing.Y] = null;
           }

           OutputFacade.UpdateBoardState(RenderGameState());

           */

            return new ValidationResult(true, game);
        }
    }
}
