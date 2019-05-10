using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChessTracking.Game
{
    public static class GameRenderer
    {
        private static Bitmap ChessboardBitmap { get; set; }

        static GameRenderer()
        {
            ChessboardBitmap = new Bitmap($@"img\Chessboard.png");
        }

        public static Bitmap RenderGameState(GameData game)
        {
            var bm = (Bitmap)ChessboardBitmap.Clone();
            
            for (int x = 0; x < 8; x++)
            {
                for (int y = 0; y < 8; y++)
                {
                    using (var graphics = Graphics.FromImage(bm))
                    {
                        // inverting y coordinate because of difference in
                        // chess and bitmap coordinates
                        var figure = game.Chessboard.Figures[x, 7 - y];

                        if (figure != null)
                        {
                            var figureBitmap = Figure.GetBitmapRepresentation(figure.Type, figure.Color);

                            graphics.DrawImageUnscaled(
                                figureBitmap,
                                x * 140,
                                y * 140);
                        }
                    }
                }
            }
            
            return bm;
        }
    }
}
