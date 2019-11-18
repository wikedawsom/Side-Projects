using System;
using System.Collections.Generic;
using System.Text;

namespace Tetfuza.Interfaces
{
    public interface IDisplay
    {
        public void DrawBoard(string boardString);
        public int DrawMenu(string prompt);
        public void WriteText(string text);
        public void ClearScreen();
        public void RedrawFrame();
    }
}
