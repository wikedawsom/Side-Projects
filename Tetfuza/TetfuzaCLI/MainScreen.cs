using System;
using System.Collections.Generic;
using System.Text;
using Tetfuza;
using System.Threading;
using FramerateStabilizer;
using System.Diagnostics;

namespace TetfuzaCLI
{
    public class MainScreen
    {
        private TetfuzaBackend game;
        private long _score;
        private Stopwatch _timer = new Stopwatch();
        private string _cheatCode;
        private bool ValidCode
        {
            get
            {
                return _cheatCode == "up" || _cheatCode == "revenge" || _cheatCode == "impossible";
            }
        }
        /// <summary>
        /// Main CLI Loop
        /// </summary>
        public void StartCLI()
        {
            bool exit = false;
            while (!exit)
            {
                _score = -1;
//                Console.SetWindowSize(60, 40);
                int startLevel = MainMenu();

                Console.CursorVisible = false;
                game = new TetfuzaBackend(startLevel);

                // Thread to draw new screen
                Thread displayThread = new Thread(new ThreadStart(this.DisplayScreen));
                displayThread.Priority = ThreadPriority.Highest;
                displayThread.Start();

                // game loop. returns when user tops out
                _score = game.Run();

                // display final game information & ask to continue
                Console.ReadLine();
                Console.Clear();
                CenterText("Your final score is: "+ CommasInNumber(_score));
                CenterText("Continue (y/n)?");
                ConsoleKey key = Console.ReadKey().Key;
                exit = (key == ConsoleKey.N);
            }
        }

        /// <summary>
        /// Allow user to choose to start on any level between 0 and 19
        /// </summary>
        /// <returns></returns>
        private int MainMenu()
        {
            int startLevel = -1;
            bool validInput = false;
            CenterText("Press enter to clear the console");
            _cheatCode = Console.ReadLine().ToLower();
            Console.Clear();
            while (!validInput)
            {
                CenterText("Which level do you want to start on? (0 - 19): ");
                string input = Console.ReadLine();
                validInput = (int.TryParse(input, out startLevel) && startLevel >= 0 && startLevel <= 19);
            }
            if (_cheatCode == "impossible")
            {
                startLevel = 29;
            }
            return startLevel;
        }

        /// <summary>
        /// Constantly updates the console window with the current visual state of the current game
        /// </summary>
        private void DisplayScreen()
        {
            Console.Clear();
            while (_score == -1)
            {
                Console.SetCursorPosition(0, 0);
                Console.CursorVisible = false;
                if (ValidCode)
                {
                    CenterText("Cheat Code: " + _cheatCode);
                }
                CenterText("Current Score ");
                CenterText(CommasInNumber(game.Score));
                CenterText("Lines Cleared: " + game.Lines);
                CenterText("Current Level: " + game.Level);

                // Convert the piece to an array of strings where the characters
                // match those on the board
                DrawNextPiece();
                DrawBoard();

                //StableFrames.Stabilize(17, timer);
            }
            CenterText("Oh no, you topped out... please press enter");
        }

        private void CenterText(string text)
        {
            int width = Console.WindowWidth;
            int spacePadding = (width - text.Length) / 2;
            int fullStringWidth = spacePadding + text.Length;
            String formatString = "{0, " + fullStringWidth.ToString() + "}";
            String centeredText = String.Format(formatString, text);
            Console.WriteLine(centeredText);
        }
        /// <summary>
        /// Displays the upcoming piece
        /// </summary>
        private void DrawNextPiece()
        {
            if (game.AfterPiece != null)
            {
                string[] pieceView = game.AfterPiece.ToString().Replace(FuzaPiece.FUZA_CHAR, TetfuzaBackend.MOVING_CHAR).Split(")");
                CenterText("Next Piece:");
                for (int i = 0; i < pieceView.Length - 1; i++)
                {
                    int location = i;
                    if (_cheatCode.ToLower() == "up") // A small Easter Egg to make the pieces appear upsidedown
                        location = pieceView.Length - 2 - i;
                    CenterText(pieceView[location]);
                }
            }

        }
        private void DrawBoard()
        {
            // Convert game board to an array of strings 
            // and convert all block characters to appear the same
            string[] boardView = game.ToString().Replace(TetfuzaBackend.LOCKDOWN_CHAR, TetfuzaBackend.MOVING_CHAR).Split(Environment.NewLine);
            CenterText("-----------------------");
            // Loop through array and have each line centered on the screen
            for (int i = 0; i < boardView.Length - 1; i++)
            {
                int location = i;
                if (_cheatCode.ToLower() == "up") // A small Easter Egg to make the pieces fall up
                    location = boardView.Length - 2 - i;
                CenterText("| " + boardView[location] + "|");
            }
            CenterText("-----------------------");
            CenterText(" ");
            CenterText("Z and X to rotate");
            CenterText("Arrow keys to move (left, right, down)");
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
                formattedNum += "," + number.Substring(((number.Length % 3)+i*3), 3);
            }
            if (_cheatCode == "revenge") // NO, NO, NO! YOU WILL DIE!!!
                formattedNum = "UNLLLLLIMITED POWEEERRRRRR!!!!";
            return formattedNum;
        }
    }

}
