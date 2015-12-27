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

namespace luainterface多个Dofile
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        Lua lua = MyLua.GetInstance();
        private void Form1_Load(object sender, EventArgs e)
        {
            MyLua.GetInstance().DoFile("test2.lua");
            MyLua.GetInstance().DoFile("test1.lua");
            
        }

        private void button1_Click(object sender, EventArgs e)
        {
            MessageBox.Show(MyLua.GetInstance()["version"].ToString());

        }

        private void button2_Click(object sender, EventArgs e)
        {
            MessageBox.Show(MyLua.GetInstance()["name"].ToString());
        }

    }
    public class MyLua : LuaInterface.Lua
    {
        private static MyLua lua = null;
        private MyLua()
        { 
        }
        public static MyLua GetInstance()
        {
            if (lua == null)
                lua = new MyLua();
            return lua;
        }
    }
}
