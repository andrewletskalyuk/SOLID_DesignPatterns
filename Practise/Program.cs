namespace Practise
{
    public class Program
    {
        static void Main(string[] args)
        {
            // Usage
            var numbers = new int[] { 1, 2, 3, 4, 5, 6 };
            foreach (var evenNumber in GetEvenNumbers(numbers))
            {
                Console.WriteLine(evenNumber);
            }
        }

        public static IEnumerable<int> GetEvenNumbers(int[] numbers)
        {
            foreach (var number in numbers)
            {
                if (number % 2 == 0)
                {
                    yield return number;
                }
            }
        }
    }
}
