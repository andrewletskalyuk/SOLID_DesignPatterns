﻿using System;
using static System.Console;

namespace Monostate_SingletonPattern
{
    class Program
    {
        public class ChiefExecutiveOfficer
        {
            private static string name;
            private static int age;

            public string Name
            {
                get => name;
                set => name = value;
            }

            public int Age
            {
                get => age;
                set => age = value;
            }

            public override string ToString()
            {
                return $"{nameof(Name)}: {Name}, {nameof(Age)}: {Age}";
            }
        }

        static void Main(string[] args)
        {
            var ceo = new ChiefExecutiveOfficer();
            ceo.Name = "Adam Smith";
            ceo.Age = 55;

            var ceo2 = new ChiefExecutiveOfficer();
            WriteLine(ceo2);
        }
    }
}
