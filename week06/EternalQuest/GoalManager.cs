using System;
using System.Collections.Generic;
using System.IO;

public class GoalManager
{
    private List<Goal> _goals = new List<Goal>();
    private int _score = 0;
    private int _level = 1;

    public void Start()
    {
        bool quit = false;

        while (!quit)
        {
            DisplayPlayerInfo();
            Console.WriteLine();
            Console.WriteLine("Menu Options:");
            Console.WriteLine("  1. Create New Goal");
            Console.WriteLine("  2. List Goals");
            Console.WriteLine("  3. Save Goals");
            Console.WriteLine("  4. Load Goals");
            Console.WriteLine("  5. Record Event");
            Console.WriteLine("  6. Quit");
            Console.Write("Select a choice from the menu: ");

            string choice = Console.ReadLine() ?? "";
            Console.WriteLine();

            switch (choice)
            {
                case "1": CreateGoal(); break;
                case "2": ListGoalDetails(); break;
                case "3": SaveGoals(); break;
                case "4": LoadGoals(); break;
                case "5": RecordEvent(); break;
                case "6": quit = true; break;
                default:
                    Console.WriteLine("Please choose a valid option from 1 to 6.");
                    break;
            }

            if (!quit)
            {
                Console.WriteLine();
                Console.WriteLine("Press Enter to continue...");
                Console.ReadLine();
                Console.Clear();
            }
        }

        Console.WriteLine("Keep working on your Eternal Quest!");
    }

    public void DisplayPlayerInfo()
    {
        Console.WriteLine($"You have {_score} points.");
        Console.WriteLine($"Level: {_level} ({GetLevelTitle()})");
    }

    public void ListGoalNames()
    {
        if (_goals.Count == 0)
        {
            Console.WriteLine("You do not have any goals yet.");
            return;
        }

        for (int i = 0; i < _goals.Count; i++)
        {
            Console.WriteLine($"{i + 1}. {_goals[i].GetShortName()}");
        }
    }

    public void ListGoalDetails()
    {
        Console.WriteLine("The goals are:");

        if (_goals.Count == 0)
        {
            Console.WriteLine("  No goals have been created yet.");
            return;
        }

        for (int i = 0; i < _goals.Count; i++)
        {
            Console.WriteLine($"{i + 1}. {_goals[i].GetDetailsString()}");
        }
    }

    public void CreateGoal()
    {
        Console.WriteLine("The types of Goals are:");
        Console.WriteLine("  1. Simple Goal");
        Console.WriteLine("  2. Eternal Goal");
        Console.WriteLine("  3. Checklist Goal");
        Console.Write("Which type of goal would you like to create? ");
        int type = ReadIntInRange(1, 3);

        Console.Write("What is the name of your goal? ");
        string name = ReadNonEmptyString();

        Console.Write("What is a short description of it? ");
        string description = ReadNonEmptyString();

        Console.Write("What is the amount of points associated with this goal? ");
        int points = ReadPositiveInt();

        if (type == 1)
        {
            _goals.Add(new SimpleGoal(name, description, points));
        }
        else if (type == 2)
        {
            _goals.Add(new EternalGoal(name, description, points));
        }
        else
        {
            Console.Write("How many times does this goal need to be accomplished for a bonus? ");
            int target = ReadPositiveInt();

            Console.Write("What is the bonus for accomplishing it that many times? ");
            int bonus = ReadPositiveInt();

            _goals.Add(new ChecklistGoal(name, description, points, target, bonus));
        }

        Console.WriteLine("Goal created successfully.");
    }

    public void RecordEvent()
    {
        if (_goals.Count == 0)
        {
            Console.WriteLine("You need to create a goal before recording an event.");
            return;
        }

        Console.WriteLine("The goals are:");
        ListGoalNames();
        Console.Write("Which goal did you accomplish? ");
        int index = ReadIntInRange(1, _goals.Count) - 1;

        Goal goal = _goals[index];

        if (goal is SimpleGoal && goal.IsComplete())
        {
            Console.WriteLine("That simple goal is already complete, so no extra points were awarded.");
            return;
        }

        int earnedPoints = goal.GetPoints();

        if (goal is ChecklistGoal checklist && checklist.IsOneAwayFromCompletion())
        {
            earnedPoints += checklist.GetBonus();
        }

        goal.RecordEvent();
        _score += earnedPoints;
        UpdateLevel();

        Console.WriteLine($"Congratulations! You have earned {earnedPoints} points!");
        Console.WriteLine($"You now have {_score} points.");

        if (goal.IsComplete())
        {
            Console.WriteLine($"Goal completed: {goal.GetShortName()}!");
        }
    }

