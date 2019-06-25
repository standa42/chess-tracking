namespace ChessTracking.UserInterface
{
    partial class VizualizationForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(VizualizationForm));
            this.VizualizationPictureBox = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.VizualizationPictureBox)).BeginInit();
            this.SuspendLayout();
            // 
            // VizualizationPictureBox
            // 
            this.VizualizationPictureBox.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.VizualizationPictureBox.Location = new System.Drawing.Point(4, 79);
            this.VizualizationPictureBox.Name = "VizualizationPictureBox";
            this.VizualizationPictureBox.Size = new System.Drawing.Size(960, 540);
            this.VizualizationPictureBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.VizualizationPictureBox.TabIndex = 0;
            this.VizualizationPictureBox.TabStop = false;
            // 
            // VizualizationForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(969, 622);
            this.Controls.Add(this.VizualizationPictureBox);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "VizualizationForm";
            this.Text = "Vizualization";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.VizualizationForm_FormClosing);
            ((System.ComponentModel.ISupportInitialize)(this.VizualizationPictureBox)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.PictureBox VizualizationPictureBox;
    }
}