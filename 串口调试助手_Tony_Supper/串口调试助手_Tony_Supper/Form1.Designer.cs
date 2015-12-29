namespace 串口调试助手_Tony_Supper
{
    partial class Form1
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.panel_serialport = new System.Windows.Forms.Panel();
            this.textBox_cmd = new System.Windows.Forms.TextBox();
            this.button_Send = new System.Windows.Forms.Button();
            this.button_ReScan = new System.Windows.Forms.Button();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.panel2 = new System.Windows.Forms.Panel();
            this.mainText1 = new 串口调试助手_Tony_Supper.MainText();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel_serialport
            // 
            this.panel_serialport.AutoScroll = true;
            this.panel_serialport.Location = new System.Drawing.Point(2, 184);
            this.panel_serialport.Name = "panel_serialport";
            this.panel_serialport.Size = new System.Drawing.Size(672, 106);
            this.panel_serialport.TabIndex = 0;
            // 
            // textBox_cmd
            // 
            this.textBox_cmd.Location = new System.Drawing.Point(3, 3);
            this.textBox_cmd.Name = "textBox_cmd";
            this.textBox_cmd.Size = new System.Drawing.Size(482, 21);
            this.textBox_cmd.TabIndex = 2;
            // 
            // button_Send
            // 
            this.button_Send.Location = new System.Drawing.Point(488, 3);
            this.button_Send.Name = "button_Send";
            this.button_Send.Size = new System.Drawing.Size(75, 23);
            this.button_Send.TabIndex = 3;
            this.button_Send.Text = "Send";
            this.button_Send.UseVisualStyleBackColor = true;
            this.button_Send.Click += new System.EventHandler(this.button_Send_Click);
            // 
            // button_ReScan
            // 
            this.button_ReScan.Location = new System.Drawing.Point(575, 3);
            this.button_ReScan.Name = "button_ReScan";
            this.button_ReScan.Size = new System.Drawing.Size(75, 23);
            this.button_ReScan.TabIndex = 4;
            this.button_ReScan.Text = "ReScan";
            this.button_ReScan.UseVisualStyleBackColor = true;
            this.button_ReScan.Click += new System.EventHandler(this.button_ReScan_Click);
            // 
            // statusStrip1
            // 
            this.statusStrip1.Location = new System.Drawing.Point(0, 506);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(674, 22);
            this.statusStrip1.TabIndex = 5;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.textBox_cmd);
            this.panel2.Controls.Add(this.button_Send);
            this.panel2.Controls.Add(this.button_ReScan);
            this.panel2.Location = new System.Drawing.Point(9, 154);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(653, 27);
            this.panel2.TabIndex = 7;
            // 
            // mainText1
            // 
            this.mainText1.Dock = System.Windows.Forms.DockStyle.Top;
            this.mainText1.Location = new System.Drawing.Point(0, 0);
            this.mainText1.Name = "mainText1";
            this.mainText1.Size = new System.Drawing.Size(674, 150);
            this.mainText1.TabIndex = 1;
            // 
            // Form1
            // 
            this.AcceptButton = this.button_Send;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(674, 528);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.mainText1);
            this.Controls.Add(this.panel_serialport);
            this.IsMdiContainer = true;
            this.Name = "Form1";
            this.Text = "Form1";
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel panel_serialport;
        private MainText mainText1;
        private System.Windows.Forms.TextBox textBox_cmd;
        private System.Windows.Forms.Button button_Send;
        private System.Windows.Forms.Button button_ReScan;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.Panel panel2;

    }
}

