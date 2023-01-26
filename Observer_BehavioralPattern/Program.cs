using System;

namespace Observer_BehavioralPattern
{
    public class FallsIllEventArgs
    {
        public string Address;
    }

    public class Person
    {
        public void CatchACold()
        {
            FallsIll?.Invoke(this,
              new FallsIllEventArgs { Address = "123 London Road" });
        }

        public event EventHandler<FallsIllEventArgs> FallsIll;
    }
    public class Program
    {
        static void Main(string[] args)
        {
            var person = new Person();

            person.FallsIll += CallDoctor; //we're subscribed to CallDoctor

            person.CatchACold(); //in case you are ill, you need to call a doctor.

            //Console.WriteLine("Hello World!");
        }
        private static void CallDoctor(object sender, FallsIllEventArgs eventArgs)
        {
            Console.WriteLine($"A doctor has been called to {eventArgs.Address}");
        }
    }
}
