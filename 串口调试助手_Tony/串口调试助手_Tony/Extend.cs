using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace 串口调试助手_Tony
{
    public partial class Extend : UserControl
    {
        public Extend()
        {
            InitializeComponent();
        }

        private void Extend_Load(object sender, EventArgs e)
        {
            this.button_Send.Tag = this.textBox1;
            this.textBox1.Tag = this.button_Send.Text;//相互绑定
        }
        
        private void Extend_SizeChanged(object sender, EventArgs e)
        {
            int height = this.textBox1.Height;
            int width = this.Width - 65;
            this.textBox1.Size = new Size(width, height);
            this.button_Send.Location = new Point(this.Width - 50, this.button_Send.Location.Y);
        }
    }
}
