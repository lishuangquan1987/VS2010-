using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using AForge.Video;
using AForge.Video.DirectShow;
using ThoughtWorks;
using ThoughtWorks.QRCode.Codec;
using ThoughtWorks.QRCode.Codec.Data;
using System.Threading;

namespace Camera_MySelf
{
    public partial class Form1 : Form
    {
        bool IsRunning = true;
        PictureBox pixtureBox;
        public Form1()
        {
            InitializeComponent();
        }
        AForge.Video.DirectShow.FilterInfoCollection devices;
        AForge.Video.DirectShow.FilterInfo currentDevice;
        AForge.Video.DirectShow.VideoCaptureDevice currentVideoSource;
        private void Form1_Load(object sender, EventArgs e)
        {
            devices = Camera.videodevices;
            if (devices == null)
            {
                MessageBox.Show("未找到摄像图设备！");
                return;
            }
            foreach (AForge.Video.DirectShow.FilterInfo fi in devices)
            {
                this.comboBox1.Items.Add(fi.Name);
            }
            comboBox1.SelectedIndex = 0;

            pixtureBox = new PictureBox();
           // pixtureBox.Size = new Size(372, 346);
            pixtureBox.Size = new Size(640, 480);
            pixtureBox.Location = new Point(259, 19);
            
            this.Controls.Add(pixtureBox);

            string[] speed = new string[] { "1", "10", "20", "50", "100", "500" };
            this.comboBox2.Items.AddRange(speed);

            button3.Enabled = false;
            button2.Enabled = false;
            button4.Enabled = false;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (comboBox1.SelectedIndex == -1)
            {
                MessageBox.Show("请选择设备录像！");
                return;
            }

            this.Size = new Size(659, 415);
            foreach (FilterInfo i in devices)
            {
                if (i.Name == this.comboBox1.SelectedItem.ToString())
                    currentDevice = i;
            }
            currentVideoSource = Camera.connectDevice(currentDevice);
            if (this.comboBox2.SelectedIndex == -1)
                currentVideoSource.DesiredFrameRate = 1;
            else
                currentVideoSource.DesiredFrameRate = int.Parse(this.comboBox2.SelectedItem.ToString());
            currentVideoSource.DesiredFrameSize = new System.Drawing.Size(this.pixtureBox.Width, this.pixtureBox.Height);
            currentVideoSource.NewFrame += videosource_NewFrame;
            Camera.openDevice(currentVideoSource);

            button1.Enabled = false;
            button2.Enabled = true;
            button3.Enabled = true;
            button4.Enabled = true;


        }
        Bitmap bmp;
        void videosource_NewFrame(object sender, NewFrameEventArgs eventArgs)
        {
             bmp=(Bitmap)eventArgs.Frame.Clone();
            // this.pixtureBox.Size = bmp.Size;
             this.pixtureBox.BackgroundImage = bmp;
            
        }

        private void button3_Click(object sender, EventArgs e)
        {
            IsRunning = false;
            //有问题，需要改进。
            if (currentDevice != null)
            {
                currentVideoSource.NewFrame -= videosource_NewFrame;
                currentVideoSource.Stop();
                currentVideoSource = null;
                bmp = null;
            }
            this.Size = new Size(261, 415);

            button2.Enabled = false;
            button4.Enabled = false;
            button1.Enabled = true;
                
        }

        private void button2_Click(object sender, EventArgs e)
        {
            
            //currentVideoSource.Stop();
            currentVideoSource.NewFrame-=videosource_NewFrame;
            Bitmap bmp_temp =(Bitmap)this.pixtureBox.BackgroundImage;
            bmp_temp.Save(Application.StartupPath + "\\" + DateTime.Now.ToString("yyyyMMdd-hhmmss")+".bmp",System.Drawing.Imaging.ImageFormat.Bmp);
            MessageBox.Show("拍照成功！");
            currentVideoSource.NewFrame += videosource_NewFrame;
            //currentVideoSource.Start();
            button3.Enabled = true;
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            System.Diagnostics.Process.GetCurrentProcess().Kill();
        }
        public bool IsQRImage(Bitmap bmp, out string content)
        {
            bool result = true;
            QRCodeDecoder qrd = new QRCodeDecoder();
            try
            {
                content = qrd.decode(new ThoughtWorks.QRCode.Codec.Data.QRCodeBitmapImage(bmp));
            }
            catch (Exception e)
            {
                result = false;
                content = null;
                return result;
            }

            return result;
        }
        public void JudgeIsQRImage()
        {
            while (IsRunning)
            {
                string result;
                if (IsQRImage(bmp, out result))
                {
                    MessageBox.Show(result);
                }
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            Thread t = new Thread(new ThreadStart(JudgeIsQRImage));
            t.Start();
        }
        Wholewindow wh=new Wholewindow();
        void videosource_NewFrame_WholeWindow(object sender, NewFrameEventArgs eventArgs)
        {
            bmp = (Bitmap)eventArgs.Frame.Clone();
            wh.BackgroundImage = bmp;

        }

        private void button5_Click(object sender, EventArgs e)
        {           
            wh.Show();
            currentVideoSource.NewFrame += videosource_NewFrame_WholeWindow;   
        }
   
    }
}
