using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace 字符串转byte
{
    class Program
    {
        static void Main(string[] args)
        {
            string s = "你好";
            byte[] t = Encoding.Default.GetBytes(s);
        }
    }
}
