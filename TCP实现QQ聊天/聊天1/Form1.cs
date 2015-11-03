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
using System.Security;
using System.Threading;
using System.IO;

namespace 聊天1
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        IPAddress ipadress;
        TcpListener tcplistener;
        TcpClient tcpclient;
        Socket s;
        ASCIIEncoding asc = new ASCIIEncoding();
        string localIP = System.Net.Dns.GetHostByName(System.Net.Dns.GetHostName()).AddressList[0].ToString();
        int portNum {
            get { return new Random().Next(1000, 9999); }
        }
        bool Host_listen = false;
        bool Slave_listen = false;
        Thread thread_Host_create=null;
        Thread thread_Slave_accept=null;
        private void button1_Click(object sender, EventArgs e)
        {
            new Thread(new ThreadStart(Slave_Connect)).Start();
            
        }

        private void radioButton_Slave_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton_Slave.Checked)//不是主机时
            {
                label_slave.Enabled = true;
                textBox_adr_slave.Enabled = true;
                label2_slave.Enabled = true;
                textBox2_slave.Enabled = true;
                button_test_slave.Enabled = true;


                label_host.Enabled = false;
                label2_host.Enabled = false;
                textBox_host.Enabled = false;
                textBox_adr_host.Enabled = false;
                updatetext("请配置IP地址和端口后点击连接测试连接主机。。", Color.Red, true);

                Slave_listen = true;
                Host_listen = false;
                Host_Dispose();
            }
            else
            {
                label_slave.Enabled = false;
                textBox_adr_slave.Enabled = false;
                label2_slave.Enabled = false;
                textBox2_slave.Enabled = false;
                button_test_slave.Enabled = false;


                label_host.Enabled = true;
                label2_host.Enabled = true;
                textBox_host.Enabled = true;
                textBox_adr_host.Enabled = true;
                updatetext("等待从机的连接。。。", Color.Red, true);

                textBox_adr_host.Text = localIP;
               
                textBox_host.Text = portNum.ToString();

                Slave_listen = false;
                Host_listen = true;
                thread_Host_create = new Thread(new ThreadStart(Host_create));
                thread_Host_create.Start();
               
            }
        }
        void Host_create()
        {
           
            
            //建立socket
            ipadress = IPAddress.Parse(localIP);
            tcplistener = new TcpListener(ipadress, portNum);
            tcplistener.Start();//开启监听服务。
            
            s = tcplistener.AcceptSocket();
            updatetext("连接来自：" + s.RemoteEndPoint, Color.Blue, true);
            byte[] b;
            while (Host_listen)
            {
                try
                {
                    b = new byte[1024];
                    int k = s.Receive(b);
                    string msg = "";
                    for (int i = 0; i < k; i++)
                    {
                        msg += Convert.ToChar(b[i]);
                    }
                    if (msg != "" && msg != null)
                    {
                        updatetext(s.RemoteEndPoint.ToString() + "--" + DateTime.Now, Color.Brown, true);
                        updatetext(msg, Color.Black, true);
                    }
                }
                catch (Exception e)
                {
                    MessageBox.Show(e.Message);
                    if (s != null)
                        s.Close();
                }
            }
        }
        void Host_Dispose()
        {
            if (tcplistener!=null)
                tcplistener.Stop();
            if (s != null)
                s.Close();
            
        }
        delegate void UpdateText(string msg, Color color, bool nextline);
        void updatetext(string msg, Color color, bool nextline)
        {
            if (this.richTextBox_chat.InvokeRequired)
                this.richTextBox_chat.Invoke(new UpdateText(updatetext), msg, color, nextline);
            else
            {
                this.richTextBox_chat.SelectionColor=color;
                if (nextline)
                    this.richTextBox_chat.AppendText(msg + "\r\n");
                else
                    this.richTextBox_chat.AppendText(msg);
            }
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            this.radioButton_Slave.Checked = true;
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
           
            Host_listen = false;
            Slave_listen = false;
            if(thread_Host_create!=null&&thread_Host_create.ThreadState!= ThreadState.Aborted)
            thread_Host_create.Abort();
            if(thread_Slave_accept!=null&&thread_Slave_accept.ThreadState!= ThreadState.Aborted)
            thread_Slave_accept.Abort();
            Host_Dispose();

        }
        void Slave_Connect()
        {
            try
            {
                tcpclient = new TcpClient(textBox_adr_slave.Text, int.Parse(textBox2_slave.Text));
                MessageBox.Show("连接成功");
                Stream stm;
                
                byte[] b;
                stm = tcpclient.GetStream();
                while (Slave_listen)
                {

                    System.Text.Encoding asc = Encoding.Default;
                    b = new byte[1024];
                    int k = stm.Read(b, 0, 100);
                    string msg = "";

                    // 从服务器返回信息
                    for (int i = 0; i < k; i++)
                    {
                        msg += Convert.ToChar(b[i]);
                    }
                    updatetext(textBox_adr_slave.Text + "--" + DateTime.Now, Color.Brown, true);
                    updatetext(msg, Color.Black, true);
                }
                if (stm != null)
                    stm.Close();

            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
                
            }
           
        }
        void Slave_Send()
        {
            Stream stm = tcpclient.GetStream();
            Encoding asc = Encoding.Default;
            byte[] b = asc.GetBytes(richTextBox_prechat.Text);
            stm.Write(b, 0, b.Length);
            updatetext(textBox_adr_slave.Text + "--" + DateTime.Now, Color.Green, true);
            updatetext(richTextBox_prechat.Text, Color.Black, true);
            this.richTextBox_prechat.Clear();
        }
        void Host_Send()
        {
            if (thread_Host_create.ThreadState != ThreadState.Suspended)
                thread_Host_create.Suspend();
            try
            {
                ASCIIEncoding asc = new ASCIIEncoding();
                s.Send(asc.GetBytes(richTextBox_prechat.Text));
                updatetext(s.LocalEndPoint.ToString() + "--" + DateTime.Now, Color.Green, true);
                updatetext(richTextBox_prechat.Text, Color.Black, true);
                this.richTextBox_prechat.Clear();
                thread_Host_create.Resume();
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }
        }
        private void button2_Click(object sender, EventArgs e)
        {
            if (richTextBox_prechat.Text == "" || richTextBox_prechat.Text == null)
            {
                MessageBox.Show("发送消息不能为空！");
                return;
            }
            if (radioButton_Host.Checked)
            {
                Host_Send();
            }
            else
            {
                Slave_Send();
            }
        }
        void test()
        {
            MessageBox.Show("111");
        }
    }
}
