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
        public const char EMPTY_CHAR = ' ';
        private Random _rand = new Random();
        private Coordinate _pieceCenter;
        private Stopwatch _timer = new Stopwatch();
        private int _userInputDirection = 0;
        private int _userInputRotation = 0;
        private bool _userInputDown = false;
        private int _frameCount = 0; 
        public FuzaPiece CurrentPiece { get; private set; }
        public FuzaPiece AfterPiece { get; private set; }
        public List<List<char>> Board { get; private set; }
        public long Score { get; private set; }
        public int Lines { get; private set; }
        public int Level
        {
            get
            {
                return Lines / 10;
            }
        }
        private List<char> EmptyLine
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
        /// <summary>
        /// Create a new board with {BOARD_HEIGHT} rows, and {BOARD_WIDTH} columns, all filled with ' ' chars
        /// </summary>
        /// <returns></returns>
        private List<List<char>> BoardInit()
        {
            var startingBoard = new List<List<char>>();
            for (int height = 0; height < BOARD_HEIGHT; height++)
            {
                startingBoard.Add(EmptyLine);
            }
            //for (int i = 18; i < BOARD_HEIGHT; i ++)
            //{
            //    for (int j = 0; j < BOARD_WIDTH; j++)
            //    {
            //        startingBoard[i][j] = LOCKDOWN_CHAR;
            //    }
            //}
            return startingBoard;
        }

        public override string ToString()
        {
            string boardString = "";
            for (int row = 2; row < Board.Count; row++)
            {
                for (int col = 0; col < BOARD_WIDTH; col++)
                {
                    boardString += Board[row][col] + " ";
                }
                boardString += "\n";
            }
            
            return boardString;
        }

        /// <summary>
        /// Main Loop. Contains timing and order of events.
        /// </summary>
        /// <returns></returns>
        public long Run()
        {
            bool gameOver = false;
            _timer.Start();
            int pieceNum = _rand.Next(0, 7);
            AfterPiece = new FuzaPiece((FuzaType)pieceNum);
            while (!gameOver)
            {
                CurrentPiece = AfterPiece;
                pieceNum = _rand.Next(0, 7);
                AfterPiece = new FuzaPiece((FuzaType)pieceNum);
                _pieceCenter = new Coordinate(5, 2);
                gameOver = CheckTopOut();

                bool isLockDown = false;
                while (!isLockDown)
                {
                    MovePieceLeftRight();
                    RotatePiece();
                    DrawPiece();
                    bool autoDown = _frameCount % 10 == 0;
                    if (_userInputDown)
                    {
                        autoDown = true;
                        _userInputDown = false;
                        Score += 1;
                    }
                    if (autoDown)
                        isLockDown = !DropPiece();
                    StableFrames.Stabilize(17, _timer);
                    _frameCount++;
                }
                DrawPiece(LOCKDOWN_CHAR);
                int linesCleared = CheckClear();
                Lines += linesCleared;
                int scoreMultiplier = 0;
                switch (linesCleared)
                {
                    case 1:
                        scoreMultiplier = 40;
                        break;
                    case 2:
                        scoreMultiplier = 100;
                        break;
                    case 3:
                        scoreMultiplier = 300;
                        break;
                    case 4:
                        scoreMultiplier = 1200;
                        break;
                }
                Score += scoreMultiplier * (Level + 1);
            }

            return Score;
        }

        private void DrawPiece(char fuzaChar = MOVING_CHAR)
        {
            for (int row = 0; row < FuzaPiece.PIECE_SIZE; row++)
            {
                for (int col = 0; col < FuzaPiece.PIECE_SIZE; col++)
                {
                    char fuza = CurrentPiece.Piece[row][col];
                    int colCoord = _pieceCenter.xPos - (FuzaPiece.PIECE_SIZE / 2) + col;
                    int rowCoord = _pieceCenter.yPos - (FuzaPiece.PIECE_SIZE / 2) + row;
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
            if (CheckMove(new Coordinate(_pieceCenter.xPos + _userInputDirection,_pieceCenter.yPos), CurrentPiece))
                _pieceCenter.xPos = _pieceCenter.xPos + _userInputDirection;
            
            _userInputDirection = 0;
        }

        private void RotatePiece()
        {
            FuzaPiece newPosition;
            
            if (_userInputRotation == -1)
            {
                newPosition = CurrentPiece.RotateLeft();
                if (CheckMove(_pieceCenter, newPosition))
                    CurrentPiece = newPosition;
            }
            else if (_userInputRotation == 1)
            {
                newPosition = CurrentPiece.RotateRight();
                if (CheckMove(_pieceCenter, newPosition))
                    CurrentPiece = newPosition;
            }
            _userInputRotation = 0;
        }

        private bool CheckTopOut()
        {
            return Board[2][6] != EMPTY_CHAR;
        }

        private bool CheckMove(Coordinate center, FuzaPiece orientation)
        {
            bool isValid = true;
            for (int row = 0; row < FuzaPiece.PIECE_SIZE; row++)
            {
                for (int col = 0; col < FuzaPiece.PIECE_SIZE; col++)
                {
                    char fuza = orientation.Piece[row][col];
                    int colCoord = center.xPos - (FuzaPiece.PIECE_SIZE / 2) + col;
                    int rowCoord = center.yPos - (FuzaPiece.PIECE_SIZE / 2) + row;
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
            for (int i = 0; i < BOARD_HEIGHT; i++)
            {
                if (!(Board[i].Contains(EMPTY_CHAR) || Board[i].Contains(MOVING_CHAR)))
                {
                    linesCleared++;
                    Board.RemoveAt(i);
                    var tempBoard = new List<List<char>>();
                    tempBoard.Add(EmptyLine);
                    tempBoard.AddRange(Board);
                    Board = tempBoard;
                }
            }
            return linesCleared;
        }


        private bool DropPiece()
        {
            bool isValid = CheckMove(new Coordinate(_pieceCenter.xPos, _pieceCenter.yPos + 1), CurrentPiece);
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
        public void SendInput(int direction, int rotation, bool down)
        {
            _userInputDirection = 0;
            _userInputRotation = 0;
            _userInputDown = false;

            _userInputDirection = direction;
            _userInputRotation = rotation;
            _userInputDown = down;
        }
    }
}