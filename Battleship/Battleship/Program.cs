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

            player1.CheckShot(2, 4);
            player1.CheckShot(2, 7);
            player1.CheckShot(5, 5);
            player1.CheckShot(8, 2);

            player1.ShowMyBoard();
            player1.ShowEnemyBoard();
            
            Console.Write("Program ended, press any key to exit window.");
            Console.ReadKey();
        }
    }
}
