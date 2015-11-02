using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace 分段读取文件并显示进度
{
    /* class Class1
     {
     using System;
 using System.Collections.Generic;
 using System.ComponentModel;
 using System.Data;
 using System.Drawing;
 using System.Linq;
 using System.Text;
 using System.Threading.Tasks;
 using System.Windows.Forms;
 using System.IO;
 using System.Threading;
 namespace CopyProgress015
 {
 public partial class Form1 : Form
 {
     public Form1()
     {
  InitializeComponent();
     }
     private Thread thdCopyFile; //创建一个线程
     private string str = "";  //用来记录源文件的名字
     FileStream FormerOpenStream;  //实例化源文件FileStream类
     FileStream ToFileOpenStream;  //实例化目标文件FileStream类

     #region //复制文件 函数
     /// <summary>
     /// 复制文件
     /// </summary>
     /// <param name="FormerFile">源文件路径</param>
     /// <param name="ToFile">目的文件路径</param>
     /// <param name="TranSize">传输大小</param>
     /// <param name="progressBar1">ProgressBar控件</param>
     public void CopyFile(string FormerFile, string ToFile, int TranSize, ProgressBar progressBar1)
     {
  progressBar1.Value = 0;//设置进度条的当前位置为0
  progressBar1.Minimum = 0; //设置进度条的最小值为0
  try
  {
      FormerOpenStream = new FileStream(FormerFile, FileMode.Open, FileAccess.Read);//以只读方式打开源文件
  }
  catch (IOException ex)
  {
      MessageBox.Show(ex.Message);
      return;
  }
  try
  {
      FileStream fileToCreate = new FileStream(ToFile, FileMode.Create); //创建目的文件，如果已存在将被覆盖
      fileToCreate.Close();//关闭所有fileToCreate的资源
      fileToCreate.Dispose();//释放所有fileToCreate的资源
  }
  catch(IOException ex)
  {
      MessageBox.Show(ex.Message);
      return;
  }

        ToFileOpenStream = new FileStream(ToFile, FileMode.Append, FileAccess.Write);//以写方式打开目的文件

        int max = Convert.ToInt32(Math.Ceiling((Double)FormerOpenStream.Length / (Double)TranSize));//根据一次传输的大小，计算最大传输个数. Math.Ceiling 方法 (Double),返回大于或等于指定的双精度浮点数的最小整数值。
  progressBar1.Maximum = max;//设置进度条的最大值
  int FileSize; //每次要拷贝的文件的大小
  if (TranSize < FormerOpenStream.Length)  //如果分段拷贝，即每次拷贝内容小于文件总长度
  {
      byte[] buffer = new byte[TranSize]; //根据传输的大小，定义一个字节数组，用来存储传输的字节
      int copied = 0;//记录传输的大小
      int tem_n = 1;//设置进度栏中进度的增加个数
      while (copied <= ((int)FormerOpenStream.Length - TranSize))
      {
   FileSize = FormerOpenStream.Read(buffer, 0, TranSize);//从0开始读到buffer字节数组中，每次最大读TranSize
   FormerOpenStream.Flush();   //清空缓存
   ToFileOpenStream.Write(buffer, 0, TranSize); //向目的文件写入字节
   ToFileOpenStream.Flush();//清空缓存
   ToFileOpenStream.Position = FormerOpenStream.Position; //是源文件的目的文件流的位置相同
   copied += FileSize; //记录已经拷贝的大小
   progressBar1.Value = progressBar1.Value + tem_n; //增加进度栏的进度块
      }
      int leftSize = (int)FormerOpenStream.Length - copied; //获取剩余文件的大小
      FileSize = FormerOpenStream.Read(buffer, 0, leftSize); //读取剩余的字节
      FormerOpenStream.Flush();
      ToFileOpenStream.Write(buffer, 0, leftSize); //写入剩余的部分
      ToFileOpenStream.Flush();
  }
  else //如果整体拷贝，即每次拷贝内容大于文件总长度
  {
      byte[] buffer = new byte[FormerOpenStream.Length];
      FormerOpenStream.Read(buffer, 0, (int)FormerOpenStream.Length);
      FormerOpenStream.Flush();
      ToFileOpenStream.Write(buffer, 0, (int)FormerOpenStream.Length);
      ToFileOpenStream.Flush();
  }
  FormerOpenStream.Close();
  ToFileOpenStream.Close();
  if (MessageBox.Show("copy finished") == DialogResult.OK)
  {
      progressBar1.Value = 0;
      txtOriginalFile.Clear();
      txtCopyFile.Clear();
      str = "";
  }
     }
     #endregion
 
     public delegate void CopyFile_Delegate(); //定义委托/托管线程
     /// <summary>
     /// 在线程上执行委托（设置托管线程函数）
     /// </summary>
     public void SetCopyFile()
     {
  //this.Invoke(new CopyFile_Delegate(RunCopyFile)); //对指定的线程进行托管
  //下面两行代码等同上面一行代码
  CopyFile_Delegate copyfile_delegate = new CopyFile_Delegate(RunCopyFile); //创建delegate对象
  this.Invoke(copyfile_delegate); //调用delegate            
     }

     /// <summary>
     /// 设置线程，运行copy文件，它与代理CopyFile_Delegate应具有相同的参数和返回类型
     /// </summary>
     public void RunCopyFile()
     {
  CopyFile(txtOriginalFile.Text, txtCopyFile.Text + "\\" + str, 1024, progressBar1); //复制文件
  Thread.Sleep(0); //避免假死 
  thdCopyFile.Abort();  //关闭线程
     }

     private void btnOriginalFile_Click(object sender, EventArgs e)
     {
  if (openFileDialog1.ShowDialog() == DialogResult.OK)  //打开文件对话框
  {
      txtOriginalFile.Text = openFileDialog1.FileName;  //获取源文件的路径
  }
     }
     private void btnCopyFile_Click(object sender, EventArgs e)
     {
  if (folderBrowserDialog1.ShowDialog() == DialogResult.OK)
  {
      txtCopyFile.Text = folderBrowserDialog1.SelectedPath;//获取目的文件的路径               
  }
     }

     private void btnBeginToCopy_Click(object sender, EventArgs e)
     {                    

  if (txtOriginalFile.Text.Trim() == string.Empty)
  {
      MessageBox.Show("OriginalFile cannot be empty!");
      return;

  }
  else
  {
      str = txtOriginalFile.Text;//记录源文件的路径
      str = str.Substring(str.LastIndexOf('\\') + 1, str.Length - str.LastIndexOf('\\') - 1); //获取源文件的名称
  }

  if (txtCopyFile.Text.Trim() == string.Empty)
  {
      MessageBox.Show("The Copyfile path cannot be empty!");
      return;
  }
  else
  {
      thdCopyFile = new Thread(new ThreadStart(SetCopyFile));
      thdCopyFile.Start();
  }             

     }

     /// <summary>
     /// 给textbox增加tooltip
     /// </summary>
     /// <param name="sender"></param>
     /// <param name="e"></param>
     private void txtOriginalFile_MouseHover(object sender, EventArgs e)
     {
  ToolTip tooltip = new ToolTip();
  if (txtOriginalFile.Text.Trim() != string.Empty)
  {

      tooltip.Show(txtOriginalFile.Text, txtOriginalFile);
  }
  else
  {
      tooltip.Hide(txtOriginalFile);
  }

     }
     /// <summary>
     /// 给textbox增加tooltip
     /// </summary>
     /// <param name="sender"></param>
     /// <param name="e"></param>
     private void txtCopyFile_MouseHover(object sender, EventArgs e)
     {
  ToolTip tooltip = new ToolTip();
  if (txtCopyFile.Text.Trim() != string.Empty)
  {

      tooltip.Show(txtCopyFile.Text, txtCopyFile);
  }
  else
  {
      tooltip.Hide(txtCopyFile); 
  }
     }
 }
 }
     }
     * */
}
