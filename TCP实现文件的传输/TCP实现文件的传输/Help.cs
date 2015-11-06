using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace TCP实现文件的传输
{
    public partial class Help : Form
    {
        public Help()
        {
            InitializeComponent();
        }
        string helpMessage1 = "1.建立主机，端口号默认为8888，用一个slave输入主机的IP地址和端口号，点击连接测试，出现连接成功即可开始聊天！\r\n";
        string helpMessage2 = "2.按F5可以换皮肤，在根目录选择不同的SSK文件，可以体验不同风格！\r\n";
        string helpMessage3 = "3.一个slave只能连接一个host主机，一个host可以被多个slave连接！\r\n";
        private void Help_Load(object sender, EventArgs e)
        {
            this.richTextBox1.Text = helpMessage1 + helpMessage2+helpMessage3;
        }
    }
}
