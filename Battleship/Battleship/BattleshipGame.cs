using System;
using System.Collections.Generic;
using System.Text;

namespace Battleship
{
    class BattleshipGame
    {
        private BattleshipBoard Player1 { get; set; } = new BattleshipBoard();
        private BattleshipBoard Player2 { get; set; } = new BattleshipBoard();
        public bool VsAI { get; private set; } = false;

        public BattleshipGame()
        {
            // Just for show (and in case i want to add anything later)
            // I mean these constructors are kinda stupid they way they're set up right now...
            // I don't know if I'll even put anything here other than pointless comments......
        }

        public BattleshipGame(int humanPlayerCount)
        {
            if (humanPlayerCount == 1)
                VsAI = true;
        }
        
        public void AITurn()
        {
            // FILL ME IN
        }

        public void Player1Turn()
        {
            Console.Clear();
            for (int i = 0; i < Player1.NumShipsAlive; i++)
            {
                Console.WriteLine( Player2.ShowEnemyBoard());
                int p2ShipCount = Player2.NumShipsAlive;

                TakeAShot(Player2);
                if (p2ShipCount > Player2.NumShipsAlive)
                {
                    Console.WriteLine("***  A ship has been destroyed!  ***");
                }
            }
            Console.WriteLine("\nPlayer One's ships have ceased fire. Press a key to end turn.");
            Console.ReadKey();
        }
        public void Player2Turn()
        {
            Console.Clear();
            for (int i = 0; i < Player2.NumShipsAlive; i++)
            {
                Console.WriteLine(Player2.ShowEnemyBoard());
                int p1ShipCount = Player1.NumShipsAlive;

                TakeAShot(Player1);
                if (p1ShipCount > Player1.NumShipsAlive)
                {
                    Console.WriteLine("***  A ship has been destroyed!  ***");
                }
            }
            Console.WriteLine("\nPlayer Two's ships have ceased fire. Press a key to end turn.");
            Console.ReadKey();
        }

        public void TakeAShot(BattleshipBoard recievingEnd)
        {
            int row = 0;
            int col = 0;
            bool shotInvalid = true;
            while (shotInvalid)
            {
                Console.WriteLine("Enter coordinates to fire at (separated by a space): ");
                
                string rawInput = Console.ReadLine();


                // Convert input string into two parameters for CheckShot
                row = rawInput.Length > 0 ? rawInput[0] - 65 : -1;

                if(!int.TryParse(rawInput.Substring(rawInput.IndexOf(" ")), out col))
                {
                    col = -1;
                }

                // Check if coordinates are on board and not already used
                // if coordinates are valid, then board is automatically updated by CheckShot
                if (recievingEnd.CheckShot(row, col) != "bad_input")
                {
                    shotInvalid = false;
                }
            }
        }
    }
}
