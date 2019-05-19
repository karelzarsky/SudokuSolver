using System;
using System.Drawing;
using System.Windows.Forms;

namespace SudokuLogic
{
    internal class ViewModel
    {
        private Board model { get; } = new Board();
        public Panel[,] Cells { get; set; } = new Panel[9, 9];
        public TableLayoutPanelCellPosition Selected { get; set; }
        Color[] NumberColors;

        public ViewModel()
        {
            NumberColors = new Color[Enum.GetNames(typeof(Origin)).Length];
        }

        public bool Solved => model.IsSolved();

        internal int FillRandomCells(int v) => model.FillRandomCells(v);

        internal bool TrySetNumber(int selectedColumn, int selectedRow, byte number, Origin origin) => model.TrySetNumber(selectedColumn, selectedRow, number, origin);

        internal string GetNumberAsString(int col, int row) => model.GetNumberAsString(col, row);

        internal void Clear() => model.Clear();

        internal void BasicPossibilitiesReduction() => Strategies.BasicPossibilitiesReduction(model);

        internal bool CellEmpty(int col, int row) => model.CellEmpty(col, row);

        internal string HintText(int col, int row, int num) => model.Possibilities[col, row, num] ? num.ToString() : "";

        public override string ToString() => model.ToString();

        internal void FillFromString(string value) => model.FillFromString(value);

        internal int CheckForSolvedCells() => Strategies.CheckForSolvedCells(model);

        internal bool BruteForce() => Strategies.BruteForceOneSolution(model);

        internal int FindHiddenSingles() => Strategies.FindHiddenSingles(model);

        internal int EliminateIntersections() => Strategies.EliminateIntersections(model);

        internal Control BigNumberLabel(int col, int row) => Cells[col, row].Controls[0];

        internal Control HintPanel(int col, int row) => Cells[col, row].Controls[1];

        internal Color GetForeColor(int col, int row)
        {
            switch (model.Origins[col, row])
            {
                case Origin.Human: return Color.DodgerBlue;
                case Origin.SolverFresh: return Color.Chartreuse;
                case Origin.SolverOld: return Color.DarkGreen;
                case Origin.BruteForce: return Color.Blue;
            }
            return Color.Black;
        }

        public void ColorChangeFreshToOld()
        {
            for (int c = 0; c < 9; c++)
                for (int r = 0; r < 9; r++)
                    if (model.Origins[c, r] == Origin.SolverFresh)
                        model.Origins[c, r] = Origin.SolverOld;
        }
    }
}
