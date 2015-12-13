using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using WeifenLuo.WinFormsUI.Docking;

namespace weifenluo的应用
{
    public partial class Form1 : DockContent
    {
        public Form1()
        {
            InitializeComponent();
        }
        private Form2 f2 = new Form2();
        private Form3 f3 = new Form3();
        private Form4 f4 = new Form4();
        private void Form1_Load(object sender, EventArgs e)
        {
            f2.Show(dockPanel1,DockState.DockLeft);
            f3.Show(dockPanel1);
            f4.Show();
            f4.Show();
            
        }
    }
    public class Form2 : DockContent
    {
        public Form2()
        {
            Button b = new Button();
            b.Text = "1111111111111111";
            this.Controls.Add(b);
        }
    }
    public class Form3 : DockContent
    {
    }
    public class Form4 : DockContent
    {
    }
   
    

}
