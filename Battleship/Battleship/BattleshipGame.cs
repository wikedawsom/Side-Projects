using System;
using System.Collections.Generic;

namespace Battleship
{
    class BattleshipGame
    {

        private Random _randomizer = new Random();
        private BattleshipBoard Player1 { get; set; } = new BattleshipBoard();
        private BattleshipBoard Player2 { get; set; } = new BattleshipBoard();
        public bool VsAI { get; }
        public bool Player1IsAlive
        {
            get
            {
                return Player1.NumShipsAlive > 0;
            }
        }
        public bool Player2IsAlive
        {
            get
            {
                return Player2.NumShipsAlive > 0;
            }
        }

        public BattleshipGame(int humanPlayerCount)
        {
            VsAI = false;

            if (humanPlayerCount == 1)
            {
                VsAI = true;
            }
        }
        
        public void AITurn()
        {
            Console.Clear();
            for (int i = 0; i < Player2.NumShipsAlive; i++)
            {
                Console.WriteLine(Player1.ShowEnemyBoard());
                int p1ShipCount = Player1.NumShipsAlive;

                TakeAShot(Player1);
                if (p1ShipCount > Player1.NumShipsAlive)
                {
                    Console.WriteLine("***  A ship has been destroyed!  ***");
                }
            }
            Console.WriteLine(Player1.ShowEnemyBoard());
            Console.WriteLine("\nPlayer Two's ships have ceased fire. Press a key to end turn.");
            Console.ReadKey();
        }

        public void Player1Turn()
        {
            if (!Player1IsAlive && !Player2IsAlive )
                return;
            Console.Clear();
            for (int i = 0; i < Player1.NumShipsAlive; i++)
            {
                Console.WriteLine(Player2.ShowEnemyBoard());
                int p2ShipCount = Player2.NumShipsAlive;

                TakeAShot(Player2);
                if (p2ShipCount > Player2.NumShipsAlive)
                {
                    Console.WriteLine("***  A ship has been destroyed!  ***");
                }
            }
            Console.WriteLine(Player2.ShowEnemyBoard());
            Console.WriteLine("\nPlayer One's ships have ceased fire. Press a key to end turn.");
            Console.ReadKey();
        }
        public void Player2Turn()
        {
            if (!Player1IsAlive && !Player2IsAlive)
                return;
            if (VsAI)
            {
                AITurn();
            }
            else
            {
                Console.Clear();
                for (int i = 0; i < Player2.NumShipsAlive; i++)
                {
                    Console.WriteLine(Player1.ShowEnemyBoard());
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
        }

        public void TakeAShot(BattleshipBoard recievingEnd)
        {
            int row = 0;
            int col = 0;
            bool shotInvalid = true;
            while (shotInvalid)
            {
                
                GetCoordinates(out row, out col, recievingEnd.Equals(Player2));
                
                // Check if coordinates are on board and not already used
                // if coordinates are valid, then board is automatically updated by CheckShot
                if (recievingEnd.CheckShot(row, col) != "bad_input")
                {
                    shotInvalid = false;
                }
            }
        }

        public void PlaceShipsAuto(BattleshipBoard player)
        {
            // Something not quite right here... first ship might get placed twice
            int row = 0;
            int col = 0;
            int shipNum;
            char[] possibleDirections = { 'u', 'd', 'l', 'r' };
            char direction;

            for (int i = 0; i < player.ShipHealth.Count; i++)
            {
                direction = possibleDirections[_randomizer.Next(0, 4)];
                row = _randomizer.Next(0, player.SideLength);
                col = _randomizer.Next(0, player.SideLength);
                shipNum = i;
                if (!player.PlaceShipManual(row, col, shipNum, direction))
                {
                    i--;
                }
            }

        }

        public void PlaceShipsInitial()
        {
            Console.WriteLine(Player1.ShowMyBoard());
            for (int i = 0; i < Player1.ShipHealth.Count; i++)
            {
                int row = -1;
                int col = -1;
                char direction = 'd';

                GetCoordinates(out row, out col, true);
                direction = GetDirection();

                Console.Clear();
                if (!Player1.PlaceShipManual(row, col, i, direction))
                {
                    i--;
                    Console.WriteLine("Invalid Placement");
                }
                Console.WriteLine(Player1.ShowMyBoard());
            }

            Console.Clear();
            Console.WriteLine("Player 1: all ships placed. Press a key to place Player 2's ships.");
            Console.ReadKey();

            if (VsAI)
            {
                PlaceShipsAuto(Player2);
            }
            else
            {
                Console.WriteLine(Player2.ShowMyBoard());
                for (int i = 0; i < Player2.ShipHealth.Count; i++)
                {
                    int row = -1;
                    int col = -1;
                    char direction = 'd';
                    GetCoordinates(out row, out col, true);
                    direction = GetDirection();

                    Console.Clear();
                    if (!Player1.PlaceShipManual(row, col, i, direction))
                    {
                        i--;
                        Console.WriteLine("Invalid Placement");
                    }
                    Console.WriteLine(Player1.ShowMyBoard());
                }

            }
            Console.Clear();
            Console.WriteLine("Player 2: all ships placed. Press a key to begin turns.");
            Console.ReadKey();
        }
            

        public bool GetCoordinates(out int row, out int col, bool isHumanPlayer)
        {
            var rand = new Random();

            if (!isHumanPlayer)
            {
                row = rand.Next(0, Player1.SideLength);
                col = rand.Next(0, Player1.SideLength);
            }
            else
            {
                Console.WriteLine("Enter coordinates (separated by a space): ");

                string rawInput = Console.ReadLine();


                // Convert input string into two parameters for CheckShot
                row = rawInput.Length > 0 ? rawInput.ToUpper()[0] - 65 : -1;

                if (!int.TryParse(rawInput.Substring(rawInput.IndexOf(" ") < 0 ? 0 : rawInput.IndexOf(" ")), out col))
                {
                    col = -1;
                }
                col--;
            }
            return (col >= 0 && col < Player1.SideLength && row >= 0 && row < Player1.SideLength);
        }

        public char GetDirection()
        {
            var possibleDirections = new List<char> { 'u', 'd', 'l', 'r' };
            char direction = 'q';

            while (!(possibleDirections.Contains(direction)))
            {
                Console.Write("Enter direction (up, down, left, right): ");
                direction = (Console.ReadLine().ToLower()+"u")[0];
            }

            return direction;
        }
    }
}
