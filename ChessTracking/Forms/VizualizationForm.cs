using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ChessTracking.Forms
{
    public partial class VizualizationForm : Form
    {
        public MainGameForm MainForm { get; }

        public VizualizationForm(MainGameForm mainForm)
        {
            InitializeComponent();
            MainForm = mainForm;
        }

        public void DisplayVizulization(Bitmap bitmap)
        {
            VizualizationPictureBox.Image = bitmap;
        }

        private void VizualizationForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            MainForm.Close();
            // e.Cancel = true;
        }
    }
}
