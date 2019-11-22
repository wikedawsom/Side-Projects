using System;
using System.Collections.Generic;
using System.Text;

namespace Tetfuza.Interfaces
{
    public interface IDisplay
    {
        public void DrawBoard(TetfuzaMain gameState);
        /// <summary>
        /// Writes text centered on xPos, yPos
        /// </summary>
        /// <param name="text"></param>
        /// <param name="xPos">A value between 0 and 1, </param>
        /// <param name="yPos"></param>
        public void WriteText(string text, decimal xPos, decimal yPos);
        public void ClearScreen();
        public void RedrawFrame();
    }
}
