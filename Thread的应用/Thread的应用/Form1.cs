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
using System.Reflection;

namespace Thread的应用
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        //Lua lua;
        public delegate void UpdateCheckBoxes(string value,int ID);
        public void updateCheckBoxes(string value,int ID)
        {
            
            if (this.InvokeRequired)
                this.Invoke(new UpdateCheckBoxes(updateCheckBoxes), value,ID);
            else
            {
                switch (ID)
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
                    case 1: richTextBox1.AppendText(DateTime.Now.ToString()+":UUT0:"); break;
                    case 2: richTextBox1.AppendText(DateTime.Now.ToString() + ":UUT1:"); break;
                    case 3: richTextBox1.AppendText(DateTime.Now.ToString() + ":UUT2:"); break;
                    case 4: richTextBox1.AppendText(DateTime.Now.ToString() + ":UUT3:"); break;
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
           

            Thread.Sleep(500);
            if (checkBox1.Checked)
            {

                new Thread(() => { Lua lua; doFile(out lua); lua["ID"] = 1; LuaFunction luafunction = lua.GetFunction("Main"); luafunction.Call(); }) { Name = "1" }.Start();

            }

            if (checkBox2.Checked)
            {
                new Thread(() => { Lua lua; doFile(out lua); lua["ID"] = 2; LuaFunction luafunction = lua.GetFunction("Main"); luafunction.Call(); }) { Name = "2" }.Start();

            }
            if (checkBox3.Checked)
            {
                new Thread(() => { Lua lua; doFile(out lua); lua["ID"] = 3; LuaFunction luafunction = lua.GetFunction("Main"); luafunction.Call(); }) { Name = "3" }.Start();

            }
            if (checkBox4.Checked)
            {
                new Thread(() => { Lua lua; doFile(out lua); lua["ID"] = 4; LuaFunction luafunction = lua.GetFunction("Main"); luafunction.Call(); }) { Name = "4" }.Start();

            }

            
        }
        public int GetID()
        {
            if (Thread.CurrentThread.Name == null)
                return 100;
            else
            return int.Parse(Thread.CurrentThread.Name);
        }
        void RegisterFunction(Lua lua)
        {
            lua.RegisterFunction("updateCheckBoxes", this, this.GetType().GetMethod("updateCheckBoxes"));
            lua.RegisterFunction("Delay", this, this.GetType().GetMethod("Delay"));
            lua.RegisterFunction("Lock", this, this.GetType().GetMethod("Lock"));
            lua.RegisterFunction("UnLock", this, this.GetType().GetMethod("UnLock"));
            lua.RegisterFunction("DbgOut", this, this.GetType().GetMethod("DbgOut"));
            lua.RegisterFunction("GetID", this, this.GetType().GetMethod("GetID"));
        }
        void doFile(out Lua lua)
        {
            lua = new Lua();
            lua.DoFile("test.lua");
            RegisterFunction(lua);

            
            
        }
        private void Form1_Load(object sender, EventArgs e)
        {
           // doFile();
        }
        public int[] AAA()
        {
            return new int[] { 1, 2, 3 };
        }
        public void msgbox(string msg)
        {
            MessageBox.Show(msg);
        }
        private void button2_Click(object sender, EventArgs e)
        {
            Lua lua1 = new Lua();
            lua1.DoFile("test1.lua");
            lua1.RegisterFunction("msgbox", this, this.GetType().GetMethod("msgbox"));
            lua1.RegisterFunction("AAA", this, this.GetType().GetMethod("AAA"));
            object result= lua1.GetFunction("Test").Call();
        }
        public Form1 GetNewForm()
        {
            return this;
        }
        public void _Show(Form1 form)
        {
            form.Show(); 
        }
        private void button2_Click_1(object sender, EventArgs e)
        {
            Lua lua2 = new Lua();
            lua2.DoFile("Test2.lua");
            lua2.RegisterFunction("GetNewForm", this, this.GetType().GetMethod("GetNewForm"));
            lua2.RegisterFunction("_Show", this, this.GetType().GetMethod("_Show"));
            lua2.RegisterFunction("GetColor", this, this.GetType().GetMethod("GetColor"));
            lua2.GetFunction("Test2").Call();
        }
       public void Test()
        {
            MessageBox.Show("这是lua调用没有登记的Test方法！");
        }
       public Color GetColor(string color)
       {
           if (color == "red")
               return Color.Red;
           else if (color == "green")
               return Color.Green;
           else
               return Color.LightCyan;
       }
       public void ShowMsg(int[] values)
       {
           foreach (int i in values)
           {
               MessageBox.Show(i.ToString());
           }
       }
       private void button3_Click(object sender, EventArgs e)
       {
           Lua lua3 = new Lua();
           lua3.DoFile("test3.lua");
           LuaFunction l= lua3.RegisterFunction("AAA", this, this.GetType().GetMethod("AAA"));
           object[] r= l.Call();
           lua3.RegisterFunction("msgbox", this, this.GetType().GetMethod("msgbox"));
           lua3.RegisterFunction("ShowMsg",this, this.GetType().GetMethod("ShowMsg"));
           lua3.GetFunction("test").Call();
           LuaTable i = lua3["str"] as LuaTable;
           object j = i["er"];
           MessageBox.Show(i["er"].GetType().ToString());
           MessageBox.Show(i.ToString());

       }
    }
}
