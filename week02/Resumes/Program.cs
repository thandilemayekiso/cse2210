using System;
using System.Collections.Generic;
using System.IO;

class Program
{
    static List<JournalEntry> journal = new List<JournalEntry>();
    static Random random = new Random();
    static string[] prompts =
    {
        "Who was the most interesting person I interacted with today?",
        "What was the best part of my day?",
        "How did I see the hand of the Lord in my life today?",
        "What was the strongest emotion I felt today?",
        "If I had one thing I could do over today, what would it be?"
    };
    
    static void Main()
    {
        Console.WriteLine("Hello World! This is the Journal Project.");
        
        while (true)
        {
            Console.WriteLine("\nMenu:");
            Console.WriteLine("1. Write a new entry");
            Console.WriteLine("2. Display the journal");
            Console.WriteLine("3. Save the journal to a file");
            Console.WriteLine("4. Load the journal from a file");
            Console.WriteLine("5. Exit");
            Console.Write("Choose an option: ");
            
            string choice = Console.ReadLine();
            switch (choice)
            {
                case "1": WriteEntry(); break;
                case "2": DisplayJournal(); break;
                case "3": SaveJournal(); break;
                case "4": LoadJournal(); break;
                case "5": return;
                default: Console.WriteLine("Invalid option. Try again."); break;
            }
        }
    }
    
    static void WriteEntry()
    {
        string prompt = prompts[random.Next(prompts.Length)];
        Console.WriteLine($"\nPrompt: {prompt}");
        Console.Write("Your response: ");
        string response = Console.ReadLine();
        
        journal.Add(new JournalEntry(DateTime.Now.ToShortDateString(), prompt, response));
    }
    
    static void DisplayJournal()
    {
        Console.WriteLine("\nJournal Entries:");
        if (journal.Count == 0)
        {
            Console.WriteLine("No entries found.");
            return;
        }
        
        foreach (var entry in journal)
        {
            Console.WriteLine($"{entry.Date} - {entry.Prompt}\n{entry.Response}\n");
        }
    }
    
    static void SaveJournal()
    {
        Console.Write("Enter filename to save (e.g., journal.csv): ");
        string filename = Console.ReadLine();

        try
        {
            using (StreamWriter writer = new StreamWriter(filename))
            {
                foreach (var entry in journal)
                {
                    writer.WriteLine($"\"{entry.Date}\",\"{entry.Prompt.Replace("\"", "\"\"")}\",\"{entry.Response.Replace("\"", "\"\"")}\"");
                }
            }
            Console.WriteLine("Journal saved successfully.");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error saving journal: {ex.Message}");
        }
    }
    
    static void LoadJournal()
    {
        Console.Write("Enter filename to load (e.g., journal.csv): ");
        string filename = Console.ReadLine();
        
        if (!File.Exists(filename))
        {
            Console.WriteLine("File not found.");
            return;
        }

        try
        {
            journal.Clear();
            using (StreamReader reader = new StreamReader(filename))
            {
                string line;
                while ((line = reader.ReadLine()) != null)
                {
                    string[] parts = line.Split("\",\"");
                    if (parts.Length >= 3)
                    {
                        string date = parts[0].Trim('"');
                        string prompt = parts[1].Trim('"');
                        string response = parts[2].Trim('"');
                        journal.Add(new JournalEntry(date, prompt, response));
                    }
                }
            }
            Console.WriteLine("Journal loaded successfully.");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error loading journal: {ex.Message}");
        }
    }
}

class JournalEntry
{
    public string Date { get; }
    public string Prompt { get; }
    public string Response { get; }
    
    public JournalEntry(string date, string prompt, string response)
    {
        Date = date;
        Prompt = prompt;
        Response = response;
    }
}

