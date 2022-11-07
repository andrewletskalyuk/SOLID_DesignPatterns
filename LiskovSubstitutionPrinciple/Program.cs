using System;
using static System.Console;

namespace LiskovSubstitutionPrinciple
{
    public class Rectangle
    {
        public virtual int Width { get; set; }
        public virtual int Height { get; set; }
        public Rectangle()
        {

        }
        public Rectangle(int width, int height)
        {
            Width = width;
            Height = height;
        }
        public override string ToString()
        {
            return $"{nameof(Width)}: {Width}, {nameof(Height)}, {Height}";
        }
    }

    public class Square : Rectangle
    {
        public override int Width
        {
            set
            {
                this.Width = Width = value;
            }
        }
        public override int Height
        {
            set
            {
                this.Width = Height = value;
            }
        }
    }

    public class Program
    {
        public static int Area(Rectangle r) => r.Width * r.Height;
        static void Main(string[] args)
        {
            Rectangle rc = new Rectangle(90, 3);
            WriteLine($"{rc} has area {Area(rc)}");
        }
    }
}
