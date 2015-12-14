using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using LuaInterface;

using TestStudio.Automation.TestManager.libCommon.Interface;
namespace TestStudio.Automation.TestManager.libCommon.Class
{
    public class ScriptEngine:IScriptEngine
    {
        public virtual int LoadFile(string filepath)
        {
            return 0;
        }
        public virtual object[] DoFile(string filepath)
        {
            return null;
        }
        public virtual int LoadString(string buffer)
        {
            return 0;
        }
        public virtual object[] DoString(string buffer)
        {
            return null;
        }
        public virtual int Realese()
        {
            return 0;
        }

        public virtual object GetScriptHandle()
        {
            return null;
        }
    }

    public class luaEngine:ScriptEngine
    {
        protected Lua m_Lua = new Lua();
        public override int LoadFile(string filepath)
        {
            return 0;
        }
        public override object[] DoFile(string filepath)
        {
            return m_Lua.DoFile(filepath);
        }
        public override int LoadString(string buffer)
        {
            return 0;
        }
        public override object[] DoString(string buffer)
        {
            return m_Lua.DoString(buffer);
        }
        public override int Realese()
        {
            return 0;
        }

        public override object GetScriptHandle()
        {
            return m_Lua;
        }

        public object RegisterFunction(string path,object target,System.Reflection.MethodInfo function)
        {
            return m_Lua.RegisterFunction(path, target, function);
        }
    }
}
