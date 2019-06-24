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
            this.GameStatePictureBox.Location = new System.Drawing.Point(706, 118);
            this.GameStatePictureBox.Name = "GameStatePictureBox";
            this.GameStatePictureBox.Size = new System.Drawing.Size(340, 340);
            this.GameStatePictureBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.GameStatePictureBox.TabIndex = 0;
            this.GameStatePictureBox.TabStop = false;
            // 
            // TrackedBoardStatePictureBox
            // 
            this.TrackedBoardStatePictureBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.TrackedBoardStatePictureBox.Location = new System.Drawing.Point(360, 118);
            this.TrackedBoardStatePictureBox.Name = "TrackedBoardStatePictureBox";
            this.TrackedBoardStatePictureBox.Size = new System.Drawing.Size(340, 340);
            this.TrackedBoardStatePictureBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.TrackedBoardStatePictureBox.TabIndex = 9;
            this.TrackedBoardStatePictureBox.TabStop = false;
            // 
            // ImmediateBoardStatePictureBox
            // 
            this.ImmediateBoardStatePictureBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.ImmediateBoardStatePictureBox.Location = new System.Drawing.Point(14, 118);
            this.ImmediateBoardStatePictureBox.Name = "ImmediateBoardStatePictureBox";
            this.ImmediateBoardStatePictureBox.Size = new System.Drawing.Size(340, 340);
            this.ImmediateBoardStatePictureBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.ImmediateBoardStatePictureBox.TabIndex = 10;
            this.ImmediateBoardStatePictureBox.TabStop = false;
            // 
            // VizualizationChoiceComboBox
            // 
            this.VizualizationChoiceComboBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.VizualizationChoiceComboBox.FormattingEnabled = true;
            this.VizualizationChoiceComboBox.Location = new System.Drawing.Point(844, 772);
            this.VizualizationChoiceComboBox.Name = "VizualizationChoiceComboBox";
            this.VizualizationChoiceComboBox.Size = new System.Drawing.Size(190, 28);
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
            this.ColorCalibrationTrackBar.Location = new System.Drawing.Point(857, 844);
            this.ColorCalibrationTrackBar.Maximum = 150;
            this.ColorCalibrationTrackBar.Minimum = -150;
            this.ColorCalibrationTrackBar.Name = "ColorCalibrationTrackBar";
            this.ColorCalibrationTrackBar.Size = new System.Drawing.Size(174, 37);
            this.ColorCalibrationTrackBar.TabIndex = 19;
            this.ColorCalibrationTrackBar.ValueChanged += new System.EventHandler(this.ColorCalibrationTrackBar_ValueChanged);
            // 
            // pictureBox1
            // 
            this.pictureBox1.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.pictureBox1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pictureBox1.Location = new System.Drawing.Point(844, 848);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(17, 17);
            this.pictureBox1.TabIndex = 29;
            this.pictureBox1.TabStop = false;
            // 
            // pictureBox2
            // 
            this.pictureBox2.BackColor = System.Drawing.SystemColors.ControlText;
            this.pictureBox2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pictureBox2.Location = new System.Drawing.Point(1028, 848);
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
            this.materialLabel2.Location = new System.Drawing.Point(356, 91);
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
            this.materialLabel3.Location = new System.Drawing.Point(702, 91);
            this.materialLabel3.MouseState = MaterialSkin.MouseState.HOVER;
            this.materialLabel3.Name = "materialLabel3";
            this.materialLabel3.Size = new System.Drawing.Size(107, 24);
            this.materialLabel3.TabIndex = 34;
            this.materialLabel3.Text = "Game state";
            // 
            // NewGameBtn
            // 
            this.NewGameBtn.AutoSize = true;
            this.NewGameBtn.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.NewGameBtn.BackColor = System.Drawing.SystemColors.Control;
            this.NewGameBtn.Depth = 0;
            this.NewGameBtn.Icon = null;
            this.NewGameBtn.Location = new System.Drawing.Point(13, 472);
            this.NewGameBtn.Margin = new System.Windows.Forms.Padding(4, 6, 4, 6);
            this.NewGameBtn.MaximumSize = new System.Drawing.Size(155, 40);
            this.NewGameBtn.MinimumSize = new System.Drawing.Size(155, 40);
            this.NewGameBtn.MouseState = MaterialSkin.MouseState.HOVER;
            this.NewGameBtn.Name = "NewGameBtn";
            this.NewGameBtn.Primary = false;
            this.NewGameBtn.Size = new System.Drawing.Size(155, 40);
            this.NewGameBtn.TabIndex = 35;
            this.NewGameBtn.Text = "New game";
            this.NewGameBtn.UseVisualStyleBackColor = false;
            this.NewGameBtn.Click += new System.EventHandler(this.NewGameBtn_Click);
            // 
            // LoadGameBtn
            // 
            this.LoadGameBtn.AutoSize = true;
            this.LoadGameBtn.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.LoadGameBtn.BackColor = System.Drawing.SystemColors.Control;
            this.LoadGameBtn.Depth = 0;
            this.LoadGameBtn.Icon = null;
            this.LoadGameBtn.Location = new System.Drawing.Point(14, 513);
            this.LoadGameBtn.Margin = new System.Windows.Forms.Padding(4, 6, 4, 6);
            this.LoadGameBtn.MaximumSize = new System.Drawing.Size(155, 40);
            this.LoadGameBtn.MinimumSize = new System.Drawing.Size(155, 40);
            this.LoadGameBtn.MouseState = MaterialSkin.MouseState.HOVER;
            this.LoadGameBtn.Name = "LoadGameBtn";
            this.LoadGameBtn.Primary = false;
            this.LoadGameBtn.Size = new System.Drawing.Size(155, 40);
            this.LoadGameBtn.TabIndex = 36;
            this.LoadGameBtn.Text = "Load game";
            this.LoadGameBtn.UseVisualStyleBackColor = false;
            this.LoadGameBtn.Click += new System.EventHandler(this.LoadGameBtn_Click);
            // 
            // SaveGameBtn
            // 
            this.SaveGameBtn.AutoSize = true;
            this.SaveGameBtn.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.SaveGameBtn.BackColor = System.Drawing.SystemColors.Control;
            this.SaveGameBtn.Depth = 0;
            this.SaveGameBtn.Icon = null;
            this.SaveGameBtn.Location = new System.Drawing.Point(14, 554);
            this.SaveGameBtn.Margin = new System.Windows.Forms.Padding(4, 6, 4, 6);
            this.SaveGameBtn.MaximumSize = new System.Drawing.Size(155, 40);
            this.SaveGameBtn.MinimumSize = new System.Drawing.Size(155, 40);
            this.SaveGameBtn.MouseState = MaterialSkin.MouseState.HOVER;
            this.SaveGameBtn.Name = "SaveGameBtn";
            this.SaveGameBtn.Primary = false;
            this.SaveGameBtn.Size = new System.Drawing.Size(155, 40);
            this.SaveGameBtn.TabIndex = 37;
            this.SaveGameBtn.Text = "Save game";
            this.SaveGameBtn.UseVisualStyleBackColor = false;
            this.SaveGameBtn.Click += new System.EventHandler(this.SaveGameBtn_Click);
            // 
            // EndGameBtn
            // 
            this.EndGameBtn.AutoSize = true;
            this.EndGameBtn.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.EndGameBtn.BackColor = System.Drawing.SystemColors.Control;
            this.EndGameBtn.Depth = 0;
            this.EndGameBtn.Icon = null;
            this.EndGameBtn.Location = new System.Drawing.Point(13, 595);
            this.EndGameBtn.Margin = new System.Windows.Forms.Padding(4, 6, 4, 6);
            this.EndGameBtn.MaximumSize = new System.Drawing.Size(155, 40);
            this.EndGameBtn.MinimumSize = new System.Drawing.Size(155, 40);
            this.EndGameBtn.MouseState = MaterialSkin.MouseState.HOVER;
            this.EndGameBtn.Name = "EndGameBtn";
            this.EndGameBtn.Primary = false;
            this.EndGameBtn.Size = new System.Drawing.Size(155, 40);
            this.EndGameBtn.TabIndex = 38;
            this.EndGameBtn.Text = "End game";
            this.EndGameBtn.UseVisualStyleBackColor = false;
            this.EndGameBtn.Click += new System.EventHandler(this.EndGameBtn_Click);
            // 
            // StartTrackingBtn
            // 
            this.StartTrackingBtn.AutoSize = true;
            this.StartTrackingBtn.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.StartTrackingBtn.BackColor = System.Drawing.SystemColors.Control;
            this.StartTrackingBtn.Depth = 0;
            this.StartTrackingBtn.Icon = null;
            this.StartTrackingBtn.Location = new System.Drawing.Point(13, 656);
            this.StartTrackingBtn.Margin = new System.Windows.Forms.Padding(4, 6, 4, 6);
            this.StartTrackingBtn.MaximumSize = new System.Drawing.Size(155, 40);
            this.StartTrackingBtn.MinimumSize = new System.Drawing.Size(155, 40);
            this.StartTrackingBtn.MouseState = MaterialSkin.MouseState.HOVER;
            this.StartTrackingBtn.Name = "StartTrackingBtn";
            this.StartTrackingBtn.Primary = false;
            this.StartTrackingBtn.Size = new System.Drawing.Size(155, 40);
            this.StartTrackingBtn.TabIndex = 39;
            this.StartTrackingBtn.Text = "Start tracking";
            this.StartTrackingBtn.UseVisualStyleBackColor = false;
            this.StartTrackingBtn.Click += new System.EventHandler(this.StartTrackingBtn_Click);
            // 
            // Recalibrate
            // 
            this.Recalibrate.AutoSize = true;
            this.Recalibrate.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.Recalibrate.BackColor = System.Drawing.SystemColors.Control;
            this.Recalibrate.Depth = 0;
            this.Recalibrate.Icon = null;
            this.Recalibrate.Location = new System.Drawing.Point(13, 697);
            this.Recalibrate.Margin = new System.Windows.Forms.Padding(4, 6, 4, 6);
            this.Recalibrate.MaximumSize = new System.Drawing.Size(155, 40);
            this.Recalibrate.MinimumSize = new System.Drawing.Size(155, 40);
            this.Recalibrate.MouseState = MaterialSkin.MouseState.HOVER;
            this.Recalibrate.Name = "Recalibrate";
            this.Recalibrate.Primary = false;
            this.Recalibrate.Size = new System.Drawing.Size(155, 40);
            this.Recalibrate.TabIndex = 40;
            this.Recalibrate.Text = "Recalibrate";
            this.Recalibrate.UseVisualStyleBackColor = false;
            this.Recalibrate.Click += new System.EventHandler(this.RecalibrateBtn_Click);
            // 
            // StopTrackingBtn
            // 
            this.StopTrackingBtn.AutoSize = true;
            this.StopTrackingBtn.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.StopTrackingBtn.BackColor = System.Drawing.SystemColors.Control;
            this.StopTrackingBtn.Depth = 0;
            this.StopTrackingBtn.Icon = null;
            this.StopTrackingBtn.Location = new System.Drawing.Point(13, 738);
            this.StopTrackingBtn.Margin = new System.Windows.Forms.Padding(4, 6, 4, 6);
            this.StopTrackingBtn.MaximumSize = new System.Drawing.Size(155, 40);
            this.StopTrackingBtn.MinimumSize = new System.Drawing.Size(155, 40);
            this.StopTrackingBtn.MouseState = MaterialSkin.MouseState.HOVER;
            this.StopTrackingBtn.Name = "StopTrackingBtn";
            this.StopTrackingBtn.Primary = false;
            this.StopTrackingBtn.Size = new System.Drawing.Size(155, 40);
            this.StopTrackingBtn.TabIndex = 41;
            this.StopTrackingBtn.Text = "Stop tracking";
            this.StopTrackingBtn.UseVisualStyleBackColor = false;
            this.StopTrackingBtn.Click += new System.EventHandler(this.StopTrackingBtn_Click);
            // 
            // GameHistoryListBox
            // 
            this.GameHistoryListBox.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.GameHistoryListBox.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.Record});
            this.GameHistoryListBox.Depth = 0;
            this.GameHistoryListBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 24F);
            this.GameHistoryListBox.FullRowSelect = true;
            this.GameHistoryListBox.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable;
            this.GameHistoryListBox.Location = new System.Drawing.Point(175, 472);
            this.GameHistoryListBox.MouseLocation = new System.Drawing.Point(-1, -1);
            this.GameHistoryListBox.MouseState = MaterialSkin.MouseState.OUT;
            this.GameHistoryListBox.Name = "GameHistoryListBox";
            this.GameHistoryListBox.OwnerDraw = true;
            this.GameHistoryListBox.Size = new System.Drawing.Size(168, 403);
            this.GameHistoryListBox.TabIndex = 45;
            this.GameHistoryListBox.UseCompatibleStateImageBehavior = false;
            this.GameHistoryListBox.View = System.Windows.Forms.View.Details;
            // 
            // Record
            // 
            this.Record.Text = "Record";
            this.Record.Width = 105;
            // 
            // TrackingLogsListBox
            // 
            this.TrackingLogsListBox.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.TrackingLogsListBox.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.TrackingLogColumn});
            this.TrackingLogsListBox.Depth = 0;
            this.TrackingLogsListBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 24F);
            this.TrackingLogsListBox.FullRowSelect = true;
            this.TrackingLogsListBox.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable;
            this.TrackingLogsListBox.Location = new System.Drawing.Point(349, 472);
            this.TrackingLogsListBox.MouseLocation = new System.Drawing.Point(-1, -1);
            this.TrackingLogsListBox.MouseState = MaterialSkin.MouseState.OUT;
            this.TrackingLogsListBox.Name = "TrackingLogsListBox";
            this.TrackingLogsListBox.OwnerDraw = true;
            this.TrackingLogsListBox.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.TrackingLogsListBox.Size = new System.Drawing.Size(490, 403);
            this.TrackingLogsListBox.TabIndex = 22;
            this.TrackingLogsListBox.UseCompatibleStateImageBehavior = false;
            this.TrackingLogsListBox.View = System.Windows.Forms.View.Details;
            // 
            // TrackingLogColumn
            // 
            this.TrackingLogColumn.Text = "Tracking log - 00:00:00";
            this.TrackingLogColumn.Width = 340;
            // 
            // WhosPlayingLabel
            // 
            this.WhosPlayingLabel.AutoSize = true;
            this.WhosPlayingLabel.Depth = 0;
            this.WhosPlayingLabel.Font = new System.Drawing.Font("Roboto", 11F);
            this.WhosPlayingLabel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(222)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.WhosPlayingLabel.Location = new System.Drawing.Point(845, 573);
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
            this.FPSLabel.Location = new System.Drawing.Point(845, 599);
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
            this.materialLabel6.Location = new System.Drawing.Point(844, 739);
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
            this.materialLabel7.Location = new System.Drawing.Point(844, 810);
            this.materialLabel7.MouseState = MaterialSkin.MouseState.HOVER;
            this.materialLabel7.Name = "materialLabel7";
            this.materialLabel7.Size = new System.Drawing.Size(205, 24);
            this.materialLabel7.TabIndex = 52;
            this.materialLabel7.Text = "Figure color calibration";
            // 
            // AdvancedSettingsBtn
            // 
            this.AdvancedSettingsBtn.AutoSize = true;
            this.AdvancedSettingsBtn.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.AdvancedSettingsBtn.BackColor = System.Drawing.SystemColors.Control;
            this.AdvancedSettingsBtn.Depth = 0;
            this.AdvancedSettingsBtn.Icon = null;
            this.AdvancedSettingsBtn.Location = new System.Drawing.Point(13, 835);
            this.AdvancedSettingsBtn.Margin = new System.Windows.Forms.Padding(4, 6, 4, 6);
            this.AdvancedSettingsBtn.MaximumSize = new System.Drawing.Size(155, 40);
            this.AdvancedSettingsBtn.MinimumSize = new System.Drawing.Size(155, 40);
            this.AdvancedSettingsBtn.MouseState = MaterialSkin.MouseState.HOVER;
            this.AdvancedSettingsBtn.Name = "AdvancedSettingsBtn";
            this.AdvancedSettingsBtn.Primary = false;
            this.AdvancedSettingsBtn.Size = new System.Drawing.Size(155, 40);
            this.AdvancedSettingsBtn.TabIndex = 53;
            this.AdvancedSettingsBtn.Text = "Advanced settings";
            this.AdvancedSettingsBtn.UseVisualStyleBackColor = false;
            this.AdvancedSettingsBtn.Click += new System.EventHandler(this.AdvancedSettingsBtn_Click);
            // 
            // SceneDisruptionBtn
            // 
            this.SceneDisruptionBtn.BackColor = System.Drawing.SystemColors.ControlLight;
            this.SceneDisruptionBtn.Depth = 0;
            this.SceneDisruptionBtn.Font = new System.Drawing.Font("Roboto", 11F);
            this.SceneDisruptionBtn.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(222)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.SceneDisruptionBtn.Location = new System.Drawing.Point(845, 521);
            this.SceneDisruptionBtn.MouseState = MaterialSkin.MouseState.HOVER;
            this.SceneDisruptionBtn.Name = "SceneDisruptionBtn";
            this.SceneDisruptionBtn.Size = new System.Drawing.Size(201, 46);
            this.SceneDisruptionBtn.TabIndex = 54;
            this.SceneDisruptionBtn.Text = "Scene Disruption";
            this.SceneDisruptionBtn.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // ValidationStateBtn
            // 
            this.ValidationStateBtn.BackColor = System.Drawing.SystemColors.ControlLight;
            this.ValidationStateBtn.Depth = 0;
            this.ValidationStateBtn.Font = new System.Drawing.Font("Roboto", 11F);
            this.ValidationStateBtn.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(222)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.ValidationStateBtn.Location = new System.Drawing.Point(845, 472);
            this.ValidationStateBtn.MouseState = MaterialSkin.MouseState.HOVER;
            this.ValidationStateBtn.Name = "ValidationStateBtn";
            this.ValidationStateBtn.Size = new System.Drawing.Size(201, 46);
            this.ValidationStateBtn.TabIndex = 55;
            this.ValidationStateBtn.Text = "Validation State";
            this.ValidationStateBtn.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // MovementBtn1
            // 
            this.MovementBtn1.AutoSize = true;
            this.MovementBtn1.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.MovementBtn1.Depth = 0;
            this.MovementBtn1.Icon = null;
            this.MovementBtn1.Location = new System.Drawing.Point(854, 693);
            this.MovementBtn1.Margin = new System.Windows.Forms.Padding(4, 6, 4, 6);
            this.MovementBtn1.MaximumSize = new System.Drawing.Size(40, 40);
            this.MovementBtn1.MinimumSize = new System.Drawing.Size(40, 40);
            this.MovementBtn1.MouseState = MaterialSkin.MouseState.HOVER;
            this.MovementBtn1.Name = "MovementBtn1";
            this.MovementBtn1.Primary = false;
            this.MovementBtn1.Size = new System.Drawing.Size(40, 40);
            this.MovementBtn1.TabIndex = 56;
            this.MovementBtn1.Text = "X";
            this.MovementBtn1.UseVisualStyleBackColor = true;
            this.MovementBtn1.Click += new System.EventHandler(this.MovementBtn1_Click);
            // 
            // MovementBtn2
            // 
            this.MovementBtn2.AutoSize = true;
            this.MovementBtn2.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.MovementBtn2.Depth = 0;
            this.MovementBtn2.Icon = null;
            this.MovementBtn2.Location = new System.Drawing.Point(902, 693);
            this.MovementBtn2.Margin = new System.Windows.Forms.Padding(4, 6, 4, 6);
            this.MovementBtn2.MaximumSize = new System.Drawing.Size(40, 40);
            this.MovementBtn2.MinimumSize = new System.Drawing.Size(40, 40);
            this.MovementBtn2.MouseState = MaterialSkin.MouseState.HOVER;
            this.MovementBtn2.Name = "MovementBtn2";
            this.MovementBtn2.Primary = false;
            this.MovementBtn2.Size = new System.Drawing.Size(40, 40);
            this.MovementBtn2.TabIndex = 57;
            this.MovementBtn2.Text = "X";
            this.MovementBtn2.UseVisualStyleBackColor = true;
            this.MovementBtn2.Click += new System.EventHandler(this.MovementBtn2_Click);
            // 
            // MovementBtn3
            // 
            this.MovementBtn3.AutoSize = true;
            this.MovementBtn3.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.MovementBtn3.Depth = 0;
            this.MovementBtn3.Icon = null;
            this.MovementBtn3.Location = new System.Drawing.Point(950, 693);
            this.MovementBtn3.Margin = new System.Windows.Forms.Padding(4, 6, 4, 6);
            this.MovementBtn3.MaximumSize = new System.Drawing.Size(40, 40);
            this.MovementBtn3.MinimumSize = new System.Drawing.Size(40, 40);
            this.MovementBtn3.MouseState = MaterialSkin.MouseState.HOVER;
            this.MovementBtn3.Name = "MovementBtn3";
            this.MovementBtn3.Primary = false;
            this.MovementBtn3.Size = new System.Drawing.Size(40, 40);
            this.MovementBtn3.TabIndex = 58;
            this.MovementBtn3.Text = "X";
            this.MovementBtn3.UseVisualStyleBackColor = true;
            this.MovementBtn3.Click += new System.EventHandler(this.MovementBtn3_Click);
            // 
            // MovementBtn4
            // 
            this.MovementBtn4.AutoSize = true;
            this.MovementBtn4.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.MovementBtn4.Depth = 0;
            this.MovementBtn4.Icon = null;
            this.MovementBtn4.Location = new System.Drawing.Point(998, 693);
            this.MovementBtn4.Margin = new System.Windows.Forms.Padding(4, 6, 4, 6);
            this.MovementBtn4.MaximumSize = new System.Drawing.Size(40, 40);
            this.MovementBtn4.MinimumSize = new System.Drawing.Size(40, 40);
            this.MovementBtn4.MouseState = MaterialSkin.MouseState.HOVER;
            this.MovementBtn4.Name = "MovementBtn4";
            this.MovementBtn4.Primary = false;
            this.MovementBtn4.Size = new System.Drawing.Size(40, 40);
            this.MovementBtn4.TabIndex = 59;
            this.MovementBtn4.Text = "X";
            this.MovementBtn4.UseVisualStyleBackColor = true;
            this.MovementBtn4.Click += new System.EventHandler(this.MovementBtn4_Click);
            // 
            // materialLabel4
            // 
            this.materialLabel4.AutoSize = true;
            this.materialLabel4.Depth = 0;
            this.materialLabel4.Font = new System.Drawing.Font("Roboto", 11F);
            this.materialLabel4.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(222)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.materialLabel4.Location = new System.Drawing.Point(840, 663);
            this.materialLabel4.MouseState = MaterialSkin.MouseState.HOVER;
            this.materialLabel4.Name = "materialLabel4";
            this.materialLabel4.Size = new System.Drawing.Size(206, 24);
            this.materialLabel4.TabIndex = 60;
            this.materialLabel4.Text = "Chessboard movement";
            // 
            // CalibrationSnapshotsButton
            // 
            this.CalibrationSnapshotsButton.AutoSize = true;
            this.CalibrationSnapshotsButton.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.CalibrationSnapshotsButton.BackColor = System.Drawing.SystemColors.Control;
            this.CalibrationSnapshotsButton.Depth = 0;
            this.CalibrationSnapshotsButton.Icon = null;
            this.CalibrationSnapshotsButton.Location = new System.Drawing.Point(13, 794);
            this.CalibrationSnapshotsButton.Margin = new System.Windows.Forms.Padding(4, 6, 4, 6);
            this.CalibrationSnapshotsButton.MaximumSize = new System.Drawing.Size(155, 40);
            this.CalibrationSnapshotsButton.MinimumSize = new System.Drawing.Size(155, 40);
            this.CalibrationSnapshotsButton.MouseState = MaterialSkin.MouseState.HOVER;
            this.CalibrationSnapshotsButton.Name = "CalibrationSnapshotsButton";
            this.CalibrationSnapshotsButton.Primary = false;
            this.CalibrationSnapshotsButton.Size = new System.Drawing.Size(155, 40);
            this.CalibrationSnapshotsButton.TabIndex = 61;
            this.CalibrationSnapshotsButton.Text = "Calibration snapshots";
            this.CalibrationSnapshotsButton.UseVisualStyleBackColor = false;
            this.CalibrationSnapshotsButton.Click += new System.EventHandler(this.CalibrationSnapshotsButton_Click);
            // 
            // MainGameForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1058, 950);
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
            this.MinimumSize = new System.Drawing.Size(1058, 950);
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