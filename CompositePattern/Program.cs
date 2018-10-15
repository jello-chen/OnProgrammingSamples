//透明模式
//#define TRANSPARENT_MODE
using System;
using System.Collections.Generic;

//透明模式
#if TRANSPARENT_MODE
namespace CompositePattern
{
    class Program
    {
        static void Main(string[] args)
        {
            Component txtFile = new File("1.txt");
            Component txtFile2 = new File("2.txt");
            Component wordFile = new File("1.doc");
            Component pdfFile = new File("1.pdf");
            Component fold1 = new Folder("txt");
            Component fold2 = new Folder("other");
            fold1.Add(txtFile);
            fold1.Add(txtFile2);
            fold2.Add(wordFile);
            fold2.Add(pdfFile);
            fold1.Add(fold2);
            fold1.Display(1);
            Console.ReadKey();
        }
    }

    abstract class Component
    {
        protected string name;
        protected Component(string name)
        {
            this.name = name;
        }

        public abstract void Add(Component component);
        public abstract void Remove(Component component);
        public abstract void Display(int depth);
    }

    class File : Component
    {
        public File(string name)
            : base(name)
        {

        }

        public override void Add(Component component)
        {
            throw new NotSupportedException("File can't support the operation!");
        }

        public override void Remove(Component component)
        {
            throw new NotSupportedException("File can't support the operation!");
        }

        public override void Display(int depth)
        {
            Console.WriteLine(new string('-', depth) + name);
        }
    }

    class Folder : Component
    {
        public List<Component> components = new List<Component>();
        public Folder(string name)
            : base(name)
        {

        }
        public override void Add(Component component)
        {
            if (!components.Contains(component))
                components.Add(component);
        }

        public override void Remove(Component component)
        {
            components.Remove(component);
        }

        public override void Display(int depth)
        {
            Console.WriteLine(new string('-', depth) + name);
            foreach (var component in components)
            {
                component.Display(depth + 2);
            }
        }
    }
}
#else
//安全模式
namespace CompositePattern
{
    class Program
    {
        static void Main(string[] args)
        {
            Component txtFile = new File("1.txt");
            Component txtFile2 = new File("2.txt");
            Component wordFile = new File("1.doc");
            Component pdfFile = new File("1.pdf");
            Folder fold1 = new Folder("txt");
            Folder fold2 = new Folder("other");
            fold1.Add(txtFile);
            fold1.Add(txtFile2);
            fold2.Add(wordFile);
            fold2.Add(pdfFile);
            fold1.Add(fold2);
            fold1.Display(1);
            Console.WriteLine("-----删除1.txt-----");
            fold1.Remove(txtFile);
            fold1.Display(1);
            Console.WriteLine("-----删除other文件夹-----");
            fold1.Remove(fold2);
            fold1.Display(1);
            Console.ReadKey();
        }
    }

    abstract class Component
    {
        protected string Name { get; set; }
        protected Component(String name)
        {
            this.Name = name;
        }

        public abstract void Display(int depth);
    }

    class File : Component
    {
        public File(string name)
            : base(name) { }
        public override void Display(int depth)
        {
            Console.WriteLine(new string('-', depth) + Name);
        }
    }

    class Folder : Component
    {
        public List<Component> components = new List<Component>(); 
        public Folder(string name)
            : base(name)
        {

        }

        public void Add(Component component)
        {
            if (!components.Contains(component))
                components.Add(component);
        }

        public void Remove(Component component)
        {
            components.Remove(component);
        }

        public override void Display(int depth)
        {
            Console.WriteLine(new string('-', depth) + Name);
            foreach (var component in components)
            {
                component.Display(depth + 2);
            }
        }
    }
}
#endif