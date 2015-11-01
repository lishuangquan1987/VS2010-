using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace TCP实现文件的传输
{
    public partial class Friends : UserControl
    {
        public Friends()
        {
            InitializeComponent();
        }
        delegate string GetSelectText();
        string getSelectText()
        {
            string text = "";
            if (this.listBox1.InvokeRequired)
                this.listBox1.Invoke(new GetSelectText(getSelectText));
            else
                text= this.listBox1.SelectedItem.ToString();
            return text;
        }
        
        public string SelectText
        {
            get
            {
                return getSelectText();
            }

        }
    }
}
