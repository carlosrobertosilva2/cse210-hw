using System;
using System.Collections.Generic;

class Program
{
    static void Main(string[] args)
    {
        List<int> numbers = new List<int>();

        Console.WriteLine("Enter a list of numbers, type 0 when finished.");

        int number = -1;

        while (number != 0)
        {
            Console.Write("Enter number: ");
            number = int.Parse(Console.ReadLine() ?? "0");

            if (number != 0)
            {
                numbers.Add(number);
            }
        }

        int sum = 0;

        foreach (int item in numbers)
        {
            sum += item;
        }

        double average = (double)sum / numbers.Count;

        int largestNumber = numbers[0];

        foreach (int item in numbers)
        {
            if (item > largestNumber)
            {
                largestNumber = item;
            }
        }

        int smallestPositiveNumber = int.MaxValue;

        foreach (int item in numbers)
        {
            if (item > 0 && item < smallestPositiveNumber)
            {
                smallestPositiveNumber = item;
            }
        }

        Console.WriteLine($"The sum is: {sum}");
        Console.WriteLine($"The average is: {average}");
        Console.WriteLine($"The largest number is: {largestNumber}");

        if (smallestPositiveNumber != int.MaxValue)
        {
            Console.WriteLine(
                $"The smallest positive number is: {smallestPositiveNumber}");
        }

        numbers.Sort();

        Console.WriteLine("The sorted list is:");

        foreach (int item in numbers)
        {
            Console.WriteLine(item);
        }
    }
}
