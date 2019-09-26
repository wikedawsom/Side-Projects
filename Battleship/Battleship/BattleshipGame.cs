using System;
using System.Collections.Generic;
using System.Text;

namespace Battleship
{
    class BattleshipGame
    {
        private BattleshipBoard Player1 { get; set; } = new BattleshipBoard();
        private BattleshipBoard Player2 { get; set; } = new BattleshipBoard();
        private bool _vsAI = false;

        public BattleshipGame()
        {

        }

        public BattleshipGame(int humanPlayerCount)
        {
            if (humanPlayerCount == 1)
                _vsAI = true;
            else
                _vsAI = false;
        }

        public bool GetVsAI()
        {
            return _vsAI;
        }
        
        public void AITurn()
        {

        }

        public void Player1Turn()
        {
            Console.Clear();
            for (int i = 0; i < Player1.NumShipsAlive; i++)
            {
                Player2.ShowEnemyBoard();
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
                Player2.ShowEnemyBoard();
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
