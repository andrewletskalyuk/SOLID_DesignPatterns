using NUnit.Framework;
using System;
using System.Collections.Generic;
    using static System.Console;

namespace Coding.Exercise
{
    // See https://aka.ms/new-console-template for more information
    public class Command
    {
        public enum Action
        {
            Deposit,
            Withdraw
        }

        public Action TheAction;
        public int Amount;
        public bool Success;
    }

    public class Account
    {
        public int Balance { get; set; }

        public void Process(Command c)
        {
            switch (c.TheAction)
            {
                case Command.Action.Deposit:
                    Balance += c.Amount;
                    c.Success = true;
                    break;
                case Command.Action.Withdraw:
                    c.Success = Balance >= c.Amount;
                    if (c.Success) Balance -= c.Amount;
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
    }

    [TestFixture]
    public class TestSuite
    {
        [Test]
        public void Test()
        {
            var a = new Account();

            var command = new Command { Amount = 100, TheAction = Command.Action.Deposit };
            a.Process(command);

            Assert.That(a.Balance, Is.EqualTo(100));
            Assert.IsTrue(command.Success);

            command = new Command { Amount = 50, TheAction = Command.Action.Withdraw };
            a.Process(command);

            Assert.That(a.Balance, Is.EqualTo(50));
            Assert.IsTrue(command.Success);

            command = new Command { Amount = 150, TheAction = Command.Action.Withdraw };
            a.Process(command);

            Assert.That(a.Balance, Is.EqualTo(50));
            Assert.IsFalse(command.Success);
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            
        }
    }
}
