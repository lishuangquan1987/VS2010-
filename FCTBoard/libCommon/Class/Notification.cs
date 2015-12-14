using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TestStudio.Automation.TestManager.libCommon.Class
{
    public delegate int NotificationEntry(Notification nf);
    public struct List_NotifiEntry
    {
        public string name;
        public NotificationEntry entry;
        public object obj;
        public List_NotifiEntry(string name, NotificationEntry entry, object sender)
        {
            this.name = name;
            this.entry = entry;
            this.obj = sender;
        }
        public List_NotifiEntry(string name, NotificationEntry entry)
        {
            this.name = name;
            this.entry = entry;
            this.obj = null;
        }
    }

    public class Notification
    {
        public string name;
        public object context;
        public Notification(string name, object context)
        {
            this.name = name;
            this.context = context;
        }
    }
    public class NotificationCenter
    {
        List<List_NotifiEntry> m_ListRegisterNotification=new List<List_NotifiEntry>();
        static NotificationCenter defaultCenter=new NotificationCenter();
        static NotificationCenter()
        {

        }

        static public NotificationCenter  DefaultCenter()
        {
            return defaultCenter;
        }
        public int AddObserver(string name, NotificationEntry entry, object sender)
        {
            List_NotifiEntry list;
            list.name = name;
            list.obj = sender;
            list.entry = entry;
            m_ListRegisterNotification.Add(list);
            return 0;
        }

        public int AddObserver(string name, NotificationEntry entry)
        {
            return AddObserver(name, entry,null);
        }

        public int PostNotification(string name, object context, object obj)
        {
            Notification nf = new Notification(name, context);
            foreach (List_NotifiEntry l in m_ListRegisterNotification)
            {
                if (name == l.name)
                {
                    if (l.obj != null)
                    {
                        if (l.obj != obj)
                            continue;
                    }
                    l.entry(nf);
                }
            }
            return 0;
        }
        public int PostNotification(string name, object context)
        {
            return PostNotification(name, context, null);
        }

        public void Notification2Log(int nid, string msg)
        {
            DictionaryEx dic = new DictionaryEx();
            dic["id"] = 0;
            dic["msg"] = msg;
            NotificationCenter.DefaultCenter().PostNotification(tmMarcos.kDegbugMessage, dic);
        }

        public void Notification2Statue(int nid, string msg)
        {
            DictionaryEx dic = new DictionaryEx();
            dic["id"] = 0;
            dic["msg"] = msg;
            NotificationCenter.DefaultCenter().PostNotification(tmMarcos.kTestFlowMessage, dic);
        }
    }
}
