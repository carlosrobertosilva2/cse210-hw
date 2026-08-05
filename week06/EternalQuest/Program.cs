using System;

/*
EXCEEDS REQUIREMENTS:
This program adds a fourth ProgressGoal type, a level-and-title system, and
achievement badges for completed goals, score milestones, and level increases.
The extra goal, badges, score, and all progress are included in save/load files.
The program also validates input and prevents completed goals from awarding
duplicate points.
*/

class Program
{
    static void Main(string[] args)
    {
        GoalManager manager = new GoalManager();
        manager.Start();
    }
}
