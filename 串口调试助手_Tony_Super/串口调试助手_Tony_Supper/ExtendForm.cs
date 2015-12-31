using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using WeifenLuo.WinFormsUI.Docking;

namespace 串口调试助手_Tony_Supper
{
    public partial class ExtendForm : DockContent
    {
        public ExtendForm()
        {
            InitializeComponent();
            this.DockAreas = DockAreas.DockLeft | DockAreas.DockRight | DockAreas.DockTop | DockAreas.DockBottom | DockAreas.Float;
            this.HideOnClose = true;
        }
        public extend_Unit[] extend_units = new extend_Unit[50];
        private void ExtendForm_Load(object sender, EventArgs e)
        {
            for (int i = 0; i < 50; i++)
            {
                extend_units[i] = new extend_Unit();
                extend_units[i].Location = new Point(0, 30 * i);
                extend_units[i].button.Text = (i+1).ToString();
                extend_units[i].button.Tag = extend_units[i].textBox1;
                extend_units[i].textBox1.Tag = (i + 1).ToString();
                this.Controls.Add(extend_units[i]);
            }
            this.ShowIcon = true;
        }

        private void ExtendForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            this.DockState = WeifenLuo.WinFormsUI.Docking.DockState.Hidden;
            e.Cancel = true;
        }

    }
}
