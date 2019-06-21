using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ChessTracking.ImageProcessing.PipelineData;
using MaterialSkin;
using MaterialSkin.Controls;
using Bitmap = System.Drawing.Bitmap;

namespace ChessTracking.UserInterface
{
    public partial class CalibrationSnapshotForm : MaterialForm
    {
        private MainGameForm MainForm { get; }
        private List<Tuple<string,Bitmap>> Data { get; set; }
        private int CurrentPosition { get; set; } = 0;

        public CalibrationSnapshotForm(MainGameForm mainForm, SceneCalibrationSnapshot snapshot)
        {
            this.MainForm = mainForm;

            InitializeComponent();

            var materialSkinManager = MaterialSkinManager.Instance;
            materialSkinManager.AddFormToManage(this);

            Update(snapshot);
        }
        
        private void CalibrationSnapshotForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            MainForm.SceneSnapshotFormClosing();
        }

        public void Update(SceneCalibrationSnapshot snapshot)
        {
            if (snapshot == null)
            {
                NameLabel.Text = "Null data came";
                LeftButton.Visible = false;
                RightButton.Visible = false;
                return;
            }

            LeftButton.Visible = true;
            RightButton.Visible = true;
            GenerateListFromSnapshot(snapshot);
            CurrentPosition = 0;
            UpdatePicturebox();
        }

        public void Reset()
        {
            CurrentPosition = 0;
        }

        private void LeftButton_Click(object sender, EventArgs e)
        {
            CurrentPosition--;
            UpdatePicturebox();
        }

        private void RightButton_Click(object sender, EventArgs e)
        {
            CurrentPosition++;
            UpdatePicturebox();
        }

        private void UpdatePicturebox()
        {
            if (CurrentPosition >= Data.Count)
                CurrentPosition = 0;
            if (CurrentPosition < 0)
                CurrentPosition = Data.Count - 1;

            if (Data[CurrentPosition].Item2 != null)
            {
                PictureBox1.Image = Data[CurrentPosition].Item2;
                NameLabel.Text = Data[CurrentPosition].Item1;
            }
        }

        private void GenerateListFromSnapshot(SceneCalibrationSnapshot snapshot)
        {
            Data = new List<Tuple<string, Bitmap>>()
            {
                new Tuple<string, Bitmap>(nameof(snapshot.BinarizationImage), snapshot.BinarizationImage),
                new Tuple<string, Bitmap>(nameof(snapshot.CannyImage), snapshot.CannyImage),
                new Tuple<string, Bitmap>(nameof(snapshot.GrayImage), snapshot.GrayImage),
                new Tuple<string, Bitmap>(nameof(snapshot.MaskedColorImage), snapshot.MaskedColorImage)
            };
        }
    }
}
