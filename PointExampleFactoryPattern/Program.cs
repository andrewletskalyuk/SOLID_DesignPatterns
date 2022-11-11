using System;
using static System.Console;

namespace PointExampleFactoryPattern
{
    //public static class Factory
    //{
    //    public static Point NewCartesianPoint(double x, double y)
    //    {
    //        return new Point(x, y);
    //    }

    //    public static Point NewPolarPoint(double rho, double theta)
    //    {
    //        return new Point(rho*Math.Cos(theta), rho*Math.Sin(theta));
    //    }
    //}


    public class Point
    {

        //factory method

        private double x, y;
        
        //if we want to close this constructor from outside
        //and using it as a library - we just do our constructor
        //internal
        //public Point(double x, double y)
        private Point(double x, double y)
        {
            this.x = x;
            this.y = y;
        }

        public override string ToString()
        {
            return $"{nameof(x)}: {x}, {nameof(y)}: {y}";
        }

        //public static Point origin => new Point(0,0);
        public static Point origin2 = new Point(0,0); //it's better
        public static class Factory
        {
            public static Point NewCartesianPoint(double x, double y)
            {
                return new Point(x, y);
            }

            public static Point NewPolarPoint(double rho, double theta)
            {
                return new Point(rho * Math.Cos(theta), rho * Math.Sin(theta));
            }
        }
    }
    internal class Program
    {
        static void Main(string[] args)
        {
            var point = Point.Factory.NewPolarPoint(1.0, Math.PI / 2);
            WriteLine(point);
            var point1 = Point.Factory.NewCartesianPoint(1.0, 5.0);
            WriteLine(point1);
        }
    }
}
