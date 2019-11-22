using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Tetfuza;
using Tetfuza.Interfaces;
using static Tetfuza.Interfaces.IInput;

namespace TetfuzaTests
{
    public class FakeIConsole : IInput
    {
        private Input _toReturn;
        public bool ClearCalled { get; set; }

        public bool InputAvailable {
            get { return true; }
        }

        public FakeIConsole(Input toReturn)
        {
            _toReturn = toReturn;
        }
        public Input ReadInput()
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
            FakeIConsole fakeConsole = new FakeIConsole(Input.RotateCounterClockwise);
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
            int xDir = 0;
            int yDir = 0;
            int rot = 0;
            FakeIConsole fakeConsole = new FakeIConsole(Input.RotateCounterClockwise);
            InputChecker inputChecker = new InputChecker(fakeConsole);

            inputChecker.GetInput(ref xDir, ref yDir, ref rot);
            Assert.AreEqual(xDir, 0);
            Assert.AreEqual(yDir, 0);
            Assert.AreEqual(rot, -1);
        }

        [TestMethod]
        public void TestGetInputRotationRight()
        {
            int xDir = 0;
            int yDir = 0;
            int rot = 0;

            FakeIConsole fakeConsole = new FakeIConsole(Input.RotateClockwise);
            InputChecker inputChecker = new InputChecker(fakeConsole);
            inputChecker.GetInput(ref xDir, ref yDir, ref rot);

            Assert.AreEqual(xDir, 0);
            Assert.AreEqual(rot, 1);
            Assert.AreEqual(yDir, 0);
        }

        [TestMethod]
        public void TestGetInputDirectionLeft()
        {
            int xDir = 0;
            int yDir = 0;
            int rot = 0;

            FakeIConsole fakeConsole = new FakeIConsole(Input.Left);
            InputChecker inputChecker = new InputChecker(fakeConsole);
            inputChecker.GetInput(ref xDir, ref yDir, ref rot);

            Assert.AreEqual(xDir, -1);
            Assert.AreEqual(rot, 0);
            Assert.AreEqual(yDir, 0);
        }

        [TestMethod]
        public void TestGetInputDirectionRight()
        {
            int xDir = 0;
            int yDir = 0;
            int rot = 0;

            FakeIConsole fakeConsole = new FakeIConsole(Input.Right);
            InputChecker inputChecker = new InputChecker(fakeConsole);
            inputChecker.GetInput(ref xDir, ref yDir, ref rot);

            Assert.AreEqual(xDir, 1);
            Assert.AreEqual(rot, 0);
            Assert.AreEqual(yDir, 0);
        }

        [TestMethod]
        public void TestGetInputDown()
        {
            int xDir = 0;
            int yDir = 0;
            int rot = 0;

            FakeIConsole fakeConsole = new FakeIConsole(Input.Down);
            InputChecker inputChecker = new InputChecker(fakeConsole);
            inputChecker.GetInput(ref xDir, ref yDir, ref rot);

            Assert.AreEqual(xDir, 0);
            Assert.AreEqual(rot, 0);
            Assert.AreEqual(yDir, -1);
        }

        [TestMethod]
        public void TestGetInputClear()
        {
            int xDir = 0;
            int yDir = 0;
            int rot = 0;

            FakeIConsole fakeConsole = new FakeIConsole(Input.Option);
            InputChecker inputChecker = new InputChecker(fakeConsole);
            inputChecker.GetInput(ref xDir, ref yDir, ref rot);

            Assert.AreEqual(fakeConsole.ClearCalled, true);
        }

        [TestMethod]
        public void TestNoValidInput()
        {
            int xDir = 0;
            int yDir = 0;
            int rot = 0;

            FakeIConsole fakeConsole = new FakeIConsole(Input.NoInput);
            InputChecker inputChecker = new InputChecker(fakeConsole);
            inputChecker.GetInput(ref xDir, ref yDir, ref rot);

            Assert.AreEqual(xDir, 0);
            Assert.AreEqual(rot, 0);
            Assert.AreEqual(yDir, 0);
        }

        [TestMethod]
        public void TestKeyAvailable()
        {
            FakeIConsole fakeConsole = new FakeIConsole(Input.Left);
            InputChecker inputChecker = new InputChecker(fakeConsole);
            Assert.AreEqual(inputChecker.InputAvailable, true);
        }

    }
}
