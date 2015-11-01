using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Windows.Forms;
using NPOI.HSSF.UserModel;
using NPOI.SS.UserModel;
using NPOI.SS.Util;

namespace Log_Analysizer
{
   public class LogFile
    {
          /// <summary>
          /// 用来存储Log的类。
          /// </summary>
           public int resetProductsCount;
           public int badProductsCount;
           public int goodProductsCount;
           
       /// <summary>
       /// 构造函数
       /// </summary>
           public LogFile()
           {
               SN_Pass = new List<MLB_FCT>();
               SN_Retest = new List<MLB_FCT>();
               SN_Fail = new List<MLB_FCT>();
           }

           public int SN_Total;
           public List<MLB_FCT> SN_Pass;// 1st pass
           public List<MLB_FCT> SN_Retest;//retest pass（记录第一次fail的对象）
           public List<MLB_FCT> SN_Fail;//fail
           public double FPY
           {
               get { return ((double)this.SN_Pass.Count / (double)this.SN_Total) * 100; }
           }
           public double RetestRate 
           {
               get { return ((double)this.SN_Retest.Count / (double)this.SN_Total) * 100; }
           }
           public double FailRate
           {
               get { return ((double)this.SN_Fail.Count / (double)this.SN_Total) * 100; }
           }



           public double resetRate;//已经乘以100.
           public DetailMessage1[] detailmessage1;
           public DetailMessage2[] detailmessage2;

