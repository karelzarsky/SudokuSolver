using Microsoft.VisualStudio.TestTools.UnitTesting;
using static SudokuLogic.SimpleFunctions;

namespace SudokuLogicTests
{
    [TestClass]
    public class SimpleFunctionsTests
    {
        [TestMethod]
        public void GroupValid_Correct()
        {
            Assert.IsTrue(IsGroupValid(new byte[] { 1, 2, 3, 4, 5, 6, 7, 8, 9 }));
            Assert.IsTrue(IsGroupValid(new byte[] { 9, 8, 7, 6, 5, 4, 3, 2, 1 }));
            Assert.IsTrue(IsGroupValid(new byte[] { 2, 3, 4, 5, 6, 7, 8, 9, 1 }));
        }

        [TestMethod]
        public void GroupValid_MissingNumber()
        {
            Assert.IsTrue(IsGroupValid(new byte[] { 1, 2, 3, 4, 0, 6, 7, 8, 9 }));
        }

        [TestMethod]
        public void GroupValid_DoubleNumber()
        {
            Assert.IsFalse(IsGroupValid(new byte[] { 1, 2, 3, 3, 5, 6, 7, 8, 9 }));
            Assert.IsFalse(IsGroupValid(new byte[] { 2, 3, 4, 5, 5, 5, 8, 9, 1 }));
        }

        [TestMethod]
        public void GroupSolved_Correct()
        {
            Assert.IsTrue(IsGroupSolved(new byte[] { 1, 2, 3, 4, 5, 6, 7, 8, 9 }));
            Assert.IsTrue(IsGroupSolved(new byte[] { 9, 8, 7, 6, 5, 4, 3, 2, 1 }));
            Assert.IsTrue(IsGroupSolved(new byte[] { 2, 3, 4, 5, 6, 7, 8, 9, 1 }));
        }

        [TestMethod]
        public void GroupSolved_DoubleNumber()
        {
            Assert.IsFalse(IsGroupSolved(new byte[] { 1, 2, 3, 3, 5, 6, 7, 8, 9 }));
            Assert.IsFalse(IsGroupSolved(new byte[] { 2, 3, 4, 5, 5, 5, 8, 9, 1 }));
        }

        [TestMethod]
        public void GroupSolved_MissingNumber()
        {
            Assert.IsFalse(IsGroupSolved(new byte[] { 1, 2, 3, 4, 0, 6, 7, 8, 9 }));
        }

        [TestMethod]
        public void GetBoxNumber_Tests()
        {
            Assert.AreEqual(0, GetBoxNumber(0, 0));
            Assert.AreEqual(0, GetBoxNumber(1, 0));
            Assert.AreEqual(1, GetBoxNumber(3, 0));
            Assert.AreEqual(2, GetBoxNumber(8, 0));
            Assert.AreEqual(0, GetBoxNumber(0, 1));
            Assert.AreEqual(0, GetBoxNumber(0, 2));
            Assert.AreEqual(3, GetBoxNumber(0, 3));
            Assert.AreEqual(8, GetBoxNumber(8, 8));
            Assert.AreEqual(8, GetBoxNumber(6, 6));
        }

        [TestMethod]
        public void GetColumnNumber_Test()
        {
            Assert.AreEqual(0, GetRowNumber(0, 0));
            Assert.AreEqual(1, GetRowNumber(0, 1));
            Assert.AreEqual(2, GetRowNumber(0, 8));
            Assert.AreEqual(3, GetRowNumber(1, 0));
            Assert.AreEqual(6, GetRowNumber(2, 0));
            Assert.AreEqual(8, GetRowNumber(2, 2));
            Assert.AreEqual(0, GetRowNumber(3, 0));
            Assert.AreEqual(3, GetRowNumber(4, 0));
            Assert.AreEqual(8, GetRowNumber(8, 8));
        }

        [TestMethod]
        public void GetRowNumber_Tests()
        {
            Assert.AreEqual(0, GetColumnNumber(0, 0));
            Assert.AreEqual(0, GetColumnNumber(0, 1));
            Assert.AreEqual(2, GetColumnNumber(0, 8));
            Assert.AreEqual(0, GetColumnNumber(1, 0));
            Assert.AreEqual(0, GetColumnNumber(2, 0));
            Assert.AreEqual(0, GetColumnNumber(2, 2));
            Assert.AreEqual(3, GetColumnNumber(3, 0));
            Assert.AreEqual(3, GetColumnNumber(4, 0));
            Assert.AreEqual(8, GetColumnNumber(8, 8));
        }
    }
}
