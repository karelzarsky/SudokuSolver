using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
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
            {
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
            {
                for (int j = 0; j < 9; j++)
                {
                    if (1 == Enumerable.Range(1, 9).Count(number => b.possibilities[i, j, number]))
                    {
                        if (!b.CellEmpty(i, j))
                        {
                            continue;
                        }

                        if (!b.TrySetNumber(i, j,
                            (byte)Enumerable.Range(1, 9).First(number => b.possibilities[i, j, number]), Source.Solver))
                        {
                            throw new ArgumentException();
                        }
                        solvedCounter++;
                        BasicPossibilitiesReduction(b);
                    }
                }
            }

            return solvedCounter;
        }

        public static int BruteForce(Board b)
        {
            var EmptyCell = GetEmptyCell(b);
            if (EmptyCell.Column == 10)
            {
                return 1;
            }

            int solutions = 0;
            for (byte num = 1; num < 10; num++)
            {
                if (!b.possibilities[EmptyCell.Column, EmptyCell.Row, num]) continue;
                if (b.TrySetNumber(EmptyCell.Column, EmptyCell.Row, num, Source.BruteForce))
                {
                    int found = BruteForce(b);
                    if (found > 0)
                    {
                        solutions += found;
                    }
                    else
                    {
                        b.solution[EmptyCell.Column, EmptyCell.Row] = 0;
                    }
                }
            }
            return solutions;
        }

        private static TableLayoutPanelCellPosition GetEmptyCell(Board b)
        {
            for (int i = 0; i < 9; i++)
            {
                for (int j = 0; j < 9; j++)
                {
                    if (b.CellEmpty(i, j))
                    {
                        return new TableLayoutPanelCellPosition(i, j);
                    }
                }
            }
            return new TableLayoutPanelCellPosition(10, 10);
        }

        private static IEnumerable<TableLayoutPanelCellPosition> GetEmptyCells(Board b)
        {
            var res = new List<TableLayoutPanelCellPosition>();
            for (int i = 0; i < 9; i++)
            {
                for (int j = 0; j < 9; j++)
                {
                    if (b.CellEmpty(i, j))
                    {
                        res.Add(new TableLayoutPanelCellPosition(i, j));
                    }
                }
            }
            return res;
        }
    }
}
