﻿using System;
using System.Collections.Generic;
using System.Linq;
using static System.Console;

namespace FunctionalBuilder
{
    public class Person
    {
        public string Name, Position;
    }

    public abstract class FunctionalBuilder<TSubject, TSelf>
        where TSelf: FunctionalBuilder<TSubject, TSelf>
        where TSubject: new()
    {
        private readonly List<Func<Person, Person>> _actions
            = new List<Func<Person, Person>>();

        public TSelf Do(Action<Person> action)
            => AddAction(action);

        public Person Build()
            => _actions.Aggregate(new Person(), (p, f) => f(p));

        private TSelf AddAction(Action<Person> action)
        {
            _actions.Add(p =>
            {
                action(p);
                return p;
            });
            return (TSelf)this;
        }
    }

    public sealed class PersonBuilder : FunctionalBuilder<Person, PersonBuilder>
    {
        public PersonBuilder Called(string name)=>
            Do(p => p.Name = name);
    }
    //public sealed class PersonBuilder
    //{
    //    private readonly List<Func<Person,Person>> _actions
    //    = new List<Func<Person,Person>>();

    //    public PersonBuilder Called(string name)
    //        => Do(p => p.Name = name);

    //    public PersonBuilder Do(Action<Person> action)
    //        => AddAction(action);

    //    public Person Build()
    //        => _actions.Aggregate(new Person(), (p, f) => f(p));

    //    private PersonBuilder AddAction(Action<Person> action)
    //    {
    //        _actions.Add(p =>
    //        {
    //            action(p);
    //            return p;
    //        });
    //        return this;
    //    }
    //}

    public static class PersonBuilderExtentions
    {
        public static PersonBuilder WorksAs(
            this PersonBuilder builder, string position)
            => builder.Do(p => p.Position = position);
    }

    internal class Program
    {
        static void Main(string[] args)
        {
            var person = new PersonBuilder()
                .Called("Sarah")
                .WorksAs("Developer")
                .Build();
            WriteLine("Hello World!");
        }
    }
}
