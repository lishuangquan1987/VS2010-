using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Reflection;
using LibCom;

namespace TM
{
    public partial class SpalishForm : Form
    {
        public SpalishForm()
        {
            InitializeComponent();
        }
        public void AppLoad()
        { 

        }
        public IMoudle LoadInstrument(string path)
        {
            IMoudle moudle = null;
            Assembly asm = Assembly.Load(path);
            foreach (Type t in asm.GetTypes())
            {
                if (t.GetInterface("IMoudle")!=null)
                {
                    moudle = asm.CreateInstance(t.FullName) as IMoudle; 
                }
            }
            return moudle;
        }
    }
    

}
