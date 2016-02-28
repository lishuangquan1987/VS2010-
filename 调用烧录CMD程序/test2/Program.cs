using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace test2
{
    class Program
    {
        static void Main(string[] args)
        {
            for (int i = 0; i < 500; i++)
            {
                Console.WriteLine(i.ToString());
                System.Threading.Thread.Sleep(20);
                Console.Clear();
            }
            Console.WriteLine("OK");
        }
    }
}
