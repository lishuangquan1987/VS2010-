using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using System.Security;
using System.Windows;

namespace 调用
{
    class Program
    {
        static void Main(string[] args)
        {
            Process p = new Process();
            p.StartInfo.FileName=@"C:\VS2012\Project\HelloWorld\HelloWorld\bin\Debug\HelloWorld.exe";
            
            p.StartInfo.RedirectStandardOutput = true;
            p.StartInfo.RedirectStandardInput = true;
            p.StartInfo.UseShellExecute = false;//必须添加
            //p.StartInfo.Arguments = "1";
            p.Start();
            p.StandardInput.WriteLine("1");
            //p.WaitForExit();
            string result = p.StandardOutput.ReadToEnd();
            Console.WriteLine(result);
            Console.WriteLine(AppDomain.CurrentDomain.BaseDirectory);
            Console.ReadLine();

        }
    }
}
