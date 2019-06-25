using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ChessTracking.MultithreadingMessages;
using ZedGraph;

namespace ChessTracking.Game
{
    /// <summary>
    /// Validator of chess game
    /// </summary>
    static class GameValidator
    {
        /// <summary>
        /// Validates and performs move described in text
        /// </summary>
        /// <param name="game">Description of game</param>
        /// <param name="move">Move to process</param>
        /// <returns>Result of validation</returns>
        public static ValidationResult ValidateAndPerform(GameData game, string move)
        {
            return ValidateAndPerform(game, DecodeMove(game, move));
        }

        /// <summary>
        /// Validates and performs move described by difference between game and trackingState
        /// </summary>
        /// <param name="game">Description of game</param>
        /// <param name="move">Tracking state of game</param>
        /// <returns>Result of validation</returns>
        public static ValidationResult ValidateAndPerform(GameData game, TrackingState trackingState)
        {
            return ValidateAndPerform(game, DecodeMove(game, trackingState));
        }

        /// <summary>
        /// Validates and performs given move 
        /// </summary>
        /// <param name="game">Description of game</param>
        /// <returns>Result of validation</returns>
        private static ValidationResult ValidateAndPerform(GameData game, GameMove myMove)
        {
            if (myMove == null)
                return new ValidationResult(false, null);

            var myMoves = GetAllMoves(game.Chessboard, game.PlayerOnMove);

            // move is in possible moves
            if (myMoves.Any(x => x.IsEquivalent(myMove)))
                PerformMove(game, myMove);
            else
                return new ValidationResult(false, null);

            // move can't end in beeing checked
            if (PlayerIsChecked(game, game.PlayerOnMove))
                return new ValidationResult(false, null);

            game.RecordOfGame.Add(SerializeMoveToAlgebraicNotation(myMove));

            // chech whether opponent in in checkmate or stalemate
            if (PlayerHasNoPossibleMoves(game, GetOppositeColor(game.PlayerOnMove)))
            {
                if (PlayerIsChecked(game, GetOppositeColor(game.PlayerOnMove)))
                    game.EndState = GetWinStateFromPlayerColor(game.PlayerOnMove);
                else
                    game.EndState = GameState.Draw;
            }

            // alternate playing player
            game.PlayerOnMove = GetOppositeColor(game.PlayerOnMove);

            return new ValidationResult(true, game);
        }

        /// <summary>
        /// Serializes move into its algebraic notation representation
        /// </summary>
        /// <param name="move">Move to serialize</param>
        /// <returns>Textual representation of move</returns>
        private static string SerializeMoveToAlgebraicNotation(GameMove move)
        {
            // TODO: make it member function of Move?
            char separator = move.ToWhom == null ? '-' : 'x';
            return
                $"{GetCharacterForFigure(move.Who)}{GetCharacterForPosition(move.From.X)}{move.From.Y + 1}{separator}{GetCharacterForPosition(move.To.X)}{move.To.Y + 1}";
        }

        /// <summary>
        /// Gets character representation for given figure
        /// </summary>
        private static char GetCharacterForFigure(FigureType figure)
        {
            // TODO: move to another file?
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
                    return 'B';
                case FigureType.Pawn:
                    return 'P';
                default:
                    throw new ArgumentOutOfRangeException(nameof(figure), figure, null);
            }
        }

        /// <summary>
        /// Gets figure type for given character
        /// </summary>
        private static FigureType GetFigureForCharacter(char ch)
        {
            // TODO: move to another file?
            switch (ch)
            {
                case 'Q':
                    return FigureType.Queen;
                case 'K':
                    return FigureType.King;
                case 'R':
                    return FigureType.Rook;
                case 'N':
                    return FigureType.Knight;
                case 'B':
                    return FigureType.Bishop;
                case 'P':
                    return FigureType.Pawn;
                default:
                    throw new ArgumentOutOfRangeException(nameof(ch), ch, null);
            }
        }

        /// <summary>
        /// Gets character represenation of column on chessboard
        /// </summary>
        /// <param name="position">Integer represenation of column</param>
        private static char GetCharacterForPosition(int position)
        {
            // TODO: move to another file?
            return (char)((int)'a' + position);
        }

        /// <summary>
        /// Gets integer represenation of column on chessboard
        /// </summary>
        /// <param name="ch">Character represenation of column</param>
        private static int GetPositionForCharacter(char ch)
        {
            // TODO: move to another file?
            return (int)((int)ch - (int)'a');
        }

        /// <summary>
        /// Performs given move on chessboard
        /// </summary>
        /// <param name="game">Description of game</param>
        /// <param name="move">Move to perform</param>
        private static void PerformMove(GameData game, GameMove move)
        {
            game.Chessboard.GetFigureOnPosition(move.From).Moved = true;
            game.Chessboard.MoveTo(move.From, move.To);
        }

        /// <summary>
        /// Reverts given move
        /// </summary>
        /// <param name="game">Description of game</param>
        /// <param name="move">Move to revert</param>
        /// <param name="tempSavedTakenFigure">Saved figure in case, that move was capture</param>
        private static void RevertMove(GameData game, GameMove move, Figure tempSavedTakenFigure)
        {
            game.Chessboard.MoveTo(move.To, move.From);
            game.Chessboard.AddFigure(tempSavedTakenFigure, move.To);
        }

