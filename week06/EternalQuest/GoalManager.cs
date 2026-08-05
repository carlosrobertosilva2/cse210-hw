using System;
using System.Collections.Generic;
using System.IO;

public class GoalManager
{
    private List<Goal> _goals;
    private List<string> _badges;
    private int _score;

    public GoalManager()
    {
        _goals = new List<Goal>();
        _badges = new List<string>();
        _score = 0;
    }

    public void Start()
    {
        bool quit = false;

        while (!quit)
        {
            Console.Clear();
            DisplayPlayerInfo();

            Console.WriteLine();
            Console.WriteLine("Menu Options:");
            Console.WriteLine("  1. Create New Goal");
            Console.WriteLine("  2. List Goals");
            Console.WriteLine("  3. Save Goals");
            Console.WriteLine("  4. Load Goals");
            Console.WriteLine("  5. Record Event");
            Console.WriteLine("  6. View Badges");
            Console.WriteLine("  7. Quit");
            Console.Write("Select a choice from the menu: ");

            string choice = Console.ReadLine() ?? "";

            Console.WriteLine();

            switch (choice)
            {
                case "1":
                    CreateGoal();
                    break;
                case "2":
                    ListGoalDetails();
                    break;
                case "3":
                    SaveGoals();
                    break;
                case "4":
                    LoadGoals();
                    break;
                case "5":
                    RecordEvent();
                    break;
                case "6":
                    DisplayBadges();
                    break;
                case "7":
                    quit = true;
                    break;
                default:
                    Console.WriteLine("Please enter a menu option from 1 to 7.");
                    break;
            }

            if (!quit)
            {
                Pause();
            }
        }

        Console.WriteLine("Keep moving forward on your Eternal Quest!");
    }

    public void DisplayPlayerInfo()
    {
        int level = GetLevel();

        Console.WriteLine($"You have {_score} points.");
        Console.WriteLine($"Level {level}: {GetLevelTitle(level)}");
        Console.WriteLine($"Badges earned: {_badges.Count}");
    }

    public void ListGoalNames()
    {
        if (_goals.Count == 0)
        {
            Console.WriteLine("No goals have been created.");
            return;
        }

        for (int i = 0; i < _goals.Count; i++)
        {
            Console.WriteLine($"{i + 1}. {_goals[i].GetShortName()}");
        }
    }

    public void ListGoalDetails()
    {
        Console.WriteLine("Your goals:");

        if (_goals.Count == 0)
        {
            Console.WriteLine("  No goals have been created.");
            return;
        }

        for (int i = 0; i < _goals.Count; i++)
        {
            Console.WriteLine($"{i + 1}. {_goals[i].GetDetailsString()}");
        }
    }

    public void CreateGoal()
    {
        Console.WriteLine("Goal Types:");
        Console.WriteLine("  1. Simple Goal");
        Console.WriteLine("  2. Eternal Goal");
        Console.WriteLine("  3. Checklist Goal");
        Console.WriteLine("  4. Progress Goal");
        Console.Write("Which type of goal would you like to create? ");

        int type = ReadIntInRange(1, 4);

        Console.Write("What is the name of your goal? ");
        string name = ReadNonEmptyString();

        Console.Write("What is a short description of it? ");
        string description = ReadNonEmptyString();

        Console.Write("How many points is each recorded event worth? ");
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
            Console.Write("How many events are required to complete the goal? ");
            int target = ReadPositiveInt();

            Console.Write("What completion bonus should be awarded? ");
            int bonus = ReadPositiveInt();

            if (type == 3)
            {
                _goals.Add(new ChecklistGoal(name, description, points, target, bonus));
            }
            else
            {
                _goals.Add(new ProgressGoal(name, description, points, target, bonus));
            }
        }

