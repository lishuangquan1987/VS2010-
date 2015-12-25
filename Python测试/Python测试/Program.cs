using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using IronPython.Hosting;
using Microsoft.Scripting.Hosting;

namespace Python测试
{
    class Program
    {
        static void Main(string[] args)
        {
            ScriptRuntime python = Python.CreateRuntime();
            dynamic r= python.UseFile("hello.py");

            Console.WriteLine(r.yreadlines(@"C:\Users\LSQ\Desktop\Mes讨论内容.txt"));
            Console.Read();
        }
    }
}
