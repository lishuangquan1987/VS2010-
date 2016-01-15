using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Reflection;
using LibCom;
namespace 反射应用
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        ImyInterface instance;
        private void button1_Click(object sender, EventArgs e)
        {
            Assembly asm = Assembly.LoadFile(Application.StartupPath+"\\MyDLL.dll");
            
            foreach (Type t in asm.GetTypes())
            {
                if (t.GetInterface("ImyInterface") != null)
                {
                    instance = asm.CreateInstance(t.FullName) as ImyInterface;
                }
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            string result = instance.add(3, 4).ToString();
            MessageBox.Show(result);
        }
    }
}
