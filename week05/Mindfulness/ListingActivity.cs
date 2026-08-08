using System;
using System.Collections.Generic;

public class ListingActivity : Activity
{
    private readonly List<string> _prompts = new List<string>
    {
        "Who are people that you appreciate?",
        "What are personal strengths of yours?",
        "Who are people that you have helped this week?",
        "When have you felt the Holy Ghost this month?",
        "Who are some of your personal heroes?"
    };

    public ListingActivity()
        : base(
            "Listing Activity",
            "This activity will help you reflect on the good things in your life by having you list as many things as you can in a certain area.")
    {
    }

    public override void Run()
    {
        DisplayStartingMessage();

        string prompt = Shuffle(_prompts)[0];
        Console.WriteLine("List as many responses as you can to the following prompt:\n");
        Console.WriteLine($" --- {prompt} --- ");
        Console.Write("\nYou may begin in: ");
        ShowCountdown(5);
        Console.WriteLine("\n");

        DateTime endTime = DateTime.Now.AddSeconds(GetDuration());
        int count = 0;

        while (DateTime.Now < endTime)
        {
            Console.Write("> ");
            string item = Console.ReadLine() ?? "";

            if (!string.IsNullOrWhiteSpace(item))
            {
                count++;
            }
        }

        Console.WriteLine($"\nYou listed {count} item{(count == 1 ? "" : "s")}!");
        DisplayEndingMessage();
    }

}


