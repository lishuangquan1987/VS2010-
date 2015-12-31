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
           mark:
            Console.WriteLine("请输入一串数字，并以逗号（,）分隔：");
            string input = Console.ReadLine();
            string[] data = input.Split(',');
            double[] _data = new double[data.Length];
            for (int i = 0; i < _data.Length; i++)
            {
                _data[i] = double.Parse(data[i]);
            }
            double[] result = Sortting(_data);
            Console.WriteLine("结果是：");
            foreach (double i in result)
            {
                Console.Write(i.ToString() + ",");
            }
            Console.WriteLine("");
            goto mark;
        }
        /// <summary>
        /// 从小到大排列
        /// </summary>
        /// <param name="source"></param>
        /// <returns></returns>
       static double[] Sortting(double[] source)
        {
            for (int i = source.Length; i > 0; i--)
            {
                for (int j = 0; j < i-1; j++)
                {
                    if (source[j] > source[j + 1])
                    {
                        double temp = source[j];
                        source[j] = source[j + 1];
                        source[j + 1] = temp;
                    }

                }
            }
            return source;
        }
    }
    

}
