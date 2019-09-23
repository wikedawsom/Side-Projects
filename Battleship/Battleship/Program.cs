using System;

namespace Battleship
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello Battleship!");
        }
    }
    public class BattleshipBoard
    {
        private char[][] BoardSquares;
        private char[][] HiddenBoardSquares;
        public int SideLength;
        private string[] ShipHealth;
        private int NumShipsAlive;

        BattleshipBoard()
        {
            SideLength = 10;
            ShipHealth = new string[] {"55555", "4444", "333","222","11"};
            NumShipsAlive = 5;

            for (int row = 0; row < SideLength; row ++)
            {
                for (int col = 0; col < SideLength; col++)
                {
                    BoardSquares[row][col] = 'O';
                }
            }
            for (int row = 0; row < SideLength; row++)
            {
                for (int col = 0; col < SideLength; col++)
                {
                    HiddenBoardSquares[row][col] = 'O';
                }
            }
        }

        BattleshipBoard(int userDefinedLength)
        {
            SideLength = userDefinedLength;
            ShipHealth = new string[] { "55555", "4444", "333", "222", "11" };
            NumShipsAlive = 5;
            for (int row = 0; row < SideLength; row++)
            {
                for (int col = 0; col < SideLength; col++)
                {
                    BoardSquares[row][col] = 'O';
                }
            }
            for (int row = 0; row < SideLength; row++)
            {
                for (int col = 0; col < SideLength; col++)
                {
                    HiddenBoardSquares[row][col] = 'O';
                }
            }
        }

        public void PlaceShipsManual()
        {

        }

        public void PlaceShipsAuto()
        {

        }

        public bool CheckValidShipPlacement(int shipNum)
        {
            return true;
        }

    };
}
