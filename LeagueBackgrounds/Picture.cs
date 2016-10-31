using System.Drawing;
using System.Windows.Forms;

namespace LeagueBackgrounds
{
    public partial class Picture : UserControl
    {
        public Picture()
        {
            InitializeComponent();
        }

        public new string Name
        {
            get { return _label.Text; }
            set
            {
                PictureBox.Name = value;
                _label.Text = value;
            }
        }

        public Image Image
        {
            get { return PictureBox.Image; }
            set { PictureBox.Image = value; }
        }

        public new int Width
        {
            get { return PictureBox.Width; }
            set { PictureBox.Width = value; }
        }

        public new int Height
        {
            get { return PictureBox.Height; }
            set { PictureBox.Height = value; }
        }

        public new BorderStyle BorderStyle
        {
            get { return PictureBox.BorderStyle; }
            set { PictureBox.BorderStyle = value; }
        }

        public new Cursor Cursor
        {
            get { return PictureBox.Cursor; }
            set { PictureBox.Cursor = value; }
        }

        public PictureBoxSizeMode SizeMode
        {
            get { return PictureBox.SizeMode; }
            set { PictureBox.SizeMode = value; }
        }

        public PictureBox PictureBox { get; set; }
    }
}