using System;

class Program
{
    static void Main(string[] args)
    {
        Console.WriteLine("Hello World! This is the Exercise4 Project.");

        List<int> numbers = new List<int>();
        Console.WriteLine("Enter a list of numbers, type 0 when finished.");

        while (true)
        {
            Console.Write("Enter number: ");
            int number = int.Parse(Console.ReadLine());

            if (number == 0)
                break;

            numbers.Add(number);
        }

        if (numbers.Count > 0)
        {
            int sum = numbers.Sum();
            double average = numbers.Average();
            int max = numbers.Max();
            int? smallestPositive = numbers.Where(n => n > 0).DefaultIfEmpty().Min();
            numbers.Sort();

            Console.WriteLine($"The sum is: {sum}");
            Console.WriteLine($"The average is: {average}");
            Console.WriteLine($"The largest number is: {max}");

            if (smallestPositive.HasValue && smallestPositive > 0)
                Console.WriteLine($"The smallest positive number is: {smallestPositive}");

            Console.WriteLine("The sorted list is:");
            foreach (int num in numbers)
                Console.WriteLine(num);
        }
        else
        {
            Console.WriteLine("No numbers were entered.");
        }
    }
}