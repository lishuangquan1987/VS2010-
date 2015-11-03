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
using System.Threading;

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
            new Thread(() => { ReadFileAndCalculateMD5(op.FileName, 1024 * 1024 * 10); }).Start();//10M
          //ReadFileAndCalculateMD5(op.FileName,1024*1024*10)
            
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
            md5.Clear();
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
                this.Invoke(new UpdateTextBox(updateTextBox), msg,color,nextline);
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
            md5 = new MD5CryptoServiceProvider();
            updateTextBox(FileName, Color.Blue, true);
            FileStream fl_read = new FileStream(FileName, FileMode.Open, FileAccess.Read);
            //FileStream fl_output;
            if(fl_read.Length<ReadSize)
            {
                string result = CalculateMD5(fl_read);
                updateTextBox("MD5:" + result, Color.Green, true);
                updateProcessBar(100);
                if (fl_read != null)
                    fl_read.Close();
                return;
            }
            int ReadTimes;
            int s =(int)(fl_read.Length % ReadSize);
            ReadTimes=(int)((fl_read.Length-s)/ReadSize);
            byte[] temp_buffer = new byte[ReadSize];
            byte[] output_buffer=new byte[ReadSize];
            for (int i = 1; i <= ReadTimes; i++)
            {
                temp_buffer=new byte[ReadSize];
                int n=fl_read.Read(temp_buffer, 0, (int)ReadSize);
               int readbyte= md5.TransformBlock(temp_buffer, 0, n, output_buffer, 0);
               int value = (int)(((decimal)i / ReadTimes) * 100);
               updateProcessBar(value);
            }
            int left = fl_read.Read(temp_buffer, 0, (int)ReadSize);
            byte[] md5byte= md5.TransformFinalBlock(temp_buffer, 0, left);
            if (fl_read != null)
            {
                fl_read.Close();
            }
            string _result = BitConverter.ToString(md5.Hash);
            md5.Clear();
           
            updateTextBox("MD5:" + _result, Color.Green, true);
        }
        
    
        
    
    }
}
