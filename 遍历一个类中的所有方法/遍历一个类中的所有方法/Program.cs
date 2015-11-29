using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LuaInterface;


namespace 遍历一个类中的所有方法
{
    class Program
    {
        static void Main(string[] args)
        {
            
        }
    }
    public class LuaFunction : Attribute
    {
        private String FunctionName;

        public LuaFunction(String strFuncName)
        {
            FunctionName = strFuncName;
        }

        public String getFuncName()
        {
            return FunctionName;
        }
    }

    /// <summary>
    /// Lua引擎
    /// </summary>
    class LuaFramework
    {
        private Lua pLuaVM = new Lua();//lua虚拟机

        /// <summary>
        /// 注册lua函数
        /// </summary>
        /// <param name="pLuaAPIClass">lua函数类</param>
        public void BindLuaApiClass(Object pLuaAPIClass)
        {
            foreach (System.Reflection.MethodInfo mInfo in pLuaAPIClass.GetType().GetMethods())
            {
                foreach (Attribute attr in Attribute.GetCustomAttributes(mInfo))
                {
                    string LuaFunctionName = (attr as LuaFunction).getFuncName();
                    pLuaVM.RegisterFunction(LuaFunctionName, pLuaAPIClass, mInfo);
                }
            }
        }

        /// <summary>
        /// 执行lua脚本文件
        /// </summary>
        /// <param name="luaFileName">脚本文件名</param>
        public void ExecuteFile(string luaFileName)
        {
            try
            {
                pLuaVM.DoFile(luaFileName);
            }
            catch (Exception e)
            {
                //MessageBox.Show(e.ToString());
            }
        }

        /// <summary>
        /// 执行lua脚本
        /// </summary>
        /// <param name="luaCommand">lua指令</param>
        public void ExecuteString(string luaCommand)
        {
            try
            {
                pLuaVM.DoString(luaCommand);
            }
            catch (Exception e)
            {
              //  MessageBox.Show(e.ToString());
            }
        }
    }


}


