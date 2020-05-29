using System;
using System.Collections.Generic;
using System.Text;
using Tetfuza.Interfaces;
using static Tetfuza.Interfaces.IInput;

namespace Tetfuza
{
    public class TetfuzaMenu
    {
        private IInput _input;
        private IDisplay _display;
        public TetfuzaMenu(IInput inputDevice, IDisplay screen)
        {
            _input = inputDevice;
            _display = screen;
        }
        /// <summary>
        /// Main entry point. Provides a menu for player to choose starting level and save scores.
        /// </summary>
        /// <returns></returns>
        public int MainMenu()
        {
            bool exit = false;
            while (!exit)
            {
                long score;

                int startLevel = ChooseStartingLevel();
                TetfuzaBackend currentGame = new TetfuzaBackend(_input, _display, startLevel);
                score = currentGame.Run();

                _display.WriteText("Oh no, you topped out... please press start to proceed", .5m, .5m);
                
                while (_input.ReadInput() != Input.Pause);

                // display final game information & ask to continue

                _display.ClearScreen();
                _display.WriteText("Your final score is: " + CommasInNumber(score), .5m, .55m);

                SaveScore(score);

                _display.WriteText("Press start to exit, any other button to play again", .5m, .6m);


                // Wait for input and end program if start was pressed

                exit = (_input.ReadInput() == Input.Pause);
            }
            return 1;
        }

        /// <summary>
        /// Prompt user to choose which level to start on
        /// </summary>
        /// <returns>Chosen starting level</returns>
        private int ChooseStartingLevel()
        {
            int currentSelection = 0;
            Input input;
            do
            {
                DisplayLevels(currentSelection);
                input = _input.ReadInput();

                switch (input)
                {
                    case Input.Left:
                        currentSelection -= 1;
                        if (currentSelection < 0)
                            currentSelection = 0;
                        break;
                    case Input.Right:
                        currentSelection += 1;
                        if (currentSelection > 9)
                            currentSelection = 9;
                        break;
                    case Input.Up:
                        currentSelection -= 5;
                        if (currentSelection < 0)
                            currentSelection += 5;
                        break;
                    case Input.Down:
                        currentSelection += 5;
                        if (currentSelection > 9)
                            currentSelection -= 5;
                        break;
                    default:
                        break;
                }
                
            } while (input != Input.Pause);
            return currentSelection;
        }
        private void DisplayLevels(int selection)
        {
            List<int> possibleLevels = new List<int>() { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9 };
            string levelMenuDisplay = "";
            for (int i = 0; i < possibleLevels.Count; i++ )
            {
                char cursor = ' ';
                if (selection == possibleLevels[i])
                {
                    cursor = '*';
                }
                levelMenuDisplay += cursor + possibleLevels[i].ToString();
                if (i == (possibleLevels.Count-1) / 2)
                    levelMenuDisplay += ")";
            }

            _display.RedrawFrame();
            _display.WriteText("Press start to choose a starting level", .5m, 0m);

            string[] levelDisplayLines = levelMenuDisplay.Split(")");
            decimal yOffset = 0;
            foreach (string line in levelDisplayLines)
            {
                _display.WriteText(line, .5m, .04m + yOffset);
                yOffset += .05m;
            }
        }
        private void SaveScore(long score)
        {
            // Implement a method to save the given score in long term storage
        }

        /// <summary>
        /// Converts a large number to a string with normal comma separation
        /// </summary>
        /// <param name="num"></param>
        /// <returns></returns>
        public string CommasInNumber(long num)
        {
            string number = num.ToString();
            int numCommas = (number.Length - 1) / 3;
            string formattedNum = "";
            if (numCommas == 0)
            {
                formattedNum = number;
            }
            else
            {
                formattedNum += number.Substring(0, number.Length % 3);
            }
            for (int i = 0; i < numCommas; i++)
            {
                formattedNum += "," + number.Substring(((number.Length % 3) + i * 3), 3);
            }
            return formattedNum;
        }
    }
}
