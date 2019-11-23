using FramerateStabilizer;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using Tetfuza.Interfaces;

namespace Tetfuza
{
    public class TetfuzaBoard
    {

        public const int BOARD_HEIGHT = 22;
        public const int BOARD_WIDTH = 10;

        public List<List<GridSpace>> Board { get; private set; }

        private List<GridSpace> EmptyLine
        {
            get
            {
                return BoardLine(BlockColor.Background);
            }
        }
        /// <summary>
        /// Create a new board with {BOARD_HEIGHT} rows, and {BOARD_WIDTH} columns, all filled with ' ' chars
        /// </summary>
        public TetfuzaBoard()
        {
            Board = new List<List<GridSpace>>();
            for (int height = 0; height < BOARD_HEIGHT; height++)
            {
                Board.Add(EmptyLine);
            }
        }

        /// <summary>
        /// Creates a new row to be added to the board
        /// </summary>
        /// <param name="fillerColor">The color data to store in that grid space</param>
        /// <returns>A list of char, filled with fillerChar</returns>
        public List<GridSpace> BoardLine(BlockColor fillerColor)
        {
            var line = new List<GridSpace>();
            for (int i = 0; i < BOARD_WIDTH; i++)
            {
                line.Add(new GridSpace(fillerColor));
            }
            return line;
        }

        /// <summary>
        /// Converts the board to a single string, with each row separated by ')'
        /// </summary>
        /// <returns>The current state of the board</returns>
        public override string ToString()
        {
            string boardString = "";
            for (int row = 2; row < Board.Count; row++)
            {
                for (int col = 0; col < BOARD_WIDTH; col++)
                {
                    boardString += (int)Board[row][col].Color;
                }
                boardString += ")";
            }

            return boardString;
        }

        /// <summary>
        /// Takes data from CurrentPiece.Piece (a 2-D List of char, which represents piece orientation), 
        /// and copies it to the Board, centered around the coordinated defined by "pieceCenter"
        /// </summary>
        /// <param name="fuzaChar">The character to write to the Board</param>
        public void DrawPiece(FuzaPiece currentPiece, Coordinate pieceCenter)
        {
            const int CENTER = FuzaPiece.PIECE_SIZE / 2;
            BlockColor color = currentPiece.Piece[CENTER][CENTER].Color;
            for (int row = 0; row < FuzaPiece.PIECE_SIZE; row++)
            {
                for (int col = 0; col < FuzaPiece.PIECE_SIZE; col++)
                {
                    GridSpace block = new GridSpace(currentPiece.Piece[row][col]);

                    // Find piece position in the playfield
                    int colCoord = pieceCenter.xPos - (CENTER) + col;
                    int rowCoord = pieceCenter.yPos - (CENTER) + row;

                    // Update playfield
                    if (colCoord >= 0 && colCoord < BOARD_WIDTH
                        && rowCoord >= 0 && rowCoord < BOARD_HEIGHT)
                    {
                        if (block.Color != BlockColor.Background)
                        {
                            Board[rowCoord][colCoord] = block;
                        }
                        // Overwrite any potential ghost trails
                        else if (block.Color == BlockColor.Background && !Board[rowCoord][colCoord].IsLockedDownBlock)
                        {
                            Board[rowCoord][colCoord] = block;
                        }
                    }
                }
            }
        }
        
        /// <summary>
        /// Removes full rows from the board and returns the number of rows removed
        /// </summary>
        /// <returns></returns>
        public int ClearLines()
        {
            int linesCleared = 0;
            List<int> clearedIndex = new List<int>();
            for (int i = 0; i < BOARD_HEIGHT; i++)
            {
                bool full = true;
                for (int j = 0; j < BOARD_WIDTH; j++)
                {
                    if(!Board[i][j].IsLockedDownBlock)
                    {
                        full = false;
                    }
                }
                if (full)
                {
                    linesCleared++;
                    Board.RemoveAt(i);
                    Board.Insert(0, EmptyLine);
                }
            }
            return linesCleared;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="index"></param>
        /// <param name="newLine"></param>
        public void ReplaceLine(int index, List<GridSpace> newLine)
        {
            Board[index] = newLine;
        }

        /// <summary>
        /// Determines if the pieces have stacked to the top of the screen. Should be called after a piece has locked down, and before the next piece is put on the board.
        /// </summary>
        /// <returns>True if index 2,6 is not empty</returns>
        public bool CheckTopOut()
        {
            return Board[2][6].Color != BlockColor.Background;
        }
    }
}
