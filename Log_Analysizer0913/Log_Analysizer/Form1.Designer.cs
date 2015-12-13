namespace Log_Analysizer
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.button1 = new System.Windows.Forms.Button();
            this.radioButton_FCT = new System.Windows.Forms.RadioButton();
            this.radioButton_CAP = new System.Windows.Forms.RadioButton();
            this.radioButton_PCBA_FFT = new System.Windows.Forms.RadioButton();
            this.radioButton_PCBA_P = new System.Windows.Forms.RadioButton();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.progressBar1 = new System.Windows.Forms.ProgressBar();
            this.button_result = new System.Windows.Forms.Button();
            this.richTextBox2 = new System.Windows.Forms.RichTextBox();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.clearToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.button2 = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.radioButton2 = new System.Windows.Forms.RadioButton();
            this.radioButton1 = new System.Windows.Forms.RadioButton();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.label_assembly = new System.Windows.Forms.Label();
            this.linkLabel1 = new System.Windows.Forms.LinkLabel();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.radioButton_A145 = new System.Windows.Forms.RadioButton();
            this.radioButton_B22 = new System.Windows.Forms.RadioButton();
            this.resultBox1 = new Log_Analysizer.ResultBox();
            this.contextMenuStrip1.SuspendLayout();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(607, 55);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(86, 23);
            this.button1.TabIndex = 1;
            this.button1.Text = "载入CSV文件";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // radioButton_FCT
            // 
            this.radioButton_FCT.AutoSize = true;
            this.radioButton_FCT.Location = new System.Drawing.Point(12, 205);
            this.radioButton_FCT.Name = "radioButton_FCT";
            this.radioButton_FCT.Size = new System.Drawing.Size(65, 16);
            this.radioButton_FCT.TabIndex = 2;
            this.radioButton_FCT.TabStop = true;
            this.radioButton_FCT.Text = "MLB_FCT";
            this.radioButton_FCT.UseVisualStyleBackColor = true;
            // 
            // radioButton_CAP
            // 
            this.radioButton_CAP.AutoSize = true;
            this.radioButton_CAP.Location = new System.Drawing.Point(83, 205);
            this.radioButton_CAP.Name = "radioButton_CAP";
            this.radioButton_CAP.Size = new System.Drawing.Size(65, 16);
            this.radioButton_CAP.TabIndex = 3;
            this.radioButton_CAP.TabStop = true;
            this.radioButton_CAP.Text = "MLB_CAP";
            this.radioButton_CAP.UseVisualStyleBackColor = true;
            // 
            // radioButton_PCBA_FFT
            // 
            this.radioButton_PCBA_FFT.AutoSize = true;
            this.radioButton_PCBA_FFT.Location = new System.Drawing.Point(154, 205);
            this.radioButton_PCBA_FFT.Name = "radioButton_PCBA_FFT";
            this.radioButton_PCBA_FFT.Size = new System.Drawing.Size(71, 16);
            this.radioButton_PCBA_FFT.TabIndex = 4;
            this.radioButton_PCBA_FFT.TabStop = true;
            this.radioButton_PCBA_FFT.Text = "PCBA_FFT";
            this.radioButton_PCBA_FFT.UseVisualStyleBackColor = true;
            // 
            // radioButton_PCBA_P
            // 
            this.radioButton_PCBA_P.AutoSize = true;
            this.radioButton_PCBA_P.Location = new System.Drawing.Point(231, 205);
            this.radioButton_PCBA_P.Name = "radioButton_PCBA_P";
            this.radioButton_PCBA_P.Size = new System.Drawing.Size(149, 16);
            this.radioButton_PCBA_P.TabIndex = 5;
            this.radioButton_PCBA_P.TabStop = true;
            this.radioButton_PCBA_P.Text = "PCBA_PowerConsumption";
            this.radioButton_PCBA_P.UseVisualStyleBackColor = true;
            // 
            // statusStrip1
            // 
            this.statusStrip1.Location = new System.Drawing.Point(0, 402);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(740, 22);
            this.statusStrip1.TabIndex = 6;
            this.statusStrip1.Text = "statusStrip1";
            this.statusStrip1.ItemClicked += new System.Windows.Forms.ToolStripItemClickedEventHandler(this.statusStrip1_ItemClicked);
            // 
            // progressBar1
            // 
            this.progressBar1.Location = new System.Drawing.Point(0, 406);
            this.progressBar1.Name = "progressBar1";
            this.progressBar1.Size = new System.Drawing.Size(209, 15);
            this.progressBar1.TabIndex = 7;
            // 
            // button_result
            // 
            this.button_result.Location = new System.Drawing.Point(607, 112);
            this.button_result.Name = "button_result";
            this.button_result.Size = new System.Drawing.Size(86, 23);
            this.button_result.TabIndex = 9;
            this.button_result.Text = "分析";
            this.button_result.UseVisualStyleBackColor = true;
            this.button_result.Click += new System.EventHandler(this.button_result_Click);
            // 
            // richTextBox2
            // 
            this.richTextBox2.ContextMenuStrip = this.contextMenuStrip1;
            this.richTextBox2.EnableAutoDragDrop = true;
            this.richTextBox2.Location = new System.Drawing.Point(12, 57);
            this.richTextBox2.Name = "richTextBox2";
            this.richTextBox2.Size = new System.Drawing.Size(577, 135);
            this.richTextBox2.TabIndex = 10;
            this.richTextBox2.Text = "";
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.clearToolStripMenuItem});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(107, 26);
            // 
            // clearToolStripMenuItem
            // 
            this.clearToolStripMenuItem.Name = "clearToolStripMenuItem";
            this.clearToolStripMenuItem.Size = new System.Drawing.Size(106, 22);
            this.clearToolStripMenuItem.Text = "Clear";
            this.clearToolStripMenuItem.Click += new System.EventHandler(this.clearToolStripMenuItem_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label1.ForeColor = System.Drawing.Color.Red;
            this.label1.Location = new System.Drawing.Point(12, 18);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(512, 16);
            this.label1.TabIndex = 11;
            this.label1.Text = "请注意：所载入的CSV档案必须有SN，并且第六行开始是产品的数据";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(217, 408);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(0, 12);
            this.label2.TabIndex = 12;
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(607, 169);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(86, 23);
            this.button2.TabIndex = 14;
            this.button2.Text = "保存结果";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click_1);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.radioButton2);
            this.groupBox1.Controls.Add(this.radioButton1);
            this.groupBox1.Location = new System.Drawing.Point(133, 228);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(92, 39);
            this.groupBox1.TabIndex = 15;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "CSV分隔符";
            // 
            // radioButton2
            // 
            this.radioButton2.AutoSize = true;
            this.radioButton2.Location = new System.Drawing.Point(44, 13);
            this.radioButton2.Name = "radioButton2";
            this.radioButton2.Size = new System.Drawing.Size(29, 16);
            this.radioButton2.TabIndex = 1;
            this.radioButton2.TabStop = true;
            this.radioButton2.Text = ";";
            this.radioButton2.UseVisualStyleBackColor = true;
            // 
            // radioButton1
            // 
            this.radioButton1.AutoSize = true;
            this.radioButton1.Location = new System.Drawing.Point(9, 13);
            this.radioButton1.Name = "radioButton1";
            this.radioButton1.Size = new System.Drawing.Size(29, 16);
            this.radioButton1.TabIndex = 0;
            this.radioButton1.TabStop = true;
            this.radioButton1.Text = ",";
            this.radioButton1.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            this.radioButton1.UseVisualStyleBackColor = true;
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox1.Image")));
            this.pictureBox1.Location = new System.Drawing.Point(539, 4);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(174, 45);
            this.pictureBox1.TabIndex = 16;
            this.pictureBox1.TabStop = false;
            // 
            // label_assembly
            // 
            this.label_assembly.AutoSize = true;
            this.label_assembly.BackColor = System.Drawing.Color.Lime;
            this.label_assembly.Location = new System.Drawing.Point(633, 409);
            this.label_assembly.Name = "label_assembly";
            this.label_assembly.Size = new System.Drawing.Size(0, 12);
            this.label_assembly.TabIndex = 19;
            // 
            // linkLabel1
            // 
            this.linkLabel1.AutoSize = true;
            this.linkLabel1.Location = new System.Drawing.Point(244, 243);
            this.linkLabel1.Name = "linkLabel1";
            this.linkLabel1.Size = new System.Drawing.Size(173, 12);
            this.linkLabel1.TabIndex = 20;
            this.linkLabel1.TabStop = true;
            this.linkLabel1.Text = "For change notice,click here";
            this.linkLabel1.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkLabel1_LinkClicked);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.radioButton_A145);
            this.groupBox2.Controls.Add(this.radioButton_B22);
            this.groupBox2.Location = new System.Drawing.Point(5, 228);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(113, 39);
            this.groupBox2.TabIndex = 21;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "项目选择";
            // 
            // radioButton_A145
            // 
            this.radioButton_A145.AutoSize = true;
            this.radioButton_A145.Location = new System.Drawing.Point(54, 17);
            this.radioButton_A145.Name = "radioButton_A145";
            this.radioButton_A145.Size = new System.Drawing.Size(47, 16);
            this.radioButton_A145.TabIndex = 1;
            this.radioButton_A145.TabStop = true;
            this.radioButton_A145.Text = "A145";
            this.radioButton_A145.UseVisualStyleBackColor = true;
            // 
            // radioButton_B22
            // 
            this.radioButton_B22.AutoSize = true;
            this.radioButton_B22.Location = new System.Drawing.Point(7, 17);
            this.radioButton_B22.Name = "radioButton_B22";
            this.radioButton_B22.Size = new System.Drawing.Size(41, 16);
            this.radioButton_B22.TabIndex = 0;
            this.radioButton_B22.TabStop = true;
            this.radioButton_B22.Text = "B22";
            this.radioButton_B22.UseVisualStyleBackColor = true;
            // 
            // resultBox1
            // 
            this.resultBox1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.resultBox1.Location = new System.Drawing.Point(0, 273);
            this.resultBox1.Name = "resultBox1";
            this.resultBox1.Size = new System.Drawing.Size(740, 129);
            this.resultBox1.TabIndex = 13;
            this.resultBox1.Load += new System.EventHandler(this.resultBox1_Load);
            // 
            // Form1
            // 
            this.AllowDrop = true;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(740, 424);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.resultBox1);
            this.Controls.Add(this.progressBar1);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.linkLabel1);
            this.Controls.Add(this.label_assembly);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.richTextBox2);
            this.Controls.Add(this.button_result);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.radioButton_PCBA_P);
            this.Controls.Add(this.radioButton_PCBA_FFT);
            this.Controls.Add(this.radioButton_CAP);
            this.Controls.Add(this.radioButton_FCT);
            this.Controls.Add(this.button1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.KeyPreview = true;
            this.MaximizeBox = false;
            this.Name = "Form1";
            this.Text = "Log_Analyzer Author:Tony.li";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form1_FormClosing);
            this.Load += new System.EventHandler(this.Form1_Load);
            this.DragDrop += new System.Windows.Forms.DragEventHandler(this.Form1_DragDrop);
            this.DragEnter += new System.Windows.Forms.DragEventHandler(this.Form1_DragEnter);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.Form1_KeyDown);
            this.contextMenuStrip1.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.RadioButton radioButton_FCT;
        private System.Windows.Forms.RadioButton radioButton_CAP;
        private System.Windows.Forms.RadioButton radioButton_PCBA_FFT;
        private System.Windows.Forms.RadioButton radioButton_PCBA_P;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.Button button_result;
        private System.Windows.Forms.RichTextBox richTextBox2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        public ResultBox resultBox1;
        public System.Windows.Forms.ProgressBar progressBar1;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.RadioButton radioButton2;
        private System.Windows.Forms.RadioButton radioButton1;
        public System.Windows.Forms.Button button2;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem clearToolStripMenuItem;
        private System.Windows.Forms.Label label_assembly;
        private System.Windows.Forms.LinkLabel linkLabel1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.RadioButton radioButton_A145;
        private System.Windows.Forms.RadioButton radioButton_B22;
    }
}

