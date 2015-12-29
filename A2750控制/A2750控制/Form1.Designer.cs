namespace A2750控制
{
    partial class A2750Control
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
            this.richTextBox_output = new System.Windows.Forms.RichTextBox();
            this.label_config = new System.Windows.Forms.Label();
            this.textBox_primary = new System.Windows.Forms.TextBox();
            this.button_connect = new System.Windows.Forms.Button();
            this.textBox_cmd = new System.Windows.Forms.TextBox();
            this.label_second = new System.Windows.Forms.Label();
            this.textBox_sencond = new System.Windows.Forms.TextBox();
            this.label_boardID = new System.Windows.Forms.Label();
            this.textBox_boardID = new System.Windows.Forms.TextBox();
            this.button_send = new System.Windows.Forms.Button();
            this.panel_extend = new System.Windows.Forms.Panel();
            this.checkBox_autosend = new System.Windows.Forms.CheckBox();
            this.comboBox_delay = new System.Windows.Forms.ComboBox();
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // richTextBox_output
            // 
            this.richTextBox_output.Dock = System.Windows.Forms.DockStyle.Top;
            this.richTextBox_output.Location = new System.Drawing.Point(0, 0);
            this.richTextBox_output.Name = "richTextBox_output";
            this.richTextBox_output.Size = new System.Drawing.Size(485, 96);
            this.richTextBox_output.TabIndex = 0;
            this.richTextBox_output.Text = "";
            // 
            // label_config
            // 
            this.label_config.AutoSize = true;
            this.label_config.Location = new System.Drawing.Point(13, 103);
            this.label_config.Name = "label_config";
            this.label_config.Size = new System.Drawing.Size(89, 12);
            this.label_config.TabIndex = 1;
            this.label_config.Text = "PramaryAdress:";
            // 
            // textBox_primary
            // 
            this.textBox_primary.Location = new System.Drawing.Point(99, 100);
            this.textBox_primary.Name = "textBox_primary";
            this.textBox_primary.Size = new System.Drawing.Size(41, 21);
            this.textBox_primary.TabIndex = 2;
            this.textBox_primary.Text = "16";
            // 
            // button_connect
            // 
            this.button_connect.Location = new System.Drawing.Point(404, 98);
            this.button_connect.Name = "button_connect";
            this.button_connect.Size = new System.Drawing.Size(75, 23);
            this.button_connect.TabIndex = 3;
            this.button_connect.Text = "Connect";
            this.button_connect.UseVisualStyleBackColor = true;
            this.button_connect.Click += new System.EventHandler(this.button_connect_Click);
            // 
            // textBox_cmd
            // 
            this.textBox_cmd.Location = new System.Drawing.Point(13, 130);
            this.textBox_cmd.Name = "textBox_cmd";
            this.textBox_cmd.Size = new System.Drawing.Size(369, 21);
            this.textBox_cmd.TabIndex = 4;
            // 
            // label_second
            // 
            this.label_second.AutoSize = true;
            this.label_second.Location = new System.Drawing.Point(146, 103);
            this.label_second.Name = "label_second";
            this.label_second.Size = new System.Drawing.Size(83, 12);
            this.label_second.TabIndex = 5;
            this.label_second.Text = "SecondAdress:";
            // 
            // textBox_sencond
            // 
            this.textBox_sencond.Location = new System.Drawing.Point(226, 100);
            this.textBox_sencond.Name = "textBox_sencond";
            this.textBox_sencond.Size = new System.Drawing.Size(41, 21);
            this.textBox_sencond.TabIndex = 6;
            this.textBox_sencond.Text = "0";
            // 
            // label_boardID
            // 
            this.label_boardID.AutoSize = true;
            this.label_boardID.Location = new System.Drawing.Point(273, 103);
            this.label_boardID.Name = "label_boardID";
            this.label_boardID.Size = new System.Drawing.Size(53, 12);
            this.label_boardID.TabIndex = 7;
            this.label_boardID.Text = "BoardID:";
            // 
            // textBox_boardID
            // 
            this.textBox_boardID.Location = new System.Drawing.Point(322, 100);
            this.textBox_boardID.Name = "textBox_boardID";
            this.textBox_boardID.Size = new System.Drawing.Size(41, 21);
            this.textBox_boardID.TabIndex = 8;
            this.textBox_boardID.Text = "0";
            // 
            // button_send
            // 
            this.button_send.Location = new System.Drawing.Point(404, 127);
            this.button_send.Name = "button_send";
            this.button_send.Size = new System.Drawing.Size(75, 23);
            this.button_send.TabIndex = 9;
            this.button_send.Text = "Send";
            this.button_send.UseVisualStyleBackColor = true;
            this.button_send.Click += new System.EventHandler(this.button_send_Click);
            // 
            // panel_extend
            // 
            this.panel_extend.AutoScroll = true;
            this.panel_extend.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel_extend.Location = new System.Drawing.Point(0, 180);
            this.panel_extend.Name = "panel_extend";
            this.panel_extend.Size = new System.Drawing.Size(485, 252);
            this.panel_extend.TabIndex = 10;
            // 
            // checkBox_autosend
            // 
            this.checkBox_autosend.AutoSize = true;
            this.checkBox_autosend.Location = new System.Drawing.Point(15, 158);
            this.checkBox_autosend.Name = "checkBox_autosend";
            this.checkBox_autosend.Size = new System.Drawing.Size(72, 16);
            this.checkBox_autosend.TabIndex = 11;
            this.checkBox_autosend.Text = "AutoSend";
            this.checkBox_autosend.UseVisualStyleBackColor = true;
            this.checkBox_autosend.CheckedChanged += new System.EventHandler(this.checkBox_autosend_CheckedChanged);
            // 
            // comboBox_delay
            // 
            this.comboBox_delay.FormattingEnabled = true;
            this.comboBox_delay.Location = new System.Drawing.Point(94, 155);
            this.comboBox_delay.Name = "comboBox_delay";
            this.comboBox_delay.Size = new System.Drawing.Size(74, 20);
            this.comboBox_delay.TabIndex = 12;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(404, 156);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 13;
            this.button1.Text = "Read";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(322, 154);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(75, 23);
            this.button2.TabIndex = 14;
            this.button2.Text = "Query";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // A2750Control
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(485, 432);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.comboBox_delay);
            this.Controls.Add(this.checkBox_autosend);
            this.Controls.Add(this.panel_extend);
            this.Controls.Add(this.button_send);
            this.Controls.Add(this.textBox_boardID);
            this.Controls.Add(this.label_boardID);
            this.Controls.Add(this.textBox_sencond);
            this.Controls.Add(this.label_second);
            this.Controls.Add(this.textBox_cmd);
            this.Controls.Add(this.button_connect);
            this.Controls.Add(this.textBox_primary);
            this.Controls.Add(this.label_config);
            this.Controls.Add(this.richTextBox_output);
            this.Name = "A2750Control";
            this.Text = "Form1";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.A2750Control_FormClosing);
            this.Load += new System.EventHandler(this.A2750Control_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.RichTextBox richTextBox_output;
        private System.Windows.Forms.Label label_config;
        private System.Windows.Forms.TextBox textBox_primary;
        private System.Windows.Forms.Button button_connect;
        private System.Windows.Forms.TextBox textBox_cmd;
        private System.Windows.Forms.Label label_second;
        private System.Windows.Forms.TextBox textBox_sencond;
        private System.Windows.Forms.Label label_boardID;
        private System.Windows.Forms.TextBox textBox_boardID;
        private System.Windows.Forms.Button button_send;
        private System.Windows.Forms.Panel panel_extend;
        private System.Windows.Forms.CheckBox checkBox_autosend;
        private System.Windows.Forms.ComboBox comboBox_delay;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
    }
}

