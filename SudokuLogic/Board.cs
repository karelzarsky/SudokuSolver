using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using static SudokuLogic.SimpleFunctions;

namespace SudokuLogic
{
    /// <summary>
    /// Data structure for playing board
    /// </summary>
    public class Board
    {
        public byte[,] Solution = new byte[9, 9]; // indexes [column, row]
        public bool[,,] Possibilities = new bool[9, 9, 10]; // False means coordinates I, J cannot be filled with number K
        public Source[,] Sources = new Source[9,9];

        public Board()
        {
            FillAllPossibilitiesTrue();
        }

        public IEnumerable<byte> GetSolutionRow(int rowNumber) =>
            Enumerable.Range(0, Solution.GetLength(0)).Select(x => Solution[x, rowNumber]);

        public IEnumerable<byte> GetSolutionColumn(int colNumber) =>
            Enumerable.Range(0, Solution.GetLength(1)).Select(x => Solution[colNumber, x]);

        public IEnumerable<byte> GetSolutionBox(int boxNr) =>
            Enumerable.Range(0, 9).Select(i => Solution[GetRowNumber(boxNr, i), GetColNumber(boxNr, i)]);

        public IEnumerable<bool> GetPossibilitiesRow(int rowNumber, byte number) =>
            Enumerable.Range(0, Possibilities.GetLength(0)).Select(x => Possibilities[x, rowNumber, number]);

        public IEnumerable<bool> GetPossibilitiesColumn(int colNumber, byte number) =>
            Enumerable.Range(0, Possibilities.GetLength(1)).Select(x => Possibilities[colNumber, x, number]);

        public IEnumerable<bool> GetBoxPossibilities(int boxNr, byte number) =>
            Enumerable.Range(0, 9).Select(i => Possibilities[GetRowNumber(boxNr, i), GetColNumber(boxNr, i), number]);

        public bool IsSolutionSolved()
        {
            for (int i = 0; i < 9; i++)
            {
                if (!IsGroupSolved(GetSolutionColumn(i))) return false;
                if (!IsGroupSolved(GetSolutionRow(i))) return false;
                if (!IsGroupSolved(GetSolutionBox(i))) return false;
            }
            return true;
        }

        public bool IsSolutionValid()
        {
            for (int i = 0; i < 9; i++)
            {
                if (!IsGroupValid(GetSolutionColumn(i))) return false;
                if (!IsGroupValid(GetSolutionRow(i))) return false;
                if (!IsGroupValid(GetSolutionBox(i))) return false;
            }
            return true;
        }

        public bool CellEmpty(int i, int j) => Solution[i, j] == 0;

        public void FillAllPossibilitiesTrue()
        {
            for (int c = 0; c < 9; c++)
                for (int r = 0; r < 9; r++)
                    for (int num = 0; num < 10; num++)
                        Possibilities[c, r, num] = true;
        }

        public int FillRandomCells(int count)
        {
            int filledCounter = 0;
            Random rnd = new Random();
            for (int i = 0; i < count; i++)
            {
                for (int attempt = 0; attempt < 1000; attempt++)
                {
                    int col = rnd.Next(9);
                    int row = rnd.Next(9);
                    if (Solution[col, row] != 0)
                        continue;
                    if (TrySetNumber(col, row, (byte) (rnd.Next(9) + 1), Source.Random))
                    {
                        filledCounter++;
                        break;
                    }
                }
            }
            return filledCounter;
        }

        public override string ToString()
        {
            var sb = new StringBuilder();
            for (int c = 0; c < 9; c++)
            {
                for (int r = 0; r < 9; r++)
                {
                    sb.Append(Solution[c, r].ToString());
                }
            }
            return sb.ToString();
        }

        public void FillFromString(string content)
        {
            if (content.Length < 81) return;
            Clear();
            int characterCounter = 0;
            for (int c = 0; c < 9; c++)
            {
                for (int r = 0; r < 9; r++)
                {
                    byte.TryParse(content[characterCounter++].ToString(), out Solution[c, r]);
                    if (Solution[c, r] > 0)
                        Sources[c, r] = Source.File;
                }
            }
            FillAllPossibilitiesTrue();
        }

        public void Clear()
        {
            for (int c = 0; c < 9; c++)
            {
                for (int r = 0; r < 9; r++)
                {
                    Solution[c, r] = 0;
                    Sources[c, r] = Source.Empty;
                }
            }
            FillAllPossibilitiesTrue();
        }

        public byte GetNumber(int col, int row) => Solution[col, row];

        public string GetNumberAsString(int col, int row) => Solution[col, row] == 0 ? "" : Solution[col, row].ToString();

        public void SetSolution(byte[,] v) => Solution = v;

        /// <summary>
        /// Fills number in entered coordinates. Tests for validity.
        /// </summary>
        /// <param name="col">Column</param>
        /// <param name="row">Row</param>
        /// <param name="value">number to be entered</param>
        /// <param name="s">source</param>
        /// <returns>True for success. False for invalid entry.</returns>
        public bool TrySetNumber(int col, int row, byte value, Source s)
        {
            byte oldValue = Solution[col, row];
            Solution[col, row] = value;
            if (IsSolutionValid())
            {
                Sources[col, row] = s;
                return true;
            }
            Solution[col, row] = oldValue;
            return false;
        }

        public int GetNumberOfFilled()
        {
            int res = 0;
            foreach (var n in Solution)
            {
                if (n > 0) res++;
            }
            return res;
        }
    }
}
