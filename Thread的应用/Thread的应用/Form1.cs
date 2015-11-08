using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Threading;
using LuaInterface;

namespace Thread的应用
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        Lua lua;
        public delegate void UpdateCheckBoxes(string value);
        public void updateCheckBoxes(string value)
        {
            if (this.InvokeRequired)
                this.Invoke(new UpdateCheckBoxes(updateCheckBoxes), value);
            else
            {
                switch (GetID())
                {
                    case 1: checkBox1.Text = value; break;
                    case 2: checkBox2.Text = value; break;
                    case 3: checkBox3.Text = value; break;
                    case 4: checkBox4.Text = value; break;

                }
            }
        }
        delegate void UpdateRichTextBox(string msg, int ID);
        public void DbgOut(string msg, int ID)
        {
            if (this.InvokeRequired)
                this.Invoke(new UpdateRichTextBox(DbgOut), msg, ID);
            else
            {
                switch (ID)
                {
                    case 0: richTextBox1.AppendText(DateTime.Now.ToString()+":UUT0:"); break;
                    case 1: richTextBox1.AppendText(DateTime.Now.ToString() + ":UUT1:"); break;
                    case 2: richTextBox1.AppendText(DateTime.Now.ToString() + ":UUT2:"); break;
                    case 3: richTextBox1.AppendText(DateTime.Now.ToString() + ":UUT3:"); break;
                }
                richTextBox1.AppendText(msg + "\r\n");
                this.richTextBox1.Focus();
            }
        }
        public void Lock()
        {
            Monitor.Enter(this);
        }
        public void UnLock()
        {
            Monitor.Exit(this);
        }
        public void Delay(int value)
        {
            Thread.Sleep(value);
        }
        private void button1_Click(object sender, EventArgs e)
        {
            LuaFunction luafunction = lua.GetFunction("Main");

            Thread.Sleep(500);
            if (checkBox1.Checked)
            {

                new Thread(() => { luafunction.Call(); }) { Name = "1" }.Start();
                
            }

            if (checkBox2.Checked)
            {
                new Thread(() => { luafunction.Call(); }) { Name = "2" }.Start();
                
            }
            if (checkBox3.Checked)
            {
                new Thread(() => { luafunction.Call(); }) { Name = "3" }.Start();
                
            }
            if (checkBox4.Checked)
            {
                new Thread(() => { luafunction.Call(); }) { Name = "4" }.Start();
              
            }

            
        }
        public int GetID()
        {
            if (Thread.CurrentThread.Name == null)
                return 100;
            else
            return int.Parse(Thread.CurrentThread.Name);
        }
        void RegisterFunction()
        {
            lua.RegisterFunction("updateCheckBoxes", this, this.GetType().GetMethod("updateCheckBoxes"));
            lua.RegisterFunction("Delay", this, this.GetType().GetMethod("Delay"));
            lua.RegisterFunction("Lock", this, this.GetType().GetMethod("Lock"));
            lua.RegisterFunction("UnLock", this, this.GetType().GetMethod("UnLock"));
            lua.RegisterFunction("DbgOut", this, this.GetType().GetMethod("DbgOut"));
            lua.RegisterFunction("GetID", this, this.GetType().GetMethod("GetID"));
        }
        void doFile()
        {
            lua = new Lua();
            lua.DoFile("test.lua");
            RegisterFunction();

            
            
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            doFile();
        }
    }
}
