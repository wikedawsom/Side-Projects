using System;
using System.Collections.Generic;
using System.Text;
using Tetfuza;
using System.Threading;
using FramerateStabilizer;
using System.Diagnostics;
using Tetfuza.Interfaces;

namespace TetfuzaCLI
{
    public class MainScreen
    {

        private IConsole _keyboard;
        private IDisplay _screen;
        private TetfuzaBackend game;
        private long _score;
        private Stopwatch _timer = new Stopwatch();
        private string _cheatCode; // Easter eggs will come back later
        private bool ValidCode
        {
            get
            {
                return _cheatCode == "up" || _cheatCode == "revenge" || _cheatCode == "impossible";
            }
        }

        public MainScreen()
        {
            _keyboard = new SystemConsole();
            _screen = new SystemConsoleDisplay();
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
                // Console.SetWindowSize(60, 40);
                
                int startLevel = _screen.DrawMenu("Choose a starting level (0-19)");


                game = new TetfuzaBackend(_keyboard, _screen, startLevel);

                // game loop. returns when user tops out
                _score = game.Run();

                //CenterText("Oh no, you topped out... please press enter");

                // display final game information & ask to continue
                Console.ReadLine();
                Console.Clear();
                //CenterText("Your final score is: "+ CommasInNumber(_score));
                //CenterText("Continue (y/n)?");
                ConsoleKey key = Console.ReadKey().Key;
                exit = (key == ConsoleKey.N);
            }
        }

        


        
        /// <summary>
        /// Displays the upcoming piece
        /// </summary>
        private void DrawNextPiece()
        {
            if (game.AfterPiece != null)
            {
                string[] pieceView = game.AfterPiece.ToString().Replace(FuzaPiece.FUZA_CHAR, TetfuzaBoard.MOVING_CHAR).Split(")");
                _screen.WriteText("Next Piece:");
                for (int i = 0; i < pieceView.Length - 1; i++)
                {
                    int location = i;
                    if (_cheatCode.ToLower() == "up") // A small Easter Egg to make the pieces appear upsidedown
                        location = pieceView.Length - 2 - i;
                    _screen.WriteText(pieceView[location]);
                }
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
                formattedNum += "," + number.Substring(((number.Length % 3)+i*3), 3);
            }
            if (_cheatCode == "revenge") // NO, NO, NO! YOU WILL DIE!!!
                formattedNum = "UNLLLLLIMITED POWEEERRRRRR!!!!";
            return formattedNum;
        }
    }

}
