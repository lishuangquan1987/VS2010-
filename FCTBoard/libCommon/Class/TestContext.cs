using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TestStudio.Automation.TestManager.libCommon.Class
{
    public class TestContext
    {
        public static DictionaryEx m_dicGlobal = new DictionaryEx();
        public static DictionaryEx m_dicConfig = new DictionaryEx();
        public DictionaryEx m_dicContext = new DictionaryEx();
        public static List<string> testcaseList=new List<string>();
        public  static  SqlDbHelper dbhelp= new SqlDbHelper();

        public static string connstr;

        public string getContext(string key,int index)
        {
            object obj;
            switch (index)
            {
                case 0: //test context
                    if (m_dicContext.ContainsKey(key))
                    {
                        obj = m_dicContext[key];
                    }
                    else
                    {
                        return null;
                    }
                    break;
                case 1: //global information
                    if (m_dicGlobal.ContainsKey(key))
                    {
                        obj = m_dicGlobal[key];
                    }
                    else
                    {
                        return null;
                    }
                    break;
                case 2: //system configuration
                    if (m_dicConfig.ContainsKey(key))
                    {
                        obj = m_dicConfig[key];
                    }
                    else
                    {
                        return null;
                    }
                    break;
                default:
                    return null;
                    break;
            }
            return obj.ToString();
        }
    }
}
