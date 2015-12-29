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

namespace 调用烧录CMD程序
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        string fileName = @"C:\Program Files (x86)\DediProg\SF100\dpcmd.exe";
        Process p = null;
        private void button1_Click(object sender, EventArgs e)
        {
            p= new Process();
            p.StartInfo.FileName = fileName;
            p.StartInfo.Arguments = " "+"--help";
            p.StartInfo.UseShellExecute = false;
            p.StartInfo.RedirectStandardOutput = true;
            p.StartInfo.RedirectStandardInput = true;
            p.StartInfo.CreateNoWindow = true;
            p.StartInfo.RedirectStandardError = true;
            //p.OutputDataReceived += p_OutputDataReceived;
            p.Start();
            this.richTextBox1.Text = p.StandardOutput.ReadToEnd();
            p.WaitForExit();
            
        }

        void p_OutputDataReceived(object sender, DataReceivedEventArgs e)
        {
            //if(this.InvokeRequired)
            //    this.Invoke()
            //this.richTextBox1.Text += p.StandardOutput.ReadLine();
        }
    }
}
