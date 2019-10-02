using System;
using System.Collections.Generic;
using System.Text;

namespace NumberGuesserConsoleGame
{
    public class NumberGuesserConsoleClient
    {
        private NumberGuesser _computer;
        public int MinPossibleNum
        {
            get
            {
                return _computer.MinNum;
            }
        }
        public int MaxPossibleNum
        {
            get
            {
                return _computer.MaxNum;
            }
        }
        
        public NumberGuesserConsoleClient()
        {
            int max = 100;
            Console.Write("Would you like to specify an upper bound for the range of possible numbers? (y/n): ");
            if (Console.ReadLine().ToString().ToLower() == "y")
            {
                Console.Write("Specify highest possible number: ");
                int.TryParse(Console.ReadLine(), out max);
            }
            _computer = new NumberGuesser(max);
        }

        public int ComputerChosenNumber()
        {
            int guessCount = 0;
            bool exit = false;
            Console.WriteLine($"I'm thinking of a number between {MinPossibleNum} and {MaxPossibleNum}.");
            
            while (!exit)
            {
                Console.Write("Take a guess: ");
                int playerGuess = -1;
                if (!int.TryParse(Console.ReadLine(), out playerGuess))
                {
                    Console.WriteLine("Invalid Input");
                }
                else if (playerGuess > MaxPossibleNum || playerGuess < MinPossibleNum)
                {
                    Console.WriteLine("Guess is outside possible range");
                }
                else
                {
                    string guessResponse = _computer.CheckUserGuess(playerGuess);
                    Console.WriteLine("Your guess is " + guessResponse + "!");
                    if (guessResponse == "correct")
                    {
                        exit = true;
                        Console.WriteLine("You win!");
                    }
                    else 
                    {
                        Console.WriteLine("Keep trying :)");
                    }
                    guessCount++;
                }
            }
            return guessCount;
        }
        public int PlayerChosenNumber()
        {
            int guessCount = 0;
            bool exit = false;
            Console.WriteLine($"Think of a number between {_computer.MinNum} and {_computer.MaxNum}.");
            
            while (!exit)
            {
                int computerGuess = _computer.GuessNum;
                Console.WriteLine("My guess is: " + computerGuess);

                Console.WriteLine("Is your number higher, lower, or euqal? (h/l/e): ");
                char playerResponse = (Console.ReadLine()+"e")[0];

                string computerResponse = _computer.CheckComputerGuess(playerResponse);
                Console.WriteLine(computerResponse);
                if (computerResponse == "I win" || computerResponse == "You're a cheater.")
                {
                    exit = true;
                }
                guessCount++;
            }
            return guessCount;
        }
        
    }
}
