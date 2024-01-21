using System;
using System.Collections.Generic;
using System.IO;

class JournalEntry
{
    public string Prompt { get; set; }
    public string Response { get; set; }
    public string Date { get; set; }

    public JournalEntry(string prompt, string response)
    {
        Prompt = prompt;
        Response = response;
        Date = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
    }

    public override string ToString()
    {
        return $"[{Date}] Prompt: {Prompt}\nResponse: {Response}\n";
    }

    public string ToCsvString()
    {
        return $"{Date},{Prompt},{Response}";
    }
}

class Journal
{
    private List<JournalEntry> entries = new List<JournalEntry>();

    public void AddEntry(string prompt, string response)
    {
        JournalEntry entry = new JournalEntry(prompt, response);
        entries.Add(entry);
    }

    public void DisplayEntries()
    {
        foreach (JournalEntry entry in entries)
        {
            Console.WriteLine(entry);
        }
    }

    public void SaveToFile(string filename)
    {
        using (StreamWriter sw = new StreamWriter(filename))
        {
            foreach (JournalEntry entry in entries)
            {
                sw.WriteLine(entry.ToCsvString());
            }
        }
    }

    public void LoadFromFile(string filename)
    {
        entries.Clear();
        using (StreamReader sr = new StreamReader(filename))
        {
            string line;
            while ((line = sr.ReadLine()) != null)
            {
                string[] parts = line.Split(',');
                if (parts.Length == 3)
                {
                    JournalEntry entry = new JournalEntry(parts[1], parts[2]);
                    entry.Date = parts[0];
                    entries.Add(entry);
                }
            }
        }
    }
}

class JournalProgram
{
    static void Main()
    {
        Journal journal = new Journal();

        while (true)
        {
            Console.WriteLine("1. Write a new entry");
            Console.WriteLine("2. Display the journal");
            Console.WriteLine("3. Save the journal to a file");
            Console.WriteLine("4. Load the journal from a file");
            Console.WriteLine("5. Exit");
            Console.Write("Choose an option: ");

            string choice = Console.ReadLine();

            switch (choice)
            {
                case "1":
                    WriteNewEntry(journal);
                    break;

                case "2":
                    DisplayJournal(journal);
                    break;

                case "3":
                    SaveJournalToFile(journal);
                    break;

                case "4":
                    LoadJournalFromFile(journal);
                    break;

                case "5":
                    Environment.Exit(0);
                    break;

                default:
                    Console.WriteLine("Invalid choice. Please try again.");
                    break;
            }
        }
    }

    static void WriteNewEntry(Journal journal)
    {
        Console.WriteLine("Choose a prompt:");
        Console.WriteLine("1. Who was the most interesting person I interacted with today?");
        Console.WriteLine("2. What was the best part of my day?");
        Console.WriteLine("3. How did I see the hand of the Lord in my life today?");
        Console.WriteLine("4. What was the strongest emotion I felt today?");
        Console.WriteLine("5. If I had one thing I could do over today, what would it be?");
        Console.Write("Enter the prompt number: ");
        int promptNumber = int.Parse(Console.ReadLine());
        string prompt = GetPromptByNumber(promptNumber);
        Console.Write("Enter your response: ");
        string response = Console.ReadLine();
        journal.AddEntry(prompt, response);
    }

    static void DisplayJournal(Journal journal)
    {
        Console.WriteLine("Journal Entries:");
        journal.DisplayEntries();
    }

    static void SaveJournalToFile(Journal journal)
    {
        Console.Write("Enter the filename to save the journal: ");
        string saveFilename = Console.ReadLine();
        journal.SaveToFile(saveFilename);
        Console.WriteLine("Journal saved successfully.");
    }

    static void LoadJournalFromFile(Journal journal)
    {
        Console.Write("Enter the filename to load the journal: ");
        string loadFilename = Console.ReadLine();
        journal.LoadFromFile(loadFilename);
        Console.WriteLine("Journal loaded successfully.");
    }

    static string GetPromptByNumber(int number)
    {
        switch (number)
        {
            case 1:
                return "Who was the most interesting person I interacted with today?";
            case 2:
                return "What was the best part of my day?";
            case 3:
                return "How did I see the hand of the Lord in my life today?";
            case 4:
                return "What was the strongest emotion I felt today?";
            case 5:
                return "If I had one thing I could do over today, what would it be?";
            default:
                return "Invalid prompt";
        }
    }
}
