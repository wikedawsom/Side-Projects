using System;
using System.Collections.Generic;
using System.Text;

namespace Battleship
{
    public class BattleshipBoard
    {
        private List<List<char>> BoardSquares;
        private List<List<char>> HiddenBoardSquares;
        private int SideLength;
        private List<string> ShipHealth;
        private int NumShipsAlive;
        private Random numGen;
        public const char WaterSymbol = '*';
        public const char MissSymbol = 'X';
        public const char HitSymbol = 'O';

        public BattleshipBoard()
        {
            numGen = new Random();
            SideLength = 10;
            ShipHealth = new List<string> { "11", "222", "333", "4444", "55555" };
            NumShipsAlive = 5;

            InitBoards();
        }

        public BattleshipBoard(int userDefinedLength)
        {
            SideLength = userDefinedLength;
            numGen = new Random();
            ShipHealth = new List<string> { "11", "222", "333", "4444", "55555" };
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
                    BoardSquares[row].Add(WaterSymbol);
                }
            }
            for (int row = 0; row < SideLength; row++)
            {
                HiddenBoardSquares.Add(new List<char>());
                for (int col = 0; col < SideLength; col++)
                {
                    HiddenBoardSquares[row].Add(WaterSymbol);
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

            for (int i = 0; i < ShipHealth.Count; i++)
            {
                direction = possibleDirections[numGen.Next(0, 4)];
                row = numGen.Next(0, SideLength);
                col = numGen.Next(0, SideLength);
                shipNum = i;
                if (!PlaceShipManual(row, col, shipNum, direction))
                {
                    i--;
                }
            }

        }
        public void ShowMyBoard()
        {
            ShowBoard(false);
        }
        public void ShowEnemyBoard()
        {
            ShowBoard(true);
        }
        public void ShowBoard(bool shipsHidden)
        {
            var boardShowing = shipsHidden ? HiddenBoardSquares : BoardSquares;
            

            Console.Write("\t|");
            for (int col = 0; col < SideLength; col++)
            {
                Console.Write($"\t{col + 1}");
            }
            Console.Write("\n\t|\n--------|");
            for (int col = 0; col < SideLength*8+5; col++)
            {
                Console.Write($"-");
            }

            Console.Write("\n\t|");
            Console.WriteLine();

            for (int row = 0; row < SideLength; row++)
            {
                Console.Write($"  {row+1}\t|");
                
                for (int col = 0; col < SideLength; col++)
                {
                    Console.Write("\t" + boardShowing[row][col]);
                }

                Console.WriteLine();
                Console.WriteLine("\t|");
            }
        }

        private bool CheckValidShipPlacement(int row, int col, int shipNum, char direction)
        {
            if (row < 0
                || col < 0
                || row > SideLength
                || col > SideLength
                || shipNum < 0
                || shipNum > ShipHealth.Count)
                return false;

            bool isValid = true;
            for (int i = 0; i < ShipHealth[shipNum].Length; i++)
            {
                if (direction == 'u' && (row - i < 0 || BoardSquares[row - i][col] != WaterSymbol))
                    isValid = false;
                else if (direction == 'd' && (row + i >= SideLength || BoardSquares[row + i][col] != WaterSymbol))
                    isValid = false;
                else if (direction == 'l' && (col - i < 0 || BoardSquares[row][col - i] != WaterSymbol))
                    isValid = false;
                else if (direction == 'r' && (col + i >= SideLength || BoardSquares[row][col + i] != WaterSymbol))
                    isValid = false;
            }
            return isValid;
        }

        public int GetNumShipsAlive()
        {
            int numberOFShips = 5;
            foreach (string ship in ShipHealth)
            {
                if (int.Parse(ship) == 0)
                {
                    numberOFShips--;
                }
            }
            return numberOFShips;
        }

        public string CheckShot(int row, int col)
        {
            string hitOrMiss = "I guess they never miss, huh?";

            if (row < 0 || row >= SideLength || col < 0 || col >= SideLength)
            {
                return "bad coordinates";
            }

            if (BoardSquares[row][col] == WaterSymbol || BoardSquares[row][col] == 'X')
            {
                hitOrMiss = "miss";
                BoardSquares[row][col] = MissSymbol;
                HiddenBoardSquares[row][col] = MissSymbol;
            }
            else
            {
                hitOrMiss = "" + BoardSquares[row][col];
                DamageShip(int.Parse(hitOrMiss) - 1);
                NumShipsAlive = GetNumShipsAlive();

                BoardSquares[row][col] = HitSymbol;
                HiddenBoardSquares[row][col] = HitSymbol;
            }

            return hitOrMiss;
        }
        
        private void DamageShip(int shipNum)
        {
            ShipHealth[shipNum] = "0"+ShipHealth[shipNum].Substring(0, ShipHealth[shipNum].Length-1);
        }
    }
}
