using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace libCommon.Class
{
    class tmTestItem:Dictionary<string,object>
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
    }

    class Test
    {
        //tmTestItem t = new tmTestItem();
        Dictionary<string,object> t=new Dictionary<string,object>();
    }
}
