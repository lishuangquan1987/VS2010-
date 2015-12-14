using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TestStudio.Automation.TestManager.libCommon.Interface
{
    public interface IModule
    {
        int Load(object sender, object arg);
        int RegisterModule(object sender, object arg);
        int Initial(object sender, object arg);
        int SelfTest(object sender, object arg);
        int UnLoad(object sender, object arg);
    }
    public interface IUserInterface
    {
        int OnTestStart(object sender, object arg);
        int OnTestStop(object sender, object arg);
        int OnTestPause(object sender, object arg);
        int OnTestResume(object sender, object arg);
        int OnItemStart(object sender, object arg);
        int OnItemFinish(object sender, object arg);
        int OnTestFinish(object sender, object arg);
    }

    public interface IEngineInterface
    {
        int StartTest(object arg);
        int StopTest(object arg);
        int PauseTest(object arg);
        int ResumeTest(object arg);
        int IsTesting(int index);

        int GetEngineCore(object arg);

       // int GetEngineCore();

        int LoadProfile(string profile);
        int RegisterModule(object module);
        int RegisterScript(string szpath);
        int RegisterString(string szbuffer);
        object GetScriptEngine(int index);
        object GetTestContext(int index);
    }

    public interface IScriptEngine
    {
        int LoadFile(string filepath);
        object[] DoFile(string filepath);
        int LoadString(string buffer);
        object[] DoString(string buffer);
        int Realese();
        object GetScriptHandle();
    }

    public interface IDebugger
    {
        int StepInto(object sender);
        int StepOver(object sender);
    }

    public interface IConfig
    {
        TestStudio.Automation.TestManager.libCommon.Class.DictionaryEx GetConfig();
        void SetConfig(TestStudio.Automation.TestManager.libCommon.Class.DictionaryEx cfg);
    }
}
