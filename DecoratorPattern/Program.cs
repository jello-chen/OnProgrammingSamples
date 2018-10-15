using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace DecoratorPattern
{
    class Program
    {
        static void Main(string[] args)
        {
            //普通手机
            Console.WriteLine("----------------");
            Phone phone = new ApplePhone();
            phone.Print();
            Console.WriteLine("----------------");
            //想贴个膜
            Decorator stickerDecorator = new StickerPhone(phone);
            stickerDecorator.Print();
            Console.WriteLine("----------------");
            //又想挂个件
            Decorator accessoriesDecorator = new AccessoriesPhone(stickerDecorator);
            accessoriesDecorator.Print();
            Console.WriteLine("----------------");
            
            Console.ReadKey();
        }
    }

    /// <summary>
    /// 手机
    /// </summary>
    abstract class Phone
    {
        public abstract void Print();
    }
    /// <summary>
    /// 苹果手机
    /// </summary>
    class ApplePhone : Phone
    {
        public override void Print()
        {
            Console.WriteLine("我是一个普通的苹果手机");
        }
    }
    /// <summary>
    /// 装饰器
    /// </summary>
    abstract class Decorator : Phone
    {
        private Phone phone;
        protected Decorator(Phone phone)
        {
            this.phone = phone;
        }
        public override void Print()
        {
            if (phone != null)
                phone.Print();
        }
    }
    /// <summary>
    /// 贴了膜的手机
    /// </summary>
    class StickerPhone : Decorator
    {
        public StickerPhone(Phone phone)
            : base(phone)
        {

        }

        public override void Print()
        {
            base.Print();
            AddSticker();
        }

        public void AddSticker()
        {
            Console.WriteLine("给手机加个膜");
        }
    }
    /// <summary>
    /// 加了挂件的手机
    /// </summary>
    class AccessoriesPhone : Decorator
    {
        public AccessoriesPhone(Phone phone)
            : base(phone)
        {

        }

        public override void Print()
        {
            base.Print();
            AddAccessories();
        }

        public void AddAccessories()
        {
            Console.WriteLine("给手机加个挂件");
        }
    }
}
