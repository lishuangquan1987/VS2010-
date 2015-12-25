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

namespace c_sharp调用lua
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Lua lua = new Lua();
            lua.DoFile("test.lua");
            LuaFunction lf = lua.GetFunction("test");
            object[] result = lf.Call();
            MessageBox.Show(result[0].ToString());
        }
    }
}
