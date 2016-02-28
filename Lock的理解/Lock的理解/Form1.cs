using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Threading;

namespace Lock的理解
{
    public partial class Form1 : Form
    {
        object lock_obj = new object();
        public Form1()
        {
            InitializeComponent();
        }
        
        private void button1_Click(object sender, EventArgs e)
        {
            Thread t1 = new Thread(() => { test(0); });
            Thread t2 = new Thread(() => { test(1); });
            Thread t3 = new Thread(() => { test1(2); });
            Thread t4 = new Thread(() => { test1(3); });
            t1.Start();
            t2.Start();
            t3.Start();
            t4.Start();
        }
        delegate void UpdateText_dele(int ID, int i);
        void UpdateText(int ID, int i)
        {
            Label[] labels = { label1, label2, label3, label4 };
            if (this.InvokeRequired)
                this.Invoke(new UpdateText_dele(UpdateText), ID, i);
            else
                labels[ID].Text = i.ToString();
        }
        void test(int ID)
        {
            
                for (int i = 0; i < 100; i++)
                {
                    lock ("456")
                    {

                        UpdateText(ID, i);
                        Thread.Sleep(500);
                    }
                }
            
        }
        void test1(int ID)
        {
            lock ("123")
            {
                for (int i = 0; i < 100; i++)
                {

                    UpdateText(ID, i);
                    Thread.Sleep(500);

                }
            }
        }
    }
}
