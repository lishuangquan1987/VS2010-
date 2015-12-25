using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO.Ports;
using N_EventCenter;
using System.Threading;

namespace MyPort
{
    public class MySerialPort:SerialPort
    {
        private string receiveString = "";
        public MySerialPort(string portname)
        {
            
            this.PortName = portname;
            this.ReceivedBytesThreshold = 1;
            this.Encoding = Encoding.Default;
            this.DataReceived += MySerialPort_DataReceived;           
        }

        void MySerialPort_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {   
            int length = this.BytesToRead;
            byte[] bytes = new byte[length];
            this.Read(bytes, 0, bytes.Length);
            receiveString = Encoding.Default.GetString(bytes);            
        }
        public void Doconnect()
        {
 
        }
        public void WriteString(string msg)
        {
            this.DiscardInBuffer();
            this.DiscardOutBuffer();
            receiveString = "";
            this.WriteString(msg);
            while (this.BytesToWrite>0)
                Thread.Sleep(1);
        }
        public string ReadString()
        {
            while (this.BytesToRead > 0)
                Thread.Sleep(1);
            string r = receiveString;
            receiveString = "";
            return r;
        }

        private void InitializeComponent()
        {

        }
    }
}
