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
            PropertyInfo property = typeof(People).GetProperties()[0];
            MessageBox.Show(property.Name);

        }
    }
    public class People
    {
        public string name = "张三";
    }
}
