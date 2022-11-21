using Autofac;
using System;
using static System.Console;

namespace SingletonImplementation_Pattern
{
    class Program
    {
        public class Foo
        {
            public EventBroker Broker;

            public Foo(EventBroker broker)
            {
                Broker = broker ?? throw new ArgumentNullException(paramName: nameof(broker));
            }
        }

        public class EventBroker
        {

        }
        static void Main(string[] args)
        {
            var builder = new ContainerBuilder();
            builder.RegisterType<EventBroker>().SingleInstance();
            builder.RegisterType<Foo>();

            using (var c = builder.Build())
            {
                var foo1 = c.Resolve<Foo>();
                var foo2 = c.Resolve<Foo>();

                WriteLine(ReferenceEquals(foo1, foo2));
                WriteLine(ReferenceEquals(foo1.Broker, foo2.Broker));
            }
        }
    }
}
