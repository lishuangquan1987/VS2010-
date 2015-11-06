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
        
        public SerialPortMain()
        {
            this.DataReceived += new SerialDataReceivedEventHandler(SerialPortMain_DataReceived);
            this.ReceivedBytesThreshold = 1;
            this.DtrEnable = true;
            this.RtsEnable = true;
        }
        StringBuilder ReveiveString = new StringBuilder();
        void SerialPortMain_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            SerialPort s = (SerialPort)sender;

            if (s.BytesToRead > 0)
            {
                ReveiveString.Append(s.ReadExisting());
            }
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
            this.DiscardInBuffer();
            this.DiscardOutBuffer();
            ReveiveString = new StringBuilder();
            if (!this.IsOpen)
            {
                if (Port_Open() == -1)
                {
                    Show_MSG("串口打开失败：" + this.PortName, true, Color.Red);
                    return -1;
                }
            }
            try
            {
                this.Write(msg);
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
           
            while (this.BytesToRead != 0)
            {
               
                System.Threading.Thread.Sleep(10);
            }
            return ReveiveString.ToString();
        }
    }
}
