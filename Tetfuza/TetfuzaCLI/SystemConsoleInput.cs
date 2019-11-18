using System;
using System.Collections.Generic;
using System.Text;
using Tetfuza.Interfaces;


namespace TetfuzaCLI
{
    public class SystemConsole : IConsole
    {
        public ConsoleKey ReadKey()
        {
            return System.Console.ReadKey().Key;
        }

        public void Clear()
        {
            Console.Clear();
        }

        public bool KeyAvailable
        {
            get { return Console.KeyAvailable; }
        }
    }
}
