using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ObserverPattern
{
    class Program
    {
        static void Main(string[] args)
        {
            Subject Wangsicong = new ConcreteSubject("王思聪");
            var jello = new ConcreteObserver("jello");
            var fanbingbing = new ConcreteObserver("范冰冰");
            var huangxiaoming = new ConcreteObserver("黄晓明");
            Wangsicong.Add(jello);
            Wangsicong.Add(fanbingbing);
            Wangsicong.Add(huangxiaoming);
            Wangsicong.Notify("国足不给力啊！");
            Console.WriteLine("---jello取消关注了王思聪---");
            Wangsicong.Remove(jello);
            Console.WriteLine("---angelababy关注了王思聪---");
            var angelababy = new ConcreteObserver("angelababy");
            Wangsicong.Add(angelababy);
            Wangsicong.Notify("蜻蜓FF老板应该坐牢");

            Console.ReadKey();
        }
    }

    abstract class Observer
    {
        protected string Name { get; set; }
        protected Observer(string name)
        {
            this.Name = name;
        }

        public abstract void Update(string msg);
    }

    abstract class Subject
    {
        private List<Observer> observers = new List<Observer>();
        private string name;

        protected Subject(string name)
        {
            this.name = name;
        }

        public void Add(Observer observer)
        {
            observers.Add(observer);
        }

        public void Remove(Observer observer)
        {
            observers.Remove(observer);
        }

        public void Notify(string msg)
        {
            foreach (var observer in observers)
            {
                observer.Update(msg);
            }
        }
    }

    class ConcreteSubject:Subject
    {
        public ConcreteSubject(string name) : base(name)
        {
            
        }
    }

    class ConcreteObserver:Observer
    {
        public ConcreteObserver(string name)
            : base(name)
        {
            
        }
        public override void Update(string msg)
        {
            Console.WriteLine("{0}收到消息：{1}", Name, msg);
        }
    }
}
