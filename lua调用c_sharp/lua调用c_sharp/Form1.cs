using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using LuaInterface;
using System.Threading;

namespace lua调用c_sharp
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        Lua lua;
        delegate void UpdateTextBox(string msg);
        public void updateTextBox(string msg)
        {
            if (this.InvokeRequired)
                this.Invoke(new UpdateTextBox(updateTextBox), msg);
            else
                this.textBox1.Text = msg;
        }
        public void Delay(int ms)
        {
            System.Threading.Thread.Sleep(ms);
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            lua = new Lua();
            lua.DoFile("test.lua");
            lua.RegisterFunction("updateTextBox", this, this.GetType().GetMethod("updateTextBox"));
            lua.RegisterFunction("Delay", this, this.GetType().GetMethod("Delay"));
        }

        private void button1_Click(object sender, EventArgs e)
        {
            LuaFunction l = lua.GetFunction("run");
            new Thread(() => { l.Call(); }).Start();
        }
    }
    
}
