using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleFactoryPattern
{
    class Program
    {
        static void Main(string[] args)
        {
            var add = OperatorFactory.GetOperator("+");
            add.NumA = 1;
            add.NumB = 2;
            Console.WriteLine(add.GetResult());
            var sub = OperatorFactory.GetOperator("-");
            sub.NumA = 2;
            sub.NumB = 1;
            Console.WriteLine(sub.GetResult());
            Console.ReadKey();
        }
    }

    abstract class Operator
    {
        public double NumA { get; set; }
        public double NumB { get; set; }
        public abstract double GetResult();
    }

    class AddOperator:Operator
    {
        public override double GetResult()
        {
            return NumA + NumB;
        }
    }

    class SubstractOperator:Operator
    {
        public override double GetResult()
        {
            return NumA - NumB;
        }
    }

    class OperatorFactory
    {
        public static Operator GetOperator(string op)
        {
            Operator _operator = null;
            switch (op)
            {
                case "+":
                    _operator = new AddOperator();
                    break;
                case "-":
                    _operator = new SubstractOperator();
                    break;
                default:
                    break;
            }
            return _operator;
        }
    }
}
