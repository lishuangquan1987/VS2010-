using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace _1111
{
    class Program
    {
        static void Main(string[] args)
        {
            int a = 10;
            int b = a;

            test test1 = new test();
            test1.name = "aaaaa";
            test test2 = test1;
            test1.name = "bbbbbbb";
            Console.WriteLine(test2.name);
            Console.Read();
        }
    }
    class test
    {
       public string name;
    }
}
