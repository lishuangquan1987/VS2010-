using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;

namespace 文件流操作练习
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            OpenFileDialog op = new OpenFileDialog();
            if (op.ShowDialog() != System.Windows.Forms.DialogResult.OK)
                return;
            ReadFile(op.FileName);
        }
        void ReadFile(string FileName)
        {
            FileStream fs_read = new FileStream(FileName, FileMode.Open, FileAccess.Read);
            byte[] buffer = new byte[1024*1000];
            bool mark=true;
            while(true)
            {
               int n= fs_read.Read(buffer, 0, buffer.Length);
               if (n <= 0)
               {
                   mark = false;
               }
            }
        }
    }
}
