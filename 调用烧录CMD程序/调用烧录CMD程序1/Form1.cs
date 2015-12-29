using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Diagnostics;

namespace 调用烧录CMD程序1
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        string fileName = "cmd.exe";
        string args = @"C:\Program Files (x86)\DediProg\SF100\dpcmd.exe --help";
        Process p = null;
        string result = "";
        private void button1_Click(object sender, EventArgs e)
        {
            p = new Process();
            p.StartInfo.FileName = fileName;
            p.StartInfo.Arguments = args;
            p.StartInfo.UseShellExecute = false;
            p.StartInfo.RedirectStandardOutput = true;
            p.StartInfo.RedirectStandardInput = true;
            p.StartInfo.CreateNoWindow = true;
            p.StartInfo.RedirectStandardError = true;
            p.OutputDataReceived += p_OutputDataReceived;
            p.Start();
            for (int i = 0; i < 20; i++)
            {
                string result = p.StandardOutput.ReadLine();
                this.richTextBox1.Text = result;
            }
            //p.WaitForExit();
        }

        private void p_OutputDataReceived(object sender, DataReceivedEventArgs e)
        {
            result += p.StandardOutput.ReadToEnd();
        }

    }
}
