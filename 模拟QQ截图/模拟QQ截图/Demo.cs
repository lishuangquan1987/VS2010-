using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Runtime.InteropServices;

namespace Capture_LikeQQ
{
    public partial class Demo : Form
    {
        //
        public Demo()
        {
            InitializeComponent();
        }
        Point upperleft;
        Point downright;
        private void Demo_MouseDown(object sender, MouseEventArgs e)
        {
            this.Cursor = Cursors.Hand;
            Capture_LikeQQ.Capture.GetCursorPos(ref upperleft);
            this.pictureBox1.MouseMove += new System.Windows.Forms.MouseEventHandler(this.Demo_MouseMove);
            timer1.Enabled = false;
        }

        private void Demo_Load(object sender, EventArgs e)
        {
            HotKey.SetForegroundWindow(this.Handle);
            label1.Location = new Point(0, 0);
            this.MouseMove -= new System.Windows.Forms.MouseEventHandler(this.Demo_MouseMove);
            timer1.Enabled = true;
            g2 = this.pictureBox1.CreateGraphics();
            imag = this.BackgroundImage;
           

        }

        private void Demo_MouseUp(object sender, MouseEventArgs e)
        {
            Capture_LikeQQ.Capture.GetCursorPos(ref downright);
            Size size = new System.Drawing.Size(Math.Abs(downright.X - upperleft.X), Math.Abs(downright.Y - upperleft.Y));

            Bitmap bmp = null;
            if (upperleft.X < downright.X && upperleft.Y < downright.Y)//从左上往右下方拖动
                bmp = Capture_LikeQQ.Capture.GetImage(upperleft, size);
            else if (upperleft.X > downright.X && upperleft.Y > downright.Y) //从右下往左上方拖动
                bmp = Capture_LikeQQ.Capture.GetImage(downright, size);
            else if (upperleft.X < downright.X && upperleft.X > downright.Y)//从左下方往右上方拖动
                bmp = Capture_LikeQQ.Capture.GetImage(new Point(upperleft.X, downright.Y), size);
            else if (upperleft.X > downright.X && upperleft.Y < downright.Y)//从右上方往左下方拖动
                bmp = Capture_LikeQQ.Capture.GetImage(new Point(downright.X, upperleft.Y), size);
            
            this.pictureBox1.MouseMove -= new System.Windows.Forms.MouseEventHandler(this.Demo_MouseMove);
            Clipboard.SetDataObject(bmp);
            SaveFileDialog s = new SaveFileDialog();
            s.Filter = "BMP|*.bmp;|PNG|*.png|GIF|*.gif|JPEG|*.jpeg";
            if (s.ShowDialog() == DialogResult.OK)
            {
                switch (s.FilterIndex)
                {
                    case 0: bmp.Save(s.FileName); break;
                    case 1: bmp.Save(s.FileName, System.Drawing.Imaging.ImageFormat.Png); break;
                    case 2: bmp.Save(s.FileName, System.Drawing.Imaging.ImageFormat.Gif); break;
                    case 3: bmp.Save(s.FileName, System.Drawing.Imaging.ImageFormat.Jpeg); break;
                }
                
                bmp.Save(s.FileName);
                this.Cursor = Cursors.Default;
                timer1.Enabled = false;

                this.Close();
            }
            else
            {
                timer1.Enabled = true;//启动画面一直在前面
                this.Refresh();
            }
        }

        Graphics g2;
        Image imag;
        private void Demo_MouseMove(object sender, MouseEventArgs e)
        {
            this.pictureBox1.Refresh();
            label1.Text = string.Format("选取的bmp大小{0}:{1}", Math.Abs(e.X - upperleft.X), Math.Abs(e.Y - upperleft.Y));
            label1.ForeColor = Color.Green;
            
            int x_max;
            int x_min;
            int y_max;
            int y_min;
            if (e.X > upperleft.X)
            {
                x_max = e.X;
                x_min = upperleft.X;
            }
            else
            {
                x_min = e.X;
                x_max = upperleft.X;
            }
            if (e.Y > upperleft.Y)
            {
                y_max = e.Y;
                y_min = upperleft.Y;
            }
            else
            {
                y_min = e.Y;
                y_max = upperleft.Y;
            }
            int width = x_max - x_min;
            int height = y_max - y_min;
            g2.DrawRectangle(new Pen(new SolidBrush(Color.Blue)), new Rectangle(x_min, y_min, width, height));
            Application.DoEvents();
        }

        private void Demo_MouseClick(object sender, MouseEventArgs e)
        {

        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            IntPtr winform = HotKey.GetFocus();
            if (winform != this.Handle)
                HotKey.SetForegroundWindow(this.Handle);
        }

        private void Demo_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                this.Close();
            }
            else if (e.KeyCode == Keys.Enter)
            {
                timer1.Enabled = false;
                Bitmap bmp = Capture_LikeQQ.Capture.GetImage(new Point(0, 0), new Size(this.Width, this.Height));
                Clipboard.SetDataObject(bmp);
                SaveFileDialog s = new SaveFileDialog();
                s.Filter = "BMP|*.bmp;|PNG|*.png|GIF|*.gif|JPEG|*.jpeg";
                if (s.ShowDialog() == DialogResult.OK)
                {
                    switch (s.FilterIndex)
                    {
                        case 0: bmp.Save(s.FileName); break;
                        case 1: bmp.Save(s.FileName, System.Drawing.Imaging.ImageFormat.Png); break;
                        case 2: bmp.Save(s.FileName, System.Drawing.Imaging.ImageFormat.Gif); break;
                        case 3: bmp.Save(s.FileName, System.Drawing.Imaging.ImageFormat.Jpeg); break;
                    }
                    
                    bmp.Save(s.FileName);
                    this.Cursor = Cursors.Default;
                    timer1.Enabled = false;

                    this.Close();
                }
                else
                {
                    timer1.Enabled = true;//启动画面一直在前面
                    this.Refresh();
                }
            }
        }

        private void pictureBox1_MouseMove(object sender, MouseEventArgs e)
        {
            
        }

    }
}
