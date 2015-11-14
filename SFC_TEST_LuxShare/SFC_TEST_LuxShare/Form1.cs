using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Checkroute;
using System.IO;

namespace SFC_TEST_LuxShare
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        Checkroute.Mescheckroute mes;
        private void button1_Click(object sender, EventArgs e)
        {
            string message;
            mes = new Mescheckroute();
            string SN = "TS15022700274";
            string Station = "PT-3";
            bool result = mes.checkroute("TS15022700274", "PT-3", out message);
            if (result)
                MessageBox.Show(string.Format("{0}是属于站别{1}",SN,Station));
            else
                MessageBox.Show(message);
        }
        string str1 = "苏州1对1 资深教师 一对一提升更精准学大教育，中小学个性化辅导领军品牌，一线资深教师1对1精心辅导，普遍提升30-50分。苏州1对1.资深教师亲授，直击考点，能力成绩双提升。选择苏州1对1到学大";
        string str2 = "fhjfjdlfkoewkpgfjewoijfosnivchowajncsdhivewclojvweonvwlen";
        private void Form1_Load(object sender, EventArgs e)
        {
           // Convert(@"C:\Users\LSQ\Desktop\1.bmp");
        }
        void Convert(string path)
        {
            FileStream fl_read = new FileStream(path, FileMode.Open, FileAccess.Read);
            byte[] bytes = new byte[fl_read.Length];
            fl_read.Write(bytes,0,bytes.Length);
            fl_read.Close();
            foreach (byte i in bytes)
            {
                File.AppendAllText(@"C:\Users\LSQ\Desktop\bitmap.txt", i.ToString() + "\r\n", Encoding.Default);
            }
        }
        void Convert1(string path)
        {
            FileStream fl_read = new FileStream(path, FileMode.Open, FileAccess.Read);
            byte[] bytes = new byte[fl_read.Length];
            fl_read.Write(bytes, 0, bytes.Length);

            string s = "";
            s += Encoding.Default.GetString(bytes, 0, bytes.Length);
            MessageBox.Show(s);
        }
    }
}
