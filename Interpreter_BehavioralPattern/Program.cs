using System;
using System.Linq;

namespace Interpreter_BehavioralPattern
{
    internal class Program
    {
        static void Main(string[] args)
        {
            int Square(int x) => x * x;
            int[] numbers = { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 };

            Console.WriteLine("It's parallel select in linq");
            numbers.AsParallel().Select(x => Square(x)).ForAll(Console.WriteLine);

            Console.WriteLine("It's simple select in linq");
            var squares1 = numbers.Select(x => Square(x));

            foreach (var item in squares1)
            { 
                Console.WriteLine(item); 
            }
            Console.WriteLine("Hello World!");

            
        }
    }
}
