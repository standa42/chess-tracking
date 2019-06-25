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
            resources.ApplyResources(this.VizualizationPictureBox, "VizualizationPictureBox");
            this.VizualizationPictureBox.Name = "VizualizationPictureBox";
            this.VizualizationPictureBox.TabStop = false;
            // 
            // VizualizationForm
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.VizualizationPictureBox);
            this.Name = "VizualizationForm";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.VizualizationForm_FormClosing);
            ((System.ComponentModel.ISupportInitialize)(this.VizualizationPictureBox)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.PictureBox VizualizationPictureBox;
    }
}