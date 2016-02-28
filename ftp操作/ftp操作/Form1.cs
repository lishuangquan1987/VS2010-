using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Net;
using System.Globalization;

namespace ftp操作
{
    public partial class Form1 : Form
    {
        FtpWeb ftp;
        public Form1()
        {
            InitializeComponent();
            
        }
        bool TextValid(string text)
        {
            if (text == null)
                return false;
            if (text == "")
                return false;
            return true;
        }
        private void button_Connect_Click(object sender, EventArgs e)
        {
            if(!TextValid(textBox_address.Text)||!TextValid(textBox_pwd.Text)||!TextValid(textBox_user.Text))
            {
                MessageBox.Show("地址或者用户名、密码不能为空！");
                return;
            }
            string ftp_address = textBox_address.Text;
            string ftp_username = textBox_user.Text;
            string ftp_pwd = textBox_pwd.Text;
            ftp = new FtpWeb(ftp_address, null, ftp_username, ftp_pwd);
            string[] result = ftp.GetDirectoryList();
            foreach (string i in result)
            {
                this.richTextBox1.AppendText(i+"\r\n");
            }
        }
    }

}
