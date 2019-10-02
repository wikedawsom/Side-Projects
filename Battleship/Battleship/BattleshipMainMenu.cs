using System;

namespace Battleship
{
    public static class BattleshipMainMenu
    {
        public static void Start()
        {
            bool isExit = false;
            while (!isExit)
            {
                Console.Clear();
                Console.WriteLine("Welcome to Battleship");
                Console.WriteLine("Press a number to select an option.");
                Console.WriteLine("1.) Play VS. AI");
                Console.WriteLine("2.) Play VS. Human");
                Console.WriteLine("3.) Quit");
                char userInput = Console.ReadKey().KeyChar;
                switch (userInput)
                {
                    case '1':
                        StartGame(1);
                        break;
                    case '2':
                        StartGame(2);
                        break;
                    case '3':
                        isExit = true;
                        break;
                    default:
                        Console.WriteLine("Error: Invalid Option");
                        Console.ReadKey();
                        break;
                }
            }
        }


        public static void StartGame(int numPlayers)
        {
            BattleshipGame game = new BattleshipGame(numPlayers);
            game.PlaceShipsInitial();

            while (game.Player1IsAlive && game.Player2IsAlive)
            {
                game.Player1Turn();
                game.Player2Turn();
            }
        }
        

    }
}
