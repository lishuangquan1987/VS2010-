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
        string fileName = @"C:\VS2012\Project\调用烧录CMD程序\test\bin\Debug\test.exe";
        Process p = new Process();
        string output = "";
        private void button1_Click(object sender, EventArgs e)
        {
           
           
            //p.OutputDataReceived += p_OutputDataReceived;
            p.StandardInput.WriteLine("1");
            System.Threading.Thread.Sleep(2000);
            this.richTextBox1.AppendText(output);
            output = "";
            
            

            
        }

        void p_OutputDataReceived(object sender, DataReceivedEventArgs e)
        {
            //if(this.InvokeRequired)
            //    this.Invoke()
            //this.richTextBox1.Text += p.StandardOutput.ReadLine();
            output += e.Data;

        }

        private void Form1_Load(object sender, EventArgs e)
        {
            p.StartInfo.FileName = fileName;
            p.StartInfo.UseShellExecute = false;
            p.StartInfo.RedirectStandardOutput = true;
            p.StartInfo.RedirectStandardInput = true;
            p.StartInfo.CreateNoWindow = true;
            p.StartInfo.RedirectStandardError = true;
            p.OutputDataReceived += p_OutputDataReceived;
            p.Start();
            p.BeginOutputReadLine();
        }
    }
}
