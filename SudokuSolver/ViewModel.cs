using System.Windows.Forms;

namespace SudokuLogic
{
    internal class ViewModel
    {
        private Board model { get; } = new Board();
        public Panel[,] Cells { get; set; } = new Panel[9, 9];
        public TableLayoutPanelCellPosition Selected { get; set; }
        public bool Solved => model.IsSolutionSolved();

        internal int FillRandomCells(int v) => model.FillRandomCells(v);

        internal bool TrySetNumber(int selectedColumn, int selectedRow, byte number, Source source) => model.TrySetNumber(selectedColumn, selectedRow, number, source);

        internal string GetNumberAsString(int col, int row) => model.GetNumberAsString(col, row);

        internal void Clear() => model.Clear();

        internal void BasicPossibilitiesReduction() => Strategies.BasicPossibilitiesReduction(model);

        internal bool CellEmpty(int col, int row) => model.CellEmpty(col, row);

        internal string HintText(int col, int row, int num) => model.Possibilities[col, row, num] ? num.ToString() : "";

        public override string ToString() => model.ToString();

        internal void FillFromString(string value) => model.FillFromString(value);

        internal int CheckForSolvedCells() => Strategies.CheckForSolvedCells(model);

        internal int BruteForce() => Strategies.BruteForce(model);

        internal int FindHiddenSingles() => Strategies.FindHiddenSingles(model);

        internal int EliminateIntersections() => Strategies.EliminateIntersections(model);

        internal Control BigNumberLabel(int col, int row) => Cells[col, row].Controls[0];

        internal Control HintPanel(int col, int row) => Cells[col, row].Controls[1];
    }
}
