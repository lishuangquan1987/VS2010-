using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using PP_COM_Wrapper;
using ProgrammerLibrary;

namespace CyPressProgrammer
{
    public partial class Form1 : Form
    {
       static PSoCProgrammerCOM_ObjectClass pp = new PSoCProgrammerCOM_ObjectClass();
       static ProgrammerOperations programLib = new ProgrammerOperations();
        string FileName = "";
        public Form1()
        {
            InitializeComponent();
        }
        static string m_lastError;
        void Rescan()
        {
            this.comboBox_ports.Items.Clear();
            int hr;
            object portArray;
            hr = pp.GetPorts(out portArray,out m_lastError);
            if (hr < 0)
            {
                MessageBox.Show(m_lastError);
                return;
            }
            string[] ports = portArray as string[];
            if (ports.Length <= 0)
                return;
            this.comboBox_ports.Items.AddRange(ports);
        }
        void Open()
        {
            if (this.comboBox_ports.SelectedIndex == -1)
                return;
            string portName = this.comboBox_ports.SelectedItem.ToString();
            
            int hr= pp.OpenPort(portName, out m_lastError);
            if (hr < 0)
            {
                //pp.ClosePort();
                MessageBox.Show(m_lastError);
                return;
            }
        }

        private void btn_connect_Click(object sender, EventArgs e)
        {
            if (btn_connect.Text == "Connect")
            {
                Open();
                
                btn_connect.Text = "DisConnect";
            }
            else 
            {
                pp.ClosePort(out m_lastError);
                btn_connect.Text = "Connect";
            }

        }

        private void btn_rescan_Click(object sender, EventArgs e)
        {
            Rescan();
        }

        private void button_power_Click(object sender, EventArgs e)
        {
            int hr;
            if (button_power.Text == "Power")
            {
                if (RB_5V.Checked)
                {
                    hr = pp.SetPowerVoltage("5", out m_lastError);

                }
                else
                {
                    hr = pp.SetPowerVoltage("3.3", out m_lastError);
                }
                if (hr < 0)
                {
                    MessageBox.Show(m_lastError);
                    return;
                }
                hr = pp.PowerOn(out m_lastError);
                if (hr < 0)
                {
                    MessageBox.Show(m_lastError);
                }
                button_power.Text="Dispower";
            }
            else            
            {
                hr=pp.PowerOff(out m_lastError);
                if (hr < 0)
                {
                    MessageBox.Show(m_lastError);
                    return;
                }
                button_power.Text = "Power";
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            OpenFileDialog op = new OpenFileDialog();
            op.Filter = "hex文件|*.hex|bin文件|*.bin";
            op.Multiselect = false;
            op.Title = "请选择bin文件";
            if (op.ShowDialog() != DialogResult.OK) return;
            FileName = op.FileName;
            this.textBox1.Text = FileName;
                
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            this.SWD.Checked = true;
            this.RB_5V.Checked = true;
            this.RB_5PIN.Checked = true;

            int portopen=0;//open
            int hr = pp.IsPortOpen(out portopen, out m_lastError);
            //if(portopen==0)

            programLib.Event_AppendTextToLog += AppendTextToLog;
            programLib.Event_UpdateProgressBar += _UpdateProgressBar;
        }
        void config()
        {
            programLib.SetAutoChipDetection(true);

            programLib.SetPPCOM(pp);
            programLib.SetHexFile(FileName);
            programLib.SetAcquireMode("Reset");
            if (SWD.Checked)
                programLib.SetProtocol(enumInterfaces.SWD);
            else
                programLib.SetProtocol(enumInterfaces.JTAG);
            programLib.SetProtocolConnector(0);//5pin
            programLib.SetProtocolClock(enumFrequencies.FREQ_01_6);
        }
        private void button_program_Click(object sender, EventArgs e)
        {
            config();
            int hr = programLib.Program();        
                
        }
        void AppendTextToLog(string actions, string results, bool showTime)
        {
            if (showTime) actions += " at " + DateTime.Now.ToLongTimeString();
            UpdateText(actions + results);
        }

         void _UpdateProgressBar(int Value, int Max)
        {
            int percents = Value * 100 / Max;
            UpdateProcessBar(percents);
        }
        delegate void TextHandler(string msg);
        void UpdateText(string msg)
        {
            if (this.InvokeRequired)
                this.Invoke(new TextHandler(UpdateText), msg);
            else
                this.richTextBox1.AppendText(msg);
        }
        delegate void ProcessHandler(int value);
        void UpdateProcessBar(int value)
        {
            if (this.InvokeRequired)
                this.Invoke(new ProcessHandler(UpdateProcessBar), value);
            else
                this.toolStripProgressBar1.Value = value;
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            pp.ClosePort(out m_lastError);
        }
    }
}