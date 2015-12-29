using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using N_EventCenter;
using MyPort;
using System.IO.Ports;

namespace 串口调试助手_Tony_Supper
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        public List<Port> ports = new List<Port>();
        public Port currentPort;
        private void button1_Click(object sender, EventArgs e)
        {
            Dic dic=new Dic();
            dic["msg"]="哈哈,调用成功";
            dic["color"]=Color.Red;
            dic["nextline"]=true;
            EventCenter.GetInstance().PostNotification(EventName.UpdateUI,dic);
        }
        void ReScan()
        {
            ports.Clear();
            foreach (Control item in this.panel_serialport.Controls)
            {
                this.panel_serialport.Controls.Remove(item);
            }
	
            string[] port_names = SerialPort.GetPortNames();
            if (port_names != null)
            {
                for (int i = 0; i < port_names.Length; i++)
                {
                    Port p = new Port(port_names[i]);
                    RadioButton r = new RadioButton();
                    p.textBox_PortName.Tag = r;
                    r.CheckedChanged += r_CheckedChanged;
                    r.Location = new Point(0, i * 30 + 5);
                    p.Location = new Point(20, 30 * i);
                    ports.Add(p);
                    this.panel_serialport.Controls.Add(p);
                    this.panel_serialport.Controls.Add(r);
                }
            }
        }

        void r_CheckedChanged(object sender, EventArgs e)
        {
            RadioButton r = (RadioButton)sender;
            if (r.Checked)
            {
                foreach (Port i in ports)
                {
                    if (i.textBox_PortName.Tag == r)
                    {
                        currentPort = i;
                    }
                }
            }
        }

        private void button_ReScan_Click(object sender, EventArgs e)
        {
            ReScan();
        }

        private void button_Send_Click(object sender, EventArgs e)
        {
            if (currentPort == null)
            {
                mainText1.Updatetext("请选择一串口", Color.Red, true);
                return;
            }
            if (!currentPort.Isopen)
            {
                mainText1.Updatetext("请先打开选择的串口", Color.Red, true);
                return;
            }
            string msg = this.textBox_cmd.Text+"\r\n";
            currentPort.WriteString(msg);
            string result=currentPort.ReadString();
            if(result!=null&&result!="")
            mainText1.Updatetext(result, Color.Green, true);
        }
    }
}
