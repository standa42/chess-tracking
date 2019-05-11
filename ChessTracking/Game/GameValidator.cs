using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Accord.IO;
using Accord.Math;
using ChessTracking.MultithreadingMessages;

namespace ChessTracking.Game
{
    public static class GameValidator
    {
        public static ValidationResult ValidateAndPerform(GameData game, TrackingState trackingState)
        {
            game = game.DeepClone();

            try
            {
                if (game.EndState != GameWinState.StillPlaying)
                    return new ValidationResult(false, game);

                var tempEnPassantPosition = game.EnPassantPosition;

                var validationResult = DispatchMove(game, trackingState);

                if (validationResult.IsValid)
                {
                    game.PlayerOnMove = game.PlayerOnMove == PlayerColor.White ? PlayerColor.Black : PlayerColor.White;
                    if (tempEnPassantPosition.IsEquivalent(game.EnPassantPosition))
                        game.EnPassantPosition = null;
                }
                
                return validationResult;
            }
            catch (Exception e)
            {
                // TODO zapnout
                throw e;
                return new ValidationResult(false, game);
            }
        }

        private static ValidationResult DispatchMove(GameData game, TrackingState trackingState)
        {
            var trackingStateFigures = trackingState.Figures;
            var gameFigures = game.Chessboard.GetTrackingStates().Figures;

            var noneToWhite = new List<ChessPosition>();
            var noneToBlack = new List<ChessPosition>();
            var whiteToNone = new List<ChessPosition>();
            var blackToNone = new List<ChessPosition>();
            var whiteToBlack = new List<ChessPosition>();
            var blackToWhite = new List<ChessPosition>();

            for (int x = 0; x < 8; x++)
            {
                for (int y = 0; y < 8; y++)
                {
                    if (gameFigures[x, y] == trackingStateFigures[x, y])
                        break;

                    if (gameFigures[x, y] == TrackingFieldState.None && trackingStateFigures[x, y] == TrackingFieldState.White)
                        noneToWhite.Add(new ChessPosition(x, y));

                    if (gameFigures[x, y] == TrackingFieldState.None && trackingStateFigures[x, y] == TrackingFieldState.Black)
                        noneToBlack.Add(new ChessPosition(x, y));

                    if (gameFigures[x, y] == TrackingFieldState.White && trackingStateFigures[x, y] == TrackingFieldState.None)
                        whiteToNone.Add(new ChessPosition(x, y));

                    if (gameFigures[x, y] == TrackingFieldState.Black && trackingStateFigures[x, y] == TrackingFieldState.None)
                        blackToNone.Add(new ChessPosition(x, y));

                    if (gameFigures[x, y] == TrackingFieldState.White && trackingStateFigures[x, y] == TrackingFieldState.Black)
                        whiteToBlack.Add(new ChessPosition(x, y));

                    if (gameFigures[x, y] == TrackingFieldState.Black && trackingStateFigures[x, y] == TrackingFieldState.White)
                        blackToWhite.Add(new ChessPosition(x, y));
                }
            }
            
            if (game.PlayerOnMove == PlayerColor.White)
            {
                if (
                    whiteToNone.Count == 1 &&
                    noneToWhite.Count == 1 &&
                    blackToNone.Count == 0 &&
                    noneToBlack.Count == 0 &&
                    blackToWhite.Count == 0 &&
                    whiteToBlack.Count == 0
                    )
                {
                    MoveValidator.ValidateAndPerformMove(game, null, whiteToNone.First(), noneToWhite.First());
                }

                if (
                    whiteToNone.Count == 1 &&
                    noneToWhite.Count == 0 &&
                    blackToNone.Count == 0 &&
                    noneToBlack.Count == 0 &&
                    blackToWhite.Count == 1 &&
                    whiteToBlack.Count == 0
                )
                {
                    CaptureValidator.ValidateAndPerformMove(game, null, whiteToNone.First(), blackToWhite.First());
                }

                if (
                    whiteToNone.Count == 2 &&
                    noneToWhite.Count == 2 &&
                    blackToNone.Count == 0 &&
                    noneToBlack.Count == 0 &&
                    blackToWhite.Count == 0 &&
                    whiteToBlack.Count == 0
                )
                {
                    CastlingValidator.ValidateAndPerformMove(game, whiteToNone[0], whiteToNone[1], noneToWhite[0],
                        noneToWhite[1]);
                }

                if (
                    whiteToNone.Count == 1 &&
                    noneToWhite.Count == 1 &&
                    blackToNone.Count == 1 &&
                    noneToBlack.Count == 0 &&
                    blackToWhite.Count == 0 &&
                    whiteToBlack.Count == 0
                )
                {
                    EnPassantValidator.ValidateAndPerformMove(game, whiteToNone.First(), noneToWhite.First(),
                        blackToNone.First());
                }
            }
            else
            {
                if (
                    whiteToNone.Count == 0 &&
                    noneToWhite.Count == 0 &&
                    blackToNone.Count == 1 &&
                    noneToBlack.Count == 1 &&
                    blackToWhite.Count == 0 &&
                    whiteToBlack.Count == 0
                )
                {
                    MoveValidator.ValidateAndPerformMove(game, null, blackToNone.First(), noneToBlack.First());
                }

                if (
                    whiteToNone.Count == 0 &&
                    noneToWhite.Count == 0 &&
                    blackToNone.Count == 1 &&
                    noneToBlack.Count == 0 &&
                    blackToWhite.Count == 0 &&
                    whiteToBlack.Count == 1
                )
                {
                    CaptureValidator.ValidateAndPerformMove(game, null, blackToNone.First(), whiteToBlack.First());
                }

                if (
                    whiteToNone.Count == 0 &&
                    noneToWhite.Count == 0 &&
                    blackToNone.Count == 2 &&
                    noneToBlack.Count == 2 &&
                    blackToWhite.Count == 0 &&
                    whiteToBlack.Count == 0
                )
                {
                    CastlingValidator.ValidateAndPerformMove(game, blackToNone[0], blackToNone[1], noneToBlack[0],
                        noneToBlack[1]);
                }

                if (
                    whiteToNone.Count == 1 &&
                    noneToWhite.Count == 0 &&
                    blackToNone.Count == 1 &&
                    noneToBlack.Count == 1 &&
                    blackToWhite.Count == 0 &&
                    whiteToBlack.Count == 0
                )
                {
                    EnPassantValidator.ValidateAndPerformMove(game, blackToNone.First(), noneToBlack.First(),
                        whiteToNone.First());
                }
            }
            
            return new ValidationResult(false, game);
        }

        // TODO: probably not used, because of move operation in figure
        private static void MarkAffectedFiguresAsMoved(GameData game, ValidationResult result)
        {
            /*
            foreach (var position in result.AffectedPositions)
            {
                var figure = game.Chessboard.GetFigureOnPosition(position);
                if (figure != null)
                {
                    figure.Moved = true;
                }
            }
            */
        }
    }
}
