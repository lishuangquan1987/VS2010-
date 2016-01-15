using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using 图片的各种处理;

namespace 图片处理2
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }      
        Bitmap bmp = null;
        private void Form1_Load(object sender, EventArgs e)
        {
            foreach (Control i in this.panel1.Controls)
            {
                if (i is RadioButton)
                {
                    ((RadioButton)i).CheckedChanged += Ratio_button_checkChange;
                }
            }
        }
        
        void Ratio_button_checkChange(object sender, EventArgs e)
        {
            if(bmp==null)
            {
                MessageBox.Show("请先选择图片");
                return;
            }
            RadioButton r=(RadioButton)sender;
            if(!r.Checked)
            return;
            string text = r.Text;
            switch (text)
            {
                case "原图": this.pictureBox1.Image = bmp; break;
                case "黑白色": ImageHandle.HeiBaiSeImage(bmp, this.pictureBox1); break;
                case"雾化":ImageHandle.WuHuaImage(bmp, this.pictureBox1); break;
                case "蜕化":ImageHandle.RuiHuaImage(bmp, this.pictureBox1); break;
                case"底片":ImageHandle.DiPianImage(bmp, this.pictureBox1); break;
                case"浮雕":ImageHandle.FuDiaoImage(bmp, this.pictureBox1); break;
                case"日光照射":ImageHandle.RiGuangZhaoSheImage(bmp, this.pictureBox1); break;
                case"油画":ImageHandle.YouHuaImage(bmp, this.pictureBox1); break;
                case "垂直百叶窗":ImageHandle.BaiYeChuang1(bmp, this.pictureBox1); break;
                case"水平百叶窗":ImageHandle.BaiYeChuang2(bmp, this.pictureBox1); break;
                case "淡入":ImageHandle.DanRu(bmp, this.pictureBox1); break;
                case"逆时针旋转":ImageHandle.XuanZhuan90(bmp, this.pictureBox1); break;
                case"顺时针旋转":ImageHandle.XuanZhuan270(bmp, this.pictureBox1); break;
                case"分块":ImageHandle.FenKuai(bmp, this.pictureBox1); break;
                case"积木":ImageHandle.JiMu(bmp, this.pictureBox1); break; 
                case"马赛克":ImageHandle.MaSaiKe(bmp, this.pictureBox1); break;
                case"自动旋转":ImageHandle.XuanZhuan(bmp, this.pictureBox1); break;
                case"上下对接":ImageHandle.DuiJie_ShangXia(bmp, this.pictureBox1); break;
                case"左右对接":ImageHandle.DuiJie_ZuoYou(bmp, this.pictureBox1); break;
                case"左右翻转":ImageHandle.FanZhuan_ZuoYou(bmp, this.pictureBox1); break;
                case"四周扩散":ImageHandle.KuoSan(bmp, this.pictureBox1); break;
                case "上下拉伸": ImageHandle.LaShen_ShangDaoXiao(bmp, this.pictureBox1); break;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            OpenFileDialog op = new OpenFileDialog();
            op.Filter = "BMP|*.bmp|jepg|*.jepg|png|*.png|jpg|*.jpg";
            op.Multiselect=false;
            if (op.ShowDialog() != System.Windows.Forms.DialogResult.OK)
                return;
            bmp = new Bitmap(op.FileName);
            original.Checked = true;
        }
    }
}
