using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Tetfuza;
using Tetfuza.Interfaces;
using static Tetfuza.Interfaces.IInput;

namespace TetfuzaTests
{
    public class FakeIInput : IInput
    {
        private Input _toReturn;
        public bool ClearCalled { get; set; }

        public bool InputAvailable {
            get { return true; }
        }

        public FakeIInput(Input toReturn)
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
            FakeIInput fakeConsole = new FakeIInput(Input.RotateCounterClockwise);
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
            FakeIInput fakeConsole = new FakeIInput(Input.RotateCounterClockwise);
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

            FakeIInput fakeConsole = new FakeIInput(Input.RotateClockwise);
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

            FakeIInput fakeConsole = new FakeIInput(Input.Left);
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

            FakeIInput fakeConsole = new FakeIInput(Input.Right);
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

            FakeIInput fakeConsole = new FakeIInput(Input.Down);
            InputChecker inputChecker = new InputChecker(fakeConsole);
            inputChecker.GetInput(ref xDir, ref yDir, ref rot);

            Assert.AreEqual(xDir, 0);
            Assert.AreEqual(rot, 0);
            Assert.AreEqual(yDir, -1);
        }

        [TestMethod]
        public void TestGetInputUp()
        {
            int xDir = 0;
            int yDir = 0;
            int rot = 0;

            FakeIInput fakeConsole = new FakeIInput(Input.Up);
            InputChecker inputChecker = new InputChecker(fakeConsole);
            inputChecker.GetInput(ref xDir, ref yDir, ref rot);

            Assert.AreEqual(xDir, 0);
            Assert.AreEqual(rot, 0);
            Assert.AreEqual(yDir, 1);
        }

        [TestMethod]
        public void TestGetInputClear()
        {
            int xDir = 0;
            int yDir = 0;
            int rot = 0;

            FakeIInput fakeConsole = new FakeIInput(Input.Option);
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

            FakeIInput fakeConsole = new FakeIInput(Input.NoInput);
            InputChecker inputChecker = new InputChecker(fakeConsole);
            inputChecker.GetInput(ref xDir, ref yDir, ref rot);

            Assert.AreEqual(xDir, 0);
            Assert.AreEqual(rot, 0);
            Assert.AreEqual(yDir, 0);
        }

        [TestMethod]
        public void TestKeyAvailable()
        {
            FakeIInput fakeConsole = new FakeIInput(Input.Left);
            InputChecker inputChecker = new InputChecker(fakeConsole);
            Assert.AreEqual(inputChecker.InputAvailable, true);
        }

    }
}
