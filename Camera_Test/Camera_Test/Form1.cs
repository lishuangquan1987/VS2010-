using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using AForge.Controls;
using AForge.Imaging;
using AForge.Video;
using AForge.Video.DirectShow;

namespace Camera_Test
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        Bitmap bmp = null;
        private FilterInfoCollection videoDevies;
        private VideoCaptureDevice videoSource;
        
        public int selectedDeviceIndex = 0;
        private void Form1_Load(object sender, EventArgs e)
        {
            GetDevices();
        }
        public FilterInfoCollection GetDevices()
        {
            this.comboBox1.Items.Clear();
            try
            {
                videoDevies = new FilterInfoCollection(FilterCategory.VideoInputDevice);
                if (videoDevies.Count != 0)
                {
                    //MessageBox.Show(videoDevies[0].Name);
                    for (int i = 0; i < videoDevies.Count; i++)
                    {
                        this.comboBox1.Items.Add(videoDevies[i].Name);
                        this.comboBox1.SelectedIndex = 0;
                    }
                        return videoDevies;
                }
                else
                    return null;

            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
                return null;
            }
        }
        public VideoCaptureDevice ConnectVideo()
        {
            if (videoDevies.Count <= 0)
                return null;
            if (this.comboBox1.SelectedIndex == -1)
            {
                MessageBox.Show("请选择一个设备！","错误提示",MessageBoxButtons.OK,MessageBoxIcon.Error);
                return null;
            }
            int deviceIndex = this.comboBox1.SelectedIndex;
            videoSource = new VideoCaptureDevice(videoDevies[deviceIndex].MonikerString);
            videoSource.DesiredFrameRate = 1;
            videoSource.DesiredFrameSize = new System.Drawing.Size(this.pictureBox1.Width, this.pictureBox1.Height);
            videoSource.Start();
            
            return videoSource;
 
        }
        public void GrabBitmap()
        {
            if (videoSource == null)
                return;
            videoSource.NewFrame += new NewFrameEventHandler(videoSource_NewFrame);
        }
        void videoSource_NewFrame(object sender, AForge.Video.NewFrameEventArgs e)
        {
            //System.Threading.Thread.Sleep(500);
            bmp = (Bitmap)e.Frame.Clone();
            this.pictureBox1.BackgroundImage = bmp;
            
        }

        private void button1_Click(object sender, EventArgs e)
        {
            ConnectVideo();
            GrabBitmap();
          
        }

        private void button2_Click(object sender, EventArgs e)
        {
           videoSource.NewFrame -= videoSource_NewFrame;
           videoSource.Stop();
        }
    }
}
