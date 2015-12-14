using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Threading;
using WeifenLuo;
using System.IO;

namespace 串口调试助手_Tony
{
    public partial class Form1 : Form
    {
       List<Model_SerialPort.SerialPortMain> SerialPorts=new List<Model_SerialPort.SerialPortMain>();
       Model_SerialPort.SerialPortMain CurrentPort = null;
       Extend[] extends = new Extend[50];
        string[] Ports = null;
        Thread thread_always_send;
        int delay=0;
        int _width = 0;
        int _height = 0;
        Dictionary<string, string> configContent = new Dictionary<string, string>();
        public Form1()
        {
            InitializeComponent();
        }

        void Load_Event()
        {
            event_msgbox += textBox1.Show;
        }
        Panel panel = null;
        int height = 30;
        private void buttonSend_Click(object sender, EventArgs e)
        {
            if (this.textBox2.Text == null || this.textBox2.Text == "")
            {
                msgbox("发送消息不能为空！", true, Color.Red);
                return;
            }
            if (CurrentPort==null||!CurrentPort.IsOpen)
            {
                msgbox("请先选择一个串口并打开！", true, Color.Red);
                return;
            }
            string CMD = "";
            if (checkBox2.Checked)
                CMD = this.textBox2.Text + "\r\n";
            else
                CMD = this.textBox2.Text;
            if (CurrentPort.WriteString(CMD) == 0)
            {
                msgbox(DateTime.Now.ToString() + ":" + this.textBox2.Text, true, Color.Blue);
            }
            //msgbox(DateTime.Now.ToString() + ":" + CurrentPort.ReadString(), true, Color.Green);
            
            #region~处理拓展问题
            Panel temp_panel = new Panel();
            temp_panel.Size = new Size(this.panel2.Width, height);
            
            #endregion
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            _width = this.Width;
            _height = this.Height;
            this.checkBox2.Checked = true;
            Load_Event();
            GetSerialPorts();
            LoadComBox();
            LoadExtand();
            //test _t=new test(){a=3,b=5};
            //this.propertyGrid1.SelectedObject = _t;
            LoadConfig();

            if (this.comboBox_COM.SelectedIndex != -1)
            {
                this.button_Open.BackColor = Color.FromArgb(50, 0, 0);
            }
           
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
                        configContent.Add(key,value);

                }
                

