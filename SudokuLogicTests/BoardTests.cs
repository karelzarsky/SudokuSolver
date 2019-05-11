using Microsoft.VisualStudio.TestTools.UnitTesting;
using SudokuLogic;

namespace SudokuLogicTests
{
    [TestClass]
    public class BoardTests
    {
        [TestMethod]
        public void SolutionSolved_Correct()
        {
            var board = new Board
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

            Assert.IsTrue(board.IsSolutionSolved());
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

        [TestMethod]
        public void SolutionValid_Correct()
        {
            var board = new Board
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

            Assert.IsTrue(board.IsSolutionValid());
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
        public void GroupSolved_CorrectSolution()
        {
            var board = new Board();
            Assert.IsTrue(board.IsGroupSolved(new byte[] { 1, 2, 3, 4, 5, 6, 7, 8, 9 }));
            Assert.IsTrue(board.IsGroupSolved(new byte[] { 9, 8, 7, 6, 5, 4, 3, 2, 1 }));
            Assert.IsTrue(board.IsGroupSolved(new byte[] { 2, 3, 4, 5, 6, 7, 8, 9, 1 }));
        }

        [TestMethod]
        public void GroupSolved_DoubleNumber()
        {
            var board = new Board();
            Assert.IsFalse(board.IsGroupSolved(new byte[] { 1, 2, 3, 3, 5, 6, 7, 8, 9 }));
            Assert.IsFalse(board.IsGroupSolved(new byte[] { 2, 3, 4, 5, 5, 5, 8, 9, 1 }));
        }

        [TestMethod]
        public void GroupSolved_MissingNumber()
        {
            var board = new Board();
            Assert.IsFalse(board.IsGroupSolved(new byte[] { 1, 2, 3, 4, 0, 6, 7, 8, 9 }));
        }

        [TestMethod]
        public void GroupValid_CorrectSolution()
        {
            var board = new Board();
            Assert.IsTrue(board.IsGroupValid(new byte[] { 1, 2, 3, 4, 5, 6, 7, 8, 9 }));
            Assert.IsTrue(board.IsGroupValid(new byte[] { 9, 8, 7, 6, 5, 4, 3, 2, 1 }));
            Assert.IsTrue(board.IsGroupValid(new byte[] { 2, 3, 4, 5, 6, 7, 8, 9, 1 }));
        }

        [TestMethod]
        public void GroupValid_DoubleNumber()
        {
            var board = new Board();
            Assert.IsFalse(board.IsGroupValid(new byte[] { 1, 2, 3, 3, 5, 6, 7, 8, 9 }));
            Assert.IsFalse(board.IsGroupValid(new byte[] { 2, 3, 4, 5, 5, 5, 8, 9, 1 }));
        }

        [TestMethod]
        public void GroupValid_MissingNumber()
        {
            var board = new Board();
            Assert.IsTrue(board.IsGroupValid(new byte[] { 1, 2, 3, 4, 0, 6, 7, 8, 9 }));
        }

        [TestMethod]
        public void Box_Extraction()
        {
            var board = new Board
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

            Assert.AreEqual(6, board.Box(0)[0]);
            Assert.AreEqual(9, board.Box(0)[1]);
            Assert.AreEqual(8, board.Box(0)[2]);
            Assert.AreEqual(7, board.Box(0)[3]);
            Assert.AreEqual(1, board.Box(0)[4]);
            Assert.AreEqual(5, board.Box(0)[5]);
            Assert.AreEqual(3, board.Box(0)[6]);
            Assert.AreEqual(4, board.Box(0)[7]);
            Assert.AreEqual(2, board.Box(0)[8]);

            Assert.AreEqual(6, board.Box(8)[0]);
            Assert.AreEqual(3, board.Box(8)[1]);
            Assert.AreEqual(9, board.Box(8)[2]);
            Assert.AreEqual(7, board.Box(8)[3]);
            Assert.AreEqual(1, board.Box(8)[4]);
            Assert.AreEqual(2, board.Box(8)[5]);
            Assert.AreEqual(4, board.Box(8)[6]);
            Assert.AreEqual(5, board.Box(8)[7]);
            Assert.AreEqual(8, board.Box(8)[8]);
        }

        [TestMethod]
        public void Row_Extraction()
        {
            var board = new Board
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

            Assert.AreEqual(board.Row(0)[0], 6);
            Assert.AreEqual(board.Row(0)[1], 9);
            Assert.AreEqual(board.Row(0)[2], 8);
            Assert.AreEqual(board.Row(0)[3], 5);
            Assert.AreEqual(board.Row(0)[4], 3);
            Assert.AreEqual(board.Row(0)[5], 2);
            Assert.AreEqual(board.Row(0)[6], 1);
            Assert.AreEqual(board.Row(0)[7], 4);
            Assert.AreEqual(board.Row(0)[8], 7);

            Assert.AreEqual(board.Row(8)[0], 1);
            Assert.AreEqual(board.Row(8)[1], 7);
            Assert.AreEqual(board.Row(8)[2], 3);
            Assert.AreEqual(board.Row(8)[3], 2);
            Assert.AreEqual(board.Row(8)[4], 9);
            Assert.AreEqual(board.Row(8)[5], 6);
            Assert.AreEqual(board.Row(8)[6], 4);
            Assert.AreEqual(board.Row(8)[7], 5);
            Assert.AreEqual(board.Row(8)[8], 8);
        }

        [TestMethod]
        public void Col_Extraction()
        {
            var board = new Board
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

            Assert.AreEqual(board.Col(0)[0], 6);
            Assert.AreEqual(board.Col(0)[1], 7);
            Assert.AreEqual(board.Col(0)[2], 3);
            Assert.AreEqual(board.Col(0)[3], 4);
            Assert.AreEqual(board.Col(0)[4], 9);
            Assert.AreEqual(board.Col(0)[5], 5);
            Assert.AreEqual(board.Col(0)[6], 2);
            Assert.AreEqual(board.Col(0)[7], 8);
            Assert.AreEqual(board.Col(0)[8], 1);

            Assert.AreEqual(board.Col(8)[0], 7);
            Assert.AreEqual(board.Col(8)[1], 3);
            Assert.AreEqual(board.Col(8)[2], 5);
            Assert.AreEqual(board.Col(8)[3], 1);
            Assert.AreEqual(board.Col(8)[4], 6);
            Assert.AreEqual(board.Col(8)[5], 4);
            Assert.AreEqual(board.Col(8)[6], 9);
            Assert.AreEqual(board.Col(8)[7], 2);
            Assert.AreEqual(board.Col(8)[8], 8);
        }

    }
}
