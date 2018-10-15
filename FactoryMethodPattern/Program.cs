using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FactoryMethodPattern
{
    class Program
    {
        static void Main(string[] args)
        {
            //Add
            OperationFactory addOperationFactory = new AddOperationFactory();
            Operation addOperation = addOperationFactory.GetOperation();
            addOperation.NumA = 1;
            addOperation.NumB = 2;
            Console.WriteLine(addOperation.GetResult());
            //Sub
            OperationFactory subOperationFactory = new SubOperationFactory();
            Operation subOperation = subOperationFactory.GetOperation();
            subOperation.NumA = 2;
            subOperation.NumB = 1;
            Console.WriteLine(subOperation.GetResult());
            Console.ReadKey();
        }
    }

    abstract class OperationFactory
    {
        public abstract Operation GetOperation();
    }

    class AddOperationFactory:OperationFactory
    {
        public override Operation GetOperation()
        {
            return new AddOperation();
        }
    }

    class SubOperationFactory:OperationFactory
    {

        public override Operation GetOperation()
        {
            return new SubOperation();
        }
    }

    abstract class Operation
    {
        public double NumA { get; set; }
        public double NumB { get; set; }

        public abstract double GetResult();
    }

    class AddOperation:Operation
    {
        public override double GetResult()
        {
            return NumA + NumB;
        }
    }

    class SubOperation:Operation
    {

        public override double GetResult()
        {
            return NumA - NumB;
        }
    }
}
