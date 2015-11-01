using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Log_Analysizer
{
    public partial class ResultBox : UserControl
    {
        public ResultBox()
        {
            InitializeComponent();
        }
        private delegate void Delegate_ShowMessage(Color color, string msg,bool nextline);
        public void ShowMessage(Color color, string msg,bool nextline)
        {
            if (this.richTextBox1.InvokeRequired)
                this.richTextBox1.Invoke(new Delegate_ShowMessage(ShowMessage), color, msg,nextline);
            else
            {
                this.richTextBox1.SelectionColor = color;
                if (nextline)
                    this.richTextBox1.AppendText(msg + "\r\n");
                else
                    this.richTextBox1.AppendText(msg);
                this.richTextBox1.Focus();
            }
        }
        private delegate void Delegate_ClearMessage();
        public void ClearMessage()
        {
            if (this.richTextBox1.InvokeRequired)
                this.Invoke(new Delegate_ClearMessage(ClearMessage));
            else
            {
                this.richTextBox1.Clear();
                this.richTextBox1.Focus();
            }
        }
    }
}
