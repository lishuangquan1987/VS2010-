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
        string CalculateMD5(Stream stream)
        {
            string s = "";
            byte[] resultdata = md5.ComputeHash(stream);
            foreach (byte i in resultdata)
            {
                s += i.ToString("X2");
            }
            return s;
        }
        delegate void UpdateProcessBar(int value);
        void updateProcessBar(int value)
        {
            if (this.InvokeRequired)
                this.Invoke(new UpdateProcessBar(updateProcessBar), value);
            else
            {
                this.progressBar1.Value = value;
                this.label1.Text = string.Format("当前进度：{0}%", value);
            }
        }
        delegate void UpdateTextBox(string msg, Color color, bool nextline);
        void updateTextBox(string msg, Color color, bool nextline)
        {
            if (this.InvokeRequired)
                this.Invoke(new UpdateTextBox(updateTextBox), msg, nextline);
            else
            {
                this.richTextBox1.SelectionColor = color;
                this.richTextBox1.AppendText(msg);
                if (nextline)
                    this.richTextBox1.AppendText("\r\n");
                this.richTextBox1.Focus();
            }
        }
        void ReadFileAndCalculateMD5(string FileName,long ReadSize)
        {
            FileStream fl_read = new FileStream(FileName, FileMode.Open, FileAccess.Read);
            FileStream fl_output;
            if(fl_read.Length<ReadSize)
            {
                string result = CalculateMD5(fl_read);
                updateTextBox("MD5:" + result, Color.Green, true);
                updateProcessBar(100);
                if (fl_read != null)
                    fl_read.Close();
                return;
            }

        }
        
    
        
    
    }
}
