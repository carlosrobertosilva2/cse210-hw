using System;

public class BreathingActivity : Activity
{
    public BreathingActivity()
        : base(
            "Breathing Activity",
            "This activity will help you relax by walking you through breathing in and out slowly. Clear your mind and focus on your breathing.")
    {
    }

    public override void Run()
    {
        DisplayStartingMessage();
        DateTime endTime = DateTime.Now.AddSeconds(GetDuration());

        while (DateTime.Now < endTime)
        {
            int remaining = Math.Max(0, (int)Math.Ceiling((endTime - DateTime.Now).TotalSeconds));
            int inhaleSeconds = Math.Min(4, remaining);
            if (inhaleSeconds <= 0) break;

            Console.Write("\nBreathe in... ");
            ShowCountdown(inhaleSeconds);

            remaining = Math.Max(0, (int)Math.Ceiling((endTime - DateTime.Now).TotalSeconds));
            int exhaleSeconds = Math.Min(6, remaining);
            if (exhaleSeconds <= 0) break;

            Console.Write("\nBreathe out... ");
            ShowCountdown(exhaleSeconds);
            Console.WriteLine();
        }

        DisplayEndingMessage();
    }
}
