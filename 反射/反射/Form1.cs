using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Reflection;

namespace 反射
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            Assembly asm = Assembly.GetAssembly(typeof(People));
            //MessageBox.Show(asm.GetType().GetProperties()[0].Name);
            //People p = asm.CreateInstance("反射.People") as People;
            //PropertyInfo property = typeof(反射.People).GetProperties()[0];
            //MessageBox.Show(property.Name);
            //MessageBox.Show(p.name);

            People p;

            foreach (Type t in asm.GetTypes())
            {
                if (t.GetInterface("ImyInterface") != null)
                {
                    p = asm.CreateInstance(t.FullName) as People;
                    MessageBox.Show(p.name);
                }
            }

        }
    }
    public class People:IDisposable,ImyInterface
    {
        public string name = "张三";

        public void Dispose()
        {
            throw new NotImplementedException();
        }

        public void Test()
        {
            MessageBox.Show("test");
        }
    }
    public interface ImyInterface
    {
        void Test();
    }
}
