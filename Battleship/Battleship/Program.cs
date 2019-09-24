using System;
using System.Collections.Generic;

namespace Battleship
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello Battleship!");

            BattleshipBoard player1 = new BattleshipBoard();
            player1.ShowMyBoard();
            
            player1.PlaceShipsAuto();
            Console.WriteLine("\n");
                
            player1.ShowMyBoard();
            
            Console.Write("Program ended, press any key to exit window.");
            Console.ReadKey();
        }
    }
}
