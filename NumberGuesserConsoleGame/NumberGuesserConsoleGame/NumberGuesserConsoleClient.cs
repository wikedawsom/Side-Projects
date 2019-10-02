/*  Author:             Christian Harris
 *  Date of last edit:  Oct. 2, 2019
 *  Description:        I/O logic and input sanitization for NumberGuesser in a console UI
 */
using System;

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
                    
                }
                guessCount++;
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

                Console.Write("Is your number higher, lower, or euqal? (h/l/e): ");
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
        public void Start()
        {
            bool exit = false;
            while (!exit)
            {
                int guesses = 0;
                Console.Write("Would you like to choose a number? (y/n): ");
                char selectionYN = (Console.ReadLine().ToLower() + "y")[0];
                if (selectionYN == 'y')
                {
                    // Player picks a number, computer guesses
                    Console.Clear();
                    guesses = PlayerChosenNumber();
                    DisplayGuessCount(guesses);
                }
                else
                {
                    // Computer picks a number, player guesses
                    Console.Clear();
                    guesses = ComputerChosenNumber();
                    DisplayGuessCount(guesses);
                }
                Console.Write("Continue? (y/n): ");
                selectionYN = (Console.ReadLine().ToLower() + "n")[0];
                if (selectionYN != 'y')
                {
                    exit = true;
                }
            }
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
