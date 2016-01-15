using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ForArgsTest1
{
    class Program
    {
        static void Main(string[] args)
        {
            if (args == null)
                return;
            else
            {
                foreach(string i in args)
                Console.WriteLine(i);
            }
        }
    }
}
