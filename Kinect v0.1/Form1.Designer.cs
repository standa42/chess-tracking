namespace Kinect_v0._1
{
	partial class Form1
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
            this.FPSLabel = new System.Windows.Forms.Label();
            this.RekalibraceBtn = new System.Windows.Forms.Button();
            this.VykreslitTrojuhelnikyBtn = new System.Windows.Forms.Button();
            this.FrameCountLabel = new System.Windows.Forms.Label();
            this.availabilityLabel = new System.Windows.Forms.Label();
            this.ChangableItemsPanel = new System.Windows.Forms.Panel();
            this.LocateObjectsBtn = new System.Windows.Forms.Button();
            this.ObjectsSizeLabel = new System.Windows.Forms.Label();
            this.GaussBtn = new System.Windows.Forms.Button();
            this.SerializationBtn = new System.Windows.Forms.Button();
            this.PlaneTrackBtn = new System.Windows.Forms.Button();
            this.richTextBox1 = new System.Windows.Forms.RichTextBox();
            this.button1 = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // FPSLabel
            // 
            this.FPSLabel.AutoSize = true;
            this.FPSLabel.Location = new System.Drawing.Point(1631, 957);
            this.FPSLabel.Name = "FPSLabel";
            this.FPSLabel.Size = new System.Drawing.Size(38, 17);
            this.FPSLabel.TabIndex = 23;
            this.FPSLabel.Text = "FPS:";
            // 
            // RekalibraceBtn
            // 
            this.RekalibraceBtn.Location = new System.Drawing.Point(1380, 883);
            this.RekalibraceBtn.Name = "RekalibraceBtn";
            this.RekalibraceBtn.Size = new System.Drawing.Size(211, 30);
            this.RekalibraceBtn.TabIndex = 24;
            this.RekalibraceBtn.Text = "RANSAC recalibration (table)";
            this.RekalibraceBtn.UseVisualStyleBackColor = true;
            this.RekalibraceBtn.Click += new System.EventHandler(this.RekalibraceBtn_Click);
            // 
            // VykreslitTrojuhelnikyBtn
            // 
            this.VykreslitTrojuhelnikyBtn.Location = new System.Drawing.Point(1380, 847);
            this.VykreslitTrojuhelnikyBtn.Name = "VykreslitTrojuhelnikyBtn";
            this.VykreslitTrojuhelnikyBtn.Size = new System.Drawing.Size(211, 30);
            this.VykreslitTrojuhelnikyBtn.TabIndex = 27;
            this.VykreslitTrojuhelnikyBtn.Text = "Draw RANSAC triangles";
            this.VykreslitTrojuhelnikyBtn.UseVisualStyleBackColor = true;
            this.VykreslitTrojuhelnikyBtn.Visible = false;
            this.VykreslitTrojuhelnikyBtn.Click += new System.EventHandler(this.VykreslitTrojuhelnikyBtn_Click);
            // 
            // FrameCountLabel
            // 
            this.FrameCountLabel.AutoSize = true;
            this.FrameCountLabel.Location = new System.Drawing.Point(1379, 957);
            this.FrameCountLabel.Name = "FrameCountLabel";
            this.FrameCountLabel.Size = new System.Drawing.Size(89, 17);
            this.FrameCountLabel.TabIndex = 17;
            this.FrameCountLabel.Text = "FrameCount:";
            // 
            // availabilityLabel
            // 
            this.availabilityLabel.AutoSize = true;
            this.availabilityLabel.Location = new System.Drawing.Point(1380, 926);
            this.availabilityLabel.Name = "availabilityLabel";
            this.availabilityLabel.Size = new System.Drawing.Size(73, 17);
            this.availabilityLabel.TabIndex = 28;
            this.availabilityLabel.Text = "availability";
            // 
            // ChangableItemsPanel
            // 
            this.ChangableItemsPanel.AutoScroll = true;
            this.ChangableItemsPanel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.ChangableItemsPanel.Location = new System.Drawing.Point(1383, 13);
            this.ChangableItemsPanel.Name = "ChangableItemsPanel";
            this.ChangableItemsPanel.Size = new System.Drawing.Size(533, 364);
            this.ChangableItemsPanel.TabIndex = 31;
            // 
            // LocateObjectsBtn
            // 
            this.LocateObjectsBtn.Location = new System.Drawing.Point(1380, 811);
            this.LocateObjectsBtn.Name = "LocateObjectsBtn";
            this.LocateObjectsBtn.Size = new System.Drawing.Size(211, 30);
            this.LocateObjectsBtn.TabIndex = 32;
            this.LocateObjectsBtn.Text = "Try to find objects";
            this.LocateObjectsBtn.UseVisualStyleBackColor = true;
            this.LocateObjectsBtn.Visible = false;
            this.LocateObjectsBtn.Click += new System.EventHandler(this.LocateObjectsBtn_Click);
            // 
            // ObjectsSizeLabel
            // 
            this.ObjectsSizeLabel.AutoSize = true;
            this.ObjectsSizeLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.ObjectsSizeLabel.Location = new System.Drawing.Point(1677, 380);
            this.ObjectsSizeLabel.Name = "ObjectsSizeLabel";
            this.ObjectsSizeLabel.Size = new System.Drawing.Size(30, 17);
            this.ObjectsSizeLabel.TabIndex = 33;
            this.ObjectsSizeLabel.Text = "text";
            this.ObjectsSizeLabel.Click += new System.EventHandler(this.ObjectsSizeLabel_Click);
            // 
            // GaussBtn
            // 
            this.GaussBtn.Location = new System.Drawing.Point(1383, 770);
            this.GaussBtn.Name = "GaussBtn";
            this.GaussBtn.Size = new System.Drawing.Size(208, 35);
            this.GaussBtn.TabIndex = 34;
            this.GaussBtn.Text = "GaussianFilter";
            this.GaussBtn.UseVisualStyleBackColor = true;
            this.GaussBtn.Visible = false;
            this.GaussBtn.Click += new System.EventHandler(this.GaussBtn_Click);
            // 
            // SerializationBtn
            // 
            this.SerializationBtn.Location = new System.Drawing.Point(1383, 724);
            this.SerializationBtn.Name = "SerializationBtn";
            this.SerializationBtn.Size = new System.Drawing.Size(208, 40);
            this.SerializationBtn.TabIndex = 35;
            this.SerializationBtn.Text = "Serialize frame to file";
            this.SerializationBtn.UseVisualStyleBackColor = true;
            this.SerializationBtn.Click += new System.EventHandler(this.SerializationBtn_Click);
            // 
            // PlaneTrackBtn
            // 
            this.PlaneTrackBtn.Location = new System.Drawing.Point(1383, 678);
            this.PlaneTrackBtn.Name = "PlaneTrackBtn";
            this.PlaneTrackBtn.Size = new System.Drawing.Size(208, 40);
            this.PlaneTrackBtn.TabIndex = 36;
            this.PlaneTrackBtn.Text = "Plane track enabled";
            this.PlaneTrackBtn.UseVisualStyleBackColor = true;
            this.PlaneTrackBtn.Click += new System.EventHandler(this.PlaneTrackBtn_Click);
            // 
            // richTextBox1
            // 
            this.richTextBox1.Location = new System.Drawing.Point(1680, 422);
            this.richTextBox1.Name = "richTextBox1";
            this.richTextBox1.Size = new System.Drawing.Size(232, 316);
            this.richTextBox1.TabIndex = 40;
            this.richTextBox1.Text = "";
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(1680, 745);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 41;
            this.button1.Text = "reset";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoScroll = true;
            this.ClientSize = new System.Drawing.Size(1924, 977);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.richTextBox1);
            this.Controls.Add(this.PlaneTrackBtn);
            this.Controls.Add(this.SerializationBtn);
            this.Controls.Add(this.GaussBtn);
            this.Controls.Add(this.ObjectsSizeLabel);
            this.Controls.Add(this.LocateObjectsBtn);
            this.Controls.Add(this.ChangableItemsPanel);
            this.Controls.Add(this.availabilityLabel);
            this.Controls.Add(this.VykreslitTrojuhelnikyBtn);
            this.Controls.Add(this.RekalibraceBtn);
            this.Controls.Add(this.FPSLabel);
            this.Controls.Add(this.FrameCountLabel);
            this.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.Name = "Form1";
            this.Text = "Form1";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

		}

		#endregion
        private System.Windows.Forms.Label FPSLabel;
		private System.Windows.Forms.Button RekalibraceBtn;
        private System.Windows.Forms.Button VykreslitTrojuhelnikyBtn;
        private System.Windows.Forms.Label FrameCountLabel;
        private System.Windows.Forms.Label availabilityLabel;
        private System.Windows.Forms.Panel ChangableItemsPanel;
        private System.Windows.Forms.Button LocateObjectsBtn;
        private System.Windows.Forms.Label ObjectsSizeLabel;
        private System.Windows.Forms.Button GaussBtn;
        private System.Windows.Forms.Button SerializationBtn;
        private System.Windows.Forms.Button PlaneTrackBtn;
        private System.Windows.Forms.RichTextBox richTextBox1;
        private System.Windows.Forms.Button button1;
    }
}

