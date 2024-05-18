namespace AsyncAwaitPoo;

public class Program
{
    static void Main(string[] args)
    {

        // Usage
        int[] data = { 1, 2, 3, 4, 5, 6,7,8,9,10,11,12,13,14,15,16,17,18,19,20 };
        ProcessDataInParallel(data);
        foreach (var item in data)
        {
            Console.WriteLine(item);
        }

    }
    public static void ProcessDataInParallel(int[] data)
    {
        Parallel.For(0, data.Length, i =>
        {
            data[i] = PerformComplexCalculation(data[i]);
        });
    }

    public static int PerformComplexCalculation(int input)
    {
        // Simulate a complex calculation
        return input * input;
    }
}
