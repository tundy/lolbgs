using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace lolbgs
{
    public partial class Champs : Form
    {
        public Champs()
        {
            InitializeComponent();
        }

        void Champs_Load(object sender, EventArgs e)
        {
            Text += @"      [LOADING...]";
            Cursor.Current = Cursors.WaitCursor;

            string source;
            try
            {
                source = Settings.GetRadPath();
            }
            catch (DirectoryNotFoundException)
            {
                return;
            }
            if (string.IsNullOrEmpty(source)) return;
            var images = Directory.GetFiles(source, "*.png");
            const string pattern = "(.+)_[Ss]quare_0\\.png";

            var y = 0;
            var x = 0;
            var temp = Settings.GetChampsList();
            foreach (var image in images)
            {
                // ReSharper disable once AssignNullToNotNullAttribute
                var match = Regex.Match(Path.GetFileName(image), pattern);
                if (!match.Success) continue;
                var img = new PictureBox
                {
                    Location = new Point(x, y),
                    Width = 96,
                    Height = 96,
                    SizeMode = PictureBoxSizeMode.StretchImage,
                    Name = match.Groups[1].Value
                };
                img.Image = temp.Contains(img.Name) ? MakeGrayscale(new Bitmap(source + match.Value)) : Image.FromFile(source + match.Value);
                ChampsPanel.Controls.Add(img);
                x += 112;
                if ((x %= 560) == 0)
                    y += 112;
            }
        }

        void Champs_Shown(object sender, EventArgs e)
        {
            foreach (var control in ChampsPanel.Controls.OfType<PictureBox>())
                control.Click += img_Click;
            if (Text.EndsWith("      [LOADING...]"))
                Text = Text.Substring(0, Text.LastIndexOf("      [LOADING...]", StringComparison.Ordinal));
            Cursor.Current = Cursors.Default;
        }


        static void img_Click(object sender, EventArgs e)
        {
            var temp = Settings.GetChampsList();
            var img = sender as PictureBox;
            if (img == null) return;
            if (temp.Contains(img.Name, StringComparer.OrdinalIgnoreCase))
            {
                if (img.Name == "Corki")
                    temp.Remove("Corky");
                temp.Remove(img.Name);
                img.Image = Image.FromFile(Settings.GetRadPath() + img.Name + "_Square_0.png");
            }
            else
            {
                if (img.Name == "Corki")
                    temp.Add("Corky");
                temp.Add(img.Name);
                img.Image = MakeGrayscale(new Bitmap(img.Image));
            }
            
            var ignore = string.Empty;
            foreach (var r in temp)
            {
                if (!string.IsNullOrEmpty(r))
                    ignore += r + "\r\n";
            }
            ignore = ignore.Trim('\r', '\n');
            if (ignore.Length == 0)
                ignore = null;
            Properties.Settings.Default.Champs = ignore;
            Properties.Settings.Default.Save();
        }

        private void Close_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void All_Click(object sender, EventArgs e)
        {
            var temp = Settings.GetChampsList();
#if !DEBUG
            if (temp.Capacity > 75)
            {
                Properties.Settings.Default.Champs = null;
                Properties.Settings.Default.Save();
                ChampsPanel.Controls.Clear();
                Champs_Load(null, e);
                Champs_Shown(null, e);
            }
            else
            {
#endif
                Cursor.Current = Cursors.WaitCursor;
                foreach (var control in ChampsPanel.Controls.OfType<PictureBox>())
                {
                    if (temp.Contains(control.Name))
                        img_Click(control, e);
                }
                Cursor.Current = Cursors.Default;
#if !DEBUG
            }
#endif
        }

        private void None_Click(object sender, EventArgs e)
        {
            Cursor.Current = Cursors.WaitCursor;
            var temp = Settings.GetChampsList();
            foreach (var control in ChampsPanel.Controls.OfType<PictureBox>())
            {
                if (!temp.Contains(control.Name))
                    img_Click(control, e);
            }
            Cursor.Current = Cursors.Default;
        }

        private void Invert_Click(object sender, EventArgs e)
        {
            Cursor.Current = Cursors.WaitCursor;
            foreach (var control in ChampsPanel.Controls.OfType<PictureBox>())
            {
                img_Click(control, e);
            }
            Cursor.Current = Cursors.Default;
        }

        public static Bitmap MakeGrayscale(Bitmap original)
        {
            //create a blank bitmap the same size as original
            var newBitmap = new Bitmap(original.Width, original.Height);

            //get a graphics object from the new image
            var g = Graphics.FromImage(newBitmap);

            //create the grayscale ColorMatrix
            var colorMatrix = new ColorMatrix(
               new[]
               {
                 new[] {.3f, .3f, .3f, 0, 0},
                 new[] {.59f, .59f, .59f, 0, 0},
                 new[] {.11f, .11f, .11f, 0, 0},
                 new float[] {0, 0, 0, 1, 0},
                 new float[] {0, 0, 0, 0, 1}
               });

            //create some image attributes
            var attributes = new ImageAttributes();

            //set the color matrix attribute
            attributes.SetColorMatrix(colorMatrix);

            //draw the original image on the new image
            //using the grayscale color matrix
            g.DrawImage(original, new Rectangle(0, 0, original.Width, original.Height),
               0, 0, original.Width, original.Height, GraphicsUnit.Pixel, attributes);

            //dispose the Graphics object
            g.Dispose();
            return newBitmap;
        }
    }
}
