using System;

class Program
{
    static void Main(string[] args)
    {
        Journal journal = new Journal();
        PromptGenerator prompts = new PromptGenerator();

        int choice = 0;

        while (choice != 5)
        {
            Console.WriteLine();
            Console.WriteLine("Journal Menu");
            Console.WriteLine("1. Write");
            Console.WriteLine("2. Display");
            Console.WriteLine("3. Load");
            Console.WriteLine("4. Save");
            Console.WriteLine("5. Quit");
            Console.Write("Choose an option: ");

            choice = int.Parse(Console.ReadLine());

            switch (choice)
            {
                case 1:

                    Entry entry = new Entry();

                    entry._date = DateTime.Now.ToShortDateString();

                    entry._promptText = prompts.GetRandomPrompt();

                    Console.WriteLine();
                    Console.WriteLine(entry._promptText);

                    Console.Write("> ");

                    entry._entryText = Console.ReadLine();

                    journal.AddEntry(entry);

                    break;

                case 2:

                    journal.DisplayAll();

                    break;

                case 3:

                    Console.Write("Filename: ");

                    string loadFile = Console.ReadLine();

                    journal.LoadFromFile(loadFile);

                    break;

                case 4:

                    Console.Write("Filename: ");

                    string saveFile = Console.ReadLine();

                    journal.SaveToFile(saveFile);

                    break;

                case 5:

                    Console.WriteLine("Goodbye!");

                    break;

                default:

                    Console.WriteLine("Invalid option.");

                    break;
            }
        }
    }
}
