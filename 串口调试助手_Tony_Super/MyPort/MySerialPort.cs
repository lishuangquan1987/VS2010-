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
            byte[] bytes = new byte[this.BytesToRead];
            while (this.BytesToRead > 0)
            {
                this.Read(bytes, 0, bytes.Length);
                receiveString += Encoding.Default.GetString(bytes);
            }
                      
        }
        public void Doconnect()
        {
 
        }
        public void WriteString(string msg)
        {
            try
            {
                this.DiscardInBuffer();
                this.DiscardOutBuffer();
                receiveString = "";
                Thread.Sleep(10);
                this.Write(msg);
                while (this.BytesToWrite > 0)
                    Thread.Sleep(1);
            }
            catch (Exception e)
            {
                System.Windows.Forms.MessageBox.Show(e.Message);
            }
            
        }
        public string ReadString()
        {
            for (int i = 0; i < 100; i++)
            {
                if (this.BytesToRead > 0)
                    Thread.Sleep(2);
                if (receiveString == null || receiveString == "")
                    Thread.Sleep(2);
            }
            Thread.Sleep(20);
            return receiveString;
        }

        private void InitializeComponent()
        {

        }
    }
}
