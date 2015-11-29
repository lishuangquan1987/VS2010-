using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace 观察者模式.Net实现
{
    class Program
    {
        static void Main(string[] args)
        {
            Robber rober = new Robber();
            Police police = new Police();
            Wife wife = new Wife();

            rober.run += police.CatchRobber;
            rober.run += wife.Pray;

            rober.runAway("天津");
            rober.runAway("上海");
            Console.Read();
        }
    }
    /// <summary>
    /// 逃跑事件
    /// </summary>
    public class RunArgs
    {
      public string currentLocation;
      public string preLocation;
    }
    public delegate void RunAwayHandler(Robber robber,RunArgs args);
    public class Robber
    {
        private string _location = "北京";

        public string Location
        {
            get { return _location; }
           
        }
        public event RunAwayHandler run = null;
        public void runAway(string Location)
        {
            if (this.run != null)
            {
                RunArgs args = new RunArgs();
                args.currentLocation = Location;
                args.preLocation = _location;
                _location = Location;
                this.run(this,args);//激发事件只能在事件所在类的内部进行。
                
            }
        }
    }
    public class Police
    {
        public void CatchRobber(Robber robber, RunArgs args)
        {
            Console.WriteLine("抢劫犯从{0}跑到{1}了，去抓！",args.preLocation,args.currentLocation);//推模式
        }
    }
    public class Wife
    {
        public void Pray(Robber husband,RunArgs args)
        {
            Console.WriteLine("愿在{0}的丈夫一切安好！",husband.Location);//拉模式
        }
    }
}
