namespace ChessTracking.UserInterface
{
    partial class AdvancedSettingsForm
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
            this.MilimetersClippedFromFigureLabel = new System.Windows.Forms.Label();
            this.MilimetersClippedValueLabel = new System.Windows.Forms.Label();
            this.MilimetersClippedTrackBar = new System.Windows.Forms.TrackBar();
            this.PointsIndicatingFigureLabel = new System.Windows.Forms.Label();
            this.PointsIndicatingFigureValueLabel = new System.Windows.Forms.Label();
            this.PointsIndicatingFigureTrackBar = new System.Windows.Forms.TrackBar();
            ((System.ComponentModel.ISupportInitialize)(this.MilimetersClippedTrackBar)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.PointsIndicatingFigureTrackBar)).BeginInit();
            this.SuspendLayout();
            // 
            // MilimetersClippedFromFigureLabel
            // 
            this.MilimetersClippedFromFigureLabel.AutoSize = true;
            this.MilimetersClippedFromFigureLabel.Location = new System.Drawing.Point(12, 9);
            this.MilimetersClippedFromFigureLabel.Name = "MilimetersClippedFromFigureLabel";
            this.MilimetersClippedFromFigureLabel.Size = new System.Drawing.Size(192, 17);
            this.MilimetersClippedFromFigureLabel.TabIndex = 0;
            this.MilimetersClippedFromFigureLabel.Text = "Milimeters clipped from figure";
            // 
            // MilimetersClippedValueLabel
            // 
            this.MilimetersClippedValueLabel.AutoSize = true;
            this.MilimetersClippedValueLabel.Location = new System.Drawing.Point(210, 9);
            this.MilimetersClippedValueLabel.Name = "MilimetersClippedValueLabel";
            this.MilimetersClippedValueLabel.Size = new System.Drawing.Size(24, 17);
            this.MilimetersClippedValueLabel.TabIndex = 1;
            this.MilimetersClippedValueLabel.Text = "10";
            // 
            // MilimetersClippedTrackBar
            // 
            this.MilimetersClippedTrackBar.AutoSize = false;
            this.MilimetersClippedTrackBar.Location = new System.Drawing.Point(12, 29);
            this.MilimetersClippedTrackBar.Maximum = 40;
            this.MilimetersClippedTrackBar.Name = "MilimetersClippedTrackBar";
            this.MilimetersClippedTrackBar.Size = new System.Drawing.Size(219, 27);
            this.MilimetersClippedTrackBar.TabIndex = 2;
            this.MilimetersClippedTrackBar.Value = 10;
            this.MilimetersClippedTrackBar.ValueChanged += new System.EventHandler(this.MilimetersClippedTrackBar_ValueChanged);
            // 
            // PointsIndicatingFigureLabel
            // 
            this.PointsIndicatingFigureLabel.AutoSize = true;
            this.PointsIndicatingFigureLabel.Location = new System.Drawing.Point(12, 63);
            this.PointsIndicatingFigureLabel.Name = "PointsIndicatingFigureLabel";
            this.PointsIndicatingFigureLabel.Size = new System.Drawing.Size(220, 17);
            this.PointsIndicatingFigureLabel.TabIndex = 3;
            this.PointsIndicatingFigureLabel.Text = "Number of points indicating figure";
            // 
            // PointsIndicatingFigureValueLabel
            // 
            this.PointsIndicatingFigureValueLabel.AutoSize = true;
            this.PointsIndicatingFigureValueLabel.Location = new System.Drawing.Point(235, 63);
            this.PointsIndicatingFigureValueLabel.Name = "PointsIndicatingFigureValueLabel";
            this.PointsIndicatingFigureValueLabel.Size = new System.Drawing.Size(16, 17);
            this.PointsIndicatingFigureValueLabel.TabIndex = 4;
            this.PointsIndicatingFigureValueLabel.Text = "5";
            // 
            // PointsIndicatingFigureTrackBar
            // 
            this.PointsIndicatingFigureTrackBar.AutoSize = false;
            this.PointsIndicatingFigureTrackBar.Location = new System.Drawing.Point(15, 83);
            this.PointsIndicatingFigureTrackBar.Maximum = 30;
            this.PointsIndicatingFigureTrackBar.Minimum = -2;
            this.PointsIndicatingFigureTrackBar.Name = "PointsIndicatingFigureTrackBar";
            this.PointsIndicatingFigureTrackBar.Size = new System.Drawing.Size(219, 27);
            this.PointsIndicatingFigureTrackBar.TabIndex = 5;
            this.PointsIndicatingFigureTrackBar.Value = 5;
            this.PointsIndicatingFigureTrackBar.ValueChanged += new System.EventHandler(this.PointsIndicatingFigureTrackBar_ValueChanged);
            // 
            // AdvancedSettingsForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(852, 498);
            this.Controls.Add(this.PointsIndicatingFigureTrackBar);
            this.Controls.Add(this.PointsIndicatingFigureValueLabel);
            this.Controls.Add(this.PointsIndicatingFigureLabel);
            this.Controls.Add(this.MilimetersClippedTrackBar);
            this.Controls.Add(this.MilimetersClippedValueLabel);
            this.Controls.Add(this.MilimetersClippedFromFigureLabel);
            this.Name = "AdvancedSettingsForm";
            this.Text = "AdvancedSettingsForm";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.AdvancedSettingsForm_FormClosing);
            ((System.ComponentModel.ISupportInitialize)(this.MilimetersClippedTrackBar)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.PointsIndicatingFigureTrackBar)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label MilimetersClippedFromFigureLabel;
        private System.Windows.Forms.Label MilimetersClippedValueLabel;
        private System.Windows.Forms.TrackBar MilimetersClippedTrackBar;
        private System.Windows.Forms.Label PointsIndicatingFigureLabel;
        private System.Windows.Forms.Label PointsIndicatingFigureValueLabel;
        private System.Windows.Forms.TrackBar PointsIndicatingFigureTrackBar;
    }
}