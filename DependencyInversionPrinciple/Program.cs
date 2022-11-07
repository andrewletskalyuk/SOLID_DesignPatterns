using System.Collections.Generic;
using System.Linq;
using static System.Console;

namespace DependencyInversionPrinciple
{
    public enum Relationship
    {
        Parent,
        Child,
        Sibling
    }

    public class Person
    {
        public string Name;
        //public DateTime DateOfBirth;
    }

    public interface IRelationshipBrowser
    {
        IEnumerable<Person> FindAllChildrenOf(string name);
    }


    //low-level
    public class Relationships : IRelationshipBrowser
    {
        private List<(Person, Relationship, Person)> relations
            = new List<(Person, Relationship, Person)>();

        public void AddParentAndChild(Person parent, Person child)
        {
            relations.Add((parent, Relationship.Parent, child));
            relations.Add((child, Relationship.Child, parent));
        }

        public IEnumerable<Person> FindAllChildrenOf(string name)
        {
            return relations.Where(
                x => x.Item1.Name == name &&
                     x.Item2 == Relationship.Parent
                ).Select(e => e.Item3);
        }
    }

    public class Research
    {
        public Research(IRelationshipBrowser browser, string name)
        {
            foreach (var p in browser.FindAllChildrenOf(name))
            {
                WriteLine($"{name} has a child(s) called {p.Name}");
            }
        }
        static void Main(string[] args)
        {
            var parent = new Person { Name = "Ivan" };
            var child1 = new Person { Name = "Andrew" };
            var child2 = new Person { Name = "Lilia" };

            var relationships = new Relationships();
            relationships.AddParentAndChild(parent, child1);
            relationships.AddParentAndChild(parent, child2);
        }
    }
}

