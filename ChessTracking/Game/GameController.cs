using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ChessTracking.Game.Figures;
using ChessTracking.MultithreadingMessages;
using ChessTracking.UserInterface;

namespace ChessTracking.Game
{
    class GameController
    {
        private UserInterfaceOutputFacade OutputFacade { get; }
        private GameData Game { get; set; }

        public GameController(UserInterfaceOutputFacade outputFacade)
        {
            OutputFacade = outputFacade;
        }

        public int? InitiateWithTracingInput(TrackingState trackingState)
        {
            var figures = Game.GetTrackingStates().Figures;

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
            var tsfigures = trackingState.Figures;
            var gamefigures = Game.GetTrackingStates().Figures;

            Position missing = null;
            Position appearance = null;

            for (int x = 0; x < 8; x++)
            {
                for (int y = 0; y < 8; y++)
                {
                    if (gamefigures[x,y] != TrackingFieldState.None && tsfigures[x,y] == TrackingFieldState.None)
                    {
                        missing = new Position(x,y);
                    }

                    if (gamefigures[x, y] == TrackingFieldState.None && tsfigures[x, y] != TrackingFieldState.None)
                    {
                        appearance = new Position(x,y);
                    }

                    if (
                        (gamefigures[x, y] == TrackingFieldState.Black && tsfigures[x, y] == TrackingFieldState.White) ||
                        (gamefigures[x, y] == TrackingFieldState.White && tsfigures[x, y] == TrackingFieldState.Black)
                        )
                    {
                        appearance = new Position(x, y);
                    }
                }
            }

            if (missing != null && appearance != null)
            {
                Game.Figures[appearance.X, appearance.Y] = Game.Figures[missing.X, missing.Y];
                Game.Figures[missing.X, missing.Y] = null;
            }

            OutputFacade.UpdateBoardState(RenderGameState());
        }

        public void NewGame()
        {
            var figures = new Figure[8, 8];

            figures[0, 0] = new Vez(new Position(0, 0), true);
            figures[1, 0] = new Jezdec(new Position(1, 0), true);
            figures[2, 0] = new Strelec(new Position(2, 0), true);
            figures[3, 0] = new Kral(new Position(3, 0), true);
            figures[4, 0] = new Dama(new Position(4, 0), true);
            figures[5, 0] = new Strelec(new Position(5, 0), true);
            figures[6, 0] = new Jezdec(new Position(6, 0), true);
            figures[7, 0] = new Vez(new Position(7, 0), true);

            figures[0, 1] = new Pesec(new Position(0, 1), true);
            figures[1, 1] = new Pesec(new Position(1, 1), true);
            figures[2, 1] = new Pesec(new Position(2, 1), true);
            figures[3, 1] = new Pesec(new Position(3, 1), true);
            figures[4, 1] = new Pesec(new Position(4, 1), true);
            figures[5, 1] = new Pesec(new Position(5, 1), true);
            figures[6, 1] = new Pesec(new Position(6, 1), true);
            figures[7, 1] = new Pesec(new Position(7, 1), true);

            figures[0, 7] = new Vez(new Position(0, 7), false);
            figures[1, 7] = new Jezdec(new Position(1, 7), false);
            figures[2, 7] = new Strelec(new Position(2, 7), false);
            figures[3, 7] = new Kral(new Position(3, 7), false);
            figures[4, 7] = new Dama(new Position(4, 7), false);
            figures[5, 7] = new Strelec(new Position(5, 7), false);
            figures[6, 7] = new Jezdec(new Position(6, 7), false);
            figures[7, 7] = new Vez(new Position(7, 7), false);

            figures[0, 6] = new Pesec(new Position(0, 6), false);
            figures[1, 6] = new Pesec(new Position(1, 6), false);
            figures[2, 6] = new Pesec(new Position(2, 6), false);
            figures[3, 6] = new Pesec(new Position(3, 6), false);
            figures[4, 6] = new Pesec(new Position(4, 6), false);
            figures[5, 6] = new Pesec(new Position(5, 6), false);
            figures[6, 6] = new Pesec(new Position(6, 6), false);
            figures[7, 6] = new Pesec(new Position(7, 6), false);

            Game = new GameData(figures, isWhitePlaying: true);

            OutputFacade.UpdateBoardState(RenderGameState());
        }

        public void SaveGame()
        {

        }

        public void LoadGame()
        {

        }

        public Bitmap RenderGameState()
        {
            var bm = new Bitmap(320, 320, PixelFormat.Format24bppRgb);
            SolidBrush blackBrush = new SolidBrush(Color.Black);
            SolidBrush whiteBrush = new SolidBrush(Color.White);
            SolidBrush blueBrush = new SolidBrush(Color.LightSkyBlue);
            
            using (Graphics graphics = Graphics.FromImage(bm))
            {
                graphics.Clear(Color.LightSkyBlue);
            }

            for (int x = 0; x < 8; x++)
            {
                for (int y = 0; y < 8; y++)
                {
                    using (Graphics graphics = Graphics.FromImage(bm))
                    {
                        if (Game.Figures[x, y] != null)
                        {
                            graphics.DrawImageUnscaled(Game.Figures[x, y].ImageBitmap, x * 40, y * 40);
                        }
                    }
                }
            }

            bm.RotateFlip(RotateFlipType.Rotate180FlipNone);

            return bm;
        }
    }
}
