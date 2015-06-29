using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace LeagueBackgrounds
{
    public partial class Champs : Form
    {
        public Champs()
        {
            InitializeComponent();
            Shown += Champs_Shown;
            Load += Champs_Load;
        }

        void Champs_Load(object sender, EventArgs e)
        {
            Text += @"      [LOADING...]";
            Cursor.Current = Cursors.WaitCursor;
            string source;
            try
            {
                source = Static.GetRadPath();
            }
            catch (DirectoryNotFoundException)
            {
                return;
            }
            if (string.IsNullOrEmpty(source)) return;
            var images = Directory.GetFiles(source, "*.png");
            const string pattern = "(.+)_[Ss]quare_0\\.png";

            var temp = Static.GetChampsList();

            foreach (var image in images)
            {
                // ReSharper disable once AssignNullToNotNullAttribute
                var match = Regex.Match(Path.GetFileName(image), pattern);
                if (!match.Success) continue;
                    if (File.Exists(source + "\\" + match.Groups[1].Value + "_0.jpg"))
                        ChampsPanel.Controls.Add(new Picture
                        {
                            Width = 96,
                            Height = 96,
                            SizeMode = PictureBoxSizeMode.StretchImage,
                            Name = match.Groups[1].Value,
                            Margin = new Padding(8),
                            BorderStyle = BorderStyle.Fixed3D,
                            Cursor = Cursors.Hand,
                            Image = temp.Contains(match.Groups[1].Value) ? MakeGrayscale(new Bitmap(source + match.Value)) : Image.FromFile(source + match.Value)
                        });
            }
        }

        void Champs_Shown(object sender, EventArgs e)
        {
            foreach (var control in ChampsPanel.Controls.OfType<Picture>())
                control.PictureBox.Click += img_Click;
            if (Text.EndsWith("      [LOADING...]"))
                Text = Text.Substring(0, Text.LastIndexOf("      [LOADING...]", StringComparison.Ordinal));
            Cursor.Current = Cursors.Default;
        }

        static void img_Click(object sender, EventArgs e)
        {
            var temp = Static.GetChampsList();
            var img = sender as PictureBox;
            if (img == null) return;
            if (temp.Contains(img.Name, StringComparer.OrdinalIgnoreCase))
            {
                if (img.Name == "Corki")
                    temp.Remove("Corky");
                temp.Remove(img.Name);
                img.Image = Image.FromFile(Static.GetRadPath() + img.Name + "_Square_0.png");
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
            ignore = ignore.Trim();
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
            Cursor.Current = Cursors.WaitCursor;
            Properties.Settings.Default.Champs = null;
            Properties.Settings.Default.Save();
            var source = Static.GetRadPath();
            foreach (var control in ChampsPanel.Controls.OfType<Picture>())
                control.PictureBox.Image = Image.FromFile(source + control.Name + "_Square_0.png");
            Cursor.Current = Cursors.Default;
        }

        private void None_Click(object sender, EventArgs e)
        {
            Cursor.Current = Cursors.WaitCursor;
            var temp = new List<String>();
            foreach (var control in ChampsPanel.Controls.OfType<Picture>())
            {
                if (control.Name == "Corki")
                    temp.Add("Corky");
                temp.Add(control.Name);
                control.PictureBox.Image = MakeGrayscale(new Bitmap(control.PictureBox.Image));
            }
            var ignore = string.Empty;
            foreach (var r in temp)
            {
                if (!string.IsNullOrEmpty(r))
                    ignore += r + "\r\n";
            }
            ignore = ignore.Trim();
            if (ignore.Length == 0)
                ignore = null;
            Properties.Settings.Default.Champs = ignore;
            Properties.Settings.Default.Save();
            Cursor.Current = Cursors.Default;
        }

        private void Invert_Click(object sender, EventArgs e)
        {
            Cursor.Current = Cursors.WaitCursor;
            var temp = Static.GetChampsList();
            foreach (var img in ChampsPanel.Controls.OfType<Picture>())
            {
                if (temp.Contains(img.Name, StringComparer.OrdinalIgnoreCase))
                {
                    if (img.Name == "Corki")
                        temp.Remove("Corky");
                    temp.Remove(img.Name);
                    img.PictureBox.Image = Image.FromFile(Static.GetRadPath() + img.Name + "_Square_0.png");
                }
                else
                {
                    if (img.Name == "Corki")
                        temp.Add("Corky");
                    temp.Add(img.Name);
                    img.PictureBox.Image = MakeGrayscale(new Bitmap(img.PictureBox.Image));
                }

            }
            var ignore = string.Empty;
            foreach (var r in temp)
            {
                if (!string.IsNullOrEmpty(r))
                    ignore += r + "\r\n";
            }
            ignore = ignore.Trim();
            if (ignore.Length == 0)
                ignore = null;
            Properties.Settings.Default.Champs = ignore;
            Properties.Settings.Default.Save();
            Cursor.Current = Cursors.Default;
        }

        private void Search_TextChanged(object sender, EventArgs e)
        {
            //SuspendLayout();
            ChampsPanel.SuspendLayout();
            if (string.IsNullOrEmpty(Search.Text))
                foreach (var img in ChampsPanel.Controls.OfType<Picture>())
                    img.Visible = true;
            else
                foreach (var img in ChampsPanel.Controls.OfType<Picture>().Reverse())   // Revesre is faster 'cos it don't need to redraw so often
                    img.Visible = img.Name.IndexOf(Regex.Replace(Search.Text, @"[']+", ""), StringComparison.CurrentCultureIgnoreCase) != -1;   // Ignore Apostrophe
            ChampsPanel.ResumeLayout();
            //ResumeLayout();
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