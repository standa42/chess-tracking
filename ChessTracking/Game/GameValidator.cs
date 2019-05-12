using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ChessTracking.MultithreadingMessages;

namespace ChessTracking.Game
{
    static class GameValidator
    {
        public static ValidationResult ValidateAndPerform(GameData game, string move)
        {
            return null;

            // get moves

            // decode move

            // jsem v šachu?

            // je nepřítel v šachumatu, patu?

            // změn hrajícího hráče
        }

        public static ValidationResult ValidateAndPerform(GameData game, TrackingState trackingState)
        {
            // get moves
            var myMoves = GetAllMoves(game.Chessboard, game.PlayerOnMove);

            // decode move
            var myMove = DecodeMove(game, trackingState);

            if (myMove == null)
                return new ValidationResult(false, null);

            if (myMoves.Any(x => x.IsEquivalent(myMove)))
                PerformMove(game, myMove);
            else
                return new ValidationResult(false, null);

            // jsem v šachu?
            var IamChecked = PlayerIsChecked(game, game.PlayerOnMove);
            if (IamChecked)
                return new ValidationResult(false, null);

            // zaznamenej pohyb
            game.RecordOfGame.Add(RecordMove(game, myMove));

            // je nepřítel v šachumatu, patu?
            if (PlayerHasNoPossibleMoves(game, GetOppositeColor(game.PlayerOnMove)))
            {
                if (PlayerIsChecked(game, GetOppositeColor(game.PlayerOnMove)))
                    game.EndState = GetWinStateFromPlayerColor(game.PlayerOnMove);
                else
                    game.EndState = GameWinState.Draw;
            }

            // změn hrajícího hráče
            game.PlayerOnMove = GetOppositeColor(game.PlayerOnMove);

            return new ValidationResult(true, game);
        }

        private static string RecordMove(GameData game, GameMove move)
        {
            char separator = move.ToWhom == null ? '-' : 'x';
            return
                $"{GetCharacterForFigure(move.Who)}{GetCharacterForPosition(move.From.X)}{move.From.Y}{separator}{GetCharacterForPosition(move.To.X)}{move.To.Y}";
        }

        private static char GetCharacterForFigure(FigureType figure)
        {
            switch (figure)
            {
                case FigureType.Queen:
                    return 'Q';
                case FigureType.King:
                    return 'K';
                case FigureType.Rook:
                    return 'R';
                case FigureType.Knight:
                    return 'N';
                case FigureType.Bishop:
                    return 'R';
                case FigureType.Pawn:
                    return 'P';
                default:
                    throw new ArgumentOutOfRangeException(nameof(figure), figure, null);
            }
        }

        private static char GetCharacterForPosition(int position)
        {
            return (char) ((int) 'a' + position);
        }

        private static void PerformMove(GameData game, GameMove move)
        {
            game.Chessboard.MoveTo(move.From, move.To);
        }

        private static void RevertMove(GameData game, GameMove move, Figure tempSavedTakenFigure)
        {
            game.Chessboard.MoveTo(move.To, move.From);
            game.Chessboard.AddFigure(tempSavedTakenFigure, move.To);
        }

        private static bool PlayerHasNoPossibleMoves(GameData game, PlayerColor playerColor)
        {
            var possibleMoves = GetAllMoves(game.Chessboard, playerColor);

            foreach (var move in possibleMoves)
            {
                var tempSavedTakenFigure = game.Chessboard.GetFigureOnPosition(move.To);
                PerformMove(game, move);
                bool IsChecked = PlayerIsChecked(game, playerColor);
                RevertMove(game, move, tempSavedTakenFigure);
                if (!IsChecked)
                    return false;
            }

            return true;
        }

        private static GameWinState GetWinStateFromPlayerColor(PlayerColor color)
        {
            return color == PlayerColor.White ? GameWinState.WhiteWin : GameWinState.BlackWin;
        }

        private static bool PlayerIsChecked(GameData game, PlayerColor color)
        {
            var myKingPosition = GetKingPosition(game.Chessboard.Figures, color);
            var enemyMoves = GetAllMoves(game.Chessboard, GetOppositeColor(color));
            return enemyMoves.Any(x => x.To.IsEquivalent(myKingPosition));
        }

        private static PlayerColor GetOppositeColor(PlayerColor color)
        {
            return color == PlayerColor.White ? PlayerColor.Black : PlayerColor.White;
        }

        private static ChessPosition GetKingPosition(Figure[,] figures, PlayerColor playerColor)
        {
            for (int x = 0; x < 8; x++)
            {
                for (int y = 0; y < 8; y++)
                {
                    var figure = figures[x, y];
                    if (figure != null && figure.Type == FigureType.King && figure.Color == playerColor)
                    {
                        return new ChessPosition(x, y);
                    }
                }
            }

            return null;
        }

