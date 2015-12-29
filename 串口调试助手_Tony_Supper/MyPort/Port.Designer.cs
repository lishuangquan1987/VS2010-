namespace MyPort
{
    partial class Port
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

        #region 组件设计器生成的代码

        /// <summary> 
        /// 设计器支持所需的方法 - 不要
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.btn_open = new System.Windows.Forms.Button();
            this.label_COMName = new System.Windows.Forms.Label();
            this.textBox_PortName = new System.Windows.Forms.TextBox();
            this.label_BauteRate = new System.Windows.Forms.Label();
            this.comboBox_BauteRate = new System.Windows.Forms.ComboBox();
            this.label_Parity = new System.Windows.Forms.Label();
            this.comboBox_Parity = new System.Windows.Forms.ComboBox();
            this.comboBox_StopBit = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.comboBox_DataBit = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // btn_open
            // 
            this.btn_open.Location = new System.Drawing.Point(604, 4);
            this.btn_open.Name = "btn_open";
            this.btn_open.Size = new System.Drawing.Size(54, 23);
            this.btn_open.TabIndex = 0;
            this.btn_open.Text = "Open";
            this.btn_open.UseVisualStyleBackColor = true;
            this.btn_open.Click += new System.EventHandler(this.btn_open_Click);
            // 
            // label_COMName
            // 
            this.label_COMName.AutoSize = true;
            this.label_COMName.Location = new System.Drawing.Point(3, 8);
            this.label_COMName.Name = "label_COMName";
            this.label_COMName.Size = new System.Drawing.Size(59, 12);
            this.label_COMName.TabIndex = 1;
            this.label_COMName.Text = "PortName:";
            // 
            // textBox_PortName
            // 
            this.textBox_PortName.Location = new System.Drawing.Point(59, 5);
            this.textBox_PortName.Name = "textBox_PortName";
            this.textBox_PortName.ReadOnly = true;
            this.textBox_PortName.Size = new System.Drawing.Size(53, 21);
            this.textBox_PortName.TabIndex = 2;
            // 
            // label_BauteRate
            // 
            this.label_BauteRate.AutoSize = true;
            this.label_BauteRate.Location = new System.Drawing.Point(114, 9);
            this.label_BauteRate.Name = "label_BauteRate";
            this.label_BauteRate.Size = new System.Drawing.Size(65, 12);
            this.label_BauteRate.TabIndex = 3;
            this.label_BauteRate.Text = "BauteRate:";
            // 
            // comboBox_BauteRate
            // 
            this.comboBox_BauteRate.FormattingEnabled = true;
            this.comboBox_BauteRate.Items.AddRange(new object[] {
            "115200",
            "57600",
            "38400",
            "19200",
            "9600",
            "4800",
            "2400"});
            this.comboBox_BauteRate.Location = new System.Drawing.Point(179, 5);
            this.comboBox_BauteRate.Name = "comboBox_BauteRate";
            this.comboBox_BauteRate.Size = new System.Drawing.Size(67, 20);
            this.comboBox_BauteRate.TabIndex = 4;
            this.comboBox_BauteRate.SelectedIndexChanged += new System.EventHandler(this.comboBox_BauteRate_SelectedIndexChanged);
            // 
            // label_Parity
            // 
            this.label_Parity.AutoSize = true;
            this.label_Parity.Location = new System.Drawing.Point(250, 9);
            this.label_Parity.Name = "label_Parity";
            this.label_Parity.Size = new System.Drawing.Size(47, 12);
            this.label_Parity.TabIndex = 5;
            this.label_Parity.Text = "Parity:";
            // 
            // comboBox_Parity
            // 
            this.comboBox_Parity.FormattingEnabled = true;
            this.comboBox_Parity.Location = new System.Drawing.Point(296, 5);
            this.comboBox_Parity.Name = "comboBox_Parity";
            this.comboBox_Parity.Size = new System.Drawing.Size(63, 20);
            this.comboBox_Parity.TabIndex = 6;
            this.comboBox_Parity.SelectedIndexChanged += new System.EventHandler(this.comboBox_Parity_SelectedIndexChanged);
            // 
            // comboBox_StopBit
            // 
            this.comboBox_StopBit.FormattingEnabled = true;
            this.comboBox_StopBit.Location = new System.Drawing.Point(413, 5);
            this.comboBox_StopBit.Name = "comboBox_StopBit";
            this.comboBox_StopBit.Size = new System.Drawing.Size(63, 20);
            this.comboBox_StopBit.TabIndex = 8;
            this.comboBox_StopBit.SelectedIndexChanged += new System.EventHandler(this.comboBox_StopBit_SelectedIndexChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(361, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(53, 12);
            this.label1.TabIndex = 7;
            this.label1.Text = "StopBit:";
            // 
            // comboBox_DataBit
            // 
            this.comboBox_DataBit.FormattingEnabled = true;
            this.comboBox_DataBit.Location = new System.Drawing.Point(538, 6);
            this.comboBox_DataBit.Name = "comboBox_DataBit";
            this.comboBox_DataBit.Size = new System.Drawing.Size(63, 20);
            this.comboBox_DataBit.TabIndex = 10;
            this.comboBox_DataBit.SelectedIndexChanged += new System.EventHandler(this.comboBox_DataBit_SelectedIndexChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(479, 8);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(59, 12);
            this.label2.TabIndex = 9;
            this.label2.Text = "DataBits:";
            // 
            // Port
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.comboBox_DataBit);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.comboBox_StopBit);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.comboBox_Parity);
            this.Controls.Add(this.label_Parity);
            this.Controls.Add(this.comboBox_BauteRate);
            this.Controls.Add(this.label_BauteRate);
            this.Controls.Add(this.textBox_PortName);
            this.Controls.Add(this.label_COMName);
            this.Controls.Add(this.btn_open);
            this.Name = "Port";
            this.Size = new System.Drawing.Size(664, 28);
            this.Load += new System.EventHandler(this.Port_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btn_open;
        private System.Windows.Forms.Label label_COMName;
        private System.Windows.Forms.Label label_BauteRate;
        private System.Windows.Forms.ComboBox comboBox_BauteRate;
        private System.Windows.Forms.Label label_Parity;
        private System.Windows.Forms.ComboBox comboBox_Parity;
        private System.Windows.Forms.ComboBox comboBox_StopBit;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox comboBox_DataBit;
        private System.Windows.Forms.Label label2;
        public System.Windows.Forms.TextBox textBox_PortName;
    }
}
