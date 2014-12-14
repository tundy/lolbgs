namespace lolbgs
{
    partial class Options
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
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.Duplicates = new System.Windows.Forms.Button();
            this.Cancel = new System.Windows.Forms.Button();
            this.About = new System.Windows.Forms.Button();
            this.IgnoreList = new System.Windows.Forms.Button();
            this.Allowed = new System.Windows.Forms.Button();
            this.tableLayoutPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 1;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Controls.Add(this.Duplicates, 0, 3);
            this.tableLayoutPanel1.Controls.Add(this.Cancel, 0, 4);
            this.tableLayoutPanel1.Controls.Add(this.About, 0, 2);
            this.tableLayoutPanel1.Controls.Add(this.IgnoreList, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.Allowed, 0, 0);
            this.tableLayoutPanel1.Location = new System.Drawing.Point(12, 12);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 6;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 35F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 35F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 35F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 35F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 35F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(210, 187);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // Duplicates
            // 
            this.Duplicates.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.Duplicates.Location = new System.Drawing.Point(5, 110);
            this.Duplicates.Margin = new System.Windows.Forms.Padding(5);
            this.Duplicates.Name = "Duplicates";
            this.Duplicates.Size = new System.Drawing.Size(200, 25);
            this.Duplicates.TabIndex = 4;
            this.Duplicates.Text = "Check for Duplicates";
            this.Duplicates.UseVisualStyleBackColor = true;
            this.Duplicates.Click += new System.EventHandler(this.Duplicates_Click);
            // 
            // Cancel
            // 
            this.Cancel.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.Cancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.Cancel.Location = new System.Drawing.Point(5, 145);
            this.Cancel.Margin = new System.Windows.Forms.Padding(5);
            this.Cancel.Name = "Cancel";
            this.Cancel.Size = new System.Drawing.Size(200, 25);
            this.Cancel.TabIndex = 0;
            this.Cancel.Text = "Close";
            this.Cancel.UseVisualStyleBackColor = true;
            this.Cancel.Click += new System.EventHandler(this.Close_Click);
            // 
            // About
            // 
            this.About.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.About.Location = new System.Drawing.Point(5, 75);
            this.About.Margin = new System.Windows.Forms.Padding(5);
            this.About.Name = "About";
            this.About.Size = new System.Drawing.Size(200, 25);
            this.About.TabIndex = 1;
            this.About.Text = "About";
            this.About.UseVisualStyleBackColor = true;
            this.About.Click += new System.EventHandler(this.About_Click);
            // 
            // IgnoreList
            // 
            this.IgnoreList.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.IgnoreList.Location = new System.Drawing.Point(5, 40);
            this.IgnoreList.Margin = new System.Windows.Forms.Padding(5);
            this.IgnoreList.Name = "IgnoreList";
            this.IgnoreList.Size = new System.Drawing.Size(200, 25);
            this.IgnoreList.TabIndex = 2;
            this.IgnoreList.Text = "Ignore List";
            this.IgnoreList.UseVisualStyleBackColor = true;
            this.IgnoreList.Click += new System.EventHandler(this.IgnoreList_Click);
            // 
            // Allowed
            // 
            this.Allowed.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.Allowed.Location = new System.Drawing.Point(5, 5);
            this.Allowed.Margin = new System.Windows.Forms.Padding(5);
            this.Allowed.Name = "Allowed";
            this.Allowed.Size = new System.Drawing.Size(200, 25);
            this.Allowed.TabIndex = 3;
            this.Allowed.Text = "Allowed Champions";
            this.Allowed.UseVisualStyleBackColor = true;
            this.Allowed.Click += new System.EventHandler(this.Allowed_Click);
            // 
            // Options
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.Cancel;
            this.ClientSize = new System.Drawing.Size(234, 211);
            this.Controls.Add(this.tableLayoutPanel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Options";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Options";
            this.tableLayoutPanel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Button Cancel;
        private System.Windows.Forms.Button About;
        private System.Windows.Forms.Button IgnoreList;
        private System.Windows.Forms.Button Allowed;
        private System.Windows.Forms.Button Duplicates;
    }
}