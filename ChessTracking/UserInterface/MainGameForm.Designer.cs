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
            this.NewGameBtn = new System.Windows.Forms.Button();
            this.LoadGameBtn = new System.Windows.Forms.Button();
            this.SaveGameBtn = new System.Windows.Forms.Button();
            this.StartTrackingBtn = new System.Windows.Forms.Button();
            this.Recalibrate = new System.Windows.Forms.Button();
            this.StopTrackingBtn = new System.Windows.Forms.Button();
            this.GameHistoryListBox = new System.Windows.Forms.ListBox();
            this.TrackedBoardStatePictureBox = new System.Windows.Forms.PictureBox();
            this.ImmediateBoardStatePictureBox = new System.Windows.Forms.PictureBox();
            this.TrackingLogsListBox = new System.Windows.Forms.ListBox();
            this.FPSLabel = new System.Windows.Forms.Label();
            this.HandDetectedBtn = new System.Windows.Forms.Button();
            this.UserLogsListBox = new System.Windows.Forms.ListBox();
            this.VizualizationChoiceComboBox = new System.Windows.Forms.ComboBox();
            this.ResultProcessingTimer = new System.Windows.Forms.Timer(this.components);
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.ColorCalibrationTrackBar = new System.Windows.Forms.TrackBar();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.GameStatePictureBox)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.TrackedBoardStatePictureBox)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ImmediateBoardStatePictureBox)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ColorCalibrationTrackBar)).BeginInit();
            this.SuspendLayout();
            // 
            // GameStatePictureBox
            // 
            this.GameStatePictureBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.GameStatePictureBox.Location = new System.Drawing.Point(624, 30);
            this.GameStatePictureBox.Name = "GameStatePictureBox";
            this.GameStatePictureBox.Size = new System.Drawing.Size(300, 300);
            this.GameStatePictureBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.GameStatePictureBox.TabIndex = 0;
            this.GameStatePictureBox.TabStop = false;
            // 
            // NewGameBtn
            // 
            this.NewGameBtn.Location = new System.Drawing.Point(15, 336);
            this.NewGameBtn.Name = "NewGameBtn";
            this.NewGameBtn.Size = new System.Drawing.Size(120, 45);
            this.NewGameBtn.TabIndex = 1;
            this.NewGameBtn.Text = "New game";
            this.NewGameBtn.UseVisualStyleBackColor = true;
            this.NewGameBtn.Click += new System.EventHandler(this.NewGameBtn_Click);
            // 
            // LoadGameBtn
            // 
            this.LoadGameBtn.Location = new System.Drawing.Point(15, 387);
            this.LoadGameBtn.Name = "LoadGameBtn";
            this.LoadGameBtn.Size = new System.Drawing.Size(120, 45);
            this.LoadGameBtn.TabIndex = 2;
            this.LoadGameBtn.Text = "Load game";
            this.LoadGameBtn.UseVisualStyleBackColor = true;
            this.LoadGameBtn.Click += new System.EventHandler(this.LoadGameBtn_Click);
            // 
            // SaveGameBtn
            // 
            this.SaveGameBtn.Location = new System.Drawing.Point(15, 438);
            this.SaveGameBtn.Name = "SaveGameBtn";
            this.SaveGameBtn.Size = new System.Drawing.Size(120, 45);
            this.SaveGameBtn.TabIndex = 3;
            this.SaveGameBtn.Text = "Save game";
            this.SaveGameBtn.UseVisualStyleBackColor = true;
            this.SaveGameBtn.Click += new System.EventHandler(this.SaveGameBtn_Click);
            // 
            // StartTrackingBtn
            // 
            this.StartTrackingBtn.Location = new System.Drawing.Point(12, 533);
            this.StartTrackingBtn.Name = "StartTrackingBtn";
            this.StartTrackingBtn.Size = new System.Drawing.Size(120, 45);
            this.StartTrackingBtn.TabIndex = 4;
            this.StartTrackingBtn.Text = "Start tracking";
            this.StartTrackingBtn.UseVisualStyleBackColor = true;
            this.StartTrackingBtn.Click += new System.EventHandler(this.StartTrackingBtn_Click);
            // 
            // Recalibrate
            // 
            this.Recalibrate.Location = new System.Drawing.Point(12, 584);
            this.Recalibrate.Name = "Recalibrate";
            this.Recalibrate.Size = new System.Drawing.Size(120, 45);
            this.Recalibrate.TabIndex = 5;
            this.Recalibrate.Text = "Recalibrate";
            this.Recalibrate.UseVisualStyleBackColor = true;
            this.Recalibrate.Click += new System.EventHandler(this.RecalibrateBtn_Click);
            // 
            // StopTrackingBtn
            // 
            this.StopTrackingBtn.Location = new System.Drawing.Point(12, 635);
            this.StopTrackingBtn.Name = "StopTrackingBtn";
            this.StopTrackingBtn.Size = new System.Drawing.Size(120, 45);
            this.StopTrackingBtn.TabIndex = 6;
            this.StopTrackingBtn.Text = "Stop tracking";
            this.StopTrackingBtn.UseVisualStyleBackColor = true;
            this.StopTrackingBtn.Click += new System.EventHandler(this.StopTrackingBtn_Click);
            // 
            // GameHistoryListBox
            // 
            this.GameHistoryListBox.FormattingEnabled = true;
            this.GameHistoryListBox.ItemHeight = 16;
            this.GameHistoryListBox.Location = new System.Drawing.Point(141, 357);
            this.GameHistoryListBox.Name = "GameHistoryListBox";
            this.GameHistoryListBox.Size = new System.Drawing.Size(131, 324);
            this.GameHistoryListBox.TabIndex = 7;
            // 
            // TrackedBoardStatePictureBox
            // 
            this.TrackedBoardStatePictureBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.TrackedBoardStatePictureBox.Location = new System.Drawing.Point(318, 30);
            this.TrackedBoardStatePictureBox.Name = "TrackedBoardStatePictureBox";
            this.TrackedBoardStatePictureBox.Size = new System.Drawing.Size(300, 300);
            this.TrackedBoardStatePictureBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.TrackedBoardStatePictureBox.TabIndex = 9;
            this.TrackedBoardStatePictureBox.TabStop = false;
            // 
            // ImmediateBoardStatePictureBox
            // 
            this.ImmediateBoardStatePictureBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.ImmediateBoardStatePictureBox.Location = new System.Drawing.Point(12, 30);
            this.ImmediateBoardStatePictureBox.Name = "ImmediateBoardStatePictureBox";
            this.ImmediateBoardStatePictureBox.Size = new System.Drawing.Size(300, 300);
            this.ImmediateBoardStatePictureBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.ImmediateBoardStatePictureBox.TabIndex = 10;
            this.ImmediateBoardStatePictureBox.TabStop = false;
            // 
            // TrackingLogsListBox
            // 
            this.TrackingLogsListBox.FormattingEnabled = true;
            this.TrackingLogsListBox.ItemHeight = 16;
            this.TrackingLogsListBox.Location = new System.Drawing.Point(496, 357);
            this.TrackingLogsListBox.Name = "TrackingLogsListBox";
            this.TrackingLogsListBox.Size = new System.Drawing.Size(214, 324);
            this.TrackingLogsListBox.TabIndex = 11;
            // 
            // FPSLabel
            // 
            this.FPSLabel.AutoSize = true;
            this.FPSLabel.Location = new System.Drawing.Point(716, 387);
            this.FPSLabel.Name = "FPSLabel";
            this.FPSLabel.Size = new System.Drawing.Size(42, 17);
            this.FPSLabel.TabIndex = 12;
            this.FPSLabel.Text = "FPS: ";
            // 
            // HandDetectedBtn
            // 
            this.HandDetectedBtn.Enabled = false;
            this.HandDetectedBtn.Location = new System.Drawing.Point(716, 336);
            this.HandDetectedBtn.Name = "HandDetectedBtn";
            this.HandDetectedBtn.Size = new System.Drawing.Size(208, 45);
            this.HandDetectedBtn.TabIndex = 13;
            this.HandDetectedBtn.UseVisualStyleBackColor = true;
            // 
            // UserLogsListBox
            // 
            this.UserLogsListBox.FormattingEnabled = true;
            this.UserLogsListBox.ItemHeight = 16;
            this.UserLogsListBox.Location = new System.Drawing.Point(278, 357);
            this.UserLogsListBox.Name = "UserLogsListBox";
            this.UserLogsListBox.Size = new System.Drawing.Size(212, 324);
            this.UserLogsListBox.TabIndex = 14;
            // 
            // VizualizationChoiceComboBox
            // 
            this.VizualizationChoiceComboBox.FormattingEnabled = true;
            this.VizualizationChoiceComboBox.Location = new System.Drawing.Point(716, 599);
            this.VizualizationChoiceComboBox.Name = "VizualizationChoiceComboBox";
            this.VizualizationChoiceComboBox.Size = new System.Drawing.Size(134, 24);
            this.VizualizationChoiceComboBox.TabIndex = 15;
            this.VizualizationChoiceComboBox.SelectedIndexChanged += new System.EventHandler(this.VizualizationChoiceComboBox_SelectedIndexChanged);
            // 
            // ResultProcessingTimer
            // 
            this.ResultProcessingTimer.Enabled = true;
            this.ResultProcessingTimer.Interval = 30;
            this.ResultProcessingTimer.Tick += new System.EventHandler(this.ResultProcessingTimer_Tick);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(9, 10);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(161, 17);
            this.label1.TabIndex = 16;
            this.label1.Text = "Immediate tracking state";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(315, 10);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(158, 17);
            this.label2.TabIndex = 17;
            this.label2.Text = "Averaged tracking state";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(621, 10);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(81, 17);
            this.label3.TabIndex = 18;
            this.label3.Text = "Game state";
            // 
            // ColorCalibrationTrackBar
            // 
            this.ColorCalibrationTrackBar.AutoSize = false;
            this.ColorCalibrationTrackBar.LargeChange = 1;
            this.ColorCalibrationTrackBar.Location = new System.Drawing.Point(716, 644);
            this.ColorCalibrationTrackBar.Maximum = 40;
            this.ColorCalibrationTrackBar.Minimum = -40;
            this.ColorCalibrationTrackBar.Name = "ColorCalibrationTrackBar";
            this.ColorCalibrationTrackBar.Size = new System.Drawing.Size(205, 37);
            this.ColorCalibrationTrackBar.TabIndex = 19;
            this.ColorCalibrationTrackBar.ValueChanged += new System.EventHandler(this.ColorCalibrationTrackBar_ValueChanged);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(713, 579);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(132, 17);
            this.label4.TabIndex = 20;
            this.label4.Text = "Visualisation choice";
            // 
            // label5
            // 
            this.label5.Location = new System.Drawing.Point(713, 626);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(154, 19);
            this.label5.TabIndex = 21;
            this.label5.Text = "Figure color calibration";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(138, 337);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(91, 17);
            this.label6.TabIndex = 22;
            this.label6.Text = "Game record";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(275, 337);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(69, 17);
            this.label7.TabIndex = 23;
            this.label7.Text = "Game log";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(493, 337);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(86, 17);
            this.label8.TabIndex = 24;
            this.label8.Text = "Tracking log";
            // 
            // MainGameForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(929, 685);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.ColorCalibrationTrackBar);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.VizualizationChoiceComboBox);
            this.Controls.Add(this.UserLogsListBox);
            this.Controls.Add(this.HandDetectedBtn);
            this.Controls.Add(this.FPSLabel);
            this.Controls.Add(this.TrackingLogsListBox);
            this.Controls.Add(this.ImmediateBoardStatePictureBox);
            this.Controls.Add(this.TrackedBoardStatePictureBox);
            this.Controls.Add(this.GameHistoryListBox);
            this.Controls.Add(this.StopTrackingBtn);
            this.Controls.Add(this.Recalibrate);
            this.Controls.Add(this.StartTrackingBtn);
            this.Controls.Add(this.SaveGameBtn);
            this.Controls.Add(this.LoadGameBtn);
            this.Controls.Add(this.NewGameBtn);
            this.Controls.Add(this.GameStatePictureBox);
            this.Name = "MainGameForm";
            this.Text = "Chess tracking";
            ((System.ComponentModel.ISupportInitialize)(this.GameStatePictureBox)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.TrackedBoardStatePictureBox)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ImmediateBoardStatePictureBox)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ColorCalibrationTrackBar)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox GameStatePictureBox;
        private System.Windows.Forms.Button NewGameBtn;
        private System.Windows.Forms.Button LoadGameBtn;
        private System.Windows.Forms.Button SaveGameBtn;
        private System.Windows.Forms.Button StartTrackingBtn;
        private System.Windows.Forms.Button Recalibrate;
        private System.Windows.Forms.Button StopTrackingBtn;
        private System.Windows.Forms.ListBox GameHistoryListBox;
        private System.Windows.Forms.PictureBox TrackedBoardStatePictureBox;
        private System.Windows.Forms.PictureBox ImmediateBoardStatePictureBox;
        private System.Windows.Forms.ListBox TrackingLogsListBox;
        private System.Windows.Forms.Label FPSLabel;
        private System.Windows.Forms.Button HandDetectedBtn;
        private System.Windows.Forms.ListBox UserLogsListBox;
        private System.Windows.Forms.ComboBox VizualizationChoiceComboBox;
        private System.Windows.Forms.Timer ResultProcessingTimer;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TrackBar ColorCalibrationTrackBar;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label8;
    }
}