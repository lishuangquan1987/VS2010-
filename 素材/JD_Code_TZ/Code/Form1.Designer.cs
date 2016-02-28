namespace Code
{
    partial class frmOrCode
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
            this.tabMain = new System.Windows.Forms.TabControl();
            this.tabEncode = new System.Windows.Forms.TabPage();
            this.btnSave = new System.Windows.Forms.Button();
            this.btnEncode = new System.Windows.Forms.Button();
            this.txtSize = new System.Windows.Forms.TextBox();
            this.cboCorrectionLevel = new System.Windows.Forms.ComboBox();
            this.cboVersion = new System.Windows.Forms.ComboBox();
            this.cboEncoding = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.txtEncodeData = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.picEncode = new System.Windows.Forms.PictureBox();
            this.tabDecode = new System.Windows.Forms.TabPage();
            this.btnDecode = new System.Windows.Forms.Button();
            this.btnOpen = new System.Windows.Forms.Button();
            this.txtDecodedData = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.picDecode = new System.Windows.Forms.PictureBox();
            this.saveFileDialog1 = new System.Windows.Forms.SaveFileDialog();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.tabMain.SuspendLayout();
            this.tabEncode.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picEncode)).BeginInit();
            this.tabDecode.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picDecode)).BeginInit();
            this.SuspendLayout();
            // 
            // tabMain
            // 
            this.tabMain.Controls.Add(this.tabEncode);
            this.tabMain.Controls.Add(this.tabDecode);
            this.tabMain.Location = new System.Drawing.Point(3, 4);
            this.tabMain.Name = "tabMain";
            this.tabMain.SelectedIndex = 0;
            this.tabMain.Size = new System.Drawing.Size(556, 501);
            this.tabMain.TabIndex = 1;
            // 
            // tabEncode
            // 
            this.tabEncode.Controls.Add(this.btnSave);
            this.tabEncode.Controls.Add(this.btnEncode);
            this.tabEncode.Controls.Add(this.txtSize);
            this.tabEncode.Controls.Add(this.cboCorrectionLevel);
            this.tabEncode.Controls.Add(this.cboVersion);
            this.tabEncode.Controls.Add(this.cboEncoding);
            this.tabEncode.Controls.Add(this.label3);
            this.tabEncode.Controls.Add(this.label6);
            this.tabEncode.Controls.Add(this.label7);
            this.tabEncode.Controls.Add(this.label5);
            this.tabEncode.Controls.Add(this.label4);
            this.tabEncode.Controls.Add(this.label2);
            this.tabEncode.Controls.Add(this.txtEncodeData);
            this.tabEncode.Controls.Add(this.label1);
            this.tabEncode.Controls.Add(this.picEncode);
            this.tabEncode.Location = new System.Drawing.Point(4, 22);
            this.tabEncode.Name = "tabEncode";
            this.tabEncode.Padding = new System.Windows.Forms.Padding(3);
            this.tabEncode.Size = new System.Drawing.Size(548, 475);
            this.tabEncode.TabIndex = 0;
            this.tabEncode.Text = "生成二维码";
            this.tabEncode.UseVisualStyleBackColor = true;
            // 
            // btnSave
            // 
            this.btnSave.Location = new System.Drawing.Point(283, 432);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(94, 25);
            this.btnSave.TabIndex = 6;
            this.btnSave.Text = "保存";
            this.btnSave.UseVisualStyleBackColor = true;
            // 
            // btnEncode
            // 
            this.btnEncode.Location = new System.Drawing.Point(128, 432);
            this.btnEncode.Name = "btnEncode";
            this.btnEncode.Size = new System.Drawing.Size(94, 25);
            this.btnEncode.TabIndex = 6;
            this.btnEncode.Text = "生成二维码";
            this.btnEncode.UseVisualStyleBackColor = true;
            this.btnEncode.Click += new System.EventHandler(this.btnEncode_Click);
            // 
            // txtSize
            // 
            this.txtSize.Location = new System.Drawing.Point(128, 408);
            this.txtSize.Name = "txtSize";
            this.txtSize.Size = new System.Drawing.Size(100, 21);
            this.txtSize.TabIndex = 5;
            this.txtSize.Text = "4";
            // 
            // cboCorrectionLevel
            // 
            this.cboCorrectionLevel.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboCorrectionLevel.FormattingEnabled = true;
            this.cboCorrectionLevel.Items.AddRange(new object[] {
            "L",
            "M",
            "Q",
            "H"});
            this.cboCorrectionLevel.Location = new System.Drawing.Point(445, 348);
            this.cboCorrectionLevel.Name = "cboCorrectionLevel";
            this.cboCorrectionLevel.Size = new System.Drawing.Size(93, 20);
            this.cboCorrectionLevel.TabIndex = 4;
            // 
            // cboVersion
            // 
            this.cboVersion.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboVersion.FormattingEnabled = true;
            this.cboVersion.Items.AddRange(new object[] {
            "1",
            "2",
            "3",
            "4",
            "5",
            "6",
            "7",
            "8",
            "9",
            "10",
            "11",
            "12",
            "13",
            "14",
            "15",
            "16",
            "17",
            "18",
            "19",
            "20",
            "21",
            "22",
            "23",
            "40",
            "25",
            "26",
            "27",
            "28",
            "29",
            "30",
            "31",
            "32",
            "33",
            "34",
            "35",
            "36",
            "37",
            "38",
            "39",
            "40"});
            this.cboVersion.Location = new System.Drawing.Point(128, 379);
            this.cboVersion.Name = "cboVersion";
            this.cboVersion.Size = new System.Drawing.Size(121, 20);
            this.cboVersion.TabIndex = 4;
            // 
            // cboEncoding
            // 
            this.cboEncoding.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboEncoding.FormattingEnabled = true;
            this.cboEncoding.Items.AddRange(new object[] {
            "AlphaNumeric",
            "Numeric",
            "Byte"});
            this.cboEncoding.Location = new System.Drawing.Point(128, 348);
            this.cboEncoding.Name = "cboEncoding";
            this.cboEncoding.Size = new System.Drawing.Size(121, 20);
            this.cboEncoding.TabIndex = 4;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label3.Location = new System.Drawing.Point(279, 349);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(160, 19);
            this.label3.TabIndex = 3;
            this.label3.Text = "Correction Level(校正)";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label6.ForeColor = System.Drawing.Color.Red;
            this.label6.Location = new System.Drawing.Point(23, 457);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(245, 17);
            this.label6.TabIndex = 3;
            this.label6.Text = "编码默认Byte,校正随意,版本默认7大小默认4";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label7.ForeColor = System.Drawing.Color.Blue;
            this.label7.Location = new System.Drawing.Point(242, 412);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(296, 17);
            this.label7.TabIndex = 3;
            this.label7.Text = "生成的二维码图片可能过大请保存后自行打开进行扫描";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label5.Location = new System.Drawing.Point(48, 410);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(74, 19);
            this.label5.TabIndex = 3;
            this.label5.Text = "Size(大小)";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label4.Location = new System.Drawing.Point(22, 380);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(100, 19);
            this.label4.TabIndex = 3;
            this.label4.Text = "Version(版本)";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label2.Location = new System.Drawing.Point(11, 349);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(111, 19);
            this.label2.TabIndex = 3;
            this.label2.Text = "Encoding(编码)";
            // 
            // txtEncodeData
            // 
            this.txtEncodeData.Location = new System.Drawing.Point(60, 260);
            this.txtEncodeData.Multiline = true;
            this.txtEncodeData.Name = "txtEncodeData";
            this.txtEncodeData.Size = new System.Drawing.Size(482, 76);
            this.txtEncodeData.TabIndex = 2;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label1.Location = new System.Drawing.Point(7, 260);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(58, 22);
            this.label1.TabIndex = 1;
            this.label1.Text = "内容：";
            // 
            // picEncode
            // 
            this.picEncode.BackColor = System.Drawing.Color.White;
            this.picEncode.Location = new System.Drawing.Point(6, 6);
            this.picEncode.Name = "picEncode";
            this.picEncode.Size = new System.Drawing.Size(536, 247);
            this.picEncode.TabIndex = 0;
            this.picEncode.TabStop = false;
            // 
            // tabDecode
            // 
            this.tabDecode.Controls.Add(this.btnDecode);
            this.tabDecode.Controls.Add(this.btnOpen);
            this.tabDecode.Controls.Add(this.txtDecodedData);
            this.tabDecode.Controls.Add(this.label8);
            this.tabDecode.Controls.Add(this.picDecode);
            this.tabDecode.Location = new System.Drawing.Point(4, 22);
            this.tabDecode.Name = "tabDecode";
            this.tabDecode.Padding = new System.Windows.Forms.Padding(3);
            this.tabDecode.Size = new System.Drawing.Size(548, 475);
            this.tabDecode.TabIndex = 1;
            this.tabDecode.Text = "解码";
            this.tabDecode.UseVisualStyleBackColor = true;
            // 
            // btnDecode
            // 
            this.btnDecode.Location = new System.Drawing.Point(299, 353);
            this.btnDecode.Name = "btnDecode";
            this.btnDecode.Size = new System.Drawing.Size(75, 23);
            this.btnDecode.TabIndex = 3;
            this.btnDecode.Text = "解码";
            this.btnDecode.UseVisualStyleBackColor = true;
            // 
            // btnOpen
            // 
            this.btnOpen.Location = new System.Drawing.Point(164, 353);
            this.btnOpen.Name = "btnOpen";
            this.btnOpen.Size = new System.Drawing.Size(75, 23);
            this.btnOpen.TabIndex = 3;
            this.btnOpen.Text = "打开文件";
            this.btnOpen.UseVisualStyleBackColor = true;
            // 
            // txtDecodedData
            // 
            this.txtDecodedData.Location = new System.Drawing.Point(54, 226);
            this.txtDecodedData.Multiline = true;
            this.txtDecodedData.Name = "txtDecodedData";
            this.txtDecodedData.Size = new System.Drawing.Size(487, 98);
            this.txtDecodedData.TabIndex = 2;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label8.Location = new System.Drawing.Point(6, 222);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(42, 21);
            this.label8.TabIndex = 1;
            this.label8.Text = "内容";
            // 
            // picDecode
            // 
            this.picDecode.BackColor = System.Drawing.Color.White;
            this.picDecode.Location = new System.Drawing.Point(7, 7);
            this.picDecode.Name = "picDecode";
            this.picDecode.Size = new System.Drawing.Size(535, 203);
            this.picDecode.TabIndex = 0;
            this.picDecode.TabStop = false;
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            // 
            // frmOrCode
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(563, 509);
            this.Controls.Add(this.tabMain);
            this.MaximizeBox = false;
            this.Name = "frmOrCode";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "二维码生成工具";
            this.tabMain.ResumeLayout(false);
            this.tabEncode.ResumeLayout(false);
            this.tabEncode.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picEncode)).EndInit();
            this.tabDecode.ResumeLayout(false);
            this.tabDecode.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picDecode)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl tabMain;
        private System.Windows.Forms.TabPage tabEncode;
        private System.Windows.Forms.TabPage tabDecode;
        private System.Windows.Forms.PictureBox picEncode;
        private System.Windows.Forms.ComboBox cboCorrectionLevel;
        private System.Windows.Forms.ComboBox cboVersion;
        private System.Windows.Forms.ComboBox cboEncoding;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtEncodeData;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnEncode;
        private System.Windows.Forms.TextBox txtSize;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.PictureBox picDecode;
        private System.Windows.Forms.SaveFileDialog saveFileDialog1;
        private System.Windows.Forms.Button btnDecode;
        private System.Windows.Forms.Button btnOpen;
        private System.Windows.Forms.TextBox txtDecodedData;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
    }
}

