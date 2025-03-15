using System;
using System.Collections.Generic;
using System.IO;

class Entry
{
    public string Date { get; set; }
    public string Prompt { get; set; }
    public string Response { get; set; }

    public Entry(string prompt, string response)
    {
        Date = DateTime.Now.ToShortDateString();
        Prompt = prompt;
        Response = response;
    }

    public void Display()
    {
        Console.WriteLine($"Date: {Date}\nPrompt: {Prompt}\nResponse: {Response}\n");
    }

    public string ToFileFormat()
    {
        return $"{Date}|{Prompt}|{Response}";
    }
}

class Journal
{
    private List<Entry> _entries = new List<Entry>();

    public void AddEntry(Entry entry)
    {
        _entries.Add(entry);
    }

    public void DisplayEntries()
    {
        foreach (var entry in _entries)
        {
            entry.Display();
        }
    }

    public void SaveToFile(string filename)
    {
        using (StreamWriter outputFile = new StreamWriter(filename))
        {
            foreach (var entry in _entries)
            {
                outputFile.WriteLine(entry.ToFileFormat());
            }
        }
    }

    public void LoadFromFile(string filename)
    {
        _entries.Clear();
        string[] lines = File.ReadAllLines(filename);
        foreach (string line in lines)
        {
            string[] parts = line.Split('|');
            if (parts.Length == 3)
            {
                _entries.Add(new Entry(parts[1], parts[2]) { Date = parts[0] });
            }
        }
    }
}

class PromptGenerator
{
    private List<string> prompts = new List<string>
    {
        "Who was the most interesting person I interacted with today?",
        "What was the best part of my day?",
        "How did I see the hand of the Lord in my life today?",
        "What was the strongest emotion I felt today?",
        "If I had one thing I could do over today, what would it be?"
    };

    public string GetRandomPrompt()
    {
        Random rnd = new Random();
        return prompts[rnd.Next(prompts.Count)];
    }
}

class Program
{
    static void Main()
    {
        Journal myJournal = new Journal();
        PromptGenerator promptGen = new PromptGenerator();
        
        while (true)
        {
            Console.WriteLine("Journal Menu:");
            Console.WriteLine("1. Write a new entry");
            Console.WriteLine("2. Display journal");
            Console.WriteLine("3. Save journal to file");
            Console.WriteLine("4. Load journal from file");
            Console.WriteLine("5. Exit");
            Console.Write("Choose an option: ");
            string choice = Console.ReadLine();

            switch (choice)
            {
                case "1":
                    string prompt = promptGen.GetRandomPrompt();
                    Console.WriteLine($"Prompt: {prompt}");
                    Console.Write("Your response: ");
                    string response = Console.ReadLine();
                    myJournal.AddEntry(new Entry(prompt, response));
                    break;
                case "2":
                    myJournal.DisplayEntries();
                    break;
                case "3":
                    Console.Write("Enter filename: ");
                    string saveFile = Console.ReadLine();
                    myJournal.SaveToFile(saveFile);
                    break;
                case "4":
                    Console.Write("Enter filename: ");
                    string loadFile = Console.ReadLine();
                    myJournal.LoadFromFile(loadFile);
                    break;
                case "5":
                    return;
                default:
                    Console.WriteLine("Invalid choice. Try again.");
                    break;
            }
        }
    }
}
