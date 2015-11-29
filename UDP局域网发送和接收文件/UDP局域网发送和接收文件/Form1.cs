using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Net;
using System.Net.Sockets;
using System.Net.NetworkInformation;
using System.IO;
using System.Threading;

namespace UDP局域网发送和接收文件
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        /// <summary>
        /// 判断端口号是否被占用
        /// </summary>
        /// <param name="port">端口号</param>
        /// <param name="TcpOrUdp">类型：tcp或者udp</param>
        /// <returns>返回是否被占用</returns>
        public static bool PortInUse(int port, string TcpOrUdp)
        {
            bool inUse = false;

            IPGlobalProperties ipProperties = IPGlobalProperties.GetIPGlobalProperties();
            IPEndPoint[] ipEndPoints;
            if (TcpOrUdp.ToLower() == "tcp")
            {
                ipEndPoints = ipProperties.GetActiveTcpListeners();
            }
            else
            {
                ipEndPoints = ipProperties.GetActiveUdpListeners();
            }

            foreach (IPEndPoint endPoint in ipEndPoints)
            {
                if (endPoint.Port == port)
                {
                    inUse = true;
                    break;
                }
            }

            return inUse;
        }
        UdpClient Udpclient_receive;
        UdpClient Udpclient_send;
        bool IsListenning = false;
        private void button1_Click(object sender, EventArgs e)
        {
            
            if (button1.Text == "开始监听")
            {
                int port = int.Parse(this.textBox1.Text);
                if (PortInUse(port, "udp"))
                {
                    MessageBox.Show("此端口已经被占用！", "错误提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                IsListenning = true;
                Thread.Sleep(10);
                new Thread(() => { ListenThePort(port); }).Start();
                button1.Text = "停止监听";
                
            }
            else
            {
                IsListenning = false;
                button1.Text = "开始监听";
                if (Udpclient_receive != null)
                {
                    Udpclient_receive.Close();
                }
            }
        }
        /// <summary>
        /// 接收文件
        /// </summary>
        /// <param name="port">端口号</param>
        delegate string delegate_ShowForm();
        string ShowForm()
        {
            if (this.InvokeRequired)
            {
                return this.Invoke(new delegate_ShowForm(ShowForm), null)as string;
                 
            }
            else
            {
                SaveFileDialog op = new SaveFileDialog();
                if (op.ShowDialog() != DialogResult.OK)
                    return null;
                else
                    return op.FileName;
            }
        }
        void ListenThePort(int port)
        {
            Udpclient_receive = new UdpClient(port);
            Udpclient_receive.DontFragment = true;
            while (IsListenning)
            {
                if (Udpclient_receive.Available < 1)
                    continue;
                if (MessageBox.Show("有文件发送过来\r\n是否接收", "提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Information) != DialogResult.OK)
                {
                    
                    return;
                }
                string result = ShowForm();
                if (result == null)
                    return;
                string path = result;
                byte[] datas = new byte[512];
                //Udpclient_receive.Client.ReceiveBufferSize = Udpclient_receive.Available;
                IPEndPoint ip=new IPEndPoint(IPAddress.Any,0);
               
                FileStream fs_write = new FileStream(path, FileMode.Append, FileAccess.Write);
                int ReceiveLength=0;
                while ((ReceiveLength = Udpclient_receive.Client.Receive(datas)) > 0)
                {
                    fs_write.Write(datas, 0, ReceiveLength);

                }
                fs_write.Close();
                MessageBox.Show(string.Format("来自{0}端口{1}的文件已经接收完毕！", ip.Address.ToString(), ip.Port.ToString()));
                
                
            }
        }
        /// <summary>
        /// 发送文件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button2_Click(object sender, EventArgs e)
        {
            if (textBox3.Text == "" || textBox2.Text == "")
            {
                MessageBox.Show("IP地址和端口都不能为空！");
                return;
            }
            int port=int.Parse(this.textBox3.Text);
            //if (PortInUse(port, "udp"))
            //{
            //    MessageBox.Show("此端口已经被占用！", "错误提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
            //    return;
            //}
            OpenFileDialog op = new OpenFileDialog();
            if (op.ShowDialog() != DialogResult.OK)
                return;
            string path=op.FileName;
            FileStream fs_read = new FileStream(path, FileMode.Open, FileAccess.Read);
            long size = fs_read.Length;
            byte[] bytes=new byte[512];
           
            IPEndPoint ip = new IPEndPoint(IPAddress.Parse(this.textBox2.Text), port);
            Udpclient_send = new UdpClient();
            Udpclient_send.DontFragment = true;
           // Udpclient_send.Client.SendBufferSize = bytes.Length;
            int readLenth=0;
            while((readLenth= fs_read.Read(bytes,0,bytes.Length))>0)
            {
                Udpclient_send.Send(bytes, readLenth, ip);
            }
            fs_read.Close();
          // int result= Udpclient_send.Send(bytes, bytes.Length, ip);
           MessageBox.Show("发送成功！");
           Udpclient_send.Close();
            

        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            System.Diagnostics.Process.GetCurrentProcess().Kill();
        }
        string IP = System.Net.Dns.GetHostByName(System.Net.Dns.GetHostName()).AddressList[0].ToString();
        private void Form1_Load(object sender, EventArgs e)
        {
            this.toolStripStatusLabel1.Text = "本机IP地址：" + IP;
        }
    }
}
