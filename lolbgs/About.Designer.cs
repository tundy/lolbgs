namespace lolbgs
{
    partial class About
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
            this.Contact = new System.Windows.Forms.LinkLabel();
            this.label1 = new System.Windows.Forms.Label();
            this.Version = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.Author = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // Contact
            // 
            this.Contact.AutoSize = true;
            this.Contact.Location = new System.Drawing.Point(31, 55);
            this.Contact.Name = "Contact";
            this.Contact.Size = new System.Drawing.Size(62, 13);
            this.Contact.TabIndex = 0;
            this.Contact.TabStop = true;
            this.Contact.Text = "Contact Me";
            this.Contact.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.Contact_LinkClicked);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(45, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Version:";
            // 
            // Version
            // 
            this.Version.Location = new System.Drawing.Point(63, 6);
            this.Version.Name = "Version";
            this.Version.ReadOnly = true;
            this.Version.Size = new System.Drawing.Size(50, 20);
            this.Version.TabIndex = 2;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(16, 35);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(41, 13);
            this.label2.TabIndex = 3;
            this.label2.Text = "Author:";
            // 
            // Author
            // 
            this.Author.Location = new System.Drawing.Point(63, 32);
            this.Author.Name = "Author";
            this.Author.ReadOnly = true;
            this.Author.Size = new System.Drawing.Size(50, 20);
            this.Author.TabIndex = 4;
            this.Author.Text = "Tundy";
            // 
            // About
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(129, 77);
            this.Controls.Add(this.Author);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.Version);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.Contact);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "About";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "About";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.LinkLabel Contact;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox Version;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox Author;
    }
}