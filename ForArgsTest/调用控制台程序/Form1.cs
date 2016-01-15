using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Diagnostics;

namespace 调用控制台程序
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            ProcessStartInfo startInfo = new ProcessStartInfo();
            //startInfo.FileName = "ForArgsTest.exe";
            startInfo.FileName = "cmd.exe";
            startInfo.Arguments = "ForArgsTest1.exe 1000";
            //startInfo.Verb = "start ";
            startInfo.UseShellExecute = false;
            startInfo.RedirectStandardError = true;
            startInfo.RedirectStandardInput = true;
            startInfo.RedirectStandardOutput = true;
            startInfo.CreateNoWindow = true;
            Process p = Process.Start(startInfo);
            //string result = "";
            while (!p.StandardOutput.EndOfStream)
            {
                this.richTextBox1.Text += p.StandardOutput.ReadLine();
            }
        }
    }
}
