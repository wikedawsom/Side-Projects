using System;
using System.Collections.Generic;
using System.Text;
using Tetfuza;
using System.Threading;
using FramerateStabilizer;
using System.Diagnostics;
using Tetfuza.Interfaces;
using static Tetfuza.Interfaces.IInput;

namespace TetfuzaCLI
{
    public class MainScreen
    {

        private IInput _keyboard;
        private IDisplay _screen;
        private TetfuzaMain game;
        private long _score;
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
                _screen.WriteText("Choose a starting level (0-19)", 1m, 1m);
                int startLevel = (int)_keyboard.ReadInput();

                game = new TetfuzaMain(_keyboard, _screen, startLevel);
                _screen.ClearScreen();

                // game loop. returns when user tops out
                _score = game.Run();

                _screen.WriteText("Oh no, you topped out... please press enter",1m,1m);

                // display final game information & ask to continue
                Console.ReadLine();
                _screen.ClearScreen();
                _screen.WriteText("Your final score is: "+ CommasInNumber(_score), 1m, 1m);
                _screen.WriteText("Press start to exit, any other button to play again", 1m, 1m);
                Input key = _keyboard.ReadInput();
                exit = (key == Input.Pause);
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
