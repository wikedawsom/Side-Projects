using System;
using System.Collections.Generic;
using System.Text;
using Tetfuza;
using Tetfuza.Interfaces;

namespace TetfuzaCLI
{
    public class SystemConsoleDisplay : IDisplay
    {
        public void DrawBoard(string board)
        {
            Console.CursorVisible = false;
            // Convert game board to an array of strings 
            // and convert all block characters to appear the same
            string[] boardView = board.Replace(TetfuzaBoard.LOCKDOWN_CHAR, TetfuzaBoard.MOVING_CHAR).Split(")");
            WriteText("-----------------------");
            // Loop through array and have each line centered on the screen
            for (int i = 0; i < boardView.Length - 1; i++)
            {
                int location = i;
                //if (_cheatCode.ToLower() == "up") // A small Easter Egg to make the pieces fall up
                //    location = boardView.Length - 2 - i;
                WriteText("| " + boardView[location] + "|");
            }
            WriteText("-----------------------");
            WriteText(" ");
            WriteText("Z and X to rotate");
            WriteText("Arrow keys to move (left, right, down)");
        }

        public void WriteText(string text)
        {
            int width = Console.WindowWidth;
            int textBeginIndex = (width / 2) - (text.Length / 2) - 1;
            for (int space = 0; space < textBeginIndex; space++)
            {
                Console.Write(" ");
                text += " ";
            }
            Console.WriteLine(text);
        }

        /// <summary>
        /// Allow user to choose to start on any level between 0 and 19
        /// </summary>
        /// <returns></returns>
        public int DrawMenu(string prompt)
        {
            Console.CursorVisible = false;
            int startLevel = -1;
            bool validInput = false;
            //_cheatCode = Console.ReadLine().ToLower();
            ClearScreen();
            do
            {
                WriteText(prompt);
                string input = Console.ReadLine();
                validInput = (int.TryParse(input, out startLevel) && startLevel >= 0 && startLevel <= 19);
            } while (!validInput);
            //if (_cheatCode == "impossible")
            //{
            //    startLevel = 29;
            //}
            return startLevel;
        }

        public void ClearScreen()
        {
            Console.Clear();
        }

        public void RedrawFrame()
        {
            Console.SetCursorPosition(0, 0);
        }
    }
}
