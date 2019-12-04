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
        private TetfuzaMenu game;

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
            
            game = new TetfuzaMenu(_keyboard, _screen);
            _screen.ClearScreen();

            // Open game menu. returns when user quits game
            int exitPath = game.MainMenu();

        }
    }

}
