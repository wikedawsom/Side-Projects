using System;
using System.Collections.Generic;
using System.Text;
using Tetfuza;
using Tetfuza.Interfaces;

namespace TetfuzaCLI
{
    public class SystemConsoleDisplay : IDisplay
    {
        public void DrawGameplayScreen(TetfuzaBackend gameInfo)
        {
            RedrawFrame();
            Console.CursorVisible = false;

            //WriteText("Level: " + gameState.Level,1m,1m);
            //WriteText("Score: " + CommasInNumber(gameState.Score), 1m, 1m);
            //DrawNextPiece(gameState.AfterPiece);


            // Convert playField string to an array of strings 
            // and convert all block characters to appear the same
            string[] board = gameInfo.ToString().Split(")");

            // Display Board
            DrawBoardCentered(board);

            // Display Next Piece
            PreviewNextPiece(gameInfo.NextPiece, .25m, .5m);

            // Display Score
            Console.CursorTop = 22;
            DisplayStat("Score", CommasInNumber(gameInfo.Score));

            // Display Level
            DisplayStat("Level", gameInfo.Level.ToString());

            // Display Line Count
            DisplayStat("Lines", gameInfo.Lines.ToString());

            // Display Next Piece
        }
        private void DisplayStat(string title, string stat)
        {
            string score = title + ": " + stat;
            Console.CursorLeft = Console.WindowWidth / 2 - score.Length / 2;
            Console.Write(title + ": ");
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine(stat);
            Console.ForegroundColor = ConsoleColor.Gray;
        }
        /// <summary>
        /// Draw the board in a fancy way, and keep it centered on the screen
        /// </summary>
        /// <param name="board">A string array representing all the rows of the tetris board</param>
        private void DrawBoardCentered(string [] board)
        {
            // Draw Top Bar
            
            Console.BackgroundColor = ConsoleColor.DarkMagenta;
            int cursorPosition = Console.WindowWidth / 2 - 12;
            if (cursorPosition >= 0)
                Console.CursorLeft = cursorPosition;
            Console.WriteLine("                        ");
            Console.BackgroundColor = ConsoleColor.Black;

            // Loop through array and have each line centered on the screen
            for (int row = 0; row < board.Length - 1; row++)
            {
                cursorPosition = Console.WindowWidth / 2 - board[row].Length / 2 - 7;
                if (cursorPosition >= 0)
                    Console.CursorLeft = cursorPosition;

                Console.BackgroundColor = ConsoleColor.DarkMagenta;
                Console.Write("  ");
                Console.BackgroundColor = ConsoleColor.Black;
                for (int col = 0; col < board[row].Length; col++)
                {
                    SelectBlockColor(board[row][col]);
                    Console.Write("  ");
                }

                Console.BackgroundColor = ConsoleColor.DarkMagenta;
                Console.WriteLine("  ");
                Console.BackgroundColor = ConsoleColor.Black;
            }

            // Draw Bottom Bar
            Console.BackgroundColor = ConsoleColor.DarkMagenta;
            cursorPosition = Console.WindowWidth / 2 - 12;
            if (cursorPosition >= 0)
                Console.CursorLeft = cursorPosition;
            Console.WriteLine("                        ");
            Console.BackgroundColor = ConsoleColor.Black;
        }

        private void SelectBlockColor(char squareChar)
        {
            // Draw the appropriate colored space
            switch (squareChar)
            {
                case '1':
                    Console.BackgroundColor = ConsoleColor.White;
                    break;
                case '2':
                    Console.BackgroundColor = ConsoleColor.Red;
                    break;
                case '3':
                    Console.BackgroundColor = ConsoleColor.Blue;
                    break;
                case '4':
                    Console.BackgroundColor = ConsoleColor.Cyan;
                    break;
                default:
                    Console.BackgroundColor = ConsoleColor.Black;
                    break;
            }
        }
        

        public void WriteText(string text, decimal xPos, decimal yPos)
        {
            //int width = Console.WindowWidth;
            Console.CursorLeft = (int)(xPos * Console.WindowWidth) - text.Length / 2;
            Console.CursorTop = (int)(yPos * Console.WindowHeight);
            //int spacePadding = (int)((width - text.Length) / 2 - 1);
            //int fullStringWidth = spacePadding + text.Length;
            //String formatString = "  {0, " + fullStringWidth.ToString() + "}  "; // Extra characters to overwrite any ghost characters
            //String centeredText = String.Format(formatString, text);
            Console.WriteLine(text);
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
        public void PreviewNextPiece(FuzaPiece nextPiece, decimal xPos, decimal yPos)
        {
            Console.CursorTop = (int)(yPos * Console.WindowHeight);

            string[] pieceView = nextPiece.ToString().Split(")");
            WriteText("Next Piece:", xPos, yPos);
            for (int i = 0; i < pieceView.Length - 1; i++)
            {
                Console.CursorLeft = (int)(xPos * Console.WindowWidth) - pieceView.Length;
                for (int j = 0; j < pieceView[i].Length; j++)
                {
                    SelectBlockColor(pieceView[i][j]);
                    Console.Write("  ");
                    Console.BackgroundColor = ConsoleColor.Black;
                }
                Console.WriteLine();
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
