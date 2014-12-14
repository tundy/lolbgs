using System;
using System.Drawing;
using System.Windows.Forms;

namespace lolbgs
{
    public partial class Picture : UserControl
    {
        public new string Name {
            get { return _label.Text; }
            set 
            {
                _pictureBox.Name = value;
                _label.Text = value;
            }
        }
        public Image Image
        {
            get { return _pictureBox.Image; }
            set { _pictureBox.Image = value; }
        }
        public new int Width
        {
            get { return _pictureBox.Width; }
            set { _pictureBox.Width = value; }
        }
        public new int Height
        {
            get { return _pictureBox.Height; }
            set { _pictureBox.Height = value; }
        }
        public new BorderStyle BorderStyle
        {
            get { return _pictureBox.BorderStyle; }
            set { _pictureBox.BorderStyle = value; }
        }
        public new Cursor Cursor
        {
            get { return _pictureBox.Cursor; }
            set { _pictureBox.Cursor = value; }
        }
        
        public PictureBoxSizeMode SizeMode
        {
            get { return _pictureBox.SizeMode; }
            set { _pictureBox.SizeMode = value; }
        }

        public PictureBox PictureBox
        {
            get { return _pictureBox; }
            set { _pictureBox = value; }
        }
       
        public Picture()
        {
            InitializeComponent();
        }
    }
}
