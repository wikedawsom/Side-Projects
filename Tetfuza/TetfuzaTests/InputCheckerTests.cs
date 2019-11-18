using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Tetfuza;
using Tetfuza.Interfaces;

namespace TetfuzaTests
{
    public class FakeIConsole : IConsole
    {
        private ConsoleKey _toReturn;
        public bool ClearCalled { get; set; }

        public bool KeyAvailable {
            get { return true; }
        }

        public FakeIConsole(ConsoleKey toReturn)
        {
            _toReturn = toReturn;
        }
        public ConsoleKey ReadKey()
        {
            return _toReturn;
        }
        public void Clear()
        {
            ClearCalled = true;
        }
    }
    [TestClass]
    public class InputCheckerTests
    {
        InputChecker _inputChecker;

        [TestInitialize]
        public void Initialize()
        {
            FakeIConsole fakeConsole = new FakeIConsole(ConsoleKey.Z);
            _inputChecker = new InputChecker(fakeConsole);
        }

        [TestMethod]
        public void InputCheckerConstructorTest()
        {
            Assert.IsInstanceOfType(_inputChecker, typeof(InputChecker));
        }

        [TestMethod]
        public void TestGetInputRotationLeft()
        {
            int dir = 0;
            int rot = 0;
            bool down = false;
            FakeIConsole fakeConsole = new FakeIConsole(ConsoleKey.Z);
            InputChecker inputChecker = new InputChecker(fakeConsole);

            inputChecker.GetInput(ref dir, ref rot, ref down);
            Assert.AreEqual(dir, 0);
            Assert.AreEqual(rot, -1);
            Assert.AreEqual(down, false);
        }

        [TestMethod]
        public void TestGetInputRotationRight()
        {
            int dir = 0;
            int rot = 0;
            bool down = false;

            FakeIConsole fakeConsole = new FakeIConsole(ConsoleKey.X);
            InputChecker inputChecker = new InputChecker(fakeConsole);
            inputChecker.GetInput(ref dir, ref rot, ref down);

            Assert.AreEqual(dir, 0);
            Assert.AreEqual(rot, 1);
            Assert.AreEqual(down, false);
        }

        [TestMethod]
        public void TestGetInputDirectionLeft()
        {
            int dir = 0;
            int rot = 0;
            bool down = false;

            FakeIConsole fakeConsole = new FakeIConsole(ConsoleKey.LeftArrow);
            InputChecker inputChecker = new InputChecker(fakeConsole);
            inputChecker.GetInput(ref dir, ref rot, ref down);

            Assert.AreEqual(dir, -1);
            Assert.AreEqual(rot, 0);
            Assert.AreEqual(down, false);
        }

        [TestMethod]
        public void TestGetInputDirectionRight()
        {
            int dir = 0;
            int rot = 0;
            bool down = false;

            FakeIConsole fakeConsole = new FakeIConsole(ConsoleKey.RightArrow);
            InputChecker inputChecker = new InputChecker(fakeConsole);
            inputChecker.GetInput(ref dir, ref rot, ref down);

            Assert.AreEqual(dir, 1);
            Assert.AreEqual(rot, 0);
            Assert.AreEqual(down, false);
        }

        [TestMethod]
        public void TestGetInputDown()
        {
            int dir = 0;
            int rot = 0;
            bool down = false;

            FakeIConsole fakeConsole = new FakeIConsole(ConsoleKey.DownArrow);
            InputChecker inputChecker = new InputChecker(fakeConsole);
            inputChecker.GetInput(ref dir, ref rot, ref down);

            Assert.AreEqual(dir, 0);
            Assert.AreEqual(rot, 0);
            Assert.AreEqual(down, true);
        }

        [TestMethod]
        public void TestGetInputClear()
        {
            int dir = 0;
            int rot = 0;
            bool down = false;

            FakeIConsole fakeConsole = new FakeIConsole(ConsoleKey.C);
            InputChecker inputChecker = new InputChecker(fakeConsole);
            inputChecker.GetInput(ref dir, ref rot, ref down);

            Assert.AreEqual(fakeConsole.ClearCalled, true);
        }

        [TestMethod]
        public void TestNoValidInput()
        {
            int dir = 0;
            int rot = 0;
            bool down = false;

            FakeIConsole fakeConsole = new FakeIConsole(ConsoleKey.Y);
            InputChecker inputChecker = new InputChecker(fakeConsole);
            inputChecker.GetInput(ref dir, ref rot, ref down);

            Assert.AreEqual(dir, 0);
            Assert.AreEqual(rot, 0);
            Assert.AreEqual(down, false);
        }

        [TestMethod]
        public void TestKeyAvailable()
        {
            FakeIConsole fakeConsole = new FakeIConsole(ConsoleKey.Y);
            InputChecker inputChecker = new InputChecker(fakeConsole);
            Assert.AreEqual(inputChecker.InputAvailable, true);
        }

    }
}
