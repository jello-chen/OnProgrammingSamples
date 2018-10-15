using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DelegateEventForObserverPattern
{
    public delegate void PutableDelegate(string s);
    class Program
    {
        static void Main(string[] args)
        {
            //被订阅者
            MilkSender milkSender = new MilkSender();
            NewspaperSender newspaperSender = new NewspaperSender();

            //订阅者
            Subscriber subscriber1 = new Subscriber("jello");
            Subscriber subscriber2 = new Subscriber("jimmy");
            Subscriber subscriber3 = new Subscriber("taffy");

            //开始订阅牛奶
            milkSender.AddSubscriber(subscriber1.PutMilkIn);
            milkSender.AddSubscriber(subscriber2.PutMilkIn);
            //开始订阅报纸
            newspaperSender.AddSubscriber(subscriber2.PutNewspaperIn);
            newspaperSender.AddSubscriber(subscriber3.PutNewspaperIn);

            //送牛奶
            milkSender.SendMilk();
            Console.WriteLine();
            //送报纸
            newspaperSender.SendNewspaper();

            Console.WriteLine();
            //jimmy不订阅报纸了，使用报箱装牛奶
            newspaperSender.RemoveSubscriber(subscriber2.PutNewspaperIn);
            newspaperSender.SendNewspaper();
            Console.WriteLine();
            milkSender.RemoveSubscriber(subscriber2.PutMilkIn);
            milkSender.AddSubscriber(subscriber2.PutNewspaperIn);
            milkSender.SendMilk();
            
            Console.ReadKey();
        }
    }

    public interface IMilkBox
    {
        void PutMilkIn(string s);
    }

    public interface INewspaperBox
    {
        void PutNewspaperIn(string s);
    }

    public class Subscriber : IMilkBox, INewspaperBox
    {
        private string name;
        public Subscriber(string name)
        {
            this.name = name;
        }

        #region IMilkBox Members

        public void PutMilkIn(string s)
        {
            Console.WriteLine("{0}收到奶箱中的{1}", name, s);
        }

        #endregion

        #region INewspaperBox Members

        public void PutNewspaperIn(string s)
        {
            Console.WriteLine("{0}收到报箱中的{1}", name, s);
        }

        #endregion
    }

    public class MilkSender
    {
        private event PutableDelegate subscribers;

        public void AddSubscriber(PutableDelegate subscriber)
        {
            subscribers += subscriber;
        }

        public void RemoveSubscriber(PutableDelegate subscriber)
        {
            subscribers -= subscriber;
        }

        public void SendMilk()
        {
            if (subscribers != null)
                subscribers("牛奶");
        }
    }

    public class NewspaperSender
    {
        private event PutableDelegate subscribers;

        public void AddSubscriber(PutableDelegate subscriber)
        {
            subscribers += subscriber;
        }

        public void RemoveSubscriber(PutableDelegate subscriber)
        {
            subscribers -= subscriber;
        }

        public void SendNewspaper()
        {
            if (subscribers != null)
                subscribers("报纸");
        }
    }
}
