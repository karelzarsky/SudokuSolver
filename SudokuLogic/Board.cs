using System;
using System.Collections.Generic;
using System.Linq;
using static SudokuLogic.SimpleFunctions;

namespace SudokuLogic
{
    /// <summary>
    /// Data structure for playing board
    /// </summary>
    public class Board
    {
        public byte[,] submission = new byte[9, 9]; // Number 0 means empty
        public byte[,] solution = new byte[9, 9];
        public bool[,,] possibilities = new bool[9, 9, 9]; // False means coordinates I, J cannot be filled with number K

        public Board()
        {
            FillAllPossibilitiesTrue();
        }

        public IEnumerable<byte> GetCol(int columnNumber) =>
            Enumerable.Range(0, solution.GetLength(0)).Select(x => solution[x, columnNumber]);

        public IEnumerable<byte> GetRow(int rowNumber) =>
            Enumerable.Range(0, solution.GetLength(1)).Select(x => solution[rowNumber, x]);

        public IEnumerable<byte> GetBox(int boxNr) =>
            Enumerable.Range(0, 9).Select(i => solution[GetRowNumber(boxNr, i), GetColumnNumber(boxNr, i)]);

        public bool IsSolutionSolved()
        {
            for (int i = 0; i < 9; i++)
            {
                if (!IsGroupSolved(GetRow(i))) return false;
                if (!IsGroupSolved(GetCol(i))) return false;
                if (!IsGroupSolved(GetBox(i))) return false;
            }
            return true;
        }

        public bool IsSolutionValid()
        {
            for (int i = 0; i < 9; i++)
            {
                if (!IsGroupValid(GetRow(i))) return false;
                if (!IsGroupValid(GetCol(i))) return false;
                if (!IsGroupValid(GetBox(i))) return false;
            }
            return true;
        }

        public void FillAllPossibilitiesTrue()
        {
            for (int i = 0; i < 9; i++)
                for (int j = 0; j < 9; j++)
                    for (int k = 0; k < 9; k++)
                        possibilities[i, j, k] = true;
        }

        public void CopySubmissionToSolution()
        {
            for (int i = 0; i < 9; i++)
                for (int j = 0; j < 9; j++)
                    solution[i, j] = submission[i, j];
        }
    }
}
