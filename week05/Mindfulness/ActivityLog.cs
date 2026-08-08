using System;
using System.Collections.Generic;
using System.IO;

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
                int count;

                if (parts.Length == 2 && int.TryParse(parts[1], out count))
                {
                    _activityCounts[parts[0]] = count;
                }
            }
        }
        catch (IOException)
        {
            // The program can still run if the activity log cannot be read.
        }
        catch (UnauthorizedAccessException)
        {
            // The program can still run if access to the activity log is restricted.
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
            // A file-writing problem should not stop the mindfulness activities.
        }
        catch (UnauthorizedAccessException)
        {
            // The program remains usable even if it cannot save the activity log.
        }
    }
}

