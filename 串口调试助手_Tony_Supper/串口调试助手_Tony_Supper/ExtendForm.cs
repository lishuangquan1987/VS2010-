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
    }
}
