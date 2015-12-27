using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace 装饰器模式_练习
{
    class Program
    {
        static void Main(string[] args)
        {
            //西服装饰男人
            People person = new Man();
            Decrator decrator = new Xifu();
            decrator.instanceToBeDectrated = person;
            decrator.Show();
            Console.WriteLine("===================");

           //西服和皮鞋装饰男人
            Decrator decrator1 = new Pixie();
            decrator.instanceToBeDectrated = decrator1;
            decrator1.instanceToBeDectrated = person;
            decrator.Show();
            Console.Read();

        }
    }
    public abstract class People
    {
        public abstract void Show();
    }
    public class Man : People
    {
        public override void Show()
        {
            Console.WriteLine("男人");
        }
    }
    public class Women : People
    {
        public override void Show()
        {
            Console.WriteLine("女人");
        }
    }
    public class Decrator : People  
    {
        public People instanceToBeDectrated;
        public override void Show()
        {
            if (instanceToBeDectrated != null)
                instanceToBeDectrated.Show();
        }
    }
    public class Xifu : Decrator
    {
        public override void Show()
        {
            base.Show();
            Console.WriteLine("西服");
        }
    }
    public class Pixie : Decrator
    {
        public override void Show()
        {
            base.Show();
            Console.WriteLine("皮鞋");
        }
    }
}
