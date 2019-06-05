using System.Drawing;
using System.Windows.Forms;
using MaterialSkin;
using MaterialSkin.Controls;

namespace ChessTracking.UserInterface
{
    public partial class VizualizationForm : MaterialForm
    {
        /// <summary>
        /// Reference to main form of application
        /// </summary>
        public MainGameForm MainForm { get; }

        public VizualizationForm(MainGameForm mainForm)
        {
            InitializeComponent();
            MainForm = mainForm;

            var materialSkinManager = MaterialSkinManager.Instance;
            materialSkinManager.AddFormToManage(this);
            materialSkinManager.Theme = MaterialSkinManager.Themes.LIGHT;
            materialSkinManager.ColorScheme = new ColorScheme(Primary.BlueGrey800, Primary.BlueGrey900, Primary.BlueGrey500, Accent.LightBlue200, TextShade.WHITE);
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
