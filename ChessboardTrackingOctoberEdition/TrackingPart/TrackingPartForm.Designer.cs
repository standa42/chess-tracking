namespace ChessboardTrackingOctoberEdition.TrackingPart
{
    partial class TrackingPartForm
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
            this.GameStatePictureBox = new System.Windows.Forms.PictureBox();
            this.TrackingLogListBox = new System.Windows.Forms.ListBox();
            this.FrameCountLabel = new System.Windows.Forms.Label();
            this.FpsLabel = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.GameStatePictureBox)).BeginInit();
            this.SuspendLayout();
            // 
            // GameStatePictureBox
            // 
            this.GameStatePictureBox.Location = new System.Drawing.Point(13, 13);
            this.GameStatePictureBox.Name = "GameStatePictureBox";
            this.GameStatePictureBox.Size = new System.Drawing.Size(367, 348);
            this.GameStatePictureBox.TabIndex = 0;
            this.GameStatePictureBox.TabStop = false;
            // 
            // TrackingLogListBox
            // 
            this.TrackingLogListBox.FormattingEnabled = true;
            this.TrackingLogListBox.ItemHeight = 16;
            this.TrackingLogListBox.Location = new System.Drawing.Point(387, 13);
            this.TrackingLogListBox.Name = "TrackingLogListBox";
            this.TrackingLogListBox.Size = new System.Drawing.Size(200, 340);
            this.TrackingLogListBox.TabIndex = 1;
            // 
            // FrameCountLabel
            // 
            this.FrameCountLabel.AutoSize = true;
            this.FrameCountLabel.Location = new System.Drawing.Point(13, 368);
            this.FrameCountLabel.Name = "FrameCountLabel";
            this.FrameCountLabel.Size = new System.Drawing.Size(103, 17);
            this.FrameCountLabel.TabIndex = 2;
            this.FrameCountLabel.Text = "Frame count: 0";
            // 
            // FpsLabel
            // 
            this.FpsLabel.AutoSize = true;
            this.FpsLabel.Location = new System.Drawing.Point(16, 389);
            this.FpsLabel.Name = "FpsLabel";
            this.FpsLabel.Size = new System.Drawing.Size(50, 17);
            this.FpsLabel.TabIndex = 3;
            this.FpsLabel.Text = "FPS: 0";
            // 
            // TrackingPartForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(604, 411);
            this.Controls.Add(this.FpsLabel);
            this.Controls.Add(this.FrameCountLabel);
            this.Controls.Add(this.TrackingLogListBox);
            this.Controls.Add(this.GameStatePictureBox);
            this.Name = "TrackingPartForm";
            this.Text = "TrackingPartForm";
            this.Load += new System.EventHandler(this.TrackingPartForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.GameStatePictureBox)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox GameStatePictureBox;
        private System.Windows.Forms.ListBox TrackingLogListBox;
        private System.Windows.Forms.Label FrameCountLabel;
        private System.Windows.Forms.Label FpsLabel;
    }
}