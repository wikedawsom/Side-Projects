using System;

namespace NumberGuesserConsoleGame
{
    class Program
    {
        static void Main(string[] args)
        {
            //Console.WriteLine("Hello World!");
            var game = new NumberGuesserConsoleClient();

            // Player picks a number, computer guesses
            Console.Clear();
            int guesses = game.PlayerChosenNumber();
            DisplayGuessCount(guesses);
            Console.ReadKey();

            // Computer picks a number, player guesses
            Console.Clear();
            game.ComputerChosenNumber();
            DisplayGuessCount(guesses);
            Console.ReadKey();
            
            Console.WriteLine("***Program end, press any key to exit***");
            Console.ReadKey();
        }
        private static void DisplayGuessCount(int guesses)
        {
            if (guesses == 1)
            {
                Console.WriteLine($"Number was guessed on the first try!");
            }
            else
            {
                Console.WriteLine($"It took {guesses} tries to guess the number.");
            }
        }
    }
}
