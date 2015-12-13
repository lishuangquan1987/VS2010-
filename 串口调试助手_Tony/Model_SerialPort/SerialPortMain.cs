using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO.Ports;
using System.Drawing;

namespace Model_SerialPort
{
    public partial class SerialPortMain:SerialPort
    {
        string result = "";
        public SerialPortMain()
        {
            this.DataReceived += new SerialDataReceivedEventHandler(SerialPortMain_DataReceived);
            this.ReceivedBytesThreshold = 1;
            this.Encoding = Encoding.Default;
            //this.DtrEnable = true;
            //this.RtsEnable = true;
        }
        //StringBuilder ReveiveString = new StringBuilder();
        void SerialPortMain_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            //result = "";
            SerialPort s = (SerialPort)sender;
            //while (s.BytesToRead != 0)
            //{
            //    ReveiveString.Append(s.ReadLine());
            //}
            List<byte> receiveBytes = new List<byte>();
            while (s.BytesToRead > 0)
            {
                System.Threading.Thread.Sleep(50);
                byte[] bytes = new byte[512];
                int readCount = s.Read(bytes, 0, bytes.Length);
                for (int i = 0; i < readCount; i++)
                {
                    receiveBytes.Add(bytes[i]);
                }
                
                //byte[] bytes = new byte[s.BytesToRead];
                //s.Read(bytes, 0, bytes.Length);
                //ReveiveString.Append(Encoding.Default.GetString(bytes,0,bytes.Length));
            }
            string ReceiveMSG = Encoding.UTF8.GetString(receiveBytes.ToArray());
            Show_MSG(DateTime.Now.ToString() + ":" + ReceiveMSG, true, Color.Green);
        }
        public int Port_Open()
        {
            if (!this.IsOpen)
            {
                try
                {
                    this.Open();
                }
                catch (Exception e)
                {
                    this.Show_MSG(e.Message, true, Color.Red);
                    return -1;
                }
            }
            return 0;
        }
        public int Port_Close()
        {
            if (this.IsOpen)
            {
                try
                {
                    this.Close();
                }
                catch (Exception e)
                {
                    this.Show_MSG(e.Message, true, Color.Red);
                    return -1;
                }
            }
            return 0;
        }
        
        public int WriteString(string msg)
        {
           
            //ReveiveString = new StringBuilder();
            if (!this.IsOpen)
            {
                if (Port_Open() == -1)
                {
                    Show_MSG("串口打开失败：" + this.PortName, true, Color.Red);
                    return -1;
                }
            }
            this.DiscardInBuffer();
            this.DiscardOutBuffer();
            result = "";
            try
            {
                this.Write(msg);
                result = "";
                while (this.BytesToWrite != 0)
                {
                    System.Threading.Thread.Sleep(1);
                }
            }
            catch (Exception e)
            {
                Show_MSG("写入字符串失败：" + this.PortName, true, Color.Red);
                return -1;
            }
            return 0;
        }
        
        public string ReadString()
        {
            
            //超时不回复2000ms
            for (int i = 0; i < 2000; i++)
            {
                if (this.BytesToRead == 0&&result=="")
                    System.Threading.Thread.Sleep(1);
                else
                    break;
            }
            if (this.BytesToRead == 0&&result=="")
            {
                Show_MSG("超时2s未回复", true, Color.Red);
                return null;
            }
            while (this.BytesToRead != 0)
            {                
                System.Threading.Thread.Sleep(10);             
            }
            return result;
        }
    }
}
