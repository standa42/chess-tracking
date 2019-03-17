using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ChessTracking.ControllingElements;

namespace ChessTracking.Forms
{
    public partial class MainGameForm : Form
    {
        private VizualizationForm VizualizationForm { get; }
        private TrackingManager TrackingManager { get; }

        public MainGameForm()
        {
            InitializeComponent();
            VizualizationForm = new VizualizationForm();
            VizualizationForm.Show();
            TrackingManager = new TrackingManager(this);
        }

        public void DisplayVizuaization(Bitmap bitmap)
        {
            VizualizationForm?.DisplayVizulization(bitmap);
        }

        #region Click events

        private void NewGameBtn_Click(object sender, EventArgs e)
        {

        }

        private void LoadGameBtn_Click(object sender, EventArgs e)
        {

        }

        private void SaveGameBtn_Click(object sender, EventArgs e)
        {

        }

        private void StartTrackingBtn_Click(object sender, EventArgs e)
        {
            TrackingManager.StartTracking();
        }

        private void RestartTrackingBtn_Click(object sender, EventArgs e)
        {

        }

        private void StopTrackingBtn_Click(object sender, EventArgs e)
        {
            TrackingManager.StopTracking();
        }


        #endregion

        private void ResultProcessingTimer_Tick(object sender, EventArgs e)
        {
            TrackingManager?.ProcessQueue();
        }
    }
}
