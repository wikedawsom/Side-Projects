using System.Collections.Generic;
using System;

namespace Tetfuza
{
    public class TetfuzaBackend
    {
        private Random _rand = new Random();
        public List<List<char>> Board { get; private set; }

        public TetfuzaBackend()
        {
            Board = BoardInit();
        }
        private List<List<char>> BoardInit()
        {
            var startingBoard = new List<List<char>>();
            for (int height = 0; height < 22; height++)
            {
                startingBoard.Add(new List<char>());
                for (int width = 0; width < 10; height++)
                {
                    startingBoard[height].Add(' ');
                }
            }
            return startingBoard;
        }


    }
}