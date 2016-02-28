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
        string args = @"C:\VS2012\Project\调用烧录CMD程序\test2\bin\Debug\test2.exe";
        Process p = null;
        string result = "";
        private void button1_Click(object sender, EventArgs e)
        {
            p = new Process();
            p.StartInfo.FileName = args;
          
            p.StartInfo.UseShellExecute = true;
            //p.StartInfo.RedirectStandardOutput = true;
            //p.StartInfo.RedirectStandardInput = true;
            p.StartInfo.CreateNoWindow = true;
            //p.StartInfo.RedirectStandardError = true;
            p.OutputDataReceived += p_OutputDataReceived;
            p.Start();
            //p.BeginOutputReadLine();
           // p.StandardInput.WriteLine(args);
           // p.StandardOutput.BaseStream.Flush();
            //p.StandardInput.WriteLine(args);
            ////p.BeginOutputReadLine();
            //string output = "";
            //while (!p.StandardOutput.EndOfStream)
            //{
            //    output += p.StandardOutput.ReadLine();
            //}
            //MessageBox.Show(output);
            
        }

        private void p_OutputDataReceived(object sender, DataReceivedEventArgs e)
        {
            result += p.StandardOutput.ReadLine();
        }

    }
}
