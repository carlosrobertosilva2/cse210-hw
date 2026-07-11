using System;

class Program
{
    static void Main(string[] args)
    {
        // Declare variables to hold user information
        string name;
        string mailingAddress;
        string phoneNumber;

        // Prompt for and read input
        Console.Write("Please enter your name:");
        name = Console.ReadLine();

        Console.Write("Please enter your mainling Address:");
        mailingAddress = Console.ReadLine();

        Console.Write("Please enter your phone number:");
        phoneNumber = Console.ReadLine();

        // Display the collected information
        Console.WriteLine();
        Console.WriteLine($"Name: {name}");
        Console.WriteLine($"Mailing Address: {mailingAddress}");
        Console.WriteLine($"Phone Number: {phoneNumber}");
    }
}
