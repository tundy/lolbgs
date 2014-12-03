﻿namespace lolbgs
{
    partial class Champs
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
            this.label1 = new System.Windows.Forms.Label();
            this.All = new System.Windows.Forms.Label();
            this.None = new System.Windows.Forms.Label();
            this.Cancel = new System.Windows.Forms.Button();
            this.ChampsPanel = new System.Windows.Forms.Panel();
            this.Invert = new System.Windows.Forms.Label();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 419);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(40, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Select:";
            // 
            // All
            // 
            this.All.AutoSize = true;
            this.All.ForeColor = System.Drawing.Color.Blue;
            this.All.Location = new System.Drawing.Point(58, 419);
            this.All.Name = "All";
            this.All.Size = new System.Drawing.Size(18, 13);
            this.All.TabIndex = 1;
            this.All.Text = "All";
            this.All.Click += new System.EventHandler(this.All_Click);
            // 
            // None
            // 
            this.None.AutoSize = true;
            this.None.ForeColor = System.Drawing.Color.Blue;
            this.None.Location = new System.Drawing.Point(82, 419);
            this.None.Name = "None";
            this.None.Size = new System.Drawing.Size(33, 13);
            this.None.TabIndex = 2;
            this.None.Text = "None";
            this.None.Click += new System.EventHandler(this.None_Click);
            // 
            // Cancel
            // 
            this.Cancel.Location = new System.Drawing.Point(555, 414);
            this.Cancel.Name = "Cancel";
            this.Cancel.Size = new System.Drawing.Size(57, 23);
            this.Cancel.TabIndex = 3;
            this.Cancel.Text = "Close";
            this.Cancel.UseVisualStyleBackColor = true;
            this.Cancel.Click += new System.EventHandler(this.Close_Click);
            // 
            // ChampsPanel
            // 
            this.ChampsPanel.AutoScroll = true;
            this.ChampsPanel.BackColor = System.Drawing.SystemColors.Control;
            this.ChampsPanel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.ChampsPanel.Location = new System.Drawing.Point(15, 12);
            this.ChampsPanel.Name = "ChampsPanel";
            this.ChampsPanel.Size = new System.Drawing.Size(597, 396);
            this.ChampsPanel.TabIndex = 5;
            // 
            // Invert
            // 
            this.Invert.AutoSize = true;
            this.Invert.ForeColor = System.Drawing.Color.Blue;
            this.Invert.Location = new System.Drawing.Point(121, 419);
            this.Invert.Name = "Invert";
            this.Invert.Size = new System.Drawing.Size(34, 13);
            this.Invert.TabIndex = 6;
            this.Invert.Text = "Invert";
            this.Invert.Click += new System.EventHandler(this.Invert_Click);
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(299, 417);
            this.textBox1.Name = "textBox1";
            this.textBox1.ReadOnly = true;
            this.textBox1.Size = new System.Drawing.Size(100, 20);
            this.textBox1.TabIndex = 7;
            this.textBox1.Text = "    WIP ...";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(249, 420);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(44, 13);
            this.label2.TabIndex = 8;
            this.label2.Text = "Search:";
            // 
            // Champs
            // 
            this.AcceptButton = this.Cancel;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(624, 441);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.Invert);
            this.Controls.Add(this.ChampsPanel);
            this.Controls.Add(this.Cancel);
            this.Controls.Add(this.None);
            this.Controls.Add(this.All);
            this.Controls.Add(this.label1);
            this.Shown += Champs_Shown;
            this.Load += Champs_Load;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "Champs";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Allowed Champions";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label All;
        private System.Windows.Forms.Label None;
        private System.Windows.Forms.Button Cancel;
        private System.Windows.Forms.Panel ChampsPanel;
        private System.Windows.Forms.Label Invert;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Label label2;
    }
}