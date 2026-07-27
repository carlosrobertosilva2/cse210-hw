public class EternalGoal : Goal
{
    public EternalGoal(string shortName, string description, int points)
        : base(shortName, description, points)
    {
    }

    public override void RecordEvent()
    {
        // Eternal goals never become complete.
    }

    public override bool IsComplete()
    {
        return false;
    }

    public override string GetStringRepresentation()
    {
        return $"EternalGoal:{GetShortName()}|{GetDescription()}|{GetPoints()}";
    }
}
