using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Notice
{
    public delegate void dele_noticeCenter(Parameter par);
    public class Noticer
    {
        private List<dele_noticeCenter> list_registers = new List<dele_noticeCenter>();
        private static Noticer notice = new Noticer();
        public static Noticer GetInstance()
        {
            return notice;
        }
        private Noticer()
        {
        }
        public void AddObserver(dele_noticeCenter func)
        {
            list_registers.Add(func);
        }
        public void PostFunction(Parameter par)
        {
            foreach (dele_noticeCenter i in list_registers)
            {
                
            }
        }
    }
    public class Parameter
    {
       public string name;
       public object context;
        public Parameter(string name,object context)
        {
            this.name=name;
            this.context=context;
        }
    }
}
