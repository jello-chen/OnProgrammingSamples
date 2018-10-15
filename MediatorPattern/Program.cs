using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.Linq;
using System.Runtime;
using System.Text;
using System.Threading.Tasks;

namespace MediatorPattern
{
    class Program
    {
        static void Main(string[] args)
        {
            AbstractCardPartner partnerA = new PartnerA("张三");
            AbstractCardPartner partnerB = new PartnerB("李四");
            partnerA.Money = 20m;
            partnerB.Money = 20m;
            AbstractMediator mediator = new ConcreteMediator(partnerA, partnerB);
            partnerA.ChangeMoney(5, mediator);
            Console.WriteLine("{0}:{1},{2}:{3}", partnerA.Name, partnerA.Money.ToString("C"), partnerB.Name, partnerB.Money.ToString("C"));
            partnerB.ChangeMoney(10, mediator);
            Console.WriteLine("{0}:{1},{2}:{3}", partnerA.Name, partnerA.Money.ToString("C"), partnerB.Name, partnerB.Money.ToString("C"));
            Console.ReadKey();
        }
    }

    abstract class AbstractMediator
    {
        protected AbstractCardPartner partnerA;
        protected AbstractCardPartner partnerB;

        protected AbstractMediator(AbstractCardPartner partnerA, AbstractCardPartner partnerB)
        {
            this.partnerA = partnerA;
            this.partnerB = partnerB;
        }

        public abstract void AWin(decimal money);
        public abstract void BWin(decimal money);
    }

    class ConcreteMediator:AbstractMediator
    {
        public ConcreteMediator(AbstractCardPartner partnerA, AbstractCardPartner partnerB) : base(partnerA, partnerB)
        {
            
        }
         
        public override void AWin(decimal money)
        {
            partnerA.Money += money;
            partnerB.Money -= money;
        }

        public override void BWin(decimal money)
        {
            partnerA.Money -= money;
            partnerB.Money += money;
        }
    }

    abstract class AbstractCardPartner
    {
        public decimal Money { get; set; }
        public string Name { get; set; }

        protected AbstractCardPartner(string name)
        {
            this.Name = name;
        }

        public abstract void ChangeMoney(decimal money, AbstractMediator mediator);
    }

    class PartnerA:AbstractCardPartner
    {
        public PartnerA(string name) : base(name)
        {
            
        }

        public override void ChangeMoney(decimal money, AbstractMediator mediator)
        {
            mediator.AWin(money);
        }
    }

    class PartnerB:AbstractCardPartner
    {
        public PartnerB(string name) : base(name)
        {
            
        }
        public override void ChangeMoney(decimal money, AbstractMediator mediator)
        {
            mediator.BWin(money);
        }
    }
}
