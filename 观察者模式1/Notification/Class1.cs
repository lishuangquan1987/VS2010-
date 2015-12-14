using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Notification
{
    public delegate void NotificationEntry(Notification_par par);
    public class NotificationCenter
    {
        private static NotificationCenter sigle_notificationCenter = new NotificationCenter();
        private List<NotificationEntry> notificationentrys = new List<NotificationEntry>();
        private NotificationCenter()
        { 
        }
        /// <summary>
        /// 单例模式
        /// </summary>
        /// <returns></returns>
        public static NotificationCenter GetInstance()
        {
            return sigle_notificationCenter;
        }
        public void PostNotice(Notification_par par)
        {
            foreach (NotificationEntry i in notificationentrys)
            {
                if (i != null)
                    i(par);
            }
        }
        public void AddObserver(NotificationEntry nf)
        {
            notificationentrys.Add(nf);
        }

    }
    /// <summary>
    /// 用作委托的参数
    /// </summary>
    public class Notification_par
    {
        public string name;
        public object context;
        public Notification_par(string name,object context)
        {
            this.name=name;
            this.context=context;
        }
    }
    public class DIC : Dictionary<string, object>
    {
 
    }
}
