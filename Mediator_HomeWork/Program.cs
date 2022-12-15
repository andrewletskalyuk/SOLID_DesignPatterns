using System;
using System.Collections.Generic;

namespace Mediator_HomeWork
{
    public class Participant
    {
        public int Value { get; set; }
        protected Mediator mediator;

        public Participant(Mediator mediator)
        {
            this.mediator = mediator;
            mediator.Alert += Mediator_Alert;
        }

        public void Mediator_Alert(object sender, int i)
        {
            if (sender!=this)
            {
                Value += i;
            }
        }
        public void Say(int n)
        {
            if (n!=Value)
            {
                mediator.Broadcast(this, n);
                Console.WriteLine($" {n} number was changed!");
            }
        }
    }

    public class Mediator
    {
        public event EventHandler<int> Alert;
        public void Broadcast(object sender, int n)
        {
            Alert?.Invoke(sender, n);
        }
    }
    internal class Program
    {
        static void Main(string[] args)
        {
            Mediator mediator = new Mediator();
            var x = new Participant(mediator);
            var x1 = new Participant(mediator);

            x.Say(2);

            x1.Say(4);
        }
    }
}
