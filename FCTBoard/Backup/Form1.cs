using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using LuaInterface;

namespace LuaTestPrj
{
    public partial class Form1 : Form
    {
        Lua lua;
        CSharpObject csharpobj;
        CSharpObject second;
        CSharpObject obj;
        public Form1()
        {
            InitializeComponent();
            lua = new Lua();
            csharpobj = new CSharpObject("First Object");
            obj = csharpobj;
            second = new CSharpObject("Second Object");
        }

        private void button1_Click(object sender, EventArgs e)
        {
            CSharpObject o = lua["obj"] as CSharpObject;
            o.Msgbox();
            MessageBox.Show((string)lua["str"]);
            obj = second;
            lua["obj"] = obj;
            double y = lua.GetNumber("y");
        }

        private void Form1_Load(object sender, EventArgs e)
        {
           
            lua["str"] = "this string is pushed by c#";
            lua["obj"] = obj;
            lua["obj2"] = second;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            //lua.DoString("str='adfafafasfa'");
            try
            {
                //lua.DoFile(@"C:\Users\Ryan\Documents\123.lua");
                lua.DoString("obj:Msgbox();");
            }
            catch (Exception err)
            {
                MessageBox.Show(err.Message);
            }
        }
    }
    public class CSharpObject
    {
        string Name;
        public CSharpObject(string id)
        {
            Name = id;
        }
        public int Add(int x, int y)
        {
            return x + y;
        }

        public void Msgbox()
        {
            MessageBox.Show("This function call from c# object,Name is : "+Name);
        }
    }
}

namespace MyWorkSpace
{
    public class TestClass
    {
        LuaTestPrj.CSharpObject Obj;
    }
}