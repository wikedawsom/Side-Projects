using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;
using Tetfuza;
using static Tetfuza.Interfaces.IInput;

namespace TetfuzaTests
{
    [TestClass]
    public class TetfuzaMainTests
    {
        public TetfuzaMenu _main;
        [TestInitialize]
        public void Initialize()
        {
            var input = new FakeIInput(Input.NoInput);
            var display = new FakeIDisplay();
            _main = new TetfuzaMenu(input, display);
        }
        [TestMethod]
        public void CommasInNumberTest()
        {
            // A number like "1000000" should be converted into a string
            // that looks like this "1,000,000"

            Assert.AreEqual("1,000,000", _main.CommasInNumber(1000000));
            Assert.AreEqual("1,000", _main.CommasInNumber(1000));
            Assert.AreEqual("100", _main.CommasInNumber(100));
        }
    }
}
