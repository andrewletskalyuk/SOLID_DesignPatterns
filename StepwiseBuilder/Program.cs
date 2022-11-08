using System;
using static System.Console;

namespace StepwiseBuilder
{
    public enum CarType
    {
        Sedan,
        Crossover
    }

    public class Car
    {
        public CarType Type;
        public int WheelSize;
    }

    public interface ISpecifyCarType
    {
        ISpecifyWheelSize OfType(CarType type);
    }

    public interface ISpecifyWheelSize
    {
        IBuildCar WithWheels(int size);
    }

    public interface IBuildCar
    {
        public Car Build();
    }

    public class CarBuilder
    {
        private class Impl : 
            ISpecifyCarType, 
            ISpecifyWheelSize, 
            IBuildCar
        {
            private readonly Car _car = new Car();
            public ISpecifyWheelSize OfType(CarType type)
            {
                _car.Type = type;
                return this;
            }

            public IBuildCar WithWheels(int size)
            {
                switch (_car.Type)
                {
                    case CarType.Crossover when size < 17 || size > 20:
                    case CarType.Sedan when size < 15 || size > 17:
                        throw new ArgumentException($"Wrong size of wheel for {_car.Type}");
                }
                return this;
            }

            public Car Build()
            {
                return _car;
            }
        }
        public static ISpecifyCarType Create()
        {
            return new Impl();
        }
    }
    public class Program
    {
        static void Main(string[] args)
        {
            var car = CarBuilder.Create()
                .OfType(CarType.Crossover)
                .WithWheels(18)
                .Build();
            WriteLine(car);
            WriteLine("Hello World!");
        }
    }
}
