using System;
using System.Collections.Generic;
using System.Text;

namespace Battleship
{
    public class BattleshipBoard
    {
        public const char _waterSymbol = '*';
        public const char _missSymbol = 'X';
        public const char _hitSymbol = 'O';
        private Random _randomizer = new Random();

        public List<List<char>> BoardSquares { get; private set; }
        public List<List<char>> HiddenBoardSquares { get; private set; }
        public int SideLength { get; private set; } = 10;
        public List<string> ShipHealth { get; private set; } = new List<string> { "11", "222", "333", "4444", "55555" };
        public int NumShipsAlive
        {
            get
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
        }


        public BattleshipBoard()
        {
            InitBoards();
        }

        public BattleshipBoard(int userDefinedLength)
        {
            SideLength = userDefinedLength;
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
                    BoardSquares[row].Add(_waterSymbol);
                }
            }
            for (int row = 0; row < SideLength; row++)
            {
                HiddenBoardSquares.Add(new List<char>());
                for (int col = 0; col < SideLength; col++)
                {
                    HiddenBoardSquares[row].Add(_waterSymbol);
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
                direction = possibleDirections[_randomizer.Next(0, 4)];
                row = _randomizer.Next(0, SideLength);
                col = _randomizer.Next(0, SideLength);
                shipNum = i;
                if (!PlaceShipManual(row, col, shipNum, direction))
                {
                    i--;
                }
            }

        }
        public string ShowMyBoard()
        {
            return ShowBoard(false);
        }
        public string ShowEnemyBoard()
        {
            return ShowBoard(true);
        }
        public string ShowBoard(bool shipsHidden)
        {
            var boardShowing = shipsHidden ? HiddenBoardSquares : BoardSquares;
            string output = "";

            output += "\t|";
            for (int col = 0; col < SideLength; col++)
            {
                output += $"\t{col + 1}";
            }
            output += "\n\t|\n--------|";
            for (int col = 0; col < SideLength*8+5; col++)
            {
                output += $"-";
            }

            output += "\n\t|\n";

            for (int row = 0; row < SideLength; row++)
            {
                output += $"  {(char)(row+65)}\t|";
                
                for (int col = 0; col < SideLength; col++)
                {
                    output += "\t" + boardShowing[row][col];
                }

                output += "\n\t|\n";
            }
            return output;
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
                if (direction == 'u' && (row - i < 0 || BoardSquares[row - i][col] != _waterSymbol))
                    isValid = false;
                else if (direction == 'd' && (row + i >= SideLength || BoardSquares[row + i][col] != _waterSymbol))
                    isValid = false;
                else if (direction == 'l' && (col - i < 0 || BoardSquares[row][col - i] != _waterSymbol))
                    isValid = false;
                else if (direction == 'r' && (col + i >= SideLength || BoardSquares[row][col + i] != _waterSymbol))
                    isValid = false;
            }
            return isValid;
        }

        public string CheckShot(int row, int col)
        {
            string hitOrMiss = "I guess they never miss, huh?";

            if (row < 0 
                || row >= SideLength 
                || col < 0 
                || col >= SideLength 
                || BoardSquares[row][col] == 'X')
            {
                return "bad_input";
            }

            if (BoardSquares[row][col] == _waterSymbol)
            {
                hitOrMiss = "miss";
                BoardSquares[row][col] = _missSymbol;
                HiddenBoardSquares[row][col] = _missSymbol;
            }
            else
            {
                hitOrMiss = "" + BoardSquares[row][col];
                DamageShip(int.Parse(hitOrMiss) - 1);

                BoardSquares[row][col] = _hitSymbol;
                HiddenBoardSquares[row][col] = _hitSymbol;
            }

            return hitOrMiss;
        }
        
        private void DamageShip(int shipNum)
        {
            ShipHealth[shipNum] = "0"+ShipHealth[shipNum].Substring(0, ShipHealth[shipNum].Length-1);
        }
    }
}
