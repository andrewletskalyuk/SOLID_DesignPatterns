using System;
using System.Collections.Generic;

namespace Memento_BehavioralPattern
{
    public class Token
    {
        public int Value = 0;

        public Token(int value)
        {
            this.Value = value;
        }
    }

    public class Memento
    {
        public int MementoValue { get; }
        public Memento(int mementoValue)
        {
            MementoValue=mementoValue;
        }
    }

    public class TokenMachine
    {
        private int Value { get; set; }
        private List<Memento> changes = new List<Memento>();
        private int current;
        public Memento currentMemento { get; set; }
        public TokenMachine(int value)
        {
            Value=value;
            changes.Add(new Memento(value));
        }

        public Memento AddToken(int value)
        {
            Value = value;
            var m = new Memento(value);
            changes.Add(m);
            ++current;
            return m;
        }

        public Memento AddToken(Token token)
        {
            Value = token.Value;
            var m = new Memento(Value);
            changes.Add(m);
            ++current;
            return m;
        }

        public void Revert(Memento m)
        {
            if (m!=null)
            {
                Value = m.MementoValue;
                changes.Add(m);
                current = changes.Count - 1;
            }
        }
    }
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
        }
    }
}
