using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

using System.Windows.Forms;

using TestStudio.Automation.TestManager.libCommon.Interface;

namespace TestStudio.Automation.TestManager.libCommon.Class
{
    public abstract class Engine:IEngineInterface
    {
        NotificationCenter nc = NotificationCenter.DefaultCenter();
        protected int m_ModuleCount = 0;
        Thread m_threadManager=null;

        public virtual int StartTest(object arg)
        {
            m_threadManager = new Thread(TestEntry);
            m_threadManager.Start(arg);
            return 0;
        }
        public virtual int StopTest(object arg)
        {
            //m_threadManager.Abort();
            return 0;
        }
        public virtual int PauseTest(object arg)
        {
            m_threadManager.Suspend();
            return 0;
        }
        public virtual int ResumeTest(object arg)
        {
            m_threadManager.Resume();
            return 0;
        }
        public virtual int IsTesting(int index)
        {
            return 0;
        }

        public virtual void SetEngineCore(int nnum)
        {
            m_ModuleCount = nnum;
        }
         public virtual int GetEngineCore(object arg)
        {
          return m_ModuleCount;
         }


               



        public virtual int RegisterModule(object module)
        {
            return 0;
        }
        public virtual int RegisterScript(string szpath)
        {
            return 0;
        }
        public virtual int RegisterString(string szbuffer)
        {
            return 0;
        }
        public virtual object GetScriptEngine(int index)
        {
            return 0;
        }
        public virtual object GetTestContext(int index)
        {
            return 0;
        }

        public virtual int LoadProfile(string profile)
        {
            return 0;
        }


        public virtual int LoadTestCaseFromDb(string file)//这里发消息，桌面去加载

        {
            return 0;
        
        }

        protected virtual void TestEntry(object obj)
        {
            return;
        }
    }
}
