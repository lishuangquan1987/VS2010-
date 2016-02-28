using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO.Ports;

namespace 十六进制与byte的转换
{
    class Program
    {
        static void Main(string[] args)
        {
            string str = "00";
            byte b = Convert.ToByte(str, 16);
            Console.WriteLine(b.ToString());
            Console.Read();
            byte[] bytes = new byte[5] { 23, 100, 59, 255, 169 };
            for (int i = 0; i < bytes.Length; i++)
            {
                Console.WriteLine(bytes[i].ToString("X2"));
            }
            Console.Read();
            Console.Read();
           
        }
    }
}
