namespace 聊天1
{
    partial class Form1
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
            
            
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.textBox_adr_slave = new System.Windows.Forms.TextBox();
            this.label_slave = new System.Windows.Forms.Label();
            this.label2_slave = new System.Windows.Forms.Label();
            this.textBox2_slave = new System.Windows.Forms.TextBox();
            this.button_test_slave = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.richTextBox_chat = new System.Windows.Forms.RichTextBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.richTextBox_prechat = new System.Windows.Forms.RichTextBox();
            this.button2 = new System.Windows.Forms.Button();
            this.textBox_adr_host = new System.Windows.Forms.TextBox();
            this.label_host = new System.Windows.Forms.Label();
            this.textBox_host = new System.Windows.Forms.TextBox();
            this.label2_host = new System.Windows.Forms.Label();
            this.radioButton_Slave = new System.Windows.Forms.RadioButton();
            this.radioButton_Host = new System.Windows.Forms.RadioButton();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // textBox_adr_slave
            // 
            this.textBox_adr_slave.Location = new System.Drawing.Point(121, 25);
            this.textBox_adr_slave.Name = "textBox_adr_slave";
            this.textBox_adr_slave.Size = new System.Drawing.Size(147, 21);
            this.textBox_adr_slave.TabIndex = 0;
            // 
            // label_slave
            // 
            this.label_slave.AutoSize = true;
            this.label_slave.Location = new System.Drawing.Point(74, 28);
            this.label_slave.Name = "label_slave";
            this.label_slave.Size = new System.Drawing.Size(53, 12);
            this.label_slave.TabIndex = 1;
            this.label_slave.Text = "IP地址：";
            // 
            // label2_slave
            // 
            this.label2_slave.AutoSize = true;
            this.label2_slave.Location = new System.Drawing.Point(293, 28);
            this.label2_slave.Name = "label2_slave";
            this.label2_slave.Size = new System.Drawing.Size(41, 12);
            this.label2_slave.TabIndex = 3;
            this.label2_slave.Text = "端口：";
            // 
            // textBox2_slave
            // 
            this.textBox2_slave.Location = new System.Drawing.Point(330, 25);
            this.textBox2_slave.Name = "textBox2_slave";
            this.textBox2_slave.Size = new System.Drawing.Size(70, 21);
            this.textBox2_slave.TabIndex = 2;
            // 
            // button_test_slave
            // 
            this.button_test_slave.Location = new System.Drawing.Point(432, 23);
            this.button_test_slave.Name = "button_test_slave";
            this.button_test_slave.Size = new System.Drawing.Size(80, 23);
            this.button_test_slave.TabIndex = 4;
            this.button_test_slave.Text = "连接测试";
            this.button_test_slave.UseVisualStyleBackColor = true;
            this.button_test_slave.Click += new System.EventHandler(this.button1_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.richTextBox_chat);
            this.groupBox1.Location = new System.Drawing.Point(15, 88);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(448, 122);
            this.groupBox1.TabIndex = 5;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "聊天记录";
            // 
            // richTextBox_chat
            // 
            this.richTextBox_chat.Dock = System.Windows.Forms.DockStyle.Fill;
            this.richTextBox_chat.Location = new System.Drawing.Point(3, 17);
            this.richTextBox_chat.Name = "richTextBox_chat";
            this.richTextBox_chat.Size = new System.Drawing.Size(442, 102);
            this.richTextBox_chat.TabIndex = 15;
            this.richTextBox_chat.Text = "";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.richTextBox_prechat);
            this.groupBox2.Location = new System.Drawing.Point(15, 217);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(324, 45);
            this.groupBox2.TabIndex = 6;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "聊天输入框";
            // 
            // richTextBox_prechat
            // 
            this.richTextBox_prechat.Dock = System.Windows.Forms.DockStyle.Fill;
            this.richTextBox_prechat.Location = new System.Drawing.Point(3, 17);
            this.richTextBox_prechat.Name = "richTextBox_prechat";
            this.richTextBox_prechat.Size = new System.Drawing.Size(318, 25);
            this.richTextBox_prechat.TabIndex = 0;
            this.richTextBox_prechat.Text = "";
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(376, 234);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(75, 23);
            this.button2.TabIndex = 7;
            this.button2.Text = "发送";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // textBox_adr_host
            // 
            this.textBox_adr_host.Location = new System.Drawing.Point(121, 52);
            this.textBox_adr_host.Name = "textBox_adr_host";
            this.textBox_adr_host.ReadOnly = true;
            this.textBox_adr_host.Size = new System.Drawing.Size(147, 21);
            this.textBox_adr_host.TabIndex = 8;
            // 
            // label_host
            // 
            this.label_host.AutoSize = true;
            this.label_host.Location = new System.Drawing.Point(74, 55);
            this.label_host.Name = "label_host";
            this.label_host.Size = new System.Drawing.Size(53, 12);
            this.label_host.TabIndex = 9;
            this.label_host.Text = "IP地址：";
            // 
            // textBox_host
            // 
            this.textBox_host.Location = new System.Drawing.Point(330, 52);
            this.textBox_host.Name = "textBox_host";
            this.textBox_host.ReadOnly = true;
            this.textBox_host.Size = new System.Drawing.Size(70, 21);
            this.textBox_host.TabIndex = 10;
            // 
            // label2_host
            // 
            this.label2_host.AutoSize = true;
            this.label2_host.Location = new System.Drawing.Point(293, 55);
            this.label2_host.Name = "label2_host";
            this.label2_host.Size = new System.Drawing.Size(41, 12);
            this.label2_host.TabIndex = 11;
            this.label2_host.Text = "端口：";
            // 
            // radioButton_Slave
            // 
            this.radioButton_Slave.AutoSize = true;
            this.radioButton_Slave.Location = new System.Drawing.Point(15, 26);
            this.radioButton_Slave.Name = "radioButton_Slave";
            this.radioButton_Slave.Size = new System.Drawing.Size(53, 16);
            this.radioButton_Slave.TabIndex = 13;
            this.radioButton_Slave.TabStop = true;
            this.radioButton_Slave.Text = "Slave";
            this.radioButton_Slave.UseVisualStyleBackColor = true;
            this.radioButton_Slave.CheckedChanged += new System.EventHandler(this.radioButton_Slave_CheckedChanged);
            // 
            // radioButton_Host
            // 
            this.radioButton_Host.AutoSize = true;
            this.radioButton_Host.Location = new System.Drawing.Point(15, 53);
            this.radioButton_Host.Name = "radioButton_Host";
            this.radioButton_Host.Size = new System.Drawing.Size(47, 16);
            this.radioButton_Host.TabIndex = 14;
            this.radioButton_Host.TabStop = true;
            this.radioButton_Host.Text = "Host";
            this.radioButton_Host.UseVisualStyleBackColor = true;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(527, 275);
            this.Controls.Add(this.radioButton_Host);
            this.Controls.Add(this.radioButton_Slave);
            this.Controls.Add(this.textBox_adr_host);
            this.Controls.Add(this.label_host);
            this.Controls.Add(this.textBox_host);
            this.Controls.Add(this.label2_host);
            this.Controls.Add(this.textBox_adr_slave);
            this.Controls.Add(this.label_slave);
            this.Controls.Add(this.textBox2_slave);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button_test_slave);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.label2_slave);
            this.Controls.Add(this.groupBox1);
            this.Name = "Form1";
            this.Text = "模拟QQ聊天";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form1_FormClosing);
            this.Load += new System.EventHandler(this.Form1_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox textBox_adr_slave;
        private System.Windows.Forms.Label label_slave;
        private System.Windows.Forms.Label label2_slave;
        private System.Windows.Forms.TextBox textBox2_slave;
        private System.Windows.Forms.Button button_test_slave;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.RichTextBox richTextBox_prechat;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.TextBox textBox_adr_host;
        private System.Windows.Forms.Label label_host;
        private System.Windows.Forms.TextBox textBox_host;
        private System.Windows.Forms.Label label2_host;
        private System.Windows.Forms.RadioButton radioButton_Slave;
        private System.Windows.Forms.RadioButton radioButton_Host;
        private System.Windows.Forms.RichTextBox richTextBox_chat;
    }
}

