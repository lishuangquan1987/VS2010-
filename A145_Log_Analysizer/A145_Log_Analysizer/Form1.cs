using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Threading;
using System.IO;
using NPOI.HSSF.UserModel;
using NPOI.SS.UserModel;
using NPOI.SS.Util;

namespace A145_Log_Analysizer
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }
#region~更新UI
        private delegate void UpdateResult(string msg,Color color,bool nextline);
        void updateResult(string msg, Color color, bool nextline)
        {
            if (this.richTextBox_Result.InvokeRequired)
                this.richTextBox_Result.Invoke(new UpdateResult(updateResult), msg, color, nextline);
            else
            {
                this.richTextBox_Result.SelectionColor = color;
                this.richTextBox_Result.AppendText(msg);
                if (nextline)
                    this.richTextBox_Result.AppendText("\r\n");
                this.richTextBox_Result.Focus();
            }
        }
        private delegate void UpdateStatus(string msg);
        void UpdateStatus_lable(string msg)
        {
            if (this.InvokeRequired)
                this.Invoke(new UpdateStatus(UpdateStatus_lable), msg);
            else
                this.toolStripStatusLabel1.Text = msg;
        }
        void UpdateStatus_processBar(string msg)
        {
            if (this.InvokeRequired)
                this.Invoke(new UpdateStatus(UpdateStatus_processBar), msg);
            else
                this.toolStripProgressBar1.Value = int.Parse(msg);
        }
