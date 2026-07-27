using System;
using System.IO;

public class Program
{
    /*
     * Creativity / Exceeding Requirements:
     * 1. The program keeps a simple activity log in mindfulness_log.txt.
     *    Each completed activity is saved with the activity name, duration, and timestamp.
     * 2. The menu includes an option to view the saved activity log.
     * 3. Reflection questions are randomized without repeats until every question has been used once.
     * 4. Duration input is validated so invalid or negative values do not crash the program.
     */

    private const string LogFileName = "mindfulness_log.txt";

    public static void Main()
    {
        bool running = true;

        while (running)
        {
            Console.Clear();
            Console.WriteLine("Menu Options:");
            Console.WriteLine("  1. Start breathing activity");
            Console.WriteLine("  2. Start reflection activity");
            Console.WriteLine("  3. Start listing activity");
            Console.WriteLine("  4. View activity log");
            Console.WriteLine("  5. Quit");
            Console.Write("Select a choice from the menu: ");

            string? choice = Console.ReadLine();

            switch (choice)
            {
                case "1":
                    RunAndLog(new BreathingActivity());
                    break;

                case "2":
                    RunAndLog(new ReflectionActivity());
                    break;

                case "3":
                    RunAndLog(new ListingActivity());
                    break;

                case "4":
                    DisplayLog();
                    break;

                case "5":
                    running = false;
                    break;

                default:
                    Console.WriteLine("Please choose a number from 1 to 5.");
                    Console.WriteLine("Press Enter to return to the menu.");
                    Console.ReadLine();
                    break;
            }
        }
    }

    private static void RunAndLog(Activity activity)
    {
        switch (activity)
        {
            case BreathingActivity breathing:
                breathing.Run();
                break;
            case ReflectionActivity reflection:
                reflection.Run();
                break;
            case ListingActivity listing:
                listing.Run();
                break;
        }

        SaveLog(activity);
    }

    private static void SaveLog(Activity activity)
    {
        string entry = $"{DateTime.Now:yyyy-MM-dd HH:mm:ss} | {activity.GetName()} | {activity.GetDuration()} seconds";
        File.AppendAllLines(LogFileName, new[] { entry });
    }

    private static void DisplayLog()
    {
        Console.Clear();
        Console.WriteLine("Mindfulness Activity Log\n");

        if (!File.Exists(LogFileName))
        {
            Console.WriteLine("No activities have been logged yet.");
        }
        else
        {
            string[] entries = File.ReadAllLines(LogFileName);

            if (entries.Length == 0)
            {
                Console.WriteLine("No activities have been logged yet.");
            }
            else
            {
                foreach (string entry in entries)
                {
                    Console.WriteLine(entry);
                }
            }
        }

        Console.WriteLine("\nPress Enter to return to the menu.");
        Console.ReadLine();
    }
}

