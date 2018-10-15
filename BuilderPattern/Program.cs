using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BuilderPattern
{
    class Program
    {
        static void Main(string[] args)
        {
            Director director = new Director();
            //构造加法运算
            BinaryOperationBuilder addBinaryOperationBuilder = new AddBinaryOperationBuilder();
            director.BuildBinaryOperation(addBinaryOperationBuilder);
            BinaryOperation addBinaryOperation = addBinaryOperationBuilder.GetBinaryOperation();
            addBinaryOperation.Show();
            //构造减法运算
            BinaryOperationBuilder subBinaryOperationBuilder = new SubBinaryOperationBuilder();
            director.BuildBinaryOperation(subBinaryOperationBuilder);
            BinaryOperation subBinaryOperation = subBinaryOperationBuilder.GetBinaryOperation();
            subBinaryOperation.Show();
            Console.ReadKey();
        }
    }

    class Director
    {
        public void BuildBinaryOperation(BinaryOperationBuilder builder)
        {
            builder.BuildNumA();
            builder.BuildNumB();
            builder.BuildBinaryOperator();
        }
    }

    /// <summary>
    /// 二元运算建造者
    /// </summary>
    abstract class BinaryOperationBuilder
    {
        public abstract void BuildNumA();
        public abstract void BuildNumB();
        public abstract void BuildBinaryOperator();
        public abstract BinaryOperation GetBinaryOperation();
    }

    class BinaryOperation
    {
        public double NumA { get; set; }
        public double NumB { get; set; }
        public string Operator { get; set; }

        public void Show()
        {
            Console.WriteLine(string.Format("Build Binary Operation:{0} {1} {2}", NumA, Operator, NumB));
        }
    }

    class AddBinaryOperationBuilder:BinaryOperationBuilder
    {
        private BinaryOperation binaryOperation = new BinaryOperation();
        public override void BuildNumA()
        {
            binaryOperation.NumA = 1;
        }

        public override void BuildNumB()
        {
            binaryOperation.NumB = 2;
        }

        public override void BuildBinaryOperator()
        {
            binaryOperation.Operator = "+";
        }

        public override BinaryOperation GetBinaryOperation()
        {
            return binaryOperation;
        }
    }

    class SubBinaryOperationBuilder:BinaryOperationBuilder
    {
        private BinaryOperation binaryOperation = new BinaryOperation();

        public override void BuildNumA()
        {
            binaryOperation.NumA = 7;
        }

        public override void BuildNumB()
        {
            binaryOperation.NumB = 1;
        }

        public override void BuildBinaryOperator()
        {
            binaryOperation.Operator = "-";
        }

        public override BinaryOperation GetBinaryOperation()
        {
            return binaryOperation;
        }
    }

}
