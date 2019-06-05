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
            this.ValidationStateBtn = new MaterialSkin.Controls.MaterialFlatButton();
            this.SceneDisruptionBtn = new MaterialSkin.Controls.MaterialFlatButton();
            this.WhosPlayingLabel = new MaterialSkin.Controls.MaterialLabel();
            this.FPSLabel = new MaterialSkin.Controls.MaterialLabel();
            this.materialLabel6 = new MaterialSkin.Controls.MaterialLabel();
            this.materialLabel7 = new MaterialSkin.Controls.MaterialLabel();
            this.AdvancedSettingsBtn = new MaterialSkin.Controls.MaterialFlatButton();
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
            this.GameStatePictureBox.Location = new System.Drawing.Point(626, 118);
            this.GameStatePictureBox.Name = "GameStatePictureBox";
            this.GameStatePictureBox.Size = new System.Drawing.Size(300, 300);
            this.GameStatePictureBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.GameStatePictureBox.TabIndex = 0;
            this.GameStatePictureBox.TabStop = false;
            // 
            // TrackedBoardStatePictureBox
            // 
            this.TrackedBoardStatePictureBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.TrackedBoardStatePictureBox.Location = new System.Drawing.Point(320, 118);
            this.TrackedBoardStatePictureBox.Name = "TrackedBoardStatePictureBox";
            this.TrackedBoardStatePictureBox.Size = new System.Drawing.Size(300, 300);
            this.TrackedBoardStatePictureBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.TrackedBoardStatePictureBox.TabIndex = 9;
            this.TrackedBoardStatePictureBox.TabStop = false;
            // 
            // ImmediateBoardStatePictureBox
            // 
            this.ImmediateBoardStatePictureBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.ImmediateBoardStatePictureBox.Location = new System.Drawing.Point(14, 118);
            this.ImmediateBoardStatePictureBox.Name = "ImmediateBoardStatePictureBox";
            this.ImmediateBoardStatePictureBox.Size = new System.Drawing.Size(300, 300);
            this.ImmediateBoardStatePictureBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.ImmediateBoardStatePictureBox.TabIndex = 10;
            this.ImmediateBoardStatePictureBox.TabStop = false;
            // 
            // VizualizationChoiceComboBox
            // 
            this.VizualizationChoiceComboBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.VizualizationChoiceComboBox.FormattingEnabled = true;
            this.VizualizationChoiceComboBox.Location = new System.Drawing.Point(739, 636);
            this.VizualizationChoiceComboBox.Name = "VizualizationChoiceComboBox";
            this.VizualizationChoiceComboBox.Size = new System.Drawing.Size(190, 30);
            this.VizualizationChoiceComboBox.TabIndex = 15;
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
            this.ColorCalibrationTrackBar.AutoSize = false;
            this.ColorCalibrationTrackBar.LargeChange = 1;
            this.ColorCalibrationTrackBar.Location = new System.Drawing.Point(752, 708);
            this.ColorCalibrationTrackBar.Maximum = 40;
            this.ColorCalibrationTrackBar.Minimum = -40;
            this.ColorCalibrationTrackBar.Name = "ColorCalibrationTrackBar";
            this.ColorCalibrationTrackBar.Size = new System.Drawing.Size(174, 37);
            this.ColorCalibrationTrackBar.TabIndex = 19;
            this.ColorCalibrationTrackBar.ValueChanged += new System.EventHandler(this.ColorCalibrationTrackBar_ValueChanged);
            // 
            // pictureBox1
            // 
            this.pictureBox1.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.pictureBox1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pictureBox1.Location = new System.Drawing.Point(739, 712);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(17, 17);
            this.pictureBox1.TabIndex = 29;
            this.pictureBox1.TabStop = false;
            // 
            // pictureBox2
            // 
            this.pictureBox2.BackColor = System.Drawing.SystemColors.ControlText;
            this.pictureBox2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pictureBox2.Location = new System.Drawing.Point(923, 712);
            this.pictureBox2.Name = "pictureBox2";
            this.pictureBox2.Size = new System.Drawing.Size(17, 17);
            this.pictureBox2.TabIndex = 30;
            this.pictureBox2.TabStop = false;
            // 
            // materialLabel1
            // 
            this.materialLabel1.AutoSize = true;
            this.materialLabel1.Depth = 0;
            this.materialLabel1.Font = new System.Drawing.Font("Roboto", 11F);
            this.materialLabel1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(222)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.materialLabel1.Location = new System.Drawing.Point(11, 91);
            this.materialLabel1.MouseState = MaterialSkin.MouseState.HOVER;
            this.materialLabel1.Name = "materialLabel1";
            this.materialLabel1.Size = new System.Drawing.Size(221, 24);
            this.materialLabel1.TabIndex = 32;
            this.materialLabel1.Text = "Immediate tracking state";
            // 
            // materialLabel2
            // 
            this.materialLabel2.AutoSize = true;
            this.materialLabel2.Depth = 0;
            this.materialLabel2.Font = new System.Drawing.Font("Roboto", 11F);
            this.materialLabel2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(222)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.materialLabel2.Location = new System.Drawing.Point(316, 91);
            this.materialLabel2.MouseState = MaterialSkin.MouseState.HOVER;
            this.materialLabel2.Name = "materialLabel2";
            this.materialLabel2.Size = new System.Drawing.Size(209, 24);
            this.materialLabel2.TabIndex = 33;
            this.materialLabel2.Text = "Averaged tracking state";
            // 
            // materialLabel3
            // 
            this.materialLabel3.AutoSize = true;
            this.materialLabel3.Depth = 0;
            this.materialLabel3.Font = new System.Drawing.Font("Roboto", 11F);
            this.materialLabel3.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(222)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.materialLabel3.Location = new System.Drawing.Point(622, 91);
            this.materialLabel3.MouseState = MaterialSkin.MouseState.HOVER;
            this.materialLabel3.Name = "materialLabel3";
            this.materialLabel3.Size = new System.Drawing.Size(107, 24);
            this.materialLabel3.TabIndex = 34;
            this.materialLabel3.Text = "Game state";
            // 
            // NewGameBtn
            // 
            this.NewGameBtn.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.NewGameBtn.Depth = 0;
            this.NewGameBtn.Icon = null;
            this.NewGameBtn.Location = new System.Drawing.Point(14, 455);
            this.NewGameBtn.Margin = new System.Windows.Forms.Padding(4, 6, 4, 6);
            this.NewGameBtn.MouseState = MaterialSkin.MouseState.HOVER;
            this.NewGameBtn.Name = "NewGameBtn";
            this.NewGameBtn.Primary = false;
            this.NewGameBtn.Size = new System.Drawing.Size(155, 40);
            this.NewGameBtn.TabIndex = 35;
            this.NewGameBtn.Text = "New game";
            this.NewGameBtn.UseVisualStyleBackColor = true;
            this.NewGameBtn.Click += new System.EventHandler(this.NewGameBtn_Click);
            // 
            // LoadGameBtn
            // 
            this.LoadGameBtn.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.LoadGameBtn.Depth = 0;
            this.LoadGameBtn.Icon = null;
            this.LoadGameBtn.Location = new System.Drawing.Point(15, 496);
            this.LoadGameBtn.Margin = new System.Windows.Forms.Padding(4, 6, 4, 6);
            this.LoadGameBtn.MouseState = MaterialSkin.MouseState.HOVER;
            this.LoadGameBtn.Name = "LoadGameBtn";
            this.LoadGameBtn.Primary = false;
            this.LoadGameBtn.Size = new System.Drawing.Size(155, 40);
            this.LoadGameBtn.TabIndex = 36;
            this.LoadGameBtn.Text = "Load game";
            this.LoadGameBtn.UseVisualStyleBackColor = true;
            this.LoadGameBtn.Click += new System.EventHandler(this.LoadGameBtn_Click);
            // 
            // SaveGameBtn
            // 
            this.SaveGameBtn.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.SaveGameBtn.Depth = 0;
            this.SaveGameBtn.Icon = null;
            this.SaveGameBtn.Location = new System.Drawing.Point(15, 537);
            this.SaveGameBtn.Margin = new System.Windows.Forms.Padding(4, 6, 4, 6);
            this.SaveGameBtn.MouseState = MaterialSkin.MouseState.HOVER;
            this.SaveGameBtn.Name = "SaveGameBtn";
            this.SaveGameBtn.Primary = false;
            this.SaveGameBtn.Size = new System.Drawing.Size(155, 40);
            this.SaveGameBtn.TabIndex = 37;
            this.SaveGameBtn.Text = "Save game";
            this.SaveGameBtn.UseVisualStyleBackColor = true;
            this.SaveGameBtn.Click += new System.EventHandler(this.SaveGameBtn_Click);
            // 
            // EndGameBtn
            // 
            this.EndGameBtn.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.EndGameBtn.Depth = 0;
            this.EndGameBtn.Icon = null;
            this.EndGameBtn.Location = new System.Drawing.Point(14, 578);
            this.EndGameBtn.Margin = new System.Windows.Forms.Padding(4, 6, 4, 6);
            this.EndGameBtn.MouseState = MaterialSkin.MouseState.HOVER;
            this.EndGameBtn.Name = "EndGameBtn";
            this.EndGameBtn.Primary = false;
            this.EndGameBtn.Size = new System.Drawing.Size(155, 40);
            this.EndGameBtn.TabIndex = 38;
            this.EndGameBtn.Text = "End game";
            this.EndGameBtn.UseVisualStyleBackColor = true;
            this.EndGameBtn.Click += new System.EventHandler(this.EndGameBtn_Click);
            // 
            // StartTrackingBtn
            // 
            this.StartTrackingBtn.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.StartTrackingBtn.Depth = 0;
            this.StartTrackingBtn.Icon = null;
            this.StartTrackingBtn.Location = new System.Drawing.Point(15, 626);
            this.StartTrackingBtn.Margin = new System.Windows.Forms.Padding(4, 6, 4, 6);
            this.StartTrackingBtn.MouseState = MaterialSkin.MouseState.HOVER;
            this.StartTrackingBtn.Name = "StartTrackingBtn";
            this.StartTrackingBtn.Primary = false;
            this.StartTrackingBtn.Size = new System.Drawing.Size(155, 40);
            this.StartTrackingBtn.TabIndex = 39;
            this.StartTrackingBtn.Text = "Start tracking";
            this.StartTrackingBtn.UseVisualStyleBackColor = true;
            this.StartTrackingBtn.Click += new System.EventHandler(this.StartTrackingBtn_Click);
            // 
            // Recalibrate
            // 
            this.Recalibrate.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.Recalibrate.Depth = 0;
            this.Recalibrate.Icon = null;
            this.Recalibrate.Location = new System.Drawing.Point(15, 667);
            this.Recalibrate.Margin = new System.Windows.Forms.Padding(4, 6, 4, 6);
            this.Recalibrate.MouseState = MaterialSkin.MouseState.HOVER;
            this.Recalibrate.Name = "Recalibrate";
            this.Recalibrate.Primary = false;
            this.Recalibrate.Size = new System.Drawing.Size(155, 40);
            this.Recalibrate.TabIndex = 40;
            this.Recalibrate.Text = "Recalibrate";
            this.Recalibrate.UseVisualStyleBackColor = true;
            this.Recalibrate.Click += new System.EventHandler(this.RecalibrateBtn_Click);
            // 
            // StopTrackingBtn
            // 
            this.StopTrackingBtn.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.StopTrackingBtn.Depth = 0;
            this.StopTrackingBtn.Icon = null;
            this.StopTrackingBtn.Location = new System.Drawing.Point(15, 708);
            this.StopTrackingBtn.Margin = new System.Windows.Forms.Padding(4, 6, 4, 6);
            this.StopTrackingBtn.MouseState = MaterialSkin.MouseState.HOVER;
            this.StopTrackingBtn.Name = "StopTrackingBtn";
            this.StopTrackingBtn.Primary = false;
            this.StopTrackingBtn.Size = new System.Drawing.Size(155, 40);
            this.StopTrackingBtn.TabIndex = 41;
            this.StopTrackingBtn.Text = "Stop tracking";
            this.StopTrackingBtn.UseVisualStyleBackColor = true;
            this.StopTrackingBtn.Click += new System.EventHandler(this.StopTrackingBtn_Click);
            // 
            // GameHistoryListBox
            // 
            this.GameHistoryListBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.GameHistoryListBox.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.Record});
            this.GameHistoryListBox.Depth = 0;
            this.GameHistoryListBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 24F);
            this.GameHistoryListBox.FullRowSelect = true;
            this.GameHistoryListBox.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable;
            this.GameHistoryListBox.Location = new System.Drawing.Point(176, 439);
            this.GameHistoryListBox.MouseLocation = new System.Drawing.Point(-1, -1);
            this.GameHistoryListBox.MouseState = MaterialSkin.MouseState.OUT;
            this.GameHistoryListBox.Name = "GameHistoryListBox";
            this.GameHistoryListBox.OwnerDraw = true;
            this.GameHistoryListBox.Size = new System.Drawing.Size(138, 395);
            this.GameHistoryListBox.TabIndex = 45;
            this.GameHistoryListBox.UseCompatibleStateImageBehavior = false;
            this.GameHistoryListBox.View = System.Windows.Forms.View.Details;
            // 
            // Record
            // 
            this.Record.Text = "Record";
            this.Record.Width = 125;
            // 
            // TrackingLogsListBox
            // 
            this.TrackingLogsListBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.TrackingLogsListBox.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.TrackingLogColumn});
            this.TrackingLogsListBox.Depth = 0;
            this.TrackingLogsListBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 24F);
            this.TrackingLogsListBox.FullRowSelect = true;
            this.TrackingLogsListBox.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable;
            this.TrackingLogsListBox.Location = new System.Drawing.Point(320, 439);
            this.TrackingLogsListBox.MouseLocation = new System.Drawing.Point(-1, -1);
            this.TrackingLogsListBox.MouseState = MaterialSkin.MouseState.OUT;
            this.TrackingLogsListBox.Name = "TrackingLogsListBox";
            this.TrackingLogsListBox.OwnerDraw = true;
            this.TrackingLogsListBox.Size = new System.Drawing.Size(409, 395);
            this.TrackingLogsListBox.TabIndex = 46;
            this.TrackingLogsListBox.UseCompatibleStateImageBehavior = false;
            this.TrackingLogsListBox.View = System.Windows.Forms.View.Details;
            // 
            // TrackingLogColumn
            // 
            this.TrackingLogColumn.Text = "Tracking log - 00:00:00";
            this.TrackingLogColumn.Width = 500;
            // 
            // ValidationStateBtn
            // 
            this.ValidationStateBtn.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.ValidationStateBtn.Depth = 0;
            this.ValidationStateBtn.Icon = null;
            this.ValidationStateBtn.Location = new System.Drawing.Point(739, 439);
            this.ValidationStateBtn.Margin = new System.Windows.Forms.Padding(4, 6, 4, 6);
            this.ValidationStateBtn.MouseState = MaterialSkin.MouseState.HOVER;
            this.ValidationStateBtn.Name = "ValidationStateBtn";
            this.ValidationStateBtn.Primary = false;
            this.ValidationStateBtn.Size = new System.Drawing.Size(190, 46);
            this.ValidationStateBtn.TabIndex = 47;
            this.ValidationStateBtn.Text = "Validation State";
            this.ValidationStateBtn.UseVisualStyleBackColor = true;
            // 
            // SceneDisruptionBtn
            // 
            this.SceneDisruptionBtn.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.SceneDisruptionBtn.Depth = 0;
            this.SceneDisruptionBtn.Icon = null;
            this.SceneDisruptionBtn.Location = new System.Drawing.Point(739, 487);
            this.SceneDisruptionBtn.Margin = new System.Windows.Forms.Padding(4, 6, 4, 6);
            this.SceneDisruptionBtn.MouseState = MaterialSkin.MouseState.HOVER;
            this.SceneDisruptionBtn.Name = "SceneDisruptionBtn";
            this.SceneDisruptionBtn.Primary = false;
            this.SceneDisruptionBtn.Size = new System.Drawing.Size(190, 49);
            this.SceneDisruptionBtn.TabIndex = 48;
            this.SceneDisruptionBtn.Text = "Scene Disruption";
            this.SceneDisruptionBtn.UseVisualStyleBackColor = true;
            // 
            // WhosPlayingLabel
            // 
            this.WhosPlayingLabel.AutoSize = true;
            this.WhosPlayingLabel.Depth = 0;
            this.WhosPlayingLabel.Font = new System.Drawing.Font("Roboto", 11F);
            this.WhosPlayingLabel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(222)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.WhosPlayingLabel.Location = new System.Drawing.Point(739, 542);
            this.WhosPlayingLabel.MouseState = MaterialSkin.MouseState.HOVER;
            this.WhosPlayingLabel.Name = "WhosPlayingLabel";
            this.WhosPlayingLabel.Size = new System.Drawing.Size(50, 24);
            this.WhosPlayingLabel.TabIndex = 49;
            this.WhosPlayingLabel.Text = "        ";
            // 
            // FPSLabel
            // 
            this.FPSLabel.AutoSize = true;
            this.FPSLabel.Depth = 0;
            this.FPSLabel.Font = new System.Drawing.Font("Roboto", 11F);
            this.FPSLabel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(222)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.FPSLabel.Location = new System.Drawing.Point(739, 570);
            this.FPSLabel.MouseState = MaterialSkin.MouseState.HOVER;
            this.FPSLabel.Name = "FPSLabel";
            this.FPSLabel.Size = new System.Drawing.Size(54, 24);
            this.FPSLabel.TabIndex = 50;
            this.FPSLabel.Text = "FPS: ";
            // 
            // materialLabel6
            // 
            this.materialLabel6.AutoSize = true;
            this.materialLabel6.Depth = 0;
            this.materialLabel6.Font = new System.Drawing.Font("Roboto", 11F);
            this.materialLabel6.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(222)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.materialLabel6.Location = new System.Drawing.Point(739, 603);
            this.materialLabel6.MouseState = MaterialSkin.MouseState.HOVER;
            this.materialLabel6.Name = "materialLabel6";
            this.materialLabel6.Size = new System.Drawing.Size(180, 24);
            this.materialLabel6.TabIndex = 51;
            this.materialLabel6.Text = "Visualisation choice";
            // 
            // materialLabel7
            // 
            this.materialLabel7.AutoSize = true;
            this.materialLabel7.Depth = 0;
            this.materialLabel7.Font = new System.Drawing.Font("Roboto", 11F);
            this.materialLabel7.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(222)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.materialLabel7.Location = new System.Drawing.Point(739, 674);
            this.materialLabel7.MouseState = MaterialSkin.MouseState.HOVER;
            this.materialLabel7.Name = "materialLabel7";
            this.materialLabel7.Size = new System.Drawing.Size(205, 24);
            this.materialLabel7.TabIndex = 52;
            this.materialLabel7.Text = "Figure color calibration";
            // 
            // AdvancedSettingsBtn
            // 
            this.AdvancedSettingsBtn.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.AdvancedSettingsBtn.Depth = 0;
            this.AdvancedSettingsBtn.Icon = null;
            this.AdvancedSettingsBtn.Location = new System.Drawing.Point(739, 754);
            this.AdvancedSettingsBtn.Margin = new System.Windows.Forms.Padding(4, 6, 4, 6);
            this.AdvancedSettingsBtn.MouseState = MaterialSkin.MouseState.HOVER;
            this.AdvancedSettingsBtn.Name = "AdvancedSettingsBtn";
            this.AdvancedSettingsBtn.Primary = false;
            this.AdvancedSettingsBtn.Size = new System.Drawing.Size(190, 40);
            this.AdvancedSettingsBtn.TabIndex = 53;
            this.AdvancedSettingsBtn.Text = "Advanced settings";
            this.AdvancedSettingsBtn.UseVisualStyleBackColor = true;
            this.AdvancedSettingsBtn.Click += new System.EventHandler(this.AdvancedSettingsBtn_Click);
            // 
            // MainGameForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1106, 888);
            this.Controls.Add(this.AdvancedSettingsBtn);
            this.Controls.Add(this.materialLabel7);
            this.Controls.Add(this.materialLabel6);
            this.Controls.Add(this.FPSLabel);
            this.Controls.Add(this.WhosPlayingLabel);
            this.Controls.Add(this.SceneDisruptionBtn);
            this.Controls.Add(this.ValidationStateBtn);
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
            this.Text = "Chess tracking";
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
        private MaterialSkin.Controls.MaterialFlatButton ValidationStateBtn;
        private MaterialSkin.Controls.MaterialFlatButton SceneDisruptionBtn;
        private MaterialSkin.Controls.MaterialLabel WhosPlayingLabel;
        private MaterialSkin.Controls.MaterialLabel FPSLabel;
        private MaterialSkin.Controls.MaterialLabel materialLabel6;
        private MaterialSkin.Controls.MaterialLabel materialLabel7;
        private MaterialSkin.Controls.MaterialFlatButton AdvancedSettingsBtn;
    }
}