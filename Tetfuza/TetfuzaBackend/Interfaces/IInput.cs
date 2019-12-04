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
        /// <summary>
        /// Read a button that is currently being pressed, or wait for one
        /// </summary>
        /// <returns>The input value of the pressed button</returns>
        public Input ReadInput();
        /// <summary>
        /// Clear screen and/or input buffer
        /// </summary>
        public void Clear();
        /// <summary>
        /// Check if a button is being pressed
        /// </summary>
        public bool InputAvailable { get; }
    }
}
