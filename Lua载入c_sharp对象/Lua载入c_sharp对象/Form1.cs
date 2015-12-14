using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using LuaInterface;

namespace Lua载入c_sharp对象
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        public static People people;
        public static Lua lua;
        public void msgbox(string msg)
        {
            MessageBox.Show(msg);
        }
        private void Form1_Load(object sender, EventArgs e)
        {

            people = new People();
            people.age = 12;
            people.name = "13909e3";
            lua = new Lua();


            lua.DoFile("test.lua");
            lua["people"] = people;
            //lua.DoString("A= require \"test1\"");
            
            


            LuaFunction lf = lua.GetFunction("showA");
            lf.Call(7, 8);
        }
        void test()
        {
            people = new People();
            lua = new Lua();
            people.name = "1111";
            people.age = 25;
            lua.DoFile("test.lua");
            lua["people"] = people;
            LuaFunction lf1 = lua.GetFunction("Eat");
            lf1.Call();
            LuaFunction lf2 = lua.GetFunction("cry");
            lf2.Call();

            Test t = new Test();
            t.Dosomething();           
        }
    
        
    }
    public class People
    {
        public int age;
        public string name;
        public void Eat()
        {
            MessageBox.Show(string.Format("一个{0}岁的人{1}在吃饭",age,name));
        }
        public void cry()
        {
            MessageBox.Show(string.Format("一个{0}岁的人{1}在哭", age, name));
        }
    }
    public class Test
    {
        public void Dosomething()
        {
            Form1.people.Eat();
        }
        public People p = Form1.lua["peple"] as People;
    }
}
