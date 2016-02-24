namespace LeagueBackgrounds
{
    partial class MainForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.LeagueFolder_Label = new System.Windows.Forms.Label();
            this.DestinationFolder_Label = new System.Windows.Forms.Label();
            this.LeagueFolder_Button = new System.Windows.Forms.Button();
            this.DestinationFolder_Button = new System.Windows.Forms.Button();
            this.Output_TextBox = new System.Windows.Forms.TextBox();
            this.Export_Button = new System.Windows.Forms.Button();
            this.Options_Button = new System.Windows.Forms.Button();
            this.Output_ProgressBar = new System.Windows.Forms.ProgressBar();
            this.DestinationFolder_TextBox = new System.Windows.Forms.TextBox();
            this.LeagueFolder_TextBox = new System.Windows.Forms.TextBox();
            this.Cancel_Button = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // LeagueFolder_Label
            // 
            this.LeagueFolder_Label.AutoSize = true;
            this.LeagueFolder_Label.Location = new System.Drawing.Point(12, 9);
            this.LeagueFolder_Label.Name = "LeagueFolder_Label";
            this.LeagueFolder_Label.Size = new System.Drawing.Size(178, 13);
            this.LeagueFolder_Label.TabIndex = 0;
            this.LeagueFolder_Label.Text = "League of Legends Folder Location:";
            // 
            // DestinationFolder_Label
            // 
            this.DestinationFolder_Label.AutoSize = true;
            this.DestinationFolder_Label.Location = new System.Drawing.Point(12, 48);
            this.DestinationFolder_Label.Name = "DestinationFolder_Label";
            this.DestinationFolder_Label.Size = new System.Drawing.Size(95, 13);
            this.DestinationFolder_Label.TabIndex = 2;
            this.DestinationFolder_Label.Text = "Destination Folder:";
            // 
            // LeagueFolder_Button
            // 
            this.LeagueFolder_Button.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.LeagueFolder_Button.Location = new System.Drawing.Point(537, 25);
            this.LeagueFolder_Button.Name = "LeagueFolder_Button";
            this.LeagueFolder_Button.Size = new System.Drawing.Size(75, 20);
            this.LeagueFolder_Button.TabIndex = 4;
            this.LeagueFolder_Button.Text = "Browse";
            this.LeagueFolder_Button.UseVisualStyleBackColor = true;
            this.LeagueFolder_Button.Click += new System.EventHandler(this.LeagueFolder_Button_Click);
            // 
            // DestinationFolder_Button
            // 
            this.DestinationFolder_Button.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.DestinationFolder_Button.Location = new System.Drawing.Point(537, 64);
            this.DestinationFolder_Button.Name = "DestinationFolder_Button";
            this.DestinationFolder_Button.Size = new System.Drawing.Size(75, 20);
            this.DestinationFolder_Button.TabIndex = 5;
            this.DestinationFolder_Button.Text = "Browse";
            this.DestinationFolder_Button.UseVisualStyleBackColor = true;
            this.DestinationFolder_Button.Click += new System.EventHandler(this.DestinationFolder_Button_Click);
            // 
            // Output_TextBox
            // 
            this.Output_TextBox.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.Output_TextBox.Location = new System.Drawing.Point(12, 90);
            this.Output_TextBox.Multiline = true;
            this.Output_TextBox.Name = "Output_TextBox";
            this.Output_TextBox.ReadOnly = true;
            this.Output_TextBox.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.Output_TextBox.Size = new System.Drawing.Size(600, 191);
            this.Output_TextBox.TabIndex = 6;
            // 
            // Export_Button
            // 
            this.Export_Button.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.Export_Button.Location = new System.Drawing.Point(537, 287);
            this.Export_Button.Name = "Export_Button";
            this.Export_Button.Size = new System.Drawing.Size(75, 23);
            this.Export_Button.TabIndex = 7;
            this.Export_Button.Text = "Export";
            this.Export_Button.UseVisualStyleBackColor = true;
            this.Export_Button.Click += new System.EventHandler(this.Export_Button_Click);
            // 
            // Options_Button
            // 
            this.Options_Button.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.Options_Button.Location = new System.Drawing.Point(447, 287);
            this.Options_Button.Name = "Options_Button";
            this.Options_Button.Size = new System.Drawing.Size(84, 23);
            this.Options_Button.TabIndex = 8;
            this.Options_Button.Text = "More Options";
            this.Options_Button.UseVisualStyleBackColor = true;
            this.Options_Button.Click += new System.EventHandler(this.Options_Button_Click);
            // 
            // Output_ProgressBar
            // 
            this.Output_ProgressBar.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.Output_ProgressBar.Location = new System.Drawing.Point(12, 287);
            this.Output_ProgressBar.Name = "Output_ProgressBar";
            this.Output_ProgressBar.Size = new System.Drawing.Size(429, 23);
            this.Output_ProgressBar.TabIndex = 9;
            // 
            // DestinationFolder_TextBox
            // 
            this.DestinationFolder_TextBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.DestinationFolder_TextBox.Location = new System.Drawing.Point(12, 64);
            this.DestinationFolder_TextBox.Name = "DestinationFolder_TextBox";
            this.DestinationFolder_TextBox.Size = new System.Drawing.Size(519, 20);
            this.DestinationFolder_TextBox.TabIndex = 3;
            this.DestinationFolder_TextBox.Text = global::LeagueBackgrounds.Properties.Settings.Default.DestinationFolder;
            // 
            // LeagueFolder_TextBox
            // 
            this.LeagueFolder_TextBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.LeagueFolder_TextBox.Location = new System.Drawing.Point(12, 25);
            this.LeagueFolder_TextBox.Name = "LeagueFolder_TextBox";
            this.LeagueFolder_TextBox.Size = new System.Drawing.Size(519, 20);
            this.LeagueFolder_TextBox.TabIndex = 1;
            this.LeagueFolder_TextBox.Text = global::LeagueBackgrounds.Properties.Settings.Default.LeagueFolder;
            // 
            // Cancel_Button
            // 
            this.Cancel_Button.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.Cancel_Button.Location = new System.Drawing.Point(537, 287);
            this.Cancel_Button.Name = "Cancel_Button";
            this.Cancel_Button.Size = new System.Drawing.Size(75, 23);
            this.Cancel_Button.TabIndex = 10;
            this.Cancel_Button.Text = "Cancel";
            this.Cancel_Button.UseVisualStyleBackColor = true;
            this.Cancel_Button.Visible = false;
            this.Cancel_Button.Click += new System.EventHandler(this.Cancel_Button_Click);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(632, 330);
            this.Controls.Add(this.Cancel_Button);
            this.Controls.Add(this.Output_ProgressBar);
            this.Controls.Add(this.Options_Button);
            this.Controls.Add(this.Export_Button);
            this.Controls.Add(this.Output_TextBox);
            this.Controls.Add(this.DestinationFolder_Button);
            this.Controls.Add(this.LeagueFolder_Button);
            this.Controls.Add(this.DestinationFolder_TextBox);
            this.Controls.Add(this.DestinationFolder_Label);
            this.Controls.Add(this.LeagueFolder_TextBox);
            this.Controls.Add(this.LeagueFolder_Label);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MinimumSize = new System.Drawing.Size(640, 360);
            this.Name = "MainForm";
            this.Text = "League of Legends Backgrounds Exporter";
            this.Activated += new System.EventHandler(this.MainForm_Activated);
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MainForm_FormClosing);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label LeagueFolder_Label;
        private System.Windows.Forms.TextBox LeagueFolder_TextBox;
        private System.Windows.Forms.Label DestinationFolder_Label;
        private System.Windows.Forms.TextBox DestinationFolder_TextBox;
        private System.Windows.Forms.Button LeagueFolder_Button;
        private System.Windows.Forms.Button DestinationFolder_Button;
        private System.Windows.Forms.TextBox Output_TextBox;
        private System.Windows.Forms.Button Export_Button;
        private System.Windows.Forms.Button Options_Button;
        private System.Windows.Forms.ProgressBar Output_ProgressBar;
        private System.Windows.Forms.Button Cancel_Button;
    }
}

