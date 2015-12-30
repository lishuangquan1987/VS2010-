using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WeifenLuo.WinFormsUI.Docking;

namespace 串口调试助手_Tony_Supper
{
    public partial class MidForm :DockContent
    {
        public MidForm()
        {
            InitializeComponent();
        }
        public string[] Delays = new string[] { "20ms", "50ms", "100ms", "300ms", "500ms", "1000ms", "2000ms", "5000ms" };
        public int delay
        {
            get
            {
                if (this.comboBox_delay.SelectedIndex == -1)
                    return 500;//默认1000ms
                else
                {
                    int d =int.Parse(this.comboBox_delay.SelectedItem.ToString().Split('m')[0]);
                    return d;
                }
            }
        }
        private void button_ReScan_Click(object sender, EventArgs e)
        {

        }

        private void MidForm_Load(object sender, EventArgs e)
        {
            this.comboBox_delay.Items.AddRange(Delays);
        }
    }
}
