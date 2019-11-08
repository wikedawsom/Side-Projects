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
                _timer.Start();
                Thread userInputThread = new Thread(new ThreadStart(this.InputListener));
                userInputThread.Start();
                Thread displayThread = new Thread(new ThreadStart(this.DisplayScreen));
                displayThread.Priority = ThreadPriority.Highest;
                displayThread.Start();
                _score = game.Run();

                Console.ReadLine();
                Console.Clear();
                DrawBoard();
                CenterText("Your final score is: "+ CommasInNumber(_score));
                string p1Continue = "y";//Console.ReadLine().ToLower();
                if (p1Continue != null && p1Continue != "" && p1Continue[0] == 'n')
                {
                    exit = true;
                }
                else
                {
                    exit = false;
                }
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
        /// Really crappy input listener that only detects one key at a time. 
        /// </summary>
        private void InputListener()
        {
            while(_score == -1)
            {
                int rotation = 0;
                int direction = 0;
                bool down = false;
                ConsoleKey key = Console.ReadKey().Key;

                switch (key)
                {
                    case ConsoleKey.LeftArrow:
                        direction = -1;
                        break;
                    case ConsoleKey.RightArrow:
                        direction = 1;
                        break;
                    case ConsoleKey.Z:
                        rotation = -1;
                        break;
                    case ConsoleKey.X:
                        rotation = 1;
                        break;
                    case ConsoleKey.DownArrow:
                        down = true;
                        break;
                    case ConsoleKey.C:
                        Console.Clear();
                        break;
                    default:
                        break;
                }

                game.SendInput(direction, rotation, down);
            }
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
            int textBeginIndex = (width / 2) - (text.Length / 2);
            for (int space = 0; space < textBeginIndex; space++)
            {
                Console.Write(" ");
                text += " ";
            }
            Console.WriteLine(text);
        }
        /// <summary>
        /// Displays the upcoming piece
        /// </summary>
        private void DrawNextPiece()
        {
            string[] pieceView = game.AfterPiece.ToString().Replace(FuzaPiece.FUZA_CHAR, TetfuzaBackend.MOVING_CHAR).Split("\n");
            CenterText("Next Piece:");
            for (int i = 0; i < pieceView.Length - 1; i++)
            {
                int location = i;
                if (_cheatCode.ToLower() == "up") // A small Easter Egg to make the pieces appear upsidedown
                    location = pieceView.Length - 2 - i;
                CenterText(pieceView[location]);
            }
        }
        private void DrawBoard()
        {
            // Convert game board to an array of strings 
            // and convert all block characters to appear the same
            string[] boardView = game.ToString().Replace(TetfuzaBackend.LOCKDOWN_CHAR, TetfuzaBackend.MOVING_CHAR).Split("\n");
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
