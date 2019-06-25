namespace ChessTracking.UserInterface
{
    partial class MainGameForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainGameForm));
            this.GameStatePictureBox = new System.Windows.Forms.PictureBox();
            this.TrackedBoardStatePictureBox = new System.Windows.Forms.PictureBox();
            this.ImmediateBoardStatePictureBox = new System.Windows.Forms.PictureBox();
            this.VizualizationChoiceComboBox = new System.Windows.Forms.ComboBox();
            this.ResultProcessingTimer = new System.Windows.Forms.Timer(this.components);
            this.ColorCalibrationTrackBar = new System.Windows.Forms.TrackBar();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.pictureBox2 = new System.Windows.Forms.PictureBox();
            this.materialLabel1 = new MaterialSkin.Controls.MaterialLabel();
            this.materialLabel2 = new MaterialSkin.Controls.MaterialLabel();
            this.materialLabel3 = new MaterialSkin.Controls.MaterialLabel();
            this.NewGameBtn = new MaterialSkin.Controls.MaterialFlatButton();
            this.LoadGameBtn = new MaterialSkin.Controls.MaterialFlatButton();
            this.SaveGameBtn = new MaterialSkin.Controls.MaterialFlatButton();
            this.EndGameBtn = new MaterialSkin.Controls.MaterialFlatButton();
            this.StartTrackingBtn = new MaterialSkin.Controls.MaterialFlatButton();
            this.Recalibrate = new MaterialSkin.Controls.MaterialFlatButton();
            this.StopTrackingBtn = new MaterialSkin.Controls.MaterialFlatButton();
            this.GameHistoryListBox = new MaterialSkin.Controls.MaterialListView();
            this.Record = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.TrackingLogsListBox = new MaterialSkin.Controls.MaterialListView();
            this.TrackingLogColumn = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.WhosPlayingLabel = new MaterialSkin.Controls.MaterialLabel();
            this.FPSLabel = new MaterialSkin.Controls.MaterialLabel();
            this.materialLabel6 = new MaterialSkin.Controls.MaterialLabel();
            this.materialLabel7 = new MaterialSkin.Controls.MaterialLabel();
            this.AdvancedSettingsBtn = new MaterialSkin.Controls.MaterialFlatButton();
            this.SceneDisruptionBtn = new MaterialSkin.Controls.MaterialLabel();
            this.ValidationStateBtn = new MaterialSkin.Controls.MaterialLabel();
            this.MovementBtn1 = new MaterialSkin.Controls.MaterialFlatButton();
            this.MovementBtn2 = new MaterialSkin.Controls.MaterialFlatButton();
            this.MovementBtn3 = new MaterialSkin.Controls.MaterialFlatButton();
            this.MovementBtn4 = new MaterialSkin.Controls.MaterialFlatButton();
            this.materialLabel4 = new MaterialSkin.Controls.MaterialLabel();
            this.CalibrationSnapshotsButton = new MaterialSkin.Controls.MaterialFlatButton();
            ((System.ComponentModel.ISupportInitialize)(this.GameStatePictureBox)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.TrackedBoardStatePictureBox)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ImmediateBoardStatePictureBox)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ColorCalibrationTrackBar)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
            this.SuspendLayout();
            // 
            // GameStatePictureBox
            // 
            this.GameStatePictureBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            resources.ApplyResources(this.GameStatePictureBox, "GameStatePictureBox");
            this.GameStatePictureBox.Name = "GameStatePictureBox";
            this.GameStatePictureBox.TabStop = false;
            // 
            // TrackedBoardStatePictureBox
            // 
            this.TrackedBoardStatePictureBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            resources.ApplyResources(this.TrackedBoardStatePictureBox, "TrackedBoardStatePictureBox");
            this.TrackedBoardStatePictureBox.Name = "TrackedBoardStatePictureBox";
            this.TrackedBoardStatePictureBox.TabStop = false;
            // 
            // ImmediateBoardStatePictureBox
            // 
            this.ImmediateBoardStatePictureBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            resources.ApplyResources(this.ImmediateBoardStatePictureBox, "ImmediateBoardStatePictureBox");
            this.ImmediateBoardStatePictureBox.Name = "ImmediateBoardStatePictureBox";
            this.ImmediateBoardStatePictureBox.TabStop = false;
            // 
            // VizualizationChoiceComboBox
            // 
            resources.ApplyResources(this.VizualizationChoiceComboBox, "VizualizationChoiceComboBox");
            this.VizualizationChoiceComboBox.FormattingEnabled = true;
            this.VizualizationChoiceComboBox.Name = "VizualizationChoiceComboBox";
            this.VizualizationChoiceComboBox.SelectedIndexChanged += new System.EventHandler(this.VizualizationChoiceComboBox_SelectedIndexChanged);
            // 
            // ResultProcessingTimer
            // 
            this.ResultProcessingTimer.Enabled = true;
            this.ResultProcessingTimer.Interval = 30;
            this.ResultProcessingTimer.Tick += new System.EventHandler(this.ResultProcessingTimer_Tick);
            // 
            // ColorCalibrationTrackBar
            // 
            resources.ApplyResources(this.ColorCalibrationTrackBar, "ColorCalibrationTrackBar");
            this.ColorCalibrationTrackBar.LargeChange = 1;
            this.ColorCalibrationTrackBar.Maximum = 150;
            this.ColorCalibrationTrackBar.Minimum = -150;
            this.ColorCalibrationTrackBar.Name = "ColorCalibrationTrackBar";
            this.ColorCalibrationTrackBar.ValueChanged += new System.EventHandler(this.ColorCalibrationTrackBar_ValueChanged);
            // 
            // pictureBox1
            // 
            this.pictureBox1.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.pictureBox1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            resources.ApplyResources(this.pictureBox1, "pictureBox1");
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.TabStop = false;
            // 
            // pictureBox2
            // 
            this.pictureBox2.BackColor = System.Drawing.SystemColors.ControlText;
            this.pictureBox2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            resources.ApplyResources(this.pictureBox2, "pictureBox2");
            this.pictureBox2.Name = "pictureBox2";
            this.pictureBox2.TabStop = false;
            // 
            // materialLabel1
            // 
            resources.ApplyResources(this.materialLabel1, "materialLabel1");
            this.materialLabel1.Depth = 0;
            this.materialLabel1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(222)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.materialLabel1.MouseState = MaterialSkin.MouseState.HOVER;
            this.materialLabel1.Name = "materialLabel1";
            // 
            // materialLabel2
            // 
            resources.ApplyResources(this.materialLabel2, "materialLabel2");
            this.materialLabel2.Depth = 0;
            this.materialLabel2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(222)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.materialLabel2.MouseState = MaterialSkin.MouseState.HOVER;
            this.materialLabel2.Name = "materialLabel2";
            // 
            // materialLabel3
            // 
            resources.ApplyResources(this.materialLabel3, "materialLabel3");
            this.materialLabel3.Depth = 0;
            this.materialLabel3.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(222)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.materialLabel3.MouseState = MaterialSkin.MouseState.HOVER;
            this.materialLabel3.Name = "materialLabel3";
            // 
            // NewGameBtn
            // 
            resources.ApplyResources(this.NewGameBtn, "NewGameBtn");
            this.NewGameBtn.BackColor = System.Drawing.SystemColors.Control;
            this.NewGameBtn.Depth = 0;
            this.NewGameBtn.Icon = null;
            this.NewGameBtn.MouseState = MaterialSkin.MouseState.HOVER;
            this.NewGameBtn.Name = "NewGameBtn";
            this.NewGameBtn.Primary = false;
            this.NewGameBtn.UseVisualStyleBackColor = false;
            this.NewGameBtn.Click += new System.EventHandler(this.NewGameBtn_Click);
            // 
            // LoadGameBtn
            // 
            resources.ApplyResources(this.LoadGameBtn, "LoadGameBtn");
            this.LoadGameBtn.BackColor = System.Drawing.SystemColors.Control;
            this.LoadGameBtn.Depth = 0;
            this.LoadGameBtn.Icon = null;
            this.LoadGameBtn.MouseState = MaterialSkin.MouseState.HOVER;
            this.LoadGameBtn.Name = "LoadGameBtn";
            this.LoadGameBtn.Primary = false;
            this.LoadGameBtn.UseVisualStyleBackColor = false;
            this.LoadGameBtn.Click += new System.EventHandler(this.LoadGameBtn_Click);
            // 
            // SaveGameBtn
            // 
            resources.ApplyResources(this.SaveGameBtn, "SaveGameBtn");
            this.SaveGameBtn.BackColor = System.Drawing.SystemColors.Control;
            this.SaveGameBtn.Depth = 0;
            this.SaveGameBtn.Icon = null;
            this.SaveGameBtn.MouseState = MaterialSkin.MouseState.HOVER;
            this.SaveGameBtn.Name = "SaveGameBtn";
            this.SaveGameBtn.Primary = false;
            this.SaveGameBtn.UseVisualStyleBackColor = false;
            this.SaveGameBtn.Click += new System.EventHandler(this.SaveGameBtn_Click);
            // 
            // EndGameBtn
            // 
            resources.ApplyResources(this.EndGameBtn, "EndGameBtn");
            this.EndGameBtn.BackColor = System.Drawing.SystemColors.Control;
            this.EndGameBtn.Depth = 0;
            this.EndGameBtn.Icon = null;
            this.EndGameBtn.MouseState = MaterialSkin.MouseState.HOVER;
            this.EndGameBtn.Name = "EndGameBtn";
            this.EndGameBtn.Primary = false;
            this.EndGameBtn.UseVisualStyleBackColor = false;
            this.EndGameBtn.Click += new System.EventHandler(this.EndGameBtn_Click);
            // 
            // StartTrackingBtn
            // 
            resources.ApplyResources(this.StartTrackingBtn, "StartTrackingBtn");
            this.StartTrackingBtn.BackColor = System.Drawing.SystemColors.Control;
            this.StartTrackingBtn.Depth = 0;
            this.StartTrackingBtn.Icon = null;
            this.StartTrackingBtn.MouseState = MaterialSkin.MouseState.HOVER;
            this.StartTrackingBtn.Name = "StartTrackingBtn";
            this.StartTrackingBtn.Primary = false;
            this.StartTrackingBtn.UseVisualStyleBackColor = false;
            this.StartTrackingBtn.Click += new System.EventHandler(this.StartTrackingBtn_Click);
            // 
            // Recalibrate
            // 
            resources.ApplyResources(this.Recalibrate, "Recalibrate");
            this.Recalibrate.BackColor = System.Drawing.SystemColors.Control;
            this.Recalibrate.Depth = 0;
            this.Recalibrate.Icon = null;
            this.Recalibrate.MouseState = MaterialSkin.MouseState.HOVER;
            this.Recalibrate.Name = "Recalibrate";
            this.Recalibrate.Primary = false;
            this.Recalibrate.UseVisualStyleBackColor = false;
            this.Recalibrate.Click += new System.EventHandler(this.RecalibrateBtn_Click);
            // 
            // StopTrackingBtn
            // 
            resources.ApplyResources(this.StopTrackingBtn, "StopTrackingBtn");
            this.StopTrackingBtn.BackColor = System.Drawing.SystemColors.Control;
            this.StopTrackingBtn.Depth = 0;
            this.StopTrackingBtn.Icon = null;
            this.StopTrackingBtn.MouseState = MaterialSkin.MouseState.HOVER;
            this.StopTrackingBtn.Name = "StopTrackingBtn";
            this.StopTrackingBtn.Primary = false;
            this.StopTrackingBtn.UseVisualStyleBackColor = false;
            this.StopTrackingBtn.Click += new System.EventHandler(this.StopTrackingBtn_Click);
            // 
            // GameHistoryListBox
            // 
            this.GameHistoryListBox.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.GameHistoryListBox.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.Record});
            this.GameHistoryListBox.Depth = 0;
            resources.ApplyResources(this.GameHistoryListBox, "GameHistoryListBox");
            this.GameHistoryListBox.FullRowSelect = true;
            this.GameHistoryListBox.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable;
            this.GameHistoryListBox.MouseLocation = new System.Drawing.Point(-1, -1);
            this.GameHistoryListBox.MouseState = MaterialSkin.MouseState.OUT;
            this.GameHistoryListBox.Name = "GameHistoryListBox";
            this.GameHistoryListBox.OwnerDraw = true;
            this.GameHistoryListBox.UseCompatibleStateImageBehavior = false;
            this.GameHistoryListBox.View = System.Windows.Forms.View.Details;
            // 
            // Record
            // 
            resources.ApplyResources(this.Record, "Record");
            // 
            // TrackingLogsListBox
            // 
            this.TrackingLogsListBox.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.TrackingLogsListBox.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.TrackingLogColumn});
            this.TrackingLogsListBox.Depth = 0;
            resources.ApplyResources(this.TrackingLogsListBox, "TrackingLogsListBox");
            this.TrackingLogsListBox.FullRowSelect = true;
            this.TrackingLogsListBox.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable;
            this.TrackingLogsListBox.MouseLocation = new System.Drawing.Point(-1, -1);
            this.TrackingLogsListBox.MouseState = MaterialSkin.MouseState.OUT;
            this.TrackingLogsListBox.Name = "TrackingLogsListBox";
            this.TrackingLogsListBox.OwnerDraw = true;
            this.TrackingLogsListBox.UseCompatibleStateImageBehavior = false;
            this.TrackingLogsListBox.View = System.Windows.Forms.View.Details;
            // 
            // TrackingLogColumn
            // 
            resources.ApplyResources(this.TrackingLogColumn, "TrackingLogColumn");
            // 
            // WhosPlayingLabel
            // 
            resources.ApplyResources(this.WhosPlayingLabel, "WhosPlayingLabel");
            this.WhosPlayingLabel.Depth = 0;
            this.WhosPlayingLabel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(222)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.WhosPlayingLabel.MouseState = MaterialSkin.MouseState.HOVER;
            this.WhosPlayingLabel.Name = "WhosPlayingLabel";
            // 
            // FPSLabel
            // 
            resources.ApplyResources(this.FPSLabel, "FPSLabel");
            this.FPSLabel.Depth = 0;
            this.FPSLabel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(222)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.FPSLabel.MouseState = MaterialSkin.MouseState.HOVER;
            this.FPSLabel.Name = "FPSLabel";
            // 
            // materialLabel6
            // 
            resources.ApplyResources(this.materialLabel6, "materialLabel6");
            this.materialLabel6.Depth = 0;
            this.materialLabel6.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(222)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.materialLabel6.MouseState = MaterialSkin.MouseState.HOVER;
            this.materialLabel6.Name = "materialLabel6";
            // 
            // materialLabel7
            // 
            resources.ApplyResources(this.materialLabel7, "materialLabel7");
            this.materialLabel7.Depth = 0;
            this.materialLabel7.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(222)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.materialLabel7.MouseState = MaterialSkin.MouseState.HOVER;
            this.materialLabel7.Name = "materialLabel7";
            // 
            // AdvancedSettingsBtn
            // 
            resources.ApplyResources(this.AdvancedSettingsBtn, "AdvancedSettingsBtn");
            this.AdvancedSettingsBtn.BackColor = System.Drawing.SystemColors.Control;
            this.AdvancedSettingsBtn.Depth = 0;
            this.AdvancedSettingsBtn.Icon = null;
            this.AdvancedSettingsBtn.MouseState = MaterialSkin.MouseState.HOVER;
            this.AdvancedSettingsBtn.Name = "AdvancedSettingsBtn";
            this.AdvancedSettingsBtn.Primary = false;
            this.AdvancedSettingsBtn.UseVisualStyleBackColor = false;
            this.AdvancedSettingsBtn.Click += new System.EventHandler(this.AdvancedSettingsBtn_Click);
            // 
            // SceneDisruptionBtn
            // 
            this.SceneDisruptionBtn.BackColor = System.Drawing.SystemColors.ControlLight;
            this.SceneDisruptionBtn.Depth = 0;
            resources.ApplyResources(this.SceneDisruptionBtn, "SceneDisruptionBtn");
            this.SceneDisruptionBtn.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(222)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.SceneDisruptionBtn.MouseState = MaterialSkin.MouseState.HOVER;
            this.SceneDisruptionBtn.Name = "SceneDisruptionBtn";
            // 
            // ValidationStateBtn
            // 
            this.ValidationStateBtn.BackColor = System.Drawing.SystemColors.ControlLight;
            this.ValidationStateBtn.Depth = 0;
            resources.ApplyResources(this.ValidationStateBtn, "ValidationStateBtn");
            this.ValidationStateBtn.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(222)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.ValidationStateBtn.MouseState = MaterialSkin.MouseState.HOVER;
            this.ValidationStateBtn.Name = "ValidationStateBtn";
            // 
            // MovementBtn1
            // 
            resources.ApplyResources(this.MovementBtn1, "MovementBtn1");
            this.MovementBtn1.Depth = 0;
            this.MovementBtn1.Icon = null;
            this.MovementBtn1.MouseState = MaterialSkin.MouseState.HOVER;
            this.MovementBtn1.Name = "MovementBtn1";
            this.MovementBtn1.Primary = false;
            this.MovementBtn1.UseVisualStyleBackColor = true;
            this.MovementBtn1.Click += new System.EventHandler(this.MovementBtn1_Click);
            // 
            // MovementBtn2
            // 
            resources.ApplyResources(this.MovementBtn2, "MovementBtn2");
            this.MovementBtn2.Depth = 0;
            this.MovementBtn2.Icon = null;
            this.MovementBtn2.MouseState = MaterialSkin.MouseState.HOVER;
            this.MovementBtn2.Name = "MovementBtn2";
            this.MovementBtn2.Primary = false;
            this.MovementBtn2.UseVisualStyleBackColor = true;
            this.MovementBtn2.Click += new System.EventHandler(this.MovementBtn2_Click);
            // 
            // MovementBtn3
            // 
            resources.ApplyResources(this.MovementBtn3, "MovementBtn3");
            this.MovementBtn3.Depth = 0;
            this.MovementBtn3.Icon = null;
            this.MovementBtn3.MouseState = MaterialSkin.MouseState.HOVER;
            this.MovementBtn3.Name = "MovementBtn3";
            this.MovementBtn3.Primary = false;
            this.MovementBtn3.UseVisualStyleBackColor = true;
            this.MovementBtn3.Click += new System.EventHandler(this.MovementBtn3_Click);
            // 
            // MovementBtn4
            // 
            resources.ApplyResources(this.MovementBtn4, "MovementBtn4");
            this.MovementBtn4.Depth = 0;
            this.MovementBtn4.Icon = null;
            this.MovementBtn4.MouseState = MaterialSkin.MouseState.HOVER;
            this.MovementBtn4.Name = "MovementBtn4";
            this.MovementBtn4.Primary = false;
            this.MovementBtn4.UseVisualStyleBackColor = true;
            this.MovementBtn4.Click += new System.EventHandler(this.MovementBtn4_Click);
            // 
            // materialLabel4
            // 
            resources.ApplyResources(this.materialLabel4, "materialLabel4");
            this.materialLabel4.Depth = 0;
            this.materialLabel4.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(222)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.materialLabel4.MouseState = MaterialSkin.MouseState.HOVER;
            this.materialLabel4.Name = "materialLabel4";
            // 
            // CalibrationSnapshotsButton
            // 
            resources.ApplyResources(this.CalibrationSnapshotsButton, "CalibrationSnapshotsButton");
            this.CalibrationSnapshotsButton.BackColor = System.Drawing.SystemColors.Control;
            this.CalibrationSnapshotsButton.Depth = 0;
            this.CalibrationSnapshotsButton.Icon = null;
            this.CalibrationSnapshotsButton.MouseState = MaterialSkin.MouseState.HOVER;
            this.CalibrationSnapshotsButton.Name = "CalibrationSnapshotsButton";
            this.CalibrationSnapshotsButton.Primary = false;
            this.CalibrationSnapshotsButton.UseVisualStyleBackColor = false;
            this.CalibrationSnapshotsButton.Click += new System.EventHandler(this.CalibrationSnapshotsButton_Click);
            // 
            // MainGameForm
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.CalibrationSnapshotsButton);
            this.Controls.Add(this.materialLabel4);
            this.Controls.Add(this.MovementBtn4);
            this.Controls.Add(this.MovementBtn3);
            this.Controls.Add(this.MovementBtn2);
            this.Controls.Add(this.MovementBtn1);
            this.Controls.Add(this.ValidationStateBtn);
            this.Controls.Add(this.SceneDisruptionBtn);
            this.Controls.Add(this.AdvancedSettingsBtn);
            this.Controls.Add(this.materialLabel7);
            this.Controls.Add(this.materialLabel6);
            this.Controls.Add(this.FPSLabel);
            this.Controls.Add(this.WhosPlayingLabel);
            this.Controls.Add(this.TrackingLogsListBox);
            this.Controls.Add(this.GameHistoryListBox);
            this.Controls.Add(this.StopTrackingBtn);
            this.Controls.Add(this.Recalibrate);
            this.Controls.Add(this.StartTrackingBtn);
            this.Controls.Add(this.EndGameBtn);
            this.Controls.Add(this.SaveGameBtn);
            this.Controls.Add(this.LoadGameBtn);
            this.Controls.Add(this.NewGameBtn);
            this.Controls.Add(this.materialLabel3);
            this.Controls.Add(this.materialLabel2);
            this.Controls.Add(this.materialLabel1);
            this.Controls.Add(this.pictureBox2);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.ColorCalibrationTrackBar);
            this.Controls.Add(this.VizualizationChoiceComboBox);
            this.Controls.Add(this.ImmediateBoardStatePictureBox);
            this.Controls.Add(this.TrackedBoardStatePictureBox);
            this.Controls.Add(this.GameStatePictureBox);
            this.Name = "MainGameForm";
            ((System.ComponentModel.ISupportInitialize)(this.GameStatePictureBox)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.TrackedBoardStatePictureBox)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ImmediateBoardStatePictureBox)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ColorCalibrationTrackBar)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox GameStatePictureBox;
        private System.Windows.Forms.PictureBox TrackedBoardStatePictureBox;
        private System.Windows.Forms.PictureBox ImmediateBoardStatePictureBox;
        private System.Windows.Forms.ComboBox VizualizationChoiceComboBox;
        private System.Windows.Forms.Timer ResultProcessingTimer;
        private System.Windows.Forms.TrackBar ColorCalibrationTrackBar;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.PictureBox pictureBox2;
        private MaterialSkin.Controls.MaterialLabel materialLabel1;
        private MaterialSkin.Controls.MaterialLabel materialLabel2;
        private MaterialSkin.Controls.MaterialLabel materialLabel3;
        private MaterialSkin.Controls.MaterialFlatButton NewGameBtn;
        private MaterialSkin.Controls.MaterialFlatButton LoadGameBtn;
        private MaterialSkin.Controls.MaterialFlatButton SaveGameBtn;
        private MaterialSkin.Controls.MaterialFlatButton EndGameBtn;
        private MaterialSkin.Controls.MaterialFlatButton StartTrackingBtn;
        private MaterialSkin.Controls.MaterialFlatButton Recalibrate;
        private MaterialSkin.Controls.MaterialFlatButton StopTrackingBtn;
        private MaterialSkin.Controls.MaterialListView GameHistoryListBox;
        private System.Windows.Forms.ColumnHeader Record;
        private MaterialSkin.Controls.MaterialListView TrackingLogsListBox;
        private System.Windows.Forms.ColumnHeader TrackingLogColumn;
        private MaterialSkin.Controls.MaterialLabel WhosPlayingLabel;
        private MaterialSkin.Controls.MaterialLabel FPSLabel;
        private MaterialSkin.Controls.MaterialLabel materialLabel6;
        private MaterialSkin.Controls.MaterialLabel materialLabel7;
        private MaterialSkin.Controls.MaterialFlatButton AdvancedSettingsBtn;
        private MaterialSkin.Controls.MaterialLabel SceneDisruptionBtn;
        private MaterialSkin.Controls.MaterialLabel ValidationStateBtn;
        private MaterialSkin.Controls.MaterialFlatButton MovementBtn1;
        private MaterialSkin.Controls.MaterialFlatButton MovementBtn2;
        private MaterialSkin.Controls.MaterialFlatButton MovementBtn3;
        private MaterialSkin.Controls.MaterialFlatButton MovementBtn4;
        private MaterialSkin.Controls.MaterialLabel materialLabel4;
        private MaterialSkin.Controls.MaterialFlatButton CalibrationSnapshotsButton;
    }
}