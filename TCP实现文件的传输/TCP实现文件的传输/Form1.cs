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
using System.Threading;
using System.IO;
using System.Diagnostics;

namespace TCP实现文件的传输
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        
        bool RunningFlag = true;
        string localIP = System.Net.Dns.GetHostByName(System.Net.Dns.GetHostName()).AddressList[0].ToString();
        List<TcpClient> TcpClinets_host = new List<TcpClient>();
        List<TcpClient> TcpClinets_slave = new List<TcpClient>();
        int i = 0;
        Thread listen_thread;
        Thread receive_thread;
        Thread slave_receive_thread;
        Thread UDP_ReceiveFile_thread;
        UdpClient udpclient;
        
        
        bool IsSlave = true;
        int portNum=8888;
        
        private void Form1_Load(object sender, EventArgs e)
        {
            
            
            udpclient = new UdpClient(new IPEndPoint(IPAddress.Parse(localIP), 4444));
            this.radioButton_Slave.Checked = true;
            UDP_ReceiveFile_thread = new Thread(new ThreadStart(UDP_ReceiveFile));
            UDP_ReceiveFile_thread.Start();
            

        }
        void UDP_ReceiveFile()
        {
            List<byte> data_temp;
            
            while (RunningFlag)
            {
                if (udpclient.Client.Available < 1)
                    continue;
                byte[] data = new byte[1024000];
                data_temp = new List<byte>();
                
                EndPoint endpoint = new IPEndPoint(new IPAddress(0), 0);
                int k= udpclient.Client.ReceiveFrom(data, ref endpoint);
                for (int i = 0; i < k; i++)
                {
                    data_temp.Add(data[i]);
                }
                string path = Application.StartupPath + "\\test";
                File.WriteAllBytes(path,data_temp.ToArray());
                string msg=string.Format("来自：{0}的文件已经接收完毕",((IPEndPoint)endpoint).Address.ToString());
                updatetext(msg, Color.Blue, true);


            }
        }
        void listenAndReceive()
        {
            TcpListener tcp_listener = new TcpListener(portNum);
            tcp_listener.Start();
            Thread.Sleep(1);
            //if (listen_thread != null && listen_thread.ThreadState !=System.Threading.ThreadState.Aborted)
            //    listen_thread.Abort();
            listen_thread = new Thread(() =>
            {
                while (RunningFlag&&!IsSlave)
                {
                    TcpClient tcp = tcp_listener.AcceptTcpClient();
                    
                    TcpClinets_host.Add(tcp);
                    addFriend(tcp);
                   
                }
            });
            //if (receive_thread != null && receive_thread.ThreadState !=System.Threading.ThreadState.Aborted)
            //    receive_thread.Abort();
            receive_thread = new Thread(() =>
            {
                while (RunningFlag&&!IsSlave)
                {
                    if (TcpClinets_host.Count != 0)
                    {
                       foreach(TcpClient tcp in TcpClinets_host)
                       {
                            byte[] data = new byte[1024];
                            IPEndPoint ip_temp = (IPEndPoint)tcp.Client.RemoteEndPoint;
                           
                            int k = tcp.Client.Receive(data);
                            string msg = Encoding.Default.GetString(data, 0, k);
                            string Head=ip_temp.Address+":"+ip_temp.Port+"--"+DateTime.Now;
                                if (msg != "" && msg != null && msg.Trim() != "")
                                {
                                    updatetext(Head, Color.Red, true);
                                    updatetext(msg, Color.Black, true);
                                }
                        }
                    }
                }
            });
            listen_thread.Start();
            receive_thread.Start();
        }
        delegate void AddFriend(TcpClient tcp);
        void addFriend(TcpClient tcp)
        {
            IPEndPoint ip_temp;
            if (this.friends1.listBox1.InvokeRequired)
                this.friends1.listBox1.Invoke(new AddFriend(addFriend), tcp);
            else
            {
                ip_temp = (IPEndPoint)tcp.Client.RemoteEndPoint;
                this.friends1.listBox1.Items.Add(ip_temp.Address.ToString()+":"+ip_temp.Port.ToString());
                this.friends1.listBox1.SelectedIndex = 0;
            }
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            RunningFlag = false;
            if (listen_thread != null && listen_thread.ThreadState !=System.Threading.ThreadState.Aborted)
                listen_thread.Abort();
            if (receive_thread != null && receive_thread.ThreadState != System.Threading.ThreadState.Aborted)
                receive_thread.Abort();
            if (slave_receive_thread != null && slave_receive_thread.ThreadState != System.Threading.ThreadState.Aborted)
                slave_receive_thread.Abort();

            foreach (TcpClient tcp in TcpClinets_host)
            {
                if (tcp.Connected)
                {
                    tcp.Client.Disconnect(false);
                    tcp.Close();
                }
            }
            foreach (TcpClient tcp in TcpClinets_slave)
            {
                if (tcp.Connected)
                {
                    tcp.Client.Disconnect(false);
                    tcp.Close();
                    
                }
            }
            System.Diagnostics.Process.GetCurrentProcess().Kill();
        }

        private void radioButton_Slave_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton_Slave.Checked)//不是主机时
            {
                IsSlave = true;
                label_slave.Enabled = true;
                textBox_adr_slave.Enabled = true;
                label2_slave.Enabled = true;
                textBox2_slave.Enabled = true;
                button_test_slave.Enabled = true;


                label_host.Enabled = false;
                label2_host.Enabled = false;
                textBox_host.Enabled = false;
                textBox_adr_host.Enabled = false;

            }
            else
            {
                IsSlave = false;
                label_slave.Enabled = false;
                textBox_adr_slave.Enabled = false;
                label2_slave.Enabled = false;
                textBox2_slave.Enabled = false;
                button_test_slave.Enabled = false;


                label_host.Enabled = true;
                label2_host.Enabled = true;
                textBox_host.Enabled = true;
                textBox_adr_host.Enabled = true;

                
                textBox_adr_host.Text = localIP;
                textBox_host.Text = portNum.ToString();
                listenAndReceive();
            }
            
        }

        private void button_test_slave_Click(object sender, EventArgs e)
        {
            try
            {
                TcpClient tcp = new TcpClient(textBox_adr_slave.Text, int.Parse(textBox2_slave.Text));
                MessageBox.Show("连接成功！");
                TcpClinets_slave.Add(tcp);
                addFriend(tcp);
                slave_receive_thread = new Thread(new ThreadStart(slave_Receive));
                slave_receive_thread.Start();
                button_test_slave.Enabled = false;
            }
            catch (Exception ee)
            {
                MessageBox.Show(ee.Message);
            }
        }
        void slave_Receive()
        {
            while (RunningFlag&&IsSlave)
            {
                foreach (TcpClient tcp in TcpClinets_slave)
                {
                    byte[] data = new byte[1024];
                    IPEndPoint ip_temp = (IPEndPoint)tcp.Client.RemoteEndPoint;
                        int k = tcp.Client.Receive(data);
                        string msg = Encoding.Default.GetString(data, 0, k);
                        string Head=ip_temp.Address.ToString()+":"+ip_temp.Port.ToString()+"--"+DateTime.Now;
                        if (msg != "" && msg != null && msg.Trim() != "")
                        {
                            updatetext(Head, Color.Red, true);
                            updatetext(msg, Color.Black, true);
                        }                    
                }
            }
        }
        delegate void UpdateText(string msg, Color color, bool nextline);
        void updatetext(string msg, Color color, bool nextline)
        {
            if (this.richTextBox_chat.InvokeRequired)
                this.richTextBox_chat.Invoke(new UpdateText(updatetext), msg, color, nextline);
            else
            {
                this.richTextBox_chat.SelectionColor = color;
                if (nextline)
                    this.richTextBox_chat.AppendText(msg + "\r\n");
                else
                    this.richTextBox_chat.AppendText(msg);
                this.richTextBox_chat.Focus();
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (richTextBox_prechat.Text == "" || richTextBox_prechat.Text == null)
            {
                MessageBox.Show("发送消息不能为空！");
                return;
            }
            
            if (IsSlave)
            {
                if (TcpClinets_slave.Count < 1)
                {
                    MessageBox.Show("请先连接至主机！");
                    return;
                }
                TcpClient tcp = TcpClinets_slave.ToArray()[0];
                byte[] data;
                IPEndPoint ip_temp =(IPEndPoint)tcp.Client.LocalEndPoint;
                
                data = Encoding.Default.GetBytes(richTextBox_prechat.Text);
                tcp.Client.Send(data);
                string Head = string.Format("我（{0}:{1}--{2}）", ip_temp.Address.ToString(), ip_temp.Port.ToString(), DateTime.Now.ToString());
                updatetext(Head, Color.Green, true);
                updatetext(richTextBox_prechat.Text, Color.Black, true);
            }
            else
            {
                if (TcpClinets_host.Count < 1)
                {
                    MessageBox.Show("请等待一个连接！");
                }
                foreach (TcpClient tcp in TcpClinets_host)
                {
                    IPEndPoint ip_temp =(IPEndPoint)tcp.Client.RemoteEndPoint;
                    if (ip_temp.Address.ToString() == this.friends1.SelectText.Split(':')[0])
                    {
                        byte[] data;
                        data = Encoding.Default.GetBytes(richTextBox_prechat.Text);
                        tcp.Client.Send(data);
                        IPEndPoint _ip_temp = (IPEndPoint)tcp.Client.LocalEndPoint;
                        string Head = string.Format("我（{0}:{1}--{2}）", _ip_temp.Address.ToString(), _ip_temp.Port.ToString(), DateTime.Now.ToString());
                        updatetext(Head, Color.Green, true);
                        updatetext(richTextBox_prechat.Text, Color.Black, true);
                    }
                }
            }
            this.richTextBox_prechat.Clear();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (this.friends1.listBox1.SelectedIndex < 0)
            {
                MessageBox.Show("请先连接主机或者与其他从机连接！");
                return;
            }
            OpenFileDialog op=new OpenFileDialog();
            op.Multiselect=false;
            if(op.ShowDialog()!= System.Windows.Forms.DialogResult.OK)
                return;

            byte[] data=File.ReadAllBytes(op.FileName);
            IPEndPoint RemotePoint = new IPEndPoint(IPAddress.Parse(this.friends1.SelectText.Split(':')[0]), 4444);
            UdpClient UDP = new UdpClient(new IPEndPoint(IPAddress.Any, 0));
            int k= UDP.Client.SendTo(data, RemotePoint);
        }
    }
}
