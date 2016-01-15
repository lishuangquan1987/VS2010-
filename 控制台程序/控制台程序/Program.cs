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
            foreach (string i in args)
            {
                Console.WriteLine("this is "+i);
            }
        }
    }
}
