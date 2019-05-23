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
        public const int Size = 9;
        public byte[,] Cells = new byte[Size, Size]; // indexes: [column, row]
        public bool[,,] Possibilities = new bool[Size, Size, 10]; // False means coordinates I, J cannot be filled with number K
        public Origin[,] Origins = new Origin[Size, Size]; // Where that numbers come from

        public Board()
        {
            SetAllPossibilitiesTrue();
        }

        public IEnumerable<byte> GetRow(int rowNumber) =>
            Enumerable.Range(0, Size).Select(x => Cells[x, rowNumber]);

        public IEnumerable<byte> GetColumn(int colNumber) =>
            Enumerable.Range(0, Size).Select(x => Cells[colNumber, x]);

        public IEnumerable<byte> GetBox(int boxNr) =>
            Enumerable.Range(0, Size).Select(i => Cells[GetRowNumber(boxNr, i), GetColNumber(boxNr, i)]);

        public IEnumerable<bool> GetPossibilitiesRow(int rowNumber, byte number) =>
            Enumerable.Range(0, Size).Select(x => Possibilities[x, rowNumber, number]);

        public IEnumerable<bool> GetPossibilitiesColumn(int colNumber, byte number) =>
            Enumerable.Range(0, Size).Select(x => Possibilities[colNumber, x, number]);

        public IEnumerable<bool> GetBoxPossibilities(int boxNr, byte number) =>
            Enumerable.Range(0, Size).Select(i => Possibilities[GetRowNumber(boxNr, i), GetColNumber(boxNr, i), number]);

        public bool IsSolved()
        {
            for (int i = 0; i < Size; i++)
            {
                if (!IsGroupSolved(GetColumn(i))) return false;
                if (!IsGroupSolved(GetRow(i))) return false;
                if (!IsGroupSolved(GetBox(i))) return false;
            }
            return true;
        }

        public bool IsValid()
        {
            for (int i = 0; i < Size; i++)
            {
                if (!IsGroupValid(GetColumn(i))) return false;
                if (!IsGroupValid(GetRow(i))) return false;
                if (!IsGroupValid(GetBox(i))) return false;
            }
            return true;
        }

        public bool IsCellEmpty(int i, int j) => Cells[i, j] == 0;

        public void SetAllPossibilitiesTrue()
        {
            for (int c = 0; c < Size; c++)
                for (int r = 0; r < Size; r++)
                    for (int num = 0; num < 10; num++)
                        Possibilities[c, r, num] = true;
        }

        /// <summary>
        /// Fills ramdom cells with ramdom number.
        /// Numbers will not collide with other in column, row and box.
        /// Board can be insolvable.
        /// </summary>
        /// <param name="count">Number of cell to fill</param>
        /// <returns>Number of really filled cells. Can be lower than requested if no valid numbers are found.</returns>
        public int FillRandomCells(int count)
        {
            int filledCounter = 0;
            Random rnd = new Random();
            for (int i = 0; i < count; i++)
            {
                for (int attempt = 0; attempt < 1000; attempt++)
                {
                    int col = rnd.Next(Size);
                    int row = rnd.Next(Size);
                    if (Cells[col, row] != 0)
                        continue;
                    if (TrySetNumber(col, row, (byte) (rnd.Next(9) + 1), Origin.Random))
                    {
                        filledCounter++;
                        break;
                    }
                }
            }
            return filledCounter;
        }

        /// <summary>
        /// Return board as single string.
        /// Used for saving to file.
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            var sb = new StringBuilder();
            for (int c = 0; c < Size; c++)
            {
                for (int r = 0; r < Size; r++)
                {
                    sb.Append(Cells[c, r].ToString());
                }
            }
            return sb.ToString();
        }

        /// <summary>
        /// Fill board from string
        /// </summary>
        /// <param name="content"></param>
        public void FillFromString(string content)
        {
            if (content.Length != Size*Size) return;
            Clear();
            int characterCounter = 0;
            for (int c = 0; c < Size; c++)
            {
                for (int r = 0; r < Size; r++)
                {
                    byte.TryParse(content[characterCounter++].ToString(), out Cells[c, r]);
                    if (Cells[c, r] > 0)
                        Origins[c, r] = Origin.File;
                }
            }
            SetAllPossibilitiesTrue();
        }

        public void Clear()
        {
            for (int c = 0; c < Size; c++)
            {
                for (int r = 0; r < Size; r++)
                {
                    Cells[c, r] = 0;
                    Origins[c, r] = Origin.Empty;
                }
            }
            SetAllPossibilitiesTrue();
        }

        public byte GetNumber(int col, int row) => Cells[col, row];

        public string GetNumberAsString(int col, int row) => Cells[col, row] == 0 ? "" : Cells[col, row].ToString();

        public void SetSolution(byte[,] v) => Cells = v;

        /// <summary>
        /// Fills number in entered coordinates. Tests for validity.
        /// </summary>
        /// <param name="col">Column</param>
        /// <param name="row">Row</param>
        /// <param name="value">number to be entered</param>
        /// <param name="o">origin</param>
        /// <returns>True for success. False for invalid entry.</returns>
        public bool TrySetNumber(int col, int row, byte value, Origin o)
        {
            byte oldValue = Cells[col, row];
            Cells[col, row] = value;
            if (IsValid())
            {
                Origins[col, row] = o;
                return true;
            }
            Cells[col, row] = oldValue;
            return false;
        }

        public int GetNumberOfFilled()
        {
            int res = 0;
            foreach (var n in Cells)
            {
                if (n > 0) res++;
            }
            return res;
        }
    }
}
