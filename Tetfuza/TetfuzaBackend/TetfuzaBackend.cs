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
        public const char TOPOUT_CHAR = '%';
        public const int MS_PER_FRAME = 17;
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
        private List<char> EmptyLine
        {
            get
            {
                return BoardLine(EMPTY_CHAR);
            }
        }
        
        /// <summary>
        /// Creates a new instance of a TetfuzaBackend, and initializes an empty board
        /// </summary>
        public TetfuzaBackend()
        {
            Board = BoardInit();
        }
        /// <summary>
        /// Starts the game on a specified level (which determines automatic drop speed and point multiplier)
        /// </summary>
        /// <param name="startLevel"></param>
        public TetfuzaBackend(int startLevel)
        {
            Board = BoardInit();
            StartLevel = startLevel;
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

        private List<char> BoardLine(char fillerChar)
        {
            var line = new List<char>();
            for (int i = 0; i < BOARD_WIDTH; i++)
            {
                line.Add(fillerChar);
            }
            return line;
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
        /// <returns>Final Score</returns>
        public long Run()
        {
            bool gameOver = false;
            _timer.Start();
            int pieceNum = _rand.Next(0, 7);
            AfterPiece = new FuzaPiece((FuzaType)pieceNum);
            while (!gameOver)
            {
                // Spawn a new piece and reset center
                CurrentPiece = AfterPiece;
                _pieceCenter = new Coordinate(5, 2);

                // Determine next piece
                pieceNum = _rand.Next(0, 7);
                AfterPiece = new FuzaPiece((FuzaType)pieceNum);

                // Check if player has topped out (game over)
                gameOver = CheckTopOut();

                bool isLockDown = false;
                while (!isLockDown)
                {
                    // Check Inputs
                    MovePieceLeftRight();
                    RotatePiece();

                    DrawPiece();

                    // Move piece down at constant rate, or instant if user inputs down
                    bool autoDown = _frameCount % DropSpeed == 0;
                    if (_userInputDown)
                    {
                        autoDown = true;
                        _userInputDown = false;
                        Score += 1;
                    }
                    if (autoDown)
                        isLockDown = !DropPiece();
                    StableFrames.Stabilize(MS_PER_FRAME, _timer);
                    _frameCount++;
                }
                // Represent locked piece on board with a different character
                DrawPiece(LOCKDOWN_CHAR);

                // Check if lines were cleared and update score
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
            // Plays the game over animation
            TopOutAnimation();
            return Score;
        }
        /// <summary>
        /// Takes data from CurrentPiece.Piece (a 2-D List that represents piece orientation), 
        /// and copies it to the Board, centered around member variable "_pieceCenter"
        /// </summary>
        /// <param name="fuzaChar">The character to write to the Board</param>
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
        /// <summary>
        /// Checks the value of member variable "_userInputDirection", and slides piece right for 1, or left for -1, 
        /// then resets the default value of "_userInputDirection"
        /// </summary>
        private void MovePieceLeftRight()
        {
            if (CheckMove(new Coordinate(_pieceCenter.xPos + _userInputDirection,_pieceCenter.yPos), CurrentPiece))
                _pieceCenter.xPos = _pieceCenter.xPos + _userInputDirection;
            
            _userInputDirection = 0;
        }
        /// <summary>
        /// Checks the value of member variable "_userInputRotation", and rotates piece clockwise for 1, or counterclockwise for -1, 
        /// then resets the default value of "_userInputRotation"
        /// </summary>
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
        /// <summary>
        /// Determines when the pieces have stacked to the top of the screen
        /// </summary>
        /// <returns></returns>
        private bool CheckTopOut()
        {
            return Board[2][6] != EMPTY_CHAR;
        }
        /// <summary>
        /// Draws the TOPOUTCHAR over the board one line at a time, starting at the top
        /// </summary>
        private void TopOutAnimation()
        {
            for (int i = 0; i < BOARD_HEIGHT; i++)
            {
                StableFrames.Stabilize(MS_PER_FRAME * 5, _timer);
                //Board.RemoveAt(BOARD_HEIGHT - 1);
                //Board.Insert(0, BoardLine(TOPOUT_CHAR));
                Board[i] = BoardLine(TOPOUT_CHAR);
            }

            var gameO1 = new List<char>();
            gameO1.AddRange(BoardLine(TOPOUT_CHAR));
            gameO1.InsertRange(3, "GAME");
            Board[BOARD_HEIGHT / 2] = gameO1;

            var gameO2 = new List<char>();
            gameO2.AddRange(BoardLine(TOPOUT_CHAR));
            gameO2.InsertRange(3, "OVER");
            Board[BOARD_HEIGHT / 2 + 1] = gameO2;

            StableFrames.Stabilize(MS_PER_FRAME*4, _timer);
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
        /// <summary>
        /// Checks, clears, and returns the count of how many lines are full of pieces. 
        /// This is called after each piece locks down at the end of its fall duration. 
        /// </summary>
        /// <returns></returns>
        private int CheckClear()
        {
            int linesCleared = 0;
            List<int> clearedIndex = new List<int>();
            for (int i = 0; i < BOARD_HEIGHT; i++)
            {
                if (!(Board[i].Contains(EMPTY_CHAR) || Board[i].Contains(MOVING_CHAR)))
                {
                    linesCleared++;
                    Board[i] = BoardLine('-');
                    clearedIndex.Add(i);
                }
            }
            StableFrames.Stabilize(MS_PER_FRAME * 10, _timer);
            for (int index = 0; index < linesCleared; index++)
            {
                Board.RemoveAt(clearedIndex[index]);
                Board.Insert(0, EmptyLine);
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
        /// <param name="down">True will move the piece down one space on next frame, False will wait for auto-drop</param>
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