        Console.WriteLine("Goal created successfully.");
    }

    public void RecordEvent()
    {
        if (_goals.Count == 0)
        {
            Console.WriteLine("Create a goal before recording an event.");
            return;
        }

        Console.WriteLine("The goals are:");
        ListGoalNames();
        Console.Write("Which goal did you accomplish? ");

        int index = ReadIntInRange(1, _goals.Count) - 1;
        Goal goal = _goals[index];

        int previousLevel = GetLevel();
        int earnedPoints = goal.RecordEvent();

        if (earnedPoints == 0)
        {
            Console.WriteLine("That goal is already complete. No additional points were awarded.");
            return;
        }

        _score += earnedPoints;
        int currentLevel = GetLevel();

        Console.WriteLine($"Congratulations! You earned {earnedPoints} points.");
        Console.WriteLine($"You now have {_score} points.");

        if (goal.IsComplete())
        {
            Console.WriteLine($"Goal completed: {goal.GetShortName()}!");
            AwardBadge($"Goal Finisher: {goal.GetShortName()}");
        }

        if (currentLevel > previousLevel)
        {
            Console.WriteLine($"Level up! You reached Level {currentLevel}: {GetLevelTitle(currentLevel)}.");
            AwardBadge($"Reached Level {currentLevel}");
        }

        CheckScoreBadges();
    }

    public void SaveGoals()
    {
        Console.Write("What filename should be used? ");
        string filename = ReadNonEmptyString();

        try
        {
            using (StreamWriter outputFile = new StreamWriter(filename))
            {
                outputFile.WriteLine($"Score:{_score}");
                outputFile.WriteLine($"Badges:{string.Join("~", _badges)}");

                foreach (Goal goal in _goals)
                {
                    outputFile.WriteLine(goal.GetStringRepresentation());
                }
            }

            Console.WriteLine($"Your score, badges, goals, and progress were saved to {filename}.");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"The file could not be saved: {ex.Message}");
        }
    }

    public void LoadGoals()
    {
        Console.Write("What filename should be loaded? ");
        string filename = ReadNonEmptyString();

        if (!File.Exists(filename))
        {
            Console.WriteLine("That file does not exist.");
            return;
        }

        try
        {
            string[] lines = File.ReadAllLines(filename);

            if (lines.Length < 2 ||
                !lines[0].StartsWith("Score:") ||
                !int.TryParse(lines[0].Substring("Score:".Length), out int loadedScore) ||
                !lines[1].StartsWith("Badges:"))
            {
                Console.WriteLine("The save file is not in the expected format.");
                return;
            }

            List<string> loadedBadges = new List<string>();
            string badgeData = lines[1].Substring("Badges:".Length);

            if (!string.IsNullOrWhiteSpace(badgeData))
            {
                loadedBadges.AddRange(
                    badgeData.Split('~', StringSplitOptions.RemoveEmptyEntries));
            }

            List<Goal> loadedGoals = new List<Goal>();

            for (int i = 2; i < lines.Length; i++)
            {
                if (string.IsNullOrWhiteSpace(lines[i]))
                {
                    continue;
                }

                Goal goal = CreateGoalFromString(lines[i]);

                if (goal == null)
                {
                    Console.WriteLine($"Warning: a goal on line {i + 1} could not be loaded.");
                    continue;
                }

                loadedGoals.Add(goal);
            }

            _score = loadedScore;
            _badges = loadedBadges;
            _goals = loadedGoals;

            Console.WriteLine(
                $"Loaded {_goals.Count} goal(s), {_badges.Count} badge(s), and {_score} points.");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"The file could not be loaded: {ex.Message}");
        }
    }

    public void DisplayBadges()
    {
        Console.WriteLine("Achievement Badges:");

        if (_badges.Count == 0)
        {
            Console.WriteLine("  No badges earned yet.");
            return;
        }

        foreach (string badge in _badges)
        {
            Console.WriteLine($"  * {badge}");
        }
    }

    private Goal CreateGoalFromString(string line)
    {
        string[] typeAndDetails = line.Split(new[] { ':' }, 2);

        if (typeAndDetails.Length != 2)
        {
            return null;
        }

        string type = typeAndDetails[0];
        string[] details = typeAndDetails[1].Split('|');

        try
        {
            if (type == "SimpleGoal" && details.Length == 4)
            {
                return new SimpleGoal(
                    details[0],
                    details[1],
                    int.Parse(details[2]),
                    bool.Parse(details[3]));
            }

            if (type == "EternalGoal" && details.Length == 3)
            {
                return new EternalGoal(
                    details[0],
                    details[1],
                    int.Parse(details[2]));
            }

            if (type == "ChecklistGoal" && details.Length == 6)
            {
                return new ChecklistGoal(
                    details[0],
                    details[1],
                    int.Parse(details[2]),
                    int.Parse(details[4]),
                    int.Parse(details[3]),
                    int.Parse(details[5]));
            }

            if (type == "ProgressGoal" && details.Length == 6)
            {
                return new ProgressGoal(
                    details[0],
                    details[1],
                    int.Parse(details[2]),
                    int.Parse(details[4]),
                    int.Parse(details[3]),
                    int.Parse(details[5]));
            }
        }
        catch (FormatException)
        {
            return null;
        }
        catch (OverflowException)
        {
            return null;
        }

        return null;
    }

    private int GetLevel()
    {
        return (_score / 1000) + 1;
    }

    private string GetLevelTitle(int level)
    {
        if (level >= 10)
        {
            return "Legend of the Eternal Quest";
        }

        if (level >= 7)
        {
            return "Quest Champion";
        }

        if (level >= 4)
        {
            return "Goal Warrior";
        }

        if (level >= 2)
        {
            return "Faithful Adventurer";
        }

        return "Beginning Explorer";
    }

    private void AwardBadge(string badge)
    {
        if (!_badges.Contains(badge))
        {
            _badges.Add(badge);
            Console.WriteLine($"Badge earned: {badge}");
        }
    }

    private void CheckScoreBadges()
    {
        if (_score >= 500)
        {
            AwardBadge("500 Point Starter");
        }

        if (_score >= 2500)
        {
            AwardBadge("2,500 Point Achiever");
        }

        if (_score >= 5000)
        {
            AwardBadge("5,000 Point Champion");
        }
    }

    private int ReadPositiveInt()
    {
        while (true)
        {
            string input = Console.ReadLine() ?? "";

            if (int.TryParse(input, out int value) && value > 0)
            {
                return value;
            }

            Console.Write("Please enter a whole number greater than zero: ");
        }
    }

    private int ReadIntInRange(int minimum, int maximum)
    {
        while (true)
        {
            string input = Console.ReadLine() ?? "";

            if (int.TryParse(input, out int value) &&
                value >= minimum &&
                value <= maximum)
            {
                return value;
            }

            Console.Write($"Please enter a number from {minimum} to {maximum}: ");
        }
    }

    private string ReadNonEmptyString()
    {
        while (true)
        {
            string input = Console.ReadLine() ?? "";

            if (!string.IsNullOrWhiteSpace(input))
            {
                return input.Trim();
            }

            Console.Write("Please enter a value: ");
        }
    }

    private void Pause()
    {
        Console.WriteLine();
        Console.WriteLine("Press Enter to continue...");
        Console.ReadLine();
    }
}
