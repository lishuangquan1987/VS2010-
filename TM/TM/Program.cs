using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace TM
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            SpalishForm sf = new SpalishForm();
            sf.Show();
            sf.AppLoad();
            Application.DoEvents();
            sf.Close();
            Application.Run();
            //Application.EnableVisualStyles();
            //Application.SetCompatibleTextRenderingDefault(false);
            //Application.Run(new SpalishForm());
        }
    }
}
