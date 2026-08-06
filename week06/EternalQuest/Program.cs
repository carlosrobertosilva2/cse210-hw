// EXCEEDS CORE REQUIREMENTS:
// 1. The program saves and loads a persistent activity log in "activity_log.txt".
//    This allows the user to see how many times each activity has been completed
//    across multiple program runs, rather than only during the current session.
// 2. Reflection questions are shuffled and are not repeated until every question
//    has been used once in that reflection session.
// 3. Menu and duration input are validated so invalid entries do not crash the program.

class Program
{
    static void Main()
    {
        ActivityLog activityLog = new ActivityLog("activity_log.txt");
        string choice = "";

        while (choice != "4")
        {
            Console.Clear();
            Console.WriteLine("Menu Options:");
            Console.WriteLine("  1. Start breathing activity");
            Console.WriteLine("  2. Start reflection activity");
            Console.WriteLine("  3. Start listing activity");
            Console.WriteLine("  4. Quit");
            Console.Write("Select a choice from the menu: ");
            choice = Console.ReadLine()?.Trim() ?? "";

            Activity? activity = null;

            switch (choice)
            {
                case "1":
                    activity = new BreathingActivity();
                    break;
                case "2":
                    activity = new ReflectionActivity();
                    break;
                case "3":
                    activity = new ListingActivity();
                    break;
                case "4":
                    break;
                default:
                    Console.WriteLine("Please enter a number from 1 to 4.");
                    Thread.Sleep(1500);
                    break;
            }

            if (activity != null)
            {
                activity.Run();
                activityLog.RecordActivity(activity.GetName());
            }
        }

        Console.Clear();
        activityLog.DisplaySummary();
        Console.WriteLine("\nThank you for using the Mindfulness Program.");
    }
}
