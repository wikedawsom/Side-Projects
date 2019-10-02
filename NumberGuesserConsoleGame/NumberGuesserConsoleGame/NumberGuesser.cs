/*  Author:             Christian Harris
 *  Date of last edit:  Oct. 2, 2019
 *  Description:        Backend logic for a simple number guessing game
 */

using System;

namespace NumberGuesserConsoleGame
{
    public class NumberGuesser
    {
        private int _tempMinNum;
        private int _tempMaxNum;
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
                return (_tempMaxNum - _tempMinNum) / 2 + _tempMinNum;
            }
        }

        public NumberGuesser()
        {
            ChosenNum = RandNumInRange;
        }

        public NumberGuesser(int maxNum)
        {
            MaxNum = maxNum;
            _tempMinNum = MinNum;
            _tempMaxNum = MaxNum;

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

        public string CheckComputerGuess(char higherLowerEqual) // For when computer is guessing
        {
            string output = "No info given, thanks for nothing.";

            if (higherLowerEqual == 'h')
            {
                output = "I will guess higher.";
                _tempMinNum = GuessNum + 1;
            }
            else if (higherLowerEqual == 'l')
            {
                output = "I will guess lower.";
                _tempMaxNum = GuessNum;
            }
            else if (higherLowerEqual == 'e')
            {
                output = "I win";
                _tempMinNum = MinNum;
                _tempMaxNum = MaxNum;
            }
            if (_tempMaxNum <= _tempMinNum)
            {
                output = "You're a cheater.";
                _tempMinNum = MinNum;
                _tempMaxNum = MaxNum;
            }
            return output;
        }
    }
}
