using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Threading;
using System.Reflection;

namespace Log_Analysizer
{
    public partial class Form1 : Form
    {
		//
        public Form1()
        {
            InitializeComponent();
        }
        OpenFileDialog op = null;
        string path;
        string[] First_content = null;
        public MLB_FCT[] fcts = null;
        List<MLB_FCT> list_fcts = null;
        string[] FileNames = null;
        string[][] AllFile_contents = null;
        List<string> contents = null;
        LogFile logfile = new LogFile();
        public bool analysize = false;
        private delegate void Delegete_updatelable(string msg);
        public void updateLable(string msg)//显示信息
        {
            if (this.label2.InvokeRequired)
                this.label2.Invoke(new Delegete_updatelable(updateLable), msg);
            else
                this.label2.Text = msg;

        }
        private delegate void Delegate_Updaterichtextbox2(string msg);
        public void update_richtextBox2(string msg)
        {
            if (this.richTextBox2.InvokeRequired)
                this.richTextBox2.Invoke(new Delegate_Updaterichtextbox2(update_richtextBox2),msg);
            else
            {
                this.richTextBox2.AppendText(msg);
                this.richTextBox2.Focus();
                
            }
        }
        void CheckLog(string FileName)
        {
            if (FileName.Contains("FCT"))
            {
                radioButton_FCT.Checked = true;
            }
            else if (FileName.Contains("CAP"))
            {
                radioButton_CAP.Checked = true;
            }
            else if (FileName.Contains("FFT"))
            {
                radioButton_PCBA_FFT.Checked = true;
            }
            else if (FileName.Contains("Power"))
            {
                radioButton_PCBA_P.Checked = true;
            }
        }
        bool GetAndWriteContent()
        {
            //bool result = false;
            #region~读出每个Log的内容
           
                for (int i = 0; i < FileNames.Length; i++)
                {
                    if (FileNames[i] == "" || FileNames[i] == null)
                        continue;
                    try
                    {
                        AllFile_contents[i] = File.ReadAllLines(FileNames[i], Encoding.Default);
                    }
                    catch (Exception e)
                    {
                        MessageBox.Show(e.Message);

                        return false;
                    }
                    for (int j = 0; j < AllFile_contents[i].Length; j++)
                    {
                        if (i == 0)//第一行
                            contents.Add(AllFile_contents[i][j]);
                        else
                        {
                            if (j >= 5)//从第个Log开始，前五行去掉
                                contents.Add(AllFile_contents[i][j]);
                        }

                    }
                    
                }

                First_content = contents.ToArray();
                //将contents内容写入到test.csv
                File.WriteAllLines(path + "\\test.csv", contents.ToArray(), Encoding.Default);
                return true;
           #endregion
 
        }
        private void button1_Click(object sender, EventArgs e)
        {
            op = new OpenFileDialog();
            op.Filter = "CSV文件|*.csv";
            op.Multiselect = true;
            
            if (path != null && path != "")
            {
                op.InitialDirectory = path;
            }
            
            if (op.ShowDialog() == DialogResult.OK)
            {
                FileNames = op.FileNames;
                //判断是什么LOG
                //CheckLog(op.FileName);
                richTextBox2.Text = "";
                
                string msg = null;
                foreach (string i in op.FileNames)
                {
                    msg += i + "\r\n";
                }
                Thread.Sleep(1);
                new Thread(() => { this.update_richtextBox2(msg); }).Start();

                //new Thread(new ThreadStart(GetAndWriteContent)).Start();  
            }
            
            
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            this.radioButton_FCT.Checked = true;
            //this.richTextBox1.ReadOnly = true;
            this.richTextBox2.ReadOnly = true;
            this.radioButton1.Checked = true;
            this.label_assembly.Text ="V"+ Assembly.GetEntryAssembly().GetName().Version.ToString();
        }
        delegate void Update(int value);
        public void UpdateProgresser(int value)
        {
            if (this.progressBar1.InvokeRequired)
                this.progressBar1.Invoke(new Update(UpdateProgresser), value);
            else
                this.progressBar1.Value = value;
        }
        /// <summary>
        /// 之前的逻辑
        /// </summary>
        void thread_AnalysizeData()
        {
            int startTime = System.Environment.TickCount;
            #region~判断Log格式是否正确
            if (First_content == null)
            {
                MessageBox.Show("请选择文件");
                return;
            }
            if (First_content[4].StartsWith("C"))
            {
                MessageBox.Show("csv第四行开头应该不是以条码开头", "错误提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (!First_content[5].StartsWith("C"))
            {
                MessageBox.Show("csv第五行开头应该是以条码开头", "错误提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            #endregion
            #region~进度条20%
            UpdateProgresser(20);
            #endregion
            #region~将读取到的内容赋值给对象
            fcts = new MLB_FCT[First_content.Length - 5];
            string[] temp = null;
            string[] total_SNs = new string[First_content.Length - 5];//存放总的条码。
            for (int i = 5; i < First_content.Length; i++)
            {
                try
                {
                    fcts[i - 5] = new MLB_FCT();
                    if (this.radioButton1.Checked)
                        temp = First_content[i].Split(',');
                    else
                        temp = First_content[i].Split(';');
                    //当SN为空时过滤掉
                    if (temp[0] == "" || temp == null)
                        continue;
                    fcts[i - 5].SerialNumber = temp[0];
                    fcts[i - 5].Test_Pass_Fail_Status = temp[1];
                    //fcts[i - 5].List_Of_Fail_items = temp[2];
                    fcts[i - 5].Error_Description = temp[3];
                    total_SNs[i - 5] = temp[0];
                    fcts[i - 5].LineID = temp[7];
                    // fcts[i-5].....
                }
                catch (Exception ee)
                {
                    MessageBox.Show(ee.Message);
                    return;
                }
            }
            #endregion
            #region~进度条40%
            UpdateProgresser(40);
            #endregion
            /*var pass_item = from i in fcts where i.Test_Pass_Fail_Status == "PASS" select i;
            int pass_count = pass_item.Count();
            int fail_count = fcts.Length - pass_count;
             */
            #region~分析出Reset的产品：
            //取出FAIL的对象的条码
            var Fail_SN_fcts = from i in fcts where i.Test_Pass_Fail_Status == "FAIL" select i.SerialNumber;
            string[] Distinct_Fail_SNs = Fail_SN_fcts.Distinct().ToArray();//去除Fail条码中的重复（有的条码不止Fail一次）

            int retest_count = 0;//记录有fail有pass的条码数量

            List<MLB_FCT> pass_fail_instance = new List<MLB_FCT>();//储存有fail有pass的产品(状态是PASS的)；
            List<MLB_FCT> _pass_fail_instance = new List<MLB_FCT>();//储存有fail有pass的产品(状态是Fail的,同一条码多次Fail只记录一次)
            //遍历数组，寻找Fail的条码是否有pass的。
            for (int i = 0; i < Distinct_Fail_SNs.Length; i++)
            {
                var result = from j in fcts where j.SerialNumber == Distinct_Fail_SNs[i] && j.Test_Pass_Fail_Status == "PASS" select j;
                if (result.Count() == 1)
                {
                    //MLB_FCT[] _result = result.ToArray();
                    pass_fail_instance.Add(result.ToArray()[0]);
                    retest_count += 1;
                }

            }
            #endregion
            #region~进度条80%
            UpdateProgresser(80);
            #endregion
            //坏品统计
            int fail_count = Distinct_Fail_SNs.Length - retest_count;
            //总的条码去重之后的数量减去重测之后也是fail的数量。
            int avalable_count = total_SNs.Distinct().Count() - fail_count;
            #region~显示
            resultBox1.ShowMessage(Color.Black, "====================================Summary=====================================", true);
            resultBox1.ShowMessage(Color.Black, "重测的产品数量为：" + retest_count.ToString(), true);
            resultBox1.ShowMessage(Color.Black, "坏的产品为：" + fail_count.ToString(), true);
            resultBox1.ShowMessage(Color.Black, "总的良品为：" + avalable_count.ToString(), true);
            resultBox1.ShowMessage(Color.Black,"重测率为：" + ((double)((double)retest_count / (double)avalable_count)*100).ToString()+"%",true);
            resultBox1.ShowMessage(Color.Black, "详细如下：", true);

            logfile.resetProductsCount = retest_count;
            logfile.badProductsCount = fail_count;
            logfile.goodProductsCount = avalable_count;
            logfile.resetRate = (double)((double)retest_count / (double)avalable_count) * 100;
            resultBox1.ShowMessage(Color.Black, "--------------------------------1---------------------------", true);
            
            //richTextBox1.AppendText("====================================Summary=====================================\r\n");
            //richTextBox1.AppendText("重测的产品数量为：" + retest_count.ToString() + "\r\n");
            //richTextBox1.AppendText("坏的产品为：" + fail_count.ToString() + "\r\n");
            //richTextBox1.AppendText("总的良品为：" + avalable_count.ToString() + "\r\n");
            //richTextBox1.AppendText("重测率为：" + ((double)((double)retest_count / (double)avalable_count)).ToString() + "\r\n");
            //richTextBox1.AppendText("详细如下：\r\n");
            //richTextBox1.AppendText("--------------------------------1---------------------------\r\n");
            logfile.detailmessage1 = new LogFile.DetailMessage1[pass_fail_instance.Count];
            for (int i = 0; i < pass_fail_instance.Count; i++)
            {
                string ErrorMessage = "";
                var temp1 = from j in fcts where j.SerialNumber == pass_fail_instance[i].SerialNumber && j.Test_Pass_Fail_Status == "FAIL" select j;
                //var temp2=temp1.ToArray();
                //for (int k = 0; k < temp1.Count(); k++)
                //{
                //    ErrorMessage += temp2[k].Error_Description.Split(';')[0] + ";";
                //}
                //只显示第一次Fail的错误item
                var temp2 = temp1.ToArray()[0];
                _pass_fail_instance.Add(temp2);
                ErrorMessage = temp2.Error_Description.Split(';')[0];
                resultBox1.ShowMessage(Color.Red, pass_fail_instance.ToArray()[i].SerialNumber + ":", false);
                resultBox1.ShowMessage(Color.Blue, ErrorMessage, true);

                logfile.detailmessage1[i] = new LogFile.DetailMessage1();
                logfile.detailmessage1[i].SerialNumber = pass_fail_instance.ToArray()[i].SerialNumber;
                logfile.detailmessage1[i].ErrorDestription = ErrorMessage;
                //richTextBox1.SelectionColor = Color.Red;
                //richTextBox1.AppendText(pass_fail_instance.ToArray()[i].SerialNumber + ":");
                //richTextBox1.SelectionColor = Color.Blue;
                //richTextBox1.AppendText(ErrorMessage + "\r\n");//显示错误信息。
            }
            resultBox1.ShowMessage(Color.Black, "--------------------------------2---------------------------", true);
            //richTextBox1.AppendText("--------------------------------2---------------------------\r\n");
            List<string> LineNo = new List<string>();//存储reset产品的所有线别名称（包含重复）
            string[] Distinct_LineNo;//存储reset产品的所有线别名称（不重复）
            List<string> error_discription;//存放某一台机的Error Discription
            string[] Distinct_error_discription;//存放某一台机不重复的ErrorDiscription
            string serialnumbers;
            foreach (MLB_FCT i in _pass_fail_instance)
            {
                LineNo.Add(i.LineID);
            }
            Distinct_LineNo = LineNo.Distinct().ToArray();
            logfile.detailmessage2 = new LogFile.DetailMessage2[Distinct_LineNo.Length];
            for (int i = 0; i < Distinct_LineNo.Length; i++)
            {
                var items = from j in _pass_fail_instance where j.LineID == Distinct_LineNo[i] select j;
                var _items = items.ToArray();//在reset的产品中，都为一台机的测试产品。
                error_discription = new List<string>();
                resultBox1.ShowMessage(Color.Blue, Distinct_LineNo[i] + ":", false);
                logfile.detailmessage2[i] = new LogFile.DetailMessage2();
                logfile.detailmessage2[i].LineNo = Distinct_LineNo[i];
                
                //richTextBox1.SelectionColor = Color.Pink;
                //richTextBox1.AppendText(Distinct_LineNo[i] + ":");
                //richTextBox1.SelectionColor = Color.Black;
                for (int k = 0; k < _items.Length; k++)
                {
                    error_discription.Add(_items[k].Error_Description.Split(';')[0]);

                    
                    //if (k == 0)
                    //    richTextBox1.AppendText(_items[0].SerialNumber + "  " + _items[0].Error_Description.Split(';')[0] + "\r\n");
                    //else
                    //    richTextBox1.AppendText("          " + _items[k].SerialNumber + "  " + _items[k].Error_Description.Split(';')[0] + "\r\n");
                }
                Distinct_error_discription = error_discription.Distinct().ToArray();
                int resetItem_count = 0;
                
                logfile.detailmessage2[i].detail_message2_message = new LogFile.DetailMessage2_message[Distinct_error_discription.Length];
                
                for (int l = 0; l < Distinct_error_discription.Length; l++)
                {
                    var temp_item = from j in _pass_fail_instance where j.Error_Description.Split(';')[0] == Distinct_error_discription[l] && j.LineID == Distinct_LineNo[i] select j.SerialNumber;
                    var _temp_item = temp_item.ToArray();
                    serialnumbers = null;
                    foreach (string s in _temp_item)
                    {
                        serialnumbers += s + ";";
                    }
                    resetItem_count = _temp_item.Length;
                    
                    resultBox1.ShowMessage(Color.Black, Distinct_error_discription[l] + ":" + serialnumbers + ":" + resetItem_count, true);
                    logfile.detailmessage2[i].detail_message2_message[l] = new LogFile.DetailMessage2_message();
                    logfile.detailmessage2[i].detail_message2_message[l].ErrorDestription = Distinct_error_discription[l];
                    logfile.detailmessage2[i].detail_message2_message[l].SerialNumbers = serialnumbers;
                    logfile.detailmessage2[i].detail_message2_message[l].retestItem_count = resetItem_count;
                   
                }
                resultBox1.ShowMessage(Color.Black, "\r\n", false);
                //Thread.Sleep(1000);
                int endTime = System.Environment.TickCount;
                int escapeTime = endTime - startTime;
                this.updateLable("本次分析用时" + escapeTime.ToString() + "ms");
                //this.label2.Text = "本次分析用时" + escapeTime.ToString() + "ms";
            }
            #endregion
            #region~进度条100%
             UpdateProgresser(100);
            #endregion
 
        }
        
        private delegate void Button_status(Button button,bool enable);
        public void button_status_change(Button button, bool enable) 
          {
              if (button.InvokeRequired)
                  button.Invoke(new Button_status(button_status_change), button,enable);
              else button.Enabled = enable;
          }
        /// <summary>
        /// 之后的逻辑
        /// </summary>
        void thread_AnalysezeData_newLogic()
        {
            int startTime = System.Environment.TickCount;
            this.updateLable("正在分析...");
            button_status_change(this.button_result, false);


            #region~判断Log格式是否正确
            if (First_content == null)
            {
                MessageBox.Show("请选择文件");
                button_status_change(this.button_result, true);
                updateLable("分析失败！");
                return;
            }
            if (First_content[4].StartsWith("C"))
            {
                MessageBox.Show("csv第四行开头应该不是以条码开头", "错误提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
                button_status_change(this.button_result, true);
                updateLable("分析失败！");
                return;
            }
            if (!First_content[5].StartsWith("C"))
            {
                MessageBox.Show("csv第五行开头应该是以条码开头", "错误提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
                button_status_change(this.button_result, true);
                updateLable("分析失败！");
                return;
            }
            #endregion
            #region~进度条20%
            UpdateProgresser(20);
            #endregion
            #region~将读取到的内容赋值给对象
            string[] temp=null;
            string[] total_SNs = null;//存放总的条码。
            //fcts = new MLB_FCT[First_content.Length - 5];
            MLB_FCT temp_fct = null;
            for (int i = 5; i < First_content.Length; i++)
            {
                try
                {
                    
                    if (this.radioButton1.Checked)
                        temp = First_content[i].Split(',');
                    else
                        temp = First_content[i].Split(';');
                    //当SN为空时过滤掉
                    if (temp[0] == "" || temp[0] == null||!temp[0].Trim().StartsWith("C"))
                        continue;
                    temp_fct = new MLB_FCT();
                    temp_fct.SerialNumber = temp[0].Trim();
                    temp_fct.Test_Pass_Fail_Status = temp[1];
                    temp_fct.Error_Description = temp[3];
                    temp_fct.LineID = temp[7];
                    list_fcts.Add(temp_fct);


/*
                    fcts[i - 5] = new MLB_FCT();
                    if (this.radioButton1.Checked)
                        temp = First_content[i].Split(',');
                    else
                        temp = First_content[i].Split(';');
                    //当SN为空时过滤掉
                    if (temp[0] == "" || temp == null)
                        continue;
                    fcts[i - 5].SerialNumber = temp[0];
                    fcts[i - 5].Test_Pass_Fail_Status = temp[1];
                    //fcts[i - 5].List_Of_Fail_items = temp[2];
                    fcts[i - 5].Error_Description = temp[3];
                    total_SNs[i - 5] = temp[0];
                    fcts[i - 5].LineID = temp[7];
                    // fcts[i-5].....*/
                }
                catch (Exception ee)
                {
                    MessageBox.Show(ee.Message);
                    button_status_change(this.button_result, true);
                    updateLable("分析失败！");
                    return;
                }

            }
            fcts = list_fcts.ToArray();
            #endregion
            #region~SN_Total
            var _total_SN = from i in fcts select i.SerialNumber;
            total_SNs = _total_SN.ToArray();
            logfile.SN_Total = total_SNs.Distinct().Count();//去重后的SN个数。
            string[] temp_string;
            //string[] Distinct_temp_string;
            #endregion
            
            #region~SN PASS
            logfile.SN_Pass = analysize_SN_Pass(fcts);
            ////SN_PASS 
            ////1.找出全部的pass对象
            //var pass_instances = from i in fcts where i.Test_Pass_Fail_Status == "PASS" select i;
            ////2.将全部的pass对象的条码放入数组；
            //temp_string = (from i in pass_instances select i.SerialNumber).ToArray();
            ////3.数组去除重复
            //Distinct_temp_string = temp_string.Distinct().ToArray();
            ////4.在fcts中判断和每一个Distinct_temp_string中相同条码的状态是否存在FAIL,若不存在，且条码只出现一次，则为1st pass。
            //foreach (string i in Distinct_temp_string)
            //{
            //    string SNS = "";
            //    var unique_pass_instance = from j in fcts where j.SerialNumber == i select j;
            //    foreach (MLB_FCT k in unique_pass_instance)
            //    {
            //        SNS += k.Test_Pass_Fail_Status;
            //    }
            //    if (!SNS.Contains("FAIL")&&SNS.Contains("PASS"))
            //    {
                   
            //            logfile.SN_Pass.Add(unique_pass_instance.ToArray()[0]);
                  
            //    }
            //}
        #endregion
            #region~进度条40%
            UpdateProgresser(40);
            #endregion
          #region~SN_Retest
            logfile.SN_Retest = analysize_SN_Retest(fcts);
            //1.找出全部Fail的对象
           // var fail_instances = from i in fcts where i.Test_Pass_Fail_Status == "FAIL" select i;
           // //2.将全部FAIL的对象的条码放入数组；
           // temp_string = (from i in fail_instances select i.SerialNumber).ToArray();
           // //3.将数组去重
           // Distinct_temp_string = temp_string.Distinct().ToArray();
           //// List<MLB_FCT> retestProducts_fail = new List<MLB_FCT>();//记录retest的SN，且第一次Fail的。
           // //2.在fcts中判断和每一个fail的对象相同的条码的状态是否存在pass，若存在，则为retest
           // foreach (string i in Distinct_temp_string)
           // {
           //     string SNS = "";
           //     var retest_instances = from j in fcts where j.SerialNumber == i select j;
           //     foreach (MLB_FCT k in retest_instances)
           //     {
           //         SNS += k.Test_Pass_Fail_Status;
           //     }
           //     if (SNS.Contains("PASS") && SNS.Contains("FAIL"))
           //     {
           //         var retest_instance_fail = from k in retest_instances where k.Test_Pass_Fail_Status == "FAIL" select k;
           //         //retestProducts_fail.Add(retest_instance_fail.ToArray()[0]);
           //         logfile.SN_Retest.Add(retest_instance_fail.ToArray()[0]);//记录第一个FAIL的retest产品
           //     }
                
           // }
          #endregion
            #region~进度条80%
            UpdateProgresser(80);
            #endregion
            #region~SN_FAIL
            //SN FAIL
            logfile.SN_Fail = analysize_SN_Fail(fcts);
            //1.找出全部Fail的对象 ,取出条码，去除重复得到Distinct_temp_string
            //2.遍历判断
            //foreach (string i in Distinct_temp_string)
            //{
            //    string SNS = "";
            //    var retest_instances = from j in fcts where j.SerialNumber == i select j;
            //    foreach (MLB_FCT k in retest_instances)
            //    {
            //        SNS += k.Test_Pass_Fail_Status;
            //    }
            //    if (!SNS.Contains("PASS") && SNS.Contains("FAIL"))
            //    {
            //       // var retest_instance = from k in retest_instances where k.Test_Pass_Fail_Status == "FAIL" select k;
            //        logfile.SN_Fail.Add(retest_instances.ToArray()[0]);//随意捞的一个FAIL。
            //    }
            //}
            #endregion
            #region~显示：
            
            resultBox1.ShowMessage(Color.Black, "====================================Summary=====================================", true);
            resultBox1.ShowMessage(Color.Black, "Total Products:" + logfile.SN_Total, true);
            resultBox1.ShowMessage(Color.Black, "1st Pass Products:" + logfile.SN_Pass.Count, true);
            resultBox1.ShowMessage(Color.Black, "Retest Products:" + logfile.SN_Retest.Count, true);
            resultBox1.ShowMessage(Color.Black, "Bad Products:" + logfile.SN_Fail.Count, true);
            resultBox1.ShowMessage(Color.Black, string.Format("FPY:{0}%", logfile.FPY), true);
            resultBox1.ShowMessage(Color.Black, string.Format("RetestRate:{0}%", logfile.RetestRate), true);
            resultBox1.ShowMessage(Color.Black, string.Format("FailRate:{0}%", logfile.FailRate), true);
            resultBox1.ShowMessage(Color.Red, "更多详细分析信息，点击保存结果，在所选文件的目录查看Summary.xls!", true);

            int endTime = System.Environment.TickCount;
            button_status_change(this.button_result, true);
            analysize = true;
            this.updateLable("本次分析用时" + (endTime - startTime).ToString() + "ms");
            #endregion
            #region~进度条100%
            UpdateProgresser(100);
            #endregion


        }
        void Initial()//初始化
        {
            contents = new List<string>();
            path = null;
            First_content = null;
            AllFile_contents = new string[FileNames.Length][];
            list_fcts = new List<MLB_FCT>();
            

        }
        private void button_result_Click(object sender, EventArgs e)
        {
            
            if (this.richTextBox2.Text == "" || this.richTextBox2.Text == null)
            {
                MessageBox.Show("请载入文件！");
                return;
            }
            FileNames = this.richTextBox2.Text.Split('\n');
            Initial();
            #region~获取路劲
            string[] s = FileNames[0].Split('\\');
            for (int i = 0; i < s.Length; i++)
            {
                if (i == s.Length - 1)
                    continue;
                else
                    this.path += s[i] + "\\";
            }
           // MessageBox.Show(path);
            #endregion
            if (!this.GetAndWriteContent())
            {
                
                return;
            }
            Thread.Sleep(1);
            //new Thread(new ThreadStart(thread_AnalysizeData)).Start();
            new Thread(new ThreadStart(thread_AnalysezeData_newLogic)).Start();
            /*1.条数按照条码去除重复后avalable_count
             * 2.去除重复的条码：
             *   将所有的SN放入一个数组
             *   去重后得到count
             * 3.得出全部为fail的SN,去重，遍历，得出既有pass也有fail的产品pass_fail_instance。
             * 4.重测率=retest_count/avalable_count
             * 5.坏的产品为fail_count
             */
        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F5)
            {
                new Instructions().Show();
                
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {

        }

        private void button2_Click_1(object sender, EventArgs e)
        {

           // new Thread(() => { logfile.SaveContent(this, path); }).Start();
            new Thread(() => { logfile.SaveContent_newlogic(this, path); }).Start();
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            this.Dispose();
            logfile = null;
        }
        public List<MLB_FCT> analysize_SN_Pass(MLB_FCT[] source)
        {
            //SN_PASS 
            //1.找出全部的pass对象
            string[] temp_string;
            string[] Distinct_temp_string;
            List<MLB_FCT> list=new List<MLB_FCT>();
            var pass_instances = from i in source where i.Test_Pass_Fail_Status == "PASS" select i;
            //2.将全部的pass对象的条码放入数组；
            temp_string = (from i in pass_instances select i.SerialNumber).ToArray();
            //3.数组去除重复
            Distinct_temp_string = temp_string.Distinct().ToArray();
            //4.在fcts中判断和每一个Distinct_temp_string中相同条码的状态是否存在FAIL,若不存在，且条码只出现一次，则为1st pass。
            foreach (string i in Distinct_temp_string)
            {
                string SNS = "";
                var unique_pass_instance = from j in source where j.SerialNumber == i select j;
                foreach (MLB_FCT k in unique_pass_instance)
                {
                    SNS += k.Test_Pass_Fail_Status;
                }
                if (!SNS.Contains("FAIL") && SNS.Contains("PASS"))
                {

                    list.Add(unique_pass_instance.ToArray()[0]);

                }
            }
            return list;
        }
        public List<MLB_FCT> analysize_SN_Retest(MLB_FCT[] source)
        {
            string[] temp_string;
            string[] Distinct_temp_string;
            List<MLB_FCT> list = new List<MLB_FCT>();
            var fail_instances = from i in source where i.Test_Pass_Fail_Status == "FAIL" select i;
            //2.将全部FAIL的对象的条码放入数组；
            temp_string = (from i in fail_instances select i.SerialNumber).ToArray();
            //3.将数组去重
            Distinct_temp_string = temp_string.Distinct().ToArray();
            // List<MLB_FCT> retestProducts_fail = new List<MLB_FCT>();//记录retest的SN，且第一次Fail的。
            //2.在fcts中判断和每一个fail的对象相同的条码的状态是否存在pass，若存在，则为retest
            foreach (string i in Distinct_temp_string)
            {
                string SNS = "";
                var retest_instances = from j in source where j.SerialNumber == i select j;
                foreach (MLB_FCT k in retest_instances)
                {
                    SNS += k.Test_Pass_Fail_Status;
                }
                if (SNS.Contains("PASS") && SNS.Contains("FAIL"))
                {
                    var retest_instance_fail = from k in retest_instances where k.Test_Pass_Fail_Status == "FAIL" select k;
                    //retestProducts_fail.Add(retest_instance_fail.ToArray()[0]);
                    list.Add(retest_instance_fail.ToArray()[0]);//记录第一个FAIL的retest产品
                }

            }
            return list;
        }
        public List<MLB_FCT> analysize_SN_Fail(MLB_FCT[] source)
        {
            string[] temp_string;
            string[] Distinct_temp_string;
            List<MLB_FCT> list = new List<MLB_FCT>();
            var fail_instances = from i in source where i.Test_Pass_Fail_Status == "FAIL" select i;
            //2.将全部FAIL的对象的条码放入数组；
            temp_string = (from i in fail_instances select i.SerialNumber).ToArray();
            //3.将数组去重
            Distinct_temp_string = temp_string.Distinct().ToArray();
            foreach (string i in Distinct_temp_string)
            {
                string SNS = "";
                var retest_instances = from j in source where j.SerialNumber == i select j;
                foreach (MLB_FCT k in retest_instances)
                {
                    SNS += k.Test_Pass_Fail_Status;
                }
                if (!SNS.Contains("PASS") && SNS.Contains("FAIL"))
                {
                    // var retest_instance = from k in retest_instances where k.Test_Pass_Fail_Status == "FAIL" select k;
                    list.Add(retest_instances.ToArray()[0]);//随意捞的一个FAIL。
                }
            }
            return list;
        }

        private void Form1_DragDrop(object sender, DragEventArgs e)
        {
            Array paths = (System.Array)e.Data.GetData(DataFormats.FileDrop);
            for (int i = 0; i < paths.Length; i++)
            {
                this.richTextBox2.AppendText(paths.GetValue(i).ToString()+"\r\n");
                if(i==0)
                CheckLog(paths.GetValue(i).ToString());
                this.Refresh();
            }

        }

        private void Form1_DragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                e.Effect = DragDropEffects.Link;
            }
            else
                e.Effect = DragDropEffects.None;
        }

        private void clearToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.richTextBox2.Clear();
            FileNames = null;
        }

        private void resultBox1_Load(object sender, EventArgs e)
        {

        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            new Instructions().ShowDialog();
        }
    }
}
