using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using 

namespace CyPressProgrammer1
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        string FileName = "";
        str
        private void button1_Click(object sender, EventArgs e)
        {
            OpenFileDialog op = new OpenFileDialog();
            op.Filter = "hex文件|*.hex|bin文件|*.bin";
            op.Multiselect = false;
            op.Title = "请选择bin文件";
            if (op.ShowDialog() != DialogResult.OK) return;
            FileName = op.FileName;
            this.textBox1.Text = FileName;
        }
    }
}
