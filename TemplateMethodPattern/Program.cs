using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TemplateMethodPattern
{
    class Program
    {
        static void Main(string[] args)
        {
            Vegetabel spinach = new Spinach("菠菜");
            spinach.CookVegetabel();
            Console.WriteLine("------------------------");
            Vegetabel cabbage = new ChineseCabbage("白菜");
            cabbage.CookVegetabel();
            Console.ReadKey();
        }
    }

    public abstract class Vegetabel
    {
        private string name;
        protected Vegetabel(string name)
        {
            this.name = name;
        }
        // 模板方法，不要把模版方法定义为Virtual或abstract方法，避免被子类重写，防止更改流程的执行顺序
        public void CookVegetabel()
        {
            Console.WriteLine("抄{0}的一般做法", this.name);
            this.pourOil();
            this.HeatOil();
            this.pourVegetable();
            this.stir_fry();
        }

        // 第一步倒油
        public void pourOil()
        {
            Console.WriteLine("倒油");
        }

        // 把油烧热
        public void HeatOil()
        {
            Console.WriteLine("把油烧热");
        }

        // 油热了之后倒蔬菜下去，具体哪种蔬菜由子类决定
        public abstract void pourVegetable();

        // 开发翻炒蔬菜
        public void stir_fry()
        {
            Console.WriteLine("翻炒");
        }
    }

    // 菠菜
    public class Spinach : Vegetabel
    {
        public Spinach(string name) : base(name)
        {
            
        }
        public override void pourVegetable()
        {
            Console.WriteLine("倒菠菜进锅中");
        }
    }

    // 大白菜
    public class ChineseCabbage : Vegetabel
    {
        public ChineseCabbage(string name) : base(name)
        {
            
        }
        public override void pourVegetable()
        {
            Console.WriteLine("倒大白菜进锅中");
        }
    }
}
