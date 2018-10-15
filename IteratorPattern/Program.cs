using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IteratorPattern
{
    class Program
    {
        static void Main(string[] args)
        {
            object[] objects = new[] { "hello", "jello", "welcome", "my", "home" };
            Aggregate aggregate = new ConcreteAggregate(objects);
            Iterator iterator = aggregate.CreateIterator();
            while (iterator.MoveNext())
            {
                Console.WriteLine(iterator.GetCurrent());
            }
            Console.ReadKey();
        }
    }

    abstract class Iterator
    {
        public abstract object GetCurrent();
        public abstract bool MoveNext();
        public abstract void Reset();
    }

    abstract class Aggregate
    {
        public abstract Iterator CreateIterator();
    }

    class ConcreteAggregate : Aggregate
    {
        private object[] objects;
        private int len;
        public int Len { get { return len; } }

        public ConcreteAggregate(object[] objects)
        {
            this.objects = objects;
            len = objects.Length;
        }

        public object this[int index]
        {
            get
            {
                if (index < 0 || index >= len)
                    throw new IndexOutOfRangeException("index");
                return objects[index];
            }
        }

        public override Iterator CreateIterator()
        {
            return new ConcreteIterator(this);
        }
    }

    class ConcreteIterator : Iterator
    {
        private int index = -1;
        private ConcreteAggregate aggregate;
        public ConcreteIterator(ConcreteAggregate aggregate)
        {
            this.aggregate = aggregate;
        }

        public override object GetCurrent()
        {
            return aggregate[index];
        }

        public override bool MoveNext()
        {
            if (index >= aggregate.Len - 1) return false;
            index++;
            return true;
        }

        public override void Reset()
        {
            index = -1;
        }
    }
}