                for (int i = 1; i <= 50; i++)
                {                
                    string key=i.ToString();
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
        void LoadComBox()
        {
            #region~加载combox的其他配置
            string[] BauRate = new string[] {"2400","4800", "9600", "19200", "38400", "57600", "115200" };
            string[] Parity = new string[] {"NONE","ODD","EVEN","MARK","SPACE"};
            string[] DataBits = new string[] {"6","7","8" };
            string[] StopBits = new string[] {"0","1","1.5","2" };
            this.comboBox_Baute.Items.AddRange(BauRate);
            this.comboBox_Parity.Items.AddRange(Parity);
            this.comboBox_DataBits.Items.AddRange(DataBits);
            this.comboBox_StopBits.Items.AddRange(StopBits);

            this.comboBox_Baute.SelectedIndex = 6;//默认选择115200
            this.comboBox_Parity.SelectedIndex = 0;//默认为NONE;
            this.comboBox_DataBits.SelectedIndex = 2;//默认为8；
            this.comboBox_StopBits.SelectedIndex = 1;

            #endregion
        }
        void LoadExtand()
        {
            int width = this.panel2.Size.Width;
            int height = 30;
            for (int i = 0; i < extends.Length; i++)
            {
                extends[i] = new Extend();
                extends[i].Location = new Point(0, i * 30);
                extends[i].button_Send.Text = (i + 1).ToString();
                extends[i].Size = new System.Drawing.Size(width-20, height);
                extends[i].button_Send.Click += Extend_button_Send_Click;
                //extends[i].Dock = DockStyle.Top;
                extends[i].textBox1.TextChanged += Extend_TextChangeHandle;
                this.panel2.Controls.Add(extends[i]);
            }

            string[] Delay = new string[] {"500ms", "1000ms", "2000ms", "5000ms", "10000ms" };
            this.comboBox_Delay.Items.AddRange(Delay);
        }
        void Extend_TextChangeHandle(object sender, EventArgs e)
        {
            TextBox textbox = (TextBox)sender;
            configContent[textbox.Tag.ToString()] = textbox.Text;
        }
        void Extend_button_Send_Click(object sender, EventArgs e)
        {
            Button button = (Button)sender;
            this.textBox2.Text = ((TextBox)button.Tag).Text;//sendButton的Tag为对应的textbox
            buttonSend_Click(sender, e);
        }
        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void button_ReScan_Click(object sender, EventArgs e)
        {
            GetSerialPorts();
            
        }
        void GetSerialPorts()
        {
            Ports = Model_SerialPort.SerialPortMain.GetPortNames();
            this.comboBox_COM.Items.Clear();
            string Exist_Ports = "";//获取已经存在的portname
            if(this.SerialPorts.Count!=0)
            {
                foreach (Model_SerialPort.SerialPortMain i in this.SerialPorts)
                {
                    Exist_Ports += i.PortName;
                }
            }
            if (Ports.Length != 0)
            {
                
                this.comboBox_COM.Items.AddRange(Ports);
                
                Model_SerialPort.SerialPortMain[] temp_ports = new Model_SerialPort.SerialPortMain[Ports.Length];
                for (int i = 0; i < temp_ports.Length; i++)
                {
                    if (Exist_Ports.Contains(Ports[i]))//如果以前的port还在，则不需要重新创建实例。
                        continue;
                    temp_ports[i] = new Model_SerialPort.SerialPortMain();
                    temp_ports[i].PortName = Ports[i];
                    temp_ports[i].Event_Show += this.textBox1.Show;//装载事件
                    this.SerialPorts.Add(temp_ports[i]);//如果有新的port口，则添加进来
                }
                this.comboBox_COM.SelectedIndex = 0; 
            }

        }
        void ConfigSerialPort(Model_SerialPort.SerialPortMain s)
        {
            s.Encoding = Encoding.Default;
            s.PortName = this.comboBox_COM.SelectedItem.ToString();
            s.BaudRate = int.Parse(this.comboBox_Baute.SelectedItem.ToString());
            switch (this.comboBox_Parity.SelectedIndex)
            {
                case 0: s.Parity = System.IO.Ports.Parity.None; break;
                case 1: s.Parity = System.IO.Ports.Parity.Odd; break;
                case 2: s.Parity = System.IO.Ports.Parity.Even; break;
                case 3: s.Parity = System.IO.Ports.Parity.Mark; break;
                case 4: s.Parity = System.IO.Ports.Parity.Space; break;
            }
            s.DataBits = int.Parse(this.comboBox_DataBits.SelectedItem.ToString());
            switch (this.comboBox_StopBits.SelectedIndex)
            {
                case 0: s.StopBits = System.IO.Ports.StopBits.None; break;
                case 1: s.StopBits = System.IO.Ports.StopBits.One; break;
                case 2: s.StopBits = System.IO.Ports.StopBits.OnePointFive; break;
                case 3: s.StopBits = System.IO.Ports.StopBits.Two; break;

            }
        }

        private void button_Open_Click(object sender, EventArgs e)
        {
            if (this.comboBox_COM.SelectedIndex == -1)
            {
                msgbox("请选择COM口", true, Color.Red);
                return;
            }
            
            if (button_Open.Text == "Open")
            {
                ConfigSerialPort(CurrentPort);   
                if (CurrentPort.Port_Open() == 0)
                {
                    button_Open.Text = "Close";
                    this.toolStripStatusLabel1.Text = string.Format("端口：{0}  状态:打开  波特率：{1}", CurrentPort.PortName, CurrentPort.BaudRate.ToString());
                }
                this.button_Open.BackColor = Color.FromArgb(0,50, 0);
            }
            else
            {
                if (CurrentPort.Port_Close() == 0)
                {
                    button_Open.Text = "Open";
                    this.toolStripStatusLabel1.Text = string.Format("端口：{0}  状态:关闭", CurrentPort.PortName);
                }
                this.button_Open.BackColor = Color.FromArgb(50, 0, 0);
            }
        }

        private void comboBox_COM_SelectedIndexChanged(object sender, EventArgs e)
        {
            foreach (Model_SerialPort.SerialPortMain i in SerialPorts)
            {
                if (i.PortName == this.comboBox_COM.SelectedItem.ToString())
                {
                    CurrentPort = i;

                    if (i.IsOpen)
                    {
                        this.button_Open.Text = "Close";
                        this.toolStripStatusLabel1.Text = string.Format("端口：{0}  状态:打开  波特率：{1}", CurrentPort.PortName,CurrentPort.BaudRate.ToString());
                    }
                    else
                    {
                        this.button_Open.Text = "Open";
                        this.toolStripStatusLabel1.Text = string.Format("端口：{0}  状态:关闭", CurrentPort.PortName);
                    }
                }
            }
        }
        delegate void send_button_Click(object sender, EventArgs e);
        void Send_button_Click(object sender, EventArgs e)
        {
            if (InvokeRequired)
                this.Invoke(new send_button_Click(Send_button_Click), sender, e);
            else
                Extend_button_Send_Click(sender, e);
        }
        void always_send(object sender,EventArgs e)
        {
            for (int i = 0; i < extends.Length; i++)
            {
                if (!this.checkBox1.Checked)
                    break;
                if (i == extends.Length - 1)
                    i = 0;
                if (!extends[i].checkBox.Checked)
                    continue;
                Send_button_Click(extends[i].button_Send, e);
                
                System.Threading.Thread.Sleep(delay);
            }
        }
        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (!this.checkBox1.Checked)
            {
                if (thread_always_send != null && thread_always_send.ThreadState != ThreadState.Aborted)
                    thread_always_send.Abort();
                return;
            }
            this.label6.Visible = true;
            this.comboBox_Delay.Visible = true;
            if (this.comboBox_Delay.SelectedIndex == -1)
                delay = 1000;
            else
                delay =int.Parse((this.comboBox_Delay.SelectedItem.ToString().Split('m')[0]));
            thread_always_send = new Thread(() => { always_send(sender, e); });
            thread_always_send.Start();
        }

