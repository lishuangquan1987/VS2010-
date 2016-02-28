using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace TreeView
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void treeView1_AfterSelect(object sender, TreeViewEventArgs e)
        {
            

        }

        private void button2_Click(object sender, EventArgs e)
        {
            TreeNode node1 = treeView1.SelectedNode;//获取选中的节点
            node1.Nodes.Add("新建子节点");

        }

        private void button1_Click(object sender, EventArgs e)
        {
            treeView1.Nodes.Add("新建根节点");
        }
    }
}
