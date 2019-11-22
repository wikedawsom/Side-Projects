using System;
using System.Collections.Generic;
using System.Text;

namespace Tetfuza.Interfaces
{
    public interface IInput
    {
        enum Input
        {
            NoInput = 0,
            RotateClockwise = 1,
            RotateCounterClockwise = 2,
            Left = 3,
            Right = 4,
            Up = 5,
            Down = 6,
            Pause = 7,
            Option = 8
        };
        public Input ReadInput();
        public void Clear();
        public bool InputAvailable { get; }
    }
}
