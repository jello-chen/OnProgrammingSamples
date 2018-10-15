using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlyweightPattern
{
    class Program
    {
        static void Main(string[] args)
        {
            // 定义外部状态，例如字母的位置等信息
            int externalstate = 10;
            string[] strings = new[] {"A", "B", "C", "D"};
            Random random = new Random();
            FlyweightFactory factory = new FlyweightFactory();
            for (int i = 0; i < 10; i++)
            {
                var s = strings[random.Next(4)];
                Flyweight fd = factory.GetFlyweight(s);
                if (fd != null)
                {
                    fd.Operate(--externalstate);
                }
                else
                {
                    Console.WriteLine("{0}加入驻留池", s);
                    factory.Flyweights.Add("D", new ConcreteFlyweight(s));
                }
            }

            Console.ReadKey();
        }
    }

    class FlyweightFactory
    {
        public Dictionary<string, Flyweight> Flyweights = new Dictionary<string, Flyweight>();

        public FlyweightFactory()
        {
            Flyweights.Add("A", new ConcreteFlyweight("A"));
            Flyweights.Add("B", new ConcreteFlyweight("B"));
            Flyweights.Add("C", new ConcreteFlyweight("C"));
        }

        public Flyweight GetFlyweight(string key)
        {
            if (Flyweights.ContainsKey(key))
                return Flyweights[key];
            return null;
        }
    }

    abstract class Flyweight
    {
        public abstract void Operate(int externalState);
    }

    class ConcreteFlyweight : Flyweight
    {
        private string inernalState;

        public ConcreteFlyweight(string internalState)
        {
            this.inernalState = internalState;
        }

        public override void Operate(int externalState)
        {
            Console.WriteLine("具体实现类: intrinsicstate {0}, extrinsicstate {1}", inernalState, externalState);
        }
    }
}