#endregion
        private void button_result_Click(object sender, EventArgs e)
        {
            string[] FileNames=richTextBox2.Text.Split('\n');
            if (!GetData(FileNames))
                return;
            Analysize();
            MessageBox.Show("分析完毕！");
            
        }
        string[] Head=new string[5];
        List<string> Content=new List<string>();
        A145_FCT[] A145s=null;
        List<A145_FCT> A145s_list = new List<A145_FCT>();
        bool CheckLog(string[] temp)
        {
               if (!temp[4].Contains("U"))
                {
                    MessageBox.Show("csv第5行应该以U开头\r\n请检查csv格式是否正确！", "错误提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return false;
                }
               if (!temp[5].StartsWith("Z")&&!temp[5].StartsWith("C")&&!temp[5].ToUpper().StartsWith("K"))
               {
                   MessageBox.Show("csv第6行应该以Z开头\r\n请检查csv格式是否正确！", "错误提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
                   return false;
               }
               return true;
        }
        bool GetData(string[] FileNames)
        {
            
            for (int i = 0; i < FileNames.Length; i++)
            {
                if (FileNames[i] == "" || FileNames[i] == null)
                    continue;
                string[] temp=File.ReadAllLines(FileNames[i],Encoding.Default);
                
                if (!CheckLog(temp))
                    return false;
                if(i==0)
                    Array.Copy(temp, 0, Head, 0, Head.Length);//提取Head
                for (int j = 5; j < temp.Length ; j++)
                {
                    Content.Add(temp[j]);

                }
            }
            
            for (int i = 0; i < Content.Count; i++)
            {
                A145_FCT A145_temp = new A145_FCT();
                string[] temp=Content[i].Split(',');
                if (temp[0].Trim() == "" || temp[0] == "nil" || temp[0] == null)
                    continue;//过滤条码为空的情况。
                A145_temp.SN = temp[0];
                A145_temp.StationID = temp[1];
                A145_temp.PASS_FAIL_Status = temp[2];
                A145_temp.SiteID = temp[3];
                A145_temp.FailItem = temp[4];
                A145_temp.StartTime = temp[5];
                A145_temp.TestTime = temp[6];
                A145_temp.USB_DP1_TO_GND = temp[7];
                A145_temp.USB_DN1_TO_GND = temp[8];
                A145_temp.USBC_VBUS_TO_GND = temp[9];
                A145_temp.USBC_VCONN_TO_GND = temp[10];
                A145_temp.USBC_CC1_TO_GND = temp[11];
                A145_temp.USBC_VBUS_TO_USBC_CC1 = temp[12];
                A145_temp.USBC_VBUS_TO_USBC_VCONN = temp[13];
                A145_temp.USBC_CC1_TO_USB_DP1 = temp[14];
                A145_temp.USB_DP1_TO_USB_DN1 = temp[15];
                A145_temp.USBC_VBUS_TO_USB_DN1 = temp[16];
                A145_temp.PP_USBC_VBUS = temp[17];
                A145_temp.TP0304 = temp[18];
                A145_temp.USBC_CC1 = temp[19];
                A145_temp.USB_DP1 = temp[20];
                A145_temp.USB_DN1 = temp[21];
                A145_temp.CURRENT_CC1_475uA = temp[22];
                A145_temp.CURRENT_VCONN_475uA = temp[23];
                A145_temp.CURRENT_CC1_550uA = temp[24];
                A145_temp.CURRENT_VCONN_550uA = temp[25];

                A145s_list.Add(A145_temp);
            }
            A145s = A145s_list.ToArray();
                return true;
        }
        private void Form1_DragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
                e.Effect = DragDropEffects.Link;
            else
                e.Effect = DragDropEffects.None;
        }

        private void Form1_DragDrop(object sender, DragEventArgs e)
        {
            Array paths = (Array)e.Data.GetData(DataFormats.FileDrop);
            for (int i = 0; i < paths.Length; i++)
            {
                this.richTextBox2.AppendText(paths.GetValue(i).ToString()+"\r\n");
                this.Refresh();
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.richTextBox2.Clear();
            OpenFileDialog op = new OpenFileDialog();
            op.Filter = "CSV文件|*.csv";
            op.Title = "请选择CSV文件";
            op.Multiselect = true;
            if (op.ShowDialog() == DialogResult.OK)
            {
                foreach (string filename in op.FileNames)
                this.richTextBox2.AppendText(filename + "\r\n");
            }
        }
        A145_FCT[][] content;//存放每个sheet的所有FCT的对象
        void Analysize()
        {
            var SiteIDs = from i in A145s select i.SiteID;
            
            string[] SiteIDs_Distinct = SiteIDs.Distinct().ToArray();
           SiteIDs_Distinct= (SiteIDs_Distinct.OrderBy(i => i)).ToArray();
            content=new A145_FCT[SiteIDs_Distinct.Length][];
            for (int i = 0; i < SiteIDs_Distinct.Length; i++)
            {
                content[i] = (from j in A145s where j.SiteID == SiteIDs_Distinct[i] select j).ToArray(); 

            }

        }
        HSSFWorkbook workbook;
        ISheet sheet;
        FileStream fs_write;
        bool saveAndWriteContent()
        {
            string path="";
            string[] _path = this.richTextBox2.Text.Split('\n')[0].Split('\\');
            string[] _newPath=new string[_path.Length-1];
            Array.Copy(_path, 0, _newPath, 0, _path.Length - 1);
            foreach (string i in _newPath)
            {
                path += i+"\\";
            }
            path += "new.xls";
            if(File.Exists(path))
                File.Delete(path);
            try
            {
                fs_write = new FileStream(path, FileMode.Create, FileAccess.Write);
                workbook = new HSSFWorkbook();
            }
            catch (Exception e)
            {
                MessageBox.Show("创建文件失败" + path);
                fs_write.Close();
                return false;
            }
            //content:二维数组，存储每个sheet的所有A145_FCT对象
            #region~添加原始数据
            sheet = workbook.CreateSheet("原始数据");
            for (int j = 0; j < Head.Length; j++)
            {
                string[] head_temp = Head[j].Split(',');
                for (int k = 0; k < head_temp.Length; k++)
                {
                    if (k == 0)
                        sheet.CreateRow(j).CreateCell(0).SetCellValue(head_temp[k]);
                    else
                        sheet.GetRow(j).CreateCell(k).SetCellValue(head_temp[k]);
                }
            }
            for (int i = 0; i < A145s.Length; i++)
            {
                sheet.CreateRow(i+5).CreateCell(0).SetCellValue(A145s[i].SN);
                
                sheet.GetRow(i+5).CreateCell(1).SetCellValue(A145s[i].StationID);
                sheet.GetRow(i+5).CreateCell(2).SetCellValue(A145s[i].PASS_FAIL_Status);
                sheet.GetRow(i+5).CreateCell(3).SetCellValue(A145s[i].SiteID);
                sheet.GetRow(i+5).CreateCell(4).SetCellValue(A145s[i].FailItem);
                sheet.GetRow(i+5).CreateCell(5).SetCellValue(A145s[i].StartTime);
                sheet.GetRow(i+5).CreateCell(6).SetCellValue(A145s[i].TestTime);
                sheet.GetRow(i+5).CreateCell(7).SetCellValue(A145s[i].USB_DP1_TO_GND);
                sheet.GetRow(i+5).CreateCell(8).SetCellValue(A145s[i].USB_DN1_TO_GND);
                sheet.GetRow(i+5).CreateCell(9).SetCellValue(A145s[i].USBC_VBUS_TO_GND);
                sheet.GetRow(i+5).CreateCell(10).SetCellValue(A145s[i].USBC_VCONN_TO_GND);
                sheet.GetRow(i+5).CreateCell(11).SetCellValue(A145s[i].USBC_CC1_TO_GND);
                sheet.GetRow(i+5).CreateCell(12).SetCellValue(A145s[i].USBC_VBUS_TO_USBC_CC1);
                sheet.GetRow(i+5).CreateCell(13).SetCellValue(A145s[i].USBC_VBUS_TO_USBC_VCONN);
                sheet.GetRow(i+5).CreateCell(14).SetCellValue(A145s[i].USBC_CC1_TO_USB_DP1);
                sheet.GetRow(i+5).CreateCell(15).SetCellValue(A145s[i].USB_DP1_TO_USB_DN1);
                sheet.GetRow(i+5).CreateCell(16).SetCellValue(A145s[i].USBC_VBUS_TO_USB_DN1);
                sheet.GetRow(i+5).CreateCell(17).SetCellValue(A145s[i].PP_USBC_VBUS);
                sheet.GetRow(i+5).CreateCell(18).SetCellValue(A145s[i].TP0304);
                sheet.GetRow(i+5).CreateCell(19).SetCellValue(A145s[i].USBC_CC1);
                sheet.GetRow(i+5).CreateCell(20).SetCellValue(A145s[i].USB_DP1);
                sheet.GetRow(i+5).CreateCell(21).SetCellValue(A145s[i].USB_DN1);
                sheet.GetRow(i+5).CreateCell(22).SetCellValue(A145s[i].CURRENT_CC1_475uA);
                sheet.GetRow(i+5).CreateCell(23).SetCellValue(A145s[i].CURRENT_VCONN_475uA);
                sheet.GetRow(i+5).CreateCell(24).SetCellValue(A145s[i].CURRENT_CC1_550uA);
                sheet.GetRow(i+5).CreateCell(25).SetCellValue(A145s[i].CURRENT_VCONN_550uA);
            }
            #endregion
            for (int i = 0; i < content.Length; i++)
            {
                #region~添加每个sheet的数据
                sheet = workbook.CreateSheet("#" + content[i][0].SiteID);
                for (int j = 0; j < Head.Length; j++)
                {
                    string[] head_temp = Head[j].Split(',');
                    for (int k = 0; k < head_temp.Length; k++)
                    {
                        if (k == 0)
                            sheet.CreateRow(j).CreateCell(0).SetCellValue(head_temp[k]);
                        else
                            sheet.GetRow(j).CreateCell(k).SetCellValue(head_temp[k]);
                    }
                }
                    for (int j = 0; j < content[i].Length; j++)
                    {
                        sheet.CreateRow(j+5).CreateCell(0).SetCellValue(content[i][j].SN);
                        sheet.GetRow(j+5).CreateCell(1).SetCellValue(content[i][j].StationID);
                        sheet.GetRow(j+5).CreateCell(2).SetCellValue(content[i][j].PASS_FAIL_Status);
                        sheet.GetRow(j+5).CreateCell(3).SetCellValue(content[i][j].SiteID);
                        sheet.GetRow(j+5).CreateCell(4).SetCellValue(content[i][j].FailItem);
                        sheet.GetRow(j+5).CreateCell(5).SetCellValue(content[i][j].StartTime);
                        sheet.GetRow(j+5).CreateCell(6).SetCellValue(content[i][j].TestTime);
                        sheet.GetRow(j+5).CreateCell(7).SetCellValue(content[i][j].USB_DP1_TO_GND);
                        sheet.GetRow(j+5).CreateCell(8).SetCellValue(content[i][j].USB_DN1_TO_GND);
                        sheet.GetRow(j+5).CreateCell(9).SetCellValue(content[i][j].USBC_VBUS_TO_GND);
                        sheet.GetRow(j+5).CreateCell(10).SetCellValue(content[i][j].USBC_VCONN_TO_GND);
                        sheet.GetRow(j+5).CreateCell(11).SetCellValue(content[i][j].USBC_CC1_TO_GND);
                        sheet.GetRow(j+5).CreateCell(12).SetCellValue(content[i][j].USBC_VBUS_TO_USBC_CC1);
                        sheet.GetRow(j+5).CreateCell(13).SetCellValue(content[i][j].USBC_VBUS_TO_USBC_VCONN);
                        sheet.GetRow(j+5).CreateCell(14).SetCellValue(content[i][j].USBC_CC1_TO_USB_DP1);
                        sheet.GetRow(j+5).CreateCell(15).SetCellValue(content[i][j].USB_DP1_TO_USB_DN1);
                        sheet.GetRow(j+5).CreateCell(16).SetCellValue(content[i][j].USBC_VBUS_TO_USB_DN1);
                        sheet.GetRow(j+5).CreateCell(17).SetCellValue(content[i][j].PP_USBC_VBUS);
                        sheet.GetRow(j+5).CreateCell(18).SetCellValue(content[i][j].TP0304);
                        sheet.GetRow(j+5).CreateCell(19).SetCellValue(content[i][j].USBC_CC1);
                        sheet.GetRow(j+5).CreateCell(20).SetCellValue(content[i][j].USB_DP1);
                        sheet.GetRow(j+5).CreateCell(21).SetCellValue(content[i][j].USB_DN1);
                        sheet.GetRow(j+5).CreateCell(22).SetCellValue(content[i][j].CURRENT_CC1_475uA);
                        sheet.GetRow(j+5).CreateCell(23).SetCellValue(content[i][j].CURRENT_VCONN_475uA);
                        sheet.GetRow(j+5).CreateCell(24).SetCellValue(content[i][j].CURRENT_CC1_550uA);
                        sheet.GetRow(j+5).CreateCell(25).SetCellValue(content[i][j].CURRENT_VCONN_550uA);

                    }
                #endregion

            }
            try
            {
                workbook.Write(fs_write);
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
                return false;
            }
            finally
            {
                if (fs_write != null)
                    fs_write.Close();
            }
            return true;
        }
        private void button2_Click(object sender, EventArgs e)
        {
            if (saveAndWriteContent())
                MessageBox.Show("保存成功！");
        }
    }
}
