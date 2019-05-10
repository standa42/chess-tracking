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
           var tsfigures = trackingState.Figures;
           var gamefigures = game.Chessboard.GetTrackingStates().Figures;

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
               game.Chessboard.Figures[appearance.X, appearance.Y] = game.Chessboard.Figures[missing.X, missing.Y];
               game.Chessboard.Figures[missing.X, missing.Y] = null;
               game.RecordOfGame.Add($"st with fields {missing.X} {missing.Y} and {appearance.X} {appearance.Y}");
           }
           
           return new ValidationResult(true, game);
        }
    }
}
