using System;
using System.Collections.Generic;

namespace Tetfuza
{
	public enum FuzaType
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
		public List<List<char>> Piece { get; private set; }
		private FuzaType _type;
		private int rotation = 0;
		private List<Coordinate> _blocks;

		public FuzaPiece(FuzaType type)
		{
			_type = type;
            _blocks = GetBlockCoords();
            Piece = MakePiece();
		}

        private List<Coordinate> GetBlockCoords()
        {
            List<Coordinate> coords = null;
            switch (_type)
            {
                case FuzaType.TBlock:
                    coords = new List<Coordinate>
                    {
                        new Coordinate(0,0),
                        new Coordinate(0,1),
                        new Coordinate(-1,0),
                        new Coordinate(1,0)
                    };
                    break;
                case FuzaType.Squiggly:
                    coords = new List<Coordinate>
                    {
                        new Coordinate(0,0),
                        new Coordinate(0,-1),
                        new Coordinate(-1,-1),
                        new Coordinate(1,0)
                    };
                    break;
                case FuzaType.ReverseSquiggly:
                    coords = new List<Coordinate>
                    {
                        new Coordinate(0,0),
                        new Coordinate(1,0),
                        new Coordinate(-1,1),
                        new Coordinate(0,1)
                    };
                    break;
                case FuzaType.LBlock:
                    coords = new List<Coordinate>
                    {
                        new Coordinate(0,0),
                        new Coordinate(-1,1),
                        new Coordinate(-1,0),
                        new Coordinate(1,0)
                    };
                    break;
                case FuzaType.ReverseLBlock:
                    coords = new List<Coordinate>
                    {
                        new Coordinate(0,0),
                        new Coordinate(1,1),
                        new Coordinate(-1,0),
                        new Coordinate(1,0)
                    };
                    break;
                case FuzaType.LinePiece:
                    coords = new List<Coordinate>
                    {
                        new Coordinate(0,0),
                        new Coordinate(0,1),
                        new Coordinate(0,-1),
                        new Coordinate(0,-2)
                    };
                    break;
                case FuzaType.SquareBlock:
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
		
		private List<List<char>> MakePiece()
		{
            var piece = new List<List<char>>();
			for (int y = 0; y < 5; y++)
			{
				piece.Add(new List<char>());
				for (int x = 0; x < 5; x++)
				{
					piece[y].Add(' ');
				}
			}
            for (int i = 0; i < _blocks.Count; i++)
            {

            }
            return piece;
		}
		
		public void RotateLeft()
		{
			var newPos = new List<List<char>>(Piece);
            for (int y = 0; y < 5; y++)
			{
                newPos.Add(new List<char>());
                for (int x = 4; x >= 0; x--)
                {
                    newPos[y].Add(Piece[x][y]);
				}
			}
            Piece = newPos;
		}
		
		public void RotateRight()
		{
            var newPos = new List<List<char>>(Piece);
            for (int i = 0; i < 5; i++)
            {
                newPos.Add(new List<char>());
            }
            for (int y = 4; y >= 0; y--)
            {
                for (int x = 0; x < 5; x++)
                {
                    newPos[y].Add(Piece[x][y]);
                }
            }
            Piece = newPos;
        }
		
	}
}