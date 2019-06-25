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
            resources.ApplyResources(this.MilimetersClippedTrackBar, "MilimetersClippedTrackBar");
            this.MilimetersClippedTrackBar.Maximum = 40;
            this.MilimetersClippedTrackBar.Name = "MilimetersClippedTrackBar";
            this.MilimetersClippedTrackBar.Value = 10;
            this.MilimetersClippedTrackBar.ValueChanged += new System.EventHandler(this.MilimetersClippedTrackBar_ValueChanged);
            // 
            // PointsIndicatingFigureTrackBar
            // 
            resources.ApplyResources(this.PointsIndicatingFigureTrackBar, "PointsIndicatingFigureTrackBar");
            this.PointsIndicatingFigureTrackBar.Maximum = 30;
            this.PointsIndicatingFigureTrackBar.Minimum = -1;
            this.PointsIndicatingFigureTrackBar.Name = "PointsIndicatingFigureTrackBar";
            this.PointsIndicatingFigureTrackBar.Value = 5;
            this.PointsIndicatingFigureTrackBar.ValueChanged += new System.EventHandler(this.PointsIndicatingFigureTrackBar_ValueChanged);
            // 
            // MilisecondsTasksTrackBar
            // 
            resources.ApplyResources(this.MilisecondsTasksTrackBar, "MilisecondsTasksTrackBar");
            this.MilisecondsTasksTrackBar.LargeChange = 25;
            this.MilisecondsTasksTrackBar.Maximum = 1000;
            this.MilisecondsTasksTrackBar.Name = "MilisecondsTasksTrackBar";
            this.MilisecondsTasksTrackBar.SmallChange = 5;
            this.MilisecondsTasksTrackBar.Value = 220;
            this.MilisecondsTasksTrackBar.ValueChanged += new System.EventHandler(this.MilisecondsTasksTrackBar_ValueChanged);
            // 
            // MilimetersClippedFromFigureLabel
            // 
            resources.ApplyResources(this.MilimetersClippedFromFigureLabel, "MilimetersClippedFromFigureLabel");
            this.MilimetersClippedFromFigureLabel.Depth = 0;
            this.MilimetersClippedFromFigureLabel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(222)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.MilimetersClippedFromFigureLabel.MouseState = MaterialSkin.MouseState.HOVER;
            this.MilimetersClippedFromFigureLabel.Name = "MilimetersClippedFromFigureLabel";
            // 
            // MilimetersClippedValueLabel
            // 
            resources.ApplyResources(this.MilimetersClippedValueLabel, "MilimetersClippedValueLabel");
            this.MilimetersClippedValueLabel.Depth = 0;
            this.MilimetersClippedValueLabel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(222)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.MilimetersClippedValueLabel.MouseState = MaterialSkin.MouseState.HOVER;
            this.MilimetersClippedValueLabel.Name = "MilimetersClippedValueLabel";
            // 
            // PointsIndicatingFigureLabel
            // 
            resources.ApplyResources(this.PointsIndicatingFigureLabel, "PointsIndicatingFigureLabel");
            this.PointsIndicatingFigureLabel.Depth = 0;
            this.PointsIndicatingFigureLabel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(222)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.PointsIndicatingFigureLabel.MouseState = MaterialSkin.MouseState.HOVER;
            this.PointsIndicatingFigureLabel.Name = "PointsIndicatingFigureLabel";
            // 
            // PointsIndicatingFigureValueLabel
            // 
            resources.ApplyResources(this.PointsIndicatingFigureValueLabel, "PointsIndicatingFigureValueLabel");
            this.PointsIndicatingFigureValueLabel.Depth = 0;
            this.PointsIndicatingFigureValueLabel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(222)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.PointsIndicatingFigureValueLabel.MouseState = MaterialSkin.MouseState.HOVER;
            this.PointsIndicatingFigureValueLabel.Name = "PointsIndicatingFigureValueLabel";
            // 
            // MilisecondsTasksLabel
            // 
            resources.ApplyResources(this.MilisecondsTasksLabel, "MilisecondsTasksLabel");
            this.MilisecondsTasksLabel.Depth = 0;
            this.MilisecondsTasksLabel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(222)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.MilisecondsTasksLabel.MouseState = MaterialSkin.MouseState.HOVER;
            this.MilisecondsTasksLabel.Name = "MilisecondsTasksLabel";
            // 
            // MilisecondsTasksValueLabel
            // 
            resources.ApplyResources(this.MilisecondsTasksValueLabel, "MilisecondsTasksValueLabel");
            this.MilisecondsTasksValueLabel.Depth = 0;
            this.MilisecondsTasksValueLabel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(222)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.MilisecondsTasksValueLabel.MouseState = MaterialSkin.MouseState.HOVER;
            this.MilisecondsTasksValueLabel.Name = "MilisecondsTasksValueLabel";
            // 
            // BinarizationThresholdLabel
            // 
            resources.ApplyResources(this.BinarizationThresholdLabel, "BinarizationThresholdLabel");
            this.BinarizationThresholdLabel.Depth = 0;
            this.BinarizationThresholdLabel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(222)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.BinarizationThresholdLabel.MouseState = MaterialSkin.MouseState.HOVER;
            this.BinarizationThresholdLabel.Name = "BinarizationThresholdLabel";
            // 
            // BinarizationThresholdTrackbar
            // 
            resources.ApplyResources(this.BinarizationThresholdTrackbar, "BinarizationThresholdTrackbar");
            this.BinarizationThresholdTrackbar.LargeChange = 10;
            this.BinarizationThresholdTrackbar.Maximum = 255;
            this.BinarizationThresholdTrackbar.Name = "BinarizationThresholdTrackbar";
            this.BinarizationThresholdTrackbar.Value = 120;
            this.BinarizationThresholdTrackbar.ValueChanged += new System.EventHandler(this.BinarizationThresholdTrackbar_ValueChanged);
            // 
            // BinarizationThresholdValueLabel
            // 
            resources.ApplyResources(this.BinarizationThresholdValueLabel, "BinarizationThresholdValueLabel");
            this.BinarizationThresholdValueLabel.Depth = 0;
            this.BinarizationThresholdValueLabel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(222)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.BinarizationThresholdValueLabel.MouseState = MaterialSkin.MouseState.HOVER;
            this.BinarizationThresholdValueLabel.Name = "BinarizationThresholdValueLabel";
            // 
            // OtzuToggleButton
            // 
            resources.ApplyResources(this.OtzuToggleButton, "OtzuToggleButton");
            this.OtzuToggleButton.Depth = 0;
            this.OtzuToggleButton.Icon = null;
            this.OtzuToggleButton.MouseState = MaterialSkin.MouseState.HOVER;
            this.OtzuToggleButton.Name = "OtzuToggleButton";
            this.OtzuToggleButton.Primary = false;
            this.OtzuToggleButton.UseVisualStyleBackColor = true;
            this.OtzuToggleButton.Click += new System.EventHandler(this.OtzuToggleButton_Click);
            // 
            // materialLabel1
            // 
            resources.ApplyResources(this.materialLabel1, "materialLabel1");
            this.materialLabel1.Depth = 0;
            this.materialLabel1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(222)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.materialLabel1.MouseState = MaterialSkin.MouseState.HOVER;
            this.materialLabel1.Name = "materialLabel1";
            // 
            // FiguresColorMetricButton
            // 
            resources.ApplyResources(this.FiguresColorMetricButton, "FiguresColorMetricButton");
            this.FiguresColorMetricButton.Depth = 0;
            this.FiguresColorMetricButton.Icon = null;
            this.FiguresColorMetricButton.MouseState = MaterialSkin.MouseState.HOVER;
            this.FiguresColorMetricButton.Name = "FiguresColorMetricButton";
            this.FiguresColorMetricButton.Primary = false;
            this.FiguresColorMetricButton.UseVisualStyleBackColor = true;
            this.FiguresColorMetricButton.Click += new System.EventHandler(this.FiguresColorMetricButton_Click);
            // 
            // DistanceMetricFittingChessboardButton
            // 
            resources.ApplyResources(this.DistanceMetricFittingChessboardButton, "DistanceMetricFittingChessboardButton");
            this.DistanceMetricFittingChessboardButton.Depth = 0;
            this.DistanceMetricFittingChessboardButton.Icon = null;
            this.DistanceMetricFittingChessboardButton.MouseState = MaterialSkin.MouseState.HOVER;
            this.DistanceMetricFittingChessboardButton.Name = "DistanceMetricFittingChessboardButton";
            this.DistanceMetricFittingChessboardButton.Primary = false;
            this.DistanceMetricFittingChessboardButton.UseVisualStyleBackColor = true;
            this.DistanceMetricFittingChessboardButton.Click += new System.EventHandler(this.DistanceMetricFittingChessboardButton_Click);
            // 
            // materialLabel2
            // 
            resources.ApplyResources(this.materialLabel2, "materialLabel2");
            this.materialLabel2.Depth = 0;
            this.materialLabel2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(222)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.materialLabel2.MouseState = MaterialSkin.MouseState.HOVER;
            this.materialLabel2.Name = "materialLabel2";
            // 
            // DistanceMetricFittingChessboardTrackBar
            // 
            resources.ApplyResources(this.DistanceMetricFittingChessboardTrackBar, "DistanceMetricFittingChessboardTrackBar");
            this.DistanceMetricFittingChessboardTrackBar.Maximum = 60;
            this.DistanceMetricFittingChessboardTrackBar.Minimum = 1;
            this.DistanceMetricFittingChessboardTrackBar.Name = "DistanceMetricFittingChessboardTrackBar";
            this.DistanceMetricFittingChessboardTrackBar.Value = 15;
            this.DistanceMetricFittingChessboardTrackBar.ValueChanged += new System.EventHandler(this.DistanceMetricFittingChessboardTrackBar_ValueChanged);
            // 
            // DistanceMetricFittingChessboardButtonValueLabel
            // 
            resources.ApplyResources(this.DistanceMetricFittingChessboardButtonValueLabel, "DistanceMetricFittingChessboardButtonValueLabel");
            this.DistanceMetricFittingChessboardButtonValueLabel.Depth = 0;
            this.DistanceMetricFittingChessboardButtonValueLabel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(222)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.DistanceMetricFittingChessboardButtonValueLabel.MouseState = MaterialSkin.MouseState.HOVER;
            this.DistanceMetricFittingChessboardButtonValueLabel.Name = "DistanceMetricFittingChessboardButtonValueLabel";
            // 
            // AdvancedSettingsForm
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
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
            this.Name = "AdvancedSettingsForm";
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