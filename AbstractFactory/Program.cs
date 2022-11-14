using System;
using System.Collections.Generic;
using static System.Console;

namespace AbstractFactory
{
    public interface IHotDrink
    {
        void Consume();
    }

    internal class Tea : IHotDrink
    {
        public void Consume()
        {
            WriteLine("This tea is nice but I'd prefer it with milk");
        }
    }

    internal class Coffee : IHotDrink
    {
        public void Consume()
        {
            WriteLine("This coffee is sensational!");
        }
    }

    public interface IHotDrinkFactory
    {
        IHotDrink Prepare(int amount);
    }

    internal class TeaFactory : IHotDrinkFactory
    {
        public IHotDrink Prepare(int amount)
        {
            WriteLine($"Put in tea bag, boil water, pour {amount} ml, add lemon, enjoy!");
            return new Tea();
        }
    }

    internal class CoffeeFactory : IHotDrinkFactory
    {
        public IHotDrink Prepare(int amount)
        {
            WriteLine($"Grind some beans, boil water, pour {amount} ml, add cream and sugar, enjoy!");
            return new Coffee();
        }
    }

    public class HotDrinkMachine
    {
        /// <summary>
        /// ця частина буде переписана щоб не порушувати принцип open-closed
        /// </summary>
        //public enum AvailableDrink // порушує принцип open-closed
        //{
        //    Coffee, Tea
        //}

        //private protected Dictionary<AvailableDrink, IHotDrinkFactory> Factories
        //    = new Dictionary<AvailableDrink, IHotDrinkFactory>();

        //public HotDrinkMachine()
        //{
        //    foreach (AvailableDrink drink in Enum.GetValues(typeof(AvailableDrink)))
        //    {
        //        var factory = (IHotDrinkFactory)Activator.CreateInstance(
        //            Type.GetType("AbstractFactory." + Enum.GetName(typeof(AvailableDrink), drink) + "Factory")
        //        );
        //        Factories.Add(drink, factory);
        //    }
        //}

        //public IHotDrink MakeDrink(AvailableDrink drink, int amount)
        //{
        //    return Factories[drink].Prepare(amount);
        //}
        
        
        private List<Tuple<string, IHotDrinkFactory>> _factories = 
            new List<Tuple<string, IHotDrinkFactory>>();
        public HotDrinkMachine()
        {
            foreach (var t in typeof(HotDrinkMachine).Assembly.GetTypes())
            {
                if (typeof(IHotDrinkFactory).IsAssignableFrom(t) 
                    && !t.IsInterface)
                {
                    _factories.Add(Tuple.Create(
                        t.Name.Replace("Factory", string.Empty),
                        (IHotDrinkFactory)Activator.CreateInstance(t)
                        ));
                }
            }    
        }

        public IHotDrink MakeDrink()
        {
            WriteLine("Available drinks:");
            for (int index = 0; index < _factories.Count; index++)
            {
                var tuple = _factories[index];
                WriteLine($"{index}: {tuple.Item1}");
            }

            while (true)
            {
                string s;
                if ((s = ReadLine())!=null && int.TryParse(s, out int i)
                    && i>=0
                    && i < _factories.Count)
                {
                    Write("Specify amount: ");
                    s = ReadLine();
                    if (s!= null && int.TryParse(s, out int amount) && amount>0)
                    {
                        return _factories[i].Item2.Prepare(amount);
                    }
                }
            }
        }
    }

    public class One
    {
        public string _name;
    }

    public class Two
    {
        public string _name;
    }

    internal class Program
    {
        static void Main(string[] args)
        {
            //var machine = new HotDrinkMachine();
            //var drink = machine.MakeDrink(HotDrinkMachine.AvailableDrink.Tea, 100);
            //drink.Consume();

            var machine = new HotDrinkMachine();
            var drink = machine.MakeDrink();
            WriteLine(drink);
            //
            (One, Two) t = (new One{_name = "title"}, new Two{_name = "title"});

        }
    }
}
