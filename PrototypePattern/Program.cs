using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;

namespace PrototypePattern
{
    class Program
    {
        static void Main(string[] args)
        {
            //原型对象
            MonkeyKingPrototype monkeyKingPrototype = new MonkeyKing("孙悟空");
            monkeyKingPrototype.Teacher = new Teacher() {Name = "唐僧"};
            //变一个
            MonkeyKingPrototype monkeyKing1 = monkeyKingPrototype.Clone();
            Console.WriteLine(string.Format("名字：{0}，师傅：{1}", monkeyKing1.Name, monkeyKing1.Teacher.Name));
            //第一个孙悟空的师傅是菩提祖师
            monkeyKing1.Teacher.Name = "菩提祖师";
            //再变一个
            MonkeyKingPrototype monkeyKing2 = monkeyKingPrototype.Clone();
            Console.WriteLine(string.Format("名字：{0}，师傅：{1}", monkeyKing2.Name, monkeyKing2.Teacher.Name));
            Console.ReadKey();
        }
    }
    [Serializable]
    abstract class MonkeyKingPrototype
    {
        public string Name { get; set; }
        public Teacher Teacher { get; set; }

        protected MonkeyKingPrototype(string name)
        {
            this.Name = name;
        }

        public abstract MonkeyKingPrototype Clone();
    }
    [Serializable]
    class MonkeyKing : MonkeyKingPrototype
    {
        public MonkeyKing(string name)
            : base(name)
        {

        }

        public override MonkeyKingPrototype Clone()
        {
            //深克隆
            using (MemoryStream ms = new MemoryStream())
            {
                BinaryFormatter formatter = new BinaryFormatter();
                formatter.Serialize(ms, this);
                ms.Seek(0, 0);
                return (MonkeyKingPrototype)formatter.Deserialize(ms);
            }
            //浅克隆
            //return (MonkeyKingPrototype)this.MemberwiseClone();
        }
    }
    [Serializable]
    class Teacher
    {
        public string Name { get; set; } 
    }
}
