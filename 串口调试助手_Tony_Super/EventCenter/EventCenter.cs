using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace N_EventCenter
{
    public delegate void dele_event(Par par);
    public class EventCenter
    {
        #region~单例模式（最简单的，不考虑多线程）
        private static EventCenter single_instance = null;
        public static EventCenter GetInstance()
        {
            if (single_instance == null)
                single_instance = new EventCenter();
            return single_instance;
        }
        private EventCenter()
        { 
        }
            
        #endregion
        private List<EventEntry> register_Events = new List<EventEntry>();//放置所有已经注册的事件以及名称
        /// <summary>
        /// 注册事件
        /// </summary>
        /// <param name="name">事件名称</param>
        /// <param name="method">注册的方法（委托类型）</param>
        public void AddObserver(string name,dele_event method)
        {
            EventEntry eventEntry = new EventEntry(name, method);
            register_Events.Add(eventEntry);
        }
        /// <summary>
        /// 调用方法触发事件
        /// </summary>
        /// <param name="name"></param>
        /// <param name="dic"></param>
        public void PostNotification(string name,Dic dic)
        {
            Par par=new Par(name,dic);
            foreach (EventEntry i in register_Events)
            {
                if (i._name == name)
                    i._method(par);
            }
        }
    }
   /// <summary>
   /// 将委托与name绑定在一起
   /// </summary>
    public class EventEntry
    {
        public string _name;
        public dele_event _method;
        public EventEntry(string name,dele_event method)
        {
            _name = name;
            _method = method;
        }
    }
    /// <summary>
    /// 委托参数
    /// </summary>
    public class Par
    {
        public string _name;
        public object _context;
        public Par(string name, object context)
        {
            this._name = name;
            this._context = context;
        }
    }
    /// <summary>
    /// 字典简化
    /// </summary>
    public class Dic:Dictionary<string,object>
    {
    }
    public class EventName
    {
        public static string UpdateUI = "str_updateUI";
        public static string ConfigChange = "ConfigChange";

        #region~更新界面的参数
        public static string msg = "msg";
        public static string color = "color";
        public static string nextline = "nextline";
        #endregion
    }
}
