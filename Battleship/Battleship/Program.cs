using System;

namespace Battleship
{
    class Program
    {
        static void Main(string[] args)
        {
            BattleshipMainMenu.Start();

            Console.Clear();
            Console.Write("\n\n***Program ended, press any key to exit window***");
            Console.ReadKey();
        }
    }
}
