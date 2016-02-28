using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Threading;

namespace 利用tree显示电脑所有的文件_资源管理器_
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
         
                DriveInfo[] drivers = DriveInfo.GetDrives();//获取磁盘驱动器
                for (int i = 0; i < drivers.Length; i++)
                {

                    TreeNode node = new TreeNode(drivers[i].Name);
                    treeView1.Nodes.Add(node);

                    node.TreeView.Click += TreeView_Click;
                }
                //TreeNode node = treeView1.Nodes.Add(drivers[0].Name);
                //GetFiles(drivers[0].Name, node);

           
        }

        void TreeView_Click(object sender, EventArgs e)
        {
            TreeView treeview = (TreeView)sender;
            TreeNode node = treeview.SelectedNode;
            
            string filename = node.Text;
            if (File.Exists(filename))
            {
                System.Diagnostics.Process.Start(filename);
            }
            else
            {
                node.Nodes.Clear();
                DirectoryInfo folder = new DirectoryInfo(filename);
                DirectoryInfo[] childfolders = folder.GetDirectories();
                FileInfo[] files = folder.GetFiles();
                foreach (DirectoryInfo i in childfolders)
                {
                    node.Nodes.Add(i.FullName);
                }
                foreach (FileInfo i in files)
                {
                    node.Nodes.Add(i.FullName);
                }
               
            }
        }

        void TreeView_AfterExpand(object sender, TreeViewEventArgs e)
        {
            TreeNode node = (TreeNode)sender;
            MessageBox.Show(node.Name);
        }
        private void GetFiles(string filePath, TreeNode node)
        {
            DirectoryInfo folder = new DirectoryInfo(filePath);
            node.Text = folder.Name;
            node.Tag = folder.FullName;

            FileInfo[] chldFiles = folder.GetFiles("*.*");
            foreach (FileInfo chlFile in chldFiles)
            {
                TreeNode chldNode = new TreeNode();
                chldNode.Text = chlFile.Name;
                chldNode.Tag = chlFile.FullName;
                node.Nodes.Add(chldNode);
            }

            DirectoryInfo[] chldFolders = folder.GetDirectories();
            foreach (DirectoryInfo chldFolder in chldFolders)
            {
                TreeNode chldNode = new TreeNode();
                chldNode.Text = folder.Name;
                chldNode.Tag = folder.FullName;
                node.Nodes.Add(chldNode);
                GetFiles(chldFolder.FullName, chldNode);
            }
        }
    }
}
