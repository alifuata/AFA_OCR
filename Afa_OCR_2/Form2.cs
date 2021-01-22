using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using Tesseract;

namespace Afa_OCR_2
{
    public partial class Form2 : Form
    {
        public Form2()
        {
            InitializeComponent();
        }
        string filename = "";
        private void btnSelectPic_Click(object sender, EventArgs e)
        {

            OpenFileDialog fd = new OpenFileDialog();
            fd.Multiselect = false;
            fd.Filter = "Image Files |*.jpg;*.png;*.bmp)";
            if (fd.ShowDialog() == DialogResult.OK)
            {
                filename = fd.FileName;
                pictureBox1.Image = null;
                pictureBox1.Load(filename);
            }
        }
        Dictionary<string, string> languages = new Dictionary<string, string>() { { "eng", "English" }, { "tur", "Turkish" }, { "fra", "French" }, { "deu", "German" }, { "spa", "Spanish" } };
        public static Bitmap bitmap;
        private void Form2_Load(object sender, EventArgs e)
        {
            //cbLanguages.Items.AddRange(new String[] { "English", "Turkish", "French", "German" });
            cbLanguages.DataSource = new BindingSource(languages, null);
            cbLanguages.DisplayMember = "Value";
            cbLanguages.ValueMember = "Key";
            cbLanguages.SelectedIndex = 0;
            cbTransLanguage.DataSource = new BindingSource(languages, null);
            cbTransLanguage.DisplayMember = "Value";
            cbTransLanguage.ValueMember = "Key";
            cbTransLanguage.SelectedIndex = 0;
            radioButton1.Checked = true;

        }


        private void btnOCR_Click(object sender, EventArgs e)
        {
            if (filename.Length > 0)
            {
                Bitmap imgsource = (Bitmap)Image.FromFile(filename);
                var ocrtext = string.Empty;
                using (var engine = new TesseractEngine(@".\tessdata", cbLanguages.SelectedValue.ToString(), EngineMode.Default))
                {
                    using (var img = PixConverter.ToPix(imgsource))
                    {
                        using (var page = engine.Process(img))
                        {
                            ocrtext = page.GetText();
                        }
                    }
                }
                richTextBox1.Text = ocrtext;
            }
            else if (bitmap != null)
            {
                //Bitmap imgsource = (Bitmap)Image.FromFile(filename);
                var ocrtext = string.Empty;
                using (var engine = new TesseractEngine(@".\tessdata", cbLanguages.SelectedValue.ToString(), EngineMode.Default))
                {
                    using (var img = PixConverter.ToPix(bitmap))
                    {
                        using (var page = engine.Process(img))
                        {
                            ocrtext = page.GetText();
                        }
                    }
                }
                richTextBox1.Text = ocrtext;
            }
        }

        private void btnTranslate_Click(object sender, EventArgs e)
        {
            string translater = radioButton1.Checked ? "Google" : "Yandex";
            Form1 form = new Form1(richTextBox1.Text, cbLanguages.SelectedValue.ToString(), cbTransLanguage.SelectedValue.ToString(),translater);

            form.ShowDialog();
        }

        private void btnSelectArea_Click(object sender, EventArgs e)
        {
            //this.WindowState = FormWindowState.Minimized;
            Screen[] screen = Screen.AllScreens;
            List<Bitmap> bmp = new List<Bitmap>();
            this.WindowState = FormWindowState.Minimized;
            Thread.Sleep(1000);
            foreach (var item in screen)
            {
                Graphics myGraphics = this.CreateGraphics();
                //Size s = this.Size;
                Bitmap memoryImage = new Bitmap(item.Bounds.Width, item.Bounds.Height, myGraphics);
                Graphics memoryGraphics = Graphics.FromImage(memoryImage);
                memoryGraphics.CopyFromScreen(item.WorkingArea.X, item.WorkingArea.Y, 0, 0, new Size(item.Bounds.Width, item.Bounds.Height));
                bmp.Add(memoryImage);
            }
            this.WindowState = FormWindowState.Normal;
            Form3 form3 = new Form3(bmp.ToArray());
            //form3.WindowState = FormWindowState.Maximized;
            form3.ShowDialog();
            pictureBox1.Image = bitmap;
        }
    }
}
