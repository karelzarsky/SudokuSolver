using System;
using System.Linq;
using static SudokuLogic.SimpleFunctions;

namespace SudokuLogic
{
    public static class Strategies
    {
        /// <summary>
        /// Remove obvious Possibilities
        /// </summary>
        /// <returns>Count of removed Possibilities</returns>
        public static int BasicPossibilitiesReduction(Board b)
        {
            int removedCounter = 0;
            for (int c = 0; c < Board.Size; c++)
            {
                for (int r = 0; r < Board.Size; r++)
                {
                    byte num = b.GetNumber(c, r);
                    if (num != 0)
                    {
                        int boxNum = GetBoxNumber(c, r);
                        for (int k = 0; k < Board.Size; k++)
                        {
                            b.Possibilities[c, r, k] = false;
                            if (b.Possibilities[c, k, num])
                            {
                                b.Possibilities[c, k, num] = false;
                                removedCounter++;
                            }
                            if (b.Possibilities[k, r, num])
                            {
                                b.Possibilities[k, r, num] = false;
                                removedCounter++;
                            }

                            if (b.Possibilities[GetColNumber(boxNum, k), GetRowNumber(boxNum, k), num])
                            {
                                b.Possibilities[GetColNumber(boxNum, k), GetRowNumber(boxNum, k), num] = false;
                                removedCounter++;
                            }
                        }
                    }
                }
            }
            return removedCounter;
        }

