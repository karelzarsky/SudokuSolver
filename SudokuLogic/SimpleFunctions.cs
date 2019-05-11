using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SudokuLogic
{
    public static class SimpleFunctions
    {
        /// <summary>
        /// Calculate index of box from coordinates
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        public static int BoxNumber(int x, int y) => x / 3 % 3 + y / 3 * 3;

        /// <summary>
        /// Calculate X coord from box and cell index
        /// </summary>
        /// <param name="box"></param>
        /// <param name="index"></param>
        public static int BoxX(int box, int index) => box % 3 * 3 + index % 3;

        /// <summary>
        /// Calculate Y coord from box and cell index
        /// </summary>
        /// <param name="box"></param>
        /// <param name="index"></param>
        public static int BoxY(int box, int index) => box / 3 * 3 + index / 3;
    }
}
