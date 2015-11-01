using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;

namespace Log_Analysizer
{
    public partial class Instructions : Form
    {
        public Instructions()
        {
            InitializeComponent();
        }

        private void Instructions_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                this.Close();
            }
        }
        string[] contents;
        private void Instructions_Load(object sender, EventArgs e)
        {
            try
            {
                contents = File.ReadAllLines(Application.StartupPath+"\\说明.txt",Encoding.Default);
            }
            catch (Exception ee)
            {
                MessageBox.Show(ee.Message);
                return;
            }
            foreach (string i in contents)
            {
                this.richTextBox1.AppendText(i+"\r\n");
            }
        }
    }
}
