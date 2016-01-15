using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace 事件的装载与卸载
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        public delegate void TestHandler();
        public event TestHandler testHandler;
        private void button1_Click(object sender, EventArgs e)
        {
            if (testHandler != null)
            {
                testHandler();
            }
        }
        void test()
        {
            MessageBox.Show("test");
        }

        private void button2_Click(object sender, EventArgs e)
        {
            testHandler += test;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            testHandler -= test;
        }
    }
}
