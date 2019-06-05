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
            this.VizualizationPictureBox = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.VizualizationPictureBox)).BeginInit();
            this.SuspendLayout();
            // 
            // VizualizationPictureBox
            // 
            this.VizualizationPictureBox.Location = new System.Drawing.Point(-1, 77);
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
            this.ClientSize = new System.Drawing.Size(957, 616);
            this.Controls.Add(this.VizualizationPictureBox);
            this.Name = "VizualizationForm";
            this.Text = "VizualizationForm";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.VizualizationForm_FormClosing);
            ((System.ComponentModel.ISupportInitialize)(this.VizualizationPictureBox)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.PictureBox VizualizationPictureBox;
    }
}