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
            //Program p = new Program();
            //int temp = p.Add(2, 3);
            //Console.WriteLine("结果是："+temp.ToString());
            //Console.Read()
            Test[] test = new Test[2];
            test[0] = new Test();
            test[1] = new Test();
            test[0].p.Name = "li";
            Console.WriteLine(test[1].p.Name);
            Console.Read();
        }
        int Add(int a, int b)
        {
            int c = a + b;
            return c;
        }
    }
    public class Test
    {
      public  People p = new People();
    }
    public class People
    {
        private string _name;
        public string Name
        {
            set { _name = value; }
            get { return _name; }
        }
        public People(string Name)
        {
            this._name = Name;
        }
        public People()
        {
        }
    }
}
