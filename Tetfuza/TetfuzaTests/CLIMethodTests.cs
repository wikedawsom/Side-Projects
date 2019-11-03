using Microsoft.VisualStudio.TestTools.UnitTesting;
using TetfuzaCLI;

namespace TetfuzaTests
{
    [TestClass]
    public class CLIMethodTests
    {
        [TestMethod]
        public void CommasInNumberTest()
        {
            // A number like "1000000" should be converted into a string
            // that looks like this "1,000,000"
            MainScreen menu = new MainScreen();
            Assert.AreEqual("1,000,000", menu.CommasInNumber(1000000));
            Assert.AreEqual("1,000", menu.CommasInNumber(1000));
            Assert.AreEqual("100", menu.CommasInNumber(100));
        }
    }
}
