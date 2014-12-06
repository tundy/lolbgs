namespace lolbgs
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
            this.LeagueFolderLabel = new System.Windows.Forms.Label();
            this.LeagueFolderLocation = new System.Windows.Forms.TextBox();
            this.BrowseLeagueFolder = new System.Windows.Forms.Button();
            this.DestinationFolder = new System.Windows.Forms.Label();
            this.DestinationFolderLocation = new System.Windows.Forms.TextBox();
            this.BrowseDestinationFolder = new System.Windows.Forms.Button();
            this.Output = new System.Windows.Forms.TextBox();
            this.ProgressBar = new System.Windows.Forms.ProgressBar();
            this.Export = new System.Windows.Forms.Button();
            this.Options = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // LeagueFolderLabel
            // 
            this.LeagueFolderLabel.AutoSize = true;
            this.LeagueFolderLabel.Location = new System.Drawing.Point(12, 12);
            this.LeagueFolderLabel.Margin = new System.Windows.Forms.Padding(3);
            this.LeagueFolderLabel.Name = "LeagueFolderLabel";
            this.LeagueFolderLabel.Size = new System.Drawing.Size(178, 13);
            this.LeagueFolderLabel.TabIndex = 0;
            this.LeagueFolderLabel.Text = "League of Legends Folder Location:\r";
            // 
            // LeagueFolderLocation
            // 
            this.LeagueFolderLocation.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.LeagueFolderLocation.Location = new System.Drawing.Point(12, 31);
            this.LeagueFolderLocation.Name = "LeagueFolderLocation";
            this.LeagueFolderLocation.Size = new System.Drawing.Size(519, 20);
            this.LeagueFolderLocation.TabIndex = 1;
            this.LeagueFolderLocation.Text = global::lolbgs.Properties.Settings.Default.LeagueFolder;
            // 
            // BrowseLeagueFolder
            // 
            this.BrowseLeagueFolder.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.BrowseLeagueFolder.Location = new System.Drawing.Point(537, 31);
            this.BrowseLeagueFolder.Name = "BrowseLeagueFolder";
            this.BrowseLeagueFolder.Size = new System.Drawing.Size(75, 20);
            this.BrowseLeagueFolder.TabIndex = 2;
            this.BrowseLeagueFolder.Text = "Browse";
            this.BrowseLeagueFolder.UseVisualStyleBackColor = true;
            this.BrowseLeagueFolder.Click += new System.EventHandler(this.LeagueFolderBrowse_Click);
            // 
            // DestinationFolder
            // 
            this.DestinationFolder.AutoSize = true;
            this.DestinationFolder.Location = new System.Drawing.Point(12, 57);
            this.DestinationFolder.Margin = new System.Windows.Forms.Padding(3);
            this.DestinationFolder.Name = "DestinationFolder";
            this.DestinationFolder.Size = new System.Drawing.Size(95, 13);
            this.DestinationFolder.TabIndex = 3;
            this.DestinationFolder.Text = "Destination Folder:";
            // 
            // DestinationFolderLocation
            // 
            this.DestinationFolderLocation.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.DestinationFolderLocation.Location = new System.Drawing.Point(12, 76);
            this.DestinationFolderLocation.Name = "DestinationFolderLocation";
            this.DestinationFolderLocation.Size = new System.Drawing.Size(519, 20);
            this.DestinationFolderLocation.TabIndex = 4;
            this.DestinationFolderLocation.Text = global::lolbgs.Properties.Settings.Default.DestinationFolder;
            // 
            // BrowseDestinationFolder
            // 
            this.BrowseDestinationFolder.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.BrowseDestinationFolder.Location = new System.Drawing.Point(537, 76);
            this.BrowseDestinationFolder.Name = "BrowseDestinationFolder";
            this.BrowseDestinationFolder.Size = new System.Drawing.Size(75, 20);
            this.BrowseDestinationFolder.TabIndex = 5;
            this.BrowseDestinationFolder.Text = "Browse";
            this.BrowseDestinationFolder.UseVisualStyleBackColor = true;
            this.BrowseDestinationFolder.Click += new System.EventHandler(this.BrowseDestinationFolder_Click);
            // 
            // Output
            // 
            this.Output.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.Output.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Output.Location = new System.Drawing.Point(12, 102);
            this.Output.Multiline = true;
            this.Output.Name = "Output";
            this.Output.ReadOnly = true;
            this.Output.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.Output.Size = new System.Drawing.Size(600, 299);
            this.Output.TabIndex = 6;
            // 
            // ProgressBar
            // 
            this.ProgressBar.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.ProgressBar.Location = new System.Drawing.Point(12, 407);
            this.ProgressBar.Name = "ProgressBar";
            this.ProgressBar.Size = new System.Drawing.Size(446, 23);
            this.ProgressBar.TabIndex = 7;
            // 
            // Export
            // 
            this.Export.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.Export.Location = new System.Drawing.Point(551, 407);
            this.Export.Name = "Export";
            this.Export.Size = new System.Drawing.Size(61, 23);
            this.Export.TabIndex = 8;
            this.Export.Text = "Export";
            this.Export.UseVisualStyleBackColor = true;
            this.Export.Click += new System.EventHandler(this.Export_Click);
            // 
            // Options
            // 
            this.Options.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.Options.Location = new System.Drawing.Point(464, 407);
            this.Options.Name = "Options";
            this.Options.Size = new System.Drawing.Size(81, 23);
            this.Options.TabIndex = 9;
            this.Options.Text = "More Options";
            this.Options.UseVisualStyleBackColor = true;
            this.Options.Click += new System.EventHandler(this.Options_Click);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(624, 442);
            this.Controls.Add(this.Options);
            this.Controls.Add(this.Export);
            this.Controls.Add(this.ProgressBar);
            this.Controls.Add(this.Output);
            this.Controls.Add(this.BrowseDestinationFolder);
            this.Controls.Add(this.DestinationFolderLocation);
            this.Controls.Add(this.DestinationFolder);
            this.Controls.Add(this.LeagueFolderLabel);
            this.Controls.Add(this.BrowseLeagueFolder);
            this.Controls.Add(this.LeagueFolderLocation);
            this.Icon = global::lolbgs.Properties.Resources._3xhumed_League_of_Legends;
            this.MinimumSize = new System.Drawing.Size(640, 480);
            this.Name = "MainForm";
            this.Text = "League Of Legends Backgrounds Exporter";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label LeagueFolderLabel;
        private System.Windows.Forms.TextBox LeagueFolderLocation;
        private System.Windows.Forms.Button BrowseLeagueFolder;
        private System.Windows.Forms.Label DestinationFolder;
        private System.Windows.Forms.TextBox DestinationFolderLocation;
        private System.Windows.Forms.Button BrowseDestinationFolder;
        private System.Windows.Forms.TextBox Output;
        private System.Windows.Forms.ProgressBar ProgressBar;
        private System.Windows.Forms.Button Export;
        private System.Windows.Forms.Button Options;
    }
}

