using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Diagnostics;

namespace 调用pythonserver
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //string cmd = "python C:\\A115\\CustommerScript\\A115_Test_Harness20160222\\server.py";
            //Process p = new Process();
            //p.StartInfo.FileName = "cmd.exe";
            //p.StartInfo.Arguments = cmd;
            //p.StartInfo.UseShellExecute = true;
            ////p.StartInfo.RedirectStandardInput = true;
            //p.StartInfo.CreateNoWindow = false;
            //p.Start();
            //p.StandardInput.WriteLine(cmd);
            //p.WaitForExit();
            Process.Start(@"C:\A115_Test_Harness\server.bat");
        }
    }
}
