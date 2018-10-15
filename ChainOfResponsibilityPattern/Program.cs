using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChainOfResponsibilityPattern
{
    class Program
    {
        static void Main(string[] args)
        {
            Approver jello = new Manager("jello");
            Approver jimmy = new VicePresident("jimmy");
            Approver taffy = new President("taffy");
            jello.NextApprover = jimmy;
            jimmy.NextApprover = taffy;
            //买打印机
            PurchaseRequest request1 = new PurchaseRequest(500, "printer");
            jello.ProcessRequest(request1);
            //买PC
            PurchaseRequest request2 = new PurchaseRequest(4000, "PC");
            jello.ProcessRequest(request2);
            //买PCs
            PurchaseRequest request3 = new PurchaseRequest(8000, "PCs");
            jello.ProcessRequest(request3);
            //买工作站
            PurchaseRequest request4 = new PurchaseRequest(15000, "WorkStation");
            jello.ProcessRequest(request4);
            Console.ReadKey();
        }
    }

    // 采购请求
    public class PurchaseRequest
    {
        // 金额
        public double Amount { get; set; }
        // 产品名字
        public string ProductName { get; set; }
        public PurchaseRequest(double amount, string productName)
        {
            Amount = amount;
            ProductName = productName;
        }
    }
    // 审批人,Handler
    abstract class Approver
    {
        public Approver NextApprover { get; set; }
        public string Name { get; set; }

        protected Approver(string name)
        {
            this.Name = name;
        }

        public abstract void ProcessRequest(PurchaseRequest request);
    }

    class Manager:Approver
    {
        public Manager(string name) : base(name)
        {
            
        }
        public override void ProcessRequest(PurchaseRequest request)
        {
            if (request.Amount < 1000)
            {
                Console.WriteLine("{0}-{1} approved the request of purshing {2}", this, Name, request.ProductName);
            }
            else
            {
                NextApprover.ProcessRequest(request);
            }
        }
    }

    class VicePresident : Approver
    {
        public VicePresident(string name)
            : base(name)
        {

        }
        public override void ProcessRequest(PurchaseRequest request)
        {
            if (request.Amount < 5000)
            {
                Console.WriteLine("{0}-{1} approved the request of purshing {2}", this, Name, request.ProductName);
            }
            else
            {
                NextApprover.ProcessRequest(request);
            }
        }
    }

    class President : Approver
    {
        public President(string name)
            : base(name)
        {

        }
        public override void ProcessRequest(PurchaseRequest request)
        {
            if (request.Amount < 10000)
            {
                Console.WriteLine("{0}-{1} approved the request of purshing {2}", this, Name, request.ProductName);
            }
            else
            {
                Console.WriteLine("Purshing {0} Request需要组织一个会议讨论", request.ProductName);
            }
        }
    }
}
