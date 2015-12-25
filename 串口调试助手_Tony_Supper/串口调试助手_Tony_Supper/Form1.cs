using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using N_EventCenter;

namespace 串口调试助手_Tony_Supper
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Dic dic=new Dic();
            dic["msg"]="哈哈,调用成功";
            dic["color"]=Color.Red;
            dic["nextline"]=true;
            EventCenter.GetInstance().PostNotification(EventName.UpdateUI,dic);
        }

    }
}
