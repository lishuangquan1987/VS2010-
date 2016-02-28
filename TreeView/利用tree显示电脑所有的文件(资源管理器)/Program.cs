using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace 利用tree显示电脑所有的文件_资源管理器_
{
    static class Program
    {
        /// <summary>
        /// 应用程序的主入口点。
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Form1());
        }
    }
}
