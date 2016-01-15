using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace 字符串处理
{
    class Program
    {
        static void Main(string[] args)
        {
            //Console.WriteLine("请输入字符串：");
            //string input = Console.ReadLine();
            //if (!input.Contains("="))
            //{
            //    Console.WriteLine("所输入的字符串格式不正确！");
               
            //}
            //string sbits1 = input.Split('=')[0];
            //string sbits2 = input.Split('=')[1];
            //string[] sbit1 = sbits1.Split(',');
            //string[] sbit2 = sbits2.Split(',');
            //for (int i = 0; i < sbit1.Length; i++)
            //{
            //    Console.WriteLine(string.Format("{0}={1}", sbit1[i], sbit2[i]));

            //}
            //Console.ReadLine();
            Program p=new Program();
            string str = "PC01,PC02,PC03=0,1,0";
            int index=p.GetIndex(str);
            Console.WriteLine(index.ToString());
            Console.ReadLine();
            

        }
        int GetIndex(string i)
        {
            for (int j = 0; j < i.Length; j++)
            {
                if (i[j] == '=')
                    return j;
            }
            return -1;
        }
    }
}
