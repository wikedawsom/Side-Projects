using System;
using System.Collections.Generic;
using System.Text;

namespace Battleship
{
    class BattleshipGame
    {
        private BattleshipBoard Player1;
        private BattleshipBoard Player2;
        private bool VsAI;

        public BattleshipGame(int humanPlayerCount)
        {
            Player1 = new BattleshipBoard();
            Player2 = new BattleshipBoard();
            if (humanPlayerCount == 1)
                VsAI = true;
            else
                VsAI = false;
        }

        public bool GetVsAI()
        {
            return VsAI;
        }
        
        public void AITurn()
        {

        }

        public void Player1Turn()
        {
            Console.Clear();
            for (int i = 0; i < Player1.GetNumShipsAlive(); i++)
            {
                Player2.ShowEnemyBoard();
                int p2ShipCount = Player2.GetNumShipsAlive();

                TakeAShot(Player2);
                if (p2ShipCount > Player2.GetNumShipsAlive())
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
            for (int i = 0; i < Player2.GetNumShipsAlive(); i++)
            {
                Player2.ShowEnemyBoard();
                int p1ShipCount = Player1.GetNumShipsAlive();

                TakeAShot(Player1);
                if (p1ShipCount > Player1.GetNumShipsAlive())
                {
                    Console.WriteLine("***  A ship has been destroyed!  ***");
                }
            }
            Console.WriteLine("\nPlayer Two's ships have ceased fire. Press a key to end turn.");
            Console.ReadKey();
        }

        public void TakeAShot(BattleshipBoard recievingEnd)
        {
            char row = 'A';
            int col = 0;
            bool shotInvalid = true;
            while (shotInvalid)
            {
                Console.WriteLine("Enter coordinates to fire at: ");

                // ...
                // Get user input
                // ...

                if (recievingEnd.CheckShot(row - 65, col) != "bad_input")
                {
                    shotInvalid = false;
                }
            }
        }
    }
}
