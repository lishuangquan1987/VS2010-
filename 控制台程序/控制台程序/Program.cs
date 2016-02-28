using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace 控制台程序
{
    class Program
    {
        static void Main(string[] args)
        {
            string sn = "A113FB35BQ000A6";
            byte[] bytes = Encoding.Default.GetBytes(sn);
            string output = "";
            foreach (byte i in bytes)
            {
                output += i.ToString() + "  ";
            }
            Console.WriteLine(output);
            Console.Read();
        }
    }
}
