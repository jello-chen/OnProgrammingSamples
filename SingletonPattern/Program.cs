using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SingletonPattern
{
    class Program
    {
        static void Main(string[] args)
        {
            Singleton.GetInstance().Call();
            Console.ReadKey();
        }
    }

    class Singleton
    {
        private static Singleton _instance;
        private Singleton()
        {
            
        }

        public static Singleton GetInstance()
        {
            if(_instance == null)
                _instance = new Singleton();
            return _instance;
        }

        public void Call()
        {
            Console.WriteLine("Call");
        }
    }
}
