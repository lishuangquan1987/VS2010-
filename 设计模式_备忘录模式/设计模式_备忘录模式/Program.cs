using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace 设计模式_备忘录模式
{
    class Program
    {
        static void Main(string[] args)
        {
            Originator ori = new Originator("状态1");
            Console.WriteLine(ori.State);
            Caretaker ct = new Caretaker();
            ct.MemenTo = ori.CreateMemento();
            ori.State = "状态2";
            Console.WriteLine(ori.State);
            ori.RestoreMemento(ct.MemenTo);
            Console.WriteLine(ori.State);
            Console.Read();
        }
    }
    public class Originator
    {
        private string state;
        public string State
        {
            get{return this.state;}
            set{state=value;}
        }
        public Originator(string state)
        {
            this.state = state;
        }
        internal void RestoreMemento(Memento m)
        {
            state=m.State;
        }
        internal Memento CreateMemento()
        {
            return new Memento(this);
        }
    }
    internal class Memento
    {
        private string state;
        internal string State
        {
            get { return this.state; }
            set { this.state = value; }
        }
        internal Memento(Originator o)
        {
            this.state = o.State;
        }
    }
    public class Caretaker
    {
        private Memento memento;
        internal Memento MemenTo
        {
            get { return this.memento; }
            set { this.memento = value; }
        }
    }
}
