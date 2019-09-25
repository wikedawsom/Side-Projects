using System;
using System.Collections.Generic;

namespace Battleship
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello Battleship!");

            BattleshipGame game = new BattleshipGame(2);

            game.Player1Turn();
            
            Console.Write("Program ended, press any key to exit window.");
            Console.ReadKey();
        }
    }
}
