using System;
using System.Collections.Generic;

public class ReflectionActivity : Activity
{
    private readonly List<string> _prompts = new List<string>
    {
        "Think of a time when you stood up for someone else.",
        "Think of a time when you did something really difficult.",
        "Think of a time when you helped someone in need.",
        "Think of a time when you did something truly selfless."
    };

    private readonly List<string> _questions = new List<string>
    {
        "Why was this experience meaningful to you?",
        "Have you ever done anything like this before?",
        "How did you get started?",
        "How did you feel when it was complete?",
        "What made this time different from other times when you were not as successful?",
        "What is your favorite thing about this experience?",
        "What could you learn from this experience that applies to other situations?",
        "What did you learn about yourself through this experience?",
        "How can you keep this experience in mind in the future?"
    };

    public ReflectionActivity()
        : base(
            "Reflection Activity",
            "This activity will help you reflect on times in your life when you have shown strength and resilience. This will help you recognize the power you have and how you can use it in other aspects of your life.")
    {
    }

    public override void Run()
    {
        DisplayStartingMessage();

        List<string> shuffledPrompts = Shuffle(_prompts);
        string prompt = shuffledPrompts[0];

        Console.WriteLine("Consider the following prompt:\n");
        Console.WriteLine($" --- {prompt} --- ");
        Console.WriteLine("\nWhen you have something in mind, press Enter to continue.");
        Console.ReadLine();

        Console.WriteLine("Now ponder each of the following questions as they relate to this experience.");
        Console.Write("You may begin in: ");
        ShowCountdown(5);
        Console.Clear();

        DateTime endTime = DateTime.Now.AddSeconds(GetDuration());
        List<string> shuffledQuestions = Shuffle(_questions);
        int questionIndex = 0;

        while (DateTime.Now < endTime)
        {
            if (questionIndex >= shuffledQuestions.Count)
            {
                shuffledQuestions = Shuffle(_questions);
                questionIndex = 0;
            }

            Console.Write($"> {shuffledQuestions[questionIndex]} ");
            questionIndex++;

            int remaining = Math.Max(1, (int)Math.Ceiling((endTime - DateTime.Now).TotalSeconds));
            ShowSpinner(Math.Min(7, remaining));
            Console.WriteLine();
        }

        DisplayEndingMessage();
    }
}

