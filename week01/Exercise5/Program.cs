using System;
using System.Collections.Generic;

class Program
{
    static void Main(string[] args)
    {
        List<int> numbers = CollectNumbers();

        if (numbers.Count == 0)
        {
            Console.WriteLine("No numbers were entered.");
            return;
        }

        Console.WriteLine($"\nSum: {ComputeSum(numbers)}");
        Console.WriteLine($"Average: {ComputeAverage(numbers):F2}");
        Console.WriteLine($"Largest: {FindLargest(numbers)}");
        Console.WriteLine($"Smallest above average: {FindSmallestAboveAverage(numbers)}");
    }

    // Prompts the user to enter numbers until 0 is entered
    static List<int> CollectNumbers()
    {
        Console.WriteLine("Enter numbers one at a time. Type 0 when you are done.");
        List<int> numbers = new List<int>();

        while (true)
        {
            Console.Write("Enter number: ");
            int number = int.Parse(Console.ReadLine());

            if (number == 0)
            {
                break;
            }

            numbers.Add(number);
        }

        return numbers;
    }

    static int ComputeSum(List<int> numbers)
    {
        int sum = 0;
        foreach (int n in numbers)
        {
            sum += n;
        }
        return sum;
    }

    static double ComputeAverage(List<int> numbers)
    {
        return (double)ComputeSum(numbers) / numbers.Count;
    }

    static int FindLargest(List<int> numbers)
    {
        int largest = numbers[0];
        foreach (int n in numbers)
        {
            if (n > largest)
            {
                largest = n;
            }
        }
        return largest;
    }

    static int FindSmallestAboveAverage(List<int> numbers)
    {
        double average = ComputeAverage(numbers);
        int smallest = int.MaxValue;

        foreach (int n in numbers)
        {
            if (n > average && n < smallest)
            {
                smallest = n;
            }
        }

        return smallest == int.MaxValue ? -1 : smallest;
    }
}
