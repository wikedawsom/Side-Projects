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
            player1.ShowBoard();


            Console.Write("Program ended, press any key to exit window.");
            Console.ReadKey();
        }
    }
    public class BattleshipBoard
    {
        private List<List<char>> BoardSquares;
        private List<List<char>> HiddenBoardSquares;
        public int SideLength;
        private string[] ShipHealth;
        private int NumShipsAlive;

        public BattleshipBoard()
        {
            SideLength = 10;
            ShipHealth = new string[] {"55555", "4444", "333","222","11"};
            NumShipsAlive = 5;
            BoardSquares = new List<List<char>>();
            HiddenBoardSquares = new List<List<char>>();

            for (int row = 0; row < SideLength; row ++)
            {
                BoardSquares.Add(new List<char>());
                for (int col = 0; col < SideLength; col++)
                {
                    BoardSquares[row].Add('O');
                }
            }
            for (int row = 0; row < SideLength; row++)
            {
                HiddenBoardSquares.Add(new List<char>());
                for (int col = 0; col < SideLength; col++)
                {
                    HiddenBoardSquares[row].Add('O');
                }
            }
        }

        public BattleshipBoard(int userDefinedLength)
        {
            SideLength = userDefinedLength;
            ShipHealth = new string[] { "55555", "4444", "333", "222", "11" };
            NumShipsAlive = 5;
            for (int row = 0; row < SideLength; row++)
            {
                BoardSquares.Add(new List<char>());
                for (int col = 0; col < SideLength; col++)
                {
                    BoardSquares[row].Add('O');
                }
            }
            for (int row = 0; row < SideLength; row++)
            {
                HiddenBoardSquares.Add(new List<char>());
                for (int col = 0; col < SideLength; col++)
                {
                    HiddenBoardSquares[row].Add('O');
                }
            }
        }

        public void PlaceShipsManual()
        {

        }

        public void PlaceShipsAuto()
        {

        }

        public void ShowBoard()
        {
            for (int row = 0; row < SideLength; row++)
            {
                for (int col = 0; col < SideLength; col++)
                {
                    Console.Write("\t" + BoardSquares[row][col]);
                }
                Console.WriteLine();
            }
        }

        private bool CheckValidShipPlacement(int row, int col, int shipNum, char direction)
        {
            if (   row < 0 
                || col < 0 
                || row > SideLength 
                || col > SideLength 
                || shipNum < 0 
                || shipNum > ShipHealth.Length)
                return false;

            bool isValid = true;
            for (int i = 0; i < ShipHealth[shipNum].Length; i++)
            {
                if (direction == 'u' && row - i >= 0 && BoardSquares[row - i][col] != 'O')
                    isValid = false;
                if (direction == 'd' && row + i < SideLength && BoardSquares[row + i][col] != 'O')
                    isValid = false;
                if (direction == 'l' && col - i >= 0 && BoardSquares[row][col - i] != 'O')
                    isValid = false;
                if (direction == 'r' && col + i < SideLength && BoardSquares[row][col + i] != 'O')
                    isValid = false;
            }
            return isValid;
        }

    };
}
