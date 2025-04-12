// This program is a simple goal tracking application that allows users to create, record, and manage goals.

using System;
using System.Collections.Generic;
using System.IO;

abstract class Goal
{
    public string Name { get; protected set; }
    public string Description { get; protected set; }
    public int Points { get; protected set; }
    public DateTime LastCompleted { get; protected set; }

    public Goal(string name, string description, int points)
    {
        Name = name;
        Description = description;
        Points = points;
        LastCompleted = DateTime.MinValue;
    }

    public abstract int RecordEvent();
    public abstract string GetStatus();
    public abstract string Serialize();
    public static Goal Deserialize(string line)
    {
        var parts = line.Split('|');
        string type = parts[0];

        return type switch
        {
            "SimpleGoal" => new SimpleGoal(parts[1], parts[2], int.Parse(parts[3]), bool.Parse(parts[4]), DateTime.Parse(parts[5])),
            "EternalGoal" => new EternalGoal(parts[1], parts[2], int.Parse(parts[3]), DateTime.Parse(parts[4])),
            "ChecklistGoal" => new ChecklistGoal(parts[1], parts[2], int.Parse(parts[3]), int.Parse(parts[4]), int.Parse(parts[5]), int.Parse(parts[6]), DateTime.Parse(parts[7])),
            _ => null,
        };
    }
}

class SimpleGoal : Goal
{
    private bool _isComplete;

    public SimpleGoal(string name, string description, int points, bool isComplete = false, DateTime? lastCompleted = null)
        : base(name, description, points)
    {
        _isComplete = isComplete;
        LastCompleted = lastCompleted ?? DateTime.MinValue;
    }

    public override int RecordEvent()
    {
        if (_isComplete) return 0;
        _isComplete = true;
        LastCompleted = DateTime.Now;
        return Points;
    }

    public override string GetStatus() => $"[{(_isComplete ? "X" : " ")}] {Name} - {Description} (Last: {LastCompleted.ToShortDateString()})";
    public override string Serialize() => $"SimpleGoal|{Name}|{Description}|{Points}|{_isComplete}|{LastCompleted}";
}

class EternalGoal : Goal
{
    public EternalGoal(string name, string description, int points, DateTime? lastCompleted = null)
        : base(name, description, points)
    {
        LastCompleted = lastCompleted ?? DateTime.MinValue;
    }

    public override int RecordEvent()
    {
        LastCompleted = DateTime.Now;
        return Points;
    }
    public override string GetStatus() => $"[∞] {Name} - {Description} (Last: {LastCompleted.ToShortDateString()})";
    public override string Serialize() => $"EternalGoal|{Name}|{Description}|{Points}|{LastCompleted}";
}

class ChecklistGoal : Goal
{
    private int _timesCompleted;
    private int _targetCount;
    private int _bonus;

    public ChecklistGoal(string name, string description, int points, int targetCount, int bonus, int timesCompleted = 0, DateTime? lastCompleted = null)
        : base(name, description, points)
    {
        _targetCount = targetCount;
        _bonus = bonus;
        _timesCompleted = timesCompleted;
        LastCompleted = lastCompleted ?? DateTime.MinValue;
    }

    public override int RecordEvent()
    {
        _timesCompleted++;
        LastCompleted = DateTime.Now;
        return _timesCompleted == _targetCount ? Points + _bonus : Points;
    }

    public override string GetStatus()
    {
        int filled = (int)(((double)_timesCompleted / _targetCount) * 5);
        string progressBar = "[" + new string('■', filled) + new string('□', 5 - filled) + "]";
        return $"[✓] {Name} - {Description} {progressBar} Completed {_timesCompleted}/{_targetCount} (Last: {LastCompleted.ToShortDateString()})";
    }

    public override string Serialize() => $"ChecklistGoal|{Name}|{Description}|{Points}|{_targetCount}|{_bonus}|{_timesCompleted}|{LastCompleted}";
}

class GoalManager
{
    private List<Goal> _goals = new List<Goal>();
    private int _score = 0;

    public void CreatePredefinedGoals()
    {
        _goals.Add(new EternalGoal("Read Book of Mormon", "Read scriptures every morning", 100));
        _goals.Add(new EternalGoal("Go to Gym", "Daily workout for fat loss and fitness (AM cardio, PM strength)", 150));
        _goals.Add(new ChecklistGoal("Temple Trips", "Attend temple with family on 4 dates in the year", 200, 4, 800));
    }

    public void RecordEvent()
    {
        ListGoals();
        Console.Write("Enter the number of the goal to record: ");
        if (int.TryParse(Console.ReadLine(), out int index) && index > 0 && index <= _goals.Count)
        {
            int earned = _goals[index - 1].RecordEvent();
            _score += earned;
            Console.WriteLine($"You earned {earned} points!");
        }
    }

    public void ListGoals()
    {
        Console.WriteLine("\nYour Goals:");
        for (int i = 0; i < _goals.Count; i++)
        {
            Console.WriteLine($"{i + 1}. {_goals[i].GetStatus()}");
        }
        Console.WriteLine($"Score: {_score}\n");

        Console.WriteLine("Morning Reminder:");
        foreach (var g in _goals)
        {
            if (g.Name.Contains("Read")) Console.WriteLine($"🌞 {g.Name}: {g.Description}");
        }

        Console.WriteLine("Evening Reminder:");
        foreach (var g in _goals)
        {
            if (g.Name.Contains("Gym")) Console.WriteLine($"🌙 {g.Name}: {g.Description}");
        }
    }

    public void SaveGoals(string filename)
    {
        using StreamWriter writer = new StreamWriter(filename);
        writer.WriteLine(_score);
        foreach (var goal in _goals)
        {
            writer.WriteLine(goal.Serialize());
        }
    }

    public void LoadGoals(string filename)
    {
        if (File.Exists(filename))
        {
            var lines = File.ReadAllLines(filename);
            _score = int.Parse(lines[0]);
            _goals.Clear();
            for (int i = 1; i < lines.Length; i++)
            {
                var goal = Goal.Deserialize(lines[i]);
                if (goal != null) _goals.Add(goal);
            }
        }
    }
}

class Program
{
    static void Main()
    {
        GoalManager manager = new GoalManager();
        manager.CreatePredefinedGoals();

        bool exit = false;
        while (!exit)
        {
            Console.WriteLine("Menu:");
            Console.WriteLine("1. List Goals");
            Console.WriteLine("2. Record Event");
            Console.WriteLine("3. Save Goals");
            Console.WriteLine("4. Load Goals");
            Console.WriteLine("5. Exit");
            Console.Write("Select an option: ");

            switch (Console.ReadLine())
            {
                case "1": manager.ListGoals(); break;
                case "2": manager.RecordEvent(); break;
                case "3": manager.SaveGoals("goals.txt"); break;
                case "4": manager.LoadGoals("goals.txt"); break;
                case "5": exit = true; break;
            }
        }
    }
}