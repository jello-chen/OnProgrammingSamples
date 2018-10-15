using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace MementoPattern
{
    class Program
    {
        static void Main(string[] args)
        {
            List<ContactPerson> persons = new List<ContactPerson>()
            {
                new ContactPerson() { Name= "Learning Hard", MobileNum = "123445"},
                new ContactPerson() { Name = "Tony", MobileNum = "234565"},
                new ContactPerson() { Name = "Jock", MobileNum = "231455"}
            };
            MobileOwner mobileOwner = new MobileOwner(persons);
            mobileOwner.Show();

            // 创建备忘录并保存备忘录对象
            Caretaker caretaker = new Caretaker();
            caretaker.ContactPersonDictionary.Add(DateTime.Now.ToString(), mobileOwner.CreateContactMemento());

            // 更改发起人联系人列表
            Console.WriteLine("----移除最后一个联系人--------");
            mobileOwner.ContactPersons.RemoveAt(2);
            mobileOwner.Show();

            Thread.Sleep(1000);
            caretaker.ContactPersonDictionary.Add(DateTime.Now.ToString(), mobileOwner.CreateContactMemento());
            int count = caretaker.ContactPersonDictionary.Count;
            for (int i = 0; i < count; i++)
            {
                Console.WriteLine("{0} {1}", i + 1, caretaker.ContactPersonDictionary.Keys.ElementAt(i));
            }
            while (true)
            {
                Console.Write("请输入数字,按窗口的关闭键退出:");

                int index = -1;
                try
                {
                    index = Int32.Parse(Console.ReadLine());
                }
                catch
                {
                    Console.WriteLine("输入的格式错误");
                    continue;
                }

                ContactMemento contactMentor = null;
                if (index < count)
                {
                    contactMentor = caretaker.ContactPersonDictionary.Values.ElementAt(index);
                    mobileOwner.RestoreMemento(contactMentor);
                    mobileOwner.Show();
                }
                else
                {
                    Console.WriteLine("输入的索引大于集合长度！");
                }
            }
            Console.Read();
        }
    }

    /// <summary>
    /// 联系人
    /// </summary>
    [Serializable]
    public class ContactPerson
    {
        public string Name { get; set; }
        public string MobileNum { get; set; }
    }
    /// <summary>
    /// 备忘录
    /// </summary>
    public class ContactMemento
    {
        // 保存发起人的内部状态
        public List<ContactPerson> contactPersonBack;

        public ContactMemento(List<ContactPerson> persons)
        {
            contactPersonBack = persons;
        }
    }

    /// <summary>
    ///  管理角色
    /// </summary>
    public class Caretaker
    {
        public Dictionary<string,ContactMemento> ContactPersonDictionary { get; set; }

        public Caretaker()
        {
            ContactPersonDictionary = new Dictionary<string, ContactMemento>();
        }
    }

    /// <summary>
    /// 发起人
    /// </summary>
    public class MobileOwner
    {
        //发起人需要保存的内部状态
        public List<ContactPerson> ContactPersons { get; set; }

        public MobileOwner(List<ContactPerson> contactPersons)
        {
            ContactPersons = contactPersons;
        }
        
        /// <summary>
        /// 创建备忘录，将当前要保存的联系人列表倒入到备忘录中
        /// </summary>
        /// <returns></returns>
        public ContactMemento CreateContactMemento()
        {
            //传递深拷贝，new list本身是浅拷贝，但ContactPerson成员都是string类型，浅拷贝和深拷贝一样
            //return new ContactMemento(new List<ContactPerson>(ContactPersons));

            var contactPersons = Serializer.Deserialize<List<ContactPerson>>(Serializer.Serialize(ContactPersons));
            return new ContactMemento(contactPersons);
        }

        public void RestoreMemento(ContactMemento memento)
        {
            //this.ContactPersons = memento.contactPersonBack;
            var contactPersons = Serializer.Deserialize<List<ContactPerson>>(Serializer.Serialize(memento.contactPersonBack));
            this.ContactPersons = contactPersons;
        }

        public void Show()
        {
            Console.WriteLine("联系人列表中有{0}个人，他们是:", ContactPersons.Count);
            foreach (ContactPerson p in ContactPersons)
            {
                Console.WriteLine("姓名: {0} 号码为: {1}", p.Name, p.MobileNum);
            }
        }
    }

    class Serializer
    {
        public static MemoryStream Serialize<T>(T t)
        {
            MemoryStream ms = new MemoryStream();
            BinaryFormatter formatter = new BinaryFormatter();
            formatter.Serialize(ms, t);
            ms.Seek(0, SeekOrigin.Begin);
            return ms;
        }

        public static T Deserialize<T>(MemoryStream ms)
        {
            BinaryFormatter formatter = new BinaryFormatter();
            return (T) formatter.Deserialize(ms);
        }
    }

}
