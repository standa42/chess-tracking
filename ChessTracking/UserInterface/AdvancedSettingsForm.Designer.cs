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
            this.MilimetersClippedTrackBar = new System.Windows.Forms.TrackBar();
            this.PointsIndicatingFigureTrackBar = new System.Windows.Forms.TrackBar();
            this.MilisecondsTasksTrackBar = new System.Windows.Forms.TrackBar();
            this.MilimetersClippedFromFigureLabel = new MaterialSkin.Controls.MaterialLabel();
            this.MilimetersClippedValueLabel = new MaterialSkin.Controls.MaterialLabel();
            this.PointsIndicatingFigureLabel = new MaterialSkin.Controls.MaterialLabel();
            this.PointsIndicatingFigureValueLabel = new MaterialSkin.Controls.MaterialLabel();
            this.MilisecondsTasksLabel = new MaterialSkin.Controls.MaterialLabel();
            this.MilisecondsTasksValueLabel = new MaterialSkin.Controls.MaterialLabel();
            this.BinarizationThresholdLabel = new MaterialSkin.Controls.MaterialLabel();
            this.BinarizationThresholdTrackbar = new System.Windows.Forms.TrackBar();
            this.BinarizationThresholdValueLabel = new MaterialSkin.Controls.MaterialLabel();
            this.OtzuToggleButton = new MaterialSkin.Controls.MaterialFlatButton();
            ((System.ComponentModel.ISupportInitialize)(this.MilimetersClippedTrackBar)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.PointsIndicatingFigureTrackBar)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.MilisecondsTasksTrackBar)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.BinarizationThresholdTrackbar)).BeginInit();
            this.SuspendLayout();
            // 
            // MilimetersClippedTrackBar
            // 
            this.MilimetersClippedTrackBar.AutoSize = false;
            this.MilimetersClippedTrackBar.Location = new System.Drawing.Point(15, 126);
            this.MilimetersClippedTrackBar.Maximum = 40;
            this.MilimetersClippedTrackBar.Name = "MilimetersClippedTrackBar";
            this.MilimetersClippedTrackBar.Size = new System.Drawing.Size(306, 41);
            this.MilimetersClippedTrackBar.TabIndex = 2;
            this.MilimetersClippedTrackBar.Value = 10;
            this.MilimetersClippedTrackBar.ValueChanged += new System.EventHandler(this.MilimetersClippedTrackBar_ValueChanged);
            // 
            // PointsIndicatingFigureTrackBar
            // 
            this.PointsIndicatingFigureTrackBar.AutoSize = false;
            this.PointsIndicatingFigureTrackBar.Location = new System.Drawing.Point(15, 197);
            this.PointsIndicatingFigureTrackBar.Maximum = 30;
            this.PointsIndicatingFigureTrackBar.Minimum = -2;
            this.PointsIndicatingFigureTrackBar.Name = "PointsIndicatingFigureTrackBar";
            this.PointsIndicatingFigureTrackBar.Size = new System.Drawing.Size(306, 41);
            this.PointsIndicatingFigureTrackBar.TabIndex = 5;
            this.PointsIndicatingFigureTrackBar.Value = 5;
            this.PointsIndicatingFigureTrackBar.ValueChanged += new System.EventHandler(this.PointsIndicatingFigureTrackBar_ValueChanged);
            // 
            // MilisecondsTasksTrackBar
            // 
            this.MilisecondsTasksTrackBar.AutoSize = false;
            this.MilisecondsTasksTrackBar.LargeChange = 25;
            this.MilisecondsTasksTrackBar.Location = new System.Drawing.Point(15, 268);
            this.MilisecondsTasksTrackBar.Maximum = 1000;
            this.MilisecondsTasksTrackBar.Name = "MilisecondsTasksTrackBar";
            this.MilisecondsTasksTrackBar.Size = new System.Drawing.Size(306, 41);
            this.MilisecondsTasksTrackBar.SmallChange = 5;
            this.MilisecondsTasksTrackBar.TabIndex = 8;
            this.MilisecondsTasksTrackBar.Value = 220;
            this.MilisecondsTasksTrackBar.ValueChanged += new System.EventHandler(this.MilisecondsTasksTrackBar_ValueChanged);
            // 
            // MilimetersClippedFromFigureLabel
            // 
            this.MilimetersClippedFromFigureLabel.AutoSize = true;
            this.MilimetersClippedFromFigureLabel.Depth = 0;
            this.MilimetersClippedFromFigureLabel.Font = new System.Drawing.Font("Roboto", 11F);
            this.MilimetersClippedFromFigureLabel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(222)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.MilimetersClippedFromFigureLabel.Location = new System.Drawing.Point(14, 99);
            this.MilimetersClippedFromFigureLabel.MouseState = MaterialSkin.MouseState.HOVER;
            this.MilimetersClippedFromFigureLabel.Name = "MilimetersClippedFromFigureLabel";
            this.MilimetersClippedFromFigureLabel.Size = new System.Drawing.Size(269, 24);
            this.MilimetersClippedFromFigureLabel.TabIndex = 9;
            this.MilimetersClippedFromFigureLabel.Text = "Milimeters clipped from figure";
            // 
            // MilimetersClippedValueLabel
            // 
            this.MilimetersClippedValueLabel.AutoSize = true;
            this.MilimetersClippedValueLabel.Depth = 0;
            this.MilimetersClippedValueLabel.Font = new System.Drawing.Font("Roboto", 11F);
            this.MilimetersClippedValueLabel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(222)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.MilimetersClippedValueLabel.Location = new System.Drawing.Point(289, 99);
            this.MilimetersClippedValueLabel.MouseState = MaterialSkin.MouseState.HOVER;
            this.MilimetersClippedValueLabel.Name = "MilimetersClippedValueLabel";
            this.MilimetersClippedValueLabel.Size = new System.Drawing.Size(32, 24);
            this.MilimetersClippedValueLabel.TabIndex = 10;
            this.MilimetersClippedValueLabel.Text = "10";
            // 
            // PointsIndicatingFigureLabel
            // 
            this.PointsIndicatingFigureLabel.AutoSize = true;
            this.PointsIndicatingFigureLabel.Depth = 0;
            this.PointsIndicatingFigureLabel.Font = new System.Drawing.Font("Roboto", 11F);
            this.PointsIndicatingFigureLabel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(222)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.PointsIndicatingFigureLabel.Location = new System.Drawing.Point(14, 170);
            this.PointsIndicatingFigureLabel.MouseState = MaterialSkin.MouseState.HOVER;
            this.PointsIndicatingFigureLabel.Name = "PointsIndicatingFigureLabel";
            this.PointsIndicatingFigureLabel.Size = new System.Drawing.Size(301, 24);
            this.PointsIndicatingFigureLabel.TabIndex = 11;
            this.PointsIndicatingFigureLabel.Text = "Number of points indicating figure";
            // 
            // PointsIndicatingFigureValueLabel
            // 
            this.PointsIndicatingFigureValueLabel.AutoSize = true;
            this.PointsIndicatingFigureValueLabel.Depth = 0;
            this.PointsIndicatingFigureValueLabel.Font = new System.Drawing.Font("Roboto", 11F);
            this.PointsIndicatingFigureValueLabel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(222)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.PointsIndicatingFigureValueLabel.Location = new System.Drawing.Point(327, 170);
            this.PointsIndicatingFigureValueLabel.MouseState = MaterialSkin.MouseState.HOVER;
            this.PointsIndicatingFigureValueLabel.Name = "PointsIndicatingFigureValueLabel";
            this.PointsIndicatingFigureValueLabel.Size = new System.Drawing.Size(21, 24);
            this.PointsIndicatingFigureValueLabel.TabIndex = 12;
            this.PointsIndicatingFigureValueLabel.Text = "5";
            // 
            // MilisecondsTasksLabel
            // 
            this.MilisecondsTasksLabel.AutoSize = true;
            this.MilisecondsTasksLabel.Depth = 0;
            this.MilisecondsTasksLabel.Font = new System.Drawing.Font("Roboto", 11F);
            this.MilisecondsTasksLabel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(222)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.MilisecondsTasksLabel.Location = new System.Drawing.Point(14, 241);
            this.MilisecondsTasksLabel.MouseState = MaterialSkin.MouseState.HOVER;
            this.MilisecondsTasksLabel.Name = "MilisecondsTasksLabel";
            this.MilisecondsTasksLabel.Size = new System.Drawing.Size(323, 24);
            this.MilisecondsTasksLabel.TabIndex = 13;
            this.MilisecondsTasksLabel.Text = "Minimum ms between tracking tasks";
            // 
            // MilisecondsTasksValueLabel
            // 
            this.MilisecondsTasksValueLabel.AutoSize = true;
            this.MilisecondsTasksValueLabel.Depth = 0;
            this.MilisecondsTasksValueLabel.Font = new System.Drawing.Font("Roboto", 11F);
            this.MilisecondsTasksValueLabel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(222)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.MilisecondsTasksValueLabel.Location = new System.Drawing.Point(352, 241);
            this.MilisecondsTasksValueLabel.MouseState = MaterialSkin.MouseState.HOVER;
            this.MilisecondsTasksValueLabel.Name = "MilisecondsTasksValueLabel";
            this.MilisecondsTasksValueLabel.Size = new System.Drawing.Size(43, 24);
            this.MilisecondsTasksValueLabel.TabIndex = 14;
            this.MilisecondsTasksValueLabel.Text = "220";
            // 
            // BinarizationThresholdLabel
            // 
            this.BinarizationThresholdLabel.AutoSize = true;
            this.BinarizationThresholdLabel.Depth = 0;
            this.BinarizationThresholdLabel.Font = new System.Drawing.Font("Roboto", 11F);
            this.BinarizationThresholdLabel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(222)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.BinarizationThresholdLabel.Location = new System.Drawing.Point(12, 312);
            this.BinarizationThresholdLabel.MouseState = MaterialSkin.MouseState.HOVER;
            this.BinarizationThresholdLabel.Name = "BinarizationThresholdLabel";
            this.BinarizationThresholdLabel.Size = new System.Drawing.Size(193, 24);
            this.BinarizationThresholdLabel.TabIndex = 15;
            this.BinarizationThresholdLabel.Text = "Binarization threshold";
            // 
            // BinarizationThresholdTrackbar
            // 
            this.BinarizationThresholdTrackbar.AutoSize = false;
            this.BinarizationThresholdTrackbar.LargeChange = 10;
            this.BinarizationThresholdTrackbar.Location = new System.Drawing.Point(18, 339);
            this.BinarizationThresholdTrackbar.Maximum = 255;
            this.BinarizationThresholdTrackbar.Name = "BinarizationThresholdTrackbar";
            this.BinarizationThresholdTrackbar.Size = new System.Drawing.Size(306, 41);
            this.BinarizationThresholdTrackbar.TabIndex = 16;
            this.BinarizationThresholdTrackbar.Value = 120;
            this.BinarizationThresholdTrackbar.ValueChanged += new System.EventHandler(this.BinarizationThresholdTrackbar_ValueChanged);
            // 
            // BinarizationThresholdValueLabel
            // 
            this.BinarizationThresholdValueLabel.AutoSize = true;
            this.BinarizationThresholdValueLabel.Depth = 0;
            this.BinarizationThresholdValueLabel.Font = new System.Drawing.Font("Roboto", 11F);
            this.BinarizationThresholdValueLabel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(222)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.BinarizationThresholdValueLabel.Location = new System.Drawing.Point(211, 312);
            this.BinarizationThresholdValueLabel.MouseState = MaterialSkin.MouseState.HOVER;
            this.BinarizationThresholdValueLabel.Name = "BinarizationThresholdValueLabel";
            this.BinarizationThresholdValueLabel.Size = new System.Drawing.Size(43, 24);
            this.BinarizationThresholdValueLabel.TabIndex = 17;
            this.BinarizationThresholdValueLabel.Text = "220";
            // 
            // OtzuToggleButton
            // 
            this.OtzuToggleButton.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.OtzuToggleButton.Depth = 0;
            this.OtzuToggleButton.Icon = null;
            this.OtzuToggleButton.Location = new System.Drawing.Point(18, 387);
            this.OtzuToggleButton.Margin = new System.Windows.Forms.Padding(4, 6, 4, 6);
            this.OtzuToggleButton.MaximumSize = new System.Drawing.Size(306, 36);
            this.OtzuToggleButton.MinimumSize = new System.Drawing.Size(306, 36);
            this.OtzuToggleButton.MouseState = MaterialSkin.MouseState.HOVER;
            this.OtzuToggleButton.Name = "OtzuToggleButton";
            this.OtzuToggleButton.Primary = false;
            this.OtzuToggleButton.Size = new System.Drawing.Size(306, 36);
            this.OtzuToggleButton.TabIndex = 18;
            this.OtzuToggleButton.Text = "Disable Otzu";
            this.OtzuToggleButton.UseVisualStyleBackColor = true;
            this.OtzuToggleButton.Click += new System.EventHandler(this.OtzuToggleButton_Click);
            // 
            // AdvancedSettingsForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(416, 450);
            this.Controls.Add(this.OtzuToggleButton);
            this.Controls.Add(this.BinarizationThresholdValueLabel);
            this.Controls.Add(this.BinarizationThresholdTrackbar);
            this.Controls.Add(this.BinarizationThresholdLabel);
            this.Controls.Add(this.MilisecondsTasksValueLabel);
            this.Controls.Add(this.MilisecondsTasksLabel);
            this.Controls.Add(this.PointsIndicatingFigureValueLabel);
            this.Controls.Add(this.PointsIndicatingFigureLabel);
            this.Controls.Add(this.MilimetersClippedValueLabel);
            this.Controls.Add(this.MilimetersClippedFromFigureLabel);
            this.Controls.Add(this.MilisecondsTasksTrackBar);
            this.Controls.Add(this.PointsIndicatingFigureTrackBar);
            this.Controls.Add(this.MilimetersClippedTrackBar);
            this.Name = "AdvancedSettingsForm";
            this.Text = "Advanced settings";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.AdvancedSettingsForm_FormClosing);
            ((System.ComponentModel.ISupportInitialize)(this.MilimetersClippedTrackBar)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.PointsIndicatingFigureTrackBar)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.MilisecondsTasksTrackBar)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.BinarizationThresholdTrackbar)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.TrackBar MilimetersClippedTrackBar;
        private System.Windows.Forms.TrackBar PointsIndicatingFigureTrackBar;
        private System.Windows.Forms.TrackBar MilisecondsTasksTrackBar;
        private MaterialSkin.Controls.MaterialLabel MilimetersClippedFromFigureLabel;
        private MaterialSkin.Controls.MaterialLabel MilimetersClippedValueLabel;
        private MaterialSkin.Controls.MaterialLabel PointsIndicatingFigureLabel;
        private MaterialSkin.Controls.MaterialLabel PointsIndicatingFigureValueLabel;
        private MaterialSkin.Controls.MaterialLabel MilisecondsTasksLabel;
        private MaterialSkin.Controls.MaterialLabel MilisecondsTasksValueLabel;
        private MaterialSkin.Controls.MaterialLabel BinarizationThresholdLabel;
        private System.Windows.Forms.TrackBar BinarizationThresholdTrackbar;
        private MaterialSkin.Controls.MaterialLabel BinarizationThresholdValueLabel;
        private MaterialSkin.Controls.MaterialFlatButton OtzuToggleButton;
    }
}