using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace 窗体传值1
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        Form2 f;
        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            f = new Form2();
            f.textBox1.TextChanged += textBox1_TextChanged;
            f.ShowDialog();
        }

        void textBox1_TextChanged(object sender, EventArgs e)
        {
            this.textBox1.Text = f.textBox1.Text;
        }
    }
}
