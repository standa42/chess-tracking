namespace ChessTracking.Forms
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
            ((System.ComponentModel.ISupportInitialize)(this.GameStatePictureBox)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.TrackedBoardStatePictureBox)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ImmediateBoardStatePictureBox)).BeginInit();
            this.SuspendLayout();
            // 
            // GameStatePictureBox
            // 
            this.GameStatePictureBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.GameStatePictureBox.Location = new System.Drawing.Point(13, 13);
            this.GameStatePictureBox.Name = "GameStatePictureBox";
            this.GameStatePictureBox.Size = new System.Drawing.Size(300, 300);
            this.GameStatePictureBox.TabIndex = 0;
            this.GameStatePictureBox.TabStop = false;
            // 
            // NewGameBtn
            // 
            this.NewGameBtn.Location = new System.Drawing.Point(538, 12);
            this.NewGameBtn.Name = "NewGameBtn";
            this.NewGameBtn.Size = new System.Drawing.Size(120, 45);
            this.NewGameBtn.TabIndex = 1;
            this.NewGameBtn.Text = "New game";
            this.NewGameBtn.UseVisualStyleBackColor = true;
            this.NewGameBtn.Click += new System.EventHandler(this.NewGameBtn_Click);
            // 
            // LoadGameBtn
            // 
            this.LoadGameBtn.Location = new System.Drawing.Point(538, 63);
            this.LoadGameBtn.Name = "LoadGameBtn";
            this.LoadGameBtn.Size = new System.Drawing.Size(120, 45);
            this.LoadGameBtn.TabIndex = 2;
            this.LoadGameBtn.Text = "Load game";
            this.LoadGameBtn.UseVisualStyleBackColor = true;
            this.LoadGameBtn.Click += new System.EventHandler(this.LoadGameBtn_Click);
            // 
            // SaveGameBtn
            // 
            this.SaveGameBtn.Location = new System.Drawing.Point(538, 114);
            this.SaveGameBtn.Name = "SaveGameBtn";
            this.SaveGameBtn.Size = new System.Drawing.Size(120, 45);
            this.SaveGameBtn.TabIndex = 3;
            this.SaveGameBtn.Text = "Save game";
            this.SaveGameBtn.UseVisualStyleBackColor = true;
            this.SaveGameBtn.Click += new System.EventHandler(this.SaveGameBtn_Click);
            // 
            // StartTrackingBtn
            // 
            this.StartTrackingBtn.Location = new System.Drawing.Point(538, 165);
            this.StartTrackingBtn.Name = "StartTrackingBtn";
            this.StartTrackingBtn.Size = new System.Drawing.Size(120, 45);
            this.StartTrackingBtn.TabIndex = 4;
            this.StartTrackingBtn.Text = "Start tracking";
            this.StartTrackingBtn.UseVisualStyleBackColor = true;
            this.StartTrackingBtn.Click += new System.EventHandler(this.StartTrackingBtn_Click);
            // 
            // Recalibrate
            // 
            this.Recalibrate.Location = new System.Drawing.Point(538, 216);
            this.Recalibrate.Name = "Recalibrate";
            this.Recalibrate.Size = new System.Drawing.Size(120, 45);
            this.Recalibrate.TabIndex = 5;
            this.Recalibrate.Text = "Recalibrate";
            this.Recalibrate.UseVisualStyleBackColor = true;
            this.Recalibrate.Click += new System.EventHandler(this.RecalibrateBtn_Click);
            // 
            // StopTrackingBtn
            // 
            this.StopTrackingBtn.Location = new System.Drawing.Point(538, 267);
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
            this.GameHistoryListBox.Location = new System.Drawing.Point(319, 13);
            this.GameHistoryListBox.Name = "GameHistoryListBox";
            this.GameHistoryListBox.Size = new System.Drawing.Size(212, 292);
            this.GameHistoryListBox.TabIndex = 7;
            // 
            // TrackedBoardStatePictureBox
            // 
            this.TrackedBoardStatePictureBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.TrackedBoardStatePictureBox.Location = new System.Drawing.Point(12, 319);
            this.TrackedBoardStatePictureBox.Name = "TrackedBoardStatePictureBox";
            this.TrackedBoardStatePictureBox.Size = new System.Drawing.Size(300, 300);
            this.TrackedBoardStatePictureBox.TabIndex = 9;
            this.TrackedBoardStatePictureBox.TabStop = false;
            // 
            // ImmediateBoardStatePictureBox
            // 
            this.ImmediateBoardStatePictureBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.ImmediateBoardStatePictureBox.Location = new System.Drawing.Point(319, 319);
            this.ImmediateBoardStatePictureBox.Name = "ImmediateBoardStatePictureBox";
            this.ImmediateBoardStatePictureBox.Size = new System.Drawing.Size(300, 300);
            this.ImmediateBoardStatePictureBox.TabIndex = 10;
            this.ImmediateBoardStatePictureBox.TabStop = false;
            // 
            // TrackingLogsListBox
            // 
            this.TrackingLogsListBox.FormattingEnabled = true;
            this.TrackingLogsListBox.ItemHeight = 16;
            this.TrackingLogsListBox.Location = new System.Drawing.Point(625, 319);
            this.TrackingLogsListBox.Name = "TrackingLogsListBox";
            this.TrackingLogsListBox.Size = new System.Drawing.Size(251, 292);
            this.TrackingLogsListBox.TabIndex = 11;
            // 
            // FPSLabel
            // 
            this.FPSLabel.AutoSize = true;
            this.FPSLabel.Location = new System.Drawing.Point(12, 673);
            this.FPSLabel.Name = "FPSLabel";
            this.FPSLabel.Size = new System.Drawing.Size(42, 17);
            this.FPSLabel.TabIndex = 12;
            this.FPSLabel.Text = "FPS: ";
            // 
            // HandDetectedBtn
            // 
            this.HandDetectedBtn.Enabled = false;
            this.HandDetectedBtn.Location = new System.Drawing.Point(12, 625);
            this.HandDetectedBtn.Name = "HandDetectedBtn";
            this.HandDetectedBtn.Size = new System.Drawing.Size(120, 45);
            this.HandDetectedBtn.TabIndex = 13;
            this.HandDetectedBtn.Text = "Hand detected";
            this.HandDetectedBtn.UseVisualStyleBackColor = true;
            // 
            // UserLogsListBox
            // 
            this.UserLogsListBox.FormattingEnabled = true;
            this.UserLogsListBox.ItemHeight = 16;
            this.UserLogsListBox.Location = new System.Drawing.Point(664, 12);
            this.UserLogsListBox.Name = "UserLogsListBox";
            this.UserLogsListBox.Size = new System.Drawing.Size(212, 292);
            this.UserLogsListBox.TabIndex = 14;
            // 
            // VizualizationChoiceComboBox
            // 
            this.VizualizationChoiceComboBox.FormattingEnabled = true;
            this.VizualizationChoiceComboBox.Location = new System.Drawing.Point(138, 625);
            this.VizualizationChoiceComboBox.Name = "VizualizationChoiceComboBox";
            this.VizualizationChoiceComboBox.Size = new System.Drawing.Size(158, 24);
            this.VizualizationChoiceComboBox.TabIndex = 15;
            // 
            // ResultProcessingTimer
            // 
            this.ResultProcessingTimer.Enabled = true;
            this.ResultProcessingTimer.Interval = 15;
            this.ResultProcessingTimer.Tick += new System.EventHandler(this.ResultProcessingTimer_Tick);
            // 
            // MainGameForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(893, 693);
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
    }
}