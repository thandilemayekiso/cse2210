using System;
using System.IO;
using System.Collections.Generic;

class Program
{
    static void Main()
    {
        Console.WriteLine("Hello World! This is the Journal Project.");
        Journal journal = new Journal();
        journal.Run();
    }
}

class Journal
{
    private List<Entry> _entries = new List<Entry>();
    
    public void Run()
    {
        // Menu-driven logic here
    }

    public void AddEntry(Entry entry)
    {
        _entries.Add(entry);
    }

    public void SaveToFile(string filename)
    {
        using (StreamWriter outputFile = new StreamWriter(filename))
        {
            outputFile.WriteLine("Date,Prompt,Response");
            foreach (Entry entry in _entries)
            {
                outputFile.WriteLine($"{entry.Date},{entry.Prompt.Replace(",", "\",\"")},{entry.Response.Replace(",", "\",\"")}");
            }
        }
    }

    public void LoadFromFile(string filename)
    {
        if (File.Exists(filename))
        {
            string[] lines = File.ReadAllLines(filename);
            _entries.Clear();
            for (int i = 1; i < lines.Length; i++) // Skip header
            {
                string[] parts = lines[i].Split(",");
                if (parts.Length == 3)
                {
                    _entries.Add(new Entry(parts[0], parts[1], parts[2]));
                }
            }
        }
    }
}

class Entry
{
    public string Date { get; set; }
    public string Prompt { get; set; }
    public string Response { get; set; }

    public Entry(string date, string prompt, string response)
    {
        Date = date;
        Prompt = prompt;
        Response = response;
    }
}
