public abstract class Activity
{
    private readonly string _name;
    private readonly string _description;
    private int _duration;

    protected Activity(string name, string description)
    {
        _name = name;
        _description = description;
    }

    public string GetName()
    {
        return _name;
    }

    protected int GetDuration()
    {
        return _duration;
    }

    protected void DisplayStartingMessage()
    {
        Console.Clear();
        Console.WriteLine($"Welcome to the {_name}.\n");
        Console.WriteLine(_description);
        Console.WriteLine();

        while (true)
        {
            Console.Write("How long, in seconds, would you like for your session? ");
            string input = Console.ReadLine()?.Trim() ?? "";

            if (int.TryParse(input, out int duration) && duration > 0)
            {
                _duration = duration;
                break;
            }

            Console.WriteLine("Please enter a positive whole number.");
        }

        Console.Clear();
        Console.WriteLine("Get ready...");
        ShowSpinner(3);
        Console.WriteLine();
    }

    protected void DisplayEndingMessage()
    {
        Console.WriteLine("\nWell done!!");
        ShowSpinner(2);
        Console.WriteLine($"\nYou have completed another {_duration} seconds of the {_name}.");
        ShowSpinner(3);
    }

    protected void ShowSpinner(int seconds)
    {
        string[] frames = { "|", "/", "-", "\\" };
        DateTime endTime = DateTime.Now.AddSeconds(seconds);
        int index = 0;

        while (DateTime.Now < endTime)
        {
            Console.Write(frames[index]);
            Thread.Sleep(200);
            Console.Write("\b \b");
            index = (index + 1) % frames.Length;
        }
    }

    protected void ShowCountdown(int seconds)
    {
        for (int i = seconds; i > 0; i--)
        {
            Console.Write(i);
            Thread.Sleep(1000);
            Console.Write(new string('\b', i.ToString().Length));
            Console.Write(new string(' ', i.ToString().Length));
            Console.Write(new string('\b', i.ToString().Length));
        }
    }

    protected static List<T> Shuffle<T>(IEnumerable<T> items)
    {
        Random random = Random.Shared;
        List<T> shuffled = items.ToList();

        for (int i = shuffled.Count - 1; i > 0; i--)
        {
            int j = random.Next(i + 1);
            (shuffled[i], shuffled[j]) = (shuffled[j], shuffled[i]);
        }

        return shuffled;
    }

    public abstract void Run();
}
