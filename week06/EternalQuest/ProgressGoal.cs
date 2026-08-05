using System;

public class ProgressGoal : Goal
{
    private int _currentAmount;
    private int _targetAmount;
    private int _completionBonus;

    public ProgressGoal(
        string shortName,
        string description,
        int points,
        int targetAmount,
        int completionBonus)
        : this(shortName, description, points, targetAmount, completionBonus, 0)
    {
    }

    public ProgressGoal(
        string shortName,
        string description,
        int points,
        int targetAmount,
        int completionBonus,
        int currentAmount)
        : base(shortName, description, points)
    {
        _targetAmount = targetAmount;
        _completionBonus = completionBonus;
        _currentAmount = currentAmount;
    }

    public override int RecordEvent()
    {
        if (IsComplete())
        {
            return 0;
        }

        _currentAmount++;

        if (IsComplete())
        {
            return GetPoints() + _completionBonus;
        }

        return GetPoints();
    }

    public override bool IsComplete()
    {
        return _currentAmount >= _targetAmount;
    }

    public override string GetDetailsString()
    {
        string checkbox = IsComplete() ? "[X]" : "[ ]";
        int percentage = (int)((double)_currentAmount / _targetAmount * 100);
        percentage = Math.Min(percentage, 100);

        return $"{checkbox} {GetShortName()} ({GetDescription()}) " +
               $"-- Progress {_currentAmount}/{_targetAmount} ({percentage}%)";
    }

    public override string GetStringRepresentation()
    {
        return $"ProgressGoal:{GetShortName()}|{GetDescription()}|{GetPoints()}|" +
               $"{_completionBonus}|{_targetAmount}|{_currentAmount}";
    }
}
