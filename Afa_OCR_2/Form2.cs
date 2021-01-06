using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
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
                pictureBox1.Load(filename);
            }
        }
        Dictionary<string, string> languages = new Dictionary<string, string>() { { "eng", "English" }, { "tur", "Turkish" }, { "fra", "French" }, { "deu", "German" }, { "spa", "Spanish" } };

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
        }

        private void btnOCR_Click(object sender, EventArgs e)
        {
            if (filename.Length>0)
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
        }

        private void btnTranslate_Click(object sender, EventArgs e)
        {
            Form1 form = new Form1(richTextBox1.Text, cbLanguages.SelectedValue.ToString(), cbTransLanguage.SelectedValue.ToString());
            form.ShowDialog();
        }
    }
}
