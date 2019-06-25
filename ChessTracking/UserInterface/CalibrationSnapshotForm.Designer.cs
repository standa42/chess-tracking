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
            this.PictureBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.PictureBox1.Location = new System.Drawing.Point(-1, 77);
            this.PictureBox1.Name = "PictureBox1";
            this.PictureBox1.Size = new System.Drawing.Size(952, 550);
            this.PictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.PictureBox1.TabIndex = 0;
            this.PictureBox1.TabStop = false;
            // 
            // NameLabel
            // 
            this.NameLabel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.NameLabel.AutoSize = true;
            this.NameLabel.Depth = 0;
            this.NameLabel.Font = new System.Drawing.Font("Roboto", 11F);
            this.NameLabel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(222)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.NameLabel.Location = new System.Drawing.Point(12, 640);
            this.NameLabel.MouseState = MaterialSkin.MouseState.HOVER;
            this.NameLabel.Name = "NameLabel";
            this.NameLabel.Size = new System.Drawing.Size(157, 24);
            this.NameLabel.TabIndex = 1;
            this.NameLabel.Text = "No data available";
            // 
            // LeftButton
            // 
            this.LeftButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.LeftButton.AutoSize = true;
            this.LeftButton.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.LeftButton.Depth = 0;
            this.LeftButton.Icon = null;
            this.LeftButton.Location = new System.Drawing.Point(871, 641);
            this.LeftButton.MaximumSize = new System.Drawing.Size(40, 40);
            this.LeftButton.MinimumSize = new System.Drawing.Size(40, 40);
            this.LeftButton.MouseState = MaterialSkin.MouseState.HOVER;
            this.LeftButton.Name = "LeftButton";
            this.LeftButton.Primary = true;
            this.LeftButton.Size = new System.Drawing.Size(40, 40);
            this.LeftButton.TabIndex = 2;
            this.LeftButton.Text = "<";
            this.LeftButton.UseVisualStyleBackColor = true;
            this.LeftButton.Visible = false;
            this.LeftButton.Click += new System.EventHandler(this.LeftButton_Click);
            // 
            // RightButton
            // 
            this.RightButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.RightButton.AutoSize = true;
            this.RightButton.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.RightButton.Depth = 0;
            this.RightButton.Icon = null;
            this.RightButton.Location = new System.Drawing.Point(917, 641);
            this.RightButton.MaximumSize = new System.Drawing.Size(40, 40);
            this.RightButton.MinimumSize = new System.Drawing.Size(40, 40);
            this.RightButton.MouseState = MaterialSkin.MouseState.HOVER;
            this.RightButton.Name = "RightButton";
            this.RightButton.Primary = true;
            this.RightButton.Size = new System.Drawing.Size(40, 40);
            this.RightButton.TabIndex = 3;
            this.RightButton.Text = ">";
            this.RightButton.UseVisualStyleBackColor = true;
            this.RightButton.Visible = false;
            this.RightButton.Click += new System.EventHandler(this.RightButton_Click);
            // 
            // CalibrationSnapshotForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(951, 677);
            this.Controls.Add(this.RightButton);
            this.Controls.Add(this.LeftButton);
            this.Controls.Add(this.NameLabel);
            this.Controls.Add(this.PictureBox1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "CalibrationSnapshotForm";
            this.Text = "CalibrationSnapshotForm";
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