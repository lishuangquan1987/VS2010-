using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Windows.Forms;

namespace 分段读取文件并显示进度
{
    public class FileAction : UserControl
    {
        public int transmitSize;
        public int leftSize;
        public int transmitTime;
        public ProgressBar progressBar1;

        public FileStream fl_read;

        private void InitializeComponent()
        {
            this.progressBar1 = new System.Windows.Forms.ProgressBar();
            this.SuspendLayout();
            // 
            // progressBar1
            // 
            this.progressBar1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.progressBar1.Location = new System.Drawing.Point(0, 0);
            this.progressBar1.Name = "progressBar1";
            this.progressBar1.Size = new System.Drawing.Size(595, 31);
            this.progressBar1.TabIndex = 0;
            // 
            // FileAction
            // 
            this.Controls.Add(this.progressBar1);
            this.Name = "FileAction";
            this.Size = new System.Drawing.Size(595, 31);
            this.ResumeLayout(false);

        }
        delegate void UpdateProcessBar(int value);

       public void updateProcessBar(int value)
        {
            if (this.progressBar1.InvokeRequired)
                this.progressBar1.Invoke(new UpdateProcessBar(updateProcessBar), value);
            else
                this.progressBar1.Value = value / (this.progressBar1.Maximum - this.progressBar1.Minimum);
        }


       public void ReadFile(string FileName)
       {
           fl_read = new FileStream(FileName, FileMode.Open, FileAccess.Read);
           int totoalSize =(int)fl_read.Length;
           int temp = totoalSize % transmitSize;
           if (temp != 0)
               transmitTime = (totoalSize - temp) / transmitSize + 1;
           else
               transmitSize = totoalSize / transmitSize;
           leftSize = totoalSize;
           while (leftSize>transmitSize)
           {
               byte[] transmitbyte = new byte[transmitSize];
               fl_read.Read(transmitbyte,)

           }

       }
    }
}
