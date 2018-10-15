using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace AbstractFactoryPattern
{
    class Program
    {
        static void Main(string[] args)
        {
            //加法的二元、三元运算
            OperationFactory addOperationFactory = new AddOperationFactory();
            BinaryOperation addBinaryOperation = addOperationFactory.GetBinaryOperation();
            addBinaryOperation.NumA = 1;
            addBinaryOperation.NumB = 2;
            Console.WriteLine(addBinaryOperation.GetResult());
            TernaryOperation addTernaryOperation = addOperationFactory.GetTernaryOperation();
            addTernaryOperation.NumA = 1;
            addTernaryOperation.NumB = 2;
            addTernaryOperation.NumC = 3;
            Console.WriteLine(addTernaryOperation.GetResult());
            //减法的二元、三元运算
            OperationFactory subOperationFactory = new SubOperationFactory();
            BinaryOperation subBinaryOperation = subOperationFactory.GetBinaryOperation();
            subBinaryOperation.NumA = 2;
            subBinaryOperation.NumB = 1;
            Console.WriteLine(subBinaryOperation.GetResult());
            TernaryOperation subTernaryOperation = subOperationFactory.GetTernaryOperation();
            subTernaryOperation.NumA = 10;
            subTernaryOperation.NumB = 2;
            subTernaryOperation.NumC = 6;
            Console.WriteLine(subTernaryOperation.GetResult());

            Console.ReadKey();
        }
    }
    /// <summary>
    /// 运算工厂
    /// </summary>
    abstract class OperationFactory
    {
        public abstract BinaryOperation GetBinaryOperation();
        public abstract TernaryOperation GetTernaryOperation();
    }
    /// <summary>
    /// 加法运算工厂
    /// </summary>
    class AddOperationFactory:OperationFactory
    {

        public override BinaryOperation GetBinaryOperation()
        {
            return new AddBinaryOperation();
        }

        public override TernaryOperation GetTernaryOperation()
        {
            return new AddTernaryOperation();
        }
    }
    /// <summary>
    /// 减法运算工厂
    /// </summary>
    class SubOperationFactory:OperationFactory
    {

        public override BinaryOperation GetBinaryOperation()
        {
            return new SubBinaryOperation();
        }

        public override TernaryOperation GetTernaryOperation()
        {
            return new SubTernaryOperation();
        }
    }

    /// <summary>
    /// 二元运算
    /// </summary>
    abstract class BinaryOperation
    {
        public double NumA { get; set; }
        public double NumB { get; set; }

        public abstract double GetResult();
    }
    /// <summary>
    /// 三元运算
    /// </summary>
    abstract class TernaryOperation
    {
        public double NumA { get; set; }
        public double NumB { get; set; }
        public double NumC { get; set; }

        public abstract double GetResult();
    }
    /// <summary>
    /// 二元加法运算
    /// </summary>
    class AddBinaryOperation:BinaryOperation
    {

        public override double GetResult()
        {
            return NumA + NumB;
        }
    }
    /// <summary>
    /// 二元减法运算
    /// </summary>
    class SubBinaryOperation:BinaryOperation
    {
        public override double GetResult()
        {
            return NumA - NumB;
        }
    }
    /// <summary>
    /// 三元加法运算
    /// </summary>
    class AddTernaryOperation:TernaryOperation
    {

        public override double GetResult()
        {
            return NumA + NumB + NumC;
        }
    }
    /// <summary>
    /// 三元减法运算
    /// </summary>
    class SubTernaryOperation:TernaryOperation
    {

        public override double GetResult()
        {
            return NumA - NumB - NumC;
        }
    }
}
