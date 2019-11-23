using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;
using Tetfuza;

namespace TetfuzaTests
{
    [TestClass]
    public class BoardTests
    {
        private TetfuzaBoard _board;
        [TestInitialize]
        public void Initialize()
        {
            _board = new TetfuzaBoard();
        }
        [TestMethod]
        public void BoardLineTest()
        {

        }
    }
}
