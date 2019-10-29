using System.Collections.Generic;
using System;
using FramerateStabilizer;
using System.Diagnostics;
using System.Linq;

namespace Tetfuza
{
    public class TetfuzaBackend
    {
        public const int BOARD_HEIGHT = 22;
        public const int BOARD_WIDTH = 10;
        public const char LOCKDOWN_CHAR = '0';
        public const char MOVING_CHAR = '@';
        public const char EMPTY_CHAR = '-';
        private Random _rand = new Random();
        private Coordinate _pieceCenter;
        private Stopwatch _timer = new Stopwatch();
        private int _userInputDirection = 0;
        private int _userInputRotation = 0;
        private FuzaPiece _nextPiece;
        private int _frameCount = 0;
        public List<List<char>> Board { get; private set; }
        public long Score { get; private set; }
        private List<char> _emptyLine
        {
            get
            {
                var line = new List<char>();
                for (int i = 0; i < BOARD_WIDTH; i++)
                {
                    line.Add(EMPTY_CHAR);
                }
                return line;
            }
        }
        

        public TetfuzaBackend()
        {
            Board = BoardInit();
        }
        private List<List<char>> BoardInit()
        {
            var startingBoard = new List<List<char>>();
            for (int height = 0; height < BOARD_HEIGHT; height++)
            {
                startingBoard.Add(_emptyLine);
            }
            return startingBoard;
        }

        public override string ToString()
        {
            string boardString = "";
            boardString += "Score: " + Score + "\n";
            for (int row = 2; row < BOARD_HEIGHT; row++)
            {
                for (int col = 0; col < BOARD_WIDTH; col++)
                {
                    boardString += Board[row][col] + "  ";
                }
                boardString += "\n";
            }
            
            return boardString;
        }

        /// <summary>
        /// Main Loop
        /// </summary>
        /// <returns></returns>
        public long Run()
        {
            bool topOut = false;
            _timer.Start();
            while (!topOut)
            {
                int pieceNum = _rand.Next(0, 7);
                _nextPiece = new FuzaPiece((FuzaType)pieceNum);
                DrawPieceInitial();
                bool isLockDown = false;
                while (!isLockDown)
                {
                    MovePieceLeftRight();
                    RotatePiece();
                    if (_frameCount % 2 == 0)
                        isLockDown = !DropPiece();
                    StableFrames.Stabilize(50, _timer);
                    _frameCount++;
                }
                DrawPiece(LOCKDOWN_CHAR);
                CheckClear();
            }

            return Score;
        }

        private void DrawPieceInitial()
        {
            _pieceCenter = new Coordinate(6, 2);
            DrawPiece();
        }

        private void DrawPiece(char fuzaChar = MOVING_CHAR)
        {
            for (int row = 0; row < FuzaPiece.PIECE_SIZE; row++)
            {
                for (int col = 0; col < FuzaPiece.PIECE_SIZE; col++)
                {
                    char fuza = _nextPiece.Piece[row][col];
                    int colCoord = _pieceCenter.xPos - 2 + col;
                    int rowCoord = _pieceCenter.yPos - 2 + row;
                    if (colCoord >= 0 && colCoord < BOARD_WIDTH
                        && rowCoord >= 0 && rowCoord < BOARD_HEIGHT)
                    {
                        if (fuza != FuzaPiece.BLANK_CHAR)
                        {
                            Board[rowCoord][colCoord] = fuzaChar;
                        }
                        else if (fuza == FuzaPiece.BLANK_CHAR && Board[rowCoord][colCoord] != LOCKDOWN_CHAR)
                        {
                            Board[rowCoord][colCoord] = EMPTY_CHAR;
                        }
                    }
                }
            }
        }

        private void MovePieceLeftRight()
        {
            if (CheckMove(new Coordinate(_pieceCenter.xPos + _userInputDirection,_pieceCenter.yPos), _nextPiece))
                _pieceCenter.xPos = _pieceCenter.xPos + _userInputDirection;
            DrawPiece();
            _userInputDirection = 0;
        }

        private void RotatePiece()
        {
            FuzaPiece newPosition = null;
            
            if (_userInputRotation == -1)
            {
                newPosition = _nextPiece.RotateLeft();
                if (CheckMove(_pieceCenter, newPosition))
                    _nextPiece = newPosition;
            }
            else if (_userInputRotation == 1)
            {
                newPosition = _nextPiece.RotateRight();
                if (CheckMove(_pieceCenter, newPosition))
                    _nextPiece = newPosition;
            }
        }

        private bool CheckMove(Coordinate center, FuzaPiece orientation)
        {
            bool isValid = true;
            for (int row = 0; row < FuzaPiece.PIECE_SIZE; row++)
            {
                for (int col = 0; col < FuzaPiece.PIECE_SIZE; col++)
                {
                    char fuza = _nextPiece.Piece[row][col];
                    int colCoord = center.xPos - 2 + col;
                    int rowCoord = center.yPos - 2 + row;
                    if (((colCoord < 0 || colCoord >= BOARD_WIDTH
                        || rowCoord < 0 || rowCoord >= BOARD_HEIGHT)
                        && fuza == FuzaPiece.FUZA_CHAR )
                        || (fuza == FuzaPiece.FUZA_CHAR 
                        && Board[rowCoord][colCoord] == LOCKDOWN_CHAR))
                    {
                        isValid = false;
                    }
                }
            }
            return isValid;
        }

        private int CheckClear()
        {
            int linesCleared = 0;
            for (int i = BOARD_HEIGHT - 1; i > 0; i--)
            {
                if (!Board[i].Contains(EMPTY_CHAR))
                {
                    linesCleared++;
                    Board.RemoveAt(i);
                    var tempBoard = new List<List<char>>();
                    tempBoard.Add(_emptyLine);
                    tempBoard.AddRange(Board);
                    Board = tempBoard;
                }
            }
            return linesCleared;
        }


        private bool DropPiece()
        {
            bool isValid = CheckMove(new Coordinate(_pieceCenter.xPos, _pieceCenter.yPos + 1), _nextPiece);
            if (isValid)
                _pieceCenter.yPos = _pieceCenter.yPos + 1;
            DrawPiece();
            return isValid;
        }

        /// <summary>
        /// For front-ends to send rotation and direction inputs 
        /// </summary>
        /// <param name="direction">(-1, 0, or 1 for left, none, and right movement)</param>
        /// <param name="rotation">(-1, 0, or 1 for counterclockwise, none, and clockwise rotation)</param>
        public void SendInput(int direction, int rotation)
        {
            _userInputDirection = 0;
            _userInputRotation = 0;
            
            _userInputDirection = direction;
            
            _userInputRotation = rotation;
        }
    }
}