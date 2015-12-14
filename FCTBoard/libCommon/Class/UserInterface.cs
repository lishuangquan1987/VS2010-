using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using TestStudio.Automation.TestManager.libCommon.Interface;

namespace TestStudio.Automation.TestManager.libCommon.Class
{
    public delegate int delegateUiMessage(object sender, object arg);


    class UserInterface:IUserInterface
    {
        public virtual int OnTestStart(object sender, object arg)
        {
            return 0;
        }
        public virtual int OnTestStop(object sender, object arg)
        {
            return 0;
        }
        public virtual int OnTestPause(object sender, object arg)
        {
            return 0;
        }
        public virtual int OnTestResume(object sender, object arg)
        {
            return 0;
        }
        public virtual int OnItemStart(object sender, object arg)
        {
            return 0;
        }
        public virtual int OnItemFinish(object sender, object arg)
        {
            return 0;
        }
        public virtual int OnTestFinish(object sender, object arg)
        {
            return 0;
        }

        public virtual int CtrlFunction(int ctrlCode, object obj)
        {
            return 0;
        }
    }
}
