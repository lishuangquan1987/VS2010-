using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using Microsoft.Win32;

namespace Capture_LikeQQ
{
    public partial class Form1 : Form
    {
        public const int SW_HIDE=0;
        public const int SW_SHOWNORMAL = 1;
        public const int SW_SHOWMINIMIZED = 2;
        public const int SW_SHOWMAXIMIZED = 3;
        public const int SW_MAXIMIZE = 3;
        public const int SW_SHOWNOACTIVATE = 4;
        public const int SW_SHOW = 5;
        public const int SW_MINIMIZE = 6;
        public const int SW_SHOWMINNOACTIVE = 7;
        public const int SW_SHOWNA = 8;
        public const int SW_RESTORE = 9;


        public Form1()
        {
            InitializeComponent();
        }
        bool isReady = true;
        private void Form1_Load(object sender, EventArgs e)
        {
            this.checkBox_AutoRun.Checked = true;
            this.Hide();
            
            if (RegisterHotKey(this.Handle, 100, KeyModifiers.Alt | KeyModifiers.Ctrl, Keys.A))
            {
                label2.ForeColor = Color.Green;
                label2.Text = "截图热键：Ctrl+Alt+A";
            }
            else
            {
                label2.ForeColor = Color.Red;
                label2.Text = "热键：Ctrl+Alt+A冲突\r\n请检查其他程序是否占\r\n用此热键！";
                isReady = false;
            }
            
        }
        [DllImport("user32.dll", SetLastError = true)]
        public static extern bool RegisterHotKey(
            IntPtr hWnd,                //要定义热键的窗口的句柄
            int id,                     //定义热键ID（不能与其它ID重复）           
            KeyModifiers fsModifiers,   //标识热键是否在按Alt、Ctrl、Shift、Windows等键时才会生效
            Keys vk                     //定义热键的内容
            );

        [DllImport("user32.dll", SetLastError = true)]
        public static extern bool UnregisterHotKey(
            IntPtr hWnd,                //要取消热键的窗口的句柄
            int id                      //要取消热键的ID
            );
        [DllImport("user32.dll")]
        public static extern bool SetForegroundWindow(IntPtr hWnd);
        [DllImport("user32.dll")]
        public static extern bool ShowWindow(IntPtr hwnd, int cmdShow);
        //定义了辅助键的名称（将数字转变为字符以便于记忆，也可去除此枚举而直接使用数值）
        [Flags()]
        public enum KeyModifiers
        {
            None = 0,
            Alt = 1,
            Ctrl = 2,
            Shift = 4,
            WindowsKey = 8
        }
        Demo d;
        protected override void WndProc(ref Message m)
        {
            const int WM_HOTKEY = 0X0312;
            if (m.Msg == WM_HOTKEY && m.WParam.ToString() == "100")
            {
                StartCapture();
            }

            base.WndProc(ref m);
        }
        void StartCapture()
        {
            if (d == null||d.IsDisposed)
                d = new Demo();
            //开始截图(截取全屏幕)
            int width = Screen.PrimaryScreen.Bounds.Width;
            int height = Screen.PrimaryScreen.Bounds.Height;

            Point p = new Point(0, 0);
            Bitmap bmp = Capture_LikeQQ.Capture.GetImage(new Point(0, 0), new Size(width, height));

            d.BackgroundImage = bmp;
            d.Show();
            //this.Hide();
        }
        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            UnregisterHotKey(this.Handle, 100);
            this.notifyIcon1.Dispose();
        }
       
        /// <summary>
        /// 设置开机启动
        /// </summary>
        /// <param name="fileName"></param>
        /// <param name="isAutoRun"></param>
        public static void SetAutoRun(string fileName, bool isAutoRun)
        {
            RegistryKey reg = null;
            try
            {
                if (!System.IO.File.Exists(fileName))
                    throw new Exception("该文件不存在!");
                String name = fileName.Substring(fileName.LastIndexOf(@"\") + 1);
                reg = Registry.LocalMachine.OpenSubKey(@"SOFTWARE\Microsoft\Windows\CurrentVersion\Run", true);
                if (reg == null)
                    reg = Registry.LocalMachine.CreateSubKey(@"SOFTWARE\Microsoft\Windows\CurrentVersion\Run");
                if (isAutoRun)
                    reg.SetValue(name, fileName);
                else
                    reg.SetValue(name, false);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.ToString());
            }
            finally
            {
                if (reg != null)
                    reg.Close();
            }

        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            MessageBox.Show("Made by Tony!\r\nAnthor's QQ:294388344");
        }

        private void showToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Show();
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void label1_Click(object sender, EventArgs e)
        {
            this.Hide();
            if(isReady)
            notifyIcon1.ShowBalloonTip(2000, "提示", "按Ctrl+Alt+A可以开始截图", ToolTipIcon.Info);
            else
             notifyIcon1.ShowBalloonTip(2000, "错误", "热键Ctrl+Alt+A与其他程序冲突，截图功能无法启用", ToolTipIcon.Error);

        }

        private void notifyIcon1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            this.Show();
        }

        private void checkBox_AutoRun_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox_AutoRun.Checked)
                SetAutoRun(Application.ExecutablePath,true);
            else
                SetAutoRun(Application.ExecutablePath, false);
        }

        private void linkLabel2_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            this.Hide();
            System.Threading.Thread.Sleep(500);
            StartCapture();
            this.Show();
        }
    }
}
