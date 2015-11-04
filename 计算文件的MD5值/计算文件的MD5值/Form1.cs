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
            this.textBox1.Text = op.FileName;
            Thread.Sleep(1);
            //new Thread(() => { ReadFileAndCalculateMD5(op.FileName, 1024 * 1024 * 10,"md5"); }).Start();//10M
            new Thread(() => { Start(op.FileName); }).Start();
        }
        HashAlgorithm md5 = new MD5CryptoServiceProvider();
        string CalculateMD5(Stream stream,string type)
        {
            string s = "";
            if (type.ToLower() == "md5")
                md5 = new MD5CryptoServiceProvider();
            else
                md5 = SHA1.Create();
            byte[] resultdata = md5.ComputeHash(stream);
            foreach (byte i in resultdata)
            {
                s += i.ToString("X2");
            }
            md5.Clear();
            return s;
        }
        delegate void UpdateStatusLable(string msg);
        void updateStatusLable(string msg)
        {
            if (this.InvokeRequired)
                this.Invoke(new UpdateStatusLable(updateStatusLable), msg);
            else
                this.toolStripStatusLabel1.Text = msg;
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
        void ReadFileAndCalculateMD5(FileStream fl_read,long ReadSize,string type)
        {
            lock (this)
            {
                if (type.ToLower() == "md5")
                    md5 = new MD5CryptoServiceProvider();
                else
                    md5 = SHA1.Create();
               
                if (fl_read.Length < ReadSize)//文件过小时，直接一步读取
                {
                    string result = CalculateMD5(fl_read,type);
                    if (type.ToLower() == "md5")
                    updateTextBox("MD5:" + result, Color.Green, true);
                    else if(type.ToLower()=="sha1")
                    updateTextBox("SHA1:" + result, Color.Green, true);
                    Thread.Sleep(1);
                    updateProcessBar(100);
                    return;
                }
                int ReadTimes;
                int s = (int)(fl_read.Length % ReadSize);
                ReadTimes = (int)((fl_read.Length - s) / ReadSize);
                byte[] temp_buffer = new byte[ReadSize];
                byte[] output_buffer = new byte[ReadSize];
                for (int i = 1; i <= ReadTimes; i++)
                {
                    temp_buffer = new byte[ReadSize];
                    int n = fl_read.Read(temp_buffer, 0, (int)ReadSize);
                    int readbyte = md5.TransformBlock(temp_buffer, 0, n, output_buffer, 0);
                    int value = (int)(((decimal)i / ReadTimes) * 100);
                    updateProcessBar(value);
                }
                int left = fl_read.Read(temp_buffer, 0, (int)ReadSize);
                byte[] md5byte = md5.TransformFinalBlock(temp_buffer, 0, left);
                string _result = BitConverter.ToString(md5.Hash);
                md5.Clear();
                _result = _result.Replace("-", "");
                if (type.ToLower() == "md5")
                    updateTextBox("MD5:" + _result, Color.Green, true);
                else if (type.ToLower() == "sha1")
                    updateTextBox("SHA1:" + _result, Color.Green, true);
            }
        }

        void Start(string path)
        {
            int startTime = System.Environment.TickCount;
            updateTextBox("文件路劲:" + path, Color.Blue, true);
            FileStream fl_read = new FileStream(path, FileMode.Open, FileAccess.Read);
            updateTextBox("文件大小:" + (((decimal)fl_read.Length / 1024) / 1024).ToString() + "M", Color.Blue, true);
            if (this.checkBox1.Checked)
            {
                updateStatusLable("正在计算MD5...");
                ReadFileAndCalculateMD5(fl_read, 1024 * 1024 * 10, "md5");
                if (fl_read != null)
                    fl_read.Close();
            }

            if (this.checkBox2.Checked)
            {
                updateStatusLable("正在计算SHA1...");
                fl_read = new FileStream(path, FileMode.Open, FileAccess.Read);
                ReadFileAndCalculateMD5(fl_read, 1024 * 1024 * 10, "sha1");
                if (fl_read != null)
                    fl_read.Close();
            }
            int endTime = System.Environment.TickCount;
            decimal useTime=(decimal)(endTime-startTime)/1000;
            updateStatusLable("计算完成，本次用时:"+useTime.ToString()+"s");

        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            if (MessageBox.Show("Made By Tony\r\nAuthor's QQ:294388344\r\nDo you want to go to Author's QQ Space?", "Author's Information", MessageBoxButtons.OKCancel, MessageBoxIcon.Information) == System.Windows.Forms.DialogResult.OK)
            {
                System.Diagnostics.Process.Start("http://294388344.qzone.qq.com", "explorer.exe");
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }
        
    
    }
}
