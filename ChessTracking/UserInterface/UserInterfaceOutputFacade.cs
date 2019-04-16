using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ChessTracking.Forms;

namespace ChessTracking.UserInterface
{
    class UserInterfaceOutputFacade
    {
        private MainGameForm MainForm { get; }
        private VizualizationForm VizualizationForm { get; }

        public UserInterfaceOutputFacade(MainGameForm mainForm, VizualizationForm vizualizationForm)
        {
            MainForm = mainForm;
            VizualizationForm = vizualizationForm;
        }

        public void UpdateImmediateBoard(Bitmap bitmap)
        {
            MainForm?.UpdateImmediateBoard(bitmap);
        }

        public void DisplayVizuaization(Bitmap bitmap)
        {
            VizualizationForm?.DisplayVizulization(bitmap);
        }

        public void UpdateFps(int fps)
        {
            MainForm?.UpdateFps(fps);
        }
    }
}
