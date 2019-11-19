using System;
using System.Collections.Generic;
using System.Text;

namespace Tetfuza.Interfaces
{
    public interface IConsole
    {
        ConsoleKey ReadKey();
        void Clear();
        bool KeyAvailable { get; }
    }
}
