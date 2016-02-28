using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Diagnostics;

namespace dpcmd.bat
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string filename=@"C:\MiTac\A115_windowspro20160120\TestManager\Output\bin\Debug\DPcmd\dpcmd1.bat";
            Process.Start(filename);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.textBox1.Focus();
            textBox1.Select(0,textBox1.Text.Length) ;
        }
    }
}
