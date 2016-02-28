using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Diagnostics;
using System.Threading;

namespace 调用.py文件
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        
        private void button_start_Click(object sender, EventArgs e)
        {
            if (textBox_path.Text == null || textBox_path.Text == "")
            {
                MessageBox.Show("Please Load py File!");
                return;
            }
           
            //p.BeginOutputReadLine();
          
        }

        void p_OutputDataReceived(object sender, DataReceivedEventArgs e)
        {
            UpdateText(e.Data);
        }

     

        private void button1_Click(object sender, EventArgs e)
        {
          
            //p.CancelOutputRead();
            string cmd =string.Format("python {0} '{1}' '{2}'",textBox_path.Text,comboBox1.Text,"quit");
            Process p = new Process();
            p.StartInfo.FileName = "cmd.exe";
            //p.StartInfo.Arguments = "python " + textBox_path.Text;
            p.StartInfo.UseShellExecute = false;
            p.StartInfo.RedirectStandardOutput = true;
            p.StartInfo.RedirectStandardInput = true;
           
            p.StartInfo.CreateNoWindow = false;
            p.StartInfo.RedirectStandardError = true;
            //p.OutputDataReceived += p_OutputDataReceived;

            p.Start();
            try
            {
                p.StandardInput.WriteLine(cmd);
                p.WaitForExit(1000);
                p.Kill();
                string output = p.StandardOutput.ReadToEnd();
                this.richTextBox1.AppendText(output + "\r\n");
            }
            catch (Exception ee)
            {
                MessageBox.Show(ee.Message);
            }
            
           
        }
        delegate void UpdateText_dele(string msg);
        void UpdateText(string msg)
        {
            if (this.InvokeRequired)
                this.Invoke(new UpdateText_dele(UpdateText), msg);
            else
                this.richTextBox1.AppendText(msg+"\r\n");
        }

        private void button_scan_Click(object sender, EventArgs e)
        {
            OpenFileDialog op = new OpenFileDialog();
            op.Filter = "Py file|*.py";
            op.Multiselect = false;
            if (op.ShowDialog() != DialogResult.OK)
                return;
            textBox_path.Text = op.FileName;
        }
    }
}
