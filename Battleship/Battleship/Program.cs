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
            
            player1.PlaceShipsAuto();
            Console.WriteLine("\n");
                
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
        private Random numGen;

        public BattleshipBoard()
        {
            numGen = new Random();
            SideLength = 10;
            ShipHealth = new string[] {"55555", "4444", "333","222","11"};
            NumShipsAlive = 5;

            InitBoards();
        }

        public BattleshipBoard(int userDefinedLength)
        {
            SideLength = userDefinedLength;
            ShipHealth = new string[] { "55555", "4444", "333", "222", "11" };
            NumShipsAlive = 5;
            InitBoards();
        }

        private void InitBoards()
        {

            BoardSquares = new List<List<char>>();
            HiddenBoardSquares = new List<List<char>>();

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

        public bool PlaceShipManual(int row, int col, int shipNum, char direction)
        {
            bool placementSuccessful = false;
            int curRow = 0;
            int curCol = 0;

            if (CheckValidShipPlacement(row, col, shipNum, direction))
            {
                for (int i = 0; i < ShipHealth[shipNum].Length; i++)
                {
                    if (direction == 'u')
                    {
                        curRow = row - i;
                        curCol = col;
                    }
                    if (direction == 'd')
                    {
                        curRow = row + i;
                        curCol = col;
                    }
                    if (direction == 'l')
                    {
                        curRow = row;
                        curCol = col - i;
                    }
                    if (direction == 'r')
                    {
                        curRow = row;
                        curCol = col + i;
                    }
                    BoardSquares[curRow][curCol] = ShipHealth[shipNum][i];
                    placementSuccessful = true;
                }
            }

            return placementSuccessful;
        }

        public void PlaceShipsAuto()
        {
            int row = 0;
            int col = 0;
            int shipNum;
            char[] possibleDirections = { 'u', 'd', 'l', 'r' };
            char direction;

            for (int i = 0; i < ShipHealth.Length; i++)
            {
                direction = possibleDirections[numGen.Next(0, 4)];
                row = numGen.Next(0, SideLength);
                col = numGen.Next(0, SideLength);
                shipNum = i;
                if(!PlaceShipManual(row, col, shipNum, direction))
                {
                    i--;
                }
            }

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
                if (direction == 'u' && (row - i < 0 || BoardSquares[row - i][col] != 'O'))
                    isValid = false;
                else if (direction == 'd' && (row + i >= SideLength || BoardSquares[row + i][col] != 'O'))
                    isValid = false;
                else if (direction == 'l' && (col - i < 0 || BoardSquares[row][col - i] != 'O'))
                    isValid = false;
                else if (direction == 'r' && (col + i >= SideLength || BoardSquares[row][col + i] != 'O'))
                    isValid = false;
            }
            return isValid;
        }

    };
}
