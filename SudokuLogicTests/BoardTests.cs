using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SudokuLogic;

namespace SudokuLogicTests
{
    [TestClass]
    public class BoardTests
    {
        Board SolvedSample = new Board
        {
            solution = new byte[,]
            {
                {6, 9, 8, 5, 3, 2, 1, 4, 7},
                {7, 1, 5, 6, 8, 4, 9, 2, 3},
                {3, 4, 2, 1, 7, 9, 8, 6, 5},
                {4, 8, 6, 7, 2, 3, 5, 9, 1},
                {9, 2, 1, 4, 5, 8, 3, 7, 6},
                {5, 3, 7, 9, 6, 1, 2, 8, 4},
                {2, 5, 4, 8, 1, 7, 6, 3, 9},
                {8, 6, 9, 3, 4, 5, 7, 1, 2},
                {1, 7, 3, 2, 9, 6, 4, 5, 8}
            }
        };

        [TestMethod]
        public void Box_Extraction()
        {
            Assert.IsTrue(Enumerable.SequenceEqual(new byte[] { 6, 9, 8, 7, 1, 5, 3, 4, 2 }, SolvedSample.GetBox(0)));
            Assert.IsTrue(Enumerable.SequenceEqual(new byte[] { 6, 3, 9, 7, 1, 2, 4, 5, 8 }, SolvedSample.GetBox(8)));
        }

        [TestMethod]
        public void Row_Extraction()
        {
            Assert.IsTrue(Enumerable.SequenceEqual(new byte[] { 6, 9, 8, 5, 3, 2, 1, 4, 7 }, SolvedSample.GetRow(0)));
            Assert.IsTrue(Enumerable.SequenceEqual(new byte[] { 1, 7, 3, 2, 9, 6, 4, 5, 8 }, SolvedSample.GetRow(8)));
        }

        [TestMethod]
        public void Col_Extraction()
        {
            Assert.IsTrue(Enumerable.SequenceEqual(new byte[] { 6, 7, 3, 4, 9, 5, 2, 8, 1 }, SolvedSample.GetCol(0)));
            Assert.IsTrue(Enumerable.SequenceEqual(new byte[] { 7, 3, 5, 1, 6, 4, 9, 2, 8 }, SolvedSample.GetCol(8)));
        }

        [TestMethod]
        public void SolutionValid_Correct()
        {
            Assert.IsTrue(SolvedSample.IsSolutionValid());
        }

        [TestMethod]
        public void SolutionValid_Incorrect_Double6InRow0()
        {
            var board = new Board
            {
                solution = new byte[,]
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
                }
            };

            Assert.IsFalse(board.IsSolutionValid());
        }

        [TestMethod]
        public void SolutionValid_Incomplete_Contains0()
        {
            var board = new Board
            {
                solution = new byte[,]
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
                }
            };

            Assert.IsTrue(board.IsSolutionValid());
        }

        [TestMethod]
        public void SolutionSolved_Correct()
        {
            Assert.IsTrue(SolvedSample.IsSolutionSolved());
        }

        [TestMethod]
        public void SolutionSolved_Incorrect_Double6InRow0()
        {
            var board = new Board
            {
                solution = new byte[,]
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
                }
            };

            Assert.IsFalse(board.IsSolutionSolved());
        }

        [TestMethod]
        public void SolutionSolved_Incorrect_Double7InMiddleBox()
        {
            var board = new Board
            {
                solution = new byte[,]
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
                }
            };

            Assert.IsFalse(board.IsSolutionSolved());
        }


        [TestMethod]
        public void SolutionSolved_Incorrect_Double1InColumn7()
        {
            var board = new Board
            {
                solution = new byte[,]
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
                }
            };

            Assert.IsFalse(board.IsSolutionSolved());
        }

        [TestMethod]
        public void SolutionSolved_Incomplete_Contains0()
        {
            var board = new Board
            {
                solution = new byte[,]
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
                }
            };

            Assert.IsFalse(board.IsSolutionSolved());
        }
    }
}