        private void comboBox_Baute_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.comboBox_Baute.SelectedIndex != -1 && CurrentPort != null)               
            CurrentPort.BaudRate = int.Parse(this.comboBox_Baute.SelectedItem.ToString());
            if (CurrentPort != null&&CurrentPort.IsOpen)
            this.toolStripStatusLabel1.Text = string.Format("端口：{0}  状态:打开  波特率：{1}", CurrentPort.PortName, CurrentPort.BaudRate.ToString());
        }
        void SizeChange()
        {
            int width_after = this.Width;
            int height_after = this.Height;
            int diff_width = width_after - _width;
            int diff_height = height_after - _height;
           // Point Location = this.panel2.Location;
            Size newsize = new Size(this.panel2.Width + diff_width, this.panel2.Height + diff_height);
            this.panel2.Size = newsize;
            _width = width_after;
            _height = height_after;

        }
        private void Form1_Resize(object sender, EventArgs e)
        {
            //SizeChange();
        }

        private void panel2_SizeChanged(object sender, EventArgs e)
        {
            //Size size=this.panel2.Size;
            //foreach(Extend i in this.extends)
            //{
            //    i.Size = size;
            //}
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            string ConfigStr = "";
            foreach (var item in configContent)
            {
                ConfigStr += item.Key + "#" + item.Value + "\r\n";
            }
            File.WriteAllText("config.ini", ConfigStr);
        }
    }
    public class test
    {
        public int a;
        public int b;
    }
}
