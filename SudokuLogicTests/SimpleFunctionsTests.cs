using Microsoft.VisualStudio.TestTools.UnitTesting;
using static SudokuLogic.SimpleFunctions;

namespace SudokuLogicTests
{
    [TestClass]
    public class SimpleFunctionsTests
    {
        [TestMethod]
        public void BoxNumber_Tests()
        {
            Assert.AreEqual(0, BoxNumber(0, 0));
            Assert.AreEqual(0, BoxNumber(1, 0));
            Assert.AreEqual(1, BoxNumber(3, 0));
            Assert.AreEqual(2, BoxNumber(8, 0));
            Assert.AreEqual(0, BoxNumber(0, 1));
            Assert.AreEqual(0, BoxNumber(0, 2));
            Assert.AreEqual(3, BoxNumber(0, 3));
            Assert.AreEqual(8, BoxNumber(8, 8));
            Assert.AreEqual(8, BoxNumber(6, 6));
        }

        [TestMethod]
        public void BoxXCoordinates()
        {
            Assert.AreEqual(0, BoxX(0, 0));
            Assert.AreEqual(1, BoxX(0, 1));
            Assert.AreEqual(2, BoxX(0, 8));
            Assert.AreEqual(3, BoxX(1, 0));
            Assert.AreEqual(6, BoxX(2, 0));
            Assert.AreEqual(8, BoxX(2, 2));
            Assert.AreEqual(0, BoxX(3, 0));
            Assert.AreEqual(3, BoxX(4, 0));
            Assert.AreEqual(8, BoxX(8, 8));
        }

        [TestMethod]
        public void BoxYCoordinates()
        {
            Assert.AreEqual(0, BoxY(0, 0));
            Assert.AreEqual(0, BoxY(0, 1));
            Assert.AreEqual(2, BoxY(0, 8));
            Assert.AreEqual(0, BoxY(1, 0));
            Assert.AreEqual(0, BoxY(2, 0));
            Assert.AreEqual(0, BoxY(2, 2));
            Assert.AreEqual(3, BoxY(3, 0));
            Assert.AreEqual(3, BoxY(4, 0));
            Assert.AreEqual(8, BoxY(8, 8));
        }
    }
}
