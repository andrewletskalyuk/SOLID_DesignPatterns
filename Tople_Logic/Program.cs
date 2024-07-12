namespace Tople_Logic;

public class Program
{
    static void Main(string[] args)
    {
        // Example list of integers
        List<int> numbers = new List<int> { 1 };

        // Example sum to check
        int sum = 9;

        var result = ContainsConsecutiveSum(numbers, sum);

        if (result != null)
        {
            Console.WriteLine($"The list contains two consecutive elements at indices {result.Item1} and {result.Item2} that sum to {sum}.");
        }
        else
        {
            Console.WriteLine($"The list does not contain two consecutive elements that sum to {sum}.");
        }
        
        
        static Tuple<int, int>? ContainsConsecutiveSum(List<int> numbers, int sum)
        {
            for (int i = 0; i < numbers.Count - 1; i++)
            {
                if (numbers[i] + numbers[i + 1] == sum)
                {
                    return Tuple.Create(i, i + 1);
                }
            }
            return null;
        }
    }
}
