public class ChecklistGoal : Goal
{
    private int _amountCompleted;
    private int _target;
    private int _bonus;

    public ChecklistGoal(
        string shortName,
        string description,
        int points,
        int target,
        int bonus)
        : this(shortName, description, points, target, bonus, 0)
    {
    }

    public ChecklistGoal(
        string shortName,
        string description,
        int points,
        int target,
        int bonus,
        int amountCompleted)
        : base(shortName, description, points)
    {
        _target = target;
        _bonus = bonus;
        _amountCompleted = amountCompleted;
    }

    public override int RecordEvent()
    {
        if (IsComplete())
        {
            return 0;
        }

        _amountCompleted++;

        if (IsComplete())
        {
            return GetPoints() + _bonus;
        }

        return GetPoints();
    }

    public override bool IsComplete()
    {
        return _amountCompleted >= _target;
    }

    public override string GetDetailsString()
    {
        string checkbox = IsComplete() ? "[X]" : "[ ]";

        return $"{checkbox} {GetShortName()} ({GetDescription()}) " +
               $"-- Completed {_amountCompleted}/{_target} times";
    }

    public override string GetStringRepresentation()
    {
        return $"ChecklistGoal:{GetShortName()}|{GetDescription()}|{GetPoints()}|" +
               $"{_bonus}|{_target}|{_amountCompleted}";
    }
}
