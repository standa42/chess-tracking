using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Security;
using System.Windows.Forms;
using ChessTracking.Forms;
using ChessTracking.MultithreadingMessages;

namespace ChessTracking.UserInterface
{
    public partial class MainGameForm : Form
    {
        private UserInterfaceInputFacade InputFacade { get; }

        public MainGameForm()
        {
            InitializeComponent();
            var vizualizationForm = new VizualizationForm(this);
            vizualizationForm.Show();

            var outputFacade = new UserInterfaceOutputFacade(this, vizualizationForm);
            InputFacade = new UserInterfaceInputFacade(outputFacade);

            InitializeVisualisationCombobox();
        }

        #region Init

        private void InitializeVisualisationCombobox()
        {
            var values = Enum.GetValues(typeof(VisualisationType)).Cast<VisualisationType>().Cast<int>();
            VizualizationChoiceComboBox.DataSource = values;
            VizualizationChoiceComboBox.SelectedIndex = (int)VisualisationType.RawRGB; 
        }

        #endregion

        #region Input events

        private void ResultProcessingTimer_Tick(object sender, EventArgs e)
        {
            InputFacade.ProcessQueueTick();
        }

        private void VizualizationChoiceComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            var chosenType = ((VisualisationType) VizualizationChoiceComboBox.SelectedIndex);
            InputFacade.ChangeVisualisation(chosenType);
        }

        private void NewGameBtn_Click(object sender, EventArgs e)
        {
            InputFacade.NewGame();
        }

        private void LoadGameBtn_Click(object sender, EventArgs e)
        {
            var fileDialog = new OpenFileDialog();
            if (fileDialog.ShowDialog() == DialogResult.OK)
            {
                StreamReader stream = null;
                try
                {
                    stream = new StreamReader(fileDialog.FileName);
                    InputFacade.LoadGame(stream);
                }
                catch (SecurityException)
                {
                    // TODO: má se tu něco dělat?
                }
                finally
                {
                    stream?.Close();
                }
            }
        }

        private void SaveGameBtn_Click(object sender, EventArgs e)
        {
            var fileDialog = new SaveFileDialog();
            if (fileDialog.ShowDialog() == DialogResult.OK)
            {
                StreamWriter stream = null;
                try
                {
                    stream = new StreamWriter(fileDialog.FileName);
                    InputFacade.SaveGame(stream);
                }
                catch (SecurityException)
                {
                    // TODO: má se tu něco dělat?
                }
                finally
                {
                    stream?.Close();
                }
            }
        }

        private void StartTrackingBtn_Click(object sender, EventArgs e)
        {
            InputFacade.StartTracking();
        }

        private void RecalibrateBtn_Click(object sender, EventArgs e)
        {
            InputFacade.Recalibrate();
        }

        private void StopTrackingBtn_Click(object sender, EventArgs e)
        {
            InputFacade.StopTracking();
        }

        #endregion
        
        #region Form updates

        public void UpdateFps(int fps)
        {
            FPSLabel.Text = $@"FPS: {fps}";
        }

        public void UpdateImmediateBoard(Bitmap bitmap)
        {
            if (bitmap != null)
            {
                ImmediateBoardStatePictureBox.Image = bitmap;
            }
        }

        public void HandDetectionUpdate(string state)
        {
            HandDetectedBtn.BackColor = state == "true" ? Color.LightCoral : Color.Gray;
            HandDetectedBtn.Text = state == "true" ? "Scene disrupted" : "";
        }

        public void UpdateAveragedBoard(Bitmap bitmap)
        {
            if (bitmap != null)
            {
                TrackedBoardStatePictureBox.Image = bitmap;
            }
        }

        public void UpdateBoardState(Bitmap bitmap)
        {
            if (bitmap != null)
            {
                GameStatePictureBox.Image = bitmap;
            }
        }

        public void UpdateRecordState(IList<string> records)
        {
            if (records != null)
            {
                GameHistoryListBox.DataSource = null;
                GameHistoryListBox.DataSource = records;
            }
        }

        private void ColorCalibrationTrackBar_ValueChanged(object sender, EventArgs e)
        {
            var additiveConstant = ColorCalibrationTrackBar.Value / 100d;

            InputFacade.CalibrateColor(additiveConstant);
        }


        #endregion
    }
}
