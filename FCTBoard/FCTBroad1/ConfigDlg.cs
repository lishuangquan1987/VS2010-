using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO.Ports;

namespace FCTBroad
{
    public partial class ConfigDlg : Form
    {
        public ConfigDlg()
        {
            InitializeComponent();
            UpdateCurrentPort();
            UpdateConnectStatue();
            try
            {
                this.tbConfig.Text = GT_FCTBroad.mConfigInfo[fctMarcro.kFCTCom].ToString();
                this.ComboxComlist.Text = GT_FCTBroad.mConfigInfo[fctMarcro.kFCTConfig].ToString();
                //Lex add delegate to update the UI.
                GT_FCTBroad.m_object.notifier+=new FCTBroad.Notify(OnRecevieData);
            }
            catch (Exception)
            {
            }
        }

        private void UpdateCurrentPort()
        {
            this.ComboxComlist.Items.Clear();
            foreach (string vPortName in SerialPort.GetPortNames())
            {
                this.ComboxComlist.Items.Add(vPortName);
            }
        }


        private void UpdateConnectStatue()
        {
            if (GT_FCTBroad.m_object.IsOpen)
            {
                this.Text = "FCT Broad Connected.";
            }
            else
            {
                this.Text = "FCT Broad Not Connect.";
            }
        }

        public string comname
        {
            get
            {
                return this.ComboxComlist.Text;
            }
            set
            {
                this.ComboxComlist.Text = value;
            }
        }

        public string config
        {
            get
            {
                return this.tbConfig.Text;
            }
            set
            {
                this.tbConfig.Text = value;
            }
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            UpdateCurrentPort();
        }

        private void OK_Click(object sender, EventArgs e)
        {
            if (this.ComboxComlist.Items.Count > 0)
            {
                GT_FCTBroad.mConfigInfo[fctMarcro.kFCTCom] = comname;
                if (config.Length > 0)
                    GT_FCTBroad.mConfigInfo[fctMarcro.kFCTConfig] = config;
                GT_FCTBroad.mConfigInfo.WriteXml(GT_FCTBroad.mCfgFilePath);
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            return;
        }

        private void btnHelp_Click(object sender, EventArgs e)
        {
            string str = "help\r\n";
            //GT_FCTBroad.m_object.
            GT_FCTBroad.m_object.WriteString(str);
        }

        private void btnReset_Click(object sender, EventArgs e)
        {
            string str = "reset\r\n";
            GT_FCTBroad.m_object.WriteString(str);
        }

        private void buttonver_Click(object sender, EventArgs e)
        {
            string str = "version\r\n";
            GT_FCTBroad.m_object.WriteString(str);
        }

        private void buttonsend_Click(object sender, EventArgs e)
        {
            string str = CMD.Text+"\r\n";
            GT_FCTBroad.m_object.WriteString(str);
        }

        public void OnRecevieData(string str)
        {
            tbEEStatue.AppendText(str);
        }
    }
}
