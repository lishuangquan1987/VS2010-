using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ForArgsTest
{
    class Program
    {
        static void Main(string[] args)
        {
            int count = int.Parse(args[0]);
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("Begin to print...");
            for (int i = 0; i < count; i++)
            {
                Console.WriteLine(i.ToString()+"ms escaped");
                System.Threading.Thread.Sleep(1);
                Console.Clear();
                
            }
            Console.WriteLine("succes!");
        }
    }
}
