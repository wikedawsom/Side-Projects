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
        private long score = 0;
        private Stopwatch timer = new Stopwatch();
        public void StartCLI()
        {
            game = new TetfuzaBackend();
            timer.Start();
            Thread userInputThread = new Thread(new ThreadStart(this.InputListener));
            userInputThread.Start();
            Thread displayThread = new Thread(new ThreadStart(this.DisplayScreen));
            displayThread.Priority = ThreadPriority.Highest;
            displayThread.Start();
            score = game.Run();
        }

        private void InputListener()
        {
            while(score == 0)
            {
                int rot = 0;
                int dir = 0;
                ConsoleKey key = Console.ReadKey().Key;
                if(key == ConsoleKey.LeftArrow)
                {
                    dir = -1;
                }
                else if(key == ConsoleKey.RightArrow)
                {
                    dir = 1;
                }


                game.SendInput(dir, rot);
            }
        }

        private void DisplayScreen()
        {
            while (true)
            {
                Console.Clear();
                Console.Write(game.ToString());
                StableFrames.Stabilize(50, timer);
            }
        }
    }
}
