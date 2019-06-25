namespace ChessTracking.UserInterface
{
    partial class CalibrationSnapshotForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(CalibrationSnapshotForm));
            this.PictureBox1 = new System.Windows.Forms.PictureBox();
            this.NameLabel = new MaterialSkin.Controls.MaterialLabel();
            this.LeftButton = new MaterialSkin.Controls.MaterialRaisedButton();
            this.RightButton = new MaterialSkin.Controls.MaterialRaisedButton();
            ((System.ComponentModel.ISupportInitialize)(this.PictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // PictureBox1
            // 
            resources.ApplyResources(this.PictureBox1, "PictureBox1");
            this.PictureBox1.Name = "PictureBox1";
            this.PictureBox1.TabStop = false;
            // 
            // NameLabel
            // 
            resources.ApplyResources(this.NameLabel, "NameLabel");
            this.NameLabel.Depth = 0;
            this.NameLabel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(222)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.NameLabel.MouseState = MaterialSkin.MouseState.HOVER;
            this.NameLabel.Name = "NameLabel";
            // 
            // LeftButton
            // 
            resources.ApplyResources(this.LeftButton, "LeftButton");
            this.LeftButton.Depth = 0;
            this.LeftButton.Icon = null;
            this.LeftButton.MouseState = MaterialSkin.MouseState.HOVER;
            this.LeftButton.Name = "LeftButton";
            this.LeftButton.Primary = true;
            this.LeftButton.UseVisualStyleBackColor = true;
            this.LeftButton.Click += new System.EventHandler(this.LeftButton_Click);
            // 
            // RightButton
            // 
            resources.ApplyResources(this.RightButton, "RightButton");
            this.RightButton.Depth = 0;
            this.RightButton.Icon = null;
            this.RightButton.MouseState = MaterialSkin.MouseState.HOVER;
            this.RightButton.Name = "RightButton";
            this.RightButton.Primary = true;
            this.RightButton.UseVisualStyleBackColor = true;
            this.RightButton.Click += new System.EventHandler(this.RightButton_Click);
            // 
            // CalibrationSnapshotForm
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.RightButton);
            this.Controls.Add(this.LeftButton);
            this.Controls.Add(this.NameLabel);
            this.Controls.Add(this.PictureBox1);
            this.Name = "CalibrationSnapshotForm";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.CalibrationSnapshotForm_FormClosing);
            ((System.ComponentModel.ISupportInitialize)(this.PictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox PictureBox1;
        private MaterialSkin.Controls.MaterialLabel NameLabel;
        private MaterialSkin.Controls.MaterialRaisedButton LeftButton;
        private MaterialSkin.Controls.MaterialRaisedButton RightButton;
    }
}