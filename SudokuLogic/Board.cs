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
        public byte[,] submission = new byte[9, 9]; // Number 0 means empty
        private byte[,] solution = new byte[9, 9];
        public bool[,,] possibilities = new bool[9, 9, 10]; // False means coordinates I, J cannot be filled with number K

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

        internal bool CellEmpty(int i, int j) => solution[i, j] == 0;

        public void FillAllPossibilitiesTrue()
        {
            for (int i = 0; i < 9; i++)
                for (int j = 0; j < 9; j++)
                    for (int k = 0; k < 10; k++)
                        possibilities[i, j, k] = true;
        }

        public void CopySubmissionToSolution()
        {
            for (int i = 0; i < 9; i++)
                for (int j = 0; j < 9; j++)
                    solution[i, j] = submission[i, j];
        }

        public void FillRandomCells(int count)
        {
            Random rnd = new Random();
            for (int i = 0; i < count; i++)
            {
                for (int attempt = 0; attempt < 1000; attempt++)
                {
                    int x = rnd.Next(9);
                    int y = rnd.Next(9);
                    if (solution[x, y] != 0) continue;
                    solution[x, y] = (byte)(rnd.Next(9) + 1);
                    if (IsSolutionValid()) break;
                    solution[x, y] = 0;
                }
            }
        }

        public override string ToString()
        {
            var sb = new StringBuilder();
            for (int i = 0; i < 9; i++)
            {
                for (int j = 0; j < 9; j++)
                {
                    sb.Append(solution[i, j].ToString());
                }
            }
            return sb.ToString();
        }

        public void FillFromString(string content)
        {
            if (content.Length < 81) return;
            int characterCounter = 0;
            for (int i = 0; i < 9; i++)
            {
                for (int j = 0; j < 9; j++)
                {
                    byte.TryParse(content[characterCounter++].ToString(), out solution[i, j]);
                }
            }
            FillAllPossibilitiesTrue();
        }

        public void Clear()
        {
            for (int i = 0; i < 9; i++)
            {
                for (int j = 0; j < 9; j++)
                {
                    solution[i, j] = 0;
                }
            }
            FillAllPossibilitiesTrue();
        }

        public byte GetNumber(int col, int row) => solution[col, row];

        public string GetNumberAsString(int col, int row) => solution[col, row] == 0 ? "" : solution[col, row].ToString();

        public void SetSolution(byte[,] v) => solution = v;

        public bool TrySetNumber(int col, int row, byte value)
        {
            byte oldValue = solution[col, row];
            solution[col, row] = value;
            if (IsSolutionValid())
            {
                return true;
            }
            solution[col, row] = oldValue;
            return false;
        }
    }
}
