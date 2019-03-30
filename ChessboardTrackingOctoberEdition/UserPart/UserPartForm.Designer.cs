namespace ChessboardTrackingOctoberEdition
{
    partial class UserPartForm
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
            this.GameStatePictureBox = new System.Windows.Forms.PictureBox();
            this.NewGameButton = new System.Windows.Forms.Button();
            this.LoadGameButton = new System.Windows.Forms.Button();
            this.SaveGameButton = new System.Windows.Forms.Button();
            this.StartTrackingButton = new System.Windows.Forms.Button();
            this.StopTrackingButton = new System.Windows.Forms.Button();
            this.GameplayLogListBox = new System.Windows.Forms.ListBox();
            this.UserNotificationsListBox = new System.Windows.Forms.ListBox();
            ((System.ComponentModel.ISupportInitialize)(this.GameStatePictureBox)).BeginInit();
            this.SuspendLayout();
            // 
            // GameStatePictureBox
            // 
            this.GameStatePictureBox.Location = new System.Drawing.Point(13, 13);
            this.GameStatePictureBox.Name = "GameStatePictureBox";
            this.GameStatePictureBox.Size = new System.Drawing.Size(334, 329);
            this.GameStatePictureBox.TabIndex = 0;
            this.GameStatePictureBox.TabStop = false;
            // 
            // NewGameButton
            // 
            this.NewGameButton.Location = new System.Drawing.Point(353, 13);
            this.NewGameButton.Name = "NewGameButton";
            this.NewGameButton.Size = new System.Drawing.Size(131, 61);
            this.NewGameButton.TabIndex = 1;
            this.NewGameButton.Text = "New game";
            this.NewGameButton.UseVisualStyleBackColor = true;
            this.NewGameButton.Click += new System.EventHandler(this.NewGameButton_Click);
            // 
            // LoadGameButton
            // 
            this.LoadGameButton.Enabled = false;
            this.LoadGameButton.Location = new System.Drawing.Point(353, 80);
            this.LoadGameButton.Name = "LoadGameButton";
            this.LoadGameButton.Size = new System.Drawing.Size(131, 61);
            this.LoadGameButton.TabIndex = 2;
            this.LoadGameButton.Text = "Load game";
            this.LoadGameButton.UseVisualStyleBackColor = true;
            // 
            // SaveGameButton
            // 
            this.SaveGameButton.Enabled = false;
            this.SaveGameButton.Location = new System.Drawing.Point(352, 147);
            this.SaveGameButton.Name = "SaveGameButton";
            this.SaveGameButton.Size = new System.Drawing.Size(131, 61);
            this.SaveGameButton.TabIndex = 3;
            this.SaveGameButton.Text = "Save game";
            this.SaveGameButton.UseVisualStyleBackColor = true;
            // 
            // StartTrackingButton
            // 
            this.StartTrackingButton.Location = new System.Drawing.Point(353, 214);
            this.StartTrackingButton.Name = "StartTrackingButton";
            this.StartTrackingButton.Size = new System.Drawing.Size(131, 61);
            this.StartTrackingButton.TabIndex = 4;
            this.StartTrackingButton.Text = "Start tracking";
            this.StartTrackingButton.UseVisualStyleBackColor = true;
            this.StartTrackingButton.Click += new System.EventHandler(this.StartTrackingButton_Click);
            // 
            // StopTrackingButton
            // 
            this.StopTrackingButton.Location = new System.Drawing.Point(353, 281);
            this.StopTrackingButton.Name = "StopTrackingButton";
            this.StopTrackingButton.Size = new System.Drawing.Size(131, 61);
            this.StopTrackingButton.TabIndex = 5;
            this.StopTrackingButton.Text = "Stop tracking";
            this.StopTrackingButton.UseVisualStyleBackColor = true;
            this.StopTrackingButton.Click += new System.EventHandler(this.StopTrackingButton_Click);
            // 
            // GameplayLogListBox
            // 
            this.GameplayLogListBox.FormattingEnabled = true;
            this.GameplayLogListBox.ItemHeight = 16;
            this.GameplayLogListBox.Location = new System.Drawing.Point(490, 13);
            this.GameplayLogListBox.Name = "GameplayLogListBox";
            this.GameplayLogListBox.Size = new System.Drawing.Size(201, 324);
            this.GameplayLogListBox.TabIndex = 6;
            // 
            // UserNotificationsListBox
            // 
            this.UserNotificationsListBox.FormattingEnabled = true;
            this.UserNotificationsListBox.ItemHeight = 16;
            this.UserNotificationsListBox.Location = new System.Drawing.Point(698, 13);
            this.UserNotificationsListBox.Name = "UserNotificationsListBox";
            this.UserNotificationsListBox.Size = new System.Drawing.Size(203, 100);
            this.UserNotificationsListBox.TabIndex = 7;
            // 
            // UserPartForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(907, 351);
            this.Controls.Add(this.UserNotificationsListBox);
            this.Controls.Add(this.GameplayLogListBox);
            this.Controls.Add(this.StopTrackingButton);
            this.Controls.Add(this.StartTrackingButton);
            this.Controls.Add(this.SaveGameButton);
            this.Controls.Add(this.LoadGameButton);
            this.Controls.Add(this.NewGameButton);
            this.Controls.Add(this.GameStatePictureBox);
            this.Name = "UserPartForm";
            this.Text = "Form1";
            ((System.ComponentModel.ISupportInitialize)(this.GameStatePictureBox)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.PictureBox GameStatePictureBox;
        private System.Windows.Forms.Button NewGameButton;
        private System.Windows.Forms.Button LoadGameButton;
        private System.Windows.Forms.Button SaveGameButton;
        private System.Windows.Forms.Button StartTrackingButton;
        private System.Windows.Forms.Button StopTrackingButton;
        private System.Windows.Forms.ListBox GameplayLogListBox;
        private System.Windows.Forms.ListBox UserNotificationsListBox;
    }
}

