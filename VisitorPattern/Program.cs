using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VisitorPattern
{
    class Program
    {
        static void Main(string[] args)
        {
            ObjectStructure objectStructure = new ObjectStructure();
            objectStructure.Add(new ConcreteElementA());
            objectStructure.Add(new ConcreteElementB());

            Visitor visitor1 = new ConcreteVisitorA();
            Visitor visitor2 = new ConcreteVisitorB();

            objectStructure.Accept(visitor1);
            objectStructure.Accept(visitor2);
            Console.ReadKey();
        }
    }

    abstract class Visitor
    {
        public abstract void VisitConcreteElementA(ConcreteElementA elementA);
        public abstract void VisitConcreteElementB(ConcreteElementB elementB);
    }

    class ConcreteVisitorA : Visitor
    {

        public override void VisitConcreteElementA(ConcreteElementA elementA)
        {
            Console.WriteLine("{0} visited by {1}", elementA, this);
        }

        public override void VisitConcreteElementB(ConcreteElementB elementB)
        {
            Console.WriteLine("{0} visited by {1}", elementB, this);
        }
    }

    class ConcreteVisitorB : Visitor
    {

        public override void VisitConcreteElementA(ConcreteElementA elementA)
        {
            Console.WriteLine("{0} visited by {1}", elementA, this);
        }

        public override void VisitConcreteElementB(ConcreteElementB elementB)
        {
            Console.WriteLine("{0} visited by {1}", elementB, this);
        }
    }

    class ObjectStructure
    {
         private List<Element> elements = new List<Element>();

        public void Add(Element element)
        {
            elements.Add(element);
        }

        public void Remove(Element element)
        {
            elements.Remove(element);
        }

        public void Accept(Visitor visitor)
        {
            foreach (var element in elements)
            {
                element.Accept(visitor);
            }
        }
    }

    abstract class Element
    {
        public abstract void Accept(Visitor visitor);
    }

    class ConcreteElementA : Element
    {

        public override void Accept(Visitor visitor)
        {
            visitor.VisitConcreteElementA(this);
        }
    }

    class ConcreteElementB : Element
    {

        public override void Accept(Visitor visitor)
        {
            visitor.VisitConcreteElementB(this);
        }
    }
}
