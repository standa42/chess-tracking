using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ChessboardTrackingOctoberEdition.TrackingPart
{
    public partial class TrackingPartForm : Form
    {
        private int frameCount;
        private DateTime originTime = DateTime.Now;
        private int secondsThatLastedSinceOrigin;
        private int lastImportantFrameCount;

        public TrackingPartForm()
        {
            InitializeComponent();
        }

        public void IncreaseFrameCount()
        {
            FrameCountLabel.Text = "Frame count: " + (++frameCount);

            if ( (DateTime.Now - originTime).TotalSeconds > (secondsThatLastedSinceOrigin) )
            {
                FpsLabel.Text = "FPS: " + (frameCount - lastImportantFrameCount);
                lastImportantFrameCount = frameCount;
                secondsThatLastedSinceOrigin++;
            }
        }

        private void TrackingPartForm_Load(object sender, EventArgs e)
        {

        }


    }
}
