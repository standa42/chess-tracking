using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ChessboardTrackingOctoberEdition.Shared
{
    public partial class DisplayForm : Form
    {
        public DisplayForm()
        {
            InitializeComponent();
        }

        public void SetDimensions(int width, int height)
        {
            this.Size = new Size((int)(width * 1.05f), (int)(height * 1.1f));
            DisplayingPictureBox.Size = new Size(width, height);
        }

        public void DisplayBitmap(Bitmap bitmap)
        {
            DisplayingPictureBox.Image = bitmap;
        }

        public void DisplayImage(Image image)
        {
            DisplayingPictureBox.Image = image;
        }

        private void DisplayForm_Load(object sender, EventArgs e)
        {

        }


    }
}
