using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Runtime.InteropServices;

namespace 图片的各种处理
{
    public partial class Form1 : Form
    {
       
        [Flags]
        public enum AnimationType
        {
            AW_HOR_POSITIVE = 0x0001,//从左向右显示
            AW_HOR_NEGATIVE = 0x0002,//从右向左显示
            AW_VER_POSITIVE = 0x0004,//从上到下显示
            AW_VER_NEGATIVE = 0x0008,//从下到上显示
            AW_CENTER = 0x0010,//从中间向四周
            AW_HIDE = 0x10000,
            AW_ACTIVATE = 0x20000,//普通显示
            AW_SLIDE = 0x40000,
            AW_BLEND = 0x80000,//透明渐变显示效果
            AW_SLIDE1 = 0x40000 | 0x0001 | 0x0004,
            AW_SLIDE2 = 0x40000 | 0x0001 | 0x0008,
            AW_SLIDE3 = 0x40000 | 0x0002 | 0x0004,
            AW_SLIDE4 = 0x40000 | 0x0002 | 0x0008,        
        }
        [DllImport("user32.dll")]
        public static extern bool AnimateWindow(IntPtr hwnd, uint dwTime, AnimationType dwFlags);
        private PictureBox pictureBox1, pictureBox2;
        private List<Image> girls = new List<Image>();
        private Timer timer = new Timer();
        private int index = 0;
        public Form1()
        {
            InitializeComponent();
            int width = Screen.GetWorkingArea(this).Width;
            int height = Screen.GetWorkingArea(this).Height;
            this.WindowState = FormWindowState.Maximized;
            this.FormBorderStyle = FormBorderStyle.None;
            this.BackColor = Color.Black;
            this.DoubleBuffered = true;
            pictureBox1 = new PictureBox();
            //pictureBox1.Location = new Point(200, 100);
            pictureBox1.Location = new Point(0, 0);
            //pictureBox1.Size = new System.Drawing.Size(640, 480);
            pictureBox1.Size = new System.Drawing.Size(width, height);
            pictureBox1.SizeMode = PictureBoxSizeMode.AutoSize;
            pictureBox1.Visible = false;
            //this.Controls.Add(pictureBox1);
            pictureBox2 = new PictureBox();
            //pictureBox2.Location = new Point(400, 300);
            pictureBox2.Location = new Point(0, 0);

            
            //pictureBox2.Size = new System.Drawing.Size(640, 480);

            pictureBox2.Size = new System.Drawing.Size(width, height);

            pictureBox2.SizeMode = PictureBoxSizeMode.AutoSize;
            pictureBox1.Visible = false;
            this.Controls.Add(pictureBox2);
            using (OpenFileDialog dlg = new OpenFileDialog())
            {
                dlg.Multiselect = true;
                dlg.Filter = "jpeg files(*.jpg)|*.jpg";
                if (dlg.ShowDialog() == DialogResult.OK)
                {
                    foreach (string file in dlg.FileNames)
                    {
                        girls.Add(Image.FromFile(file));
                    }
                }
            }
            timer.Interval = 3000;
            timer.Tick += new EventHandler(timer_Tick);
            timer.Enabled = true;
        }
        void timer_Tick(object sender, EventArgs e)
        {
            if (girls.Count == 0)
            { return; }
            Image currentGirl = girls[index++];
            pictureBox2.Image = currentGirl;
            AnimateWindow(pictureBox2.Handle, 1000,
              GetRandomAnimationType());
            //AnimateWindow(pictureBox1.Handle, 1000, AnimationType.AW_HIDE);

            //pictureBox1.Visible = false;
            //PictureBox temp = pictureBox1;
            //pictureBox1 = pictureBox2;
            //pictureBox2 = temp;
            if (index >= girls.Count)
            {
                index = 0;
            }
        }
        private Random random = new Random();
        private AnimationType[] animationTypes = null;
        private AnimationType GetRandomAnimationType()
        {
            if (animationTypes == null)
            {
                animationTypes = Enum.GetValues(typeof(AnimationType))
                  as AnimationType[];
            }
            
            return animationTypes[random.Next(0, animationTypes.Length - 1)];
        }
        protected override void OnKeyDown(KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                timer.Enabled = false;
                foreach (Image girl in girls)
                {
                    girl.Dispose();
                }
                this.Close();
            }
            base.OnKeyDown(e);
        }
    }
}
