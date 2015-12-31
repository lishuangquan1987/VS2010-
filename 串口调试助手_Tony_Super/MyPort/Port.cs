using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using N_EventCenter;

namespace MyPort
{
    public partial class Port : UserControl
    {
        private MySerialPort serialport;
        string[] BauteRate = new string[] {"1152000","960000","115200", "57600", "38400", "19200", "9600", "4800", "2400" };
        //string[] BauteRate = new string[] {"115200", "57600", "38400", "19200", "9600", "4800", "2400" };
        string[] Parity = new string[] {"NONE","ODD","EVEN","MARK","SPACE"};
        string[] StopBits=new string[]{"0","1","1.5","2"};
        string[] DataBits = new string[] {"6","7","8"};
        public string portname;
        public object tag;//与外部ratioButton绑定
        public bool Isopen
        {
            get { return serialport.IsOpen; }
        }
        public Port(string portname)
        {
            
            InitializeComponent();
            this.portname = portname;
            serialport = new MySerialPort(portname);
            //this.serialport.PortName = portname;
        }
        public int baurate
        {
            get { return this.serialport.BaudRate; }
        }
        
        public void WriteString(string cmd)
        {
            this.serialport.WriteString(cmd);
        }
        public string ReadString()
        {
            return this.serialport.ReadString();
        }
        private void Port_Load(object sender, EventArgs e)
        {
            LoadInitConfig();            
            this.textBox_PortName.Text = portname;
            this.Enabled = false;
        }
        void LoadInitConfig()
        {
            this.comboBox_BauteRate.Items.AddRange(BauteRate);
            this.comboBox_Parity.Items.AddRange(Parity);
            this.comboBox_StopBit.Items.AddRange(StopBits);
            this.comboBox_DataBit.Items.AddRange(DataBits);
            EventCenter.GetInstance().AddObserver(EventName.ConfigChange + portname, I_ConfigChange);//注册，供别人使用
            this.comboBox_BauteRate.SelectedIndex = 2;//默认配置115200；
            this.comboBox_Parity.SelectedIndex = 0;//默认配置NONE
            this.comboBox_StopBit.SelectedIndex = 1;//默认配置1
            this.comboBox_DataBit.SelectedIndex = 2;//默认配置8
        }
        void ConfigChange()
        {
            if(this.comboBox_BauteRate.SelectedIndex!=-1)
            this.serialport.BaudRate = int.Parse(this.comboBox_BauteRate.SelectedItem.ToString());
            if (this.comboBox_Parity.SelectedIndex != -1)
            {
                switch (this.comboBox_Parity.SelectedItem.ToString())
                {
                    case "NONE": this.serialport.Parity = System.IO.Ports.Parity.None; break;
                    case "ODD": this.serialport.Parity = System.IO.Ports.Parity.Odd; break;
                    case "EVEN": this.serialport.Parity = System.IO.Ports.Parity.Even; break;
                    case "MARK": this.serialport.Parity = System.IO.Ports.Parity.Mark; break;
                    case "SPACE": this.serialport.Parity = System.IO.Ports.Parity.Space; break;
                }
            }
            if (this.comboBox_StopBit.SelectedIndex != -1)
            {
                switch (this.comboBox_StopBit.SelectedItem.ToString())
                {
                    case "0": this.serialport.StopBits = System.IO.Ports.StopBits.None; break;
                    case "1": this.serialport.StopBits = System.IO.Ports.StopBits.One; break;
                    case "1.5": this.serialport.StopBits = System.IO.Ports.StopBits.OnePointFive; break;
                    case "2": this.serialport.StopBits = System.IO.Ports.StopBits.Two; break;
                }
            }
            if (this.comboBox_DataBit.SelectedIndex != -1)
            {
                this.serialport.DataBits = int.Parse(this.comboBox_DataBit.SelectedItem.ToString());
            }
            #region~通知主界面
            Dic dic=new Dic();
            string status="";
            if(this.serialport.IsOpen)
                status="打开";
            else
                status="关闭";
            dic[EventName.status]=string.Format("当前串口：{0} 波特率:{1} 状态:{2}",this.serialport.PortName,this.serialport.BaudRate,status);
            EventCenter.GetInstance().PostNotification(EventName.Update_com_status, dic);
            #endregion
        }
        void I_ConfigChange(N_EventCenter.Par par)
        {
            ConfigChange();
        }
        private void comboBox_BauteRate_SelectedIndexChanged(object sender, EventArgs e)
        {
            Dic dic=new Dic();
            EventCenter.GetInstance().PostNotification(EventName.ConfigChange+portname, dic);//配置串口
        }

        private void comboBox_Parity_SelectedIndexChanged(object sender, EventArgs e)
        {
            Dic dic = new Dic();
            EventCenter.GetInstance().PostNotification(EventName.ConfigChange + portname, dic);//配置串口
        }

        private void comboBox_StopBit_SelectedIndexChanged(object sender, EventArgs e)
        {
            Dic dic = new Dic();
            EventCenter.GetInstance().PostNotification(EventName.ConfigChange + portname, dic);//配置串口
        }

        private void comboBox_DataBit_SelectedIndexChanged(object sender, EventArgs e)
        {
            Dic dic = new Dic();
            EventCenter.GetInstance().PostNotification(EventName.ConfigChange + portname, dic);//配置串口
        }

        private void btn_open_Click(object sender, EventArgs e)
        {
            if (btn_open.Text == "Open")
            {
                if (!this.serialport.IsOpen)
                    try
                    {
                        this.serialport.Open();
                        this.btn_open.Text = "Close";
                    }
                    catch (Exception ee)
                    {
                        Dic dic = new Dic();
                        dic[EventName.color] = Color.Red;
                        dic[EventName.msg] = ee.Message;
                        dic[EventName.nextline] = true;
                        EventCenter.GetInstance().PostNotification(EventName.UpdateUI, dic);
                    }                    
                
            }
            else
            {
                if (this.serialport.IsOpen)
                    this.serialport.Close();
                this.btn_open.Text = "Open";                
            }
            Dic dic1 = new Dic();
            string status = "";
            if (this.serialport.IsOpen)
                status = "打开";
            else
                status = "关闭";
            dic1[EventName.status] = string.Format("当前串口：{0} 波特率:{1} 状态:{2}",this.portname, this.baurate, status);
            EventCenter.GetInstance().PostNotification(EventName.Update_com_status, dic1);

        }
    }
}
