using System.Collections.Generic;
using System;
using FramerateStabilizer;
using System.Diagnostics;
using System.Linq;
using Tetfuza.Interfaces;
using static Tetfuza.Interfaces.IInput;

namespace Tetfuza
{
    public class TetfuzaBackend
    {
        public const int MS_PER_FRAME = 17;

        private Random _rand = new Random();
        private Coordinate _pieceCenter;
        private Stopwatch _timer = new Stopwatch();
        private int _frameCount;
        private InputChecker _keyboard;
        private IDisplay _screen;
        private TetfuzaBoard _board;

        public FuzaPiece CurrentPiece { get; private set; }
        public FuzaPiece NextPiece { get; private set; }

        public long Score { get; private set; } = 0;
        public int Lines { get; private set; } = 0;
        public int StartLevel { get; private set; } = 0;
        public int Level
        {
            get
            {
                return StartLevel > (Lines / 10) ? StartLevel : (Lines / 10);
            }
        }
        /// <summary>
        /// Determines drop speed based on current level
        /// </summary>
        private int DropSpeed
        {
            get
            {
                int dropFrameDelay = Level * -5 + 48;
                if (Level >= 29)
                {
                    dropFrameDelay = 1;
                }
                else if (Level >= 19)
                {
                    dropFrameDelay = 2;
                }
                else if (Level >= 16)
                {
                    dropFrameDelay = 3;
                }
                else if (Level >= 13)
                {
                    dropFrameDelay = 4;
                }
                else if (Level >= 10)
                {
                    dropFrameDelay = 5;
                }
                else if (Level == 9)
                {
                    dropFrameDelay = 6;
                }
                return dropFrameDelay;
            }
        }
        
        /// <summary>
        /// Creates a new instance of a TetfuzaBackend, and initializes an empty board
        /// </summary>
        public TetfuzaBackend(IInput keyboard, IDisplay screen, int startLevel)
        {
            _board = new TetfuzaBoard();
            _keyboard = new InputChecker(keyboard);
            _screen = screen;
            StartLevel = startLevel;
        }
        
        /// <summary>
        /// Main Loop
        /// </summary>
        /// <returns>Final Score</returns>
        public long Run()
        {
            _screen.ClearScreen();
            bool gameOver = false;
            _timer.Start();
            int pieceNum = _rand.Next(0, 7);
            NextPiece = new FuzaPiece((FuzaPieceType)pieceNum);
            while (!gameOver)
            {
                // Spawn a new piece and reset center
                CurrentPiece = NextPiece;
                _pieceCenter = new Coordinate(5, 2);

                // Determine next piece
                pieceNum = _rand.Next(0, 7);
                NextPiece = new FuzaPiece((FuzaPieceType)pieceNum);

                // Draw Board
                _screen.DrawGameplayScreen(this);

                bool isLockDown = false;
                while (!isLockDown)
                {
                    // Get user key press
                    int xDirection = 0;
                    int yDirection = 0;
                    int rotation = 0;
                    if (true == _keyboard.InputAvailable)
                    {
                        _keyboard.GetInput(ref xDirection,ref yDirection, ref rotation);

                        // Move and/or Rotate piece as appropriate
                        MovePieceLeftRight(xDirection);
                        RotatePiece(rotation);
                                      
                    }

                    // Move piece down at constant rate, or instant if user inputs down
                    bool autoDown = (_frameCount % DropSpeed) == 0;
                    if (yDirection == -1)
                    {
                        autoDown = true;
                        Score += 1;
                    }
                    if (autoDown)
                    {
                        isLockDown = !DropPiece();
                    }

                    _board.DrawPiece(CurrentPiece, _pieceCenter);

                    // Draw Screen
                    _screen.DrawGameplayScreen(this);

                    StableFrames.Stabilize(MS_PER_FRAME, _timer);
                    _frameCount++;
                }
                // Lock Piece and redraw it
                CurrentPiece.LockPiece();
                _board.DrawPiece(CurrentPiece, _pieceCenter);

                // Check if lines were cleared and update score
                int linesCleared = _board.ClearLines();
                if (linesCleared > 0)
                    AddClearedLinesToScore(linesCleared);
                

                // Check if player has topped out (game over)
                gameOver = _board.CheckTopOut();
            }
            // Plays the game over animation
            TopOutAnimation();
            return Score;
        }

