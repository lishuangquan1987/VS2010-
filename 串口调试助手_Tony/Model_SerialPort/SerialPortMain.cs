﻿using System;
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
           // this.DataReceived += new SerialDataReceivedEventHandler(SerialPortMain_DataReceived);
            this.ReceivedBytesThreshold = 1;
            this.Encoding = Encoding.Default;
            this.DtrEnable = true;
            this.RtsEnable = true;
        }
        //StringBuilder ReveiveString = new StringBuilder();
        void SerialPortMain_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            SerialPort s = (SerialPort)sender;
            //while (s.BytesToRead != 0)
            //{
            //    ReveiveString.Append(s.ReadLine());
            //}
            while (s.BytesToRead > 0)
            {
               
                byte[] bytes = new byte[s.BytesToRead];
                s.Read(bytes, 0, bytes.Length);
                //ReveiveString.Append(Encoding.Default.GetString(bytes,0,bytes.Length));
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
            string result = "";
            //超时不回复2000ms
            for (int i = 0; i < 2000; i++)
            {
                if (this.BytesToRead == 0)
                    System.Threading.Thread.Sleep(1);
                else
                    break;
            }
            if (this.BytesToRead == 0)
            {
                Show_MSG("超时2s未回复", true, Color.Red);
                return null;
            }
            while (this.BytesToRead != 0)
            {
                
                System.Threading.Thread.Sleep(10);
                result += this.ReadExisting();
            }
            return result;
        }
    }
}
