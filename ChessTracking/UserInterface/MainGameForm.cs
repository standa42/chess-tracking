using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Security;
using System.Windows.Forms;
using ChessTracking.ControllingElements.ProgramState;
using ChessTracking.ImageProcessing.PipelineData;
using ChessTracking.MultithreadingMessages;

namespace ChessTracking.UserInterface
{
    public partial class MainGameForm : Form
    {
        private UserInterfaceInputFacade InputFacade { get; }
        private List<string> TrackingLog { get; }
        private UserDefinedParametersPrototypeFactory UserParameters { get; }
        private AdvancedSettingsForm AdvancedSettingsForm { get; set; }

        public MainGameForm()
        {
            InitializeComponent();
            var vizualizationForm = new VizualizationForm(this);
            vizualizationForm.Show();

            KeepAlive();

            UserParameters = new UserDefinedParametersPrototypeFactory();

            var outputFacade = new UserInterfaceOutputFacade(this, vizualizationForm);
            InputFacade = new UserInterfaceInputFacade(outputFacade, UserParameters);
            
            TrackingLog = new List<string>();

            InitializeVisualisationCombobox();
            Buttons = new List<Button>()
                { NewGameBtn, LoadGameBtn, SaveGameBtn,
                  EndGameBtn, StartTrackingBtn, Recalibrate, StopTrackingBtn };
            InitialUiLockState();
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

        private void ColorCalibrationTrackBar_ValueChanged(object sender, EventArgs e)
        {
            var additiveConstant = ColorCalibrationTrackBar.Value / 100d;

            UserParameters.ChangePrototype(x => x.ColorCalibrationAdditiveConstant = additiveConstant);
        }

        private void ResultProcessingTimer_Tick(object sender, EventArgs e)
        {
            UpdateClock();
            InputFacade.ProcessQueueTick();
        }

        private void UpdateClock()
        {
            string time = DateTime.Now.ToLongTimeString();
            if (ClockLabel.Text != time)
                ClockLabel.Text = time;
        }

        private void VizualizationChoiceComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            var chosenType = ((VisualisationType)VizualizationChoiceComboBox.SelectedIndex);

            UserParameters.ChangePrototype(x => x.VisualisationType = chosenType);
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
                    AddToTrackingLog("Game loading failed");
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
                    AddToTrackingLog("Game saving failed");
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

        private void EndGameBtn_Click(object sender, EventArgs e)
        {
            InputFacade.EndGame();
        }

        private void AdvancedSettingsBtn_Click(object sender, EventArgs e)
        {
            if (AdvancedSettingsForm == null)
            {
                AdvancedSettingsBtn.Enabled = false;
                AdvancedSettingsForm = new AdvancedSettingsForm(this, UserParameters);
                AdvancedSettingsForm.Show();
            }
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

        public void HandDetectionUpdate(bool handDetected)
        {
            HandDetectedBtn.BackColor = handDetected ? Color.LightCoral : Color.Gray;
            HandDetectedBtn.Text = handDetected ? "Scene disrupted" : "";
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

        public void AddToTrackingLog(string line)
        {
            TrackingLog.Add(DateTime.Now.ToLongTimeString() + " - " + line);
            if (TrackingLog != null)
            {
                var temp = new List<string>(TrackingLog);
                temp.Reverse();
                TrackingLogsListBox.DataSource = null;
                TrackingLogsListBox.DataSource = temp;
            }
        }

        public void Clear()
        {
            var temp = new List<string>();
            
            TrackingLog.Clear();

            TrackingLogsListBox.DataSource = null;
            TrackingLogsListBox.DataSource = temp;

            GameHistoryListBox.DataSource = null;
            GameHistoryListBox.DataSource = temp;

            HandDetectedBtn.BackColor = Color.Gray;
            HandDetectedBtn.Text = "";

            ImmediateBoardStatePictureBox.Image = null;
            TrackedBoardStatePictureBox.Image = null;
            GameStatePictureBox.Image = null;
        }

        public void UpdateValidationState(bool? isValid)
        {
            if (!isValid.HasValue)
            {
                ValidationStateBtn.BackColor = Color.LightGray;
                ValidationStateBtn.Text = "Validation State";
            }

            if (isValid.HasValue && isValid.Value)
            {
                ValidationStateBtn.BackColor = Color.LightGreen;
                ValidationStateBtn.Text = "Valid State";
            }

            if (isValid.HasValue && !isValid.Value)
            {
                ValidationStateBtn.BackColor = Color.LightCoral;
                ValidationStateBtn.Text = "Invalid State";
            }
        }

        public void AdvancedFormClosing()
        {
            AdvancedSettingsForm = null;
            AdvancedSettingsBtn.Enabled = true;
        }

        #endregion

        #region UI Locking

        private List<Button> Buttons { get; }

        private void EnableOnlyListedButtons(List<Button> listedButtons)
        {
            foreach (var button in Buttons)
                button.Enabled = listedButtons.Contains(button);
        }

        public void InitialUiLockState()
        {
            EnableOnlyListedButtons(new List<Button>() { NewGameBtn, LoadGameBtn });
            FPSLabel.Visible = false;
            ValidationStateBtn.Visible = false;
            HandDetectedBtn.Visible = false;
        }

        public void GameRunningLockState()
        {
            EnableOnlyListedButtons(new List<Button>() { SaveGameBtn, EndGameBtn, StartTrackingBtn });
            UpdateValidationState(null);
            FPSLabel.Visible = false;
            ValidationStateBtn.Visible = false;
            HandDetectedBtn.Visible = false;
        }

        public void StartedTrackingLockState()
        {
            EnableOnlyListedButtons(new List<Button>() { });
            FPSLabel.Visible = true;
            ValidationStateBtn.Visible = true;
            HandDetectedBtn.Visible = true;
        }

        public void TrackingLockState()
        {
            EnableOnlyListedButtons(new List<Button>() { SaveGameBtn, Recalibrate, StopTrackingBtn });
        }

        public void GameFinishedLockState()
        {
            EnableOnlyListedButtons(new List<Button>() { SaveGameBtn, EndGameBtn });
            UpdateValidationState(null);
            FPSLabel.Visible = false;
            ValidationStateBtn.Visible = false;
            HandDetectedBtn.Visible = false;
        }

        #endregion

        #region Disabling display sleep
        [FlagsAttribute]
        public enum EXECUTION_STATE : uint
        {
            ES_AWAYMODE_REQUIRED = 0x00000040,
            ES_CONTINUOUS = 0x80000000,
            ES_DISPLAY_REQUIRED = 0x00000002,
            ES_SYSTEM_REQUIRED = 0x00000001
            // Legacy flag, should not be used.
            // ES_USER_PRESENT = 0x00000004
        }

        /// <summary>
        /// Forces screen to stay active
        /// </summary>
        [DllImport("kernel32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        static extern uint SetThreadExecutionState(EXECUTION_STATE esFlags);
        private void KeepAlive()
        {
            SetThreadExecutionState(EXECUTION_STATE.ES_DISPLAY_REQUIRED | EXECUTION_STATE.ES_CONTINUOUS);
        }

        #endregion

        
    }
}
