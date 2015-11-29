using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace 观察者模式练习
{
    class Program
    {
        static void Main(string[] args)
        {
            Robber robber = new Robber();
            Observer citizen = new Citizen("热心市民张三");
            Observer police = new Police("警察局长老王");
            robber.Add(citizen);
            robber.Add(police);
            robber.Location = "北京";
            robber.Location = "美国";
            Console.Read();
        }
    }
    public abstract class Subject
    {
        public List<Observer> observers = new List<Observer>();
        public Observer this[int index]
        {
            get{return observers[index];}
            set{
                observers[index]=value;
            }
        }
        public void Add(Observer observer)
        {
            this.observers.Add(observer);
        }
        public void Remove(Observer observer)
        {
            this.observers.Remove(observer);
        }
        public void Notify()
        {
            foreach (Observer i in observers)
            {
                i.be_Notify(i,this);
            }
        }
    }
    public class Robber : Subject
    {
        private string _location;
        public string Location
        {
            get { return this._location; }
            set {
                if (value != _location)
                {
                    _location = value;
                    Notify();
                    
                }
            }
        }
    }
    public abstract class Observer
    {
        public abstract void be_Notify(Observer observer, Subject subject);
    }
    public class Citizen : Observer
    {
        private string _name;
        public Citizen(string name)
        {
            this._name=name;
        }
        public override void be_Notify(Observer observer, Subject subject)
        {
            Console.WriteLine(string.Format("我：{0}看见一个强盗在{1}",this._name,((Robber)subject).Location));
        }
    }
    public class Police:Observer
    {
        private string _name;
        public Police(string name)
        {
            this._name = name;
        }
        public override void be_Notify(Observer observer, Subject subject)
        {
            Console.WriteLine(string.Format("强盗在{0}，我们赶紧去抓", ((Robber)subject).Location));
        }
    }
}
