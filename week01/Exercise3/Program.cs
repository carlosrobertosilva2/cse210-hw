using System;

class Program
{
    static void Main(string[] args)
    {
        Random randomGenerator = new Random();
        string playAgain = "yes";

        while (playAgain == "yes")
        {
            int magicNumber = randomGenerator.Next(1, 101);
            int guess = 0;
            int guessCount = 0;

            while (guess != magicNumber)
            {
                Console.Write("What is your guess? ");
                guess = int.Parse(Console.ReadLine() ?? "0");
                guessCount++;

                if (guess < magicNumber)
                {
                    Console.WriteLine("Higher");
                }
                else if (guess > magicNumber)
                {
                    Console.WriteLine("Lower");
                }
                else
                {
                    Console.WriteLine("You guessed it!");
                }
            }

            Console.WriteLine($"It took you {guessCount} guesses.");
            Console.Write("Would you like to play again (yes/no)? ");
            playAgain = (Console.ReadLine() ?? "no").ToLower();

            Console.WriteLine();
        }
    }
}
