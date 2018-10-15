using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StatePattern
{
    class Program
    {
        static void Main(string[] args)
        {
            Account account = new Account("jello");
            account.Deposit(1000.00);
            account.Deposit(200.00);
            account.Deposit(600.00);
            account.PayInterest();
            account.Withdraw(2000.00);
            account.Withdraw(500.00);
            Console.ReadKey();
        }
    }

    class Account
    {
        public State State { get; set; }
        public string Owner { get; set; }
        public double Balance { get { return State.Balance; } }

        public Account(string owner)
        {
            this.Owner = owner;
            State = new SilverState(0.00, this);
        }

        //存钱
        public void Deposit(double amount)
        {
            State.Deposit(amount);
            Console.WriteLine("存款金额为 {0:C}——", amount);
            Console.WriteLine("账户余额为 =:{0:C}", this.Balance);
            Console.WriteLine("账户状态为: {0}", this.State.GetType().Name);
            Console.WriteLine();
        }

        // 取钱
        public void Withdraw(double amount)
        {
            State.Withdraw(amount);
            Console.WriteLine("取款金额为 {0:C}——", amount);
            Console.WriteLine("账户余额为 =:{0:C}", this.Balance);
            Console.WriteLine("账户状态为: {0}", this.State.GetType().Name);
            Console.WriteLine();
        }

        // 获得利息
        public void PayInterest()
        {
            State.PayInterest();
            Console.WriteLine("Interest Paid --- ");
            Console.WriteLine("账户余额为 =:{0:C}", this.Balance);
            Console.WriteLine("账户状态为: {0}", this.State.GetType().Name);
            Console.WriteLine();
        }
    }

    abstract class State
    {
        public Account Account { get; set; }
        public double Balance { get; set; }
        public double Interest { get; set; }
        public double LowerLimit { get; set; }
        public double UpperLimit { get; set; }

        public abstract void Deposit(double amount);
        public abstract void Withdraw(double amount);
        public abstract void PayInterest();
    }
    //透支状态
    class RedState : State
    {
        public RedState(State state)
        {
            this.Account = state.Account;
            this.Balance = state.Balance;
            Interest = 0.00;
            LowerLimit = -100.00;
            UpperLimit = 0.00;
        }
        public override void Deposit(double amount)
        {
            Balance += amount;
            StateChangeCheck();
        }

        public override void Withdraw(double amount)
        {
            Console.WriteLine("没有钱可以取了！");
        }

        public override void PayInterest()
        {

        }

        private void StateChangeCheck()
        {
            if (Balance > UpperLimit)
            {
                Account.State = new SilverState(this);//下个状态
            }
        }
    }
    /// <summary>
    /// 没有利息
    /// </summary>
    class SilverState : State
    {
        public SilverState(State state)
            : this(state.Balance, state.Account)
        {

        }

        public SilverState(double balance, Account account)
        {
            Balance = balance;
            Account = account;
            Interest = 0.00;
            LowerLimit = 0.00;
            UpperLimit = 1000.00;
        }

        public override void Deposit(double amount)
        {
            Balance += amount;
            StateChangeCheck();
        }

        public override void Withdraw(double amount)
        {
            Balance -= amount;
            StateChangeCheck();
        }

        public override void PayInterest()
        {

        }

        private void StateChangeCheck()
        {
            if (Balance <= LowerLimit)
            {
                Account.State = new RedState(this);
            }
            else if (Balance > UpperLimit)
            {
                Account.State = new GoldState(this);
            }
        }
    }
    /// <summary>
    /// 有利息状态
    /// </summary>
    class GoldState : State
    {
        public GoldState(State state)
        {
            Balance = state.Balance;
            Account = state.Account;
            Interest = 0.05;
            LowerLimit = 1000.00;
            UpperLimit = 1000000.00;
        }
        public override void Deposit(double amount)
        {
            Balance += amount;
            StateChangeCheck();
        }

        public override void Withdraw(double amount)
        {
            Balance -= amount;
            StateChangeCheck();
        }

        public override void PayInterest()
        {
            Balance += Balance * Interest;
            StateChangeCheck();
        }

        private void StateChangeCheck()
        {
            if (Balance <= 0.00)
            {
                Account.State = new RedState(this);
            }
            else if (Balance <= LowerLimit)
            {
                Account.State = new SilverState(this);
            }
        }
    }
}
