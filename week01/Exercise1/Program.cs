using System;

class Program
{
    static void Main(string[] args)
    {
        Console.WriteLine("Hello World! This is the Exercise1 Project.");

        // Prompt the user for their first and last name
        Console.Write("What is your first name? ");
        string firstName = Console.ReadLine();

        Console.Write("What is your last name? ");
        string lastName = Console.ReadLine();

        // Display the name in the required format
        Console.WriteLine($"Your name is {lastName}, {firstName} {lastName}.");
    }
}