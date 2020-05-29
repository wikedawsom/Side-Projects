using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Tetfuza;
using Tetfuza.Interfaces;
using static Tetfuza.Interfaces.IInput;

namespace TetfuzaTests
{
    public class FakeIDisplay : IDisplay
    {
        public bool DrawGameplayScreenCalled { get; private set; } = false;
        public bool ClearScreenCalled { get; private set; } = false;
        public bool RedrawFrameCalled { get; private set; } = false;
        public bool WriteTextCalled { get; private set; } = false;
        public bool PreviewNextPieceCalled { get; private set; } = false;
        public FakeIDisplay()
        {

        }

        public void ClearScreen()
        {
            ClearScreenCalled = true;
        }

        public void DrawGameplayScreen(TetfuzaBackend gameInfo)
        {
            DrawGameplayScreenCalled = true;
        }

        public void RedrawFrame()
        {
            RedrawFrameCalled = true;
        }

        public void WriteText(string text, decimal xPos, decimal yPos)
        {
            WriteTextCalled = true;
        }

        public void PreviewNextPiece(FuzaPiece nextPiece, decimal xPos, decimal yPos)
        {
            PreviewNextPieceCalled = true;
        }
    }
    [TestClass]
    public class TetfuzaBackendTests
    {
        public TetfuzaBackend _main;
        [TestInitialize]
        public void Initialize()
        {
            var input = new FakeIInput(Input.NoInput);
            var display = new FakeIDisplay();
            _main = new TetfuzaBackend(input, display, 0);
        }
    }
}
