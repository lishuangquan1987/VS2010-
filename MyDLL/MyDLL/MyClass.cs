using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LibCom;


namespace MyDLL
{
    public class MyClass:ImyInterface
    {

        public void Load(object sender, EventArgs e)
        {
            throw new NotImplementedException();
        }
        public int add(int a, int b)
        {
            return a - b;
        }
    }
}
