using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Security.Policy;
using System.Security.Cryptography;
using System.IO;

namespace 计算文件的MD5值
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
            op.Multiselect = false;
            if (op.ShowDialog() != System.Windows.Forms.DialogResult.OK)
                return;
           string _result= CalculateMD5(op.FileName);
           
            MessageBox.Show(_result);
            
        }
        MD5 md5 = new MD5CryptoServiceProvider();
        FileStream fl_read;
        byte[] result;
        string s = "";
        string CalculateMD5(string FileName)
        {
            s = "";
            fl_read = new FileStream(FileName, FileMode.Open, FileAccess.Read);
            result = md5.ComputeHash(fl_read);
            if(fl_read!=null)
            fl_read.Close();
            foreach (byte i in result)
            {
                s += i.ToString("X2");
            }
            return s;
        }
    }
}
