namespace 串口调试助手_Tony_Supper
{
    partial class MidForm
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
            this.panel_serialport = new System.Windows.Forms.Panel();
            this.panel2 = new System.Windows.Forms.Panel();
            this.comboBox_delay = new System.Windows.Forms.ComboBox();
            this.textBox_cmd = new System.Windows.Forms.TextBox();
            this.label_delay = new System.Windows.Forms.Label();
            this.button_Send = new System.Windows.Forms.Button();
            this.checkBox_autosend = new System.Windows.Forms.CheckBox();
            this.button_ReScan = new System.Windows.Forms.Button();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel_serialport
            // 
            this.panel_serialport.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel_serialport.AutoScroll = true;
            this.panel_serialport.Location = new System.Drawing.Point(0, 58);
            this.panel_serialport.Name = "panel_serialport";
            this.panel_serialport.Size = new System.Drawing.Size(675, 249);
            this.panel_serialport.TabIndex = 8;
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.comboBox_delay);
            this.panel2.Controls.Add(this.textBox_cmd);
            this.panel2.Controls.Add(this.label_delay);
            this.panel2.Controls.Add(this.button_Send);
            this.panel2.Controls.Add(this.checkBox_autosend);
            this.panel2.Controls.Add(this.button_ReScan);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel2.Location = new System.Drawing.Point(0, 0);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(675, 52);
            this.panel2.TabIndex = 9;
            // 
            // comboBox_delay
            // 
            this.comboBox_delay.FormattingEnabled = true;
            this.comboBox_delay.Location = new System.Drawing.Point(123, 27);
            this.comboBox_delay.Name = "comboBox_delay";
            this.comboBox_delay.Size = new System.Drawing.Size(68, 20);
            this.comboBox_delay.TabIndex = 2;
            // 
            // textBox_cmd
            // 
            this.textBox_cmd.Location = new System.Drawing.Point(3, 3);
            this.textBox_cmd.Name = "textBox_cmd";
            this.textBox_cmd.Size = new System.Drawing.Size(501, 21);
            this.textBox_cmd.TabIndex = 2;
            // 
            // label_delay
            // 
            this.label_delay.AutoSize = true;
            this.label_delay.Location = new System.Drawing.Point(81, 30);
            this.label_delay.Name = "label_delay";
            this.label_delay.Size = new System.Drawing.Size(41, 12);
            this.label_delay.TabIndex = 1;
            this.label_delay.Text = "Delay:";
            // 
            // button_Send
            // 
            this.button_Send.Location = new System.Drawing.Point(507, 2);
            this.button_Send.Name = "button_Send";
            this.button_Send.Size = new System.Drawing.Size(78, 23);
            this.button_Send.TabIndex = 3;
            this.button_Send.Text = "Send";
            this.button_Send.UseVisualStyleBackColor = true;
            // 
            // checkBox_autosend
            // 
            this.checkBox_autosend.AutoSize = true;
            this.checkBox_autosend.Location = new System.Drawing.Point(3, 30);
            this.checkBox_autosend.Name = "checkBox_autosend";
            this.checkBox_autosend.Size = new System.Drawing.Size(72, 16);
            this.checkBox_autosend.TabIndex = 0;
            this.checkBox_autosend.Text = "AutoSend";
            this.checkBox_autosend.UseVisualStyleBackColor = true;
            // 
            // button_ReScan
            // 
            this.button_ReScan.Location = new System.Drawing.Point(591, 2);
            this.button_ReScan.Name = "button_ReScan";
            this.button_ReScan.Size = new System.Drawing.Size(78, 23);
            this.button_ReScan.TabIndex = 4;
            this.button_ReScan.Text = "ReScan";
            this.button_ReScan.UseVisualStyleBackColor = true;
            this.button_ReScan.Click += new System.EventHandler(this.button_ReScan_Click);
            // 
            // MidForm
            // 
            this.AcceptButton = this.button_Send;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(675, 303);
            this.CloseButton = false;
            this.CloseButtonVisible = false;
            this.Controls.Add(this.panel_serialport);
            this.Controls.Add(this.panel2);
            this.DockAreas = WeifenLuo.WinFormsUI.Docking.DockAreas.Document;
            this.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.Name = "MidForm";
            this.Text = "Option&&Config";
            this.Load += new System.EventHandler(this.MidForm_Load);
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        public System.Windows.Forms.Panel panel_serialport;
        public System.Windows.Forms.Panel panel2;
        public System.Windows.Forms.TextBox textBox_cmd;
        public System.Windows.Forms.Button button_Send;
        public System.Windows.Forms.Button button_ReScan;
        private System.Windows.Forms.ComboBox comboBox_delay;
        private System.Windows.Forms.Label label_delay;
        public System.Windows.Forms.CheckBox checkBox_autosend;

    }
}