using System.Collections.Generic;
using static System.Console;

namespace HomeWorkFactory
{
    public interface IPerson
    {
        void Create();
    }

    public class Person : IPerson
    {
        public int Id = 0;
        public string Name { get; set; }
        public void Create()
        {
            WriteLine($"Person {Name} was created");
        }

        public override string ToString()
        {
            return $"{nameof(Name)}: {Name}, {nameof(Id)}: {Id}";
        }
    }

    public interface IPersonFactory
    {
        IPerson Prepare(string name);
    }
    public class PersonFactory : IPersonFactory
    {
        public List<Person> _persons = new List<Person>();
        public int id = 0;
        public IPerson Prepare(string name)
        {
            if (!string.IsNullOrEmpty(name))
            {
                var person = new Person { Id = id++, Name = name };
                _persons.Add(person);
                WriteLine($"Person with name: {name} was prepared!!!");
                return person;
            }
            return _persons[id];
        }

        public override string ToString()
        {
            if (_persons.Count != 0)
            {
                foreach (var person in _persons)
                {
                    person.ToString();
                }
            }
            return null;
        }
    }

    internal class Program
    {
        static void Main(string[] args)
        {
            var factory = new PersonFactory();
            factory.Prepare("Andrew").Create();
            factory.Prepare("Ivan").Create();
            foreach (var person in factory._persons)
            {
                WriteLine(person.ToString());
            }
        }
    }
}
