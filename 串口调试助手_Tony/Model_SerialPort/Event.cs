using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO.Ports;
using System.Drawing;

namespace Model_SerialPort
{
    public partial class SerialPortMain : SerialPort
    {
        public delegate void _Show(string msg, bool nextline, Color color);
        public event _Show Event_Show;
        private void Show_MSG(string msg, bool nextline, Color color)
        {
            if (this != null && Event_Show != null)
            {
                Event_Show(msg, nextline, color); 
            }
        }
    }
}