        /// <summary>
        /// Looks for cells with only one possible number and fills it to Solution matrix
        /// </summary>
        /// <param name="b">playing board</param>
        /// <returns>number of filled cells</returns>
        public static int CheckForSolvedCells(Board b)
        {
            int solvedCounter = 0;
            for (int c = 0; c < Board.Size; c++)
            {
                for (int r = 0; r < Board.Size; r++)
                {
                    if (1 == Enumerable.Range(1, Board.Size).Count(number => b.Possibilities[c, r, number]))
                    {
                        if (!b.IsCellEmpty(c, r))
                        {
                            continue;
                        }

                        if (!b.TrySetNumber(c, r,
                            (byte)Enumerable.Range(1, Board.Size).First(number => b.Possibilities[c, r, number]), Origin.SolverFresh))
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

        /// <summary>
        /// Recursively tries all combinations until first solution is found.
        /// </summary>
        /// <returns>True if there is a solution</returns>
        public static bool BruteForceOneSolution(Board b)
        {
            var EmptyCell = GetEmptyCell(b);
            if (EmptyCell.Item1 == -1)
            {
                return true;
            }
            for (byte num = 1; num < 10; num++)
            {
                if (!b.Possibilities[EmptyCell.Item1, EmptyCell.Item2, num])
                {
                    continue;
                }

                if (b.TrySetNumber(EmptyCell.Item1, EmptyCell.Item2, num, Origin.BruteForce))
                {
                    if (BruteForceOneSolution(b))
                    {
                        return true;
                    }
                    else
                    {
                        b.Cells[EmptyCell.Item1, EmptyCell.Item2] = 0;
                    }
                }
            }
            return false;
        }

        /// <summary>
        /// Recursively tries all combinations.
        /// </summary>
        /// <param name="b"></param>
        /// <returns>Number of solutions</returns>
        public static int BruteForceAllSolutions(Board b)
        {
            var EmptyCell = GetEmptyCell(b);
            if (EmptyCell.Item1 == -1)
            {
                return 1;
            }

            int solutions = 0;
            for (byte num = 1; num < 10; num++)
            {
                if (!b.Possibilities[EmptyCell.Item1, EmptyCell.Item2, num])
                {
                    continue;
                }

                if (b.TrySetNumber(EmptyCell.Item1, EmptyCell.Item2, num, Origin.BruteForce))
                {
                    int found = BruteForceAllSolutions(b);
                    if (found > 0)
                    {
                        solutions += found;
                    }
                    else
                    {
                        b.Cells[EmptyCell.Item1, EmptyCell.Item2] = 0;
                    }
                }
            }
            return solutions;
        }

        /// <summary>
        /// Search for first empty cell
        /// </summary>
        /// <param name="b"></param>
        /// <returns>Coordinates of empty cell</returns>
        private static Tuple<int, int> GetEmptyCell(Board b)
        {
            for (int r = 0; r < Board.Size; r++)
            {
                for (int c = 0; c < Board.Size; c++)
                {
                    if (b.IsCellEmpty(c, r))
                    {
                        return new Tuple<int, int>(c, r);
                    }
                }
            }
            return new Tuple<int, int>(-1, -1);
        }

        /// <summary>
        /// Search for last remaining possibility in each column, row and box
        /// </summary>
        /// <param name="b"></param>
        public static int FindHiddenSingles(Board b)
        {
            int solvedCounter = 0;
            BasicPossibilitiesReduction(b);
            for (int i = 0; i < Board.Size; i++)
            {
                for (byte num = 1; num < 10; num++)
                {
                    var colPossibilities = b.GetPossibilitiesColumn(i, num).ToList();
                    if (colPossibilities.Count(x => x) == 1)
                    {
                        int indexOfSingle = colPossibilities.FindIndex(x => x);
                        if (b.TrySetNumber(i, indexOfSingle, num, Origin.SolverFresh))
                        {
                            solvedCounter++;
                        }
                    }
                    var rowPossibilities = b.GetPossibilitiesRow(i, num).ToList();
                    if (rowPossibilities.Count(x => x) == 1)
                    {
                        int indexOfSingle = rowPossibilities.FindIndex(x => x);
                        if (b.TrySetNumber(indexOfSingle, i, num, Origin.SolverFresh))
                        {
                            solvedCounter++;
                        }
                    }
                    var boxPossibilities = b.GetBoxPossibilities(i, num).ToList();
                    if (boxPossibilities.Count(x => x) == 1)
                    {
                        int indexOfSingle = boxPossibilities.FindIndex(x => x);
                        if (b.TrySetNumber(GetRowNumber(i, indexOfSingle), GetColNumber(i, indexOfSingle), num, Origin.SolverFresh))
                        {
                            solvedCounter++;
                        }
                    }
                }
            }
            return solvedCounter;
        }

        /// <summary>
        /// If any one number occurs twice or three times in just one unit (any row, column or box)
        /// then we can remove that number from the intersection of another unit.
        /// </summary>
        /// <param name="b"></param>
        public static int EliminateIntersections(Board b)
        {
            int eliminatedCounter = 0;
            BasicPossibilitiesReduction(b);
            for (int group = 0; group < Board.Size; group++)
            {
                for (byte num = 1; num < 10; num++)
                {
                    var boxCandidatesInRow = new int[3];
                    for (int r = 0; r < 3; r++)
                    {
                        boxCandidatesInRow[r] = Enumerable.Range(r * 3, 3).Count(x => b.Possibilities[GetColNumber(group, x), GetRowNumber(group, x), num]);
                    }
                    var sortedCounts = boxCandidatesInRow.ToList().OrderBy(x => x).ToArray();
                    if (sortedCounts[0] == 0 && sortedCounts[1] == 0 && sortedCounts[2] > 0)
                    {
                        var intersectionRow = Array.IndexOf(boxCandidatesInRow, sortedCounts[2]) + GetRowNumber(group, 0);
                        for (int c = 0; c < Board.Size; c++)
                        {
                            if (GetBoxNumber(c, intersectionRow) != @group && b.Possibilities[c, intersectionRow, num])
                            {
                                eliminatedCounter++;
                                b.Possibilities[c, intersectionRow, num] = false;
                            }
                        }
                    }

                    var boxCandidatesInCol = new int[3];
                    for (int c = 0; c < 3; c++)
                    {
                        boxCandidatesInCol[c] = Enumerable.Range(0, 3).Count(x => b.Possibilities[GetColNumber(group, x), GetRowNumber(group, x), num]);
                    }
                    var sortedCountsInCol = boxCandidatesInCol.ToList().OrderBy(x => x).ToArray();
                    if (sortedCountsInCol[0] == 0 && sortedCountsInCol[1] == 0 && sortedCountsInCol[2] > 0)
                    {
                        var IntersectionCol = Array.IndexOf(boxCandidatesInCol, sortedCountsInCol[2]) + GetColNumber(group, 0);
                        for (int r = 0; r < Board.Size; r++)
                        {
                            if (GetBoxNumber(IntersectionCol, r) != group)
                            {
                                if (b.Possibilities[IntersectionCol, r, num])
                                {
                                    eliminatedCounter++;
                                    b.Possibilities[IntersectionCol, r, num] = false;
                                }
                            }
                        }
                    }

                }
            }
            return eliminatedCounter;
        }

        private static int RemovePossibilitiesInRow(Board b, int r, byte num)
        {
            int removed = 0;
            for (int c = 0; c < 9; c++)
            {
                if (b.Possibilities[c, r, num])
                {
                    b.Possibilities[c, r, num] = false;
                    removed++;
                }
            }
            return removed;
        }
    }
}
