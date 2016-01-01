using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Test
{
    class Program
    {
        static void Main(string[] args)
        {
            Program p = new Program();
            int temp = p.Add(2, 3);
            Console.WriteLine("结果是："+temp.ToString());
            Console.Read();
        }
        int Add(int a, int b)
        {
            int c = a + b;
            return c;
        }
    }
}
