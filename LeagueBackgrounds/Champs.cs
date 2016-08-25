using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LeagueBackgrounds
{
    public partial class Champs : Form
    {
        public Champs()
        {
            InitializeComponent();
            Load += Champs_Load;
            Shown += Champs_Shown;
        }
        
        private const string Loading = @"      [LOADING...]";
        private void Champs_Load(object sender, EventArgs e)
        {
            Text += Loading;
            Cursor.Current = Cursors.WaitCursor;
            ChampsPanel.SuspendLayout();
            SuspendLayout();
            var source = Static.GetRadPath();
            if (string.IsNullOrEmpty(source)) return;
            var images = Directory.GetFiles(source, "*.png");
            const string pattern = "(.+)_[Ss]quare_0\\.png";

            var list = images.Select(image => Regex.Match(Path.GetFileName(image) ?? string.Empty, pattern))
                       .Where(match => match.Success)
                       .Where(match => File.Exists($@"{source}\{match.Groups[1].Value}_0.jpg")).ToList();
            var temp = Static.GetChampsList();
            var picturelist = new Picture[list.Count];
            // For loading pictures from disk
            Parallel.For(0, picturelist.Length, i =>
                {
                    picturelist[i] = new Picture
                    {
                        Width = 96,
                        Height = 96,
                        SizeMode = PictureBoxSizeMode.StretchImage,
                        Name = list[i].Groups[1].Value,
                        Margin = new Padding(8),
                        BorderStyle = BorderStyle.Fixed3D,
                        Cursor = Cursors.Hand,
                        Tag = list[i].Value,
                        Visible = false,
                        Image = temp.Contains(list[i].Groups[1].Value)
                            ? MakeGrayscale(new Bitmap(source + list[i].Value))
                            : Image.FromFile(source + list[i].Value)
                    };
                }
                );
            for (var j = 0; j < picturelist.Length; j++)
            {
                if (picturelist[j] == null) // slow loading ?
                {
                    j--;
                    Thread.Sleep(100);
                }
                else
                {
                    ChampsPanel.Controls.Add(picturelist[j]);
                }
            }
            
            ResumeLayout();
            ChampsPanel.ResumeLayout();
            GC.Collect();
        }
        
        private void Champs_Shown(object sender, EventArgs e)
        {
            ChampsPanel.SuspendLayout();
            SuspendLayout();
            foreach (var control in ChampsPanel.Controls.OfType<Picture>())
            {
                control.PictureBox.Click += img_Click;
                control.Visible = true;
            }
            if (Text.EndsWith(Loading))
                Text = Text.Substring(0, Text.LastIndexOf(Loading, StringComparison.Ordinal));
            ResumeLayout();
            ChampsPanel.ResumeLayout();
            Cursor.Current = Cursors.Default;
            GC.Collect();
        }

        private static void img_Click(object sender, EventArgs e)
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

            var ignore = temp.Where(r => !string.IsNullOrEmpty(r)).Aggregate(string.Empty, (current, r) => current + (r + "\r\n"));
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
            var temp = new List<string>();
            foreach (var control in ChampsPanel.Controls.OfType<Picture>())
            {
                if (control.Name == "Corki")
                    temp.Add("Corky");
                temp.Add(control.Name);
                control.PictureBox.Image = MakeGrayscale(new Bitmap(control.PictureBox.Image));
            }
            var ignore = temp.Where(r => !string.IsNullOrEmpty(r)).Aggregate(string.Empty, (current, r) => current + (r + "\r\n"));
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
            var ignore = temp.Where(r => !string.IsNullOrEmpty(r)).Aggregate(string.Empty, (current, r) => current + (r + "\r\n"));
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
            {
                foreach (var img in ChampsPanel.Controls.OfType<Picture>())
                {
                    img.Visible = true;
                }
            }
            else
            {
                // Revesre is faster 'cos it don't need to redraw so often
                foreach (var img in ChampsPanel.Controls.OfType<Picture>().Reverse())
                {
                    // Ignore Apostrophe
                    img.Visible = img.Name.IndexOf(Regex.Replace(Search.Text, @"[']+", ""), StringComparison.CurrentCultureIgnoreCase) != -1;
                }
            }
            ChampsPanel.ResumeLayout();
            //ResumeLayout();
        }

        private static Bitmap MakeGrayscale(Bitmap original)
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