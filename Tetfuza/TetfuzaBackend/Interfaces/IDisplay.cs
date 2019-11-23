using System;
using System.Collections.Generic;
using System.Text;

namespace Tetfuza.Interfaces
{
    public interface IDisplay
    {
        /// <summary>
        /// Display the playfield (preferably in the center of the screen and with a stylish border)
        /// </summary>
        /// <param name="gameState">A string with all rows of the playfield, separated by ')'</param>
        public void DrawGameplayScreen(TetfuzaBackend gameInfo);
        /// <summary>
        /// Displays the upcoming piece
        /// </summary>
        public void PreviewNextPiece(FuzaPiece nextPiece, decimal xPos, decimal yPos);
        /// <summary>
        /// Writes text to screen, centered on xPos, yPos
        /// </summary>
        /// <param name="text"></param>
        /// <param name="xPos">A value between 0 (screen left), and 1 (screen right)</param>
        /// <param name="yPos">A value between 0 (screen bottom) and 1 (screen top)</param>
        public void WriteText(string text, decimal xPos, decimal yPos);
        /// <summary>
        /// Full Screen Clear (often slower, but more reliable than RedrawFrame())
        /// </summary>
        public void ClearScreen();
        /// <summary>
        /// Prepare screen contents to be overwritten (can leave visual artifacts if only partial screen is overwritten) 
        /// </summary>
        public void RedrawFrame();

    }
}
