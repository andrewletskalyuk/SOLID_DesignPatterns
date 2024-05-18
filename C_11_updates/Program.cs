namespace C_11_updates
{
    internal class Program
    {
        static void Main(string[] args)
        {
            #region AsParallel LINQ
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
            #endregion

            #region Tuples - кортежі
            var tuple = (5, 10);
            Console.WriteLine(tuple.Item1);
            Console.WriteLine(tuple.Item2);
            #endregion

        }
    }
}