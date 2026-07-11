using System;

class Program
{
    static void Main(string[] args)
    {
        Console.WriteLine("Enter numbers one at a time. Type 0 when you are done.");

        int sum = 0;
        int count = 0;
        int largest = int.MinValue;

        while (true)
        {
            Console.Write("Enter number: ");
            int number = int.Parse(Console.ReadLine());

            if (number == 0)
            {
                break;
            }

            sum += number;
            count++;

            if (number > largest)
            {
                largest = number;
            }
        }

        if (count > 0)
        {
            double average = (double)sum / count;
            Console.WriteLine($"Sum: {sum}");
            Console.WriteLine($"Average: {average:F2}");
            Console.WriteLine($"Largest number: {largest}");
        }
        else
        {
            Console.WriteLine("No numbers were entered.");
        }
    }
}
