using System;
using System.Collections.Generic;
using System.Text;
using Tetfuza.Interfaces;
using static Tetfuza.Interfaces.IInput;

namespace TetfuzaCLI
{
    public class SystemConsole : IInput
    {

        public bool InputAvailable
        {
            get { return Console.KeyAvailable; }
        }

        public ConsoleKey ReadKey()
        {
            return System.Console.ReadKey().Key;
        }

        public Input ReadInput()
        {
            Input input = Input.NoInput;
            switch (System.Console.ReadKey().Key)
            {
                case ConsoleKey.LeftArrow:
                    input = Input.Left;
                    break;
                case ConsoleKey.RightArrow:
                    input = Input.Right;
                    break;
                case ConsoleKey.UpArrow:
                    input = Input.Up;
                    break;
                case ConsoleKey.DownArrow:
                    input = Input.Down;
                    break;
                case ConsoleKey.Z:
                    input = Input.RotateCounterClockwise;
                    break;
                case ConsoleKey.X:
                    input = Input.RotateClockwise;
                    break;
                case ConsoleKey.Spacebar:
                    input = Input.Pause;
                    break;
                case ConsoleKey.C:
                    input = Input.Option;
                    break;
                default:
                    break;
            }

            return input;
        }

        public void ClearInputBuffer()
        {
            Console.Clear();
        }

    }
}
