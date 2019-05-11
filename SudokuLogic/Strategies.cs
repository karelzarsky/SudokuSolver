using System;
using System.Linq;
using static SudokuLogic.SimpleFunctions;

namespace SudokuLogic
{
    public static class Strategies
    {
        /// <summary>
        /// Remove obvious possibilities
        /// </summary>
        /// <returns>Count of removed possibilities</returns>
        public static int BasicPossibilitiesReduction(Board b)
        {
            int removedCounter = 0;
            for (int i = 0; i < 9; i++)
                for (int j = 0; j < 9; j++)
                {
                    if (b.GetNumber(i, j) != 0)
                    {
                        byte num = b.GetNumber(i, j);
                        int boxNum = GetBoxNumber(i, j);
                        for (int k = 0; k < 9; k++)
                        {
                            if (b.possibilities[i, k, num])
                            {
                                b.possibilities[i, k, num] = false;
                                removedCounter++;
                            }
                            if (b.possibilities[k, j, num])
                            {
                                b.possibilities[k, j, num] = false;
                                removedCounter++;
                            }

                            if (b.possibilities[GetColumnNumber(boxNum, k), GetRowNumber(boxNum, k), num])
                            {
                                b.possibilities[GetColumnNumber(boxNum, k), GetRowNumber(boxNum, k), num] = false;
                                removedCounter++;
                            }
                        }
                    }
                }
            return removedCounter;
        }

        /// <summary>
        /// Looks for cells with only one possible number and fills it to solution matrix
        /// </summary>
        /// <param name="b">playing board</param>
        /// <returns>number of filled cells</returns>
        public static int CheckForSolvedCells(Board b)
        {
            int solvedCounter = 0;
            for (int i = 0; i < 9; i++)
                for (int j = 0; j < 9; j++)
                {
                    if (1 == Enumerable.Range(1, 9).Count(number => b.possibilities[i, j, number]))
                    {
                        if (!b.CellEmpty(i, j)) continue;
                        if (!b.TrySetNumber(i, j,
                            (byte) Enumerable.Range(1, 9).First(number => b.possibilities[i, j, number])))
                        {
                            throw new ArgumentException();
                        }
                        solvedCounter++;
                        BasicPossibilitiesReduction(b);
                    }
                }
            return solvedCounter;
        }
    }
}
