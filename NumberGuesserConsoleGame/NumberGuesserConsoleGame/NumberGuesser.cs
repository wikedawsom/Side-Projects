using System;
using System.Collections.Generic;
using System.Text;

namespace NumberGuesserConsoleGame
{
    public class NumberGuesser
    {
        public int MinNum { get; private set; } = 0;
        public int MaxNum { get; private set; } = 100;
        private int ChosenNum { get; set; }
        private int RandNumInRange
        {
            get
            {
                Random rand = new Random();
                return rand.Next(MinNum,MaxNum+1);
            }
        }
        public int GuessNum
        {
            get
            {
                return (MaxNum - MinNum) / 2 + MinNum;
            }
        }

        public NumberGuesser()
        {
            ChosenNum = RandNumInRange;
        }

        public NumberGuesser(int maxNum)
        {
            MaxNum = maxNum;
            ChosenNum = RandNumInRange;
        }

        public void PickNewRandNum()
        {
            ChosenNum = RandNumInRange;
        }

        public string CheckUserGuess(int guessNum) // For when user is guessing
        {
            string output = "too high";
            if (guessNum < ChosenNum)
            {
                output = "too low";
            }
            else if(guessNum == ChosenNum)
            {
                output = "correct";
            }
            return output;
        }

        public string CheckComputerGuess(string higherOrLower)
        {
            string output = "I will guess again.";

            if (higherOrLower == "higher")
            {
                output = "I will guess higher.";
                MinNum = GuessNum + 1;
            }
            else if (higherOrLower == "lower")
            {
                output = "I will guess lower.";
                MaxNum = GuessNum;
            }
            if (MaxNum <= MinNum)
            {
                output = "You're a cheater.";
            }
            return output;
        }
    }
}
