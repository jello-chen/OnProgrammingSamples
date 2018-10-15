using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StrategyPattern
{
    class Program
    {
        static void Main(string[] args)
        {
            AddOperation addOperation = new AddOperation();
            addOperation.NumA = 1;
            addOperation.NumB = 2;
            Context context = new Context(addOperation);
            Console.WriteLine(context.GetResult());
            Console.ReadKey();
        }
    }

    /// <summary>
    /// 运算类
    /// </summary>
    abstract class Operation
    {
        public double NumA { get; set; }
        public double NumB { get; set; }

        public abstract double GetResult();
    }
    /// <summary>
    /// 加法运算
    /// </summary>
    class AddOperation:Operation
    {
        public override double GetResult()
        {
            return NumA + NumB;
        }
    }
    /// <summary>
    /// 减法运算
    /// </summary>
    class SubOperation:Operation
    {

        public override double GetResult()
        {
            return NumA - NumB;
        }
    }
    /// <summary>
    /// 上下文
    /// </summary>
    class Context
    {
        private Operation operation;
        public Context(Operation operation)
        {
            this.operation = operation;
        }

        public double GetResult()
        {
            return operation.GetResult();
        }
    }
}
