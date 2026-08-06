public class ActivityLog
{
    private readonly string _fileName;
    private readonly Dictionary<string, int> _activityCounts;

    public ActivityLog(string fileName)
    {
        _fileName = fileName;
        _activityCounts = new Dictionary<string, int>
        {
            { "Breathing Activity", 0 },
            { "Reflection Activity", 0 },
            { "Listing Activity", 0 }
        };

        Load();
    }

    public void RecordActivity(string activityName)
    {
        if (_activityCounts.ContainsKey(activityName))
        {
            _activityCounts[activityName]++;
        }
        else
        {
            _activityCounts[activityName] = 1;
        }

        Save();
    }

    public void DisplaySummary()
    {
        Console.WriteLine("Activity History");
        Console.WriteLine("----------------");

        foreach (KeyValuePair<string, int> entry in _activityCounts)
        {
            Console.WriteLine($"{entry.Key}: {entry.Value}");
        }
    }

    private void Load()
    {
        if (!File.Exists(_fileName))
        {
            return;
        }

        try
        {
            string[] lines = File.ReadAllLines(_fileName);

            foreach (string line in lines)
            {
                string[] parts = line.Split('|');

                if (parts.Length == 2 && int.TryParse(parts[1], out int count))
                {
                    _activityCounts[parts[0]] = count;
                }
            }
        }
        catch (IOException)
        {
            // If the log cannot be read, the program continues with zeroed counts.
        }
        catch (UnauthorizedAccessException)
        {
            // If file access is restricted, the mindfulness activities still run.
        }
    }

    private void Save()
    {
        try
        {
            List<string> lines = new List<string>();

            foreach (KeyValuePair<string, int> entry in _activityCounts)
            {
                lines.Add($"{entry.Key}|{entry.Value}");
            }

            File.WriteAllLines(_fileName, lines);
        }
        catch (IOException)
        {
            // A file-writing problem should not stop the mindfulness activity.
        }
        catch (UnauthorizedAccessException)
        {
            // The program remains usable even when it cannot save the log.
        }
    }
}
