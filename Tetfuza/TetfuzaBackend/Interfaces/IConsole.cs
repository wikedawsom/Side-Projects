using System;
using System.Collections.Generic;
using System.Text;

namespace Tetfuza.Interfaces
{
    public interface IConsole
    {
        public ConsoleKey ReadKey();
        public void Clear();
        public bool KeyAvailable { get; }
    }
}
