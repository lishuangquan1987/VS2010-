using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

//模拟打兵乓球
namespace DesignPatten_TemplateMethod_Training
{
    class Program
    {
        static void Main(string[] args)
        {
            PlayGame player1 = new PlayPingbang();
            player1.play();
            Console.Read();
        }
    }
    abstract class PlayGame
    {
        protected abstract void holdgloble();
        protected abstract void sendgloble();
        protected abstract void receivegloble();
        public  void play()
        {
            this.holdgloble();
            this.sendgloble();
            this.receivegloble();
        }
    }
    class PlayPingbang : PlayGame
    {
        protected override void holdgloble()
        {
            Console.WriteLine("拿起兵乓球");
        }

        protected override void sendgloble()
        {
            Console.WriteLine("发兵乓球");
        }

        protected override void receivegloble()
        {
            Console.WriteLine("接兵乓球");
        }

       
    }
}
