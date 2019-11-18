using System;
using System.Collections.Generic;
using System.Text;
using Tetfuza;
using Tetfuza.Interfaces;

namespace TetfuzaCLI
{
    public class SystemConsoleDisplay : IDisplay
    {
        public void GameScreen(TetfuzaBackend gameState)
        {
            RedrawFrame();
            Console.CursorVisible = false;

            WriteText("Level: " + gameState.Level);
            WriteText("Score: " + CommasInNumber(gameState.Score));
            DrawNextPiece(gameState.AfterPiece);



            var board = gameState.ToString();
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

        /// <summary>
        /// Displays the upcoming piece
        /// </summary>
        private void DrawNextPiece(FuzaPiece nextPiece)
        {
            
            string[] pieceView = nextPiece.ToString().Replace(FuzaPiece.FUZA_CHAR, TetfuzaBoard.MOVING_CHAR).Split(")");
            WriteText("Next Piece:");
            for (int i = 0; i < pieceView.Length - 1; i++)
            {
                int location = i;
                //if (_cheatCode.ToLower() == "up") // A small Easter Egg to make the pieces appear upsidedown
                //    location = pieceView.Length - 2 - i;
                WriteText(pieceView[location]);
            }
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