    public void SaveGoals()
    {
        Console.Write("What is the filename for the goal file? ");
        string filename = ReadNonEmptyString();

        try
        {
            using StreamWriter outputFile = new StreamWriter(filename);
            outputFile.WriteLine(_score);

            foreach (Goal goal in _goals)
            {
                outputFile.WriteLine(goal.GetStringRepresentation());
            }

            Console.WriteLine($"Goals saved to {filename}.");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"The file could not be saved: {ex.Message}");
        }
    }

    public void LoadGoals()
    {
        Console.Write("What is the filename for the goal file? ");
        string filename = ReadNonEmptyString();

        if (!File.Exists(filename))
        {
            Console.WriteLine("That file does not exist.");
            return;
        }

        try
        {
            string[] lines = File.ReadAllLines(filename);

            if (lines.Length == 0 || !int.TryParse(lines[0], out int loadedScore))
            {
                Console.WriteLine("The save file is invalid.");
                return;
            }

            List<Goal> loadedGoals = new List<Goal>();

            for (int i = 1; i < lines.Length; i++)
            {
                Goal? goal = CreateGoalFromString(lines[i]);
                if (goal != null)
                {
                    loadedGoals.Add(goal);
                }
            }

            _score = loadedScore;
            _goals = loadedGoals;
            UpdateLevel();

            Console.WriteLine($"Loaded {_goals.Count} goal(s) from {filename}.");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"The file could not be loaded: {ex.Message}");
        }
    }

    private Goal? CreateGoalFromString(string line)
    {
        if (string.IsNullOrWhiteSpace(line))
            return null;

        string[] typeAndData = line.Split(':', 2);
        if (typeAndData.Length != 2)
            return null;

        string type = typeAndData[0];
        string[] parts = typeAndData[1].Split('|');

        try
        {
            if (type == "SimpleGoal" && parts.Length == 4)
            {
                return new SimpleGoal(parts[0], parts[1], int.Parse(parts[2]), bool.Parse(parts[3]));
            }

            if (type == "EternalGoal" && parts.Length == 3)
            {
                return new EternalGoal(parts[0], parts[1], int.Parse(parts[2]));
            }

            if (type == "ChecklistGoal" && parts.Length == 6)
            {
                return new ChecklistGoal(
                    parts[0],
                    parts[1],
                    int.Parse(parts[2]),
                    int.Parse(parts[4]),
                    int.Parse(parts[3]),
                    int.Parse(parts[5]));
            }
        }
        catch
        {
            return null;
        }

        return null;
    }

    private void UpdateLevel()
    {
        _level = (_score / 1000) + 1;
    }

    private string GetLevelTitle()
    {
        if (_level >= 10) return "Legend of the Eternal Quest";
        if (_level >= 7) return "Quest Champion";
        if (_level >= 4) return "Goal Warrior";
        if (_level >= 2) return "Faithful Adventurer";
        return "Beginning Explorer";
    }

    private int ReadPositiveInt()
    {
        while (true)
        {
            string input = Console.ReadLine() ?? "";

            if (int.TryParse(input, out int value) && value > 0)
                return value;

            Console.Write("Please enter a whole number greater than 0: ");
        }
    }

    private int ReadIntInRange(int minimum, int maximum)
    {
        while (true)
        {
            string input = Console.ReadLine() ?? "";

            if (int.TryParse(input, out int value) && value >= minimum && value <= maximum)
                return value;

            Console.Write($"Please enter a number from {minimum} to {maximum}: ");
        }
    }

    private string ReadNonEmptyString()
    {
        while (true)
        {
            string input = Console.ReadLine() ?? "";

            if (!string.IsNullOrWhiteSpace(input))
                return input.Trim();

            Console.Write("Please enter a value: ");
        }
    }
}
