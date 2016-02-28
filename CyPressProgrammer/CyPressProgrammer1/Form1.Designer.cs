namespace CyPressProgrammer1
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
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.comboBox_frequent = new System.Windows.Forms.ComboBox();
            this.button1 = new System.Windows.Forms.Button();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.button_program = new System.Windows.Forms.Button();
            this.button_power = new System.Windows.Forms.Button();
            this.btn_connect = new System.Windows.Forms.Button();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.RB_10PIN = new System.Windows.Forms.RadioButton();
            this.RB_5PIN = new System.Windows.Forms.RadioButton();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.RB_3V3 = new System.Windows.Forms.RadioButton();
            this.RB_5V = new System.Windows.Forms.RadioButton();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.SWD = new System.Windows.Forms.RadioButton();
            this.JTAG = new System.Windows.Forms.RadioButton();
            this.richTextBox1 = new System.Windows.Forms.RichTextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.btn_rescan = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.comboBox_ports = new System.Windows.Forms.ComboBox();
            this.groupBox4.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.comboBox_frequent);
            this.groupBox4.Location = new System.Drawing.Point(172, 127);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(142, 43);
            this.groupBox4.TabIndex = 33;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "Frequent";
            // 
            // comboBox_frequent
            // 
            this.comboBox_frequent.FormattingEnabled = true;
            this.comboBox_frequent.Location = new System.Drawing.Point(6, 17);
            this.comboBox_frequent.Name = "comboBox_frequent";
            this.comboBox_frequent.Size = new System.Drawing.Size(121, 20);
            this.comboBox_frequent.TabIndex = 0;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(406, 18);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 32;
            this.button1.Text = "load bin";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(51, 18);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(348, 21);
            this.textBox1.TabIndex = 31;
            // 
            // button_program
            // 
            this.button_program.Location = new System.Drawing.Point(405, 92);
            this.button_program.Name = "button_program";
            this.button_program.Size = new System.Drawing.Size(75, 23);
            this.button_program.TabIndex = 30;
            this.button_program.Text = "Program";
            this.button_program.UseVisualStyleBackColor = true;
            // 
            // button_power
            // 
            this.button_power.Location = new System.Drawing.Point(405, 50);
            this.button_power.Name = "button_power";
            this.button_power.Size = new System.Drawing.Size(75, 23);
            this.button_power.TabIndex = 29;
            this.button_power.Text = "Power";
            this.button_power.UseVisualStyleBackColor = true;
            // 
            // btn_connect
            // 
            this.btn_connect.Location = new System.Drawing.Point(211, 51);
            this.btn_connect.Name = "btn_connect";
            this.btn_connect.Size = new System.Drawing.Size(75, 23);
            this.btn_connect.TabIndex = 28;
            this.btn_connect.Text = "Connect";
            this.btn_connect.UseVisualStyleBackColor = true;
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.RB_10PIN);
            this.groupBox3.Controls.Add(this.RB_5PIN);
            this.groupBox3.Location = new System.Drawing.Point(19, 127);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(117, 43);
            this.groupBox3.TabIndex = 27;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Connector";
            // 
            // RB_10PIN
            // 
            this.RB_10PIN.AutoSize = true;
            this.RB_10PIN.Location = new System.Drawing.Point(67, 21);
            this.RB_10PIN.Name = "RB_10PIN";
            this.RB_10PIN.Size = new System.Drawing.Size(53, 16);
            this.RB_10PIN.TabIndex = 1;
            this.RB_10PIN.TabStop = true;
            this.RB_10PIN.Text = "10pin";
            this.RB_10PIN.UseVisualStyleBackColor = true;
            // 
            // RB_5PIN
            // 
            this.RB_5PIN.AutoSize = true;
            this.RB_5PIN.Location = new System.Drawing.Point(7, 21);
            this.RB_5PIN.Name = "RB_5PIN";
            this.RB_5PIN.Size = new System.Drawing.Size(47, 16);
            this.RB_5PIN.TabIndex = 0;
            this.RB_5PIN.TabStop = true;
            this.RB_5PIN.Text = "5pin";
            this.RB_5PIN.UseVisualStyleBackColor = true;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.RB_3V3);
            this.groupBox2.Controls.Add(this.RB_5V);
            this.groupBox2.Location = new System.Drawing.Point(156, 82);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(130, 38);
            this.groupBox2.TabIndex = 26;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Voltage";
            // 
            // RB_3V3
            // 
            this.RB_3V3.AutoSize = true;
            this.RB_3V3.Location = new System.Drawing.Point(74, 20);
            this.RB_3V3.Name = "RB_3V3";
            this.RB_3V3.Size = new System.Drawing.Size(47, 16);
            this.RB_3V3.TabIndex = 8;
            this.RB_3V3.TabStop = true;
            this.RB_3V3.Text = "3.3V";
            this.RB_3V3.UseVisualStyleBackColor = true;
            // 
            // RB_5V
            // 
            this.RB_5V.AutoSize = true;
            this.RB_5V.Location = new System.Drawing.Point(16, 20);
            this.RB_5V.Name = "RB_5V";
            this.RB_5V.Size = new System.Drawing.Size(35, 16);
            this.RB_5V.TabIndex = 7;
            this.RB_5V.TabStop = true;
            this.RB_5V.Text = "5V";
            this.RB_5V.UseVisualStyleBackColor = true;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.SWD);
            this.groupBox1.Controls.Add(this.JTAG);
            this.groupBox1.Location = new System.Drawing.Point(19, 79);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(117, 41);
            this.groupBox1.TabIndex = 25;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Protocal";
            // 
            // SWD
            // 
            this.SWD.AutoSize = true;
            this.SWD.Location = new System.Drawing.Point(67, 20);
            this.SWD.Name = "SWD";
            this.SWD.Size = new System.Drawing.Size(41, 16);
            this.SWD.TabIndex = 3;
            this.SWD.TabStop = true;
            this.SWD.Text = "SWD";
            this.SWD.UseVisualStyleBackColor = true;
            // 
            // JTAG
            // 
            this.JTAG.AutoSize = true;
            this.JTAG.Location = new System.Drawing.Point(9, 19);
            this.JTAG.Name = "JTAG";
            this.JTAG.Size = new System.Drawing.Size(47, 16);
            this.JTAG.TabIndex = 4;
            this.JTAG.TabStop = true;
            this.JTAG.Text = "JTAG";
            this.JTAG.UseVisualStyleBackColor = true;
            // 
            // richTextBox1
            // 
            this.richTextBox1.Location = new System.Drawing.Point(-2, 176);
            this.richTextBox1.Name = "richTextBox1";
            this.richTextBox1.Size = new System.Drawing.Size(527, 169);
            this.richTextBox1.TabIndex = 24;
            this.richTextBox1.Text = "";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(25, 92);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(0, 12);
            this.label2.TabIndex = 23;
            // 
            // btn_rescan
            // 
            this.btn_rescan.Location = new System.Drawing.Point(292, 50);
            this.btn_rescan.Name = "btn_rescan";
            this.btn_rescan.Size = new System.Drawing.Size(75, 23);
            this.btn_rescan.TabIndex = 22;
            this.btn_rescan.Text = "Rescan";
            this.btn_rescan.UseVisualStyleBackColor = true;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(17, 56);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(35, 12);
            this.label1.TabIndex = 21;
            this.label1.Text = "Port:";
            // 
            // comboBox_ports
            // 
            this.comboBox_ports.FormattingEnabled = true;
            this.comboBox_ports.Location = new System.Drawing.Point(58, 53);
            this.comboBox_ports.Name = "comboBox_ports";
            this.comboBox_ports.Size = new System.Drawing.Size(145, 20);
            this.comboBox_ports.TabIndex = 20;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(522, 362);
            this.Controls.Add(this.groupBox4);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.button_program);
            this.Controls.Add(this.button_power);
            this.Controls.Add(this.btn_connect);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.richTextBox1);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.btn_rescan);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.comboBox_ports);
            this.Name = "Form1";
            this.Text = "Form1";
            this.groupBox4.ResumeLayout(false);
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.ComboBox comboBox_frequent;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Button button_program;
        private System.Windows.Forms.Button button_power;
        private System.Windows.Forms.Button btn_connect;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.RadioButton RB_10PIN;
        private System.Windows.Forms.RadioButton RB_5PIN;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.RadioButton RB_3V3;
        private System.Windows.Forms.RadioButton RB_5V;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.RadioButton SWD;
        private System.Windows.Forms.RadioButton JTAG;
        private System.Windows.Forms.RichTextBox richTextBox1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button btn_rescan;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox comboBox_ports;
    }
}

