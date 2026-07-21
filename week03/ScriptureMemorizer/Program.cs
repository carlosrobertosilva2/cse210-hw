using System;

class Program
{
    static void Main(string[] args)
    {
        // Create a scripture library
        List<Scripture> scriptureLibrary = CreateScriptureLibrary();
        
        // Select a random scripture
        Random random = new Random();
        Scripture currentScripture = scriptureLibrary[random.Next(scriptureLibrary.Count)];
        
        // Display the complete scripture
        Console.Clear();
        Console.WriteLine(currentScripture.GetDisplayText());
        Console.WriteLine($"\nWords remaining: {currentScripture.GetVisibleWordCount()}/{currentScripture.GetTotalWordCount()}");
        Console.WriteLine("\nPress ENTER to hide words or type 'quit' to exit.");

        // Main program loop
        while (true)
        {
            string input = Console.ReadLine();
            
            // Check if user wants to quit
            if (input.ToLower() == "quit")
            {
                break;
            }
            
            // Hide random words (hide 3 at a time)
            currentScripture.HideRandomWords(3);
            
            // Clear console and display updated scripture
            Console.Clear();
            Console.WriteLine(currentScripture.GetDisplayText());
            Console.WriteLine($"\nWords remaining: {currentScripture.GetVisibleWordCount()}/{currentScripture.GetTotalWordCount()}");
            
            // Check if all words are hidden
            if (currentScripture.IsCompletelyHidden())
            {
                Console.WriteLine("\n✨ All words are hidden! You've memorized the scripture! ✨");
                Console.WriteLine("Press any key to exit...");
                Console.ReadKey();
                break;
            }
            
            Console.WriteLine("\nPress ENTER to hide more words or type 'quit' to exit.");
        }
    }

    static List<Scripture> CreateScriptureLibrary()
    {
        List<Scripture> library = new List<Scripture>();
        
        // Add multiple scriptures
        Reference ref1 = new Reference("John", 3, 16);
        Scripture scripture1 = new Scripture(ref1, "For God so loved the world that he gave his one and only Son that whoever believes in him shall not perish but have eternal life");
        library.Add(scripture1);
        
        Reference ref2 = new Reference("Proverbs", 3, 5, 6);
        Scripture scripture2 = new Scripture(ref2, "Trust in the Lord with all your heart and lean not on your own understanding in all your ways submit to him and he will make your paths straight");
        library.Add(scripture2);
        
        Reference ref3 = new Reference("Philippians", 4, 13);
        Scripture scripture3 = new Scripture(ref3, "I can do all things through him who strengthens me");
        library.Add(scripture3);
        
        Reference ref4 = new Reference("Psalm", 23, 1);
        Scripture scripture4 = new Scripture(ref4, "The Lord is my shepherd I shall not want");
        library.Add(scripture4);
        
        Reference ref5 = new Reference("Romans", 8, 28);
        Scripture scripture5 = new Scripture(ref5, "And we know that in all things God works for the good of those who love him who have been called according to his purpose");
        library.Add(scripture5);
        
        return library;
    }
}
