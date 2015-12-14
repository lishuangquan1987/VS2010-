using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Intelligent.Automation.iTMP.Common.Interface.Instruments;

namespace Intelligent.Automation.iTMP.Common.Class.Instruments
{
    public abstract class DaqDevice:IDaqDevice
    {
        public virtual double MeasureVoltage(int chn)
        {
            return 0;
        }
        public virtual void SetVoltage(int chn,double value)
        {
            return;
        }
        public virtual double MeasureResistance(int chn)
        {
            return 0;
        }
        public virtual double MeasureFrequency(int chn)
        {
            return 0;
        }
        public virtual double MeasurePeriod(int chn)
        {
            return 0;
        }
        public virtual void SetPort(int port, byte value)
        {
            
        }
        public virtual byte GetPort(int port)
        {
            return 0;
        }
   }
}