        /// <summary>
        /// Checks the value of parameter "direction", and slides piece right for 1, or left for -1, 
        /// </summary>
        private void MovePieceLeftRight(int direction)
        {
            if (CheckMove(new Coordinate(_pieceCenter.xPos + direction,_pieceCenter.yPos), CurrentPiece))
                _pieceCenter.xPos = _pieceCenter.xPos + direction;
        }

        /// <summary>
        /// Checks the value of parameter "rotation", and rotates piece clockwise for 1, or counterclockwise for -1, 
        /// </summary>
        private void RotatePiece(int rotation)
        {
            FuzaPiece newPosition;
            
            if (-1 == rotation)
            {
                newPosition = CurrentPiece.RotateLeft();
                if (CheckMove(_pieceCenter, newPosition))
                    CurrentPiece = newPosition;
            }
            else if (1 == rotation)
            {
                newPosition = CurrentPiece.RotateRight();
                if (CheckMove(_pieceCenter, newPosition))
                    CurrentPiece = newPosition;
            }
        }

        

        /// <summary>
        /// Returns true if gives piece placement is valid
        /// </summary>
        /// <param name="center">Board coordinates of piece center</param>
        /// <param name="orientation">2-D List representation of a piece's orientation</param>
        /// <returns></returns>
        private bool CheckMove(Coordinate center, FuzaPiece orientation)
        {
            bool isValid = true;
            for (int row = 0; row < FuzaPiece.PIECE_SIZE; row++)
            {
                for (int col = 0; col < FuzaPiece.PIECE_SIZE; col++)
                {
                    GridSpace block = new GridSpace(orientation.Piece[row][col].Color);

                    // Find piece position in the playfield
                    int colCoord = center.xPos - (FuzaPiece.PIECE_SIZE / 2) + col;
                    int rowCoord = center.yPos - (FuzaPiece.PIECE_SIZE / 2) + row;

                    // Check for conflicts
                    if (((colCoord < 0 || colCoord >= TetfuzaBoard.BOARD_WIDTH
                        || rowCoord < 0 || rowCoord >= TetfuzaBoard.BOARD_HEIGHT)
                        && block.Color != BlockColor.Background)
                        || (block.Color != BlockColor.Background
                        && _board.Board[rowCoord][colCoord].IsLockedDownBlock))
                    {
                        isValid = false;
                    }
                }
            }
            return isValid;
        }

        /// <summary>
        /// Checks, clears, and returns the count of how many lines are full of pieces. 
        /// This is called after each piece locks down at the end of its fall duration. 
        /// </summary>
        /// <param name="linesCleared">Number of lines cleared</param>
        /// <returns></returns>
        private int AddClearedLinesToScore(int linesCleared)
        {
            // Update total line count
            Lines += linesCleared;

            // Update total score
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
            return linesCleared;
        }

        private bool DropPiece()
        {
            bool isValid = CheckMove(new Coordinate(_pieceCenter.xPos, _pieceCenter.yPos + 1), CurrentPiece);
            if (isValid)
                _pieceCenter.yPos = _pieceCenter.yPos + 1;
            _board.DrawPiece(CurrentPiece, _pieceCenter);
            return isValid;
        }

        public override string ToString()
        {
            return _board.ToString();
        }

        /// <summary>
        /// Draws the TOPOUTCHAR over the board one line at a time, starting at the top
        /// </summary>
        public void TopOutAnimation()
        {
            for (int i = 0; i < TetfuzaBoard.BOARD_HEIGHT; i++)
            {
                StableFrames.Stabilize(MS_PER_FRAME * 4, _timer);
                _board.ReplaceLine(i, _board.BoardLine(BlockColor.Topout));
                _screen.DrawGameplayScreen(this);
            }

            _screen.DrawGameplayScreen(this);
        }
    }
}