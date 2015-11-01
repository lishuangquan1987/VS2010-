using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Model_Message
{
    public partial class TextBox : UserControl
    {
        public TextBox()
        {
            InitializeComponent();
        }
        private delegate void _Show(string msg, bool nextline, Color color);
        public void Show(string msg, bool nextline, Color color)
        {
            if (this.richTextBox1.InvokeRequired)
                this.Invoke(new _Show(Show), msg, nextline, color);
            else
            {
                this.richTextBox1.SelectionColor = color;
                if (nextline)
                {
                    this.richTextBox1.AppendText(msg + "\r\n");
                    
                }
                else
                    this.richTextBox1.AppendText(msg);
                this.richTextBox1.Focus();
            }
        }
    
    }
}
