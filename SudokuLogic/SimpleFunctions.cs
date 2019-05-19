using System.Collections.Generic;
using System.Linq;

namespace SudokuLogic
{
    public static class SimpleFunctions
    {
        /// <summary>
        /// Check whether group contains all numbers 1-9 exactly once.
        /// Group can be column, row or box. Cells can't be empty (0).
        /// </summary>
        /// <param name="group"></param>
        public static bool IsGroupSolved(IEnumerable<byte> group) =>
            group.Count() == 9
            && Enumerable.Range(1, 9).All(seekingNumber => group.Count(x => x == seekingNumber) == 1);

        /// <summary>
        /// Check whether group is correct.
        /// No number can occur more than once.
        /// Some cells can be empty (0).
        /// </summary>
        /// <param name="group"></param>
        public static bool IsGroupValid(IEnumerable<byte> group)
        {
            return group.Count() == 9
                && group.All(x => x < 10)
                && Enumerable.Range(1, 9).All(seekingNumber => group.Count(x => x == seekingNumber) <= 1);
        }

        /// <summary>
        /// Calculate index of box from coordinates
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        public static int GetBoxNumber(int x, int y) => x / 3 % 3 + y / 3 * 3;

        /// <summary>
        /// Calculate global board row number from box number and cell index inside box
        /// </summary>
        /// <param name="box"></param>
        /// <param name="index"></param>
        public static int GetColNumber(int box, int index) => box % 3 * 3 + index % 3;

        /// <summary>
        /// Calculate global board column number from box number and cell index inside box
        /// </summary>
        /// <param name="box"></param>
        /// <param name="index"></param>
        public static int GetRowNumber(int box, int index) => box / 3 * 3 + index / 3;
    }
}
