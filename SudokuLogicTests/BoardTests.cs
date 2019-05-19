using Microsoft.VisualStudio.TestTools.UnitTesting;
using SudokuLogic;
using System.Linq;

namespace SudokuLogicTests
{
    [TestClass]
    public class BoardTests
    {
        private readonly Board SolvedSample;

        public BoardTests()
        {
            SolvedSample = new Board();
            SolvedSample.FillFromString(
                "698532147" +
                "715684923" +
                "342179865" +
                "486723591" +
                "921458376" +
                "537961284" +
                "254817639" +
                "869345712" +
                "173296458");
        }

        [TestMethod]
        public void Box_Extraction()
        {
            Assert.IsTrue(Enumerable.SequenceEqual(new byte[] { 6, 9, 8, 7, 1, 5, 3, 4, 2 }, SolvedSample.GetBox(0)));
            Assert.IsTrue(Enumerable.SequenceEqual(new byte[] { 6, 3, 9, 7, 1, 2, 4, 5, 8 }, SolvedSample.GetBox(8)));
        }

        [TestMethod]
        public void Column_Extraction()
        {
            Assert.IsTrue(Enumerable.SequenceEqual(new byte[] { 6, 9, 8, 5, 3, 2, 1, 4, 7 }, SolvedSample.GetColumn(0)));
            Assert.IsTrue(Enumerable.SequenceEqual(new byte[] { 1, 7, 3, 2, 9, 6, 4, 5, 8 }, SolvedSample.GetColumn(8)));
        }

        [TestMethod]
        public void Row_Extraction()
        {
            Assert.IsTrue(Enumerable.SequenceEqual(new byte[] { 6, 7, 3, 4, 9, 5, 2, 8, 1 }, SolvedSample.GetRow(0)));
            Assert.IsTrue(Enumerable.SequenceEqual(new byte[] { 7, 3, 5, 1, 6, 4, 9, 2, 8 }, SolvedSample.GetRow(8)));
        }

        [TestMethod]
        public void SolutionValid_Correct()
        {
            Assert.IsTrue(SolvedSample.IsValid());
        }

        [TestMethod]
        public void SolutionValid_Incorrect_Double6InRow0()
        {
            var board = new Board();
            board.SetSolution(new byte[,]
            {
                {6, 6, 8, 5, 3, 2, 1, 4, 7},
                {7, 1, 5, 6, 8, 4, 9, 2, 3},
                {3, 4, 2, 1, 7, 9, 8, 6, 5},
                {4, 8, 6, 7, 2, 3, 5, 9, 1},
                {9, 2, 1, 4, 5, 8, 3, 7, 6},
                {5, 3, 7, 9, 6, 1, 2, 8, 4},
                {2, 5, 4, 8, 1, 7, 6, 3, 9},
                {8, 6, 9, 3, 4, 5, 7, 1, 2},
                {1, 7, 3, 2, 9, 6, 4, 5, 8}
            });
            Assert.IsFalse(board.IsValid());
        }

        [TestMethod]
        public void SolutionValid_Incomplete_Contains0()
        {
            var board = new Board();
            board.SetSolution(new byte[,]
                {
                    {6, 9, 8, 5, 3, 2, 1, 4, 7},
                    {7, 0, 5, 6, 8, 4, 9, 2, 3},
                    {3, 4, 2, 1, 7, 9, 8, 6, 5},
                    {4, 8, 6, 7, 2, 3, 5, 9, 1},
                    {9, 2, 1, 4, 5, 8, 3, 7, 6},
                    {5, 3, 7, 9, 6, 1, 2, 8, 4},
                    {2, 5, 4, 8, 1, 7, 6, 3, 9},
                    {8, 6, 9, 3, 4, 5, 7, 1, 2},
                    {1, 7, 3, 2, 9, 6, 4, 5, 8}
                });

            Assert.IsTrue(board.IsValid());
        }

        [TestMethod]
        public void SolutionSolved_Correct()
        {
            Assert.IsTrue(SolvedSample.IsSolved());
        }

        [TestMethod]
        public void SolutionSolved_Incorrect_Double6InRow0()
        {
            var board = new Board();
            board.SetSolution(new byte[,]
            {
                {6, 6, 8, 5, 3, 2, 1, 4, 7},
                {7, 1, 5, 6, 8, 4, 9, 2, 3},
                {3, 4, 2, 1, 7, 9, 8, 6, 5},
                {4, 8, 6, 7, 2, 3, 5, 9, 1},
                {9, 2, 1, 4, 5, 8, 3, 7, 6},
                {5, 3, 7, 9, 6, 1, 2, 8, 4},
                {2, 5, 4, 8, 1, 7, 6, 3, 9},
                {8, 6, 9, 3, 4, 5, 7, 1, 2},
                {1, 7, 3, 2, 9, 6, 4, 5, 8}
            });

            Assert.IsFalse(board.IsSolved());
        }

        [TestMethod]
        public void SolutionSolved_Incorrect_Double7InMiddleBox()
        {
            var board = new Board();
            board.SetSolution(new byte[,]
            {
                {6, 9, 8, 5, 3, 2, 1, 4, 7},
                {7, 1, 5, 6, 8, 4, 9, 2, 3},
                {3, 4, 2, 1, 7, 9, 8, 6, 5},
                {4, 8, 6, 7, 2, 3, 5, 9, 1},
                {9, 2, 1, 4, 7, 8, 3, 7, 6},
                {5, 3, 7, 9, 6, 1, 2, 8, 4},
                {2, 5, 4, 8, 1, 7, 6, 3, 9},
                {8, 6, 9, 3, 4, 5, 7, 1, 2},
                {1, 7, 3, 2, 9, 6, 4, 5, 8}
            });

            Assert.IsFalse(board.IsSolved());
        }

        [TestMethod]
        public void SolutionSolved_Incorrect_Double1InColumn7()
        {
            var board = new Board();
            board.SetSolution(new byte[,]
            {
                {6, 9, 8, 5, 3, 2, 1, 1, 7},
                {7, 1, 5, 6, 8, 4, 9, 2, 3},
                {3, 4, 2, 1, 7, 9, 8, 6, 5},
                {4, 8, 6, 7, 2, 3, 5, 9, 1},
                {9, 2, 1, 4, 5, 8, 3, 7, 6},
                {5, 3, 7, 9, 6, 1, 2, 8, 4},
                {2, 5, 4, 8, 1, 7, 6, 3, 9},
                {8, 6, 9, 3, 4, 5, 7, 1, 2},
                {1, 7, 3, 2, 9, 6, 4, 5, 8}
            });

            Assert.IsFalse(board.IsSolved());
        }

        [TestMethod]
        public void SolutionSolved_Incomplete_Contains0()
        {
            var board = new Board();
            board.SetSolution(new byte[,]
            {
                {6, 9, 8, 5, 3, 2, 1, 4, 7},
                {7, 0, 5, 6, 8, 4, 9, 2, 3},
                {3, 4, 2, 1, 7, 9, 8, 6, 5},
                {4, 8, 6, 7, 2, 3, 5, 9, 1},
                {9, 2, 1, 4, 5, 8, 3, 7, 6},
                {5, 3, 7, 9, 6, 1, 2, 8, 4},
                {2, 5, 4, 8, 1, 7, 6, 3, 9},
                {8, 6, 9, 3, 4, 5, 7, 1, 2},
                {1, 7, 3, 2, 9, 6, 4, 5, 8}
            });

            Assert.IsFalse(board.IsSolved());
        }
    }
}
