using Microsoft.VisualStudio.TestTools.UnitTesting;
using SudokuLogic;
using static SudokuLogic.SimpleFunctions;

namespace SudokuLogicTests
{
    [TestClass]
    public class StrategyTests
    {
        [TestMethod]
        public void BasicPossibilitiesReduction()
        {
            //Arrange
            int col = 1;
            int row = 2;
            byte filledNumber = 3;
            int box = GetBoxNumber(col, row);

            var b = new Board();
            b.FillAllPossibilitiesTrue();
            b.solution[col, row] = filledNumber;

            for (int i = 0; i < 9; i++)
            {
                Assert.IsTrue(b.possibilities[i, row, filledNumber]);
                Assert.IsTrue(b.possibilities[col, i, filledNumber]);
                Assert.IsTrue(b.possibilities[GetColumnNumber(box, i), GetRowNumber(box, i), filledNumber]);
            }

            //Act
            int removedCount = Strategies.BasicPossibilitiesReduction(b);

            //Assert
            Assert.AreEqual(21, removedCount);
            for (int i = 0; i < 9; i++)
            {
                Assert.IsFalse(b.possibilities[i, row, filledNumber]);
                Assert.IsFalse(b.possibilities[col, i, filledNumber]);
                Assert.IsFalse(b.possibilities[GetColumnNumber(box, i), GetRowNumber(box, i), filledNumber]);
            }
            Assert.IsTrue(b.possibilities[8, 8, filledNumber]);
            Assert.IsTrue(b.possibilities[3, 3, filledNumber]);
        }
    }
}
