using System;
using Tetfuza.Interfaces;
using static Tetfuza.Interfaces.IInput;

namespace Tetfuza
{
    

    public class InputChecker
    {
        private IInput _keyboard;

        public InputChecker(IInput keyboard)
        {
            _keyboard = keyboard;
        }

        /// <summary>
        /// returns true when a key was pressed and the value is waiting in the input buffer.
        /// when true, Console.ReadKey will not block.
        /// </summary>
        /// <returns>
        /// true - a value is in the input buffer
        /// false - no value is available, ReadKey will block
        /// </returns>
        public bool InputAvailable
        {
            get { return _keyboard.InputAvailable; }
        }

        /// <summary>
        /// returns the rotation, direction, and whether the user pressed the
        /// down key, so we can adjust the pieces on the game board.
        /// </summary>
        /// <param name="direction">returns (-1, 0, or 1 for left, none, and right movement)</param>
        /// <param name="rotation">returns (-1, 0, or 1 for counterclockwise, none, and clockwise rotation)</param>
        /// <param name="down">returns true to move the piece down one space on next frame, False will wait for auto-drop</param>
        /// <returns>
        /// </returns>
        public void GetInput(ref int xDirection, ref int yDirection, ref int rotation)
        {
            Input key = _keyboard.ReadInput();

            switch (key)
            {
                case Input.Left:
                    xDirection = -1;
                    break;
                case Input.Right:
                    xDirection = 1;
                    break;
                case Input.RotateCounterClockwise:
                    rotation = -1;
                    break;
                case Input.RotateClockwise:
                    rotation = 1;
                    break;
                case Input.Up:
                    yDirection = 1;
                    break;
                case Input.Down:
                    yDirection = -1;
                    break;
                case Input.Option:
                    _keyboard.ClearInputBuffer();
                    break;
                case Input.Pause:
                    // Pause the game or something...
                    break;
                default:
                    break;
            }
        }
    }
}
