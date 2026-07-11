using System;
using System.Collections.Generic;

class Program
{
    static void Main(string[] args)
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

        if (numbers.Count == 0)
        {
            Console.WriteLine("No numbers were entered.");
            return;
        }

        // Compute sum
        int sum = 0;
        foreach (int n in numbers)
        {
            sum += n;
        }

        double average = (double)sum / numbers.Count;

        // Find largest
        int largest = numbers[0];
        foreach (int n in numbers)
        {
            if (n > largest)
            {
                largest = n;
            }
        }

        // Find smallest number greater than average
        int? smallestAboveAverage = null;
        foreach (int n in numbers)
        {
            if (n > average)
            {
                if (smallestAboveAverage == null || n < smallestAboveAverage)
                {
                    smallestAboveAverage = n;
                }
            }
        }

        // Sort and display list
        numbers.Sort();
        Console.WriteLine("\nSorted list:");
        foreach (int n in numbers)
        {
            Console.WriteLine($"  {n}");
        }

        Console.WriteLine($"\nSum: {sum}");
        Console.WriteLine($"Average: {average:F2}");
        Console.WriteLine($"Largest: {largest}");

        if (smallestAboveAverage != null)
        {
            Console.WriteLine($"Smallest number greater than average: {smallestAboveAverage}");
        }
        else
        {
            Console.WriteLine("No numbers are greater than the average.");
        }
    }
}
