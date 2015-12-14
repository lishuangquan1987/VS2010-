namespace FCTBroad
{
    partial class ConfigDlg
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
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.tbConfig = new System.Windows.Forms.TextBox();
            this.ComboxComlist = new System.Windows.Forms.ComboBox();
            this.OK = new System.Windows.Forms.Button();
            this.btnRefresh = new System.Windows.Forms.Button();
            this.tbEEStatue = new System.Windows.Forms.TextBox();
            this.btnReset = new System.Windows.Forms.Button();
            this.btnHelp = new System.Windows.Forms.Button();
            this.buttonver = new System.Windows.Forms.Button();
            this.buttonsend = new System.Windows.Forms.Button();
            this.CMD = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label3.Location = new System.Drawing.Point(509, 113);
            this.label3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(159, 31);
            this.label3.TabIndex = 13;
            this.label3.Text = "115200,n,8,1";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label2.Location = new System.Drawing.Point(19, 113);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(95, 31);
            this.label2.TabIndex = 12;
            this.label2.Text = "Config:";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label1.Location = new System.Drawing.Point(19, 24);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(149, 31);
            this.label1.TabIndex = 11;
            this.label1.Text = "Com Name:";
            // 
            // tbConfig
            // 
            this.tbConfig.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.tbConfig.Location = new System.Drawing.Point(173, 109);
            this.tbConfig.Margin = new System.Windows.Forms.Padding(4);
            this.tbConfig.Name = "tbConfig";
            this.tbConfig.Size = new System.Drawing.Size(297, 39);
            this.tbConfig.TabIndex = 10;
            // 
            // ComboxComlist
            // 
            this.ComboxComlist.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.ComboxComlist.FormattingEnabled = true;
            this.ComboxComlist.Location = new System.Drawing.Point(173, 17);
            this.ComboxComlist.Margin = new System.Windows.Forms.Padding(4);
            this.ComboxComlist.Name = "ComboxComlist";
            this.ComboxComlist.Size = new System.Drawing.Size(297, 39);
            this.ComboxComlist.TabIndex = 9;
            // 
            // OK
            // 
            this.OK.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.OK.Location = new System.Drawing.Point(699, 101);
            this.OK.Margin = new System.Windows.Forms.Padding(4);
            this.OK.Name = "OK";
            this.OK.Size = new System.Drawing.Size(175, 57);
            this.OK.TabIndex = 8;
            this.OK.Text = "OK";
            this.OK.UseVisualStyleBackColor = true;
            this.OK.Click += new System.EventHandler(this.OK_Click);
            // 
            // btnRefresh
            // 
            this.btnRefresh.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnRefresh.Location = new System.Drawing.Point(699, 12);
            this.btnRefresh.Margin = new System.Windows.Forms.Padding(4);
            this.btnRefresh.Name = "btnRefresh";
            this.btnRefresh.Size = new System.Drawing.Size(175, 57);
            this.btnRefresh.TabIndex = 7;
            this.btnRefresh.Text = "ReScan";
            this.btnRefresh.UseVisualStyleBackColor = true;
            this.btnRefresh.Click += new System.EventHandler(this.btnRefresh_Click);
            // 
            // tbEEStatue
            // 
            this.tbEEStatue.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.tbEEStatue.Location = new System.Drawing.Point(0, 385);
            this.tbEEStatue.Margin = new System.Windows.Forms.Padding(4);
            this.tbEEStatue.Multiline = true;
            this.tbEEStatue.Name = "tbEEStatue";
            this.tbEEStatue.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.tbEEStatue.Size = new System.Drawing.Size(901, 336);
            this.tbEEStatue.TabIndex = 24;
            // 
            // btnReset
            // 
            this.btnReset.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnReset.Location = new System.Drawing.Point(220, 208);
            this.btnReset.Margin = new System.Windows.Forms.Padding(4);
            this.btnReset.Name = "btnReset";
            this.btnReset.Size = new System.Drawing.Size(175, 57);
            this.btnReset.TabIndex = 25;
            this.btnReset.Text = "Reset";
            this.btnReset.UseVisualStyleBackColor = true;
            this.btnReset.Click += new System.EventHandler(this.btnReset_Click);
            // 
            // btnHelp
            // 
            this.btnHelp.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnHelp.Location = new System.Drawing.Point(24, 208);
            this.btnHelp.Margin = new System.Windows.Forms.Padding(4);
            this.btnHelp.Name = "btnHelp";
            this.btnHelp.Size = new System.Drawing.Size(175, 57);
            this.btnHelp.TabIndex = 26;
            this.btnHelp.Text = "HELP";
            this.btnHelp.UseVisualStyleBackColor = true;
            this.btnHelp.Click += new System.EventHandler(this.btnHelp_Click);
            // 
            // buttonver
            // 
            this.buttonver.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.buttonver.Location = new System.Drawing.Point(415, 208);
            this.buttonver.Margin = new System.Windows.Forms.Padding(4);
            this.buttonver.Name = "buttonver";
            this.buttonver.Size = new System.Drawing.Size(175, 57);
            this.buttonver.TabIndex = 27;
            this.buttonver.Text = "Version";
            this.buttonver.UseVisualStyleBackColor = true;
            this.buttonver.Click += new System.EventHandler(this.buttonver_Click);
            // 
            // buttonsend
            // 
            this.buttonsend.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.buttonsend.Location = new System.Drawing.Point(699, 305);
            this.buttonsend.Margin = new System.Windows.Forms.Padding(4);
            this.buttonsend.Name = "buttonsend";
            this.buttonsend.Size = new System.Drawing.Size(175, 57);
            this.buttonsend.TabIndex = 28;
            this.buttonsend.Text = "Send";
            this.buttonsend.UseVisualStyleBackColor = true;
            this.buttonsend.Click += new System.EventHandler(this.buttonsend_Click);
            // 
            // CMD
            // 
            this.CMD.Location = new System.Drawing.Point(24, 317);
            this.CMD.Margin = new System.Windows.Forms.Padding(4);
            this.CMD.Name = "CMD";
            this.CMD.Size = new System.Drawing.Size(645, 35);
            this.CMD.TabIndex = 29;
            // 
            // ConfigDlg
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(12F, 24F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(901, 721);
            this.Controls.Add(this.CMD);
            this.Controls.Add(this.buttonsend);
            this.Controls.Add(this.buttonver);
            this.Controls.Add(this.btnHelp);
            this.Controls.Add(this.btnReset);
            this.Controls.Add(this.tbEEStatue);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.tbConfig);
            this.Controls.Add(this.ComboxComlist);
            this.Controls.Add(this.OK);
            this.Controls.Add(this.btnRefresh);
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "ConfigDlg";
            this.Text = "ConfigDlg";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox tbConfig;
        private System.Windows.Forms.ComboBox ComboxComlist;
        private System.Windows.Forms.Button OK;
        private System.Windows.Forms.Button btnRefresh;
        private System.Windows.Forms.TextBox tbEEStatue;
        private System.Windows.Forms.Button btnReset;
        private System.Windows.Forms.Button btnHelp;
        private System.Windows.Forms.Button buttonver;
        private System.Windows.Forms.Button buttonsend;
        private System.Windows.Forms.TextBox CMD;
    }
}