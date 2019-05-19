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
            b.TrySetNumber(col, row, filledNumber, Origin.SolverFresh);

            for (int i = 0; i < 9; i++)
            {
                Assert.IsTrue(b.Possibilities[i, row, filledNumber]);
                Assert.IsTrue(b.Possibilities[col, i, filledNumber]);
                Assert.IsTrue(b.Possibilities[GetColNumber(box, i), GetRowNumber(box, i), filledNumber]);
            }

            //Act
            int removedCount = Strategies.BasicPossibilitiesReduction(b);

            //Assert
            Assert.AreEqual(21, removedCount);
            for (int i = 0; i < 9; i++)
            {
                Assert.IsFalse(b.Possibilities[i, row, filledNumber]);
                Assert.IsFalse(b.Possibilities[col, i, filledNumber]);
                Assert.IsFalse(b.Possibilities[GetColNumber(box, i), GetRowNumber(box, i), filledNumber]);
            }
            Assert.IsTrue(b.Possibilities[8, 8, filledNumber]);
            Assert.IsTrue(b.Possibilities[3, 3, filledNumber]);
        }
    }
}
