using System;
using System.Text;

namespace Facade_StructuralPattern
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var vBuilder = new StringBuilder();
            vBuilder.Append("Hello_World")
                .Replace("_", " ").Append(" Valera jgii");
            Console.WriteLine(vBuilder);
            Console.WriteLine("Hello World!");
        }
    }
}
