using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Afa_OCR_2
{
    public partial class Form3 : Form
    {
        public Form3(Bitmap[] image)
        {
            InitializeComponent();
            this.bitmap = image;
        }
        private int selectX;
        private int selectY;
        private int selectWidth;
        private int selectHeight;
        public Pen selectPen;
        private bool start = false;
        //private IContainer components = (IContainer)null;

        private Bitmap[] bitmap;
        int arrayNum = 0;
        private void Form3_Load(object sender, EventArgs e)
        {
            pictureBox1.Image = bitmap[arrayNum];
            this.Cursor = Cursors.Cross;
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void nextScreenToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (bitmap.Length > 1)
            {
                arrayNum++;
                if (arrayNum < bitmap.Length)
                    pictureBox1.Image = bitmap[arrayNum];
                else
                    arrayNum--;
            }
        }

        private void previousScreenToolStripMenuItem_Click(object sender, EventArgs e)
        {
            arrayNum--;
            if (-1 != arrayNum)
                pictureBox1.Image = bitmap[arrayNum];
            else
                arrayNum++;
        }
        int[] mp = new int[2];
        int[] mpe = new int[2];
        private Size ilkSize;
        private Size sonSize;
        private void pictureBox1_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                mp[0] = MousePosition.X;
                mp[1] = MousePosition.Y;
            }


        }
        private void SaveToClipboard()
        {
            if (this.selectWidth > 0)
            {
                Rectangle srcRect = new Rectangle(this.selectX, this.selectY, this.selectWidth, this.selectHeight);
                Bitmap bitmap1 = new Bitmap(this.pictureBox1.Image, this.pictureBox1.Width, this.pictureBox1.Height);
                Bitmap bitmap2 = new Bitmap(this.selectWidth, this.selectHeight);
                Graphics graphics = Graphics.FromImage((Image)bitmap2);
                graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;
                graphics.PixelOffsetMode = PixelOffsetMode.HighQuality;
                graphics.CompositingQuality = CompositingQuality.HighQuality;
                graphics.DrawImage((Image)bitmap1, 0, 0, srcRect, GraphicsUnit.Pixel);
                Clipboard.SetImage((Image)bitmap2);
            }
            this.Close();
        }
        private void pictureBox1_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button==MouseButtons.Left)
            {
                mpe[0] = MousePosition.X;
                mpe[1] = MousePosition.Y;
                Bitmap bmpCrop = new Bitmap(pictureBox1.Image);
                Bitmap kestane = bmpCrop.Clone(new Rectangle(mp[0], mp[1], Math.Abs(mpe[0] - mp[0]), Math.Abs(mpe[1] - mp[1])), bmpCrop.PixelFormat);
                Form2.bitmap = kestane;
                this.Close();
            }

        }
    }
}
