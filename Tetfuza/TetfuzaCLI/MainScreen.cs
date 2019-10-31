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
        private Stopwatch timer = new Stopwatch();
        public void StartCLI()
        {
            bool exit = false;
            while (!exit)
            {
                _score = -1;
                Console.SetWindowSize(60, 50);
                Console.CursorVisible = false;
                game = new TetfuzaBackend();
                timer.Start();
                Thread userInputThread = new Thread(new ThreadStart(this.InputListener));
                userInputThread.Start();
                Thread displayThread = new Thread(new ThreadStart(this.DisplayScreen));
                displayThread.Priority = ThreadPriority.Highest;
                displayThread.Start();
                _score = game.Run();

                Console.ReadLine();
                Console.Clear();
                DrawBoard();
                Console.WriteLine("Your final score is: "+ _score);
                Console.WriteLine("Play again? (y/n): ");
                string p1Continue = Console.ReadLine().ToLower();
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

        private void InputListener()
        {
            while(_score == -1)
            {
                int rot = 0;
                int dir = 0;
                bool down = false;
                ConsoleKey key = Console.ReadKey().Key;
                if(key == ConsoleKey.LeftArrow)
                {
                    dir = -1;
                }
                else if(key == ConsoleKey.RightArrow)
                {
                    dir = 1;
                }
                else if(key == ConsoleKey.Z)
                {
                    rot = -1;
                }
                else if (key == ConsoleKey.X)
                {
                    rot = 1;
                }
                else if (key == ConsoleKey.DownArrow)
                {
                    down = true;
                }
                else if (key == ConsoleKey.C)
                {
                    Console.Clear();
                }

                game.SendInput(dir, rot, down);
            }
        }

        private void DisplayScreen()
        {
            Console.Clear();
            while (_score == -1)
            {
                Console.SetCursorPosition(0, 0);
                Console.CursorVisible = false;
                CenterText("Current Score ");
                CenterText(game.Score + "  ");
                CenterText("  Lines Cleared: " + game.Lines + "  ");
                CenterText("  Current Level: " + game.Level + "  ");
                string[] pieceView = game.AfterPiece.ToString().Replace(FuzaPiece.FUZA_CHAR, TetfuzaBackend.MOVING_CHAR).Split("\n");
                CenterText("Next piece ");
                for (int i = 0; i < pieceView.Length - 1; i++)
                {
                    CenterText(pieceView[i] + " ");
                }
                DrawBoard();
                
                //StableFrames.Stabilize(17, timer);
            }
            Console.WriteLine("Oh no, you topped out... please press enter");
        }
        private void CenterText(string text)
        {
            int width = Console.WindowWidth;
            for (int space = 0; space < (width / 2) - (text.Length / 2); space++)
                Console.Write(" ");
            Console.WriteLine(text);
        }

        private void DrawBoard()
        {

            string[] boardView = game.ToString().Replace(TetfuzaBackend.LOCKDOWN_CHAR, TetfuzaBackend.MOVING_CHAR).Split("\n");
            CenterText("----------------------- ");
            for (int i = 0; i < boardView.Length - 1; i++)
            {
                CenterText("| " + boardView[i] + "| ");
            }
            CenterText("----------------------- ");
        }
    }
}
