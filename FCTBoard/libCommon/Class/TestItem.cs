using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TestStudio.Automation.TestManager.libCommon.Class;

namespace TestStudio.Automation.TestManager.libCommon.Class
{
    public class TestItem : Dictionary<string, object>
    {
        public string enable { get; set; }
        public string name { get; set; }
        public string upper { get; set; }
        public string lower { get; set; }
        public string unit { get; set; }
        public string entry { get; set; }
        public string parameter { get; set; }
        public string stopfail { get; set; }
        public string pudding { get; set; }
        public string visible { get; set; }
        public string loop { get; set; }
        public string remark { get; set; }
        public override string ToString()
        {
            string buffer="";
            foreach (string key in Keys)
            {
                if (key == null)
                {
                    Console.WriteLine("lxl:key== null in Tostring\n");
                    continue;
                }
                string value = this[key] as string;
                if (value == null)
                {
                    Console.WriteLine("lxl:value == null in Tostring\n");
                    continue;
                }
                if (key=="name")
                {
                    value = "\"" + value + "\"";
                }

                if (value.Length == 0)
                {
                    value = "nil";
                }
                buffer += string.Format("{0}={1};", key, value);
            }
            buffer = "{" + buffer + "}";
            return buffer;
            //return base.ToString();
        }
    }

    public class GlobalVariable
    {
        public GlobalVariable()
        {
            name="__variable";
            contents="nil";
        }

        public GlobalVariable(string name,string contents)
        {
            this.name = name;
            this.contents = contents;
        }
        public string name { get; set; }
        public string contents { get; set; }
        public override string ToString()
        {
            return string.Format("{0}={1}", name, contents);
            //return base.ToString();
        }
    }

    public class GlobalFunction:GlobalVariable
    {
        public GlobalFunction()
        {

        }
        public GlobalFunction(string name,string contents)
        {
            this.name = name;
            this.contents = contents;
        }

        public override string ToString()
        {
            return this.contents;
        }
    }
}
