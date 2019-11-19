using System;
using System.Collections.Generic;
using System.Text;

namespace Tetfuza.Interfaces
{
    public interface IDisplay
    {
        public void GameScreen(TetfuzaBackend gameState);
        public int DrawMenu(string prompt);
        public void WriteText(string text);
        public void ClearScreen();
        public void RedrawFrame();
    }
}
