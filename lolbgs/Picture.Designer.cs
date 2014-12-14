namespace lolbgs
{
    partial class Picture
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this._pictureBox = new System.Windows.Forms.PictureBox();
            this._label = new System.Windows.Forms.Label();
            this._flowLayoutPanel = new System.Windows.Forms.FlowLayoutPanel();
            ((System.ComponentModel.ISupportInitialize)(this._pictureBox)).BeginInit();
            this._flowLayoutPanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // _pictureBox
            // 
            this._pictureBox.Anchor = System.Windows.Forms.AnchorStyles.None;
            this._pictureBox.Location = new System.Drawing.Point(0, 0);
            this._pictureBox.Margin = new System.Windows.Forms.Padding(0);
            this._pictureBox.Name = "_pictureBox";
            this._pictureBox.Size = new System.Drawing.Size(41, 50);
            this._pictureBox.TabIndex = 0;
            this._pictureBox.TabStop = false;
            //this._pictureBox.Click += new System.EventHandler(this.OnClick);
            // 
            // _label
            // 
            this._label.AutoSize = true;
            this._label.Dock = System.Windows.Forms.DockStyle.Fill;
            this._label.Location = new System.Drawing.Point(3, 50);
            this._label.Name = "_label";
            this._label.Size = new System.Drawing.Size(35, 13);
            this._label.TabIndex = 1;
            this._label.Text = "label";
            this._label.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // _flowLayoutPanel
            // 
            this._flowLayoutPanel.AutoSize = true;
            this._flowLayoutPanel.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this._flowLayoutPanel.Controls.Add(this._pictureBox);
            this._flowLayoutPanel.Controls.Add(this._label);
            this._flowLayoutPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this._flowLayoutPanel.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
            this._flowLayoutPanel.Location = new System.Drawing.Point(0, 0);
            this._flowLayoutPanel.Name = "_flowLayoutPanel";
            this._flowLayoutPanel.Size = new System.Drawing.Size(41, 63);
            this._flowLayoutPanel.TabIndex = 2;
            // 
            // Picture
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.Controls.Add(this._flowLayoutPanel);
            this.Name = "Picture";
            this.Size = new System.Drawing.Size(41, 63);
            ((System.ComponentModel.ISupportInitialize)(this._pictureBox)).EndInit();
            this._flowLayoutPanel.ResumeLayout(false);
            this._flowLayoutPanel.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        #endregion

        private System.Windows.Forms.PictureBox _pictureBox;
        private System.Windows.Forms.Label _label;
        private System.Windows.Forms.FlowLayoutPanel _flowLayoutPanel;
    }
}
