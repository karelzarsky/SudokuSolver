using System.Windows.Forms;

namespace SudokuLogic
{
    internal class ViewModel
    {
        private Board model { get; } = new Board();
        public Panel[,] Cells { get; set; } = new Panel[9, 9];
        public TableLayoutPanelCellPosition Selected { get; set; }

        internal void FillRandomCells(int v) => model.FillRandomCells(v);

        internal bool TrySetNumber(int selectedColumn, int selectedRow, byte number, Source source) => model.TrySetNumber(selectedColumn, selectedRow, number, source);

        internal string GetNumberAsString(int col, int row) => model.GetNumberAsString(col, row);

        internal void Clear() => model.Clear();

        internal void BasicPossibilitiesReduction() => Strategies.BasicPossibilitiesReduction(model);

        internal bool CellEmpty(int col, int row) => model.CellEmpty(col, row);

        internal string HintText(int col, int row, int num) => model.Possibilities[col, row, num] ? num.ToString() : "";

        public override string ToString() => model.ToString();

        internal void FillFromString(string value) => model.FillFromString(value);

        internal void CheckForSolvedCells() => Strategies.CheckForSolvedCells(model);

        internal void BruteForce() => Strategies.BruteForce(model);

        internal void FindHiddenSingles() => Strategies.FindHiddenSingles(model);

        internal void EliminateIntersections() => Strategies.EliminateIntersections(model);
    }
}
