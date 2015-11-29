using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using LuaInterface;


namespace lua调用C_Sharp返回值为数组的函数
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        public void msgbox(string msg)
        {
            MessageBox.Show(msg);
        }
       
        private void button1_Click(object sender, EventArgs e)
        {
            Lua lua = new Lua();
            ////lua  = new Lua();
            //lua.DoFile("test.lua");
            //lua.RegisterFunction("msgbox", this, this.GetType().GetMethod("msgbox"));
            //lua.RegisterFunction("AAA", this, this.GetType().GetMethod("AAA"));
            //((LuaFunction)lua["Test"]).Call();

        }
        public int[] AAA()
        {
            return new int[] { 1, 2, 3 };
        }
    }
}
