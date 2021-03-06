﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using N_EventCenter;

namespace 串口调试助手_Tony_Supper
{
    public partial class MainText : UserControl
    {
        public MainText()
        {
            InitializeComponent();
            EventCenter.GetInstance().AddObserver(EventName.UpdateUI, Add_Updatetext);
        }
        private delegate void UpdateText(string msg,Color color,bool nextline);
        public void Updatetext(string msg,Color color,bool nextline)
        {
            if (InvokeRequired)
                this.Invoke(new UpdateText(Updatetext), msg,color,nextline);
            else
            {
                this.richTextBox1.SelectionColor = color;
                if (nextline)
                    this.richTextBox1.AppendText(msg + "\r");
                else
                    this.richTextBox1.AppendText(msg);
                this.Focus();
            }
        }
        void Add_Updatetext(Par par)
        {
            Dic dic = par._context as Dic;
            string msg = dic[EventName.msg] as string;
            Color color = (Color)dic[EventName.color];
            bool nextline =(bool)dic[EventName.nextline];
            Updatetext(msg, color, nextline);
        }
    }
}
