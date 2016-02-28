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
using System.Threading;

namespace 调用CMD程序3
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        string fileName = @"C:\Program Files (x86)\DediProg\SF100\dpcmd.exe";
        string destinationfile = @"c:\111.bin";
        private void button1_Click(object sender, EventArgs e)
        {
            //ProcessStartInfo startInfo = new ProcessStartInfo();
            //startInfo.FileName = fileName;
            //startInfo.Arguments = string.Format(" -p'{0}'", destinationfile);
            //startInfo.UseShellExecute = false;
            //startInfo.RedirectStandardError = true;
            //startInfo.RedirectStandardInput = true;
            //startInfo.RedirectStandardOutput = true;
            //startInfo.CreateNoWindow = true;
            //Process p = Process.Start(startInfo);
            ////Thread.Sleep(5000);
            ////p.Kill();
            //this.richTextBox1.Text = p.StandardOutput.ReadToEnd();
            
            
            
           
            //p.StartInfo.FileName = fileName;
            //p.StartInfo.Arguments = " " + "--help";
            //p.StartInfo.UseShellExecute = false;
            //p.StartInfo.RedirectStandardOutput = true;
            //p.StartInfo.RedirectStandardInput = true;
            //p.StartInfo.CreateNoWindow = true;
            //p.StartInfo.RedirectStandardError = true;
            ////p.OutputDataReceived += p_OutputDataReceived;
            //p.Start();
            //this.richTextBox1.Text = p.StandardOutput.ReadToEnd();
            //p.WaitForExit();
            string path = @"C:\VS2012\Project\调用烧录CMD程序\test2\bin\Debug\test2.exe";
            string ret=RunCustommerScript_SN1(path, 50*1000);
            MessageBox.Show(ret);
        }
        public string RunCustommerScript_SN1(string args, int timeout)
        {
            Process p = new Process();
            //p.StartInfo.FileName = filename;
            p.StartInfo.FileName = "cmd.exe";
            //if (args != null && args != "")
            //    p.StartInfo.Arguments = args;
            p.StartInfo.UseShellExecute = false;
            p.StartInfo.RedirectStandardOutput = true;
            p.StartInfo.RedirectStandardInput = true;
            p.StartInfo.CreateNoWindow = false;
            string output = "";
            try
            {
                p.Start();
                p.StandardInput.WriteLine(args);
                Thread.Sleep(10000);
                //p.WaitForExit(timeout);
                p.Kill();
                output += p.StandardOutput.ReadToEnd();


            }
            catch (Exception e)
            {
                return e.Message;
            }
            System.Threading.Thread.Sleep(500);
            return output;
        }
    }
}
