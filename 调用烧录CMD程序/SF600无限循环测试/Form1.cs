using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Threading;
using System.Diagnostics;

namespace SF600无限循环测试
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string path=@"C:\VS2012\Project\调用烧录CMD程序\test\bin\Debug\test.exe";
            string result = Programming(path, "", "", 1);
            MessageBox.Show(result);
        }
        void TimeOut(Process p, int timeout)
        {
            int i = 0;
            Thread t = new Thread(() =>
            {
                while (true)
                {
                    Thread.Sleep(1);
                    i++;
                    if (i >= timeout * 1000)
                        break;
                }
                try
                {
                    if (p != null)
                        p.Kill();

                }
                catch (Exception)
                {
                }
            }
            );
            t.Start();
        }
        public string Programming(string path,string cmdType, string arg, int uid)//注意：使用--help会卡住
        {
            foreach (Process p in System.Diagnostics.Process.GetProcesses())
            {
                if (p.ProcessName.Contains("dpcmd"))
                    p.Kill();
            }
            //bool b_uid = false;
            bool b_result = false;
            ProcessStartInfo startInfo = new ProcessStartInfo();
            startInfo.FileName = path;
            if (arg != null)
                startInfo.Arguments = string.Format(" {0}'{1}'", cmdType, arg);
            else
                startInfo.Arguments = string.Format(" {0}", cmdType);
            startInfo.UseShellExecute = false;
            startInfo.RedirectStandardError = true;
            startInfo.RedirectStandardInput = true;
            startInfo.RedirectStandardOutput = true;
            startInfo.CreateNoWindow = true;
            DateTime startTime = DateTime.Now;
            try
            {
                Process p = Process.Start(startInfo);
                TimeOut(p, 10);
                StringBuilder str = new StringBuilder();
                int i = 0;
                while (!p.StandardOutput.EndOfStream)
                {
                    str.Append(p.StandardOutput.ReadLine()+"\r\n");
                }
                return str.ToString();
            }
            catch (Exception e)
            {
                return "NG out of memory";
            }
        }
    }
}
