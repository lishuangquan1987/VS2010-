using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Diagnostics;
using System.IO;

namespace 调用批处理
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Process p = new Process();
            p.StartInfo.FileName = @"C:\A115\CustommerScript\A115_Test_Harness\setup.bat";
            //p.StartInfo.Arguments = args;
            p.StartInfo.UseShellExecute = false;
            p.StartInfo.RedirectStandardOutput = true;
            p.StartInfo.RedirectStandardInput = true;
            p.StartInfo.CreateNoWindow = true;
            p.StartInfo.RedirectStandardError = true;
            //p.OutputDataReceived += p_OutputDataReceived;
            p.Start();
            p.WaitForExit();
            if (!File.Exists("log.txt"))
                MessageBox.Show("没有执行成功");
            else
            {
                string result = File.ReadAllText("log.txt");
                MessageBox.Show(result);
                File.Delete("log.txt");
            }

        }
    }
}
