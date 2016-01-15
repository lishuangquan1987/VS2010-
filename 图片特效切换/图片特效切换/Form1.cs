using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace 图片特效切换
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        PictureBox pb1;
        PictureBox pb2;
        private void button1_Click(object sender, EventArgs e)
        {
            IntPtr handle = this.pb1.Handle;
            DLLHandler.CLAYUI_OnAnimation(handle, 0, 1, 0, 0);
            pb2 = new PictureBox();
            pb2 = pb1;
            pb2.Image = Image.FromFile("Image\\2.jpg");
            this.Controls.Add(pb2);
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            pb1 = new PictureBox();
            pb1.BackgroundImage = Image.FromFile("Image\\1.jpg");
            this.Controls.Add(pb1);
        }
    }
}
