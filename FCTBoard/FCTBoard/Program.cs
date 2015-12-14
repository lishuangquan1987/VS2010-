using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using FCTBroad;

namespace FCTBoard
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
            FCTBroad.GT_FCTBroad G = new GT_FCTBroad();
            Application.Run(new ConfigDlg());
        }
    }
}
