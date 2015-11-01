using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Windows.Forms;

namespace 串口调试助手_Tony
{
    public partial class Form1
    {
       public delegate void delegate_msgbox(string msg, bool nextline, Color color);
       public static event delegate_msgbox event_msgbox;
       public static void msgbox(string msg, bool nextline, Color color)
       {
           if (event_msgbox != null)
               event_msgbox(msg, nextline, color);
       }
    }
}
