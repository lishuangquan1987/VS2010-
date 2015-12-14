using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using LuaInterface;
using System.IO;
using System.Threading;
using System.Runtime.InteropServices;


namespace A145Log整合
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        List<string> path=new List<string>();
        delegate void Update_paths(string filename, List<string> paths);
        public void update_paths(string filename,List<string> paths)
        {
            if (this.InvokeRequired)
                this.Invoke(new Update_paths(update_paths), filename,paths);
            else
            {
                this.label_msg.Text = string.Format("正在添加文件{0}", filename);
                this.path_richTextBox.AppendText(filename + "\r\n");
                if (filename == paths[paths.Count - 1])
                    this.label_msg.Text = string.Format("已经成功添加{0}个文件", path.ToArray().Length);
                this.path_richTextBox.Focus();
                
            }
        }
        
    
        private void button_load_Click(object sender, EventArgs e)
        {
            path.Clear();
            label_msg.Text = "";
            path_richTextBox.Clear();
            Application.DoEvents();
            OpenFileDialog op = new OpenFileDialog();
            op.Title = "请选择CSV档案";
            op.Filter = "csv文档|*.csv";
            op.Multiselect = true;
            if (op.ShowDialog() != DialogResult.OK)
                return;
            path.AddRange(op.FileNames);
            new Thread(() =>
            {
                foreach (string filename in path)
                {
                    update_paths(filename,path);
                }
            }).Start();
           

        }
        string GetDirectory(string filename)
        {
            string[] cc = filename.Split('\\');
            string aa="";
            for (int i = 0; i < cc.Length - 1; i++)
            {
                aa += cc[i] + "\\";
            }
            return aa + "ALL.CSV";
        }
        public void msgbox(string msg)
        {
            MessageBox.Show(msg);
        }
        string GetCSVHeader(string FileName)
        {
            string[] content = File.ReadAllLines(FileName,Encoding.Default);
            string Header = "";
            for (int i = 0; i < content.Length - 1; i++)
            {
                Header += content[i] + "\r\n";
            }
            return Header;
        }
        string GetTestContent(string FileName)
        {
            string[] content = File.ReadAllLines(FileName, Encoding.Default);
            string TestContent = content[content.Length-1];
            return TestContent;           
        }
        public delegate void UpdateProcessBar(int value);
        public void updateProcessBar(int value)
        {
            if (this.InvokeRequired)
                this.Invoke(new UpdateProcessBar(updateProcessBar), value);
            else
            {
                this.toolStripProgressBar1.Value = value;
                if(value!=path.Count)
                this.toolStripStatusLabel1.Text = string.Format("正在合并第{0}个文件", value.ToString());
                else
                    this.toolStripStatusLabel1.Text = string.Format("合并完成");
            }
               
        }
        /// <summary>
        /// 调用lua合并
        /// </summary>
        public void Merge()
        {
            int startTime = System.Environment.TickCount;
            Lua lua = new Lua();
            lua.DoFile("core.lua");
            lua.RegisterFunction("msgbox", this, this.GetType().GetMethod("msgbox"));
            string DestinationPath = GetDirectory(path[0]);
            LuaFunction luf2 = lua.GetFunction("AppendCSV");
            LuaFunction luf1 = lua.GetFunction("CreatCSV");
            //lua["Lenth"] = path.Length;
            for (int i = 0; i < path.ToArray().Length; i++)
            {
                updateProcessBar(i+1);
                if (i == 0)
                    luf1.Call(path[i], DestinationPath);
                else
                {
                   
                        //string filename = path[i];                      
                       // Thread.Sleep(5);//必须延迟10ms 否则出错！！！！！！！！！！！！！！

                        luf2.Call(path[i], DestinationPath);
                       // Thread.Sleep(5);
                        //Thread.Sleep(10);
 
                }
            }
            int endTime = System.Environment.TickCount;
            if (MessageBox.Show("finish,用时" + (endTime - startTime).ToString() + "ms" + "\r\n是否查看合并后的文件？","Message",MessageBoxButtons.YesNo,MessageBoxIcon.Information) == DialogResult.Yes)
            {
                System.Diagnostics.Process.Start(DestinationPath);
            }
        }
        private void button_concact_Click(object sender, EventArgs e)
        {
            this.toolStripProgressBar1.Maximum = path.ToArray().Length;
            this.toolStripProgressBar1.Value = 0;
            new Thread(new ThreadStart(Merge)).Start();             
        }
        /// <summary>
        /// c#直接合并
        /// </summary>
        void _Merge()
        {
            int startTime = System.Environment.TickCount;
            string DestinationPath = GetDirectory(path[0]);
            string allContent = GetCSVHeader(path[0]);
            for (int i = 0; i < path.Count; i++)
            {
                updateProcessBar(i + 1);
                allContent += GetTestContent(path[i]) + "\r\n";
            }
             File.WriteAllText(DestinationPath, allContent, Encoding.Default);
                int endTime = System.Environment.TickCount;
                MessageBox.Show("合并成功,用时" + (endTime - startTime).ToString() + "ms");      
        }
        private void panel1_DragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                e.Effect = DragDropEffects.Copy;
            }
            else
                e.Effect = DragDropEffects.None;
        }

        private void panel1_DragDrop(object sender, DragEventArgs e)
        {
           
            Array _paths = (Array)e.Data.GetData(DataFormats.FileDrop);
            for (int i = 0; i < _paths.Length; i++)
            {
                string str = _paths.GetValue(i).ToString();
                path.Add(str);
                path_richTextBox.Text += str;
            }
            this.label_msg.Text = string.Format("已经选择{0}个文件", path.ToArray().Length);

        }

       

        private void panel1_DragOver(object sender, DragEventArgs e)
        {
            e.Effect = DragDropEffects.All;
        }
        

     
        private void Form1_Load(object sender, EventArgs e)
        {
           
        }

        private void path_richTextBox_MouseDown(object sender, MouseEventArgs e)
        {
           
        }

        private void path_richTextBox_MouseUp(object sender, MouseEventArgs e)
        {
           
        }

        private void panel1_MouseDown(object sender, MouseEventArgs e)
        {
            MessageBox.Show("1");
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            if (File.Exists("使用说明.txt"))
                System.Diagnostics.Process.Start("使用说明.txt");
            else
                MessageBox.Show("未找到说明文件！");
        }
    }
}
