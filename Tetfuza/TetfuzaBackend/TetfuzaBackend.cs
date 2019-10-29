using System.Collections.Generic;
using System;
using FramerateStabilizer;
using System.Diagnostics;

namespace Tetfuza
{
    public class TetfuzaBackend
    {
        public const int BOARD_HEIGHT = 22;
        public const int BOARD_WIDTH = 10;
        private Random _rand = new Random();
        private Coordinate _pieceCenter;
        private Stopwatch _timer = new Stopwatch();
        private int _userInputDirection = 0;
        private int _userInputRotation = 0;
        private FuzaPiece _nextPiece;
        private int _frameCount = 0;
        public List<List<char>> Board { get; private set; }
        

        public TetfuzaBackend()
        {
            Board = BoardInit();
        }
        private List<List<char>> BoardInit()
        {
            var startingBoard = new List<List<char>>();
            for (int height = 0; height < BOARD_HEIGHT; height++)
            {
                startingBoard.Add(new List<char>());
                for (int width = 0; width < BOARD_WIDTH; width++)
                {
                    startingBoard[height].Add('-');
                }
            }
            return startingBoard;
        }

        public override string ToString()
        {
            string boardString = "";
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
            long score = 0;
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
            }

            return score;
        }

        private void DrawPieceInitial()
        {
            _pieceCenter = new Coordinate(6, 2);
            DrawPiece();
        }

        private void DrawPiece()
        {
            for (int row = 0; row < _nextPiece.Piece.Count; row++)
            {
                for (int col = 0; col < _nextPiece.Piece[0].Count; col++)
                {
                    char fuza = _nextPiece.Piece[row][col];
                    if (_pieceCenter.xPos - 2 + col >= 0 && _pieceCenter.xPos - 2 + col < BOARD_WIDTH
                        && _pieceCenter.yPos - 2 + row >= 0 && _pieceCenter.yPos - 2 + row < BOARD_HEIGHT)
                    {
                        if (fuza != ' ')
                        {
                            Board[_pieceCenter.yPos - 2 + row][_pieceCenter.xPos - 2 + col] = '@';
                        }
                        else
                        {
                            Board[_pieceCenter.yPos - 2 + row][_pieceCenter.xPos - 2 + col] = '-';
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
            if (_userInputRotation == -1)
            {
                FuzaPiece newPosition = _nextPiece.RotateLeft();
            }
            else if (_userInputRotation == 1)
            {

            }
        }

        private bool CheckMove(Coordinate center, FuzaPiece orientation)
        {
            bool isValid = true;
            for (int row = 0; row < _nextPiece.Piece.Count; row++)
            {
                for (int col = 0; col < _nextPiece.Piece[0].Count; col++)
                {
                    char fuza = _nextPiece.Piece[row][col];
                    if (_pieceCenter.xPos - 2 + col < 0 || _pieceCenter.xPos - 2 + col >= BOARD_WIDTH
                        || _pieceCenter.yPos - 2 + row < 0 || _pieceCenter.yPos - 2 + row >= BOARD_HEIGHT
                        && fuza == FuzaPiece.FUZA_CHAR)
                    {
                        isValid = false;
                    }/*
                    else if (Board[_pieceCenter.yPos - 2 + row][_pieceCenter.xPos - 2 + col] == '#')
                    {
                        isValid = false;
                    }*/
                }
            }
            return isValid;
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
            if (direction > 0)
            {
                _userInputDirection = 1;
            }
            else if (direction < 0)
            {
                _userInputDirection = -1;
            }
            if (rotation > 0)
            {
                _userInputRotation = 1;
            }
            else if (rotation < 0)
            {
                _userInputRotation = -1;
            }
        }
    }
}