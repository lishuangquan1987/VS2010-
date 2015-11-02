using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace 串口调试助手_Tony
{
    public partial class Form1 : Form
    {
       List<Model_SerialPort.SerialPortMain> SerialPorts=new List<Model_SerialPort.SerialPortMain>();
       Model_SerialPort.SerialPortMain CurrentPort = null;
       Extend[] extends = new Extend[50];
        string[] Ports = null;
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
            if (CurrentPort.WriteString(this.textBox2.Text) == 0)
            {
                msgbox(DateTime.Now.ToString() + ":" + this.textBox2.Text, true, Color.Blue);
            }
            msgbox(DateTime.Now.ToString() + ":" + CurrentPort.ReadString(), true, Color.Green);
            
            #region~处理拓展问题
            Panel temp_panel = new Panel();
            temp_panel.Size = new Size(this.panel2.Width, height);
            
            #endregion
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            
            Load_Event();
            GetSerialPorts();
            LoadComBox();
            LoadExtand();
           
        }
        void LoadComBox()
        {
            #region~加载combox的其他配置
            string[] BauRate = new string[] { "4800", "9600", "19200", "38400", "57600", "115200" };
            string[] Parity = new string[] {"NONE","ODD","EVEN","MARK","SPACE"};
            string[] DataBits = new string[] {"6","7","8" };
            string[] StopBits = new string[] {"0","1","1.5","2" };
            this.comboBox_Baute.Items.AddRange(BauRate);
            this.comboBox_Parity.Items.AddRange(Parity);
            this.comboBox_DataBits.Items.AddRange(DataBits);
            this.comboBox_StopBits.Items.AddRange(StopBits);

            this.comboBox_Baute.SelectedIndex = 5;//默认选择115200
            this.comboBox_Parity.SelectedIndex = 0;//默认为NONE;
            this.comboBox_DataBits.SelectedIndex = 2;//默认为8；
            this.comboBox_StopBits.SelectedIndex = 1;

            #endregion
        }
        void LoadExtand()
        {
            for (int i = 0; i < extends.Length; i++)
            {
                extends[i] = new Extend();
                extends[i].Location = new Point(0, i * 30);
                extends[i].button_Send.Text = (i + 1).ToString();
                extends[i].button_Send.Click += Extend_button_Send_Click;
                this.panel2.Controls.Add(extends[i]);
            }

            string[] Delay = new string[] { "20ms", "100ms", "500ms", "1000ms", "2000ms", "5000ms", "10000ms" };
            this.comboBox_Delay.Items.AddRange(Delay);
        }
        void Extend_button_Send_Click(object sender, EventArgs e)
        {
            Button button = (Button)sender;
            this.textBox2.Text = ((TextBox)button.Tag).Text;
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
            if (Ports.Length != 0)
            {
                this.comboBox_COM.Items.Clear();
                this.comboBox_COM.Items.AddRange(Ports);
                
                Model_SerialPort.SerialPortMain[] temp_ports = new Model_SerialPort.SerialPortMain[Ports.Length];
                for (int i = 0; i < temp_ports.Length; i++)
                {
                    temp_ports[i] = new Model_SerialPort.SerialPortMain();
                    temp_ports[i].PortName = Ports[i];
                    temp_ports[i].Event_Show += this.textBox1.Show;//装载事件
                    this.SerialPorts.Add(temp_ports[i]);
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
            ConfigSerialPort(CurrentPort);
            if (button_Open.Text == "Open")
            {
                
                if (CurrentPort.Port_Open() == 0)
                {
                    button_Open.Text = "Close";
                    this.toolStripStatusLabel1.Text = string.Format("端口：{0}  状态:打开", CurrentPort.PortName);
                }
            }
            else
            {
                if (CurrentPort.Port_Close() == 0)
                {
                    button_Open.Text = "Open";
                    this.toolStripStatusLabel1.Text = string.Format("端口：{0}  状态:关闭", CurrentPort.PortName);
                }
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
                        this.toolStripStatusLabel1.Text = string.Format("端口：{0}  状态:打开", CurrentPort.PortName);
                    }
                    else
                    {
                        this.button_Open.Text = "Open";
                        this.toolStripStatusLabel1.Text = string.Format("端口：{0}  状态:关闭", CurrentPort.PortName);
                    }
                }
            }
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            int delay;
            if (!this.checkBox1.Checked)
            {
                return;
            }
            this.label6.Visible = true;
            this.comboBox_Delay.Visible = true;
            if (this.comboBox_Delay.SelectedIndex == -1)
                delay = 0;
            else
                delay =int.Parse((this.comboBox_Delay.SelectedItem.ToString().Split('m')[0]));
            for (int i = 0; i < extends.Length; i++)
            {
                if (!extends[i].checkBox.Checked)
                    continue;
                Extend_button_Send_Click(extends[i].button_Send, e);
                if (i == extends.Length - 1)
                    i = 0;
                System.Threading.Thread.Sleep(delay);
            }
        }
    }
}
