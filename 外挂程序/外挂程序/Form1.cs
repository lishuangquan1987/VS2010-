using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using LuaInterface;

namespace 外挂程序
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            

        }
        Lua lua ;
        string[] Content;
        int CurrentIndex;

        public string _Text
        {
            get { return this.textBox1.Text;}
        }
        private void button1_Click(object sender, EventArgs e)
        {
            Point p = new Point(0, 0);
            Method.GetCursorPos(ref p);
            LuaFunction lf = lua.GetFunction("Dotest");
            lf.Call();
            if (CurrentIndex == Content.Length - 1)
                CurrentIndex = 0;
            CurrentIndex++;
            this.textBox1.Text = Content[CurrentIndex];
            Method.SetCursorPos(p.X, p.Y);

        }
        #region~封装方法
        public void SelectTextAndDelete(int beforeX, int beforeY, int afterX, int afterY)
        {
            Method.SetCursorPos(beforeX, beforeY);
            Method.mouse_event(2, 0, 0, 0, 0);//鼠标左键按下
            Method.mouse_event(4, 0, 0, 0, 0);//鼠标左键抬起
            for (int i = 0; i <= 30; i++)
            {
                Method.keybd_event((byte)Keys.Back, 0, 0, 0);
                Delay(10);
                Method.keybd_event((byte)Keys.Back, 0, 2, 0);
            }
            
            //Method.mouse_event(2, 0, 0, 0, 0);//鼠标左键按下
            //Method.SetCursorPos(afterX, afterY);
            //Method.mouse_event(4, 0, 0, 0, 0);//鼠标左键抬起
            //_KeyPress(Keys.Delete);
            
        }
        private void _KeyPress(Keys key)
        {
            Method.keybd_event((byte)key, 0, 0, 0);
           // Delay(500);
            Method.keybd_event((byte)key, 0, 2, 0);
        }
        private void _KeyPress(Keys key1, Keys key2)
        {
            Method.keybd_event((byte)key1, 0, 0, 0);
            Method.keybd_event((byte)key2, 0, 0, 0);
            Method.keybd_event((byte)key2, 0, 2, 0);
            Method.keybd_event((byte)key1, 0, 2, 0);
 
        }
        public void Paste(int x,int y,string content)
        {
            Clipboard.SetDataObject(content);//将内容放到剪切板
            Method.SetCursorPos(x, y);

            Method.mouse_event(2, 0, 0, 0, 0);
            Method.mouse_event(4, 0, 0, 0, 0);

            Method.keybd_event((byte)Keys.ControlKey,0,0,0);
            Method.keybd_event((byte)Keys.V, 0, 0, 0);
            Method.keybd_event((byte)Keys.V, 0, 2, 0);
            Method.keybd_event((byte)Keys.ControlKey, 0, 2, 0);
        }
        public void ClickButton(int x, int y)
        {
           Method.SetCursorPos(x, y);
           Method.mouse_event(2, 0, 0, 0, 0);//鼠标左键按下
           Method.mouse_event(4, 0, 0, 0, 0);//鼠标左键抬起
        }
        public void Delay(int ms)
        {
            System.Threading.Thread.Sleep(ms);
        }
        #endregion

        private void Form1_Load(object sender, EventArgs e)
        {
            lua = new Lua();
            try
            {
                lua.DoFile("test.lua");
            }
            catch(Exception ee)
            {
                MessageBox.Show(ee.Message);
            }
            lua["method"] = this;
            LuaTable lt = lua.GetTable("Content");
            Content=new string[lt.Values.Count];
            for (int i = 0; i < lt.Values.Count; i++)
            {
                Content[i] = lt[i + 1] as string;
            }
            this.textBox1.Text = Content[CurrentIndex];
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Point p = new Point(0, 0);
            Method.GetCursorPos(ref p);
            LuaFunction lf = lua.GetFunction("Dotest");
            lf.Call();
            if (CurrentIndex == 0)
                CurrentIndex = Content.Length-1;
            CurrentIndex--;
            this.textBox1.Text = Content[CurrentIndex];
            Method.SetCursorPos(p.X, p.Y);
        }
    }
    public class Method
    {
        [DllImport("user32.dll")]
        public static extern bool ShowWindow(IntPtr hwnd, int cmdshow);
        [DllImport("user32.dll")]
        public static extern IntPtr FindWindow(string lpClassName, string lpWindowName);
        [DllImport("user32.dll")]
        public static extern bool SetForegroundWindow(IntPtr hWnd);
        [DllImport("user32.dll")]
        public static extern bool SetWindowPos(IntPtr hWnd, IntPtr hWndInsertAfter, int X, int Y, int cx, int cy, uint uFlags);
        [DllImport("user32.dll")]
        public static extern bool GetCursorPos(ref Point p);
        [DllImport("user32.dll")]
        public static extern bool SetCursorPos(int x, int y);
        [DllImport("user32.dll", CharSet = CharSet.Auto, CallingConvention = CallingConvention.StdCall)]
        public static extern void mouse_event(int dwFlags, int dx, int dy, int cButtons, int dwExtraInfo);
        [DllImport("user32.dll")]
        public static extern IntPtr SetFocus(IntPtr hWnd);
        /// <summary>
        /// dwFlags,0,按下，2，抬起，其他为0
        /// </summary>
        /// <param name="bVk"></param>
        /// <param name="bScan"></param>
        /// <param name="dwFlags"></param>
        /// <param name="dwExtraInfo"></param>
        [DllImport("User32.dll")]
        public static extern void keybd_event(Byte bVk, Byte bScan, Int32 dwFlags, Int32 dwExtraInfo);


        //移动鼠标 
      public  static int MOUSEEVENTF_MOVE = 0x0001;
        //模拟鼠标左键按下 
      public  static int MOUSEEVENTF_LEFTDOWN = 0x0002;
        //模拟鼠标左键抬起 
      public  static int MOUSEEVENTF_LEFTUP = 0x0004;
        //模拟鼠标右键按下 
      public  static int MOUSEEVENTF_RIGHTDOWN = 0x0008;
        //模拟鼠标右键抬起 
      public  static int MOUSEEVENTF_RIGHTUP = 0x0010;
        //模拟鼠标中键按下 
      public  static int MOUSEEVENTF_MIDDLEDOWN = 0x0020;
        //模拟鼠标中键抬起 
      public  static int MOUSEEVENTF_MIDDLEUP = 0x0040;
        //标示是否采用绝对坐标 
      public static int MOUSEEVENTF_ABSOLUTE = 0x8000; 
    }

}
