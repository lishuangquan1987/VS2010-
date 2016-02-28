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
        //PSoCProgrammerCOM_ObjectClass ppp = new PSoCProgrammerCOM_ObjectClass();
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

            string[] frequents=new string[14]{"0.2MHZ","0.4MHZ","0.8MHZ","1.5MHZ","1.6MHZ","3.0MHZ","3.2MHZ","4.0MHZ","6.0MHZ","8.0MHZ","12MHZ","16MHZ","24MHZ","48MHZ"};
            this.comboBox_frequent.Items.AddRange(frequents);

            this.comboBox_frequent.SelectedIndex = 4;

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
            switch(this.comboBox_frequent.SelectedIndex)
            {
                case 0: programLib.SetProtocolClock(enumFrequencies.FREQ_00_2); break;
                case 1: programLib.SetProtocolClock(enumFrequencies.FREQ_00_4); break;
                case 2: programLib.SetProtocolClock(enumFrequencies.FREQ_00_8); break;
                case 3: programLib.SetProtocolClock(enumFrequencies.FREQ_01_5); break;
                case 4: programLib.SetProtocolClock(enumFrequencies.FREQ_01_6); break;
                case 5: programLib.SetProtocolClock(enumFrequencies.FREQ_03_0); break;
                case 6: programLib.SetProtocolClock(enumFrequencies.FREQ_03_2); break;
                case 7: programLib.SetProtocolClock(enumFrequencies.FREQ_04_0); break;
                case 8: programLib.SetProtocolClock(enumFrequencies.FREQ_06_0); break;
                case 9: programLib.SetProtocolClock(enumFrequencies.FREQ_08_0); break;
                case 10: programLib.SetProtocolClock(enumFrequencies.FREQ_12_0); break;
                case 11: programLib.SetProtocolClock(enumFrequencies.FREQ_16_0); break;
                case 12: programLib.SetProtocolClock(enumFrequencies.FREQ_24_0); break;
                case 13: programLib.SetProtocolClock(enumFrequencies.FREQ_48_0); break;
            
            }
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