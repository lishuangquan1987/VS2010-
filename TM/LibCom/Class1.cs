using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LibCom
{
    public class Class1
    {
    }
    public class Exdictionary : Dictionary<string, object>
    { 
    }
    
    public interface IMoudle
    {
        int Load(object sender, object args);
        int RegisterMoudle(object sender, object args);
        int UnLoad(object sender, object args);
    }
}
