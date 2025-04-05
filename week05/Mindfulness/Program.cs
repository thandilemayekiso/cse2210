using System;
using System.Collections.Generic;
using System.Threading;

// Base class for all mindfulness activities
abstract class MindfulnessActivity
{
    protected int duration;

    public void StartActivity()
    {
        Console.Clear();
        Console.WriteLine(GetDescription());
        Console.Write("Enter duration (seconds): ");
        duration = int.Parse(Console.ReadLine());
        Console.WriteLine("Prepare to begin...");
        ShowSpinner(3);
        RunActivity();
        Console.WriteLine("Good job! You have completed the activity.");
        Console.WriteLine($"Activity completed: {GetType().Name}, Duration: {duration} seconds");
        ShowSpinner(3);
    }

    protected abstract string GetDescription();
    protected abstract void RunActivity();

    protected void ShowSpinner(int seconds)
    {
        for (int i = 0; i < seconds; i++)
        {
            Console.Write(".");
            Thread.Sleep(1000);
        }
        Console.WriteLine();
    }
}

// Breathing Activity
class BreathingActivity : MindfulnessActivity
{
    protected override string GetDescription()
    {
        return "This activity will help you relax by guiding you through breathing exercises.";
    }

    protected override void RunActivity()
    {
        for (int i = 0; i < duration / 2; i++)
        {
            Console.WriteLine("Breathe in...");
            ShowSpinner(2);
            Console.WriteLine("Breathe out...");
            ShowSpinner(2);
        }
    }
}

// Reflection Activity
class ReflectionActivity : MindfulnessActivity
{
    private static readonly List<string> Prompts = new List<string>
    {
        "Think of a time when you stood up for someone else.",
        "Think of a time when you did something really difficult.",
        "Think of a time when you helped someone in need.",
        "Think of a time when you did something truly selfless."
    };

    private static readonly List<string> Questions = new List<string>
    {
        "Why was this experience meaningful to you?",
        "Have you ever done anything like this before?",
        "How did you feel when it was complete?",
        "What did you learn about yourself through this experience?"
    };

    protected override string GetDescription()
    {
        return "This activity will help you reflect on times in your life when you have shown strength and resilience.";
    }

    protected override void RunActivity()
    {
        Random rand = new Random();
        string prompt = Prompts[rand.Next(Prompts.Count)];
        Console.WriteLine(prompt);
        ShowSpinner(3);

        for (int i = 0; i < duration / 5; i++)
        {
            Console.WriteLine(Questions[rand.Next(Questions.Count)]);
            ShowSpinner(5);
        }
    }
}

// Listing Activity
class ListingActivity : MindfulnessActivity
{
    private static readonly List<string> Prompts = new List<string>
    {
        "Who are people that you appreciate?",
        "What are personal strengths of yours?",
        "Who are people that you have helped this week?",
        "Who are some of your personal heroes?"
    };

    protected override string GetDescription()
    {
        return "This activity will help you reflect on positive aspects of your life by listing them.";
    }

    protected override void RunActivity()
    {
        Random rand = new Random();
        string prompt = Prompts[rand.Next(Prompts.Count)];
        Console.WriteLine(prompt);
        ShowSpinner(3);

        Console.WriteLine("List as many as you can: ");
        List<string> responses = new List<string>();
        DateTime endTime = DateTime.Now.AddSeconds(duration);

        while (DateTime.Now < endTime)
        {
            Console.Write("- ");
            responses.Add(Console.ReadLine());
        }

        Console.WriteLine($"You listed {responses.Count} items.");
    }
}

// Main Program
class Program
{
    static void Main()
    {
        while (true)
        {
            Console.Clear();
            Console.WriteLine("Mindfulness Program");
            Console.WriteLine("1. Breathing Activity");
            Console.WriteLine("2. Reflection Activity");
            Console.WriteLine("3. Listing Activity");
            Console.WriteLine("4. Exit");
            Console.Write("Choose an option: ");
            string choice = Console.ReadLine();

            MindfulnessActivity activity = choice switch
            {
                "1" => new BreathingActivity(),
                "2" => new ReflectionActivity(),
                "3" => new ListingActivity(),
                "4" => null,
                _ => throw new Exception("Invalid choice")
            };

            if (activity == null) break;

            activity.StartActivity();
        }
    }
}
// The code above implements a mindfulness program with three activities: Breathing, Reflection, and Listing. Each activity has its own description and behavior. The program allows the user to choose an activity and specifies the duration for each activity. The activities include prompts and questions to guide the user through the mindfulness experience.