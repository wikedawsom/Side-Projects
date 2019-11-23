using System;
using System.Collections.Generic;

namespace Tetfuza
{
	public enum FuzaPieceType
	{
		LBlock = 0,
		ReverseLBlock = 1,
		Squiggly = 2,
		ReverseSquiggly = 3,
		SquareBlock = 4,
		TBlock = 5,
		LinePiece = 6
	}
    public struct Coordinate
    {
        public int xPos;
        public int yPos;
        public Coordinate(int x, int y)
        {
            xPos = x;
            yPos = y;
        }
        
    }
	public class FuzaPiece
	{
        public const int PIECE_SIZE = 7;
		public List<List<GridSpace>> Piece { get; private set; }
		private FuzaPieceType _type;
		private List<Coordinate> _blocks;

		public FuzaPiece(FuzaPieceType type)
		{
			_type = type;
            _blocks = GetBlockCoords();
            Piece = MakePiece();
		}

        private FuzaPiece(List<List<GridSpace>> blocks, FuzaPiece oldPiece)
        {
            _type = oldPiece._type;
            _blocks = GetBlockCoords();
            Piece = blocks;
        }

        private List<Coordinate> GetBlockCoords()
        {
            List<Coordinate> coords = null;
            switch (_type)
            {
                case FuzaPieceType.TBlock:
                    coords = new List<Coordinate>
                    {
                        new Coordinate(0,0),
                        new Coordinate(0,1),
                        new Coordinate(-1,0),
                        new Coordinate(1,0)
                    };
                    break;
                case FuzaPieceType.Squiggly:
                    coords = new List<Coordinate>
                    {
                        new Coordinate(0,0),
                        new Coordinate(0,-1),
                        new Coordinate(-1,-1),
                        new Coordinate(1,0)
                    };
                    break;
                case FuzaPieceType.ReverseSquiggly:
                    coords = new List<Coordinate>
                    {
                        new Coordinate(0,0),
                        new Coordinate(1,0),
                        new Coordinate(-1,1),
                        new Coordinate(0,1)
                    };
                    break;
                case FuzaPieceType.LBlock:
                    coords = new List<Coordinate>
                    {
                        new Coordinate(0,0),
                        new Coordinate(-1,1),
                        new Coordinate(-1,0),
                        new Coordinate(1,0)
                    };
                    break;
                case FuzaPieceType.ReverseLBlock:
                    coords = new List<Coordinate>
                    {
                        new Coordinate(0,0),
                        new Coordinate(1,1),
                        new Coordinate(-1,0),
                        new Coordinate(1,0)
                    };
                    break;
                case FuzaPieceType.LinePiece:
                    coords = new List<Coordinate>
                    {
                        new Coordinate(0,0),
                        new Coordinate(1,0),
                        new Coordinate(-1,0),
                        new Coordinate(-2,0)
                    };
                    break;
                case FuzaPieceType.SquareBlock:
                    coords = new List<Coordinate>
                    {
                        new Coordinate(0,0),
                        new Coordinate(0,-1),
                        new Coordinate(-1,0),
                        new Coordinate(-1,-1)
                    };
                    break;
            }
            return coords;
        }
		
		private List<List<GridSpace>> MakePiece()
		{
            BlockColor color = BlockColor.Background;
            switch (_type)
            {
                case FuzaPieceType.TBlock:
                case FuzaPieceType.LinePiece:
                case FuzaPieceType.SquareBlock:
                    color = BlockColor.BlockColor1;
                    break;
                case FuzaPieceType.LBlock:
                case FuzaPieceType.Squiggly:
                    color = BlockColor.BlockColor2;
                    break;
                case FuzaPieceType.ReverseLBlock:
                case FuzaPieceType.ReverseSquiggly:
                    color = BlockColor.BlockColor3;
                    break;
                default:
                    break;
            }
            var piece = new List<List<GridSpace>>();
			for (int y = 0; y < PIECE_SIZE; y++)
			{
				piece.Add(new List<GridSpace>());
				for (int x = 0; x < PIECE_SIZE; x++)
				{
					piece[y].Add(new GridSpace());
				}
			}
            foreach (Coordinate fuza in _blocks)
            {
                int xFromCenter = PIECE_SIZE/2 + fuza.xPos;
                int yFromCenter = PIECE_SIZE/2 + fuza.yPos;
                piece[yFromCenter][xFromCenter] = new GridSpace(color);
            }
            return piece;
		}
		
		public FuzaPiece RotateRight()
		{
			var newPos = new List<List<GridSpace>>();
            
            for (int col = 0; col < PIECE_SIZE; col++)
			{
                newPos.Add(new List<GridSpace>());
                for (int row = 0; row < PIECE_SIZE; row++)
                {
                    newPos[col].Add(Piece[PIECE_SIZE - 1 - row][col]);
				}
			}
            return new FuzaPiece(newPos, this);
		}
		
		public FuzaPiece RotateLeft()
		{
            var newPos = new List<List<GridSpace>>();
            for (int col = 0; col < PIECE_SIZE; col++)
            {
                newPos.Add(new List<GridSpace>());
                for (int row = 0; row < PIECE_SIZE; row++)
                {
                    newPos[col].Add(Piece[row][PIECE_SIZE - 1 - col]);
                }
            }
            return new FuzaPiece(newPos, this);
        }

        public override string ToString()
        {
            string output = "";
            for (int row = PIECE_SIZE / 2 - 1; row < PIECE_SIZE / 2 + 2; row++) 
            {
                for (int col = PIECE_SIZE / 2 - 2; col < PIECE_SIZE/2 + 2; col++)
                {
                    output += (int)Piece[row][col].Color;
                }
                output += ")";
            }
            return output;
        }

        public void LockPiece()
        {
            foreach(var row in Piece)
            {
                foreach(var block in row)
                {
                    if(block.Color != BlockColor.Background)
                    {
                        block.IsLockedDownBlock = true;
                    }
                }
            }
        }

    }
}