using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VisitorPattern2
{
    class Program
    {
        static void Main(string[] args)
        {
            Coffee coffee = new Coffee();
            Meat meat = new Meat();
            Vegetables vegetables = new Vegetables();

            VisitorA visitorA = new VisitorA();
            coffee.Accept(visitorA);

            meat.Accept(visitorA);

            VisitorB visitorB = new VisitorB();
            vegetables.Accept(visitorB);
            Console.ReadKey();
        }
    }

    abstract class Visitor
    {
        public abstract void VisitCoffee(Coffee coffee);
        public abstract void VisitMeat(Meat meat);
        public abstract void VisitVegetables(Vegetables vegetables);
    }

    abstract class Food
    {
        public abstract void Accept(Visitor visitor);
    }

    class Coffee:Food
    {

        public override void Accept(Visitor visitor)
        {
            visitor.VisitCoffee(this);
        }

        public void AddSugar()
        {
            Console.WriteLine("Add Sugar");
        }

        public void AddMilk()
        {
            Console.WriteLine("Add Milk");
        }
    }

    class Meat:Food
    {
        public override void Accept(Visitor visitor)
        {
            visitor.VisitMeat(this);
        }
    }

    class Vegetables:Food
    {

        public override void Accept(Visitor visitor)
        {
            visitor.VisitVegetables(this);
        }

        public void AddHot()
        {
            Console.WriteLine("Add hot");
        }
    }

    class VisitorA:Visitor
    {

        public override void VisitCoffee(Coffee coffee)
        {
            Console.WriteLine("{0} take a cup of {1}", this, coffee);
            coffee.AddSugar();
            coffee.AddMilk();
        }

        public override void VisitMeat(Meat meat)
        {
            Console.WriteLine("{0} do not want to eat {1}", this, meat);
        }

        public override void VisitVegetables(Vegetables vegetables)
        {
            Console.WriteLine("I do not want to eat vegetables");
        }
    }

    class VisitorB:Visitor
    {

        public override void VisitCoffee(Coffee coffee)
        {
            Console.WriteLine("I don not want to drink coffee");
        }

        public override void VisitMeat(Meat meat)
        {
            
        }

        public override void VisitVegetables(Vegetables vegetables)
        {
            Console.WriteLine("{0} take some {1}", this, vegetables);
            vegetables.AddHot();
        }
    }
}
