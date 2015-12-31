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
using System.Threading;
using System.IO;
using Sunisoft.IrisSkin;

namespace 串口调试助手_Tony_Supper
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
        }
        public List<Port> ports = new List<Port>();
        public Port currentPort;
        MidForm midForm;
        ExtendForm extend_Form;
        Thread thread_always_send;
        int delay=1000;
        Dictionary<string, string> configContent = new Dictionary<string, string>();
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
            foreach (Control item in this.midForm.panel_serialport.Controls)
            {
                this.midForm.panel_serialport.Controls.Remove(item);
            }
	
            string[] port_names = SerialPort.GetPortNames();
            if (port_names != null)
            {
                for (int i = 0; i < port_names.Length; i++)
                {
                    Port p = new Port(port_names[i]);
                    RadioButton r = new RadioButton();
                    p.textBox_PortName.Tag = r;                   
                    r.Location = new Point(5, i * 30 + 3);
                    p.Location = new Point(20, 30 * i);
                    ports.Add(p);
                    r.CheckedChanged += r_CheckedChanged;
                    this.midForm.panel_serialport.Controls.Add(p);
                    this.midForm.panel_serialport.Controls.Add(r);
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
                    if (i.textBox_PortName.Tag == r as object)
                    {
                        i.Enabled = true;
                        currentPort = i;
                    }
                }
            }
            else
            {
                foreach (Port i in ports)
                {
                    if (i.textBox_PortName.Tag == r as object)
                    {
                        i.Enabled = false;
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
            if (this.midForm.textBox_cmd.Text == null || this.midForm.textBox_cmd.Text == "")
            {
                mainText1.Updatetext("发送消息不能为空", Color.Red, true);
                return;
            }
            string msg = this.midForm.textBox_cmd.Text+"\r\n";
            currentPort.WriteString(msg);
            mainText1.Updatetext(string.Format("s[{0}]:{1}", DateTime.Now.ToString("hh:mm:ss.fff"), msg), Color.Blue, true);
            string result=currentPort.ReadString();
            if(result!=null&&result!="")
            mainText1.Updatetext(string.Format("r[{0}]:{1}", DateTime.Now.ToString("hh:mm:ss.fff"), result), Color.Green, true);
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            Sunisoft.IrisSkin.SkinEngine skin = new SkinEngine((Component)this);
            skin.SkinFile = Application.StartupPath + "\\DeepGreen.ssk";
            #region~loadExtend
            extend_Form = new ExtendForm();
            this.dockPanel1.ShowDocumentIcon = true;
            extend_Form.Show(this.dockPanel1,WeifenLuo.WinFormsUI.Docking.DockState.DockBottom);
            for (int i = 0; i < 50; i++)
            {
                extend_Form.extend_units[i].button.Click += extend_Form_button_Click;
                extend_Form.extend_units[i].textBox1.TextChanged += Extend_TextChangedHandle;
            }
            LoadConfig();
            #endregion
            midForm = new MidForm();
            midForm.button_ReScan.Click +=button_ReScan_Click;
            midForm.button_Send.Click += button_Send_Click;
            midForm.checkBox_autosend.CheckedChanged += midForm_AutoSendcheckedChange;
            midForm.Show(this.dockPanel1,WeifenLuo.WinFormsUI.Docking.DockState.Document);

            this.AcceptButton = midForm.button_Send;
            ReScan();
            
        }
        void Extend_TextChangedHandle(object sender, EventArgs e)
        {
            TextBox textbox = (TextBox)sender;
            configContent[textbox.Tag.ToString()] = textbox.Text;
        }
        delegate void extend_Form_button_Click_delegate(object sender, EventArgs e);
        void extend_Form_button_Click(object sender, EventArgs e)
        {
            if (this.InvokeRequired)
                this.Invoke(new extend_Form_button_Click_delegate(extend_Form_button_Click), sender, e);
            else
            {
                Button btn = (Button)sender;
                string cmd = ((TextBox)btn.Tag).Text;
                this.midForm.textBox_cmd.Text = cmd;
                button_Send_Click(sender, e);
            }
        }
        void always_send()
        {
            for (int i = 0; i < 50; i++)
            {
                if (i == 49)
                    i = 0;//无限循环
                if (!extend_Form.extend_units[i].checkBox.Checked)
                    continue;
                object sender=extend_Form.extend_units[i].button;
                EventArgs e=EventArgs.Empty;
                extend_Form_button_Click(sender, e);
                Thread.Sleep(delay);
            }
        }
        void midForm_AutoSendcheckedChange(object sender, EventArgs e)
        {
            delay = midForm.delay;
            if (midForm.checkBox_autosend.Checked)
            {
                    thread_always_send = new Thread(new ThreadStart(always_send));
                    thread_always_send.Start();
            }
            else
            {
                if (thread_always_send.ThreadState != ThreadState.Aborted)
                    thread_always_send.Abort();
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
                   extend_Form.extend_units[i].textBox1.Text = configContent[(i + 1).ToString()];
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
}
