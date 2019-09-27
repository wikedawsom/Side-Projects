using System;
using System.Collections.Generic;

namespace Battleship
{
    class Program
    {
        static void Main(string[] args)
        {
            BattleshipMainMenu.Start();
            
            Console.Write("Program ended, press any key to exit window.");
            Console.ReadKey();
        }
    }
}
