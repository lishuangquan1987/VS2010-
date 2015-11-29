using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;

namespace FileWhat
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (button2.Text == "开始监听")
            {
                if (op == null || op.SelectedPath == null || op.SelectedPath == "")
                {
                    MessageBox.Show("请选取监听文件夹");
                    return;
                }
                filewatch = new FileSystemWatcher();
                filewatch.Path = op.SelectedPath;
                filewatch.IncludeSubdirectories = true;
                filewatch.EnableRaisingEvents = true;
                //filewatch.Changed += filewatch_Changed;
                filewatch.Created += filewatch_Changed;
               // filewatch.Deleted += filewatch_Changed;

                button2.Text = "停止监听";
            }
            else
            {
                filewatch.Created -= filewatch_Changed;
                button2.Text = "开始监听";
            }
        }

        void filewatch_Changed(object sender, FileSystemEventArgs e)
        {
            MessageBox.Show(e.Name);
        }
        FolderBrowserDialog op;
        System.IO.FileSystemWatcher filewatch;
        private void button1_Click(object sender, EventArgs e)
        {
            
            op = new FolderBrowserDialog();
            if (this.op.ShowDialog() != DialogResult.OK)
                return;
            this.textBox1.Text = op.SelectedPath;
        }
    }
}
