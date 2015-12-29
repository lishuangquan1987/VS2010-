using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using NationalInstruments.NI4882;
using System.Threading;
using System.IO;

namespace A2750控制
{
    public partial class A2750Control : Form
    {
        Extend[] extends = new Extend[50];
        public Device device = null;
        string[] Delay = new string[] { "100ms", "300ms", "500ms", "1000ms", "2000ms", "5000ms" };
        Thread thread_always_send = null;
        Dictionary<string, string> configContent = new Dictionary<string, string>();
        public A2750Control()
        {
            InitializeComponent();
        }
        void LoadExtend()
        {
            for (int i = 0; i < 50; i++)
            {
                extends[i] = new Extend();
                extends[i].button1.Text = (i + 1).ToString();
                extends[i].button1.Tag = extends[i].textBox1;
                extends[i].textBox1.Tag = (i + 1).ToString();
                extends[i].Location = new Point(0, 30 * i);
                extends[i].button1.Click += button_Click;
                extends[i].textBox1.TextChanged += Extend_TextChangeHandle;
                this.panel_extend.Controls.Add(extends[i]);
            }
        }
        void Extend_TextChangeHandle(object sender, EventArgs e)
        {
            TextBox textbox = (TextBox)sender;
            configContent[textbox.Tag.ToString()] = textbox.Text;
        }
        void LoadConfig()
        {
            string[] config;
            if (File.Exists("config.ini"))
            {
                config = File.ReadAllLines("config.ini");
                //读取config值到字典
                //先读取配置，若没有的则赋值为空
                foreach (var item in config)
                {
                    string key = item.Split('#')[0];
                    string value = item.Split('#')[1];
                    if (configContent.ContainsKey(key))//若包含重复的键值，则取第一个
                        continue;
                    else
                        configContent.Add(key, value);

                }


                for (int i = 1; i <= 50; i++)
                {
                    string key = i.ToString();
                    if (configContent.ContainsKey(key))
                        continue;
                    else
                        configContent.Add(i.ToString(), "");
                }
                //将字典值赋值给extends
                for (int i = 0; i < 50; i++)
                {
                    extends[i].textBox1.Text = configContent[(i + 1).ToString()];
                }

            }
            else
            {
                File.Create("config.ini");
                for (int i = 1; i <= 50; i++)
                {
                    configContent.Add(i.ToString(), "");
                }
            }

        }
        void always_send(int delay)
        {
            
            for (int i = 0; i < 50; i++)
            {
                if (!extends[i].checkBox.Checked)
                    continue;
                object sender = extends[i].button1;
                EventArgs e = EventArgs.Empty;
                button_Click(sender,e);
                Thread.Sleep(delay);
            }
        }
        void button_Click(object sender, EventArgs e)
        {
            Button btn = (Button)sender;
            string cmd = ((TextBox)btn.Tag).Text;
            thread_sendCMD(cmd, sender, e);

        }
        private delegate void SendCMD(string cmd,object sender,EventArgs e);
        void thread_sendCMD(string cmd,object sender,EventArgs e)
        {
            if (this.InvokeRequired)
                this.Invoke(new SendCMD(thread_sendCMD), cmd,sender,e);
            else
            {
                this.textBox_cmd.Text = cmd;
                this.button_send_Click(sender,e);
            }
 
        }
        bool OpenDevice(int boardID,int primaryaddress,int secondadress)
        {
            bool result=false;
            try
            {
                device = new Device(0, (byte)primaryaddress,(byte)secondadress, TimeoutValue.T1s);
                result = true;
                
            }
            catch (Exception e)
            {
                device = null;
                MessageBox.Show("open fail"+e.Message);
                result = false;
            }
            return result;
        }
        private void A2750Control_Load(object sender, EventArgs e)
        {
            LoadExtend();
            LoadConfig();
            if (OpenDevice(0, 16, 0))
            {
                button_connect.Text = "Disconnect";
            }
            else
                button_connect.Text = "Connect";

            this.comboBox_delay.Items.AddRange(Delay);

        }

        private void button_connect_Click(object sender, EventArgs e)
        {
            if (button_connect.Text == "Connect")
            {
                int primaryaddress = int.Parse(this.textBox_primary.Text);
                int secondaddress = int.Parse(this.textBox_sencond.Text);
                int boardID = int.Parse(this.textBox_boardID.Text);
                if (OpenDevice(boardID, primaryaddress,secondaddress))
                {
                    button_connect.Text = "Disconnect";
                }
                else
                    button_connect.Text = "Connect";
            }
            else if (button_connect.Text == "Disconnect")
            {
                device = null;
                button_connect.Text = "Connect";
            }
        }

        private void button_send_Click(object sender, EventArgs e)
        {
            if (device != null)
            {
                string cmd = this.textBox_cmd.Text;
                device.Write(cmd);
            }
            else
            {
 
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.richTextBox_output.AppendText(device.ReadString() + "\r\n");
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (device != null)
            {
                device.Write("*IDN?");
               richTextBox_output.AppendText(device.ReadString()+"\r\n");
            }
        }

        private void checkBox_autosend_CheckedChanged(object sender, EventArgs e)
        {
            int delay = 1000;
            if (this.comboBox_delay.SelectedIndex != -1)
                delay = int.Parse(this.comboBox_delay.SelectedItem.ToString().Split('m')[0]);
            if (checkBox_autosend.Checked)
            {
                if (thread_always_send != null)
                {
                    if (thread_always_send.ThreadState != ThreadState.Aborted)
                        return;
                    else
                        thread_always_send.Start();
                }
                else
                {
                    thread_always_send = new Thread(new ThreadStart(() => { always_send(delay); }));
                    thread_always_send.Start();
                }
            }
            else
            {
                if (thread_always_send.ThreadState != ThreadState.Aborted)
                    thread_always_send.Abort();
            }
        }

        private void A2750Control_FormClosing(object sender, FormClosingEventArgs e)
        {

            string ConfigStr = "";
            foreach (var item in configContent)
            {
                ConfigStr += item.Key + "#" + item.Value + "\r\n";
            }
            File.WriteAllText("config.ini", ConfigStr);
        }
    }

}