           private ErrorDescriptionAndCount[] ED; 
           private string _path;
           private FileStream fs_write;
           private HSSFWorkbook workbook;
           private ISheet sheet;
           private ISheet sheet2;
           private ISheet sheet3;
           int usedRow_count = 0;
           public void SaveContent(Form1 form,string path)
           {
               //UpdateProgresser
               //updateLable
               _path = path + "\\Summary.xls";
               form.updateLable("正在保存...");
               #region~建立文件
               if (File.Exists(_path))
                   try
                   {
                       File.Delete(_path);
                   }
                   catch (Exception e)
                   {
                       form.updateLable("保存失败");
                       MessageBox.Show(e.Message);
                       return;
                   }
               fs_write = new FileStream(_path, FileMode.Create, FileAccess.Write);
               #endregion
               #region~创建工作表
               workbook = new HSSFWorkbook();
               sheet = workbook.CreateSheet("Summary");
               #endregion
               ///
               ///Summary
               ///
               form.UpdateProgresser(20);
               sheet.CreateRow(0).CreateCell(0).SetCellValue("====================================Summary=====================================");
               sheet.CreateRow(1).CreateCell(0).SetCellValue("重测的产品数量");
               sheet.GetRow(1).CreateCell(1).SetCellValue(this.resetProductsCount);
               sheet.CreateRow(2).CreateCell(0).SetCellValue("坏的产品数量");
               sheet.GetRow(2).CreateCell(1).SetCellValue(this.badProductsCount);
               sheet.CreateRow(3).CreateCell(0).SetCellValue("总的良品数量");
               sheet.GetRow(3).CreateCell(1).SetCellValue(this.goodProductsCount);
               sheet.CreateRow(4).CreateCell(0).SetCellValue("重测率");
               sheet.GetRow(4).CreateCell(1).SetCellValue(this.resetRate+"%");


               ///
               ///Detail
               ///
               form.UpdateProgresser(40);
               sheet2 = workbook.CreateSheet("Details");
               sheet2.CreateRow(0).CreateCell(0).SetCellValue("机台");
               sheet2.GetRow(0).CreateCell(1).SetCellValue("重测项目");
               sheet2.GetRow(0).CreateCell(2).SetCellValue("重测的产品");
               sheet2.GetRow(0).CreateCell(3).SetCellValue("不良产品数量");

               usedRow_count = 1;
               for (int i = 0; i < this.detailmessage2.Length; i++)
               {
                   
                   int Error_count = this.detailmessage2[i].detail_message2_message.Length;
                   sheet2.CreateRow(usedRow_count).CreateCell(0).SetCellValue(this.detailmessage2[i].LineNo);
                   sheet2.AddMergedRegion(new CellRangeAddress(usedRow_count, usedRow_count + Error_count-1, 0, 0));
                   

                   for (int j = usedRow_count; j < usedRow_count + Error_count; j++)
                   {
                       if(j==usedRow_count)
                       sheet2.GetRow(j).CreateCell(1).SetCellValue(this.detailmessage2[i].detail_message2_message[j - usedRow_count].ErrorDestription);
                       else
                       sheet2.CreateRow(j).CreateCell(1).SetCellValue(this.detailmessage2[i].detail_message2_message[j - usedRow_count].ErrorDestription);
                       sheet2.GetRow(j).CreateCell(2).SetCellValue(this.detailmessage2[i].detail_message2_message[j - usedRow_count].SerialNumbers);
                       sheet2.GetRow(j).CreateCell(3).SetCellValue(this.detailmessage2[i].detail_message2_message[j - usedRow_count].retestItem_count);
                   }
                   usedRow_count = usedRow_count + Error_count + 1;
               }
               form.UpdateProgresser(80);
               #region~保存Log
               try
               {
                   workbook.Write(fs_write);
                   fs_write.Close();
                   form.UpdateProgresser(100);
                   form.updateLable("保存成功！请前往所选文件目录查看Summary.xls文件");
               }
               catch (Exception e)
               {
                   MessageBox.Show(e.Message);
               }
               #endregion

           }
           public void SaveContent_newlogic(Form1 form, string path)
           {
               //UpdateProgresser
               //updateLable
               if(!form.analysize)
               {
                   MessageBox.Show("请先分析文件，然后再点击保存，谢谢！");
                   return;
               }
               form.button_status_change(form.button2, false);
               _path = path + "\\Summary.xls";
               form.updateLable("正在保存...");
               #region~建立文件
               if (File.Exists(_path))
                   try
                   {
                       File.Delete(_path);
                   }
                   catch (Exception e)
                   {
                       form.updateLable("保存失败");
                       MessageBox.Show(e.Message);
                       return;
                   }
               fs_write = new FileStream(_path, FileMode.Create, FileAccess.Write);
               #endregion
               #region~创建工作表
               workbook = new HSSFWorkbook();
               sheet = workbook.CreateSheet("Summary");
               #endregion

               #region~summary
               sheet.CreateRow(0).CreateCell(0).SetCellValue("====================================Summary=====================================");
               sheet.CreateRow(1).CreateCell(0).SetCellValue("Total Products");
               sheet.GetRow(1).CreateCell(1).SetCellValue(this.SN_Total);
               sheet.CreateRow(2).CreateCell(0).SetCellValue("1st Pass");
               sheet.GetRow(2).CreateCell(1).SetCellValue(this.SN_Pass.Count);
               sheet.CreateRow(3).CreateCell(0).SetCellValue("Retest Products");
               sheet.GetRow(3).CreateCell(1).SetCellValue(this.SN_Retest.Count);
               sheet.CreateRow(4).CreateCell(0).SetCellValue("Bad Products");
               sheet.GetRow(4).CreateCell(1).SetCellValue(this.SN_Fail.Count);
               sheet.CreateRow(5).CreateCell(0).SetCellValue("FPY");
               sheet.GetRow(5).CreateCell(1).SetCellValue(this.FPY+"%");
               sheet.CreateRow(6).CreateCell(0).SetCellValue("Retest Rate");
               sheet.GetRow(6).CreateCell(1).SetCellValue(this.RetestRate+"%");
               sheet.CreateRow(7).CreateCell(0).SetCellValue("Bad Rate");
               sheet.GetRow(7).CreateCell(1).SetCellValue(this.FailRate+"%");

               sheet.CreateRow(9).CreateCell(0).SetCellValue("Retest Item");
               sheet.GetRow(9).CreateCell(1).SetCellValue("Retest Count");
               #endregion
               #region~进度条10%
               form.UpdateProgresser(10);
               #endregion
               #region~分析retest产品的retest item，并列举出来
               var Errorlist = from i in this.SN_Retest select i.Error_Description.Split(';')[0];
               string[] _Errorlist = Errorlist.Distinct().ToArray();
               ED = new ErrorDescriptionAndCount[_Errorlist.Length];
               for (int i = 0; i < _Errorlist.Length; i++)
               {
                   ED[i] = new ErrorDescriptionAndCount();
                   var temp = from j in this.SN_Retest where j.Error_Description.Split(';')[0] == _Errorlist[i] select j;
                   ED[i].count = temp.Count();
                   ED[i].error = _Errorlist[i];
               }
               ED = ED.OrderByDescending(i => i.count).ToArray();
               int summary_usedRow = 10;
               foreach (ErrorDescriptionAndCount i in ED)
               {
                   sheet.CreateRow(summary_usedRow).CreateCell(0).SetCellValue(i.error);
                   sheet.GetRow(summary_usedRow).CreateCell(1).SetCellValue(i.count);
                   summary_usedRow++;
               }
               #endregion
               #region~进度条20%
               form.UpdateProgresser(20);
               #endregion
               #region~创建工作表2.
               sheet2 = workbook.CreateSheet("Summary By Single Station");
               #endregion
               #region~By机台列举每一台的重测情况。
               var line_Nos = from i in form.fcts select i.LineID;
               string[] Distinct_line_Nos = line_Nos.Distinct().ToArray();
               Single_Fct[] single_Fct = new Single_Fct[Distinct_line_Nos.Length];

               sheet2.CreateRow(0).CreateCell(0).SetCellValue("机台");
               sheet2.GetRow(0).CreateCell(1).SetCellValue("Total");
               sheet2.GetRow(0).CreateCell(2).SetCellValue("Pass");
               sheet2.GetRow(0).CreateCell(3).SetCellValue("Fail");
               sheet2.GetRow(0).CreateCell(4).SetCellValue("Retest");
               sheet2.GetRow(0).CreateCell(5).SetCellValue("FPY");
               sheet2.GetRow(0).CreateCell(6).SetCellValue("FailRate");
               sheet2.GetRow(0).CreateCell(7).SetCellValue("RetestRate");


               int used_Row_sheet2 = 1;
               
               for (int i = 0; i < Distinct_line_Nos.Length; i++)
               {
                   single_Fct[i] = new Single_Fct();
                   var Line_Detail = from j in form.fcts where j.LineID == Distinct_line_Nos[i] select j;
                   single_Fct[i].SN_Total = Line_Detail.Distinct().Count();
                   single_Fct[i].SN_Pass = form.analysize_SN_Pass(Line_Detail.ToArray());
                   single_Fct[i].SN_Retest = form.analysize_SN_Retest(Line_Detail.ToArray());
                   single_Fct[i].SN_Fail = form.analysize_SN_Fail(Line_Detail.ToArray());

                   sheet2.CreateRow(used_Row_sheet2).CreateCell(0).SetCellValue(Distinct_line_Nos[i]);
                   sheet2.GetRow(used_Row_sheet2).CreateCell(1).SetCellValue(single_Fct[i].SN_Total);
                   sheet2.GetRow(used_Row_sheet2).CreateCell(2).SetCellValue(single_Fct[i].SN_Pass.Count);
                   sheet2.GetRow(used_Row_sheet2).CreateCell(3).SetCellValue(single_Fct[i].SN_Fail.Count);
                   sheet2.GetRow(used_Row_sheet2).CreateCell(4).SetCellValue(single_Fct[i].SN_Retest.Count);
                   sheet2.GetRow(used_Row_sheet2).CreateCell(5).SetCellValue(single_Fct[i].FPY+"%");
                   sheet2.GetRow(used_Row_sheet2).CreateCell(6).SetCellValue(single_Fct[i].FailRate + "%");
                   sheet2.GetRow(used_Row_sheet2).CreateCell(7).SetCellValue(single_Fct[i].RetestRate + "%");
                   used_Row_sheet2++;
                   
                   
               }
               #endregion
               #region~进度条40%
               form.UpdateProgresser(40);
               #endregion

               #region~创建工作表3
               sheet3 = workbook.CreateSheet("Reset Detail By Single Station");
               sheet3.CreateRow(0).CreateCell(0).SetCellValue("机台");
               sheet3.GetRow(0).CreateCell(1).SetCellValue("重测项目");
               sheet3.GetRow(0).CreateCell(2).SetCellValue("重测的产品");
               sheet3.GetRow(0).CreateCell(3).SetCellValue("重测的数量");
               int used_Row_Sheet3 = 1;
               for (int i = 0; i < single_Fct.Length; i++)
               {

                   var error_descriptions = from k in single_Fct[i].SN_Retest select k.Error_Description.Split(';')[0];
                   string[] Distinct_error_descriptions = error_descriptions.Distinct().ToArray();
                   int merge_area = Distinct_error_descriptions.Length;
                   if (merge_area == 0)
                       merge_area = 1;//Desription为空的情况

                   sheet3.CreateRow(used_Row_Sheet3).CreateCell(0).SetCellValue(Distinct_line_Nos[i]);
                   sheet3.AddMergedRegion(new CellRangeAddress(used_Row_Sheet3, used_Row_Sheet3 + merge_area-1, 0, 0));
                   for (int j = used_Row_Sheet3; j < merge_area+used_Row_Sheet3; j++)
                   {
                       #region~对重测产品按照error归纳
                       var instances = from k in single_Fct[i].SN_Retest where k.Error_Description.Split(';')[0] == Distinct_error_descriptions[j - used_Row_Sheet3] select k;
                       string SNs = "";
                       foreach (MLB_FCT k in instances)
                       {
                           SNs += k.SerialNumber + ";";
                       }

                       #endregion

                       if (j == used_Row_Sheet3)
                           if(Distinct_error_descriptions==null||Distinct_error_descriptions.Length==0||Distinct_error_descriptions[0]==null)
                               sheet3.GetRow(j).CreateCell(1).SetCellValue("");
                          else
                               sheet3.GetRow(j).CreateCell(1).SetCellValue(Distinct_error_descriptions[j - used_Row_Sheet3]);
                       else
                           sheet3.CreateRow(j).CreateCell(1).SetCellValue(Distinct_error_descriptions[j - used_Row_Sheet3]);
                       sheet3.GetRow(j).CreateCell(2).SetCellValue(SNs);
                       sheet3.GetRow(j).CreateCell(3).SetCellValue(instances.Count());
                   }
                   used_Row_Sheet3=used_Row_Sheet3+merge_area+1;
               }
               #region~进度条80%
               form.UpdateProgresser(80);
               form.button_status_change(form.button2, true);
               #endregion
               #endregion
               try
               {
                   workbook.Write(fs_write);
                   fs_write.Close();
                   form.updateLable("保存成功，请前往选择目录下查看Summary.xls");
                   #region~进度条100%
                   form.UpdateProgresser(100);
                   #endregion
               }
               catch (Exception e)
               {
                   MessageBox.Show(e.Message);
                   fs_write.Close();
               }
           }


           
           #region~辅助类
           public class DetailMessage1
       {
           public string SerialNumber;
           public string ErrorDestription;
       }
       public class DetailMessage2
       {
           public string LineNo;
          
           public DetailMessage2_message[] detail_message2_message;
       }
       public class DetailMessage2_message
       {
           public string ErrorDestription;
           public string SerialNumbers;
           public int retestItem_count;
       }
       public class ErrorDescriptionAndCount
       {
          public string error;
          public int count;
       }
       public class Single_Fct
       {
          public string LineNo;
          public int SN_Total;
          public List<MLB_FCT> SN_Pass;
          public List<MLB_FCT> SN_Fail;
          public List<MLB_FCT> SN_Retest;
          public double FPY
          {
              get { return ((double)this.SN_Pass.Count / (double)this.SN_Total) * 100; }
          }
          public double RetestRate
          {
              get { return ((double)this.SN_Retest.Count / (double)this.SN_Total) * 100; }
          }
          public double FailRate
          {
              get { return ((double)this.SN_Fail.Count / (double)this.SN_Total) * 100; }
          }

       }
           #endregion
    }
}
