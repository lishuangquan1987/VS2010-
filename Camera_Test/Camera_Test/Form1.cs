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
            try
            {
                videoDevies = new FilterInfoCollection(FilterCategory.VideoInputDevice);
                if (videoDevies.Count != 0)
                {
                    MessageBox.Show(videoDevies[0].Name);
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
        public VideoCaptureDevice ConnectVideo(int deviceIndex=0, int resolutionIndex = 0)
        {
            if (videoDevies.Count <= 0)
                return null;
            selectedDeviceIndex = deviceIndex;
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
            System.Threading.Thread.Sleep(500);
            Bitmap bmp = (Bitmap)e.Frame.Clone();
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