        private static GameMove DecodeMove(GameData game, string move)
        {
            return null;
        }

        private static GameMove DecodeMove(GameData game, TrackingState trackingState)
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
                        continue;

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
                    return new GameMove(
                        whiteToNone.First(),
                        noneToWhite.First(),
                        game.Chessboard.GetFigureOnPosition(whiteToNone.First()).Type,
                        game.Chessboard.GetFigureOnPosition(noneToWhite.First())?.Type
                        );
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
                    return new GameMove(
                        whiteToNone.First(),
                        blackToWhite.First(),
                        game.Chessboard.GetFigureOnPosition(whiteToNone.First()).Type,
                        game.Chessboard.GetFigureOnPosition(blackToWhite.First())?.Type
                    );
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
                    return new GameMove(
                        blackToNone.First(),
                        noneToBlack.First(),
                        game.Chessboard.GetFigureOnPosition(blackToNone.First()).Type,
                        game.Chessboard.GetFigureOnPosition(noneToBlack.First())?.Type
                    );
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
                    return new GameMove(
                        blackToNone.First(),
                        whiteToBlack.First(),
                        game.Chessboard.GetFigureOnPosition(blackToNone.First()).Type,
                        game.Chessboard.GetFigureOnPosition(whiteToBlack.First())?.Type
                    );
                }
            }

            return null;
        }

        private static List<GameMove> GetAllMoves(ChessboardModel chessboard, PlayerColor playerColor)
        {
            var acumulator = new List<GameMove>();
            acumulator.AddRange(GetMovesForPawns(chessboard, playerColor));
            acumulator.AddRange(GetMovesForKing(chessboard, playerColor));
            acumulator.AddRange(GetMovesForKnights(chessboard, playerColor));
            acumulator.AddRange(GetMovesForRooks(chessboard, playerColor));
            acumulator.AddRange(GetMovesForBishops(chessboard, playerColor));
            acumulator.AddRange(GetMovesForQueens(chessboard, playerColor));

            return acumulator;
        }

        private static List<GameMove> GetMovesForRooks(ChessboardModel chessboard, PlayerColor playerColor)
        {
            var acumulator = new List<GameMove>();

            var up = new GameMoveVector(0, 1);
            var left = new GameMoveVector(-1, 0);
            var down = new GameMoveVector(0, -1);
            var right = new GameMoveVector(1, 0);

            for (int x = 0; x < 8; x++)
            {
                for (int y = 0; y < 8; y++)
                {
                    var position = new ChessPosition(x, y);
                    var figure = chessboard.GetFigureOnPosition(position);
                    if (
                        figure != null &&
                        figure.Type == FigureType.Rook &&
                        figure.Color == playerColor
                    )
                    {
                        acumulator.AddRange(CanMoveOrAttackIterative(chessboard, playerColor, position, up));
                        acumulator.AddRange(CanMoveOrAttackIterative(chessboard, playerColor, position, left));
                        acumulator.AddRange(CanMoveOrAttackIterative(chessboard, playerColor, position, down));
                        acumulator.AddRange(CanMoveOrAttackIterative(chessboard, playerColor, position, right));
                    }
                }
            }

            acumulator.RemoveAll(x => x == null);
            return acumulator;
        }

        private static List<GameMove> GetMovesForBishops(ChessboardModel chessboard, PlayerColor playerColor)
        {
            var acumulator = new List<GameMove>();

            var upLeft = new GameMoveVector(-1, 1);
            var upRight = new GameMoveVector(1, 1);
            var downLeft = new GameMoveVector(-1, -1);
            var downRight = new GameMoveVector(1, -1);

            for (int x = 0; x < 8; x++)
            {
                for (int y = 0; y < 8; y++)
                {
                    var position = new ChessPosition(x, y);
                    var figure = chessboard.GetFigureOnPosition(position);
                    if (
                        figure != null &&
                        figure.Type == FigureType.Bishop &&
                        figure.Color == playerColor
                    )
                    {
                        acumulator.AddRange(CanMoveOrAttackIterative(chessboard, playerColor, position, upLeft));
                        acumulator.AddRange(CanMoveOrAttackIterative(chessboard, playerColor, position, upRight));
                        acumulator.AddRange(CanMoveOrAttackIterative(chessboard, playerColor, position, downLeft));
                        acumulator.AddRange(CanMoveOrAttackIterative(chessboard, playerColor, position, downRight));
                    }
                }
            }

            acumulator.RemoveAll(x => x == null);
            return acumulator;
        }

        private static List<GameMove> GetMovesForQueens(ChessboardModel chessboard, PlayerColor playerColor)
        {
            var acumulator = new List<GameMove>();

            var up = new GameMoveVector(0, 1);
            var left = new GameMoveVector(-1, 0);
            var down = new GameMoveVector(0, -1);
            var right = new GameMoveVector(1, 0);
            var upLeft = new GameMoveVector(-1, 1);
            var upRight = new GameMoveVector(1, 1);
            var downLeft = new GameMoveVector(-1, -1);
            var downRight = new GameMoveVector(1, -1);

            for (int x = 0; x < 8; x++)
            {
                for (int y = 0; y < 8; y++)
                {
                    var position = new ChessPosition(x, y);
                    var figure = chessboard.GetFigureOnPosition(position);
                    if (
                        figure != null &&
                        figure.Type == FigureType.Queen &&
                        figure.Color == playerColor
                    )
                    {
                        acumulator.AddRange(CanMoveOrAttackIterative(chessboard, playerColor, position, up));
                        acumulator.AddRange(CanMoveOrAttackIterative(chessboard, playerColor, position, left));
                        acumulator.AddRange(CanMoveOrAttackIterative(chessboard, playerColor, position, down));
                        acumulator.AddRange(CanMoveOrAttackIterative(chessboard, playerColor, position, right));
                        acumulator.AddRange(CanMoveOrAttackIterative(chessboard, playerColor, position, upLeft));
                        acumulator.AddRange(CanMoveOrAttackIterative(chessboard, playerColor, position, upRight));
                        acumulator.AddRange(CanMoveOrAttackIterative(chessboard, playerColor, position, downLeft));
                        acumulator.AddRange(CanMoveOrAttackIterative(chessboard, playerColor, position, downRight));
                    }
                }
            }

            acumulator.RemoveAll(x => x == null);
            return acumulator;
        }

        private static List<GameMove> GetMovesForPawns(ChessboardModel chessboard, PlayerColor playerColor)
        {
            var acumulator = new List<GameMove>();

            var moveVector = playerColor == PlayerColor.White ? new GameMoveVector(0, 1) : new GameMoveVector(0, -1);
            var captureVector1 = playerColor == PlayerColor.White ? new GameMoveVector(1, 1) : new GameMoveVector(1, -1);
            var captureVector2 = playerColor == PlayerColor.White ? new GameMoveVector(-1, 1) : new GameMoveVector(-1, -1);

            for (int x = 0; x < 8; x++)
            {
                for (int y = 0; y < 8; y++)
                {
                    var position = new ChessPosition(x, y);
                    var figure = chessboard.GetFigureOnPosition(position);
                    if (
                        figure != null &&
                        figure.Type == FigureType.Pawn &&
                        figure.Color == playerColor
                       )
                    {
                        acumulator.Add(CanAttack(chessboard, playerColor, position, position.Add(captureVector1)));
                        acumulator.Add(CanAttack(chessboard, playerColor, position, position.Add(captureVector2)));
                        var moveOnce = CanMove(chessboard, playerColor, position, position.Add(moveVector));
                        acumulator.Add(moveOnce);
                        if (moveOnce != null)
                            acumulator.Add(CanMove(chessboard, playerColor, position, moveOnce.To.Add(moveVector)));
                    }
                }
            }

            acumulator.RemoveAll(x => x == null);
            return acumulator;
        }

        private static List<GameMove> GetMovesForKnights(ChessboardModel chessboard, PlayerColor playerColor)
        {
            var acumulator = new List<GameMove>();

            var upLeft = new GameMoveVector(-1, 2);
            var upRight = new GameMoveVector(1, 2);
            var downLeft = new GameMoveVector(-1, -2);
            var downRight = new GameMoveVector(1, -2);
            var leftUp = new GameMoveVector(-2, 1);
            var leftDown = new GameMoveVector(-2, -1);
            var rightUp = new GameMoveVector(2, -1);
            var rightDown = new GameMoveVector(2, -1);

            for (int x = 0; x < 8; x++)
            {
                for (int y = 0; y < 8; y++)
                {
                    var position = new ChessPosition(x, y);
                    var figure = chessboard.GetFigureOnPosition(position);
                    if (
                        figure != null &&
                        figure.Type == FigureType.Knight &&
                        figure.Color == playerColor
                    )
                    {
                        acumulator.Add(CanMoveOrAttack(chessboard, playerColor, position, position.Add(upLeft)));
                        acumulator.Add(CanMoveOrAttack(chessboard, playerColor, position, position.Add(upRight)));
                        acumulator.Add(CanMoveOrAttack(chessboard, playerColor, position, position.Add(downLeft)));
                        acumulator.Add(CanMoveOrAttack(chessboard, playerColor, position, position.Add(downRight)));
                        acumulator.Add(CanMoveOrAttack(chessboard, playerColor, position, position.Add(leftUp)));
                        acumulator.Add(CanMoveOrAttack(chessboard, playerColor, position, position.Add(leftDown)));
                        acumulator.Add(CanMoveOrAttack(chessboard, playerColor, position, position.Add(rightUp)));
                        acumulator.Add(CanMoveOrAttack(chessboard, playerColor, position, position.Add(rightDown)));
                    }
                }
            }

            acumulator.RemoveAll(x => x == null);
            return acumulator;
        }

        private static List<GameMove> GetMovesForKing(ChessboardModel chessboard, PlayerColor playerColor)
        {
            var acumulator = new List<GameMove>();

            var up = new GameMoveVector(0, 1);
            var left = new GameMoveVector(-1, 0);
            var down = new GameMoveVector(0, -1);
            var right = new GameMoveVector(1, 0);
            var upLeft = new GameMoveVector(-1, 1);
            var upRight = new GameMoveVector(1, 1);
            var downLeft = new GameMoveVector(-1, -1);
            var downRight = new GameMoveVector(1, -1);

            for (int x = 0; x < 8; x++)
            {
                for (int y = 0; y < 8; y++)
                {
                    var position = new ChessPosition(x, y);
                    var figure = chessboard.GetFigureOnPosition(position);
                    if (
                        figure != null &&
                        figure.Type == FigureType.King &&
                        figure.Color == playerColor
                    )
                    {
                        acumulator.Add(CanMoveOrAttack(chessboard, playerColor, position, position.Add(up)));
                        acumulator.Add(CanMoveOrAttack(chessboard, playerColor, position, position.Add(left)));
                        acumulator.Add(CanMoveOrAttack(chessboard, playerColor, position, position.Add(down)));
                        acumulator.Add(CanMoveOrAttack(chessboard, playerColor, position, position.Add(right)));
                        acumulator.Add(CanMoveOrAttack(chessboard, playerColor, position, position.Add(upLeft)));
                        acumulator.Add(CanMoveOrAttack(chessboard, playerColor, position, position.Add(upRight)));
                        acumulator.Add(CanMoveOrAttack(chessboard, playerColor, position, position.Add(downLeft)));
                        acumulator.Add(CanMoveOrAttack(chessboard, playerColor, position, position.Add(downRight)));
                    }
                }
            }

            acumulator.RemoveAll(x => x == null);
            return acumulator;
        }

        private static List<GameMove> CanMoveOrAttackIterative(ChessboardModel chessboard, PlayerColor playerColor, ChessPosition from, GameMoveVector vector)
        {
            var acumulator = new List<GameMove>();

            var temp = from;

            while (true)
            {
                temp = temp.Add(vector);

                var attack = CanAttack(chessboard, playerColor, from, temp);
                var move = CanMove(chessboard, playerColor, from, temp);

                acumulator.Add(move);
                acumulator.Add(attack);

                if (attack != null)
                    return acumulator;

                if (move == null)
                    return acumulator;
            }
        }

        private static GameMove CanMoveOrAttack(ChessboardModel chessboard, PlayerColor playerColor, ChessPosition from, ChessPosition to)
        {
            if (!to.IsValid())
                return null;

            var moveTo = chessboard.GetFigureOnPosition(to);

            if (moveTo != null && moveTo.Color == playerColor)
                return null;

            return new GameMove(from, to, chessboard.GetFigureOnPosition(from).Type, chessboard.GetFigureOnPosition(to)?.Type);
        }

        private static GameMove CanAttack(ChessboardModel chessboard, PlayerColor playerColor, ChessPosition from, ChessPosition to)
        {
            if (!to.IsValid())
                return null;

            var moveTo = chessboard.GetFigureOnPosition(to);

            if (moveTo == null)
                return null;
            if (moveTo.Color == playerColor)
                return null;

            return new GameMove(from, to, chessboard.GetFigureOnPosition(from).Type, chessboard.GetFigureOnPosition(to)?.Type);
        }

        private static GameMove CanMove(ChessboardModel chessboard, PlayerColor playerColor, ChessPosition from, ChessPosition to)
        {
            if (!to.IsValid())
                return null;

            var moveTo = chessboard.GetFigureOnPosition(to);

            if (moveTo != null)
                return null;

            return new GameMove(from, to, chessboard.GetFigureOnPosition(from).Type, chessboard.GetFigureOnPosition(to)?.Type);
        }
    }
}
