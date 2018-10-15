using System;

namespace CommandPattern
{
    class Program
    {
        static void Main(string[] args)
        {
            Receiver receiver = new Receiver("jello");
            Command command = new ConcreteCommand(receiver);
            Invoker invoker = new Invoker(command);
            invoker.Invoke();
            Console.ReadKey();
        }
    }

    abstract class Command
    {
        protected Receiver receiver;
        protected Command(Receiver receiver)
        {
            this.receiver = receiver;
        }

        public abstract void Execute();
    }

    class ConcreteCommand:Command
    {
        public ConcreteCommand(Receiver receiver):base(receiver)
        {
            
        }
        public override void Execute()
        {
            receiver.Action();
        }
    }

    class Invoker
    {
        private Command command;
        public Invoker(Command command)
        {
            this.command = command;
        }

        public void Invoke()
        {
            command.Execute();
        }
    }
    class Receiver
    {
        private string name;
        public Receiver(string name)
        {
            this.name = name;
        }
        public void Action()
        {
            Console.WriteLine("{0}执行命令", name);
        }
    }
}
