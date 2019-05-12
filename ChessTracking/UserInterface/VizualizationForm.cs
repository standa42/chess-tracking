using System.Drawing;
using System.Windows.Forms;

namespace ChessTracking.UserInterface
{
    public partial class VizualizationForm : Form
    {
        /// <summary>
        /// Reference to main form of application
        /// </summary>
        public MainGameForm MainForm { get; }

        public VizualizationForm(MainGameForm mainForm)
        {
            InitializeComponent();
            MainForm = mainForm;
        }

        /// <summary>
        /// Sets displayed image
        /// </summary>
        /// <param name="bitmap"></param>
        public void DisplayVizulization(Bitmap bitmap)
        {
            if (bitmap != null)
            {
                VizualizationPictureBox.Image = bitmap;
            }
        }
    
        /// <summary>
        /// Closes main form if closed
        /// </summary>
        private void VizualizationForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            MainForm.Close();
        }
    }
}