        /// <summary>
        /// Checks whether player has no possible moves to play
        /// </summary>
        /// <param name="game">Description of game</param>
        /// <param name="playerColor">Color of player</param>
        /// <returns>Whether player has no possible moves to play</returns>
        private static bool PlayerHasNoPossibleMoves(GameData game, PlayerColor playerColor)
        {
            var possibleMoves = GetAllMoves(game.Chessboard, playerColor);

            foreach (var move in possibleMoves)
            {
                var tempSavedTakenFigure = game.Chessboard.GetFigureOnPosition(move.To);
                PerformMove(game, move);
                bool isChecked = PlayerIsChecked(game, playerColor);
                RevertMove(game, move, tempSavedTakenFigure);
                if (!isChecked)
                    return false;
            }

            return true;
        }

        /// <summary>
        /// Transforms player color to win state of player of this color
        /// </summary>
        private static GameState GetWinStateFromPlayerColor(PlayerColor color)
        {
            // TODO: move to different file?
            return color == PlayerColor.White ? GameState.WhiteWin : GameState.BlackWin;
        }

        /// <summary>
        /// Checks wheter given player is checked in given game
        /// </summary>
        /// <param name="game">Description of game</param>
        private static bool PlayerIsChecked(GameData game, PlayerColor color)
        {
            var myKingPosition = GetKingPosition(game.Chessboard.Figures, color);
            var enemyMoves = GetAllMoves(game.Chessboard, GetOppositeColor(color));
            return enemyMoves.Any(x => x.To.IsEquivalent(myKingPosition));
        }

        /// <summary>
        /// Gets oponents color
        /// </summary>
        private static PlayerColor GetOppositeColor(PlayerColor color)
        {
            return color == PlayerColor.White ? PlayerColor.Black : PlayerColor.White;
        }

        /// <summary>
        /// Gets king position on chessboard for player of given color
        /// </summary>
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

        /// <summary>
        /// Decodes textual representation of move into object
        /// </summary>
        /// <returns>Move if it's valid, null otherwise</returns>
        private static GameMove DecodeMove(GameData game, string move)
        {
            var figure = GetFigureForCharacter(move[0]);
            var from = new ChessPosition(GetPositionForCharacter(move[1]), int.Parse(move[2].ToString()) - 1);
            var isCapture = move[3] == 'x';
            var to = new ChessPosition(GetPositionForCharacter(move[4]), int.Parse(move[5].ToString()) - 1);

            var fromFigure = game.Chessboard.GetFigureOnPosition(from);
            var toFigure = game.Chessboard.GetFigureOnPosition(to);

            if (figure != fromFigure.Type)
                return null;
            if (isCapture)
            {
                if (game.Chessboard.GetFigureOnPosition(to) == null)
                    return null;
            }
            else
            {
                if (game.Chessboard.GetFigureOnPosition(to) != null)
                    return null;
            }

            return new GameMove(from, to, fromFigure.Type, toFigure?.Type);
        }

        /// <summary>
        /// Decodes game and tracking state difference into game move object
        /// </summary>
        /// <returns>Move if it's valid, null otherwise</returns>
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

        /// <summary>
        /// Get all possible moves for given player
        /// </summary>
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

        /// <summary>
        /// Gets all possible moves of rooks of given player
        /// </summary>
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

        /// <summary>
        /// Gets all possible moves of bishops of given player
        /// </summary>
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

        /// <summary>
        /// Gets all possible moves of queens of given player
        /// </summary>
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

        /// <summary>
        /// Gets all possible moves of pawns of given player
        /// </summary>
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
                        if (moveOnce != null && !figure.Moved)
                            acumulator.Add(CanMove(chessboard, playerColor, position, moveOnce.To.Add(moveVector)));
                    }
                }
            }

            acumulator.RemoveAll(x => x == null);
            return acumulator;
        }

        /// <summary>
        /// Gets all possible moves of knights of given player
        /// </summary>
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

        /// <summary>
        /// Gets all possible moves of king of given player
        /// </summary>
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

        /// <summary>
        /// Gets moves in direction of vector, until figure of playing player is encountered, or positions ain't valid
        /// </summary>
        /// <param name="playerColor">Capturing player</param>
        /// <param name="from">Position to move from</param>
        /// <param name="vector">Vector to move to</param>
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

        /// <summary>
        /// Gets move, if there is figure of opponent on moving to position, or its empty, null otherwise
        /// </summary>
        /// <param name="playerColor">Moving player</param>
        /// <param name="from">Position to move from</param>
        /// <param name="to">Position to move to</param>
        private static GameMove CanMoveOrAttack(ChessboardModel chessboard, PlayerColor playerColor, ChessPosition from, ChessPosition to)
        {
            if (!to.IsValid())
                return null;

            var moveTo = chessboard.GetFigureOnPosition(to);

            if (moveTo != null && moveTo.Color == playerColor)
                return null;

            return new GameMove(from, to, chessboard.GetFigureOnPosition(from).Type, chessboard.GetFigureOnPosition(to)?.Type);
        }

        /// <summary>
        /// Gets move, if there is figure of opponent on moving to position, null otherwise
        /// </summary>
        /// <param name="playerColor">Capturing player</param>
        /// <param name="from">Position to capture from</param>
        /// <param name="to">Position to capture to</param>
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

        /// <summary>
        /// Gets move, if there is no figure in the place moving to, null otherwise
        /// </summary>
        /// <param name="playerColor">Moving player</param>
        /// <param name="from">Position to move from</param>
        /// <param name="to">Position to move to</param>
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
