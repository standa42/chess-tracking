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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(AdvancedSettingsForm));
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
            this.materialLabel1 = new MaterialSkin.Controls.MaterialLabel();
            this.FiguresColorMetricButton = new MaterialSkin.Controls.MaterialFlatButton();
            this.DistanceMetricFittingChessboardButton = new MaterialSkin.Controls.MaterialFlatButton();
            this.materialLabel2 = new MaterialSkin.Controls.MaterialLabel();
            this.DistanceMetricFittingChessboardTrackBar = new System.Windows.Forms.TrackBar();
            this.DistanceMetricFittingChessboardButtonValueLabel = new MaterialSkin.Controls.MaterialLabel();
            ((System.ComponentModel.ISupportInitialize)(this.MilimetersClippedTrackBar)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.PointsIndicatingFigureTrackBar)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.MilisecondsTasksTrackBar)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.BinarizationThresholdTrackbar)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.DistanceMetricFittingChessboardTrackBar)).BeginInit();
            this.SuspendLayout();
            // 
            // MilimetersClippedTrackBar
            // 
            this.MilimetersClippedTrackBar.AutoSize = false;
            this.MilimetersClippedTrackBar.Location = new System.Drawing.Point(43, 124);
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
            this.PointsIndicatingFigureTrackBar.Location = new System.Drawing.Point(43, 195);
            this.PointsIndicatingFigureTrackBar.Maximum = 30;
            this.PointsIndicatingFigureTrackBar.Minimum = -1;
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
            this.MilisecondsTasksTrackBar.Location = new System.Drawing.Point(43, 266);
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
            this.MilimetersClippedFromFigureLabel.Location = new System.Drawing.Point(21, 97);
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
            this.MilimetersClippedValueLabel.Location = new System.Drawing.Point(296, 97);
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
            this.PointsIndicatingFigureLabel.Location = new System.Drawing.Point(21, 168);
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
            this.PointsIndicatingFigureValueLabel.Location = new System.Drawing.Point(334, 168);
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
            this.MilisecondsTasksLabel.Location = new System.Drawing.Point(21, 239);
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
            this.MilisecondsTasksValueLabel.Location = new System.Drawing.Point(359, 239);
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
            this.BinarizationThresholdLabel.Location = new System.Drawing.Point(19, 310);
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
            this.BinarizationThresholdTrackbar.Location = new System.Drawing.Point(46, 337);
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
            this.BinarizationThresholdValueLabel.Location = new System.Drawing.Point(218, 310);
            this.BinarizationThresholdValueLabel.MouseState = MaterialSkin.MouseState.HOVER;
            this.BinarizationThresholdValueLabel.Name = "BinarizationThresholdValueLabel";
            this.BinarizationThresholdValueLabel.Size = new System.Drawing.Size(43, 24);
            this.BinarizationThresholdValueLabel.TabIndex = 17;
            this.BinarizationThresholdValueLabel.Text = "120";
            // 
            // OtzuToggleButton
            // 
            this.OtzuToggleButton.AutoSize = true;
            this.OtzuToggleButton.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.OtzuToggleButton.Depth = 0;
            this.OtzuToggleButton.Icon = null;
            this.OtzuToggleButton.Location = new System.Drawing.Point(46, 385);
            this.OtzuToggleButton.Margin = new System.Windows.Forms.Padding(4, 6, 4, 6);
            this.OtzuToggleButton.MaximumSize = new System.Drawing.Size(306, 36);
            this.OtzuToggleButton.MinimumSize = new System.Drawing.Size(306, 36);
            this.OtzuToggleButton.MouseState = MaterialSkin.MouseState.HOVER;
            this.OtzuToggleButton.Name = "OtzuToggleButton";
            this.OtzuToggleButton.Primary = false;
            this.OtzuToggleButton.Size = new System.Drawing.Size(306, 36);
            this.OtzuToggleButton.TabIndex = 18;
            this.OtzuToggleButton.Text = "Disable Otsu thresholding";
            this.OtzuToggleButton.UseVisualStyleBackColor = true;
            this.OtzuToggleButton.Click += new System.EventHandler(this.OtzuToggleButton_Click);
            // 
            // materialLabel1
            // 
            this.materialLabel1.AutoSize = true;
            this.materialLabel1.Depth = 0;
            this.materialLabel1.Font = new System.Drawing.Font("Roboto", 11F);
            this.materialLabel1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(222)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.materialLabel1.Location = new System.Drawing.Point(417, 97);
            this.materialLabel1.MouseState = MaterialSkin.MouseState.HOVER;
            this.materialLabel1.Name = "materialLabel1";
            this.materialLabel1.Size = new System.Drawing.Size(291, 24);
            this.materialLabel1.TabIndex = 19;
            this.materialLabel1.Text = "Choice of color metric for figures";
            // 
            // FiguresColorMetricButton
            // 
            this.FiguresColorMetricButton.AutoSize = true;
            this.FiguresColorMetricButton.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.FiguresColorMetricButton.Depth = 0;
            this.FiguresColorMetricButton.Icon = null;
            this.FiguresColorMetricButton.Location = new System.Drawing.Point(442, 127);
            this.FiguresColorMetricButton.Margin = new System.Windows.Forms.Padding(4, 6, 4, 6);
            this.FiguresColorMetricButton.MaximumSize = new System.Drawing.Size(306, 36);
            this.FiguresColorMetricButton.MinimumSize = new System.Drawing.Size(306, 36);
            this.FiguresColorMetricButton.MouseState = MaterialSkin.MouseState.HOVER;
            this.FiguresColorMetricButton.Name = "FiguresColorMetricButton";
            this.FiguresColorMetricButton.Primary = false;
            this.FiguresColorMetricButton.Size = new System.Drawing.Size(306, 36);
            this.FiguresColorMetricButton.TabIndex = 20;
            this.FiguresColorMetricButton.Text = "Set default metric";
            this.FiguresColorMetricButton.UseVisualStyleBackColor = true;
            this.FiguresColorMetricButton.Click += new System.EventHandler(this.FiguresColorMetricButton_Click);
            // 
            // DistanceMetricFittingChessboardButton
            // 
            this.DistanceMetricFittingChessboardButton.AutoSize = true;
            this.DistanceMetricFittingChessboardButton.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.DistanceMetricFittingChessboardButton.Depth = 0;
            this.DistanceMetricFittingChessboardButton.Icon = null;
            this.DistanceMetricFittingChessboardButton.Location = new System.Drawing.Point(442, 223);
            this.DistanceMetricFittingChessboardButton.Margin = new System.Windows.Forms.Padding(4, 6, 4, 6);
            this.DistanceMetricFittingChessboardButton.MaximumSize = new System.Drawing.Size(306, 36);
            this.DistanceMetricFittingChessboardButton.MinimumSize = new System.Drawing.Size(306, 36);
            this.DistanceMetricFittingChessboardButton.MouseState = MaterialSkin.MouseState.HOVER;
            this.DistanceMetricFittingChessboardButton.Name = "DistanceMetricFittingChessboardButton";
            this.DistanceMetricFittingChessboardButton.Primary = false;
            this.DistanceMetricFittingChessboardButton.Size = new System.Drawing.Size(306, 36);
            this.DistanceMetricFittingChessboardButton.TabIndex = 22;
            this.DistanceMetricFittingChessboardButton.Text = "Set default metric";
            this.DistanceMetricFittingChessboardButton.UseVisualStyleBackColor = true;
            this.DistanceMetricFittingChessboardButton.Click += new System.EventHandler(this.DistanceMetricFittingChessboardButton_Click);
            // 
            // materialLabel2
            // 
            this.materialLabel2.AutoSize = true;
            this.materialLabel2.Depth = 0;
            this.materialLabel2.Font = new System.Drawing.Font("Roboto", 11F);
            this.materialLabel2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(222)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.materialLabel2.Location = new System.Drawing.Point(417, 169);
            this.materialLabel2.MouseState = MaterialSkin.MouseState.HOVER;
            this.materialLabel2.Name = "materialLabel2";
            this.materialLabel2.Size = new System.Drawing.Size(360, 48);
            this.materialLabel2.TabIndex = 21;
            this.materialLabel2.Text = "Choice of distance metric for chessboard\r\n fitting and its clipping threshold [mm" +
    "]";
            // 
            // DistanceMetricFittingChessboardTrackBar
            // 
            this.DistanceMetricFittingChessboardTrackBar.AutoSize = false;
            this.DistanceMetricFittingChessboardTrackBar.Location = new System.Drawing.Point(442, 268);
            this.DistanceMetricFittingChessboardTrackBar.Maximum = 60;
            this.DistanceMetricFittingChessboardTrackBar.Minimum = 1;
            this.DistanceMetricFittingChessboardTrackBar.Name = "DistanceMetricFittingChessboardTrackBar";
            this.DistanceMetricFittingChessboardTrackBar.Size = new System.Drawing.Size(306, 41);
            this.DistanceMetricFittingChessboardTrackBar.TabIndex = 23;
            this.DistanceMetricFittingChessboardTrackBar.Value = 15;
            this.DistanceMetricFittingChessboardTrackBar.ValueChanged += new System.EventHandler(this.DistanceMetricFittingChessboardTrackBar_ValueChanged);
            // 
            // DistanceMetricFittingChessboardButtonValueLabel
            // 
            this.DistanceMetricFittingChessboardButtonValueLabel.AutoSize = true;
            this.DistanceMetricFittingChessboardButtonValueLabel.Depth = 0;
            this.DistanceMetricFittingChessboardButtonValueLabel.Font = new System.Drawing.Font("Roboto", 11F);
            this.DistanceMetricFittingChessboardButtonValueLabel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(222)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.DistanceMetricFittingChessboardButtonValueLabel.Location = new System.Drawing.Point(783, 169);
            this.DistanceMetricFittingChessboardButtonValueLabel.MouseState = MaterialSkin.MouseState.HOVER;
            this.DistanceMetricFittingChessboardButtonValueLabel.Name = "DistanceMetricFittingChessboardButtonValueLabel";
            this.DistanceMetricFittingChessboardButtonValueLabel.Size = new System.Drawing.Size(32, 24);
            this.DistanceMetricFittingChessboardButtonValueLabel.TabIndex = 24;
            this.DistanceMetricFittingChessboardButtonValueLabel.Text = "15";
            // 
            // AdvancedSettingsForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(832, 434);
            this.Controls.Add(this.DistanceMetricFittingChessboardButtonValueLabel);
            this.Controls.Add(this.DistanceMetricFittingChessboardTrackBar);
            this.Controls.Add(this.DistanceMetricFittingChessboardButton);
            this.Controls.Add(this.materialLabel2);
            this.Controls.Add(this.FiguresColorMetricButton);
            this.Controls.Add(this.materialLabel1);
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
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "AdvancedSettingsForm";
            this.Text = "Advanced settings";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.AdvancedSettingsForm_FormClosing);
            ((System.ComponentModel.ISupportInitialize)(this.MilimetersClippedTrackBar)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.PointsIndicatingFigureTrackBar)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.MilisecondsTasksTrackBar)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.BinarizationThresholdTrackbar)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.DistanceMetricFittingChessboardTrackBar)).EndInit();
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
        private MaterialSkin.Controls.MaterialLabel materialLabel1;
        private MaterialSkin.Controls.MaterialFlatButton FiguresColorMetricButton;
        private MaterialSkin.Controls.MaterialFlatButton DistanceMetricFittingChessboardButton;
        private MaterialSkin.Controls.MaterialLabel materialLabel2;
        private System.Windows.Forms.TrackBar DistanceMetricFittingChessboardTrackBar;
        private MaterialSkin.Controls.MaterialLabel DistanceMetricFittingChessboardButtonValueLabel;
    }
}