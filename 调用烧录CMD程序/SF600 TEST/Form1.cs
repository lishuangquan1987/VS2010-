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
using System.Text;

namespace SF600_TEST
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        public string Programming()//注意：使用--help会卡住
        {
            foreach (Process p in System.Diagnostics.Process.GetProcesses())
            {
                if (p.ProcessName.Contains("dpcmd"))
                    p.Kill();
            }
            //bool b_uid = false;
            bool b_result = false;
            ProcessStartInfo startInfo = new ProcessStartInfo();
            startInfo.FileName = @"C:\VS2012\Project\调用烧录CMD程序\test2\bin\Debug\test2.exe";
            
            startInfo.UseShellExecute = false;
            startInfo.RedirectStandardError = true;
            startInfo.RedirectStandardInput = true;
            startInfo.RedirectStandardOutput = true;
            startInfo.CreateNoWindow = true;
            DateTime startTime = DateTime.Now;
            try
            {
                Process p = Process.Start(startInfo);
                StringBuilder str = new StringBuilder();
                int i = 0;
                bool IsProcessFinish = false;
                int j = 0;
                Thread t = new Thread(() =>
                {
                    while (!IsProcessFinish)
                    {
                        Thread.Sleep(1);
                        j++;
                        if (j >= 50 * 1000)
                        {
                            if (p != null && !p.HasExited)
                            {
                                try
                                {
                                    p.Kill();
                                    System.Windows.Forms.MessageBox.Show("p is kill");
                                }
                                catch (Exception e)
                                {
                                }
                            }
                            break;
                        }

                    }

                });
                t.Start();
                while (!p.StandardOutput.EndOfStream)
                {
                    i++;
                    DateTime endTime = DateTime.Now;
                    if ((endTime - startTime).TotalSeconds > 30)//超时30s认为是fail
                    {
                        IsProcessFinish = true;
                        return "NG overTime";
                    }
                    char[] char_result = new char[100];
                    int length = p.StandardOutput.Read(char_result, 0, char_result.Length);
                    str.Insert(0, char_result);
                    string result = str.ToString(0, str.Length);
                    //if (result.Contains("Site #" + (uid + 1).ToString()))
                    //{
                    //    b_uid = true;
                    //}
                    if (result.Contains("OK"))
                    {
                        b_result = true;
                    }
                    str.Remove(0, str.Length);
                    //Thread.Sleep(100);
                }
                p.WaitForExit(20 * 1000);
                char[] buffer = new char[512];
                p.StandardOutput.ReadBlock(buffer, 0, buffer.Length);
                IsProcessFinish = true;
                if (b_result)
                    return "OK" + i.ToString();
                else
                    return "NG";
            }
            catch (Exception e)
            {
                return "NG out of memory";
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
           string ret= Programming();
           MessageBox.Show(ret);
        }
    }
    
}
