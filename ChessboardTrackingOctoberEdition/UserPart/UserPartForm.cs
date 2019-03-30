using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ChessboardTrackingOctoberEdition.TrackingPart;
using ChessboardTrackingOctoberEdition.UserPart;

namespace ChessboardTrackingOctoberEdition
{
    public partial class UserPartForm : Form
    {
        private Game game;
        private TrackingPartForm trackingForm;

        public UserPartForm()
        {
            InitializeComponent();

            trackingForm = new TrackingPartForm();
            trackingForm.Show();
        }

        private void NewGameButton_Click(object sender, EventArgs e)
        {
            game = GameFactory.NewGame();
        }

        private void StartTrackingButton_Click(object sender, EventArgs e)
        {
            game?.StartTracking(this, trackingForm);
        }

        private void StopTrackingButton_Click(object sender, EventArgs e)
        {
            game?.StopTracking();
        }
    }
}
